using System.Runtime.InteropServices;

namespace gts
{
    public class mc
    {
        public const short DLL_VERSION_0 = 2;
        public const short DLL_VERSION_1 = 1;
        public const short DLL_VERSION_2 = 0;

        public const short DLL_VERSION_3 = 1;
        public const short DLL_VERSION_4 = 5;
        public const short DLL_VERSION_5 = 0;
        public const short DLL_VERSION_6 = 6;
        public const short DLL_VERSION_7 = 0;
        public const short DLL_VERSION_8 = 7;

        public const short MC_NONE = -1;

        public const short MC_LIMIT_POSITIVE = 0;
        public const short MC_LIMIT_NEGATIVE = 1;
        public const short MC_ALARM = 2;
        public const short MC_HOME = 3;
        public const short MC_GPI = 4;
        public const short MC_ARRIVE = 5;
        public const short MC_MPG = 6;

        public const short MC_ENABLE = 10;
        public const short MC_CLEAR = 11;
        public const short MC_GPO = 12;

        public const short MC_DAC = 20;
        public const short MC_STEP = 21;
        public const short MC_PULSE = 22;
        public const short MC_ENCODER = 23;
        public const short MC_ADC = 24;

        public const short MC_AXIS = 30;
        public const short MC_PROFILE = 31;
        public const short MC_CONTROL = 32;

        public const short CAPTURE_HOME = 1;
        public const short CAPTURE_INDEX = 2;
        public const short CAPTURE_PROBE = 3;
        public const short CAPTURE_HSIO0 = 6;
        public const short CAPTURE_HSIO1 = 7;
        public const short CAPTURE_HOME_GPI = 8;

        public const short PT_MODE_STATIC = 0;
        public const short PT_MODE_DYNAMIC = 1;

        public const short PT_SEGMENT_NORMAL = 0;
        public const short PT_SEGMENT_EVEN = 1;
        public const short PT_SEGMENT_STOP = 2;

        public const short GEAR_MASTER_ENCODER = 1;
        public const short GEAR_MASTER_PROFILE = 2;
        public const short GEAR_MASTER_AXIS = 3;

        public const short FOLLOW_MASTER_ENCODER = 1;
        public const short FOLLOW_MASTER_PROFILE = 2;
        public const short FOLLOW_MASTER_AXIS = 3;

        public const short FOLLOW_EVENT_START = 1;
        public const short FOLLOW_EVENT_PASS = 2;

        public const short GEAR_EVENT_START = 1;
        public const short GEAR_EVENT_PASS = 2;
        public const short GEAR_EVENT_AREA = 5;

        public const short FOLLOW_SEGMENT_NORMAL = 0;
        public const short FOLLOW_SEGMENT_EVEN = 1;
        public const short FOLLOW_SEGMENT_STOP = 2;
        public const short FOLLOW_SEGMENT_CONTINUE = 3;

        public const short INTERPOLATION_AXIS_MAX = 4;
        public const short CRD_FIFO_MAX = 4096;
        public const short FIFO_MAX = 2;
        public const short CRD_MAX = 2;
        public const short CRD_OPERATION_DATA_EXT_MAX = 2;

        public const short CRD_OPERATION_TYPE_NONE = 0;
        public const short CRD_OPERATION_TYPE_BUF_IO_DELAY = 1;
        public const short CRD_OPERATION_TYPE_LASER_ON = 2;
        public const short CRD_OPERATION_TYPE_LASER_OFF = 3;
        public const short CRD_OPERATION_TYPE_BUF_DA = 4;
        public const short CRD_OPERATION_TYPE_LASER_CMD = 5;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW = 6;
        public const short CRD_OPERATION_TYPE_LMTS_ON = 7;
        public const short CRD_OPERATION_TYPE_LMTS_OFF = 8;
        public const short CRD_OPERATION_TYPE_SET_STOP_IO = 9;
        public const short CRD_OPERATION_TYPE_BUF_MOVE = 10;
        public const short CRD_OPERATION_TYPE_BUF_GEAR = 11;
        public const short CRD_OPERATION_TYPE_SET_SEG_NUM = 12;
        public const short CRD_OPERATION_TYPE_STOP_MOTION = 13;
        public const short CRD_OPERATION_TYPE_SET_VAR_VALUE = 14;
        public const short CRD_OPERATION_TYPE_JUMP_NEXT_SEG = 15;
        public const short CRD_OPERATION_TYPE_SYNCH_PRF_POS = 16;
        public const short CRD_OPERATION_TYPE_VIRTUAL_TO_ACTUAL = 17;
        public const short CRD_OPERATION_TYPE_SET_USER_VAR = 18;
        public const short CRD_OPERATION_TYPE_SET_DO_BIT_PULSE = 19;
        public const short CRD_OPERATION_TYPE_BUF_COMPAREPULSE = 20;
        public const short CRD_OPERATION_TYPE_LASER_ON_EX = 21;
        public const short CRD_OPERATION_TYPE_LASER_OFF_EX = 22;
        public const short CRD_OPERATION_TYPE_LASER_CMD_EX = 23;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW_RATIO_EX = 24;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW_MODE = 25;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW_OFF = 26;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW_OFF_EX = 27;
        public const short CRD_OPERATION_TYPE_LASER_FOLLOW_SPLINE = 28;
        public const short CRD_OPERATION_TYPE_MOTION_DATA = 29;

        public const short INTERPOLATION_MOTION_TYPE_LINE = 0;
        public const short INTERPOLATION_MOTION_TYPE_CIRCLE = 1;
        public const short INTERPOLATION_MOTION_TYPE_HELIX = 2;
        public const short INTERPOLATION_MOTION_TYPE_CIRCLE_3D = 3;

        public const short INTERPOLATION_CIRCLE_PLAT_XY = 0;
        public const short INTERPOLATION_CIRCLE_PLAT_YZ = 1;
        public const short INTERPOLATION_CIRCLE_PLAT_ZX = 2;

        public const short INTERPOLATION_HELIX_CIRCLE_XY_LINE_Z = 0;
        public const short INTERPOLATION_HELIX_CIRCLE_YZ_LINE_X = 1;
        public const short INTERPOLATION_HELIX_CIRCLE_ZX_LINE_Y = 2;

        public const short INTERPOLATION_CIRCLE_DIR_CW = 0;
        public const short INTERPOLATION_CIRCLE_DIR_CCW = 1;

        public const short COMPARE_PORT_HSIO = 0;
        public const short COMPARE_PORT_GPO = 1;

        public const short COMPARE2D_MODE_2D = 1;
        public const short COMPARE2D_MODE_1D = 0;

        public const short INTERFACEBOARD20 = 2;
        public const short INTERFACEBOARD30 = 3;

        public const short AXIS_LASER = 7;
        public const short AXIS_LASER_EX = 8;

        public const short LASER_CTRL_MODE_PWM1 = 0;
        public const short LASER_CTRL_FREQUENCY = 1;
        public const short LASER_CTRL_VOLTAGE = 2;
        public const short LASER_CTRL_MODE_PWM2 = 3;

        public struct TTrapPrm
        {
            public double acc;
            public double dec;
            public double velStart;
            public short  smoothTime;
        }

        public struct TJogPrm
        {
            public double acc;
            public double dec;
            public double smooth;
        }

        public struct TPid
        {
            public double kp;
            public double ki;
            public double kd;
            public double kvff;
            public double kaff;

            public int integralLimit;
            public int derivativeLimit;
            public short  limit;
        }

        public struct TThreadSts
        {
            public short run;
            public short error;
            public double result;
            public short line;
        }

        public struct TVarInfo
        {
            public short id;
            public short dataType;
            public double dumb0;
            public double dumb1;
            public double dumb2;
            public double dumb3;
        }
        public struct TCompileInfo
        {
            public string pFileName;
            public short pLineNo1;
            public short pLineNo2;
            public string pMessage;
        }
        public struct TCrdPrm
        {
            public short dimension;
            public short profile1;
            public short profile2;
            public short profile3;
            public short profile4;
            public short profile5;
            public short profile6;
            public short profile7;
            public short profile8;

            public double synVelMax;
            public double synAccMax;
            public short evenTime;
            public short setOriginFlag;
            public int originPos1;
            public int originPos2;
            public int originPos3;
            public int originPos4;
            public int originPos5;
            public int originPos6;
            public int originPos7;
            public int originPos8;
        }

        public struct TCrdBufOperation
        {
            public short flag;
            public ushort delay;
            public short doType;
            public ushort doMask;
            public ushort doValue;
            public ushort dataExt1;
            public ushort dataExt2;
        }

        public struct TCrdData
        {
            public short motionType;
            public short circlePlat;
            public int posX;
            public int posY;
            public int posZ;
            public int posA;
            public double radius;
            public short circleDir;
            public double lCenterX;
            public double lCenterY;
            public double lCenterZ;
            public double vel;
            public double acc;
            public short velEndZero;
            public TCrdBufOperation operation;

            public double cosX;
            public double cosY;
            public double cosZ;
            public double cosA;
            public double velEnd;
            public double velEndAdjust;
            public double r;
        }

        public struct TTrigger
        {
            public short encoder;
            public short probeType;
            public short probeIndex;
            public short offset;
            public short windowOnly;
            public int firstPosition;
            public int lastPosition;
        }

        public struct TTriggerStatus
        {
            public short execute;
            public short done;
            public int position;
        }

        public struct T2DCompareData
        {
            public int px;
            public int py;
        }

        public struct T2DComparePrm
        {
            public short encx;
            public short ency;
            public short source;
            public short outputType;
            public short startLevel;
            public short time;
            public short maxerr;
            public short threshold; 
        }
        [DllImport("gts.dll")]
        public static extern short GT_GetDllVersion(short cardNum,out string pDllVersion);
        [DllImport("gts.dll")]
        public static extern short GT_SetCardNo(short cardNum,short index);
        [DllImport("gts.dll")]
        public static extern short GT_GetCardNo(short cardNum,out short index);

        [DllImport("gts.dll")]
        public static extern short GT_GetVersion(short cardNum,out string pVersion);
        [DllImport("gts.dll")]
        public static extern short GT_GetInterfaceBoardSts(short cardNum,out short pStatus);
        [DllImport("gts.dll")]
        public static extern short GT_SetInterfaceBoardSts(short cardNum,short type);

        [DllImport("gts.dll")]
        public static extern short GT_Open(short cardNum,short channel,short param);
        [DllImport("gts.dll")]
        public static extern short GT_Close(short cardNum);

        [DllImport("gts.dll")]
        public static extern short GT_LoadConfig(short cardNum,string pFile);

        [DllImport("gts.dll")]
        public static extern short GT_AlarmOff(short cardNum,short axis);
        [DllImport("gts.dll")]
        public static extern short GT_AlarmOn(short cardNum,short axis);
        [DllImport("gts.dll")]
        public static extern short GT_LmtsOn(short cardNum,short axis, short limitType);
        [DllImport("gts.dll")]
        public static extern short GT_LmtsOff(short cardNum,short axis, short limitType);
        [DllImport("gts.dll")]
        public static extern short GT_ProfileScale(short cardNum,short axis, short alpha, short beta);
        [DllImport("gts.dll")]
        public static extern short GT_EncScale(short cardNum,short axis, short alpha, short beta);
        [DllImport("gts.dll")]
        public static extern short GT_StepDir(short cardNum,short step);
        [DllImport("gts.dll")]
        public static extern short GT_StepPulse(short cardNum,short step);
        [DllImport("gts.dll")]
        public static extern short GT_SetMtrBias(short cardNum,short dac, short bias);
        [DllImport("gts.dll")]
        public static extern short GT_GetMtrBias(short cardNum,short dac, out short pBias);
        [DllImport("gts.dll")]
        public static extern short GT_SetMtrLmt(short cardNum,short dac, short limit);
        [DllImport("gts.dll")]
        public static extern short GT_GetMtrLmt(short cardNum,short dac, out short pLimit);
        [DllImport("gts.dll")]
        public static extern short GT_EncSns(short cardNum,ushort sense);
        [DllImport("gts.dll")]
        public static extern short GT_EncOn(short cardNum,short encoder);
        [DllImport("gts.dll")]
        public static extern short GT_EncOff(short cardNum,short encoder);
        [DllImport("gts.dll")]
        public static extern short GT_SetPosErr(short cardNum,short control, int error);
        [DllImport("gts.dll")]
        public static extern short GT_GetPosErr(short cardNum,short control, out int pError);
        [DllImport("gts.dll")]
        public static extern short GT_SetStopDec(short cardNum,short profile, double decSmoothStop, double decAbruptStop);
        [DllImport("gts.dll")]
        public static extern short GT_GetStopDec(short cardNum,short profile, out double pDecSmoothStop, out double pDecAbruptStop);
        [DllImport("gts.dll")]
        public static extern short GT_LmtSns(short cardNum,ushort sense);
        [DllImport("gts.dll")]
        public static extern short GT_CtrlMode(short cardNum,short axis, short mode);
        [DllImport("gts.dll")]
        public static extern short GT_SetStopIo(short cardNum,short axis, short stopType, short inputType, short inputIndex);
        [DllImport("gts.dll")]
        public static extern short GT_GpiSns(short cardNum,ushort sense);
        [DllImport("gts.dll")]
        public static extern short GT_SetAdcFilter(short cardNum,short adc,short filterTime);
        [DllImport("gts.dll")]
        public static extern short GT_SetAxisPrfVelFilter(short cardNum,short axis,short filterNumExp);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisPrfVelFilter(short cardNum,short axis,out short pFilterNumExp);
        [DllImport("gts.dll")]
        public static extern short GT_SetAxisEncVelFilter(short cardNum,short axis,short filterNumExp);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisEncVelFilter(short cardNum,short axis,out short pFilterNumExp);
        [DllImport("gts.dll")]
        public static extern short GT_SetAxisInputShaping(short cardNum,short axis, short enable, short count, double k);

        [DllImport("gts.dll")]
        public static extern short GT_SetDo(short cardNum,short doType,int value);
        [DllImport("gts.dll")]
        public static extern short GT_SetDoBit(short cardNum,short doType,short doIndex,short value);
        [DllImport("gts.dll")]
        public static extern short GT_GetDo(short cardNum,short doType,out int pValue);
        [DllImport("gts.dll")]
        public static extern short GT_SetDoBitReverse(short cardNum,short doType,short doIndex,short value,short reverseTime);
        [DllImport("gts.dll")]
        public static extern short GT_SetDoMask(short cardNum,short doType,ushort doMask,int value);
        [DllImport("gts.dll")]
        public static extern short GT_EnableDoBitPulse(short cardNum,short doType,short doIndex,ushort highLevelTime,ushort lowLevelTime,int pulseNum,short firstLevel);
        [DllImport("gts.dll")]
        public static extern short GT_DisableDoBitPulse(short cardNum,short doType, short doIndex);

        [DllImport("gts.dll")]
        public static extern short GT_GetDi(short cardNum,short diType,out int pValue);
        [DllImport("gts.dll")]
        public static extern short GT_GetDiReverseCount(short cardNum,short diType,short diIndex,out uint reverseCount,short count);
        [DllImport("gts.dll")]
        public static extern short GT_SetDiReverseCount(short cardNum,short diType,short diIndex,ref uint reverseCount,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetDiRaw(short cardNum,short diType,out int pValue);

        [DllImport("gts.dll")]
        public static extern short GT_SetDac(short cardNum,short dac,ref short value,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetDac(short cardNum,short dac,out short value,short count,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_GetAdc(short cardNum,short adc,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAdcValue(short cardNum,short adc,out short pValue,short count,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_SetEncPos(short cardNum,short encoder,int encPos);
        [DllImport("gts.dll")]
        public static extern short GT_GetEncPos(short cardNum,short encoder,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetEncPosPre(short cardNum,short encoder,out double pValue,short count,uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetEncVel(short cardNum,short encoder,out double pValue,short count,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_SetCaptureMode(short cardNum,short encoder,short mode);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureMode(short cardNum,short encoder,out short pMode,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureStatus(short cardNum,short encoder,out short pStatus,out int pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_SetCaptureSense(short cardNum,short encoder, short mode,short sense);
        [DllImport("gts.dll")]
        public static extern short GT_ClearCaptureStatus(short cardNum,short encoder);
        [DllImport("gts.dll")]
        public static extern short GT_SetCaptureRepeat(short cardNum,short encoder,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureRepeatStatus(short cardNum,short encoder,out short pCount);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureRepeatPos(short cardNum,short encoder, out int pValue, short startNum, short count);
        [DllImport("gts.dll")]
        public static extern short GT_SetCaptureEncoder(short cardNum,short trigger,short encoder);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureWidth(short cardNum,short trigger,out short pWidth,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetCaptureHomeGpi(short cardNum,short trigger,out short pHomeSts,out short pHomePos,out short pGpiSts,out short pGpiPos,short count);

        [DllImport("gts.dll")]
        public static extern short GT_Reset(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_GetClock(short cardNum,out uint pClock,out uint pLoop);
        [DllImport("gts.dll")]
        public static extern short GT_GetClockHighPrecision(short cardNum,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_GetSts(short cardNum,short axis,out int pSts,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_ClrSts(short cardNum,short axis,short count);
        [DllImport("gts.dll")]
        public static extern short GT_AxisOn(short cardNum,short axis);
        [DllImport("gts.dll")]
        public static extern short GT_AxisOff(short cardNum,short axis);
        [DllImport("gts.dll")]
        public static extern short GT_Stop(short cardNum,int mask,int option);
        [DllImport("gts.dll")]
        public static extern short GT_SetPrfPos(short cardNum,short profile,int prfPos);
        [DllImport("gts.dll")]
        public static extern short GT_SynchAxisPos(short cardNum,int mask);
        [DllImport("gts.dll")]
        public static extern short GT_ZeroPos(short cardNum,short axis,short count);

        [DllImport("gts.dll")]
        public static extern short GT_SetSoftLimit(short cardNum,short axis,int positive,int negative);
        [DllImport("gts.dll")]
        public static extern short GT_GetSoftLimit(short cardNum,short axis,out int pPositive,out int pNegative);
        [DllImport("gts.dll")]
        public static extern short GT_SetAxisBand(short cardNum,short axis,int band,int time);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisBand(short cardNum,short axis,out int pBand,out int pTime);
        [DllImport("gts.dll")]
        public static extern short GT_SetBacklash(short cardNum,short axis,int compValue,double compChangeValue,int compDir);
        [DllImport("gts.dll")]
        public static extern short GT_GetBacklash(short cardNum,short axis,out int pCompValue,out double pCompChangeValue,out int pCompDir);
        [DllImport("gts.dll")]
        public static extern short GT_SetLeadScrewComp(short cardNum,short axis,short n,int startPos,int lenPos,out int pCompPos,out int pCompNeg);
        [DllImport("gts.dll")]
        public static extern short GT_EnableLeadScrewComp(short cardNum,short axis,short mode);
        [DllImport("gts.dll")]
        public static extern short GT_GetCompensate(short cardNum,short axis, out double pPitchError, out double pCrossError, out double pBacklashError, out double pEncPos, out double pPrfPos);
        
        [DllImport("gts.dll")]
        public static extern short GT_EnableGantry(short cardNum,short gantryMaster,short gantrySlave,double masterKp,double slaveKp);
        [DllImport("gts.dll")]
        public static extern short GT_DisableGantry(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_SetGantryErrLmt(short cardNum,int gantryErrLmt);
        [DllImport("gts.dll")]
        public static extern short GT_GetGantryErrLmt(short cardNum,out int pGantryErrLmt);

        [DllImport("gts.dll")]
        public static extern short GT_GetPrfPos(short cardNum,short profile,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetPrfVel(short cardNum,short profile,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetPrfAcc(short cardNum,short profile,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetPrfMode(short cardNum,short profile,out int pValue,short count,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_GetAxisPrfPos(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisPrfVel(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisPrfAcc(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisEncPos(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisEncVel(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisEncAcc(short cardNum,short axis,out double pValue,short count,out uint pClock);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisError(short cardNum,short axis,out double pValue,short count,out uint pClock);

        [DllImport("gts.dll")]
        public static extern short GT_SetLongVar(short cardNum,short index,int value);
        [DllImport("gts.dll")]
        public static extern short GT_GetLongVar(short cardNum,short index,out int pValue);
        [DllImport("gts.dll")]
        public static extern short GT_SetDoubleVar(short cardNum,short index,double pValue);
        [DllImport("gts.dll")]
        public static extern short GT_GetDoubleVar(short cardNum,short index, out double pValue);

        [DllImport("gts.dll")]
        public static extern short GT_SetControlFilter(short cardNum,short control,short index);
        [DllImport("gts.dll")]
        public static extern short GT_GetControlFilter(short cardNum,short control,out short pIndex);

        [DllImport("gts.dll")]
        public static extern short GT_SetPid(short cardNum,short control,short index,ref TPid pPid);
        [DllImport("gts.dll")]
        public static extern short GT_GetPid(short cardNum,short control,short index,out TPid pPid);

        [DllImport("gts.dll")]
        public static extern short GT_SetKvffFilter(short cardNum,short control,short index,short kvffFilterExp,double accMax);
        [DllImport("gts.dll")]
        public static extern short GT_GetKvffFilter(short cardNum,short control, short index, out short pKvffFilterExp, out double pAccMax);

        [DllImport("gts.dll")]
        public static extern short GT_Update(short cardNum,int mask);
        [DllImport("gts.dll")]
        public static extern short GT_SetPos(short cardNum,short profile,int pos);
        [DllImport("gts.dll")]
        public static extern short GT_GetPos(short cardNum,short profile,out int pPos);
        [DllImport("gts.dll")]
        public static extern short GT_SetVel(short cardNum,short profile,double vel);
        [DllImport("gts.dll")]
        public static extern short GT_GetVel(short cardNum,short profile,out double pVel);

        [DllImport("gts.dll")]
        public static extern short GT_PrfTrap(short cardNum,short profile);
        [DllImport("gts.dll")]
        public static extern short GT_SetTrapPrm(short cardNum,short profile,ref TTrapPrm pPrm);
        [DllImport("gts.dll")]
        public static extern short GT_GetTrapPrm(short cardNum,short profile,out TTrapPrm pPrm);

        [DllImport("gts.dll")]
        public static extern short GT_PrfJog(short cardNum,short profile);
        [DllImport("gts.dll")]
        public static extern short GT_SetJogPrm(short cardNum,short profile,ref TJogPrm pPrm);
        [DllImport("gts.dll")]
        public static extern short GT_GetJogPrm(short cardNum,short profile,out TJogPrm pPrm);

        [DllImport("gts.dll")]
        public static extern short GT_PrfPt(short cardNum,short profile,short mode);
        [DllImport("gts.dll")]
        public static extern short GT_SetPtLoop(short cardNum,short profile,int loop);
        [DllImport("gts.dll")]
        public static extern short GT_GetPtLoop(short cardNum,short profile,out int pLoop);
        [DllImport("gts.dll")]
        public static extern short GT_PtSpace(short cardNum,short profile,out short pSpace,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_PtData(short cardNum,short profile,double pos,int time,short type,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_PtDataWN(short cardNum,short profile,double pos,int time,short type,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_PtClear(short cardNum,short profile,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_PtStart(short cardNum,int mask,int option);
        [DllImport("gts.dll")]
        public static extern short GT_SetPtMemory(short cardNum,short profile,short memory);
        [DllImport("gts.dll")]
        public static extern short GT_GetPtMemory(short cardNum,short profile,out short pMemory);
        [DllImport("gts.dll")]
        public static extern short GT_PtGetSegNum(short cardNum,short profile, out int pSegNum);

        [DllImport("gts.dll")]
        public static extern short GT_PrfGear(short cardNum,short profile,short dir);
        [DllImport("gts.dll")]
        public static extern short GT_SetGearMaster(short cardNum,short profile,short masterIndex,short masterType,short masterItem);
        [DllImport("gts.dll")]
        public static extern short GT_GetGearMaster(short cardNum,short profile,out short pMasterIndex,out short pMasterType,out short pMasterItem);
        [DllImport("gts.dll")]
        public static extern short GT_SetGearRatio(short cardNum,short profile,int masterEven,int slaveEven,int masterSlope);
        [DllImport("gts.dll")]
        public static extern short GT_GetGearRatio(short cardNum,short profile,out int pMasterEven,out int pSlaveEven,out int pMasterSlope);
        [DllImport("gts.dll")]
        public static extern short GT_GearStart(short cardNum,int mask);
        [DllImport("gts.dll")]
        public static extern short GT_SetGearEvent(short cardNum,short profile,short gearEvent,int startPara0,int startPara1);
        [DllImport("gts.dll")]
        public static extern short GT_GetGearEvent(short cardNum,short profile, out short pEvent,out int pStartPara0, out int pStartPara1);

        [DllImport("gts.dll")]
        public static extern short GT_PrfFollow(short cardNum,short profile,short dir);
        [DllImport("gts.dll")]
        public static extern short GT_SetFollowMaster(short cardNum,short profile,short masterIndex,short masterType,short masterItem);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowMaster(short cardNum,short profile,out short pMasterIndex,out short pMasterType,out short pMasterItem);
        [DllImport("gts.dll")]
        public static extern short GT_SetFollowLoop(short cardNum,short profile,int loop);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowLoop(short cardNum,short profile,out int pLoop);
        [DllImport("gts.dll")]
        public static extern short GT_SetFollowEvent(short cardNum,short profile,short followEvent,short masterDir,int pos);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowEvent(short cardNum,short profile,out short pFollowEvent,out short pMasterDir,out int pPos);
        [DllImport("gts.dll")]
        public static extern short GT_FollowSpace(short cardNum,short profile,out short pSpace,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_FollowData(short cardNum,short profile,int masterSegment,double slaveSegment,short type,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_FollowClear(short cardNum,short profile,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_FollowStart(short cardNum,int mask,int option);
        [DllImport("gts.dll")]
        public static extern short GT_FollowSwitch(short cardNum,int mask);
        [DllImport("gts.dll")]
        public static extern short GT_SetFollowMemory(short cardNum,short profile,short memory);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowMemory(short cardNum,short profile,out short memory);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowStatus(short cardNum,short profile, out short pFifoNum, out short pSwitchStatus);
        [DllImport("gts.dll")]
        public static extern short GT_SetFollowPhasing(short cardNum,short profile, short profilePhasing);
        [DllImport("gts.dll")]
        public static extern short GT_GetFollowPhasing(short cardNum,short profile, out short pProfilePhasing);

        [DllImport("gts.dll")]
        public static extern short GT_Compile(short cardNum,string pFileName, out TCompileInfo pWrongInfo);
        [DllImport("gts.dll")]
        public static extern short GT_Download(short cardNum,string pFileName);

        [DllImport("gts.dll")]
        public static extern short GT_GetFunId(short cardNum,string pFunName,out short pFunId);
        [DllImport("gts.dll")]
        public static extern short GT_Bind(short cardNum,short thread,short funId, short page);

        [DllImport("gts.dll")]
        public static extern short GT_RunThread(short cardNum,short thread);
        [DllImport("gts.dll")]
        public static extern short GT_StopThread(short cardNum,short thread);
        [DllImport("gts.dll")]
        public static extern short GT_PauseThread(short cardNum,short thread);

        [DllImport("gts.dll")]
        public static extern short GT_GetThreadSts(short cardNum,short thread,out TThreadSts pThreadSts);

        [DllImport("gts.dll")]
        public static extern short GT_GetVarId(short cardNum,string pFunName,string pVarName,out TVarInfo pVarInfo);
        [DllImport("gts.dll")]
        public static extern short GT_SetVarValue(short cardNum,short page,ref TVarInfo pVarInfo,ref double pValue,short count);
        [DllImport("gts.dll")]
        public static extern short GT_GetVarValue(short cardNum,short page,ref TVarInfo pVarInfo,out double pValue,short count);

        [DllImport("gts.dll")]
        public static extern short GT_SetCrdPrm(short cardNum,short crd, ref TCrdPrm pCrdPrm);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdPrm(short cardNum,short crd,out TCrdPrm pCrdPrm);
        [DllImport("gts.dll")]
        public static extern short GT_CrdSpace(short cardNum,short crd,out int pSpace,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_CrdData(short cardNum,short crd,System.IntPtr pCrdData,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_CrdDataCircle(short cardNum,short crd, ref TCrdData pCrdData, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXY(short cardNum,short crd, int x, int y, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZ(short cardNum,short crd, int x, int y, int z, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZA(short cardNum,short crd, int x, int y, int z, int a, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYG0(short cardNum,short crd, int x, int y, double synVel, double synAcc, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZG0(short cardNum,short crd, int x, int y, int z, double synVel, double synAcc, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAG0(short cardNum,short crd, int x, int y, int z, int a, double synVel, double synAcc, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYR(short cardNum,short crd, int x, int y, double radius, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYC(short cardNum,short crd, int x, int y, double xCenter, double yCenter, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZR(short cardNum,short crd, int y, int z, double radius, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZC(short cardNum,short crd, int y, int z, double yCenter, double zCenter, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXR(short cardNum,short crd, int z, int x, double radius, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXC(short cardNum,short crd, int z, int x, double zCenter, double xCenter, short circleDir, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYZ(short cardNum,short crd,int x,int y,int z,double interX,double interY,double interZ,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYOverride2(short cardNum,short crd,int x,int y,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZOverride2(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAOverride2(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYG0Override2(short cardNum,short crd,int x,int y,double synVel,double synAcc,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZG0Override2(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAG0Override2(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYROverride2(short cardNum,short crd,int x,int y,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYCOverride2(short cardNum,short crd,int x,int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZROverride2(short cardNum,short crd,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZCOverride2(short cardNum,short crd,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXROverride2(short cardNum,short crd,int z,int x,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXCOverride2(short cardNum,short crd,int z,int x,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYRZ(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYCZ(short cardNum,short crd,int x,int y,int z,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZRX(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZCX(short cardNum,short crd,int x,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXRY(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXCY(short cardNum,short crd,int x,int y,int z,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYRZOverride2(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYCZOverride2(short cardNum,short crd,int x,int y,int z,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZRXOverride2(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZCXOverride2(short cardNum,short crd,int x,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXRYOverride2(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXCYOverride2(short cardNum,short crd,int x,int y,int z,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYWN(short cardNum,short crd,int x,int y,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZWN(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAWN(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYG0WN(short cardNum,short crd,int x,int y,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZG0WN(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAG0WN(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYRWN(short cardNum,short crd,int x,int y,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYCWN(short cardNum,short crd,int x,int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZRWN(short cardNum,short crd,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZCWN(short cardNum,short crd,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXRWN(short cardNum,short crd,int z,int x,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXCWN(short cardNum,short crd,int z,int x,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYZWN(short cardNum,short crd,int x,int y,int z,double interX,double interY,double interZ,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYOverride2WN(short cardNum,short crd,int x,int y,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZOverride2WN(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAOverride2WN(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYG0Override2WN(short cardNum,short crd,int x,int y,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZG0Override2WN(short cardNum,short crd,int x,int y,int z,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_LnXYZAG0Override2WN(short cardNum,short crd,int x,int y,int z,int a,double synVel,double synAcc,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYROverride2WN(short cardNum,short crd,int x,int y,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcXYCOverride2WN(short cardNum,short crd,int x,int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZROverride2WN(short cardNum,short crd,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcYZCOverride2WN(short cardNum,short crd,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXROverride2WN(short cardNum,short crd,int z,int x,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_ArcZXCOverride2WN(short cardNum,short crd,int z,int x,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYRZWN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYCZWN(short cardNum,short crd,int x,int y,int z,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZRXWN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZCXWN(short cardNum,short crd,int x,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXRYWN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXCYWN(short cardNum,short crd,int x,int y,int z,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYRZOverride2WN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixXYCZOverride2WN(short cardNum,short crd,int x,int y,int z,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZRXOverride2WN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixYZCXOverride2WN(short cardNum,short crd,int x,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXRYOverride2WN(short cardNum,short crd,int x,int y,int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_HelixZXCYOverride2WN(short cardNum,short crd,int x,int y,int z,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufIO(short cardNum,short crd, ushort doType, ushort doMask, ushort doValue, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufEnableDoBitPulse(short cardNum,short crd,short doType,short doIndex,ushort highLevelTime,ushort lowLevelTime,int pulseNum,short firstLevel,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufDisableDoBitPulse(short cardNum,short crd,short doType,short doIndex,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufDelay(short cardNum,short crd, ushort delayTime, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufComparePulse(short cardNum,short crd,short level,short outputType,short time,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufDA(short cardNum,short crd, short chn, short daValue, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufLmtsOn(short cardNum,short crd, short axis, short limitType, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufLmtsOff(short cardNum,short crd, short axis, short limitType, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufSetStopIo(short cardNum,short crd, short axis, short stopType, short inputType, short inputIndex, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufMove(short cardNum,short crd, short moveAxis, int pos, double vel, double acc, short modal, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufGear(short cardNum,short crd, short gearAxis, int pos, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufGearPercent(short cardNum,short crd,short gearAxis,int pos,short accPercent,short decPercent,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufStopMotion(short cardNum,short crd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufSetVarValue(short cardNum,short crd,short pageId,out TVarInfo pVarInfo,double value,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufJumpNextSeg(short cardNum,short crd,short axis,short limitType,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufSynchPrfPos(short cardNum,short crd,short encoder,short profile,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufVirtualToActual(short cardNum,short crd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufSetLongVar(short cardNum,short crd,short index,int value,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufSetDoubleVar(short cardNum,short crd,short index,double value,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_CrdStart(short cardNum,short mask,short option);
        [DllImport("gts.dll")]
        public static extern short GT_CrdStartStep(short cardNum,short mask, short option);
        [DllImport("gts.dll")]
        public static extern short GT_CrdStepMode(short cardNum,short mask, short option);
        [DllImport("gts.dll")]
        public static extern short GT_SetOverride(short cardNum,short crd,double synVelRatio);
        [DllImport("gts.dll")]
        public static extern short GT_SetOverride2(short cardNum,short crd, double synVelRatio);
        [DllImport("gts.dll")]
        public static extern short GT_InitLookAhead(short cardNum,short crd,short fifo,double T,double accMax,short n,ref TCrdData pLookAheadBuf);
        [DllImport("gts.dll")]
        public static extern short GT_GetLookAheadSpace(short cardNum,short crd,out int pSpace,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_GetLookAheadSegCount(short cardNum,short crd,out int pSegCount,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_CrdClear(short cardNum,short crd,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_CrdStatus(short cardNum,short crd,out short pRun,out int pSegment,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_SetUserSegNum(short cardNum,short crd,int segNum,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_GetUserSegNum(short cardNum,short crd,out int pSegment,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_GetUserSegNumWN(short cardNum,short crd,out int pSegment,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_GetRemainderSegNum(short cardNum,short crd,out int pSegment,short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_SetCrdStopDec(short cardNum,short crd,double decSmoothStop,double decAbruptStop);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdStopDec(short cardNum,short crd,out double pDecSmoothStop,out double pDecAbruptStop);
        [DllImport("gts.dll")]
        public static extern short GT_SetCrdLmtStopMode(short cardNum,short crd,short lmtStopMode);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdLmtStopMode(short cardNum,short crd,out short pLmtStopMode);
        [DllImport("gts.dll")]
        public static extern short GT_GetUserTargetVel(short cardNum,short crd,out double pTargetVel);
        [DllImport("gts.dll")]
        public static extern short GT_GetSegTargetPos(short cardNum,short crd,out int pTargetPos);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdPos(short cardNum,short crd,out double pPos);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdVel(short cardNum,short crd,out double pSynVel);
        [DllImport("gts.dll")]
        public static extern short GT_SetCrdSingleMaxVel(short cardNum,short crd,ref double pMaxVel);
        [DllImport("gts.dll")]
        public static extern short GT_GetCrdSingleMaxVel(short cardNum,short crd,out double pMaxVel);
        [DllImport("gts.dll")]
        public static extern short GT_GetCmdCount(short cardNum,short crd, out short pResult, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserOn(short cardNum,short crd,short fifo,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserOff(short cardNum,short crd,short fifo,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserPrfCmd(short cardNum,short crd,double laserPower,short fifo,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserFollowRatio(short cardNum,short crd,double ratio,double minPower,double maxPower,short fifo,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserFollowMode(short cardNum,short crd,short source ,short fifo,short channel,double startPower );
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserFollowOff(short cardNum,short crd,short fifo,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_BufLaserFollowSpline(short cardNum,short crd,short tableId,double minPower,double maxPower,short fifo,short channel);

        [DllImport("gts.dll")]
        public static extern short GT_PrfPvt(short cardNum,short profile);
        [DllImport("gts.dll")]
        public static extern short GT_SetPvtLoop(short cardNum,short profile,int loop);
        [DllImport("gts.dll")]
        public static extern short GT_GetPvtLoop(short cardNum,short profile,out int pLoopCount,out int pLoop);
        [DllImport("gts.dll")]
        public static extern short GT_PvtStatus(short cardNum,short profile,out short pTableId,out double pTime,short count);
        [DllImport("gts.dll")]
        public static extern short GT_PvtStart(short cardNum,int mask);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTableSelect(short cardNum,short profile,short tableId);

        [DllImport("gts.dll")]
        public static extern short GT_PvtTable(short cardNum,short tableId,int count,ref double pTime,ref double pPos,ref double pVel);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTableEx(short cardNum,short tableId,int count,ref double pTime,ref double pPos,ref double pVelBegin,ref double pVelEnd);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTableComplete(short cardNum,short tableId,int count,ref double pTime,ref double pPos,ref double pA,ref double pB,ref double pC,double velBegin,double velEnd);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTablePercent(short cardNum,short tableId,int count,ref double pTime,ref double pPos,ref double pPercent,double velBegin);
        [DllImport("gts.dll")]
        public static extern short GT_PvtPercentCalculate(short cardNum,int n,ref double pTime,ref double pPos,ref double pPercent,double velBegin,ref double pVel);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTableContinuous(short cardNum,short tableId,int count,ref double pPos,ref double pVel,ref double pPercent,ref double pVelMax,ref double pAcc,ref double pDec,double timeBegin);
        [DllImport("gts.dll")]
        public static extern short GT_PvtContinuousCalculate(short cardNum,int n,ref double pPos,ref double pVel,ref double pPercent,ref double pVelMax,ref double pAcc,ref double pDec,ref double pTime);
        [DllImport("gts.dll")]
        public static extern short GT_PvtTableMovePercent(short cardNum,short tableId, long distance, double vm, double acc, double pa1, double pa2, double dec, double pd1, double pd2, out double pVel, out double pAcc, out double pDec, out double pTime);

        [DllImport("gts.dll")]
        public static extern short GT_HomeInit(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_Home(short cardNum,short axis,int pos,double vel,double acc,int offset);
        [DllImport("gts.dll")]
        public static extern short GT_Index(short cardNum,short axis,int pos,int offset);
        [DllImport("gts.dll")]
        public static extern short GT_HomeStop(short cardNum,short axis,int pos,double vel,double acc);
        [DllImport("gts.dll")]
        public static extern short GT_HomeSts(short cardNum,short axis,out ushort pStatus);

        [DllImport("gts.dll")]
        public static extern short GT_HandwheelInit(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_SetHandwheelStopDec(short cardNum,short slave,double decSmoothStop,double decAbruptStop);
        [DllImport("gts.dll")]
        public static extern short GT_StartHandwheel(short cardNum,short slave,short master,short masterEven,short slaveEven,short intervalTime,double acc,double dec,double vel,short stopWaitTime);
        [DllImport("gts.dll")]
        public static extern short GT_EndHandwheel(short cardNum,short slave);

        [DllImport("gts.dll")]
        public static extern short GT_SetTrigger(short cardNum,short i,ref TTrigger pTrigger);
        [DllImport("gts.dll")]
        public static extern short GT_GetTrigger(short cardNum,short i,out TTrigger pTrigger);
        [DllImport("gts.dll")]
        public static extern short GT_GetTriggerStatus(short cardNum,short i,out TTriggerStatus pTriggerStatus,short count);
        [DllImport("gts.dll")]
        public static extern short GT_ClearTriggerStatus(short cardNum,short i);

        [DllImport("gts.dll")]
        public static extern short GT_SetComparePort(short cardNum,short channel,short hsio0,short hsio1);

        [DllImport("gts.dll")]
        public static extern short GT_ComparePulse(short cardNum,short level,short outputType,short time);
        [DllImport("gts.dll")]
        public static extern short GT_CompareStop(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_CompareStatus(short cardNum,out short pStatus,out int pCount);
        [DllImport("gts.dll")]
        public static extern short GT_CompareData(short cardNum,short encoder,short source,short pulseType,short startLevel,short time,ref int pBuf1,short count1,ref int pBuf2,short count2);
        [DllImport("gts.dll")]
        public static extern short GT_CompareLinear(short cardNum,short encoder,short channel,int startPos,int repeatTimes,int interval,short time,short source);
        [DllImport("gts.dll")]
        public static extern short GT_CompareContinuePulseMode(short cardNum,short mode, short count, short standTime);

        [DllImport("gts.dll")]
        public static extern short GT_SetEncResponseCheck(short cardNum,short control, short dacThreshold, double minEncVel, int time);
        [DllImport("gts.dll")]
        public static extern short GT_GetEncResponseCheck(short cardNum,short control, out short pDacThreshold, out double pMinEncVel, out int pTime);
        [DllImport("gts.dll")]
        public static extern short GT_EnableEncResponseCheck(short cardNum,short control);
        [DllImport("gts.dll")]
        public static extern short GT_DisableEncResponseCheck(short cardNum,short control);

        [DllImport("gts.dll")]
        public static extern short GT_2DCompareMode(short cardNum,short chn, short mode);
        [DllImport("gts.dll")]
        public static extern short GT_2DComparePulse(short cardNum,short chn, short level, short outputType, short time);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareStop(short cardNum,short chn);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareClear(short cardNum,short chn);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareStatus(short cardNum,short chn, out short pStatus, out int pCount, out short pFifo, out short pFifoCount, out short pBufCount);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareSetPrm(short cardNum,short chn, ref T2DComparePrm pPrm);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareData(short cardNum,short chn, short count, ref T2DCompareData pBuf, short fifo);
        [DllImport("gts.dll")]
        public static extern short GT_2DCompareStart(short cardNum,short chn);

        [DllImport("gts.dll")]
        public static extern short GT_SetAxisMode(short cardNum,short axis, short mode);
        [DllImport("gts.dll")]
        public static extern short GT_GetAxisMode(short cardNum,short axis, out short pMode);
        [DllImport("gts.dll")]
        public static extern short GT_SetHSIOOpt(short cardNum,ushort value, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_GetHSIOOpt(short cardNum,out ushort pValue, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_LaserPowerMode(short cardNum,short laserPowerMode, double maxValue, double minValue, short channel, short delaymode);
        [DllImport("gts.dll")]
        public static extern short GT_LaserPrfCmd(short cardNum,double outputCmd, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_LaserOutFrq(short cardNum,double outFrq, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetPulseWidth(short cardNum,uint width, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetWaitPulse(short cardNum,ushort mode, double waitPulseFrq, double waitPulseDuty, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetPreVltg(short cardNum,ushort mode, double voltageValue, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetLevelDelay(short cardNum,ushort offDelay, ushort onDelay, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_EnaFPK(short cardNum,ushort time1, ushort time2, ushort laserOffDelay, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_DisFPK(short cardNum,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetLaserDisMode(short cardNum,short mode, short source, ref int pPos, ref double pScale, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetLaserDisRatio(short cardNum,ref double pRatio, double minPower, double maxPower, short channel);
        [DllImport("gts.dll")]
        public static extern short GT_SetWaitPulseEx(short cardNum,ushort mode, double waitPulseFrq, double waitPulseDuty);
        [DllImport("gts.dll")]
        public static extern short GT_SetLaserMode(short cardNum,short mode);
        [DllImport("gts.dll")]
        public static extern short GT_SetLaserFollowSpline(short cardNum,short tableId,long n,ref double pX,ref double pY,double beginValue,double endValue,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_GetLaserFollowSpline(short cardNum,short tableId,long n,out double pX,out double pY,out double pA,out double pB,out double pC,out long pCount,short channel);
        
        //ExtMdl
        [DllImport("gts.dll")]
        public static extern short GT_OpenExtMdl(short cardNum,string pDllName);
        [DllImport("gts.dll")]
        public static extern short GT_CloseExtMdl(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_SwitchtoCardNoExtMdl(short cardNum,short card);
        [DllImport("gts.dll")]
        public static extern short GT_ResetExtMdl(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_LoadExtConfig(short cardNum,string pFileName);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtIoValue(short cardNum,short mdl,ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtIoValue(short cardNum,short mdl,out ushort pValue);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtIoBit(short cardNum,short mdl,short index,ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtIoBit(short cardNum,short mdl,short index,out ushort pValue);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtAdValue(short cardNum,short mdl,short chn,out ushort pValue);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtAdVoltage(short cardNum,short mdl,short chn,out double pValue);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtDaValue(short cardNum,short mdl,short chn,ushort value);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtDaVoltage(short cardNum,short mdl,short chn,double value);
        [DllImport("gts.dll")]
        public static extern short GT_GetStsExtMdl(short cardNum,short mdl,short chn,out ushort pStatus);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtDoValue(short cardNum,short mdl,out ushort pValue);
        [DllImport("gts.dll")]
        public static extern short GT_GetExtMdlMode(short cardNum,out short pMode);
        [DllImport("gts.dll")]
        public static extern short GT_SetExtMdlMode(short cardNum,short mode);
        [DllImport("gts.dll")]
        public static extern short GT_UploadConfig(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_DownloadConfig(short cardNum);
        [DllImport("gts.dll")]
        public static extern short GT_GetUuid(short cardNum,out char pCode,short count);

        //2D Compensate
        public struct TCompensate2DTable
        {      
            public short count1;
            public short count2;
            public int posBegin1;
            public int posBegin2;   
            public int step1;
            public int step2;
        } 
        public struct TCompensate2D 
        {
	        public short enable;                                 
	        public short tableIndex;                              
	        public short axisType1;	
            public short axisType2;              
	        public short axisIndex1;
            public short axisIndex2;              
        } 
        [DllImport("gts.dll")]
        public static extern short GT_SetCompensate2DTable(short cardNum,short tableIndex,ref TCompensate2DTable pTable,ref int pData,short externComp);
        [DllImport("gts.dll")]
        public static extern short GT_GetCompensate2DTable(short cardNum,short tableIndex,out TCompensate2DTable pTable,out short pExternComp);
        [DllImport("gts.dll")]
        public static extern short GT_SetCompensate2D(short cardNum,short axis, ref TCompensate2D pComp2d);
        [DllImport("gts.dll")]
        public static extern short GT_GetCompensate2D(short cardNum,short axis, out TCompensate2D pComp2d);
        [DllImport("gts.dll")]
        public static extern short GT_GetCompensate2DValue(short cardNum,short axis, out double pValue);

        //Smart Home
        public const short HOME_STAGE_IDLE=0;
        public const short HOME_STAGE_START=1;
        public const short HOME_STAGE_SEARCH_LIMIT=10;
        public const short HOME_STAGE_SEARCH_LIMIT_STOP=11;
        public const short HOME_STAGE_SEARCH_LIMIT_ESCAPE = 13;
        public const short HOME_STAGE_SEARCH_LIMIT_RETURN=15;
        public const short HOME_STAGE_SEARCH_LIMIT_RETURN_STOP=16;
        public const short HOME_STAGE_SEARCH_HOME=20;
        public const short HOME_STAGE_SEARCH_HOME_RETURN=25;
        public const short HOME_STAGE_SEARCH_INDEX=30;
        public const short HOME_STAGE_SEARCH_GPI=40;
        public const short HOME_STAGE_SEARCH_GPI_RETURN=45;
        public const short HOME_STAGE_GO_HOME=80;
        public const short HOME_STAGE_END=100;
        public const short HOME_ERROR_NONE=0;
        public const short HOME_ERROR_NOT_TRAP_MODE=1;
        public const short HOME_ERROR_DISABLE=2;
        public const short HOME_ERROR_ALARM=3;
        public const short HOME_ERROR_STOP=4;
        public const short HOME_ERROR_STAGE=5;
        public const short HOME_ERROR_HOME_MODE=6;
        public const short HOME_ERROR_SET_CAPTURE_HOME=7;
        public const short HOME_ERROR_NO_HOME=8;
        public const short HOME_ERROR_SET_CAPTURE_INDEX=9;
        public const short HOME_ERROR_NO_INDEX=10;
        public const short HOME_MODE_LIMIT=10;
        public const short HOME_MODE_LIMIT_HOME=11;
        public const short HOME_MODE_LIMIT_INDEX=12;
        public const short HOME_MODE_LIMIT_HOME_INDEX=13;
        public const short HOME_MODE_HOME=20;
        public const short HOME_MODE_HOME_INDEX=22;
        public const short HOME_MODE_INDEX = 30;
        public struct THomePrm
        {
	        public short mode;						
	        public short moveDir;					
	        public short indexDir;					
	        public short edge;					
	        public short triggerIndex;			
			public short pad1_1;
	        public short pad1_2;
            public short pad1_3;
	        public double velHigh;				
	        public double velLow;				
	        public double acc;					
	        public double dec;
	        public short smoothTime;
			public short pad2_1;
		    public short pad2_2;
            public short pad2_3;
	        public int homeOffset;				
	        public int searchHomeDistance;	
	        public int searchIndexDistance;	
	        public int escapeStep;			
            public int pad3_1;
            public int pad3_2;
            public int pad3_3;
        } 
        public struct THomeStatus
        {
	        public short run;
	        public short stage;
            public short error;
            public short pad1;
	        public int capturePos;
	        public int targetPos;
        }
        [DllImport("gts.dll")]
        public static extern short GT_GoHome(short cardNum,short axis, ref THomePrm pHomePrm);
        [DllImport("gts.dll")]
        public static extern short GT_GetHomePrm(short cardNum,short axis, out THomePrm pHomePrm);
        [DllImport("gts.dll")]
        public static extern short GT_GetHomeStatus(short cardNum,short axis, out THomeStatus pHomeStatus);

        //Extern Control
        public struct TControlConfigEx
        {
	        public short refType;
            public short refIndex;
            public short feedbackType;
            public short feedbackIndex;
            public int errorLimit;
            public short feedbackSmooth;
            public short controlSmooth;	
        }
        [DllImport("gts.dll")]
        public static extern short GT_SetControlConfigEx(short cardNum,short control, ref TControlConfigEx pControl);
        [DllImport("gts.dll")]
        public static extern short GT_GetControlConfigEx(short cardNum,short control, out TControlConfigEx pControl);

        //Adc filter
        public struct TAdcConfig
        {
	        public short active;
            public short reverse;
            public double a;
            public double b;
            public short filterMode;	
        }
        [DllImport("gts.dll")]
        public static extern short GT_SetAdcConfig(short cardNum,short adc, ref TAdcConfig pAdc);
        [DllImport("gts.dll")]
        public static extern short GT_GetAdcConfig(short cardNum,short adc, out TAdcConfig pAdc);
        [DllImport("gts.dll")]
        public static extern short GT_SetAdcFilterPrm(short cardNum,short adc, double k);
        [DllImport("gts.dll")]
        public static extern short GT_GetAdcFilterPrm(short cardNum,short adc, out double pk);

        //Superimposed
        [DllImport("gts.dll")]
        public static extern short GT_SetControlSuperimposed(short cardNum,short control, short superimposedType, short superimposedIndex);
        [DllImport("gts.dll")]
        public static extern short GT_GetControlSuperimposed(short cardNum,short control, out short pSuperimposedType, out short pSuperimposedIndex);

        ////////////////////
        [DllImport("gts.dll")]
        public static extern short GT_ZeroLaserOnTime(short cardNum,short channel);
        [DllImport("gts.dll")]
        public static extern short GT_GetLaserOnTime(short cardNum,short channel,out uint pTime);
    }
}
