using Anda.Fluid.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;
using Modbus.IO;
using System.ComponentModel;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Infrastructure.DataStruct;
using Newtonsoft.Json;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using System.Threading;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Sensors;

namespace Anda.Fluid.Drive.Sensors.Heater
{
    public class ThermostatOmron : IHeaterControllable
    {
        public ThermostatOmron(byte slaveAddress, EasySerialPort easySerialPort)
        {
            this.EasySerialPort = easySerialPort;
            this.AddressSlave = slaveAddress;

        }
        public ushort AddrTempOffset => 10019;
        public ushort AddrLowerAlarm => 8457;
        public ushort AddrUpperAlarm => 8456;
        public ushort AddrSetTemp => 8451;

        public ushort AddrGetTemp => 8192;

        public byte AddressSlave { get; private set; } = 1;


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
                byte[] bytes = this.ModbusMaster.BuildWriteMessage(this.AddressSlave, this.AddrSetTemp, this.ConvertToWriteTemp(value));
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
                byte[] bytes = this.ModbusMaster.BuildReadMessage(this.AddressSlave, this.AddrGetTemp, 2);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                if(data == null)
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
            try
            {
                byte[] bytes = null;
                if (limit == ToleranceType.High)
                {
                    bytes = this.ModbusMaster.BuildWriteMessage(this.AddressSlave, this.AddrUpperAlarm, this.ConvertToWriteTemp(value));
                }
                else
                {
                    bytes = this.ModbusMaster.BuildWriteMessage(this.AddressSlave, this.AddrLowerAlarm, this.ConvertToWriteTemp(value));
                }
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetAlarmTemp(out double result, ToleranceType limit, int chanelNo)
        {

            try
            {
                if (limit == ToleranceType.High)
                {
                    byte[] bytes = this.ModbusMaster.BuildReadMessage(this.AddressSlave, this.AddrUpperAlarm, 2);
                    this.EasySerialPort.SerialPort.DiscardInBuffer();
                    ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                    string s1 = data.Bytes[3].ToString("X2");
                    string s2 = data.Bytes[4].ToString("X2");
                    result = Convert.ToInt32((s1 + s2), 16) * 0.1;

                }
                else
                {
                    byte[] bytes = this.ModbusMaster.BuildReadMessage(this.AddressSlave, this.AddrLowerAlarm, 2);
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
            try
            {
                string hexValue = string.Empty;
                ushort writeValue = 0;
                byte[] bytes = null;
                if (value < 0)
                {

                    hexValue = ConvertUtil.NegativeToHexString(Convert.ToInt32(value * 10.0));
                    writeValue = Convert.ToUInt16(hexValue, 16);
                    bytes = this.ModbusMaster.BuildWriteMessage(this.AddressSlave, this.AddrTempOffset, writeValue);

                }
                else
                {
                    bytes = this.ModbusMaster.BuildWriteMessage(this.AddressSlave, this.AddrTempOffset, this.ConvertToWriteTemp(value));
                }
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetTempOffset(out double result, int chanelNo)
        {

            try
            {
                byte[] bytes = this.ModbusMaster.BuildReadMessage(this.AddressSlave, this.AddrTempOffset, 2);
                this.EasySerialPort.SerialPort.DiscardInBuffer();
                ByteData data = this.EasySerialPort.WriteAndGetReply(bytes, TimeSpan.FromSeconds(1));
                string s1 = data.Bytes[3].ToString("X2");
                string s2 = data.Bytes[4].ToString("X2");

                var rst = s1 + s2;
                result = ConvertUtil.HexStringToNegative(rst) / 10.0;
                return true;
            }
            catch (Exception)
            {
                result = 0;
                return false;
            }
        }

        private bool IsOutOfRange(double value)
        {
            if (value.ToString().Length - value.ToString().IndexOf(".") - 1 > 1)
            {
                return true;
            }
            return false;
        }

        private ushort ConvertToWriteTemp(double value)
        {
            return Convert.ToUInt16(value * 10);
        }

        private double ConvertToRealTemp(ushort value)
        {
            return value / 10.0;
        }

        public void Update()
        {
            if (this.EasySerialPort==null)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
                return;
            }
            if(!this.EasySerialPort.Connected)
            {
                this.CommunicationOK = ComCommunicationSts.ERROR;
            }
        }

        public bool StartHeating( int chanelNo)
        {
            return true;
        }

        public bool StopHeating(int chanelNo)
        {
            return true;
        }

        public bool StartAlarm(int chanelNo)
        {
            return true;
        }

    }
}
