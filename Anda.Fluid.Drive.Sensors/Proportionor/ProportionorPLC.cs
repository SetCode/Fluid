using Anda.Fluid.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Proportionor
{
    public class ProportionorPLC : ProportionorCom, IProportional
    {
        public ProportionorPLC(EasySerialPort easySerialPort)
            :base(easySerialPort)
        {
        }

        public int Channel { get; set; }

        public object Obj => this;

        public string Name => this.GetType().Name;

        public ushort CurrentValue { get; private set; }

        public ComCommunicationSts CommunicationOK { get; private set; }

        public bool SetValue(ushort value)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }

            string valueTo16 = GetValueTo16String(value).ToUpper();

            string baseCmd = "%01#WDD1223012230";
            string valueCmd = string.Format("{0}{1}", baseCmd, valueTo16);
            string cmd = string.Format("{0}{1}\r\n", valueCmd, GetBCCCode(System.Text.Encoding.Default.GetBytes(valueCmd)).ToString("X"));
            bool rtn = this.EasySerialPort.Write(cmd.ToUpper());
            if (rtn)
            {
                this.CurrentValue = value;
            }
            this.CommunicationOK = rtn ? ComCommunicationSts.OK : ComCommunicationSts.ERROR;
            return rtn;
        }
        /// <summary>
        /// BCC校验(异或校验和)
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public int GetBCCCode(byte[] byteArray)
        {
            int nbcc = -1;
            for (int i = 0; i < byteArray.Length; i++)
            {
                if (nbcc < 0)
                {
                    nbcc = byteArray[i];
                }
                else
                {
                    nbcc ^= byteArray[i];
                }
            }
            return nbcc;
        }

        /// <summary>
        /// 气压数值转换为16进制，且高低位互换
        /// </summary>
        /// <param name="value">气压值</param>
        /// <returns></returns>
        public string GetValueTo16String(ushort value)
        {

            string valueTo16 = value.ToString("X").PadLeft(4,'0');
            char[] charArr = new char[valueTo16.Length];
            int i = 0;
            foreach (char item in valueTo16)
            {
                charArr[i++] = item;
            }
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < charArr.Length; j++)
            {
                if (j < 2)
                {
                    char temp = charArr[j];
                    charArr[j] = charArr[j + 2];
                    charArr[j + 2] = temp;
                }
                sb.Append(charArr[j]);
            }
            return sb.ToString();
        }

        public void Update()
        {
            if (!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }
    }
}
