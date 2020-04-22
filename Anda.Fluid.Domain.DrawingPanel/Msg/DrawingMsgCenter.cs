using Anda.Fluid.Domain.FluProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPanel.Msg
{
    public class DrawingMsgCenter
    {
        private static DrawingMsgCenter instance = new DrawingMsgCenter();
        private List<IDrawingMsgReceiver> receivers=new List<IDrawingMsgReceiver>();

        internal ISingleEditDrawCmdable singleReceiver;
        internal IMultiDrawCmdEditable multiRecierver;
        internal IRelateScriptEditor relateScriptReceiver;
        private DrawingMsgCenter()
        {

        }
        public static DrawingMsgCenter Instance => instance;

        public void RegisterReceiver(IDrawingMsgReceiver receiver)
        {
            if (receiver is DrawProgram)
            {
                this.receivers.Insert(0, receiver);
            }
            else
            {
                this.receivers.Add(receiver);
            }
        }

        public void RegisterSingleDrawEditor(ISingleEditDrawCmdable receiver)
        {
            this.singleReceiver = receiver;
        }

        public void RegisterMultiDrawEditor(IMultiDrawCmdEditable receiver)
        {
            this.multiRecierver = receiver;
        }

        public void RegisterRelateScriptEditor(IRelateScriptEditor receiver)
        {
            this.relateScriptReceiver = receiver;
        }

        public void SendMsg(DrawingMessage msg,FluidProgram fluidProgram)
        {
            switch (msg)
            {
                case DrawingMessage.需要更新绘图程序:
                    for (int i = 0; i < this.receivers.Count; i++)
                    {
                        this.receivers[i].Update(fluidProgram);
                    }                   
                    break;
            }
        }
        public void SendMsg(DrawingMessage msg)
        {
            switch (msg)
            {
                case DrawingMessage.进入了Workpiece界面:
                    for (int i = 0; i < this.receivers.Count; i++)
                    {
                        this.receivers[i].EnterWorkpiece();
                    }
                    break;
            }
        }
        public void SendMsg(DrawingMessage msg,int patternNo)
        {
            switch (msg)
            {
                case DrawingMessage.进入了Pattern界面:
                    for (int i = 0; i < this.receivers.Count; i++)
                    {
                        this.receivers[i].EnterPattern(patternNo);
                    }
                    break;
            }
        }
        public void SendMsg(DrawingMessage msg,bool inWorkpiece,int patternNo,int[] cmdLineNo)
        {
            switch (msg)
            {
                case DrawingMessage.点击了一个绘图命令:
                    for (int i = 0; i < this.receivers.Count; i++)
                    {
                        this.receivers[i].ClickCmdLine(inWorkpiece, patternNo, cmdLineNo);
                    }
                    break;
            }
        }

    }
    public enum DrawingMessage
    {
        需要更新绘图程序,

        进入了Workpiece界面,
        进入了Pattern界面,

        点击了一个绘图命令,
    }
}
