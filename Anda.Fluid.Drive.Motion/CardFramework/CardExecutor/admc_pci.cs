using System.Runtime.InteropServices;


namespace admc_pci
{
    public class mc
    {
		public const short VERSION_DLL = 0;
		public const short VERSION_DSP = 1;
		public const short VERSION_FPGA	= 2;
                                                                        
		public const short RES_NONE = -1;
		
		public const short RES_LIMIT_POSITIVE = 0;
		public const short RES_LIMIT_NEGATIVE = 1;
		public const short RES_ALARM = 2;
		public const short RES_HOME = 3;
		public const short RES_GPI = 4;
		public const short RES_ARRIVE = 5;
		public const short RES_MPG = 6;         
		
		public const short RES_ENABLE = 10;
		public const short RES_CLEAR = 11;
		public const short RES_GPO = 12;
		
		public const short RES_DAC = 20;
		public const short RES_STEP = 21;
		public const short RES_PULSE = 22;
		public const short RES_ENCODER = 23;
		public const short RES_ADC = 24;
		
		public const short RES_AXIS = 30;
		public const short RES_PROFILE = 31;
		public const short RES_CONTROL = 32;
		public const short RES_CRD = 33;
		public const short RES_COMPARE = 34;
		
		public const short STEP_DIR	= 0;
		public const short STEP_PULSE = 1;
		
		public const short CRD_STOP_TYPE_SMOOTH = 0;
		public const short CRD_STOP_TYPE_ABRUPT = 1;
		
		public const short HOME_STAGE_START	= 100;
		public const short HOME_STAGE_SEARCH_LIMIT = 101;
		public const short HOME_STAGE_SEARCH_LIMIT_ESCAPE = 102;
		public const short HOME_STAGE_SEARCH_HOME = 103;
		public const short HOME_STAGE_SEARCH_INDEX = 104;
		public const short HOME_STAGE_GO_HOME = 105;
		public const short HOME_STAGE_END = 106;

		public const short HOME_ERROR_NONE = 0;
		public const short HOME_ERROR_NO_LIMIT = -1;
		public const short HOME_ERROR_AXIS_MAP = -2;
		public const short HOME_ERROR_LIMIT_MODE = -3;
		public const short HOME_ERROR_INDEX_DIR	= -4;
		public const short HOME_ERROR_HOME_MODE	= -5;
		public const short HOME_ERROR_NO_HOME = -6;
        public const short HOME_ERROR_NO_INDEX = -7;
        public const short HOME_ERROR_NO_STAGE = -8;
        public const short HOME_ERROR_FOLLOW = -9;
        
		
        public const short HOME_MODE_LIMIT = 10;
        public const short HOME_MODE_LIMIT_HOME = 11;
        public const short HOME_MODE_LIMIT_INDEX = 12;
        public const short HOME_MODE_LIMIT_HOME_INDEX = 13;
		public const short HOME_MODE_HOME = 14;
		public const short HOME_MODE_HOME_INDEX = 15;
        public const short HOME_MODE_INDEX = 16;
		
        public const short COMPARE_MODE_1D = 100;
        public const short COMPARE_MODE_2D = 101;
		
        public const short COMPARE_OUT_PULSE = 0;
        public const short COMPARE_OUT_LEVEL = 1;
		
        public const short COMPARE_ERROR_NONE = 0;
        public const short COMPARE_ERROR_FPGA_FIFO_EMPTY = -1;
        public const short COMPARE_ERROR_FPGA_FIFO_OVERFLOW	= -2;
        public const short COMPARE_ERROR_DSP_FIFO_EMPTY	= -3;
        public const short COMPARE_ERROR_DSP_FIFO_OVERFLOW = -4;
        public const short COMPARE_ERROR_NO_CONFIG = -5;    
		
        public const short AUTO_TRIGGER_ERROR_NONE = 0;
        public const short AUTO_TRIGGER_ERROR_CRD_NOCFG = -1;
        public const short AUTO_TRIGGER_ERROR_FPGA_FIFO_EMPTY = -2;
        public const short AUTO_TRIGGER_ERROR_FPGA_FIFO_FULL = -3;
                                                                                                

		public struct TVersion 
		{
			public short year;		//版本年份
			public short month;		//版本月份
			public short day;		//版本日期
			public short version;	//版本号
			public short chip;		//芯片代码
			public short reserve1;
			public short reserve2;
		}

		public enum EPrfMode
		{
			Trap = 0,
			Jog = 1,
			Pt = 2,
			Gear = 3,
			Follow = 4,
			Crd = 5,
			Pvt = 6,
			Home = 7
		}

		public struct TTrapPrm
		{
			public double acc;		//加速度:pulse/ms^2
			public double dec;		//减速度:pulse/ms^2
			public double velStart;//起跳速度:pulse/ms
			public short  smoothTime;//平滑时间:ms,取值范围[0,50]
		}    

		public struct TJogPrm
		{
			public double acc;		//加速度:pulse/ms^2
			public double dec;		//减速度:pulse/ms^2
			public double smooth;	//平滑系数，取值范围[0,1）
		}

		public struct THomePrm
		{
			public short mode;					// 回零模式:
												//			HOME_MODE_LIMIT：表示限位信号回零模式
												//			HOME_MODE_LIMIT_HOME：表示限位+原点信号回零模式
												//			HOME_MODE_LIMIT_INDEX：表示限位+Index信号回零模式
												//			HOME_MODE_LIMIT_HOME_INDEX：表示限位+原点+Index信号回零模式
												//			HOME_MODE_HOME：表示原点信号回零模式
												//			HOME_MODE_HOME_INDEX：表示原点+Index信号回零模式
												//			HOME_MODE_INDEX	：表示Index信号回零模式
			public short moveDir;				// 设置启动搜索时的运动方向；1表示正向，0表示反向
			public double velHigh;				// 高速搜索速度：pulse/ms
			public double velLow;				// 低速搜索速度：pulse/ms
			public double acc;					// 加速度:pulse/ms^2
			public double dec;					// 减速度:pulse/ms^2
			public short smoothTime;			// 平滑时间:ms,取值范围[0,50]
			public int homeOffset;				// 原点偏移
			public int searchHomeDistance;		// 原点信号搜索距离
			public int searchIndexDistance;	// Index信号搜索距离
			public int searchLimitDistance;	// 限位信号搜索距离
			public int escapeStep;				// 限位信号回零模式下，反向脱离步长。其他模式无效。
		}

		public struct THomeStatus
		{
			public short run;
			public short stage;					
			public short error;
			public int capturePos;
			public int targetPos;
		}


		//插补坐标系建立结构
		public struct TCrdPrm
		{
            //public short[]  axisMap;              //axisMap[0],[1],[2],[3],[4]:分别代表X,Y,Z,A,C轴号。若值为RES_NONE则表示当前轴没有映射
            public short axis1;
            public short axis2;
            public short axis3;
            public short axis4;
            public short axis5;
            //public double[] axisVelMax;           //axisVelMax[0],[1],[2],[3],[4]:分别代表X,Y,Z,A,C轴的最大速度。
            public double axisVelMax1;
            public double axisVelMax2;
            public double axisVelMax3;
            public double axisVelMax4;
            public double axisVelMax5;
            public double synVelMax;               // 坐标系内最大合成速度
			public double synAccMax;               // 坐标系内最大合成加速度
			public double decSmoothStop;           // 坐标系内平滑停止减速度
			public double decAbruptStop;           // 坐标系内紧急停止减速度
			public short  setOriginFlag;           // 设置原点坐标值标志,0:默认当前规划位置为原点位置;1:用户指定原点位置
            //public int[]   originPos;			   //originPos[0],[1],[2],[3],[4]:分别代表X,Y,Z,A,C轴的原点位置
            public int originPos1;
            public int originPos2;
            public int originPos3;
            public int originPos4;
            public int originPos5;
        }

		//Buf操作
		public struct TCrdBufIO
		{
			public short type;    
			public ushort doType;
			public ushort doMask;
			public ushort doValue;
			public short reserve0; 
		}
		public struct TCrdBufDA
		{
			public short type; 
			public short channel;                                    
			public short daValue;                                
			public short reserve0;
			public short reserve1;
		}
		public struct TCrdBufDelay
		{
			public short type;         
			public ushort delayTime; 
			public short reserve0;                                  
			public short reserve1;                       
			public short reserve2;                       
		}
		public struct TCrdBufLmts
		{
			public short type;          
			public short isLmtsOn;   
			public short axis;  
			public short limitType;            
			public short reserve0; 
		}
		public struct TCrdBufStopIO
		{
			public short type;     
			public short axis;                                   
			public short stopType;                         
			public short inputType;                               
			public short inputIndex;                                            
		}

		public struct TCrdBufMotion
		{
			public short type;  
			public short subType; 
			public short axis;                                   
			public short model;                         
			public short reserve0;                                                                         
		}

		public struct TCrdBufTrigger
		{
			public short type;                                  
			public ushort triCount; 
			public ushort preOffset;
			public short reserve0; 
			public short reserve1; 
		}
		
		[StructLayout(LayoutKind.Explicit, Size = 10)]
		public struct BufData
		{
			[FieldOffset(0)]
			public TCrdBufIO      ioData; 
			[FieldOffset(0)]            
			public TCrdBufDA      daData;
			[FieldOffset(0)]
			public TCrdBufDelay   delayData;
			[FieldOffset(0)]
			public TCrdBufLmts    lmstData;
			[FieldOffset(0)]
			public TCrdBufStopIO  stopIoData;
			[FieldOffset(0)]
			public TCrdBufMotion  motionData;
			[FieldOffset(0)]
			public TCrdBufTrigger triggerData; 
		}

		public struct TCrdData
		{
			public short[]  active;	
			public short    motionType; 
			public short    circlePlat;
			public double[] pos;
			public double   radius;
			public short    circleDir;
			public double[] center;  
			public double[] midPoint;
			public double   vel;
			public double   acc;  
			public double[] startVector;
			public double[] endVector;
			public double   velEnd;	
			public double   velEndAdjust;
			public double   length;
			public double[] crdTrans;
			public int     height; 
			public double   pitch;
			public double   transL;
			public double[] startPos;
			public BufData  bufData;
		}

		public struct TPosCompareMode
		{
			public short mode;						//位置比较模式：COMPARE_MODE_1D表示一维模式.
													//				  COMPARE_MODE_2D表示二维模式.
			public short compareSource;				//位置比较源：RES_PROFILE表示规划器
													//			   RES_ENCODER表示编码器
			public short sourceX;					//X轴索引号
			public short sourceY;					//Y轴索引号
			public short outputMode;				//信号输出方式：COMPARE_OUT_PULSE表示脉冲输出方式。
													//				 COMPARE_OUT_LEVEL表示电平输出方式。
			public uint pulseWidthH;		//脉冲信号输出方式下，高电平脉宽，us为单位
			public uint pulseWidthL;		//脉冲信号输出方式下，低电平脉宽，us为单位
			public ushort startLevel;		//输出信号的起始电平（包括脉冲模式和电平模式）。
			public short threshold;					//二维比较模式下触发阈值。
		}

		public struct TPosCompareStatus
		{
			public short run;
			public short mode;
			public short error;
			public ushort space;
			public ushort triggerCount;
			public ushort segmentId;
			public short reserve1;
			public short reserve2;
		}

		public struct TPosCompareData
		{
			public int posX;						//X轴触发数据(一维比较下仅X值有效)
			public int posY;						//Y轴触发数据
			public ushort segmentID;		//数据段号
			public ushort triValue;			//当前段数据触发后，输出的脉冲数量或电平值。
		} 

		public struct T2DAutoTrigPrm
		{
			public short crd;						//绑定的插补坐标系索引
			public short sourceX;					//X轴索引号,跟插补坐标系中X映射轴号一致。
			public short sourceY;					//Y轴索引号,跟插补坐标系中Y映射轴号一致。
			public short outputMode;				//信号输出方式：COMPARE_OUT_PULSE表示脉冲输出方式。
													//				 COMPARE_OUT_LEVEL表示电平输出方式。
			public short compareSource;				//位置比较源：RES_PROFILE表示规划器
													//			   RES_ENCODER表示编码器
			public ushort startLevel;		//输出信号的起始电平（包括脉冲模式和电平模式）。
			public ushort pulseWidthH;		//脉冲信号输出方式下，高电平脉宽，us为单位
			public ushort pulseWidthL;		//脉冲信号输出方式下，低电平脉宽，us为单位
			public short reverseX;					//X轴方向是否取反,TRUE表示取反，FALSE表示不取反。
			public short reverseY;					//Y轴方向是否取反，TRUE表示取反，FALSE表示不取反。
		}

		public struct T2DAutoTrigStatus
		{
			public short active;
			public short run;
			public short error;
			public ushort fifoSpace;
			public ushort triggerCountX;
			public ushort triggerCountY;
			public ushort segmentId;
			public short reserve1;
			public short reserve2;
		}

        public struct TProfileConfig
        {
            public short active;
            public double decSmoothStop;
            public double decAbruptStop;
        }


        [DllImport("admc_pci.dll")]
        public static extern short API_OpenBoard(short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CloseBoard(short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ResetBoard(short boardNum);

        [DllImport("admc_pci.dll")]
        public static extern short API_SaveConfig(string file, short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_LoadConfig(string file, short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_GetVersion(short type,out TVersion pVersion,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ClrSts(short axis,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetSts(short axis,out int pSts,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_AxisOn(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_AxisOff(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Stop(int mask,int option,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ZeroPos(short axis,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_AlarmOn(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_AlarmOff(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LmtsOn(short axis,short limitType,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LmtsOff(short axis,short limitType,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetPrfMode(short axis,out short pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisPrfPos(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisPrfVel(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisPrfAcc(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisEncPos(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisEncVel(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAxisEncAcc(short axis,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_TrapMode(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetTrapPrm(short axis, ref TTrapPrm pPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetTrapPrm(short axis,out TTrapPrm pPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_JogMode(short axis,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetJogPrm(short axis, ref TJogPrm pPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetJogPrm(short axis,out TJogPrm pPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Update(int mask,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetTrapPos(short axis,int pos,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetTrapPos(short axis,out int pPos,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetVel(short axis,double vel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetVel(short axis,out double pVel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetDi(short diType,out int pValue,short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_GetDo(short doType, out int pValue, short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_SetDo(short doType,int value,short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_SetDoBit(short doType, short doIndex, short value, short boardNum);
        [DllImport("admc_pci.dll")]
        public static extern short API_SetDoBitReverse(short doType,short doIndex,short value,short reverseTime,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetSoftLimit(short axis,int positive,int negative,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetSoftLimit(short axis,out int pPositive,out int pNegative,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetEncSns(int sense,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetEncSns(out int pSense,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetDacValue(short dac,short pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetDacValue(short dac,out short pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAdcVoltage5V(short adc,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAdcVoltage10V(short adc,out double pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetAdcValue(short adc,out short pValue,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GoHome(short axis, ref THomePrm pHomePrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetHomePrm(short axis,out THomePrm pHomePrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetHomeStatus(short axis,out THomeStatus pHomeStatus,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetCrdPrm(short crd, ref TCrdPrm pCrdPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetCrdPrm(short crd,out TCrdPrm pCrdPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CrdClear(short crd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CrdStart(short mask,short option,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CrdStop(short mask,short option,short stopType,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CrdSinglePause(short crd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetCrdPos(short crd,out double pPos,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetCrdStatus(short crd,out short pRun,out short pCrdComplete,out short pCrdFifo0Pause,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SendAllCrdData(short crd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetCrdSpace(short crd,out int pSpace,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetCrdSynVel(short crd,out double pSynVel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ReturnFifo0PausePos(short crd,double synVel,double synAcc,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_InitLookAhead(short crd,short fifo,double accMax, ref TCrdData pLookAheadBuf,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_CloseLookAhead(short crd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXY(short crd,int x, int y,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXYZ(short crd, int x, int y, int z,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXYZA(short crd, int x, int y, int z, int a,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXYZAB(short crd, int x, int y, int z, int a, int b,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYR(short crd, int x, int y,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYC(short crd, int x, int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcYZR(short crd, int y, int z,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcYZC(short crd, int y, int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcZXR(short crd, int z, int x,double radius,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcZXC(short crd, int z, int x,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYZ(short crd, int x, int y, int z,double interX,double interY,double interZ,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalXYR(short crd, int x, int y, int z,double radius,short circleDir,double pitch,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalYZR(short crd, int y, int z, int x,double radius,short circleDir,double pitch,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalZXR(short crd, int z, int x, int y,double radius,short circleDir,double pitch,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalXYC(short crd, int x, int y, int z,double xCenter,short yCenter,short circleDir, double pitch ,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalYZC(short crd, int x, int y, int z,double yCenter,short zCenter,short circleDir, double pitch ,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalZXC(short crd, int x, int y, int z,double zCenter,short xCenter,short circleDir, double pitch ,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_HelicalXYZ(short crd,double x,double y,double z,double interX,double interY,double interZ,int height,double pitch,double synVel,double synAcc,double velEnd,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_JumpXYZ(short crd, int x, int y, int z, int h1, int h2,double zVel,double zAcc,double synVel,double synAcc,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_BufIO(short crd,ushort doType,ushort doMask,ushort doValue,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_BufDelay(short crd,ushort delayTime,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_BufMove(short crd,short moveAxis, int pos,double vel,double acc,short model,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_EnableBezierTransition(short crd,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_DisableBezierTransition(short crd,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXY_Bezier_L(short crd,int x,int y,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXYZ_Bezier_L(short crd,int x,int y,int z,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYR_Bezier_L(short crd,int x,int y,double radius,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYC_Bezier_L(short crd,int x,int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcYZR_Bezier_L(short crd,int y,int z,double radius,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcYZC_Bezier_L(short crd,int y,int z,double yCenter,double zCenter,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcZXR_Bezier_L(short crd,int z,int x,double radius,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcZXC_Bezier_L(short crd,int z,int x,double zCenter,double xCenter,short circleDir,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYZ_Bezier_L(short crd,int x,int y,int z,double interX,double interY,double interZ,double synVel,double synAcc,double L,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetCrdPrmWith2DAutoTrigger(short crd, ref TCrdPrm pCrdPrm,short channel, ref T2DAutoTrigPrm p2DAutoTrigPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Set2DAutoTriggerPrm(short channel, ref T2DAutoTrigPrm p2DAutoTrigPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Get2DAutoTriggerPrm(short channel,out T2DAutoTrigPrm p2DAutoTrigPrm,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Get2DAutoTriggerStatus(short channel,out T2DAutoTrigStatus p2DAutoTrigStatus,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_Stop2DAutoTrigger(short channel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_LnXY_Bezier_Trigger(short crd,int x,int y,double synVel,double synAcc,double L,ushort triCount,ushort preOffset,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYR_Bezier_Trigger(short crd,int x,int y,double radius,short circleDir,double synVel,double synAcc,double L,ushort triCount,ushort preOffset,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_ArcXYC_Bezier_Trigger(short crd,int x,int y,double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double L,ushort triCount,ushort preOffset,short fifo,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetPosCompareMode(short channel, ref TPosCompareMode pMode,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetPosCompareMode(short channel,out TPosCompareMode pMode,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_GetPosCompareStatus(short channel,out TPosCompareStatus pSts,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_PosCompareClear(short channel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_PosCompareForceOut(short channel,short pulseType,short level,uint pulseWidthH,uint pulseWidthL,ushort pulseCount,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_SetPosCompareData(short channel,ref TPosCompareData pPosCompareData,short count,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_PosCompareStart(short channel,short boardNum);
		[DllImport("admc_pci.dll")]
        public static extern short API_PosCompareStop(short channel,short boardNum);

        [DllImport("admc_pci.dll")]
        public static extern short API_SetProfileConfig(short profile, ref TProfileConfig pProfile, short boardNum);

        [DllImport("admc_pci.dll")]
        public static extern short API_GetProfileConfig(short profile, ref TProfileConfig pProfile, short boardNum);
    }
}