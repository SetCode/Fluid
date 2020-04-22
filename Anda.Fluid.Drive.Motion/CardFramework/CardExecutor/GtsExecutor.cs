using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gts;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public class GtsExecutor : ICardExecutable
    {
        public CardExecutorType ExecutorType { get { return CardExecutorType.GTS; } }

        public short Open(short cardId)
        {
            return mc.GT_Open(cardId, 0, 1);
        }
        public short Close(short cardId)
        {
            return mc.GT_Close(cardId);
        }
        public short Reset(short cardId)
        {
            return mc.GT_Reset(cardId);
        }
        public short LoadConfig(short cardId, string configFile)
        {
            return mc.GT_LoadConfig(cardId, configFile);
        }

        public short AxisOn(short cardId, short axisId)
        {
            return mc.GT_AxisOn(cardId, axisId);
        }
        public short AxisOff(short cardId, short axisId)
        {
            return mc.GT_AxisOff(cardId, axisId);
        }

        public short ClrAxisSts(short cardId, short axisId)
        {
            return mc.GT_ClrSts(cardId, axisId, 1);
        }
        public short ClrAxesSts(short cardId, short axisId, short count)
        {
            return mc.GT_ClrSts(cardId, axisId, count);
        }
        /// <summary>
        /// get aixs status, output status by bit
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="axisId"></param>
        /// <param name="sts">0:Alarm|1:Error|2:PLmt|3:NLmt|4:ServoOn|5:Moving|6:SmoothStop|7:AbruptStop</param>
        /// <returns></returns>
        public short GetAxisSts(short cardId, short axisId, out int sts)
        {
            sts = 0;
            int pSts;
            uint pClock;
            short rtn = mc.GT_GetSts(cardId, axisId, out pSts, 1, out pClock);
            if (rtn != 0)
            {
                return rtn;
            }
            sts = this.ConvertAxisSts(pSts);
            return 0;
        }
        public short GetAxesSts(short cardId, short axisId, short count, out int[] sts)
        {
            sts = new int[count];
            int[] pSts = new int[count];
            uint pClock;
            short rtn = mc.GT_GetSts(cardId, axisId, out pSts[0], count, out pClock);
            if (rtn != 0)
            {
                return rtn;
            }

            for (int i = 0; i < count; i++)
            {
                sts[i] = this.ConvertAxisSts(pSts[i]);
            }

            return 0;
        }
        private int ConvertAxisSts(int pSts)
        {
            int sts = 0;

            //Alarm
            if ((pSts >> 1 & 1) == 1)
            {
                sts |= 1;
            }
            //Error
            if ((pSts >> 4 & 1) == 1)
            {
                sts |= 1 << 1;
            }
            //PLmt
            if ((pSts >> 5 & 1) == 1)
            {
                sts |= 1 << 2;
            }
            //NLmt
            if ((pSts >> 6 & 1) == 1)
            {
                sts |= 1 << 3;
            }
            //SmoothStop
            if ((pSts >> 7 & 1) == 1)
            {
                sts |= 1 << 6;
            }
            //AbruptStop
            if ((pSts >> 8 & 1) == 1)
            {
                sts |= 1 << 7;
            }
            //ServoOn
            if ((pSts >> 9 & 1) == 1)
            {
                sts |= 1 << 4;
            }
            //Moving
            if ((pSts >> 10 & 1) == 1)
            {
                sts |= 1 << 5;
            }

            return sts;
        }

        public short ZeroPos(short cardId, short axisId)
        {
            return mc.GT_ZeroPos(cardId, axisId, 1);
        }
        public short GetEncPos(short cardId, short axisId, out int pos)
        {
            pos = 0;
            double value;
            uint pClock;
            short rtn = mc.GT_GetEncPos(cardId, axisId, out value, 1, out pClock);
            if (rtn != 0) return rtn;
            pos = (int)value;
            return 0;
        }
        public short GetEncPos(short cardId, short axisId, short count, out int[] pos)
        {
            pos = new int[count];
            double[] pValue = new double[count];
            uint pClock;
            short rtn = mc.GT_GetEncPos(cardId, axisId, out pValue[0], count, out pClock);
            if (rtn != 0) return rtn;
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = (int)pValue[i];
            }
            return 0;
        }

        public short GetPrfPos(short cardId, short axisId, out int pos)
        {
            pos = 0;
            double value;
            uint pClock;
            short rtn = mc.GT_GetPrfPos(cardId, axisId, out value, 1, out pClock);
            if (rtn != 0) return rtn;
            pos = (int)value;
            return 0;
        }

        public short GetPrfPos(short cardId, short axisId, short count, out int[] pos)
        {
            pos = new int[count];
            double[] pValue = new double[count];
            uint pClock;
            short rtn = mc.GT_GetPrfPos(cardId, axisId, out pValue[0], count, out pClock);
            if (rtn != 0) return rtn;
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = (int)pValue[i];
            }
            return 0;
        }

        public short SetStopDec(short cardId, short axisId, double smoothDec, double abruptDec)
        {
            return mc.GT_SetStopDec(cardId, axisId, smoothDec, abruptDec);
        }
        public short SetAxisLimit(short cardId, short axisId, int positivePositon, int negativePostin)
        {
            return mc.GT_SetSoftLimit(cardId, axisId, positivePositon, negativePostin);
        }
        public short MoveSmoothStop(short cardId, params short[] axisId)
        {
            int mask = 0;
            foreach (var item in axisId)
            {
                mask |= 1 << (item - 1);
            }
            short rtn = mc.GT_Stop(cardId, mask, 0);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MoveAbruptStop(short cardId, params short[] axisId)
        {
            int mask = 0;
            int option = 0;
            foreach (var item in axisId)
            {
                mask |= 1 << (item - 1);
                option |= 1 << (item - 1); 
            }
            short rtn = mc.GT_Stop(cardId, mask, option);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MoveHome(short cardId, short axisId, HomeMode mode, short moveDir, short indexDir, double velH, double velL, double acc, int homeOffset, int searchHomeDis, int searchIndexDis, int escapeStep)
        {
            short t = mc.HOME_MODE_HOME;
            switch (mode)
            {
                case HomeMode.Limit:
                    t = mc.HOME_MODE_LIMIT;
                    break;
                case HomeMode.Limit_Home:
                    t = mc.HOME_MODE_LIMIT_HOME;
                    break;
                case HomeMode.Limit_Index:
                    t = mc.HOME_MODE_LIMIT_INDEX;
                    break;
                case HomeMode.Limit_Home_Index:
                    t = mc.HOME_MODE_LIMIT_HOME_INDEX;
                    break;
                case HomeMode.Home:
                    t = mc.HOME_MODE_HOME;
                    break;
                case HomeMode.Home_Index:
                    t = mc.HOME_MODE_HOME_INDEX;
                    break;
                case HomeMode.Index:
                    t = mc.HOME_MODE_INDEX;
                    break;
                default:
                    break;
            }

            mc.THomePrm homePrm;
            short rtn = mc.GT_GetHomePrm(cardId, axisId, out homePrm);
            if (rtn != 0) return rtn;
            homePrm.mode = t;
            homePrm.moveDir = moveDir;
            homePrm.indexDir = indexDir;
            homePrm.edge = 0;
            homePrm.triggerIndex = -1;
            homePrm.velHigh = velH;
            homePrm.velLow = velL;
            homePrm.acc = acc;
            homePrm.dec = acc;
            homePrm.smoothTime = 0;
            homePrm.homeOffset = homeOffset;
            homePrm.searchHomeDistance = searchHomeDis;
            homePrm.searchIndexDistance = searchIndexDis;
            homePrm.escapeStep = escapeStep;
            //clear capture status
            rtn = mc.GT_ClearCaptureStatus(cardId, axisId);
            if (rtn != 0) return rtn;
            //set trap move
            rtn = mc.GT_PrfTrap(cardId, axisId);
            if (rtn != 0) return rtn;
            //move home
            rtn = mc.GT_GoHome(cardId, axisId, ref homePrm);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short GetMoveHomeStatus(short cardId, short axisId, out short run, out short error)
        {
            run = 0;
            error = 0;
            mc.THomeStatus homeStatus = new mc.THomeStatus();
            short rtn = mc.GT_GetHomeStatus(cardId, axisId, out homeStatus);
            if (rtn != 0) return rtn;
            run = homeStatus.run;
            error = homeStatus.error;
            return 0;
        }
        public short SetMovePos(short cardId, short axisId, int pos, double vel, double acc, double dec)
        {
            //set prfPos zero
            //short rtn = 0;
            //rtn = mc.GT_SetPrfPos(cardId, axisId, 0);
            //if (rtn != 0) return rtn;
            //set trap move
            short rtn = mc.GT_PrfTrap(cardId, axisId);
            if (rtn != 0) return rtn;
            //set trap param
            mc.TTrapPrm trapPrm = new mc.TTrapPrm();
            trapPrm.acc = acc;
            trapPrm.dec = dec;
            trapPrm.smoothTime = 0;
            rtn = mc.GT_SetTrapPrm(cardId, axisId, ref trapPrm);
            if (rtn != 0) return rtn;
            //set pos
            rtn = mc.GT_SetPos(cardId, axisId, pos);
            if (rtn != 0) return rtn;
            //set vel
            rtn = mc.GT_SetVel(cardId, axisId, vel);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MovePos(short cardId, params short[] axisId)
        {
            int mask = 0;
            foreach (var item in axisId)
            {
                mask |= 1 << (item - 1);
            }
            short rtn = mc.GT_Update(cardId, mask);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MovePos(short cardId, short axisId)
        {
            //start move
            int mask = 1 << (axisId - 1);
            short rtn = mc.GT_Update(cardId, mask);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MoveJog(short cardId, short axisId, double vel, double acc)
        {
            //set jog move
            short rtn = mc.GT_PrfJog(cardId, axisId);
            if (rtn != 0) return rtn;
            mc.TJogPrm jogPrm = new mc.TJogPrm();
            jogPrm.acc = acc;
            jogPrm.dec = acc;
            //set jog param
            rtn = mc.GT_SetJogPrm(cardId, axisId, ref jogPrm);
            if (rtn != 0) return rtn;
            //set vel
            rtn = mc.GT_SetVel(cardId, axisId, vel);
            if (rtn != 0) return rtn;
            //start move
            rtn = mc.GT_Update(cardId, 1 << (axisId - 1));
            if (rtn != 0) return rtn;
            return 0;
        }

        int IIOExecutable.DiData { get; set; }
        int IIOExecutable.DoData { get; set; }

        public short GetDi(short cardId, short mdlId, out int data)
        {
            return mc.GT_GetDi(cardId, mc.MC_GPI, out data);
        }
        public short SetDo(short cardId, short mdlId, int data)
        {
            return mc.GT_SetDo(cardId, mc.MC_GPO, data);
        }
        public short GetDo(short cardId, short mdlId, out int data)
        {
            return mc.GT_GetDo(cardId, mc.MC_GPO, out data);
        }
        public short SetDoBit(short cardId, short mdlId, short doId, short value)
        {
            int v = value ^ 1;
            return mc.GT_SetDoBit(cardId, mc.MC_GPO, doId, (short)v);
        }
        public int GetDiBit(int data, short diId)
        {
            return (data >> (diId - 1)) & 1;
        }
        public int GetDoBit(int data, short doId)
        {
            return ((data >> (doId - 1)) & 1) ^ 1;
        }

        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, int orgX, int orgY)
        {
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            crdPrm.dimension = 2;
            crdPrm.synVelMax = velMax;
            crdPrm.synAccMax = accMax;
            crdPrm.evenTime = evenTime;
            if (axisX == 5 && axisY == 6)
            {
                crdPrm.profile5 = 1;
                crdPrm.profile6 = 2;
                crdPrm.originPos5 = orgX;
                crdPrm.originPos6 = orgY;
            }
            else if (axisX == 3 && axisY == 4)  //小8字机台AB轴插补
            {
                crdPrm.profile3 = 1;
                crdPrm.profile4 = 2;
                crdPrm.originPos3 = orgX;
                crdPrm.originPos4 = orgY;
            }
            else
            {
                crdPrm.profile1 = axisX;
                crdPrm.profile2 = axisY;
                crdPrm.originPos1 = orgX;
                crdPrm.originPos2 = orgY;
            }
            crdPrm.setOriginFlag = 1;
            short rtn = mc.GT_SetCrdPrm(cardId, csId, ref crdPrm);
            if (rtn != 0) return rtn;
            return 0;
        }

        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, int orgX, int orgY, int orgA, int cardType)
        {
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            crdPrm.dimension = 3;
            crdPrm.synVelMax = velMax;
            crdPrm.synAccMax = accMax;
            crdPrm.evenTime = evenTime;
            
            if (cardType == 0)//单8轴
            {
                crdPrm.profile1 = 1;
                crdPrm.profile2 = 2;
                crdPrm.profile4 = 3;
                crdPrm.setOriginFlag = 1;
                crdPrm.originPos1 = orgX;
                crdPrm.originPos2 = orgY;
                crdPrm.originPos4 = orgA;
            }

            short rtn = mc.GT_SetCrdPrm(cardId, csId, ref crdPrm);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, short axisB, int orgX, int orgY, int orgA, int orgB,int cardType)
        {
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            crdPrm.dimension = 4;
            crdPrm.synVelMax = velMax;
            crdPrm.synAccMax = accMax;
            crdPrm.evenTime = evenTime;
            if (cardType == 0)//单8轴
            {
                crdPrm.profile1 = 1;
                crdPrm.profile2 = 2;
                crdPrm.profile5 = 3;
                crdPrm.profile6 = 4;
                crdPrm.setOriginFlag = 1;
                crdPrm.originPos1 = orgX;
                crdPrm.originPos2 = orgY;
                crdPrm.originPos5 = orgA;
                crdPrm.originPos6 = orgB;
            }
            else//双4轴
            {
                if (axisX == 5 && axisY == 6)
                {
                    crdPrm.profile1 = 5;
                    crdPrm.profile2 = 6;
                    crdPrm.profile3 = 7;
                    crdPrm.profile4 = 8;
                    crdPrm.setOriginFlag = 1;
                    crdPrm.originPos1 = orgX;
                    crdPrm.originPos2 = orgY;
                    crdPrm.originPos5 = orgA;
                    crdPrm.originPos6 = orgB;
                }
                else
                {
                    crdPrm.profile1 = 1;
                    crdPrm.profile2 = 2;
                    crdPrm.profile3 = 3;
                    crdPrm.profile4 = 4;
                    crdPrm.setOriginFlag = 1;
                    crdPrm.originPos1 = orgX;
                    crdPrm.originPos2 = orgY;
                    crdPrm.originPos3 = orgA;
                    crdPrm.originPos4 = orgB;
                }
            }
            short rtn = mc.GT_SetCrdPrm(cardId, csId, ref crdPrm);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short ClrCrdBuf(short cardId, short csId)
        {
            return mc.GT_CrdClear(cardId, csId, 0);
        }
        public short AddCrdLnXY(short cardId, short csId, int posX, int posY, double vel, double acc, double velEnd)
        {
            return mc.GT_LnXY(cardId, csId, posX, posY, vel, acc, velEnd, 0);
        }
        public short AddCrdLnXYA(short cardId, short csId, int posX, int posY, int posA, double vel, double acc, double velEnd)
        {
            return mc.GT_LnXYZ(cardId, csId, posX, posY, posA, vel, acc, velEnd, 0);
        }
        public short AddCrdLnXYAB(short cardId, short csId, int posX, int posY, int posA, int posB, double vel, double acc, double velEnd)
        {
            return mc.GT_LnXYZA(cardId, csId, posX, posY, posA, posB, vel, acc, velEnd, 0);
        }
        public short AddCrdBufIO(short cardId, short csId, int mask, int value)
        {
            return mc.GT_BufIO(cardId, csId, (ushort)mc.MC_GPO, (ushort)mask, (ushort)value, 0);
        }
        public short AddCrdDelay(short cardId, short csId, ushort delay)
        {
            return mc.GT_BufDelay(cardId, csId, delay, 0);
        }
        public short GetCrdSpace(short cardId, short csId, out int space)
        {
            return mc.GT_CrdSpace(cardId, csId, out space, 0);
        }
        public short GetCrdStatus(short cardId, short csId, out short run, out int segment)
        {
            return mc.GT_CrdStatus(cardId, csId, out run, out segment, 0);
        }
        public short AddCrdArcXYC(short cardId, short csId, int posX, int posY, double centerX, double centerY, short clockwise, double vel, double acc, double velEnd)
        {
            return mc.GT_ArcXYC(cardId, csId, posX, posY, centerX, centerY, clockwise, vel, acc, velEnd, 0);
        }
        public short AddCrdArcXYR(short cardId, short csId, int posX, int posY, double r, short clockwise, double vel, double acc, double velEnd)
        {
            return mc.GT_ArcXYR(cardId, csId, posX, posY, r, clockwise, vel, acc, velEnd, 0);
        }
        public short AddCrdGear(short cardId, short csId, short gearAxisId, int gearPos)
        {
            return mc.GT_BufGear(cardId, csId, gearAxisId, gearPos, 0);
        }
        public short AddBufMove(short cardId, short csId, short axisId, int pos, double vel, double acc, int mode)
        {
            return mc.GT_BufMove(cardId, csId, axisId, pos, vel, acc, (short)mode, 0);
        }
        public short StartCrd(short cardId, short csId)
        {
            return mc.GT_CrdStart(cardId, csId, 0);
        }
        public short StopCrd(short cardId, short csId)
        {
            return this.MoveSmoothStop(cardId, (short)(csId + 8));
        }

        public short Cmp2dClear(short cardId, short chn)
        {
            return mc.GT_2DCompareClear(cardId, chn);
        }
        /// <summary>
        /// 设置二维比较输出数据
        /// </summary>
        /// <param name="cardId">卡号</param>
        /// <param name="chn">0,1</param>
        /// <param name="count">比较点数</param>
        /// <param name="x">数据x</param>
        /// <param name="y">数据y</param>
        /// <param name="fifo">0,1</param>
        /// <returns></returns>
        public short Cmp2dData(short cardId, short chn, short count, int[] x, int[] y, short fifo)
        {
            if (x == null || y == null || x.Length == 0 || y.Length == 0 || x.Length != y.Length
                || x.Length < count || y.Length < count)
            {
                return -10;
            }

            mc.T2DCompareData[] data = new mc.T2DCompareData[2048];
            for (int i = 0; i < count; i++)
            {
                data[i].px = x[i];
                data[i].py = y[i];
            }
            return mc.GT_2DCompareData(cardId, chn, count, ref data[0], fifo);
        }
        /// <summary>
        /// 默认使用2d比较的二维模式
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="chn"></param>
        /// <returns></returns>
        public short Cmp2dMode(short cardId, short chn)
        {
            return mc.GT_2DCompareMode(cardId, chn, mc.COMPARE2D_MODE_2D);
        }
        /// <summary>
        /// 设置2d比较为一维模式
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="chn"></param>
        /// <param name="compareMode">2d比较的模式，0：一维模式，1：二维模式</param>
        /// <returns></returns>
        public short Cmp2dMode1d(short cardId, short chn)
        {
            return mc.GT_2DCompareMode(cardId, chn, 0);
        }
        /// <summary>
        /// 二维比较单次输出
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="chn">0,1</param>
        /// <param name="level">level=0，位置比较输出引脚电平复（恢到初始化后的状态）
        /// level=1，位置比较输出引脚电平取反（与初始化后的状态相）</param>
        /// <param name="outType"> 输出类型 0：脉冲方式，1：电平方式</param>
        /// <param name="time">脉冲方式时的间，单位微秒</param>
        /// <returns></returns>
        public short Cmp2dPulse(short cardId, short chn, short level, short outType, short time)
        {
            return mc.GT_2DComparePulse(cardId, chn, level, outType, time);
        }
        /// <summary>
        /// 设置二维比较输出参数
        /// </summary>
        /// <param name="cardId">卡号</param>
        /// <param name="chn">0,1</param>
        /// <param name="axisxId">x轴</param>
        /// <param name="axisyId">y轴</param>
        /// <param name="src">比较源，0：规划，1：编码器</param>
        /// <param name="outType">输出方式，0：脉冲，1：电平</param>
        /// <param name="startLevel">起始电平方式0：位置比较输出引脚电平复位，1：位置比较输出引脚电平取反</param>
        /// <param name="pulseWidth">脉冲方式脉冲时间，单位微秒</param>
        /// <param name="maxerr">比较范围最大误差</param>
        /// <param name="threshold">最优算法阈值</param>
        /// <returns></returns>
        public short Cmp2dSetPrm(short cardId, short chn, short axisxId, short axisyId, short src, short outType, short startLevel, short pulseWidth, short maxerr, short threshold)
        {
            mc.T2DComparePrm p = new mc.T2DComparePrm()
            {
                encx = axisxId,
                ency = axisyId,
                source = src,
                outputType = outType,
                startLevel = startLevel,
                time = pulseWidth,
                maxerr = maxerr,
                threshold = threshold
            };
            return mc.GT_2DCompareSetPrm(cardId, chn, ref p);
        }
        public short Cmp2dStart(short cardId, short chn)
        {
            return mc.GT_2DCompareStart(cardId, chn);
        }
        /// <summary>
        /// 读取二维比较输出状态
        /// </summary>
        /// <param name="cardId">卡号</param>
        /// <param name="chn">0,1</param>
        /// <param name="status">0：正在进行二维比较，1：二维比较输出完成</param>
        /// <param name="count">位置比较已输出次数</param>
        /// <param name="fifo">当前空闲fifo</param>
        /// <param name="fifoCount">当前空闲fifo剩余空间</param>
        /// <param name="bufCount">FPGA的fifo剩余空间</param>
        /// <returns></returns>
        public short Cmp2dStatus(short cardId, short chn, out short status, out int count, out short fifo, out short fifoCount, out short bufCount)
        {
            return mc.GT_2DCompareStatus(cardId, chn, out status, out count, out fifo, out fifoCount, out bufCount);
        }
        public short Cmp2dStop(short cardId, short chn)
        {
            return mc.GT_2DCompareStop(cardId, chn);
        }

        public short CmpContiPulseMode(short cardId, short mode, short count, short standTime)
        {
            return mc.GT_CompareContinuePulseMode(cardId, mode, count, standTime);
        }
        public short CmpPulse(short cardId, short level, short outputType, short time)
        {
            return mc.GT_ComparePulse(cardId, level, outputType, time);
        }
        public short CmpStatus(short cardId, out short status, out int count)
        {
            return mc.GT_CompareStatus(cardId, out status, out count);
        }
        public short CmpStop(short cardId)
        {
            return mc.GT_CompareStop(cardId);
        }
        public short CmpLinear(short cardId, short axisId, short channel, int startPos, int repeatTimes, int interval, short time, short source)
        {
            return mc.GT_CompareLinear(cardId, axisId, channel, startPos, repeatTimes, interval, time, source);
        }
        public short CmpData(short cardId, short axisId, short source, short pulseType, short startLevel, short time, ref int[] buf1, short count1, ref int[] buf2, short count2)
        {
            return mc.GT_CompareData(cardId, axisId, source, pulseType, startLevel, time, ref buf1[0], count1, ref buf2[0], count2);
        }


        public short EncOn(short cardId, short axisId)
        {
            return mc.GT_EncOn(cardId, axisId);
        }
        public short EncOff(short cardId, short axisId)
        {
            return mc.GT_EncOff(cardId, axisId);
        }

        int ICardExecutable.DiArriveData { get; set; }
        public short GetDiArrive(short cardId, out int data)
        {
            return mc.GT_GetDi(cardId, mc.MC_ARRIVE, out data);
        }
        public int GetDiArriveBit(int data, short axisId)
        {
            return (data >> (axisId - 1)) & 1;
        }

        public short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n)
        {
            mc.TCrdData[] trdData = new mc.TCrdData[n];
            return mc.GT_InitLookAhead(cardNum, crd, fifo, T, accMax, n, ref trdData[0]);
        }

        public short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n, mc.TCrdData[] trdData)
        {
            return mc.GT_InitLookAhead(cardNum, crd, fifo, T, accMax, n, ref trdData[0]);
        }

        public short GetLookAheadSpace(short cardNum, short crd, out int pSpace, short fifo)
        {
            return mc.GT_GetLookAheadSpace(cardNum, crd, out pSpace, fifo);
        }

        public short GetLookAheadSegCount(short cardNum, short crd, out int pSegCount, short fifo)
        {
            return mc.GT_GetLookAheadSegCount(cardNum, crd, out pSegCount, fifo);
        }

        public short GT_CrdData(short cardNum, short crd, System.IntPtr pCrdData, short fifo)
        {
            return mc.GT_CrdData(cardNum, crd, pCrdData, fifo);
        }

        public short SaveConfig(short cardId, string configFile)
        {
            throw new NotImplementedException();
        }

        public short ADCmp2Data(short cardId, short chn, short axisxId, short axisyId, int[] x, int[] y, short outType, ushort startLevel, ushort pulseWidthH, ushort pulseWidthL, short threshold)
        {
            throw new NotImplementedException();
        }
    }
}
