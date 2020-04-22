using Anda.Fluid.Drive.DeviceType;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.MachineStates
{
    public enum LightTowerType
    {
        Yellow = 0,
        Green,
        Blue,
        Red
    }

    /// <summary>
    /// 用于其他地方传递声光动作类型
    /// </summary>
    public struct LightTowerMessage
    {
        /// <summary>
        /// 闪烁还是常亮
        /// </summary>
        public bool Shining;
        /// <summary>
        /// 显示颜色
        /// </summary>
        public LightTowerType Type;
        /// <summary>
        /// 是否响蜂鸣器
        /// </summary>
        public bool Beep;
        /// <summary>
        /// 信息类型
        /// 1：正常声光动作
        /// 2：报警声光动作
        /// 3：提示信息声光动作
        /// </summary>
        public int MessageType;
    }

    public class LightTowerItem
    {
        public LightTowerItem(LightTowerType type, bool shining)
        {
            this.Type = type;
            this.Shining = shining;
            this.IsBeep = false;
        }
        public LightTowerType Type { get; set; }
        public bool Shining { get; set; }
        public bool BoolValue { get; set; }
        public bool IsBeep { get; set; }

        public bool Equal(LightTowerItem other)
        {
            return this.Type == other.Type && this.Shining == other.Shining && this.IsBeep == other.IsBeep;
        }

        public void SetValue(LightTowerItem other)
        {
            this.Type = other.Type;
            this.IsBeep = other.IsBeep;
            this.Shining = other.Shining;
        }
    }

    public class LightTower
    {
        private DateTime dateTime;
        private LightTowerItem item = new LightTowerItem(LightTowerType.Red, false);
        private LightTowerItem lastItem = new LightTowerItem(LightTowerType.Red, false);

        public event Action<LightTowerType, bool> Opened;

        private ConcurrentQueue<LightTowerMessage> messageQueue = new ConcurrentQueue<LightTowerMessage>();

        public void SetMessage(LightTowerMessage msg)
        {
            // todo 接受生产提示的声光动作
            this.messageQueue.Enqueue(msg);
        }

        public LightTower Set(LightTowerType type, bool shining,bool isBeep)
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                DoType.红灯闪亮.Set(false);
                DoType.黄灯闪亮.Set(false);
                DoType.绿灯闪亮.Set(false);
                DoType.蓝灯闪亮.Set(false);
                DoType.绿色信号灯.Set(false);
                if (shining)
                {
                    switch (type)
                    {
                        case LightTowerType.Red:
                            DoType.红灯闪亮.Set(true);
                            break;
                        case LightTowerType.Yellow:
                            DoType.黄灯闪亮.Set(true);
                            break;
                        case LightTowerType.Green:
                            DoType.绿灯闪亮.Set(true);
                            break;
                        case LightTowerType.Blue:
                            DoType.蓝灯闪亮.Set(true);
                            break;
                    }
                }
                else
                {
                    if(type == LightTowerType.Green)
                    {
                        DoType.绿色信号灯.Set(true);
                    }
                }
            }
            else
            {
                lock (this)
                {
                    this.item.Type = type;
                    this.item.Shining = shining;
                    this.item.IsBeep = isBeep;
                }
            }
            return this;
        }

        public LightTower Off()
        {
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                DoType.红灯闪亮.Set(false);
                DoType.黄灯闪亮.Set(false);
                DoType.绿灯闪亮.Set(false);
                DoType.蓝灯闪亮.Set(false);
                DoType.绿色信号灯.Set(false);
            }
            else
            {
                DoType.红色信号灯.Set(false);
                DoType.黄色信号灯.Set(false);
                DoType.绿色信号灯.Set(false);
                DoType.蜂鸣器.Set(false);
            }
            return this;
        }

        public void Update()
        {
            // todo 传进来的动作只要不是绿灯都比消息优先
            if(Machine.Instance.Setting.MachineSelect == MachineSelection.AD19)
            {
                return;
            }
            //当前无报警才刷新上层传递的灯光提示信息
            if (item.Type == LightTowerType.Green)
            {
                LightTowerMessage msg;
                messageQueue.TryDequeue(out msg);
                lock (this)
                {
                    this.item.Type = msg.Type;
                    this.item.Shining = msg.Shining;
                    this.item.IsBeep = msg.Beep;
                }
            }
            if (!item.Equal(lastItem))
            {
                item.BoolValue = false;
            }
            if (item.Shining)
            {
                if ((DateTime.Now - this.dateTime > TimeSpan.FromMilliseconds(800)) || !item.Equal(lastItem))
                {
                    DoType.蜂鸣器.Set(false);
                    this.dateTime = DateTime.Now;
                    item.BoolValue = !item.BoolValue;
                    switch (item.Type)
                    {
                        case LightTowerType.Red:
                            OpenRed(item.Shining);
                            this.Opened?.Invoke(LightTowerType.Red, item.BoolValue);
                            break;
                        case LightTowerType.Yellow:
                            OpenYellow(item.Shining);
                            this.Opened?.Invoke(LightTowerType.Yellow, item.BoolValue);
                            break;
                        case LightTowerType.Green:
                            OpenGreen(item.Shining);
                            this.Opened?.Invoke(LightTowerType.Green, item.BoolValue);
                            break;
                    }
                    if (item.IsBeep)
                    {
                        DoType.蜂鸣器.Set(item.BoolValue);
                    }
                }
            }
            else
            {
                DoType.蜂鸣器.Set(false);
                switch (item.Type)
                {
                    case LightTowerType.Red:
                        OpenRed(item.Shining);
                        this.Opened?.Invoke(LightTowerType.Red, true);
                        break;
                    case LightTowerType.Yellow:
                        OpenYellow(item.Shining);
                        this.Opened?.Invoke(LightTowerType.Yellow, true);
                        break;
                    case LightTowerType.Green:
                        OpenGreen(item.Shining);
                        this.Opened?.Invoke(LightTowerType.Green, true);
                        break;
                }
                if (item.IsBeep)
                {
                    DoType.蜂鸣器.Set(true);
                }
            }
            lastItem.SetValue(item);
        }

        public void OpenRed(bool shining)
        {
            if (shining)
            {
                DoType.红色信号灯.Set(item.BoolValue);
                DoType.绿色信号灯.Set(false);
                DoType.黄色信号灯.Set(false);
            }
            else
            {
                DoType.红色信号灯.SetIfNot(true);
                DoType.绿色信号灯.SetIfNot(false);
                DoType.黄色信号灯.SetIfNot(false);
            }
        }
        public void OpenGreen(bool shining)
        {
            if (shining)
            {
                DoType.红色信号灯.Set(false);
                DoType.绿色信号灯.Set(item.BoolValue);
                DoType.黄色信号灯.Set(false);
            }
            else
            {
                DoType.红色信号灯.SetIfNot(false);
                DoType.绿色信号灯.SetIfNot(true);
                DoType.黄色信号灯.SetIfNot(false);
            }
        }
        public void OpenYellow(bool shining)
        {
            if (shining)
            {
                DoType.红色信号灯.Set(false);
                DoType.绿色信号灯.Set(false);
                DoType.黄色信号灯.Set(item.BoolValue);
            }
            else
            {
                DoType.红色信号灯.SetIfNot(false);
                DoType.绿色信号灯.SetIfNot(false);
                DoType.黄色信号灯.SetIfNot(true);
            }
        }
        public void OpenBlue(bool shining)
        {

        }
    }
}
