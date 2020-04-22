using Anda.Fluid.Infrastructure.Communication;
using Anda.Fluid.Infrastructure.DataStruct;
using Modbus.Device;
using Modbus.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class AiKaThermostat : IHeaterControllable
    {
        public AiKaThermostat(byte slaveAddress, EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
            this.SlaveAddr = slaveAddress;
        }


        /// <summary>
        /// 输出控制选择地址，对应温控器的DO0到DO7；
        /// </summary>
        public ushort[] ControlSelAddr => new ushort[] { 128, 136, 144, 152, 160, 168, 176, 184 };

        /// <summary>
        /// 各个通道pid起始地址
        /// </summary>
        public ushort[] PIDStartAddr => new ushort[] { 129, 137, 145, 153, 161, 169, 177, 185 };

        /// <summary>
        /// 从站地址默认为1
        /// </summary>
        public byte SlaveAddr { get; private set; } = 1;

        /// <summary>
        /// 获取从站地址的寄存器地址
        /// </summary>
        public ushort GetSlaveAddrCmd => 24;

        /// <summary>
        /// 加热地址128，136，144，152，160，168，176，184
        /// </summary>
        public ushort HeatingAddr => 128;

        /// <summary>
        /// 加热指令
        /// </summary>
        public ushort StartHeatingAddr => 128;

        /// <summary>
        /// 停止加热指令
        /// </summary>
        public ushort StopHeatingAddr => 136;

        /// <summary>
        /// 控温温度起始地址  64, 65, 66, 67, 68, 69, 70, 71
        /// </summary>
        public ushort CtrlTempAddr => 64;

        /// <summary>
        /// 测量值地址 0,1,2,3,4,5,6,7（对应8个通道模拟量输入）
        /// </summary>
        public ushort MeasureValueAddr => 0;

        /// <summary>
        /// 最大测量个数，艾卡为8个模拟量输入通道
        /// </summary>
        public ushort MaxMeasureNum => 8;

        /// <summary>
        /// PID控制参数
        /// </summary>
        public PIDParamStruct PIDParams => new PIDParamStruct()
        {
            SampleTime = 4,
            SeriesD0 = 1000,
            SeriesD1 = 1,
            SeriesD2 = 15000,
            CtrlArea = 100,
            OutputBase = 10
        };

        /// <summary>
        /// 绝对报警上限值地址；通道1为224，依次累加4
        /// </summary>
        public ushort UpLimitAlarmAddr => 224;

        /// <summary>
        /// 绝对报警下限值地址；通道1为225，依次累加4
        /// </summary>
        public ushort DownLimitAlarmAddr => 225;

        /// <summary>
        /// 相对报警设置地址；通道1为226，依次累加4（一定要设为0）
        /// </summary>
        public ushort RelateveAlarmAddr => 226;

        /// <summary>
        /// 控温开关，0全部关，1为全部开
        /// </summary>
        public ushort[] CtrlTempSwitch => new ushort[] { 0, 7 };

        /// <summary>
        /// 控温开关地址
        /// </summary>
        public ushort CtrlTempSwitchAddr => 262;

        /// <summary>
        /// 控温输出选择地址
        /// </summary>
        public ushort[] CtrlTempOutput => new ushort[] { 48, 49, 50, 51, 52, 53, 54, 55 };

        /// <summary>
        /// 加热状态起始地址
        /// </summary>
        public ushort HeatingState => 80;

        /// <summary>
        /// 控温状态起始地址
        /// </summary>
        public ushort HoldingState => 88;

        /// <summary>
        /// 上限报警起始地址
        /// </summary>
        public ushort UpAlarmAddr => 16;

        /// <summary>
        /// 下限报警起始地址
        /// </summary>
        public ushort DownAlarmAddr => 24;

        /// <summary>
        /// 温度开关起始地址96，97，92，99，100，101，102，103
        /// </summary>
        public ushort TempSwitchAddr => 96;

        public EasySerialPort EasySerialPort { get; private set; } = null;

        public ModbusMaster ModbusMaster { get; private set; } = null;

        public ComCommunicationSts CommunicationOK { get; private set; }

        public bool Connect(TimeSpan timeout)
        {
            if (this.EasySerialPort == null)
            {
                return false;
            }
            if (!this.EasySerialPort.Open())
            {
                return false;
            }
            CommunicationOK = ComCommunicationSts.OK;
            SerialPortAdapter adapter = new SerialPortAdapter(this.EasySerialPort.SerialPort);
            this.ModbusMaster = ModbusSerialMaster.CreateRtu(adapter);
            return true;
        }

        public void Disconnect()
        {
            if (SensorMgr.Instance.Heater.Vendor == HeaterControllerMgr.Vendor.Aika)
            {
                for (int i = 0; i < HeaterControllerMgr.Instance.FindBy(0).HeaterPrm.acitveChanel.Length; i++)
                {
                    HeaterControllerMgr.Instance.FindBy(0).Fire(new HeaterMessage(HeaterMsg.停止加热,
                        HeaterControllerMgr.Instance.FindBy(0), i));
                }
            }
            Thread.Sleep(3000);

            this.EasySerialPort?.Close();
            this.ModbusMaster?.Dispose();
        }
        public bool SetTemp(double value, int chanelNo)
        {
            if (value <= 0)
            {
                return false;
            }
            try
            {
                byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.CtrlTempAddr + chanelNo), this.ConvertToWriteTemp(value));
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetTemp(out double result, int chanelNo)
        {
            try
            {
                if (this.ModbusMaster == null)
                {
                    result = 0;
                    return false;
                }
                byte[] bytes = this.ModbusMaster.BuildReadMessage(this.SlaveAddr, (ushort)chanelNo, 2);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                if (data == null)
                {
                    result = 0;
                    return false;
                }
                this.CommunicationOK = ComCommunicationSts.OK;
                string s1 = data.Bytes[3].ToString("X2");
                string s2 = data.Bytes[4].ToString("X2");
                result = Convert.ToInt32((s1 + s2), 16) * 0.1;
                return true;
            }
            catch (Exception)
            {
                result = 0;
                return false;
            }
        }

        public bool SetAlarmTemp(double value, ToleranceType limit, int chanelNo)
        {
            if (value <= 0)
            {
                return false;
            }
            if (limit == ToleranceType.High)
            {
                try
                {
                    byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.UpLimitAlarmAddr + chanelNo * 4), this.ConvertToWriteTemp(value));
                    this.EasySerialPort.SerialPort.DiscardInBuffer();
                    ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.DownLimitAlarmAddr + chanelNo * 4), this.ConvertToWriteTemp(value));
                    this.EasySerialPort.SerialPort.DiscardInBuffer();
                    ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool GetAlarmTemp(out double result, ToleranceType limit, int chanelNo)
        {
            try
            {
                if (limit == ToleranceType.High)
                {
                    byte[] bytes = this.ModbusMaster.BuildReadMessage(this.SlaveAddr, (ushort)(this.UpLimitAlarmAddr + chanelNo * 4), 2);
                    this.EasySerialPort.SerialPort.DiscardInBuffer();
                    ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                    string s1 = data.Bytes[3].ToString("X2");
                    string s2 = data.Bytes[4].ToString("X2");
                    result = Convert.ToInt32((s1 + s2), 16) * 0.1;

                }
                else
                {
                    byte[] bytes = this.ModbusMaster.BuildReadMessage(this.SlaveAddr, (ushort)(this.DownLimitAlarmAddr + chanelNo * 4), 2);
                    this.EasySerialPort.SerialPort.DiscardInBuffer();
                    ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                    string s1 = data.Bytes[3].ToString("X2");
                    string s2 = data.Bytes[4].ToString("X2");
                    result = Convert.ToInt32((s1 + s2), 16) * 0.1;
                }
                return true;
            }
            catch (Exception)
            {
                result = 0;
                return false;
            }
        }

        public bool SetTempOffset(double value, int chanelNo)
        {
            return false;
        }

        public bool GetTempOffset(out double result, int chanelNo)
        {
            result = 0;
            return false;
        }

        private ushort ConvertToWriteTemp(double value)
        {
            return Convert.ToUInt16(value * 10);
        }

        public void Update()
        {
            if (!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }

        public bool StartHeating(int chanelNo)
        {
            try
            {
                byte[] bytes1 = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.HeatingAddr + chanelNo * 8), this.StartHeatingAddr);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data1 = this.EasySerialPort.WriteAndGetReply(bytes1, TimeSpan.FromSeconds(1));

                Thread.Sleep(30);

                byte[] bytes2 = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.TempSwitchAddr + chanelNo), 1);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data2 = this.EasySerialPort.WriteAndGetReply(bytes2, TimeSpan.FromSeconds(1));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StopHeating(int chanelNo)
        {
            try
            {
                byte[] bytes1 = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.HeatingAddr + chanelNo * 8), this.StopHeatingAddr);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data1 = this.EasySerialPort.WriteAndGetReply(bytes1, TimeSpan.FromSeconds(1));

                Thread.Sleep(30);

                byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.TempSwitchAddr + chanelNo), 0);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StartAlarm(int chanelNo)
        {
            try
            {
                byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.SlaveAddr, (ushort)(this.RelateveAlarmAddr + chanelNo * 4), 0);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
