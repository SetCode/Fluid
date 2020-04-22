using Anda.Fluid.Infrastructure.Tasker;
using System;

namespace Anda.Fluid.Drive.GlueManage
{
    /// <summary>
    /// Author：liyi
    /// Date：2019/09/10
    /// Description：胶水管控类-每支胶水
    /// </summary>
    public class GlueManager
    {
        private int key;
        public GlueManager(int key)
        {
            this.key = key;
        }

        public bool isManualSpraying { get; set; } = false;

        /// <summary>
        /// 设置胶水参数
        /// </summary>
        public void SetPrm()
        {

        }

        /// <summary>
        /// 更新胶水参数（时间参数，重量参数由外部点胶控制）
        /// 1.寿命时间
        /// 2.使用时间
        /// 3.回温时间
        /// </summary>
        public void UpdatePrm()
        {
            GlueManagePrm prm = GlueManagePrmMgr.Instance.FindBy(this.key);
            if (!prm.UseGlueManage || (Machine.Instance.Setting.ValveSelect != ValveSelection.双阀 && this.key == 1))
            {
                return;
            }
            double glueLife = prm.GlueLife;
            DateTime glueDeliverTime = prm.GlueDeliverTime;
            double remainLife = glueLife - (DateTime.Now - glueDeliverTime).TotalMinutes;
            if (remainLife < 1)
            {
                prm.GlueRemainLife = 0;
            }
            else
            {
                prm.GlueRemainLife = remainLife;
            }
        }
        /// <summary>
        /// Author: liyi
        /// Date:   2019/09/11
        /// Description:    检查胶水状态
        /// 1.刷新胶水剩余寿命
        /// 2.刷新胶水剩余量
        /// 3.判断是否预警（剩余量和时间）
        /// 4.判断胶水是否可以继续使用（剩余量和时间）
        /// </summary>
        public void CheckGlueStatue()
        {
            GlueManagePrm prm = GlueManagePrmMgr.Instance.FindBy(this.key);
            if (!prm.UseGlueManage || (Machine.Instance.Setting.ValveSelect != ValveSelection.双阀 && this.key == 1))
            {
                return;
            }
            double remainLife = prm.GlueRemainLife;
            double remainWeight = prm.RemainWeight;
            double totalWeight = prm.TotalWeight;
            double thawTime = prm.GlueThawTime;
            DateTime deliverTime = prm.GlueDeliverTime;
            double lifeWarningTime = prm.LifeWarningTime;
            double WarningWeight = prm.TotalWeight * prm.WarningPercentage / 100;

            int warningCode = 0;
            // 胶水寿命到期
            if (remainLife < 1)
            {
                // 报警停机
                warningCode = 1;
            }
            else if (remainLife < lifeWarningTime)
            {
                // 寿命预警提示
                warningCode = 2;
            }

            // 胶水剩余量用完
            if (remainWeight < 10)
            {
                // 报警停机
                warningCode = 1;
            }
            else if (remainWeight < WarningWeight)
            {
                // 重量预警提示
                warningCode = 2;
            }

            // 胶水回温时间不够
            if ((DateTime.Now - deliverTime).TotalMinutes < thawTime)
            {
                // 胶水回温时间不足，禁止运行
            }
        }
        /// <summary>
        /// 查询服务器胶水数据
        /// </summary>
        /// <returns></returns>
        public DateTime QueryGlueTimeByServer()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0);
        }
    }
    /// <summary>
    /// Author：liyi
    /// Date：2019/09/10
    /// Description：胶水管控状态轮询对象
    /// </summary>
    public class GlueManagerMgr : TaskLoop
    {
        private readonly static GlueManagerMgr instance = new GlueManagerMgr();

        private GlueManager glueManager1, glueManager2;
        /// <summary>
        /// 换胶变量，处于换胶状态，不做检测
        /// </summary>
        public bool IsChangeGlue { get; set; } = false;

        private GlueManagerMgr()
        {
            this.glueManager1 = new GlueManager(0);
            this.glueManager2 = new GlueManager(1);
            // 500ms轮询检查一次
            this.loopSleepMills = 500;
        }

        public static GlueManagerMgr Instance => instance;

        /// <summary>
        /// 加载并设置每支阀胶水管控参数
        /// </summary>
        public void Setup()
        {
            if (!GlueManagePrmMgr.Instance.Load())
            {
                GlueManagePrmMgr.Instance.Clear();
                GlueManagePrmMgr.Instance.Add(new GlueManagePrm(0));
                GlueManagePrmMgr.Instance.Add(new GlueManagePrm(1));
            }
            for (int i = 0; i < 2; i++)
            {
                if (GlueManagePrmMgr.Instance.FindBy(i) == null)
                {
                    GlueManagePrmMgr.Instance.Add(new GlueManagePrm(i));
                }
            }
        }

        public void Unload()
        {
            this.Stop();
            GlueManagePrmMgr.Instance.Save();
        }
        protected override void Loop()
        {
            
            //换胶水过程中不做检测
            if (IsChangeGlue)
            {
                return;
            }
            this.glueManager1.UpdatePrm();
            this.glueManager1.CheckGlueStatue();
            this.glueManager2.UpdatePrm();
            this.glueManager2.CheckGlueStatue();
        }
        /// <summary>
        /// Author: liyi
        /// Date:   2019/09/10
        /// Description:    更新胶水胶水剩余量
        /// </summary>
        /// <param name="key">胶阀</param>
        /// <param name="UsedWeight">使用的胶水量</param>
        public void UpdateGlueRemainWeight(int key, double UsedWeight)
        {
            if (!GlueManagePrmMgr.Instance.FindBy(key).UseGlueManage)
            {
                return;
            }
            //不需要考虑多线程访问问题
            double oldWeight = GlueManagePrmMgr.Instance.FindBy(key).RemainWeight;
            GlueManagePrmMgr.Instance.FindBy(key).RemainWeight = oldWeight - UsedWeight;
        }
    }
}
