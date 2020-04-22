using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Anda.Fluid.Infrastructure.Tasker;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.UI;
using System.Threading;
using System.Diagnostics;

namespace Anda.Fluid.Infrastructure.Alarming
{
    public sealed class AlarmServer : TaskLoop 
    {
        private readonly static AlarmServer instance = new AlarmServer();
        private List<IAlarmObservable> senderList = new List<IAlarmObservable>();
        private ConcurrentQueue<AlarmEvent> alarmEventQueue = new ConcurrentQueue<AlarmEvent>();

        private IRealTimeAlarmObservable realtimeAlarmHandle;
        private IAlarmLightable litghtTower;
        private IAlarmDiObservable diObserver;

        private ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>> alarmDic = new ConcurrentDictionary<string, Tuple<IAlarmSenderable, AlarmInfo>>();
        private ConcurrentDictionary<string, AlarmInfo> AutoImmediateAlarmDic = new ConcurrentDictionary<string, AlarmInfo>();
        private ConcurrentDictionary<string, AlarmInfo> AutoDelayAlarmDic = new ConcurrentDictionary<string, AlarmInfo>();
        private ConcurrentDictionary<DateTime, AlarmInfo> delayAlarmDic = new ConcurrentDictionary<DateTime, AlarmInfo>();

        /// <summary>
        /// 记录上一次声光提示所有alarmDic文本，如果有不一样的更新声光提示
        /// </summary>
        private List<string> lastAlarmDicInfos = new List<string>();
        /// <summary>
        /// 记录上一次声光提示所有delayAlarmDic文本，如果有不一样的更新声光提示
        /// </summary>
        private List<string> lastDelayAlarmDicInfos = new List<string>();

        private string lastImmediateAlarmInfo = "";

        private ConcurrentQueue<string> curDelayAlarmDicInfos = new ConcurrentQueue<string>();
        /// <summary>
        /// 当前所有立即抛出报警的提示
        /// </summary>
        private string immediateAlarmInfo = "";

        private object obj = new object();

        public bool MachineInitDone { get; set; } = false;
        public bool StopAlarmVoice { get; set; } = false;
        public static AlarmServer Instance => instance;

        public Func<Dictionary<DialogResult, Action>, List<string>, DialogResult> OnAlarmFormShown;

        public void Register(IAlarmObservable observer)
        {
            if (this.senderList.Contains(observer))
            {
                return;
            }
            this.senderList.Add(observer);
        }

        public void Register(IRealTimeAlarmObservable observer)
        {
            this.realtimeAlarmHandle = observer;
        }

        public void Register(IAlarmLightable observer)
        {
            this.litghtTower = observer;
        }

        public void Register(IAlarmDiObservable observer)
        {
            if (this.diObserver != null)
            {
                return;
            }
            this.diObserver = observer;
        }
        public void UnRegister(IAlarmObservable observer)
        {
            if (this.senderList.Contains(observer))
            {
                this.senderList.Remove(observer);
            }
        }

        public void UnRegister(IAlarmDiObservable observer)
        {
            this.diObserver = null;
        }

        public void Fire(IAlarmSenderable sender, AlarmInfo info)
        {
            if (info.HandleType == AlarmHandleType.OnlyRecord)
            {
                this.alarmEventQueue.Enqueue(AlarmEvent.Create(sender, info));
            }
            else if (info.HandleType == AlarmHandleType.DelayHandle)
            {
                this.FireAndDelayHandle(sender, info);
            }
            else if (info.HandleType == AlarmHandleType.AutoHandle
                || info.HandleType == AlarmHandleType.AutoAndImmeDiateHandle
                || info.HandleType == AlarmHandleType.AutoAndDelayHandle)
            {
                this.FireAndAutoHandle(sender, info);
            }
            else if (info.HandleType == AlarmHandleType.ImmediateHandle)
            {               
                Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
                dic.Add(DialogResult.Ignore, new Action(() => { }));
                this.FireAndShowMsgBox(sender, info, dic);
            }
        }

        /// <summary>
        /// 丢出需要多种处理的报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        /// <param name="dic"></param>
        public DialogResult? Fire(IAlarmSenderable sender,AlarmInfo info, Dictionary<DialogResult, Action> dic)
        {
            if (info.HandleType == AlarmHandleType.ImmediateHandle)
            {
                return this.FireAndShowMsgBox(sender, info, dic);
            }
            else
            {
                return DialogResult.Ignore;
            }
        }

        /// <summary>
        /// 丢出可消除的报警（如轴状态、报警信号）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        private void FireAndAutoHandle(IAlarmSenderable sender, AlarmInfo info)
        {
            if (this.alarmDic.ContainsKey(string.Format("{0}:{1}",sender.ToString(),info.Message)))
            {
                return;
            }
            else
            {
                if (this.alarmDic.TryAdd(string.Format("{0}:{1}", sender.ToString(), info.Message), new Tuple<IAlarmSenderable, AlarmInfo>(sender, info))) 
                {
                    this.alarmEventQueue.Enqueue(AlarmEvent.Create(sender, info));
                    if (info.HandleType == AlarmHandleType.AutoAndImmeDiateHandle)
                    {
                        this.AutoImmediateAlarmDic.TryAdd(info.Message, info);
                    }
                    if (info.HandleType == AlarmHandleType.AutoAndDelayHandle)
                    {
                        this.AutoDelayAlarmDic.TryAdd(info.Message, info);
                    }
                }
            }

         
        }

        /// <summary>
        /// 消除报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        public void RemoveAlarm(IAlarmSenderable sender, AlarmInfo info)
        {
            if (this.alarmDic.ContainsKey(string.Format("{0}:{1}", sender.ToString(), info.Message)))
            {
                Tuple<IAlarmSenderable, AlarmInfo> tuple;
                this.alarmDic.TryRemove(string.Format("{0}:{1}", sender.ToString(), info.Message), out tuple);

                if (info.HandleType == AlarmHandleType.AutoAndImmeDiateHandle)
                {
                    AlarmInfo alarmInfo;
                    this.AutoImmediateAlarmDic.TryRemove(info.Message,out alarmInfo);
                }
                if (info.HandleType == AlarmHandleType.AutoAndDelayHandle)
                {
                    AlarmInfo alarmInfo;
                    this.AutoDelayAlarmDic.TryRemove(info.Message, out alarmInfo);
                }
            }

        }

        /// <summary>
        /// 丢出报警，立即处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        private DialogResult? FireAndShowMsgBox(IAlarmSenderable sender, AlarmInfo info, Dictionary<DialogResult, Action> dic)
        {
            //记录报警
            this.alarmEventQueue.Enqueue(AlarmEvent.Create(sender, info));
            if (!this.MachineInitDone)
            {
                return DialogResult.Cancel;
            }
            this.litghtTower.HandleAlarmEvent(AlarmHandleType.ImmediateHandle);
            // 处理报警
            string senderName = "";
            if (sender != null)
            {
                senderName = sender.Name;
            }
            //DialogResult result = new AlarmForm(dic, new List<string> { string.Format("{0}:{1}", senderName, info.Message)}).ShowDialog();
            //IAsyncResult ar = this.OnAlarmFormShown?.BeginInvoke(dic, new List<string> { string.Format("{0}:{1}", senderName, info.Message) }, null, null);
            //DialogResult? result = this.OnAlarmFormShown?.EndInvoke(ar);
            immediateAlarmInfo = string.Format("{0}:{1}", senderName, info.Message);
            DialogResult? result = this.OnAlarmFormShown?.Invoke(dic, new List<string> { string.Format("{0}:{1}", senderName, info.Message) });
            immediateAlarmInfo = "";
            return result;
        }

        /// <summary>
        /// 丢出报警，生产完当前板再做处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        private void FireAndDelayHandle(IAlarmSenderable sender, AlarmInfo info)
        {
            if (this.delayAlarmDic.TryAdd(info.DateTime, info))
            {
                this.alarmEventQueue.Enqueue(AlarmEvent.Create(sender, info));
            }
        }

        /// <summary>
        /// 处理当前板生产完的报警
        /// </summary>
        /// <param name="action"></param>
        public void HandleDelayAlarm(Action action)
        {
            if (this.delayAlarmDic.Count == 0 && this.AutoDelayAlarmDic.Count == 0) 
            {
                return;
            }

            //有报警时直接通过这个事件结束掉点胶程序
            action?.Invoke();

            this.litghtTower.HandleAlarmEvent(AlarmHandleType.DelayHandle);
            Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
            //dic.Add(DialogResult.Abort, action);
            dic.Add(DialogResult.Cancel, new Action(() => { }));

            List<string> alarmInfo = new List<string>();
            foreach (var item in this.delayAlarmDic)
            {
                alarmInfo.Add(string.Format("{0}:{1}", item.Value.Where, item.Value.Message));
                curDelayAlarmDicInfos.Enqueue(string.Format("{0}:{1}", item.Value.Where, item.Value.Message));
            }
            foreach (var item in AutoDelayAlarmDic)
            {
                alarmInfo.Add(string.Format("{0}:{1}", item.Value.Where, item.Value.Message));
            }
            new AlarmForm(dic,alarmInfo).ShowDialog();
            this.delayAlarmDic.Clear();
            string str;
            while (!curDelayAlarmDicInfos.TryDequeue(out str))
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 显示运行过程中的刷到的Di报警
        /// </summary>
        public void ShowDiAlarm()
        {
            //灯塔报警
            this.litghtTower.HandleAlarmEvent(AlarmHandleType.ImmediateHandle);
            // 处理报警
            Dictionary<DialogResult, Action> dic = new Dictionary<DialogResult, Action>();
            dic.Add(DialogResult.Ignore, new Action(() => { }));
            List<string> alarmMsg = new List<string>();
            foreach (var item in this.AutoImmediateAlarmDic)
            {
                alarmMsg.Add(item.Value.Message);
            }
            new AlarmForm(dic, alarmMsg).ShowDialog();
           
        }

        /// <summary>
        /// 获取当前报警的灯塔状态
        /// </summary>
        /// <param name="IdleFlag">是否是idle状态</param>
        /// <returns>返回灯色类型，是否闪烁，是否响蜂鸣器</returns>
        public Tuple<int, Tuple<bool,bool>> GetCurrentAlarmLightTowerState(int IdleFlag = 0)
        {
            //bool onceBeep = this.ExistNewAlarm();
            if (immediateAlarmInfo.Equals(""))
            {
                this.StopAlarmVoice = false;
            }
            if (IdleFlag != 0)
            {
                bool result = ExistNewAlarm();
                return new Tuple<int, Tuple<bool, bool>>(ExistAlarm() ? 3:1, new Tuple<bool, bool>(true, result));
            }
            else
            {
                if (ExistAlarm())
                {
                    return new Tuple<int, Tuple<bool, bool>>(3, new Tuple<bool, bool>(true, !this.StopAlarmVoice));
                }
            }
            
            // 无任何异常则绿灯常亮，不响蜂鸣器
            return new Tuple<int, Tuple<bool,bool>>(1, new Tuple<bool, bool>(false,false));
        }
        /// <summary>
        /// 判断当前是否产生新的报警
        /// </summary>
        /// <returns></returns>
        public bool ExistNewAlarm()
        {
            Debug.WriteLine(",curAlarm : " + alarmDic.Count + ",lastAlarm:" + lastAlarmDicInfos.Count);
            bool result = false;
            if (!immediateAlarmInfo.Equals(lastImmediateAlarmInfo))
            {
                result = true;
                goto EndReuslt;
            }
            if (lastAlarmDicInfos.Count == 0)
            {
                if (alarmDic.Count != 0)
                {
                    result = true;
                    goto EndReuslt;
                }
            }
            else
            {
                foreach (string item in alarmDic.Keys)
                {
                    bool curItemExist = false;
                    for (int j = 0; j < lastAlarmDicInfos.Count; j++)
                    {
                        if (item.Equals(lastAlarmDicInfos[j]))
                        {
                            curItemExist = true;
                            break;
                        }
                    }
                    if (!curItemExist)
                    {
                        result = true;
                        goto EndReuslt;
                    }
                }
            }
            if (lastDelayAlarmDicInfos.Count == 0)
            {
                if (curDelayAlarmDicInfos.Count != 0)
                {
                    result = true;
                    goto EndReuslt;
                }
            }
            else
            {
                foreach (string item in curDelayAlarmDicInfos)
                {
                    bool curItemExist = false;
                    for (int j = 0; j < lastDelayAlarmDicInfos.Count; j++)
                    {
                        if (item.Equals(lastDelayAlarmDicInfos[j]))
                        {
                            curItemExist = true;
                            break;
                        }
                    }
                    if (!curItemExist)
                    {
                        result = true;
                        goto EndReuslt;
                    }
                }
            }
        EndReuslt:
            lastImmediateAlarmInfo = immediateAlarmInfo.Clone() as string;
            lastAlarmDicInfos.Clear();
            lastDelayAlarmDicInfos.Clear();
            foreach (string item in alarmDic.Keys)
            {
                lastAlarmDicInfos.Add(item.Clone() as string);
            }
            foreach (string item in curDelayAlarmDicInfos)
            {
                lastDelayAlarmDicInfos.Add(item.Clone() as string);
            }
            return result;
        }

        public bool ExistAlarm()
        {
            return !(immediateAlarmInfo == "" && curDelayAlarmDicInfos.Count == 0 && alarmDic.Count == 0);
        }

        internal void StopVoice()
        {
            this.litghtTower.StopLightTower();
            this.StopAlarmVoice = true;
        }
        protected override void Loop()
        {

            AlarmEvent e;
            
            if (this.alarmEventQueue.TryDequeue(out e))
            {
                Debug.WriteLine(e.Info.Message);
                foreach (var item in this.senderList)
                {
                    if (item == null)
                    {
                        continue;
                    }
                    item.HandleAlarmEvent(e);
                }
            }

            if (this.AutoImmediateAlarmDic.Count != 0 && this.diObserver != null) 
            {
                this.diObserver.HnadleAlarmDi();
            }

            this.realtimeAlarmHandle?.HandleRealTimeAlarm(this.alarmDic);               

        }
    }
}
