using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Scalage
{
    public class ScalebleDisable : ScalableCom, IScalable, IUpdatable
    {
        public ScalebleDisable(EasySerialPort easySerialPort) : base(easySerialPort)
        {

        }
        public ComCommunicationSts CommunicationOK { get; private set; }

        #region Cmd
        public string ExternalCaliCmd => " ";
        public string InternalCaliCmd => " ";
        public string PrintCmd => " ";
        public string ResetCmd => " ";        
        public string TareCmd => " ";
        public string ZeroCmd => " ";
        public string ZeroTareCombiCmd => " ";

        #endregion 


        public string Name => this.GetType().Name;


        public object Obj => this;
        
        

        public ScalePrm Prm { get; set; }
       

        public ScalePrm PrmBackUp { get; set; }
        

        public Scale.Vendor Vendor => Scale.Vendor.Disable;

        

        public bool CommunicationTest()
        {
            return true;
        }

        public bool ExternalCali()
        {
            return true;
        }

        public bool InternalCali()
        {
            return true;
        }

        public int Print(TimeSpan timeOut, out string value)
        {
            value = " ";
            return 0;
        }

        public int Print(TimeSpan timeout, int readTimes, out double value)
        {
            value = 0.0;
            return 0;
        }

        public bool Reset()
        {
            return true;
        }

        public void StopReadWeight()
        {
            
        }

        public bool Tare()
        {
            return true;
        }

       

        public bool Zero()
        {
            return true;
        }

        public bool ZeroTareCombi()
        {
            return true;
        }

        public void Update()
        {
            this.CommunicationOK =ComCommunicationSts.DISABLE;
        }
    }
}
