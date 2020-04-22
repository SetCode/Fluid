using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Management;

namespace CSharp_OPTControllerAPI
{
    // using OPTControllerHandleType = Int64; //when using x64 compiler, use this;
    using OPTControllerHandleType = Int32;//when using x86 compiler, use this;

    public class OPTControllerAPI
    {
        const string OPTControler_DLL = "OPTController.dll";
        private OPTControllerHandleType ControllerHandle = 0;


        public const int OPT_SUCCEED = 0;       //operation succeed					 
        public const int OPT_ERR_INVALIDHANDLE = 3001001; //invalid handle3001001  
        public const int OPT_ERR_UNKNOWN = 3001002; //error unknown 
        public const int OPT_ERR_INITSERIAL_FAILED = 3001003; //failed to initialize a serial port
        public const int OPT_ERR_RELEASESERIALPORT_FAILED = 3001004; //failed to release a serial port
        public const int OPT_ERR_SERIALPORT_UNOPENED = 3001005; //attempt to access an unopened serial port
        public const int OPT_ERR_CREATEETHECON_FAILED = 3001006; //failed to create an Ethernet connection
        public const int OPT_ERR_DESTORYETHECON_FAILED = 3001007; //failed to destroy an Ethernet connection
        public const int OPT_ERR_SN_NOTFOUND = 3001008; //SN is not found
        public const int OPT_ERR_TURNONCH_FAILED = 3001009; //failed to turn on the specified channel(s)
        public const int OPT_ERR_TURNOFFCH_FAILED = 3001010; //failed to turn off the specified channel(s)
        public const int OPT_ERR_SET_INTENSITY_FAILED = 3001011; //failed to set the intensity for the specified channel(s)
        public const int OPT_ERR_READ_INTENSITY_FAILED = 3001012; //failed to read the intensity for the specified channel(s)
        public const int OPT_ERR_SET_TRIGGERWIDTH_FAILED = 3001013; //failed to set trigger pulse width
        public const int OPT_ERR_READ_TRIGGERWIDTH_FAILED = 3001014; //failed to read trigger pulse width	
        public const int OPT_ERR_READ_HBTRIGGERWIDTH_FAILED = 3001015;//failed to read high brightness trigger pulse width
        public const int OPT_ERR_SET_HBTRIGGERWIDTH_FAILED = 3001016;//failed to set high brightness trigger pulse width
        public const int OPT_ERR_READ_SN_FAILED = 3001017; //failed to read serial number
        public const int OPT_ERR_READ_IPCONFIG_FAILED = 3001018; //failed to read IP address
        public const int OPT_ERR_CHINDEX_OUTRANGE = 3001019; //index(es) out of the range
        public const int OPT_ERR_WRITE_FAILED = 3001020; //failed to write data
        public const int OPT_ERR_PARAM_OUTRANGE = 3001021; //parameter(s) out of the range 

        [StructLayout(LayoutKind.Sequential)]
        public struct IntensityItem
        {
            public int channel;
            public int intensity;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct TriggerWidthItem
        {
            public int channel;
            public int triggerWidth;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct HBTriggerWidthItem
        {
            public int channel;
            public int HBTriggerWidth;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MaxCurrentItem
        {
            public int channel;
            public int SoftwareTriggerTime;
        };


        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_InitSerialPort(String SerialPortName, OPTControllerHandleType* ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReleaseSerialPort(OPTControllerHandleType ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_CreateEtheConnectionByIP(String IP, OPTControllerHandleType* ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_CreateEtheConnectionBySN(String SN, OPTControllerHandleType* ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_DestoryEtheConnection(OPTControllerHandleType ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_TurnOnChannel(OPTControllerHandleType ControllerHandle, int Channel);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_TurnOnMultiChannel(OPTControllerHandleType ControllerHandle, int[] ChannelArray, int len);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_TurnOffChannel(OPTControllerHandleType ControllerHandle, int Channel);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_TurnOffMultiChannel(OPTControllerHandleType ControllerHandle, int[] ChannelArray, int len);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetIntensity(OPTControllerHandleType ControllerHandle, int Channel, int vlaue);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetMultiIntensity(OPTControllerHandleType ControllerHandle, IntensityItem[] intensityArray, int length);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadIntensity(OPTControllerHandleType ControllerHandle, int Channel, int* vlaue);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetTriggerWidth(OPTControllerHandleType ControllerHandle, int Channel, int Value);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetMultiTriggerWidth(OPTControllerHandleType ControllerHandle, TriggerWidthItem[] triggerWidthArray, int length);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadTriggerWidth(OPTControllerHandleType ControllerHandle, int Channel, int* vlaue);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetHBTriggerWidth(OPTControllerHandleType ControllerHandle, int Channel, int Value);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetMultiHBTriggerWidth(OPTControllerHandleType ControllerHandle, HBTriggerWidthItem[] HBTriggerWidthArray, int length);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadHBTriggerWidth(OPTControllerHandleType ControllerHandle, int Channel, int* vlaue);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_EnableResponse(OPTControllerHandleType ControllerHandle, bool isResponse);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_EnableCheckSum(OPTControllerHandleType ControllerHandle, bool isCheckSum);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_EnablePowerOffBackup(OPTControllerHandleType ControllerHandle, bool isSave);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadSN(OPTControllerHandleType ControllerHandle, StringBuilder SN);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadIPConfig(OPTControllerHandleType ControllerHandle, StringBuilder IP, StringBuilder subnetMask, StringBuilder defaultGateway);


        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ConnectionResetByIP(String IP);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetMaxCurrent(OPTControllerHandleType ControllerHandle, int channelIndex, int current);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetMultiMaxCurrent(OPTControllerHandleType ControllerHandle, MaxCurrentItem[] softwareTriggerArray, int length);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadMaxCurrent(OPTControllerHandleType ControllerHandle, int channelIndex, int* current);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetTriggerActivation(OPTControllerHandleType ControllerHandle, int triggerActivation);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadTriggerActivation(OPTControllerHandleType ControllerHandle, int* triggerActivation);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetWorkMode(OPTControllerHandleType ControllerHandle, int workMode);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadWorkMode(OPTControllerHandleType ControllerHandle,int *workMode);

        //运行时提示dll中并没有该函数
        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_ReadMaxFrequency(OPTControllerHandleType ControllerHandle, int *maxFrequency);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_AutoDetectLoadOnce(OPTControllerHandleType ControllerHandle);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetInnerTriggerFrequency(OPTControllerHandleType controllerHandle,int channelIndex,int frequency);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetLoadMode(OPTControllerHandleType controllerHandle, int loadModel);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetEtheConnectionHeartBeat(OPTControllerHandleType ControllerHandle, UInt32 timeout);


        /*******/
        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_GetControllerListOnEthernet(StringBuilder snList);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_GetChannelState(OPTControllerHandleType controllerHandle, int channelIdx, int* state);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SetKeepaliveParameter(OPTControllerHandleType controllerHandle, int keepalive_time,
                                         int keepalive_intvl, int keepalive_probes);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_EnableKeepalive(OPTControllerHandleType controllerHandle, bool enable);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_SoftwareTrigger(OPTControllerHandleType controllerHandle, int channelIdx, int time);

        [DllImport(OPTControler_DLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe
        int
        OPTController_MultiSoftwareTrigger(OPTControllerHandleType controllerHandle, MaxCurrentItem[] softwareTriggerArray, int length);


        /******************************************************************************************/
        public unsafe
        int
        ConnectionResetByIP(string IP)
        {
            int iRet = 0;
            iRet = OPTController_ConnectionResetByIP(IP);
            return iRet;
        }

        public unsafe
        int
        InitSerialPort(string SerialPortName)
        {
            fixed (OPTControllerHandleType* controllerHandle = &ControllerHandle)
            {
                return OPTController_InitSerialPort(SerialPortName, controllerHandle);
            }
        }

        public unsafe
        int
        ReleaseSerialPort()
        {
            int iRet = OPTController_ReleaseSerialPort(ControllerHandle);
            ControllerHandle = 0;
            return iRet;
        }

        public unsafe
        int
        CreateEtheConnectionByIP(string ComName)
        {
            fixed (OPTControllerHandleType* controllerHandle = &ControllerHandle)
            {
                return OPTController_CreateEtheConnectionByIP(ComName, controllerHandle);
            }
        }

        public unsafe
        int
        CreateEtheConnectionBySN(string ComName)
        {
            fixed (OPTControllerHandleType* controllerHandle = &ControllerHandle)
            {
                return OPTController_CreateEtheConnectionBySN(ComName, controllerHandle);
            }
        }

        public unsafe
        int
        DestoryEtheConnect()
        {
            int iRet = OPTController_DestoryEtheConnection(ControllerHandle);
            ControllerHandle = 0;
            return iRet;
        }

        public unsafe
        int
        TurnOnChannel(int Channel)
        {
            return OPTController_TurnOnChannel(ControllerHandle, Channel);
        }

        public unsafe
        int
        TurnOnMultiChannel(int[] ChannelArray, int len)
        {
            return OPTController_TurnOnMultiChannel(ControllerHandle, ChannelArray, len);
        }

        public unsafe
        int
        TurnOffChannel(int Channel)
        {
            return OPTController_TurnOffChannel(ControllerHandle, Channel);
        }

        public unsafe
        int
        TurnOffMultiChannel(int[] ChannelArray, int len)
        {
            return OPTController_TurnOffMultiChannel(ControllerHandle, ChannelArray, len);
        }

        public unsafe
        int
        SetIntensity(int Channel, int Value)
        {
            return OPTController_SetIntensity(ControllerHandle, Channel, Value);
        }

        public unsafe
        int
        SetMultiIntensity(IntensityItem[] IntensityArray, int len)
        {
            return OPTController_SetMultiIntensity(ControllerHandle, IntensityArray, len);
        }

        public unsafe
        int
        ReadIntensity(int Channel, ref int Value)
        {
            fixed (int* value = &Value)
            {
                return OPTController_ReadIntensity(ControllerHandle, Channel, value);
            }
        }

        public unsafe
        int
        SetTriggerWidth(int Channel, int Value)
        {
            return OPTController_SetTriggerWidth(ControllerHandle, Channel, Value);
        }

        public unsafe
        int
        SetMultiTriggerWidth(TriggerWidthItem[] TriggerWidthArray, int len)
        {
            return OPTController_SetMultiTriggerWidth(ControllerHandle, TriggerWidthArray, len);
        }

        public unsafe
        int
        ReadTriggerWidth(int Channel, ref int Value)
        {
            fixed (int* value = &Value)
            {
                return OPTController_ReadTriggerWidth(ControllerHandle, Channel, value);
            }
        }

        public unsafe
        int
        SetHBTriggerWidth(int Channel, int Value)
        {
            return OPTController_SetHBTriggerWidth(ControllerHandle, Channel, Value);
        }

        public unsafe
        int
        SetMultiHBTriggerWidth(HBTriggerWidthItem[] HBTriggerWidthArray, int len)
        {
            return OPTController_SetMultiHBTriggerWidth(ControllerHandle, HBTriggerWidthArray, len);
        }

        public unsafe
        int
        ReadHBTriggerWidth(int Channel, ref int Value)
        {
            fixed (int* value = &Value)
            {
                return OPTController_ReadHBTriggerWidth(ControllerHandle, Channel, value);
            }
        }

        public unsafe
        int
        EnableResponse(bool isResponse)
        {
            return OPTController_EnableResponse(ControllerHandle, isResponse);
        }

        public unsafe
        int
        EnableCheckSum(bool isCheckSum)
        {
            return OPTController_EnableCheckSum(ControllerHandle, isCheckSum);
        }

        public unsafe
        int
        EnablePowerOffBackup(bool isSave)
        {
            return OPTController_EnablePowerOffBackup(ControllerHandle, isSave);
        }

        public unsafe
        int
        ReadSN(StringBuilder SN)
        {
            return OPTController_ReadSN(ControllerHandle, SN);
        }

        public unsafe
        int
        ReadIPConfig(StringBuilder IP, StringBuilder subnetMask, StringBuilder defaultGateway)
        {
            return OPTController_ReadIPConfig(ControllerHandle, IP, subnetMask, defaultGateway);
        }

        public unsafe
        int
        SetMaxCurrent(int Channel, int Value)
        {
            return OPTController_SetMaxCurrent(ControllerHandle, Channel, Value);
        }

        public unsafe
        int
        SetMultiMaxCurrent(MaxCurrentItem[] maxCurrentArray, int length)
        {
            return OPTController_SetMultiMaxCurrent(ControllerHandle, maxCurrentArray, length);
        }

        public unsafe
        int
        ReadMaxCurrent(int channelIndex, ref int Value)
        {
            fixed (int* value = &Value)
            {
                return OPTController_ReadMaxCurrent(ControllerHandle, channelIndex, value);
            }
        }

        public unsafe
        int
        SetTriggerActivation(int triggerActivation)
        {
            return OPTController_SetTriggerActivation(ControllerHandle, triggerActivation);
        }

        public unsafe
        int
        ReadTriggerActivation( ref int triggerActivation)
        {
             fixed (int* value = &triggerActivation)
            {
                return OPTController_ReadTriggerActivation(ControllerHandle, value);
            }
        }

        public unsafe
        int
        SetWorkMode(int workMode)
        {
            return OPTController_SetWorkMode(ControllerHandle, workMode);
        }

        public unsafe
        int
        ReadWorkMode(ref int workMode)
        {
            fixed (int* value = &workMode)
            {
                return OPTController_ReadTriggerActivation(ControllerHandle, value);
            }
        }

        public unsafe
        int
        ReadMaxFrequency(ref int maxFrequency)
        {
            fixed (int* value = &maxFrequency)
            {
                return OPTController_ReadMaxFrequency(ControllerHandle, value);
            }
        }

        public unsafe
        int
        SetInnerTriggerFrequency(int channelIndex, int frequency)
        {
            return OPTController_SetInnerTriggerFrequency(ControllerHandle, channelIndex, frequency);
        }

        public unsafe
        int
        SetLoadMode(int loadModel)
        {
            return OPTController_SetLoadMode(ControllerHandle, loadModel);
        }

        public unsafe
        int
        AutoDetectLoadOnce()
        {
            return OPTController_AutoDetectLoadOnce(ControllerHandle);
        }

        public unsafe
        int
        SetEtheConnectionHeartBeat(UInt32 timeout)
        {
            return OPTController_SetEtheConnectionHeartBeat(ControllerHandle, timeout);
        }

        /******************/
        public unsafe
        int
        GetControllerListOnEthernet(StringBuilder snList)
        {
            return OPTController_GetControllerListOnEthernet(snList);
        }

        public unsafe
        int
        GetChannelState(int channelIdx, int* state)
        {
            return OPTController_GetChannelState(ControllerHandle, channelIdx, state);
        }

        public unsafe
        int
        SetKeepaliveParameter(int keepalive_time, int keepalive_intvl, int keepalive_probes)
        {
            return OPTController_SetKeepaliveParameter(ControllerHandle, keepalive_time, keepalive_intvl, keepalive_probes);
        }

        public unsafe
        int
        EnableKeepalive(bool enable)
        {
            return OPTController_EnableKeepalive(ControllerHandle, enable);
        }

        public unsafe
        int
        SoftwareTrigger(int channelIdx, int time)
        {
            return OPTController_SoftwareTrigger(ControllerHandle, channelIdx, time);
        }

        public unsafe
        int
        MultiSoftwareTrigger(MaxCurrentItem[] softwareTriggerArray, int length)
        {
            return OPTController_MultiSoftwareTrigger(ControllerHandle, softwareTriggerArray, length);
        }

    }
}
