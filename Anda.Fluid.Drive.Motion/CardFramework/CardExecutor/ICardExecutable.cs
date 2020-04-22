using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gts.mc;
using gts;

namespace Anda.Fluid.Drive.Motion.CardFramework.CardExecutor
{
    public enum CardExecutorType
    {
        GTS,
        ADMC
    }

    public enum HomeMode
    {
        Limit,
        Limit_Home,
        Limit_Index,
        Limit_Home_Index,
        Home,
        Home_Index,
        Index
    }
    public interface ICardExecutable : IIOExecutable
    {
        /// <summary>
        /// 控制器类型
        /// </summary>
        CardExecutorType ExecutorType { get; }

        /// <summary>
        /// 打开控制器
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        short Open(short cardId);

        /// <summary>
        /// 关闭控制器
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        short Close(short cardId);

        /// <summary>
        /// 重置控制器
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        short Reset(short cardId);

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="configFile"></param>
        /// <returns></returns>
        short LoadConfig(short cardId, string configFile);

        short SaveConfig(short cardId, string configFile);

        /// <summary>
        /// 轴使能
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="axisId"></param>
        /// <returns></returns>
        short AxisOn(short cardId, short axisId);

        /// <summary>
        /// 轴消能
        /// </summary>
        /// <param name="cardId"></param>
        /// <param name="axisId"></param>
        /// <returns></returns>
        short AxisOff(short cardId, short axisId);

        int DiArriveData { get; set; }
        short GetDiArrive(short cardId, out int data);
        int GetDiArriveBit(int data, short axisId);

        short ClrAxisSts(short cardId, short axisId);
        short ClrAxesSts(short cardId, short axisId, short count);
        short GetAxisSts(short cardId, short axisId, out int sts);
        short GetAxesSts(short cardId, short axisId, short count, out int[] sts);
        short ZeroPos(short cardId, short axisId);
        short GetEncPos(short cardId, short axisid, out int pos);
        short GetEncPos(short cardId, short axisId, short count, out int[] pos);
        short GetPrfPos(short cardId, short axisId, out int pos);
        short GetPrfPos(short cardId, short axisId, short count, out int[] pos);
        short SetStopDec(short cardId, short axisId, double smoothDec, double abruptDec);
        short SetAxisLimit(short cardId, short axisId, int positivePositon, int negativePostin);

        short MoveSmoothStop(short cardId, params short[] axisId);
        short MoveAbruptStop(short cardId, params short[] axisId);
        short MoveHome(short cardId, short axisId, HomeMode mode, short moveDir, short indexDir, double velH, double velL, double acc, int homeOffset, int searchHomeDis, int searchIndexDis, int escapeStep);
        short GetMoveHomeStatus(short cardId, short axisId, out short run, out short error);
        short SetMovePos(short cardId, short axisId, int pos, double vel, double acc, double dec);
        short MovePos(short cardId, params short[] axisId);
        short MovePos(short cardId, short axisId);
        short MoveJog(short cardId, short axisId, double vel, double acc);

        short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, int orgX, int orgY);
        short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, int orgX, int orgY, int orgA, int cardType);
        short InitCrd(short cardId, short csId, double velMax, double accMax, short evenTime, short axisX, short axisY, short axisA, short axisB, int orgX, int orgY, int orgA, int orgB, int cardType);
        short ClrCrdBuf(short cardId, short csId);
        short AddCrdLnXY(short cardId, short csId, int posX, int posY, double vel, double acc, double velEnd);
        short AddCrdLnXYA(short cardId, short csId, int posX, int posY, int posA, double vel, double acc, double velEnd);
        short AddCrdLnXYAB(short cardId, short csId, int posX, int posY, int posA, int posB, double vel, double acc, double velEnd);
        short AddCrdBufIO(short cardId, short csId, int mask, int value);
        short AddCrdDelay(short cardId, short csId, ushort delay);
        short AddCrdGear(short cardId, short csId, short gearAxisId, int gearPos);
        short AddBufMove(short cardId, short csId, short axisId, int pos, double vel, double acc, int mode);

        short GetCrdSpace(short cardId, short csId, out int space);
        short GetCrdStatus(short cardId, short csId, out short run, out int segment);
        short AddCrdArcXYC(short cardId, short csId, int posX, int posY, double centerX, double centerY, short clockwise, double vel, double acc, double velEnd);
        short AddCrdArcXYR(short cardId, short csId, int posX, int posY, double r, short clockwise, double vel, double acc, double velEnd);
        short StartCrd(short cardId, short csId);
        short StopCrd(short cardId, short csId);
        short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n);
        short InitLookAhead(short cardNum, short crd, short fifo, double T, double accMax, short n, mc.TCrdData[] trdData);
        short GetLookAheadSpace(short cardNum, short crd, out int pSpace, short fifo);
        short GetLookAheadSegCount(short cardNum, short crd, out int pSegCount, short fifo);
        short GT_CrdData(short cardNum, short crd, System.IntPtr pCrdData, short fifo);
        short Cmp2dStart(short cardId, short chn);
        short Cmp2dStop(short cardId, short chn);
        short Cmp2dClear(short cardId, short chn);
        short Cmp2dMode(short cardId, short chn);
        short Cmp2dMode1d(short cardId, short chn);
        short Cmp2dData(short cardId, short chn, short count, int[] x, int[] y, short fifo);
        short Cmp2dPulse(short cardId, short chn, short level, short outType, short time);
        short Cmp2dSetPrm(short cardId, short chn, short axisxId, short axisyId, short src, short outType, short startLevel, short pulseWidth, short maxerr, short threshold);
        short Cmp2dStatus(short cardId, short chn, out short status, out int count, out short fifo, out short fifoCount, out short bufCount);

        short CmpContiPulseMode(short cardId, short mode, short count, short standTime);
        short CmpPulse(short cardId, short level, short outputType, short time);
        short CmpStatus(short cardId, out short status, out int count);
        short CmpStop(short cardId);
        short CmpLinear(short cardId, short axisId, short channel, int startPos, int repeatTimes, int interval, short time, short source);
        short CmpData(short cardId, short axisId, short source, short pulseType, short startLevel, short time, ref int[] buf1, short count1, ref int[] buf2, short count2);

        short EncOn(short cardId, short axisId);
        short EncOff(short cardId, short axisId);

        short ADCmp2Data(short cardId, short chn, short axisxId, short axisyId, int[] x, int[] y, short outType, ushort startLevel, ushort pulseWidthH, ushort pulseWidthL, short threshold);
        
    }
}
