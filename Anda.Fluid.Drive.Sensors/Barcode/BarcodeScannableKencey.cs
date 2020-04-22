using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;
using Anda.Fluid.Infrastructure.DataStruct;
using System.Threading;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Drive.Sensors.Barcode
{
    public class BarcodeScannableKencey : BarcodeScannableCom, IBarcodeSannable, IAlarmSenderable
    {
        public const string CMDREADSTRING = "LON\r\n";
        public BarcodeScannableKencey(EasySerialPort easySerialPort) : base(easySerialPort)
        {
            easySerialPort.isDelimiter = true;
            easySerialPort.Delimiter = this.Delimiter;
        }

        public char Delimiter { get; set; } = '\n';

        public string CmdReadValue => CMDREADSTRING;

        public ComCommunicationSts CommunicationOK { get; private set; }

        public string Name => this.GetType().Name;

        public object Obj => this;

        public BarcodeScanner.Vendor Vendor => BarcodeScanner.Vendor.SR700;

        public int ReadValue(TimeSpan timeout, out string str)
        {
            str = "";
            if (this.EasySerialPort == null)
            {
                //AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorBarcodeScannerState);
                this.CommunicationOK = ComCommunicationSts.ERROR;
                return -1;
            }
            this.EasySerialPort.ReadDelimiter = '\n';
            this.EasySerialPort.isDelimiter = true;
            for (int i = 0; i < 1; i++)
            {
                //this.EasySerialPort.SerialPort.ReceivedBytesThreshold = 29;
                ByteData reply = this.EasySerialPort.WriteAndGetReply(CmdReadValue, timeout);
                if (reply != null)
                {
                    string temp = reply.ToString();
                    temp = temp.Replace('\r',' ');
                    str = temp.Trim(new char[] { ' '});                    
                    return 0;
                }
                Logger.DEFAULT.Info(" ReadValue reply is null");
            }
            str = "";
            //连续三次读取失败关闭读取，返回异常值
            this.EasySerialPort.WriteAndGetReply("LOFF\r\n", timeout);
            //AlarmServer.Instance.Fire(this, AlarmInfoSensors.ErrorBarcodeScannerRead);
            return -1;
        }

        public void Update()
        {
            this.CommunicationOK = this.EasySerialPort.Connected ? ComCommunicationSts.OK : ComCommunicationSts.ERROR;
        }
    }
}
