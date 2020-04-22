using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.DataStruct;
using Newtonsoft.Json;
using Anda.Fluid.Infrastructure.Alarming;
using System.ComponentModel;
using System.Diagnostics;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Infrastructure.Trace;

namespace Anda.Fluid.Infrastructure.Communication
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EasySerialPort : EntityBase<int>, IAlarmSenderable, ICloneable
    {
        private ByteData reply = null;
        public ByteData Reply => this.reply;
        private ManualResetEvent hasReplied = new ManualResetEvent(false);
        private List<DateTime> serialBreakTime = new List<DateTime>();

        public EasySerialPort(int key)
            : base(key)
        {
            this.Delimiter = '\n';
            this.DataReceived += SimpleSerialPort_DataReceived;
        }

        public SerialPort SerialPort { get; private set; } = null;

        public bool Connected => this.SerialPort != null && this.SerialPort.IsOpen;

        /// <summary>
        /// 串口绑定的硬件名称
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 串口名
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public string PortName { get; set; } = string.Empty;

        /// <summary>
        /// 波特率
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public BaudRate BaudRate { get; set; } = BaudRate.BR_115200;

        /// <summary>
        /// 数据位
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public DataBits DataBits { get; set; } = DataBits.DB_8;

        /// <summary>
        /// 停止位
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public StopBits StopBits { get; set; } = StopBits.One;

        /// <summary>
        /// 校验位
        /// </summary>
        [JsonProperty]
        [CompareAtt("CMP")]
        public Parity Parity { get; set; } = Parity.None;

        [JsonProperty]
        public char Delimiter { get; set; }

        [JsonProperty]
        public char ReadDelimiter { get; set; } = '\r';
        [JsonProperty]
        [CompareAtt("CMP")]
        public bool DTR = false;
        [JsonProperty]
        [CompareAtt("CMP")]
        public bool RTS = false;

        public bool isDelimiter { get; set; } = false;

        object IAlarmSenderable.Obj => this;

        string IAlarmSenderable.Name => this.PortName;

        public event EventHandler<bool> OpenStatusChanged;

        public event EventHandler<ByteData> DataReceived;

        public event EventHandler<ByteData> DelimiterDataReceived;

        public event EventHandler<Exception> ExceptionThrown;

        public EasySerialPort Setup(string portname, BaudRate baudrate = BaudRate.BR_115200, DataBits dataBits = DataBits.DB_8, StopBits stopbits = StopBits.One, Parity parity = Parity.None)
        {
            this.PortName = portname;
            this.BaudRate = baudrate;
            this.DataBits = dataBits;
            this.StopBits = stopbits;
            this.Parity = parity;
            return this;
        }

        public bool Open()
        {
            bool rtn = false;
            Close();
            try
            {
                this.SerialPort = new SerialPort();
                this.SerialPort.PortName = this.PortName;
                this.SerialPort.BaudRate = (int)this.BaudRate;
                this.SerialPort.DataBits = (int)this.DataBits;
                this.SerialPort.StopBits = this.StopBits;
                this.SerialPort.Parity = this.Parity;
                this.SerialPort.Encoding = Encoding.UTF8;
                this.SerialPort.DtrEnable = this.DTR;
                this.SerialPort.RtsEnable = this.RTS;
                this.SerialPort.DataReceived += SerialPort_DataReceived;
                this.SerialPort.ErrorReceived += SerialPort_ErrorReceived;
                this.SerialPort.Open();
                rtn = true;
            }
            catch (Exception ex)
            {
                ExceptionThrown?.Invoke(this, ex);
            }

            if (this.SerialPort != null && this.SerialPort.IsOpen)
            {
                OpenStatusChanged?.Invoke(this, true);
            }

            return rtn;
        }

        public void Close()
        {
            if (this.SerialPort != null)
            {
                this.SerialPort.DataReceived -= SerialPort_DataReceived;
                this.SerialPort.ErrorReceived -= SerialPort_ErrorReceived;
                if (this.SerialPort.IsOpen)
                {
                    this.SerialPort.Close();
                    OpenStatusChanged?.Invoke(this, false);
                }
                this.SerialPort.Dispose();
                this.SerialPort = null;
            }
        }

        public bool Write(byte[] data)
        {
            if (this.SerialPort == null)
            {
                ExceptionThrown?.Invoke(this, new Exception("Cannot send data to a null SerialPort (check to see if Connect was called)"));
                return false;
            }

            if (!this.SerialPort.IsOpen)
            {
                ExceptionThrown?.Invoke(this, new Exception("The SerialPort is not opened"));
                return false;
            }

            if (data == null)
            {
                return false;
            }

            try
            {
                this.SerialPort.DiscardOutBuffer();
                this.SerialPort.DiscardInBuffer();
                this.SerialPort.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionThrown?.Invoke(this, ex);
                return false;
            }
        }

        public bool Write(string data)
        {
            if (this.SerialPort == null) return false;
            if (string.IsNullOrEmpty(data)) return false;
            return this.Write(this.SerialPort.Encoding.GetBytes(data));
        }

        public bool WriteLine(string data)
        {
            if (data.LastOrDefault() == BitConverter.GetBytes(this.Delimiter)[0])
            {
                return Write(data);
            }
            else
            {
                return Write(data + this.Delimiter);
            }
        }

        public ByteData WriteAndGetReply(byte[] data, TimeSpan timeout)
        {
            if (this.SerialPort == null) return null;
            this.reply = null;
            this.SerialPort.ReadTimeout = (int)timeout.TotalMilliseconds;
            if (!this.Write(data))
            {
                return null;
            }

            this.hasReplied.Reset();
            if (!this.hasReplied.WaitOne(timeout))
            {
                this.reply = null;
                ExceptionThrown?.Invoke(this, new Exception("Reply timeout"));
            }

            return this.reply;
        }

        public ByteData WriteAndGetReply(string data, TimeSpan timeout)
        {
            if (this.SerialPort == null) return null;
            return WriteAndGetReply(this.SerialPort.Encoding.GetBytes(data), timeout);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> bytesReceived = new List<byte>();
            while (this.SerialPort.BytesToRead > 0 && this.SerialPort.IsOpen && !isDelimiter)
            {
                try
                {
                    int count = this.SerialPort.BytesToRead;
                    if (this.SerialPort.BytesToRead > this.SerialPort.ReadBufferSize)
                    {
                        count = this.SerialPort.ReadBufferSize;
                    }
                    byte[] message = new byte[count];
                    this.SerialPort.Read(message, 0, count);
                    bytesReceived.AddRange(message);
                }
                catch (Exception ex)
                {
                    ExceptionThrown?.Invoke(this, ex);
                }
            }
            if (isDelimiter && this.SerialPort.IsOpen)
            {
                try
                {
                    string message2 = this.SerialPort.ReadTo(this.Delimiter.ToString());
                    bytesReceived.AddRange(System.Text.Encoding.Default.GetBytes(message2));
                }
                catch (Exception ex)
                {
                    ExceptionThrown?.Invoke(this, ex);
                }
            }

            if (bytesReceived.Count > 0)
            {
                DataReceived?.Invoke(sender, new ByteData(bytesReceived.ToArray(), this.SerialPort.Encoding));
            }
            List<byte> bytelist = new List<byte>();
            foreach (var item in bytesReceived)
            {
                if (item == BitConverter.GetBytes(this.Delimiter)[0])
                {
                    DelimiterDataReceived?.Invoke(sender, new ByteData(bytelist.ToArray(), this.SerialPort.Encoding));
                    bytelist.Clear();
                }
                else
                {
                    bytelist.Add(item);
                }
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ExceptionThrown?.Invoke(sender, new Exception(e.ToString()));
        }

        private void SimpleSerialPort_DataReceived(object sender, ByteData e)
        {
            this.reply = e;
            this.hasReplied.Set();
        }
        public object Clone()
        {
            return (EasySerialPort)this.MemberwiseClone();
        }
    }
    public enum BaudRate
    {
        [Description("BaudRate:4800")]
        BR_4800=4800,
        [Description("BaudRate:9600")]
        BR_9600 = 9600,
        [Description("BaudRate:19200")]
        BR_19200 = 19200,
        [Description("BaudRate:38400")]
        BR_38400 = 38400,
        [Description("BaudRate:57600")]
        BR_57600 = 57600,
        [Description("BaudRate:115200")]
        BR_115200 = 115200
       
    }
    public enum DataBits
    {
        [Description("DataBits:5")]
        DB_5 = 5,
        [Description("DataBits:6")]
        DB_6 = 6,
        [Description("DataBits:7")]
        DB_7 = 7,
        [Description("DataBits:8")]
        DB_8 = 8,
    }
}
