using Anda.Fluid.Domain.FluProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.Msg
{
    public interface IDrawingMsgReceiver
    {
        void Update(FluidProgram fluidProgram);
        void EnterWorkpiece();
        void EnterPattern(int patternNo);

        /// <summary>
        /// 选中了一个或多个命令
        /// </summary>
        /// <param name="inWorkpiece"></param>
        /// <param name="patternNo"></param>
        /// <param name="cmdLineNo"></param>
        void ClickCmdLine(bool inWorkpiece, int patternNo, int[] cmdLineNo);
    }

    public interface ISingleEditDrawCmdable
    {
        void EditSingleDrawCmd(int cmdLineNo);
    }

    public interface IMultiDrawCmdEditable
    {
        void EditMultiDrawCmd(List<int> cmdLineNo);
    }

    public interface IRelateScriptEditor
    {
        void RelateScirptEditor(List<int> cmdLineNo);
    }
}
