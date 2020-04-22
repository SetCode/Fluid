using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Infrastructure.Common;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC
{
    internal class FineTuneModel : IFineTuneModelable
    {
        private List<IFineTuneViewable> views;
        public PointD PatternOrigin { get; }
        public List<CmdLine> CmdLineList { get; set; }

        public int SelectedCmdLineNo { get; set; }

        public int SelectedPointNo { get; set; }

        public List<CmdLinePoint> AllCmdPointsList { get; set; }

        public List<CmdLinePoint> CurrCmdPointsList
        {
            get
            {
                List<CmdLinePoint> list = new List<CmdLinePoint>();
                foreach (var item in this.AllCmdPointsList)
                {
                    if (item.CmdLineEnable)
                        list.Add(item);
                }
                return list;
            }
        }

        public FineTuneModel(Pattern pattern)
        {
            this.PatternOrigin = pattern.GetOriginPos();
            this.SelectedCmdLineNo = 0;
            this.SelectedPointNo = 0;
            this.CmdLineList = pattern.CmdLineList;
            this.AllCmdPointsList = new List<CmdLinePoint>();
            this.Parse(pattern.CmdLineList);
            this.views = new List<IFineTuneViewable>();
        }

        public CmdLinePoint GetSelectedPoint()
        {
            for (int i = 0; i < this.AllCmdPointsList.Count; i++)
            {
                if (this.AllCmdPointsList[i].IsSameNo(this.SelectedCmdLineNo, this.SelectedPointNo))
                {
                    return this.AllCmdPointsList[i];
                }
            }
            return null;
        }

        public int GetSelectedInAllList()
        {
            for (int i = 0; i < this.AllCmdPointsList.Count; i++)
            {
                if (this.AllCmdPointsList[i].IsSameNo(this.SelectedCmdLineNo, this.SelectedPointNo))
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSelectedInCurrList()
        {
            for (int i = 0; i < this.CurrCmdPointsList.Count; i++)
            {
                if (this.CurrCmdPointsList[i].IsSameNo(this.SelectedCmdLineNo, this.SelectedPointNo))
                {
                    return i;
                }
            }
            return -1;
        }

        public CmdLinePoint GetPointByNo(int cmdLineNo, int pointNo)
        {
            for (int i = 0; i < this.AllCmdPointsList.Count; i++)
            {
                if (this.AllCmdPointsList[i].IsSameNo(cmdLineNo, pointNo))
                {
                    return this.AllCmdPointsList[i];
                }
            }
            return null;
        }

        public void AddObserver(IFineTuneViewable view)
        {
            this.views.Add(view);
        }

        public void RemoveObserver(IFineTuneViewable view)
        {
            this.views.Remove(view);
        }

        public void NotifyObserversModelChanged()
        {
            foreach (var item in this.views)
            {
                item.UpdateByModelChange(this);
            }
        }
        public void NotifyObserverSelectChanged()
        {
            foreach (var item in this.views)
            {
                item.UpdateBySelectedChange(this);
            }
        }

        private void Parse(List<CmdLine> list)
        {

            for (int i = 0; i < list.Count; i++)
            {

                if (list[i] is DotCmdLine || list[i] is LineCmdLine || list[i] is ArcCmdLine ||list[i] is SnakeLineCmdLine
                    || list[i] is StepAndRepeatCmdLine || list[i] is DoCmdLine || list[i] is SymbolLinesCmdLine || list[i] is DoMultiPassCmdLine)
                {

                    for (int j = 0; j < list[i].PointsAndDescrie.Count; j++)
                    {
                        CmdLinePoint cmdLineAndPoints = new CmdLinePoint(i, j, list[i].CmdLineName,
                            list[i].PointsAndDescrie[j].Item1, list[i].PointsAndDescrie[j].Item2);
                        this.AllCmdPointsList.Add(cmdLineAndPoints);
                    }

                }
            }

        }

    }
}
