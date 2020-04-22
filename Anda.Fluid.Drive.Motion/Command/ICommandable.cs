using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Anda.Fluid.Drive.Motion.Command
{
    public interface ICommandable : ICloneable
    {
        /// <summary>
        /// state of the command
        /// </summary>
        CommandState State { get; set; }
        
        /// <summary>
        /// unblocking update
        /// </summary>
        void Update();
        
        /// <summary>
        /// unblocking guard
        /// </summary>
        /// <returns></returns>
        bool Guard();
        
        /// <summary>
        /// unblocking call
        /// </summary>
        void Call();

        /// <summary>
        /// handle message while command is running;
        /// </summary>
        /// <param name="msgType">message type</param>
        void HandleMsg(CmdMsgType msgType);

        AutoResetEvent AutoEvent { get; }

        Action OnDone { get; set; }

        Action OnFailed { get; set; }

        Action OnFinished { get; set; }
    }
}
