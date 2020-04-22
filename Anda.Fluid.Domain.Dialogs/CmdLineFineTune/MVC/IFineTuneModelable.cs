using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC
{
    public interface IFineTuneModelable
    {
        /// <summary>
        /// 当前Pattern的原点
        /// </summary>
        PointD PatternOrigin { get; }

        /// <summary>
        /// 当前Pattern的指令列表
        /// </summary>
        List<CmdLine> CmdLineList { get; set; }

        /// <summary>
        /// 可进行微调的所有指令及指令中包含的点
        /// </summary>
        List<CmdLinePoint> AllCmdPointsList { get; set; }

        /// <summary>
        /// 需要进行微调的所有指令及包含的点
        /// </summary>
        List<CmdLinePoint> CurrCmdPointsList { get; }

        /// <summary>
        /// 当前选中的指令编号
        /// </summary>
        int SelectedCmdLineNo { get; set; }

        /// <summary>
        /// 当前选中的指令中的点编号
        /// </summary>
        int SelectedPointNo { get; set; }

        /// <summary>
        /// 得到当前选择点
        /// </summary>
        CmdLinePoint GetSelectedPoint();

        /// <summary>
        /// 得到当前选择点在所有点List中的位置
        /// </summary>
        /// <returns></returns>
        int GetSelectedInAllList();

        /// <summary>
        /// 得到当前选择点在当前List中的位置
        /// </summary>
        /// <returns></returns>
        int GetSelectedInCurrList();

        /// <summary>
        /// 根据指定条件得到点
        /// </summary>
        /// <param name="cmdLineNo"></param>
        /// <param name="pointNo"></param>
        /// <returns></returns>
        CmdLinePoint GetPointByNo(int cmdLineNo, int pointNo);

        /// <summary>
        /// 添加某视图为观察者
        /// </summary>
        /// <param name="view"></param>
        void AddObserver(IFineTuneViewable view);

        /// <summary>
        /// 将某视图从观察者中移除
        /// </summary>
        /// <param name="view"></param>
        void RemoveObserver(IFineTuneViewable view);

        /// <summary>
        /// 通知观察者，模型发生变化，更新视图
        /// </summary>
        void NotifyObserversModelChanged();

        /// <summary>
        /// 通知观察者，选择点发生改变，更新视图
        /// </summary>
        void NotifyObserverSelectChanged();
    }

    public class CmdLinePoint
    {
        /// <summary>
        /// 该点所属的命令编号
        /// </summary>
        public int CmdLineNo { get; set; } = 0;

        /// <summary>
        /// 该点在所属命令中的编号
        /// </summary>
        public int PointNo { get; set; } = 0;

        /// <summary>
        /// 指令的类型名称
        /// </summary>
        public CmdLineType CmdLineType { get; private set; }

        /// <summary>
        /// 指令是否需要进行轨迹微调
        /// </summary>
        public bool CmdLineEnable { get; set; } = true;

        /// <summary>
        /// 点坐标
        /// </summary>
        public PointD Point { get; set; }

        /// <summary>
        /// 点描述
        /// </summary>
        public string PointDescribe { get; private set; }

        /// <summary>
        /// 是否跳过该点
        /// </summary>
        public bool Skip { get; set; } = false;


        public CmdLinePoint(int cmdLineNo, int pointNo, CmdLineType cmdLineType,PointD point,string pointDescribe)
        {
            this.CmdLineNo = cmdLineNo;
            this.PointNo = pointNo;
            this.CmdLineType = cmdLineType;
            this.Point = point;
            this.PointDescribe = pointDescribe;
        }

        /// <summary>
        /// 判断传入的指令编号和点编号是否相同
        /// </summary>
        /// <param name="cmdLineNo"></param>
        /// <param name="pointNo"></param>
        /// <returns></returns>
        public bool IsSameNo(int cmdLineNo, int pointNo)
        {
            if (this.CmdLineNo == cmdLineNo && this.PointNo == pointNo)
                return true;
            else
                return false;
        }
    }
}
