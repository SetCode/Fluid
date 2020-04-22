using Anda.Fluid.Domain.Conveyor;
using Anda.Fluid.Domain.Conveyor.ConveyorMessage;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.Metro
{
    class RunBroker
    {
        private static readonly RunBroker instance = new RunBroker();
        private RunBroker()
        {
            //Executor.Instance.OnWorkDone += Instance_OnWorkDone;
            //Executor.Instance.OnWorkStop += Instance_OnWorkStop;
            //Executor.Instance.OnProgramRunning += Instance_OnProgramRunning;
            Executor.Instance.OnProgramDone += Instance_OnProgramDone;
            //Executor.Instance.OnProgramPaused += Instance_OnProgramPaused;
            //Executor.Instance.OnProgramResuming += Instance_OnProgramResuming;
        }

        public static RunBroker Instance => instance;

        public void StartWork()
        {
            if (FluidProgram.Current != null)
            {
                if (FluidProgram.Current.LotControlEnable)
                {
                    if (FluidProgram.Current.RuntimeSettings.IsStartLotById)
                    {
                        //有单号生产
                    }
                    else
                    {
                        //无单号生产
                    }
                }
            }
            Executor.Instance.Run();
        }

        private void Instance_OnProgramDone()
        {
            try
            {
                //通知轨道：点胶完成
                if (FluidProgram.Current.ExecutantOriginOffset.Equals(new PointD(0, 0)))
                {
                    ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道1点胶完成);
                }
                else
                {
                    ConveyorMsgCenter.Instance.Program.SendMessage(FluProgramMsg.轨道2点胶完成);
                }
            }
            catch (Exception)
            {
            }
          
        }
    }
}
