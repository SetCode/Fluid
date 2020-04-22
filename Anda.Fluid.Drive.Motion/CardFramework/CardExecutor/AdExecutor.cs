using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using admc_pci;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public class AdExecutor : ICardExecutable
    {
        public CardExecutorType ExecutorType => CardExecutorType.GTS;

        public short Open(short cardId)
        {
            return mc.API_OpenBoard(cardId);
        }
        public short Close(short cardId)
        {
            return mc.API_CloseBoard(cardId);
        }
        public short Reset(short cardId)
        {
            return mc.API_ResetBoard(cardId);
        }
        public short LoadConfig(short cardId, string configFile)
        {
            return mc.API_LoadConfig(configFile, cardId);
        }
        public short SaveConfig(short cardId, string configFile)
        {
            return mc.API_SaveConfig(configFile, cardId);
        }

        public short AxisOn(short cardId, short axisId)
        {
            return mc.API_AxisOn(axisId, cardId);
        }
        public short AxisOff(short cardId, short axisId)
        {
            return mc.API_AxisOff(axisId, cardId);
        }

        public short ClrAxisSts(short cardId, short axisId)
        {
            return mc.API_ClrSts(axisId, 1, cardId);
        }
        public short ClrAxesSts(short cardId, short axisId, short count)
        {
            return mc.API_ClrSts(axisId, count, cardId);
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
            short rtn = mc.API_GetSts(axisId, out pSts, 1, cardId);
            if (rtn != 0)
            {
                return rtn;
            }
            sts = this.ConvertAxisSts((int)pSts);
            return 0;
        }
        public short GetAxesSts(short cardId, short axisId, short count, out int[] sts)
        {
            sts = new int[count];
            int[] pSts = new int[count];
            short rtn = mc.API_GetSts(axisId, out pSts[0], count, cardId);
            if (rtn != 0)
            {
                return rtn;
            }

            for (int i = 0; i < count; i++)
            {
                sts[i] = this.ConvertAxisSts((int)pSts[i]);
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
            return mc.API_ZeroPos(axisId, 1, cardId);
        }
        public short GetEncPos(short cardId, short axisId, out int pos)
        {
            pos = 0;
            double value;
            short rtn = mc.API_GetAxisEncPos(axisId, out value, 1, cardId);
            if (rtn != 0) return rtn;
            pos = (int)value;
            return 0;
        }
        public short GetEncPos(short cardId, short axisId, short count, out int[] pos)
        {
            pos = new int[count];
            double[] pValue = new double[count];
            short rtn = mc.API_GetAxisEncPos(axisId, out pValue[0], count, cardId);
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
            short rtn = mc.API_GetAxisPrfPos(axisId, out value, 1, cardId);
            if (rtn != 0) return rtn;
            pos = (int)value;
            return 0;
        }

        public short GetPrfPos(short cardId, short axisId, short count, out int[] pos)
        {
            pos = new int[count];
            double[] pValue = new double[count];
            short rtn = mc.API_GetAxisPrfPos(axisId, out pValue[0], count, cardId);
            if (rtn != 0) return rtn;
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = (int)pValue[i];
            }
            return 0;
        }

        public short SetStopDec(short cardId, short axisId, double smoothDec, double abruptDec)
        {
            mc.TProfileConfig config = new mc.TProfileConfig()
            {
                active = 1,
                decSmoothStop = smoothDec,
                decAbruptStop = abruptDec
            };
            return mc.API_SetProfileConfig(axisId, ref config, cardId);
        }
        public short SetAxisLimit(short cardId, short axisId, int positivePositon, int negativePostin)
        {
            return mc.API_SetSoftLimit(axisId, positivePositon, negativePostin, cardId);
        }
        public short MoveSmoothStop(short cardId, params short[] axisId)
        {
            int mask = 0;
            foreach (var item in axisId)
            {
                mask |= 1 << (item - 1);
            }
            short rtn = mc.API_Stop(mask, 0, cardId);
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
            short rtn = mc.API_Stop(mask, option, cardId);
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
            short rtn = mc.API_GetHomePrm(axisId, out homePrm, cardId);
            if (rtn != 0) return rtn;
            homePrm.mode = t;
            homePrm.moveDir = moveDir;
            //homePrm.indexDir = indexDir;            
            //homePrm.edge = 0;
            //homePrm.triggerIndex = -1;
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
            //rtn = mc.GT_ClearCaptureStatus(cardId, axisId);
            //if (rtn != 0) return rtn;
            //set trap move
            rtn = mc.API_TrapMode(axisId, cardId);
            if (rtn != 0) return rtn;
            //move home
            rtn = mc.API_GoHome(axisId, ref homePrm, cardId);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short GetMoveHomeStatus(short cardId, short axisId, out short run, out short error)
        {
            run = 0;
            error = 0;
            mc.THomeStatus homeStatus = new mc.THomeStatus();
            short rtn = mc.API_GetHomeStatus(axisId, out homeStatus, cardId);
            if (rtn != 0) return rtn;
            run = homeStatus.run;
            error = homeStatus.error;
            return 0;
        }
        public short SetMovePos(short cardId, short axisId, int pos, double vel, double acc, double dec)
        {
            //set trap move
            short rtn = mc.API_TrapMode(axisId, cardId);
            if (rtn != 0) return rtn;
            //set trap param
            mc.TTrapPrm trapPrm = new mc.TTrapPrm();
            trapPrm.acc = acc;
            trapPrm.dec = dec;
            trapPrm.smoothTime = 0;
            rtn = mc.API_SetTrapPrm(axisId, ref trapPrm, cardId);
            if (rtn != 0) return rtn;
            //set pos
            rtn = mc.API_SetTrapPos(axisId, pos, cardId);
            if (rtn != 0) return rtn;
            //set vel
            rtn = mc.API_SetVel(axisId, vel, cardId);
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
            short rtn = mc.API_Update(mask, cardId);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MovePos(short cardId, short axisId)
        {
            //start move
            int mask = 1 << (axisId - 1);
            short rtn = mc.API_Update(mask, cardId);
            if (rtn != 0) return rtn;
            return 0;
        }
        public short MoveJog(short cardId, short axisId, double vel, double acc)
        {
            //set jog move
            short rtn = mc.API_JogMode(axisId, cardId);
            if (rtn != 0) return rtn;
            mc.TJogPrm jogPrm = new mc.TJogPrm();
            jogPrm.acc = acc;
            jogPrm.dec = acc;
            //set jog param
            rtn = mc.API_SetJogPrm(axisId, ref jogPrm, cardId);
            if (rtn != 0) return rtn;
            //set vel
            rtn = mc.API_SetVel(axisId, vel, cardId);
            if (rtn != 0) return rtn;
            //start move
            int mask = 1 << (axisId - 1);
            rtn = mc.API_Update(mask, cardId);
            if (rtn != 0) return rtn;
            return 0;
        }

        int IIOExecutable.DiData { get; set; }
        int IIOExecutable.DoData { get; set; }

        public short GetDi(short cardId, short mdlId, out int data)
        {
            return mc.API_GetDi(mc.RES_GPI, out data, cardId);
        }
        public short SetDo(short cardId, short mdlId, int data)
        {
            return mc.API_SetDo(mc.RES_GPO, data, cardId);
        }
        public short GetDo(short cardId, short mdlId, out int data)
        {
            return mc.API_GetDo(mc.RES_GPO, out data, cardId);
        }
        public short SetDoBit(short cardId, short mdlId, short doId, short value)
        {
            return mc.API_SetDoBit(mc.RES_GPO, doId, value, cardId);
        }
        public int GetDiBit(int data, short diId)
        {
            return (data >> (diId - 1)) & 1;
        }
        public int GetDoBit(int data, short doId)
        {
            return (data >> (doId - 1)) & 1;
        }
        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, int orgX, int orgY)
        {
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            //crdPrm.dimension = 2;
            crdPrm.synVelMax = velMax;
            crdPrm.synAccMax = accMax;
            crdPrm.axis1 = axisX;
            crdPrm.axis2 = axisY;
            crdPrm.axis3 = mc.RES_NONE;
            crdPrm.axis4 = mc.RES_NONE;
            crdPrm.axis5 = mc.RES_NONE;
            crdPrm.originPos1 = orgX;
            crdPrm.originPos2 = orgY;
            crdPrm.originPos3 = 0;
            crdPrm.originPos4 = 0;
            crdPrm.originPos5 = 0;
            crdPrm.axisVelMax1 = velMax;
            crdPrm.axisVelMax2 = velMax;
            //crdPrm.axisVelMax1 = 0;
            //crdPrm.axisVelMax2 = 0;
            //crdPrm.axisVelMax1 = 0;
            crdPrm.decSmoothStop = 5;
            crdPrm.decAbruptStop = 10;
            //crdPrm.evenTime = evenTime;
            //if (axisX == 5 && axisY == 6)
            //{
            //    crdPrm.profile5 = 1;
            //    crdPrm.profile6 = 2;
            //    crdPrm.originPos5 = orgX;
            //    crdPrm.originPos6 = orgY;
            //}
            //else if (axisX == 3 && axisY == 4)  //小8字机台AB轴插补
            //{
            //    crdPrm.profile3 = 1;
            //    crdPrm.profile4 = 2;
            //    crdPrm.originPos3 = orgX;
            //    crdPrm.originPos4 = orgY;
            //}
            //else
            //{
            //    crdPrm.profile1 = axisX;
            //    crdPrm.profile2 = axisY;
            //    crdPrm.originPos1 = orgX;
            //    crdPrm.originPos2 = orgY;
            //}
            crdPrm.setOriginFlag = 1;
            short rtn = mc.API_SetCrdPrm(csId, ref crdPrm, cardId);
            if (rtn != 0) return rtn;
            return 0;
        }

        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, int orgX, int orgY, int orgA, int cardType)
        {
            return 0;
        }
        public short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, short axisB, int orgX, int orgY, int orgA, int orgB, int cardType)
        {
            //mc.TCrdPrm crdPrm = new mc.TCrdPrm();
            //crdPrm.dimension = 4;
            //crdPrm.synVelMax = velMax;
            //crdPrm.synAccMax = accMax;
            //crdPrm.evenTime = evenTime;
            //if (cardType == 0)//单8轴
            //{
            //    crdPrm.profile1 = 1;
            //    crdPrm.profile2 = 2;
            //    crdPrm.profile5 = 3;
            //    crdPrm.profile6 = 4;
            //    crdPrm.setOriginFlag = 1;
            //    crdPrm.originPos1 = orgX;
            //    crdPrm.originPos2 = orgY;
            //    crdPrm.originPos5 = orgA;
            //    crdPrm.originPos6 = orgB;
            //}
            //else//双4轴
            //{
            //    if (axisX == 5 && axisY == 6)
            //    {
            //        crdPrm.profile1 = 5;
            //        crdPrm.profile2 = 6;
            //        crdPrm.profile3 = 7;
            //        crdPrm.profile4 = 8;
            //        crdPrm.setOriginFlag = 1;
            //        crdPrm.originPos1 = orgX;
            //        crdPrm.originPos2 = orgY;
            //        crdPrm.originPos5 = orgA;
            //        crdPrm.originPos6 = orgB;
            //    }
            //    else
            //    {
            //        crdPrm.profile1 = 1;
            //        crdPrm.profile2 = 2;
            //        crdPrm.profile3 = 3;
            //        crdPrm.profile4 = 4;
            //        crdPrm.setOriginFlag = 1;
            //        crdPrm.originPos1 = orgX;
            //        crdPrm.originPos2 = orgY;
            //        crdPrm.originPos3 = orgA;
            //        crdPrm.originPos4 = orgB;
            //    }
            //}
            //short rtn = mc.GT_SetCrdPrm(cardId, csId, ref crdPrm);
            //if (rtn != 0) return rtn;
            return 0;
        }
        public short ClrCrdBuf(short cardId, short csId)
        {
            return mc.API_CrdClear(csId, 0, cardId);
        }
        public short AddCrdLnXY(short cardId, short csId, int posX, int posY, double vel, double acc, double velEnd)
        {
            return mc.API_LnXY(csId, posX, posY, vel, acc, velEnd, 0, cardId);
        }
        public short AddCrdLnXYA(short cardId, short csId, int posX, int posY, int posA, double vel, double acc, double velEnd)
        {
            return mc.API_LnXYZ(csId, posX, posY, posA, vel, acc, velEnd, 0, cardId);
        }
        public short AddCrdLnXYAB(short cardId, short csId, int posX, int posY, int posA, int posB, double vel, double acc, double velEnd)
        {
            return mc.API_LnXYZA(csId, posX, posY, posA, posB, vel, acc, velEnd, 0, cardId);
        }
        public short AddCrdBufIO(short cardId, short csId, int mask, int value)
        {
            return mc.API_BufIO(csId, (ushort)mc.RES_GPO, (ushort)mask, (ushort)value, 0, cardId);
        }
        public short AddCrdDelay(short cardId, short csId, ushort delay)
        {
            return mc.API_BufDelay(csId, delay, 0, cardId);
        }
        public short GetCrdSpace(short cardId, short csId, out int space)
        {
            return mc.API_GetCrdSpace(csId, out space, 0, cardId);
        }
        public short GetCrdStatus(short cardId, short csId, out short run, out int segment)
        {
            segment = 0;
            //short pCrdComplete, pCrdFifo0Pause;
            //return mc.API_GetCrdStatus(csId, out run, out pCrdComplete, out pCrdFifo0Pause, 0, cardId);
            int[] pSts = new int[2];
            short rtn = mc.API_GetSts(1, out pSts[0], 2, cardId);
            if ((pSts[0] >> 10 & 1) == 1 || (pSts[1] >> 10 & 1) == 1)
            {
                run = 1;
            }
            else
            {
                run = 0;
            }
            return 0;
        }
        public short AddCrdArcXYC(short cardId, short csId, int posX, int posY, double centerX, double centerY, short clockwise, double vel, double acc, double velEnd)
        {
            return mc.API_ArcXYC(csId, posX, posY, centerX, centerY, clockwise, vel, acc, velEnd, 0, cardId);
        }
        public short AddCrdArcXYR(short cardId, short csId, int posX, int posY, double r, short clockwise, double vel, double acc, double velEnd)
        {
            return mc.API_ArcXYR(csId, posX, posY, r, clockwise, vel, acc, velEnd, 0, cardId);
        }
        public short StartCrd(short cardId, short csId)
        {
            int mask = 1 << (csId - 1);
            return mc.API_CrdStart((short)mask, 0, cardId);
        }
        public short StopCrd(short cardId, short csId)
        {
            int mask = 1 << (csId - 1);
            return mc.API_CrdStop((short)mask, 0, 0, cardId);
        }

        public short Cmp2dClear(short cardId, short chn)
        {
            return mc.API_PosCompareClear(++chn, cardId);
            
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
            mc.TPosCompareData[] data = new mc.TPosCompareData[2048];
            for (int i = 0; i < count; i++)
            {
                data[i].posX = -x[i];
                data[i].posY = -y[i];
                data[i].segmentID = (ushort)i;
                data[i].triValue = 1;
            }
            return mc.API_SetPosCompareData(1,ref data[0], count, cardId);
        }
        /// <summary>
        /// 默认使用2d比较的二维模式
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="chn"></param>
        /// <returns></returns>
        public short Cmp2dMode(short cardId, short chn)
        {
            //return mc.GT_2DCompareMode(cardId, chn, mc.COMPARE2D_MODE_2D);
            return 0;
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
            //return mc.GT_2DCompareMode(cardId, chn, 0);
            return 0;
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
            return 0;
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
            chn = ++chn;
            mc.TPosCompareMode mode = new mc.TPosCompareMode()
            {
                mode = mc.COMPARE_MODE_2D,
                compareSource = mc.RES_PROFILE,
                sourceX = axisxId,
                sourceY = axisyId,
                outputMode = mc.COMPARE_OUT_PULSE,
                pulseWidthH = (ushort)pulseWidth,
                pulseWidthL = (ushort)pulseWidth,
                startLevel = (ushort)startLevel,
                threshold = threshold
            };
         
            return mc.API_SetPosCompareMode(chn, ref mode, cardId);
        }
        public short Cmp2dStart(short cardId, short chn)
        {
            chn = ++chn;
            return mc.API_PosCompareStart(chn, cardId);
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
            mc.TPosCompareStatus pSts = new mc.TPosCompareStatus();
            short rtn = mc.API_GetPosCompareStatus(++chn, out pSts, cardId);
            status = pSts.run;
            if (status == 0)
            {
                status = 1;
            }
            else if (status == 1)
            {
                status = 0;
            }
            count = pSts.triggerCount;
            fifo = 0;
            fifoCount = (short)pSts.space;
            bufCount = (short)(1024 - fifoCount);
            return rtn;
            
        }
        public short Cmp2dStop(short cardId, short chn)
        {
            return mc.API_PosCompareStop(++chn, cardId);
        }

        private short pulseCount, offTime;
        public short CmpContiPulseMode(short cardId, short mode, short count, short standTime)
        {
            //return mc.GT_CompareContinuePulseMode(cardId, mode, count, standTime);
            this.pulseCount = count;
            this.offTime = standTime;
            return 0;
        }
        public short CmpPulse(short cardId, short level, short outputType, short time)
        {
            //return mc.GT_ComparePulse(cardId, level, outputType, time);
            short rtn = 0;
            if ((level & 1) == 1)
            {
                rtn = mc.API_PosCompareForceOut(1, outputType, 0, (ushort)time, (ushort)offTime, (ushort)pulseCount, cardId);
            }
            else if((level & 2) == 1)
            {
                rtn = mc.API_PosCompareForceOut(2, outputType, 0, (ushort)time, (ushort)offTime, (ushort)pulseCount, cardId);
            }
            else if(level == 0)
            {
                rtn = mc.API_PosCompareForceOut(0, outputType, 0, 0, 0, 1, cardId);
            }
            return rtn;
        }
        public short CmpStatus(short cardId, out short status, out int count)
        {
            status = 0;
            count = 0;
            return 0;
        }
        public short CmpStop(short cardId)
        {
            return mc.API_PosCompareStop(1, cardId);
        }
        public short CmpLinear(short cardId, short axisId, short channel, int startPos, int repeatTimes, int interval, short time, short source)
        {
            //return mc.GT_CompareLinear(cardId, axisId, channel, startPos, repeatTimes, interval, time, source);
            return 0;
        }
        public short CmpData(short cardId, short axisId, short source, short pulseType, short startLevel, short time, ref int[] buf1, short count1, ref int[] buf2, short count2)
        {
            //清空缓冲区
            short rtn = mc.API_PosCompareClear(1, cardId);
            if (rtn != 0) return rtn;
            //设置一维比较模式
            mc.TPosCompareMode mode = new mc.TPosCompareMode()
            {
                mode = mc.COMPARE_MODE_1D,
                compareSource = mc.RES_ENCODER,
                sourceX = axisId,
                sourceY = mc.RES_NONE,
                outputMode = pulseType,
                pulseWidthH = (ushort)time,
                pulseWidthL = 100,
                startLevel = (ushort)startLevel,
                threshold = 10
            };
            rtn = mc.API_SetPosCompareMode(1, ref mode, cardId);
            if (rtn != 0) return rtn;
            //压入数据，只压入X
            mc.TPosCompareData[] data = new mc.TPosCompareData[2048];
            for (int i = 0; i < count1; i++)
            {
                data[i].posX = -buf1[i];
                data[i].posY = 0;
                data[i].triValue = 1;
            }
            rtn = mc.API_SetPosCompareData(1, ref data[0], count1, cardId);
            if (rtn != 0) return rtn;
            //启动比较
            return mc.API_PosCompareStart(1, cardId);
        }


        public short EncOn(short cardId, short axisId)
        {
            return 0;
        }
        public short EncOff(short cardId, short axisId)
        {
            return 0;
        }

        int ICardExecutable.DiArriveData { get; set; }
        public short GetDiArrive(short cardId, out int data)
        {
            return mc.API_GetDi(mc.RES_ARRIVE, out data, cardId);
        }
        public int GetDiArriveBit(int data, short axisId)
        {
            return (data >> (axisId - 1)) & 1;
        }

        public short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n)
        {
            mc.TCrdData[] trdData = new mc.TCrdData[4096];
            return mc.API_InitLookAhead(crd, fifo, accMax, ref trdData[0], n, cardNum);
        }

        public short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n, gts.mc.TCrdData[] trdData)
        {
            //return mc.API_InitLookAhead(crd, fifo, accMax, ref trdData[0], n, cardNum);
            return -1;
        }

        public short GetLookAheadSpace(short cardNum, short crd, out int pSpace, short fifo)
        {
            pSpace = 0;
            return 0;
        }

        public short GetLookAheadSegCount(short cardNum, short crd, out int pSegCount, short fifo)
        {
            pSegCount = 0;
            return 0;
        }

        public short GT_CrdData(short cardNum, short crd, System.IntPtr pCrdData, short fifo)
        {
            return mc.API_SendAllCrdData(crd, fifo, cardNum);
        }

        public short AddCrdGear(short cardId, short csId, short gearAxisId, int gearPos)
        {
            return 0;
        }

        public short AddBufMove(short cardId, short csId, short axisId, int pos, double vel, double acc, int mode)
        {
            return 0;
        }

        public short ADCmp2Data(short cardId, short chn, short axisxId, short axisyId, int[] x, int[] y, short outType, ushort startLevel, ushort pulseWidthH, ushort pulseWidthL, short threshold)
        {
           // chn = chn++;
            short rtn =-1;
            //设置二维比较模式
            mc.TPosCompareMode mode = new mc.TPosCompareMode()
            {
                mode = mc.COMPARE_MODE_2D,
                compareSource = mc.RES_PROFILE,
                sourceX = axisxId,
                sourceY = axisyId,
                outputMode = mc.COMPARE_OUT_PULSE,
                pulseWidthH = pulseWidthH,
                pulseWidthL = pulseWidthL,
                startLevel = 0,
                threshold = 300

            };
     
            rtn = mc.API_SetPosCompareMode(1, ref mode, 1);
            if (rtn != 0) return rtn;
            mc.API_PosCompareClear(1, 1); //清除位置比较缓存区 channel为通道号
            //压入数据
            short count1 =(short) x.Length;
            mc.TPosCompareData[] data = new mc.TPosCompareData[count1];

            for (int i = 0; i < count1; i++)
            {
                data[i].posX = -x[i];
                data[i].posY = -y[i];
                data[i].segmentID = (ushort) i;
              data[i].triValue = 1;
            }
            rtn = mc.API_SetPosCompareData(1, ref data[0], count1, 1);
            if (rtn != 0) return rtn;
            //启动比较
            rtn= mc.API_PosCompareStart(1, 1);
            return 0;
        }
    }
}
