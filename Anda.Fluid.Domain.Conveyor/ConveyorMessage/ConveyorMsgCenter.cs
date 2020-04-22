using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Conveyor.ConveyorMessage
{
    /*******************************************************************************
     * 此类为轨道消息中心，主程序通过消息中心进行轨道的相关操作
     * 
     * *****************************************************************************/
    public class ConveyorMsgCenter
    {
        private static ConveyorMsgCenter instance = new ConveyorMsgCenter();
        private FluProgramSubCenter program;
        private ConveyorControlSubCenter conveyorControl;
        private ConveyorSubCenter conveyor;
        private ConveyorControlMsg conveyorState;
        private ConveyorMsgCenter()
        {
            this.program = new FluProgramSubCenter();
            this.conveyorControl = new ConveyorControlSubCenter();
            this.conveyor = new ConveyorSubCenter();
        }
        public static ConveyorMsgCenter Instance => instance;
        /// <summary>
        /// 由点胶程序发出消息
        /// </summary>
        public FluProgramSubCenter Program => this.program;
        /// <summary>
        /// 由轨道控件发出消息
        /// </summary>
        public ConveyorControlSubCenter ConveyorControl => this.conveyorControl;
        /// <summary>
        /// 由实体轨道发出消息
        /// </summary>
        public ConveyorSubCenter Conveyor => this.conveyor;

        internal ConveyorControlMsg ConveyorState
        {
            get
            {
                return this.conveyorState;
            }
            set
            {
                this.conveyorState = value;
            }
        }

        /// <summary>
        /// 分发消息到各子中心处理
        /// </summary>
        internal void PassMessage(object subCenter,object message)
        {
            if (subCenter.GetType() == typeof(FluProgramSubCenter))
            {
                this.conveyor.ReciveMessage(message);
            }
            else if(subCenter.GetType() == typeof(ConveyorControlSubCenter))
            {
                this.conveyor.ReciveMessage(message);
            }
        }
        
    }
}
