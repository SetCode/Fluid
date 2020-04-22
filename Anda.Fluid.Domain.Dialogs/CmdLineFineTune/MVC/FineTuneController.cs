using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Utils;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC
{
    internal class FineTuneController : IFineTuneControlable
    {
        private IFineTuneModelable model;
        public bool AutoTrack { get; set; } = false;

        public void Move(DirectionEnum direction, double dis)
        {
            if (Machine.Instance.IsBusy)
                return;
            switch (direction)
            {
                case DirectionEnum.Up:
                    Machine.Instance.Robot.MoveIncY(dis);
                    break;
                case DirectionEnum.Down:
                    Machine.Instance.Robot.MoveIncY(-dis);
                    break;
                case DirectionEnum.Left:
                    Machine.Instance.Robot.MoveIncX(-dis);
                    break;
                case DirectionEnum.Right:
                    Machine.Instance.Robot.MoveIncX(dis);
                    break;
                case DirectionEnum.UpLeft:
                    Machine.Instance.Robot.MoveIncXY(-dis, dis);
                    break;
                case DirectionEnum.UpRight:
                    Machine.Instance.Robot.MoveIncXY(dis, dis);
                    break;
                case DirectionEnum.DownLeft:
                    Machine.Instance.Robot.MoveIncXY(-dis, -dis);
                    break;
                case DirectionEnum.DownRight:
                    Machine.Instance.Robot.MoveIncXY(dis, -dis);
                    break;
            }
        }

        public void MoveToPoint(int cmdLineNo, int pointNo)
        {
            this.model.SelectedCmdLineNo = cmdLineNo;
            this.model.SelectedPointNo = pointNo;
            this.model.NotifyObserverSelectChanged();

            //获得选中的点
            CmdLinePoint selcetedPoint = model.GetSelectedPoint();

            Machine.Instance.Robot.MovePosXY(selcetedPoint.Point.X + this.model.PatternOrigin.X, selcetedPoint.Point.Y + this.model.PatternOrigin.Y);
        }

        public bool NextTrack()
        {
            //获得选中点在List中的位置
            int index = this.model.GetSelectedInAllList();
            //当找不到位置或者已经是最后一个点时，直接返回
            if (index == -1 || index == this.model.AllCmdPointsList.Count) 
                return false;

            //向后一步时，选中点的位置
            int next = 0;
            //向后遍历
            for (int i = index + 1; i < this.model.AllCmdPointsList.Count; i++)
            {
                if (this.model.AllCmdPointsList[i].CmdLineEnable && !this.model.AllCmdPointsList[i].Skip)
                {
                    next = i;
                    //修改选中点编号
                    this.model.SelectedCmdLineNo = this.model.AllCmdPointsList[i].CmdLineNo;
                    this.model.SelectedPointNo = this.model.AllCmdPointsList[i].PointNo;
                    this.model.NotifyObserverSelectChanged();
                    //移动到该点
                    Machine.Instance.Robot.MovePosXY(this.model.AllCmdPointsList[i].Point.X + this.model.PatternOrigin.X,
                        this.model.AllCmdPointsList[i].Point.Y + this.model.PatternOrigin.Y);
                    return true;
                }
            }

            return false;
        }

        public bool PreTrack()
        {
            //获得选中点在List中的位置
            int index = this.model.GetSelectedInAllList();
            //当找不到位置或者已经是第一个点时，直接返回
            if (index == -1 || index == 0)
                return false;

            //向后一步时，选中点的位置
            int next = 0;
            //向前遍历
            for (int i = index - 1; i >= 0; i--)
            {
                if (this.model.AllCmdPointsList[i].CmdLineEnable && !this.model.AllCmdPointsList[i].Skip)
                {
                    next = i;
                    //修改选中点编号
                    this.model.SelectedCmdLineNo = this.model.AllCmdPointsList[i].CmdLineNo;
                    this.model.SelectedPointNo = this.model.AllCmdPointsList[i].PointNo;
                    this.model.NotifyObserverSelectChanged();
                    //移动到该点
                    Machine.Instance.Robot.MovePosXY(this.model.AllCmdPointsList[i].Point.X + this.model.PatternOrigin.X,
                        this.model.AllCmdPointsList[i].Point.Y + this.model.PatternOrigin.Y);
                    return true;
                }
            }

            return false;
        }

        public void SetEnable(List<Tuple<CmdLineType, bool>> list)
        {
            foreach (var tuple in list)
            {
                foreach (var item in this.model.AllCmdPointsList)
                {
                    if ((int)item.CmdLineType == (int)tuple.Item1)
                    {
                        item.CmdLineEnable = tuple.Item2;
                    }
                }
            }

            this.model.SelectedCmdLineNo = 0;
            this.model.SelectedPointNo = 0;

            this.model.NotifyObserversModelChanged();
            this.model.NotifyObserverSelectChanged();
        }

        public void SetModel(IFineTuneModelable model)
        {
            this.model = model;
        }

        public void SetSkip(int cmdLineNo, int pointNo, bool skip)
        {
            CmdLinePoint cmdLinePoint = this.model.GetPointByNo(cmdLineNo, pointNo);
            if (cmdLinePoint == null)
                return;
            cmdLinePoint.Skip = skip;

            this.model.NotifyObserversModelChanged();
        }

        public void TeachPoint(PointD point)
        {
            CmdLinePoint cmdLinePoint = this.model.GetSelectedPoint();
            if (cmdLinePoint == null)
                return;

            cmdLinePoint.Point.X = point.X;
            cmdLinePoint.Point.Y = point.Y;
            //如果是圆弧则要重新计算圆弧中心和弧度
            if (cmdLinePoint.CmdLineType == CmdLineType.圆弧或圆环)
            {
                ArcCmdLine arc = this.model.CmdLineList[cmdLinePoint.CmdLineNo] as ArcCmdLine;
                arc.Center = MathUtils.CalculateCircleCenter(arc.Start, arc.Middle, arc.End);
                //如果不是圆环，则要重新计算弧度
                if (arc.Degree != 360 && arc.Degree != -360)
                {
                     arc.Degree = MathUtils.CalculateDegree(arc.Start, arc.Middle, arc.End, arc.Center);
                }
            }
            this.model.NotifyObserversModelChanged();

            if (this.AutoTrack)
            {
                if (!this.NextTrack())
                {
                    MessageBox.Show("已到最后一个点");
                }
            }
            else
            {
                this.model.NotifyObserverSelectChanged();
            }
        }
    }
}
