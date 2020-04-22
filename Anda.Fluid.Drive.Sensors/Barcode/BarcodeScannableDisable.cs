using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anda.Fluid.Infrastructure.Communication;

namespace Anda.Fluid.Drive.Sensors.Barcode
{
    public class BarcodeScannableDisable : BarcodeScannableCom, IBarcodeSannable
    {
        public BarcodeScannableDisable(EasySerialPort easySerialPort) : base(easySerialPort)
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }

        public char Delimiter { get; set; } = '\r';

        public string CmdReadValue => "";

        public ComCommunicationSts CommunicationOK { get; private set; }

        public BarcodeScanner.Vendor Vendor { get; }

        public int ReadValue(TimeSpan timeout, out string str)
        {
            str = "";
            return 0;
        }

        public void Update()
        {
            this.CommunicationOK = ComCommunicationSts.DISABLE;
        }
    }
}
