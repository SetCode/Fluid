using Anda.Fluid.Drive.LightSystem;
using Anda.Fluid.Drive.Motion;
using Anda.Fluid.Drive.Motion.CardFramework.MotionCard;
using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Sensors.Lighting.OPT;
using Anda.Fluid.Drive.Vision.CameraFramework;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Alarming;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive
{
    /// <summary>
    /// Author：liyi
    /// Date：2019/06/14
    /// 以相机为基准的底层运动控制
    /// </summary>
    public class CameraMotion
    {
        /// <summary>
        /// 相机触发脉宽
        /// </summary>
        public int PulseWidth { get; set; }

        public Card Card { get; set; }

        public short Chn { get; set; }
        /// <summary>
        /// 2D比较最大误差
        /// </summary>
        public int Cmp2dMaxErr { get; set; }
        /// <summary>
        /// 2D比较最优点计算阈值
        /// </summary>
        public int Cmp2dThreshold { get; set; } = 0;
        public CameraMotion(Card card,short chn,int pulseWidth,int cmp2dMaxErr,int cmp2dThreshold)
        {
            this.Card = card;
            this.Chn = chn;
            this.PulseWidth = pulseWidth;
            this.Cmp2dMaxErr = cmp2dMaxErr;
            this.Cmp2dThreshold = cmp2dThreshold;
        }
        /// <summary>
        /// 飞拍相机、光源的初始化设置
        /// </summary>
        /// <returns></returns>
        public Result FlyMarksInit(LightType lightType, int gain, int ExpExposureTime)
        {
            //高速IO设备的切换
            DOMgr.Instance.FindBy(2).Set(true);
            //光源设置
            //Machine.Instance.Light.SetLight(lightType);
            if (Machine.Instance.Light.Lighting.lightVendor == Sensors.Lighting.LightVendor.Anda)
            {
                AndaLight light = Machine.Instance.Light.Lighting as AndaLight;
                light.SetLight(lightType);
            }
           
            //相机参数设置
            Machine.Instance.Camera.StopGrabing();
            Machine.Instance.Camera.SetTriggerMode(true);
            Machine.Instance.Camera.SetGain(gain);
            Machine.Instance.Camera.SetExposure(ExpExposureTime);
            Machine.Instance.Camera.StartGrabing();
            return Result.OK;
        }
        public Result FlyMarksInit(ModelFindPrm modelFindPrm, int gain, int ExpExposureTime)
        {
            //高速IO设备的切换
            DOMgr.Instance.FindBy(2).Set(true);
            //光源设置
            //Machine.Instance.Light.SetLight(lightType);
            Machine.Instance.Light.SetLight(modelFindPrm.ExecutePrm);
            //相机参数设置
            Machine.Instance.Camera.StopGrabing();
            Machine.Instance.Camera.SetTriggerMode(true);
            Machine.Instance.Camera.SetGain(gain);
            Machine.Instance.Camera.SetExposure(ExpExposureTime);
            Machine.Instance.Camera.StartGrabing();
            return Result.OK;
        }

        /// <summary>
        /// 结束飞拍，相机光源设定换还原到连续采集状态
        /// </summary>
        /// <returns></returns>
        public Result FlyMarksEnd()
        {
            //结束飞拍恢复相关设置
            DOMgr.Instance.FindBy(2).Set(false);
            Machine.Instance.Camera.StopGrabing();
            Machine.Instance.Camera.SetContinueMode();
            Machine.Instance.Camera.StartGrabing();
            return Result.OK;
        }

        ///<summary>
        /// Description	:飞拍生成空点并执行前瞻插补
        /// Author  	:liyi
        /// Date		:2019/06/13
        ///</summary>   
        /// <param name="points">每一个飞拍Mark点位</param>
        /// <param name="vel">飞拍速度</param>
        /// <param name="acc">飞拍加速度</param>
        /// <param name="cornerSpeed">拐弯速度</param>
        /// <param name="cornerAcc">拐弯加速度</param>
        /// <param name="preDistance">空点距离</param>
        /// <param name="flyDistance">行间距离或列间距</param>
        /// <param name="isRowFirst">行模式或列模式</param>
        /// <returns></returns>
        public Result FlyMarksExecute(List<List<PointD>> points, double vel, double acc, double cornerSpeed, double preDistance, double flyDistance, bool isRowFirst)
        {
            //生成空点
            for (int i = 0; i < points.Count; i++)
            {
                int PointCount = points[i].Count;
                PointD firstNullPoint = new PointD();
                PointD LastNullPoint = new PointD();
                //奇数行先塞左边，再塞右边，偶数行先右后左
                if (isRowFirst)
                {
                    firstNullPoint.Y = points[i][0].Y;
                    LastNullPoint.Y = points[i][0].Y;
                    if ((i % 2) != 1)
                    {
                        firstNullPoint.X = points[i][0].X - preDistance;
                        LastNullPoint.X = points[i][PointCount-1].X + preDistance;
                    }
                    else
                    {
                        firstNullPoint.X = points[i][0].X + preDistance;
                        LastNullPoint.X = points[i][PointCount-1].X - preDistance;
                    }
                }
                else
                {
                    firstNullPoint.X = points[i][0].X;
                    LastNullPoint.X = points[i][0].X;
                    if ((i % 2) != 1)
                    {
                        firstNullPoint.Y = points[i][0].Y - preDistance;
                        LastNullPoint.Y = points[i][PointCount-1].Y + preDistance;
                    }
                    else
                    {
                        firstNullPoint.Y = points[i][0].Y + preDistance;
                        LastNullPoint.Y = points[i][PointCount-1].Y - preDistance;
                    }
                }
                points[i].Insert(0, firstNullPoint);
                points[i].Add(LastNullPoint);
            }
            for (int i = 1; i < points.Count; i++)
            {
                if (isRowFirst)
                {
                    if (i%2 == 0)
                    {
                        if (points[i-1].Last().X < points[i][0].X)
                        {
                            points[i][0].X = points[i-1][points[i-1].Count - 1].X;
                        }
                        else
                        {
                            points[i-1][points[i-1].Count - 1].X = points[i][0].X;
                        }
                    }
                    else
                    {
                        if (points[i-1].Last().X > points[i][0].X)
                        {
                            points[i][0].X = points[i - 1][points[i - 1].Count - 1].X;
                        }
                        else
                        {
                            points[i - 1][points[i - 1].Count - 1].X = points[i][0].X;
                        }
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        if (points[i-1].Last().Y < points[i][0].Y)
                        {
                            points[i][0].Y = points[i-1][points[i-1].Count - 1].Y;
                        }
                        else
                        {
                            points[i-1][points[i-1].Count - 1].Y = points[i][0].Y;
                        }
                    }
                    else
                    {
                        if (points[i-1].Last().Y > points[i][0].Y)
                        {
                            points[i][0].Y = points[i-1][points[i-1].Count - 1].Y;
                        }
                        else
                        {
                            points[i-1][points[i-1].Count - 1].Y = points[i][0].Y;
                        }
                    }
                }
            }
            return Machine.Instance.Robot.MoveFlyMarksPos(points, vel, acc, cornerSpeed);
        }
        /// <summary>
        /// 启动2d比较
        /// </summary>
        /// <param name="cmp2dSrc">比较源，0：规划器，1：编码器</param>
        /// <param name="cmp2dMaxErr">最大误差范围</param>
        /// <param name="points">2d比较点，绝对坐标</param>
        public Result Cmp2dStart(short cmp2dSrc, short cmp2dMaxErr, PointD[] points)
        {
            PointD[] cmp2dPoints = new PointD[points.Length];
            //获取2d比较点数据，转换为相对启动点的相对坐标
            for (int i = 0; i < points.Length; i++)
            {
                cmp2dPoints[i] = new PointD(
                    points[i].X - Machine.Instance.Robot.PosX,
                    points[i].Y - Machine.Instance.Robot.PosY);
            }
            short threshold = (short)this.Cmp2dThreshold;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }
            short rtn = Machine.Instance.cameraMotion.CmpStop();
            if (rtn != 0)
            {
                return Result.FAILED;
            }
            
            //初始化2d比较
            rtn = Machine.Instance.Robot.Cmp2dInit(this.Chn, cmp2dSrc, (short)this.PulseWidth, cmp2dMaxErr, threshold);
            if (rtn != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
                return Result.FAILED;
            }
            //导入2d比较点数据
            rtn = Machine.Instance.Robot.Cmp2dData(this.Chn, cmp2dPoints);
            if (rtn != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dData);
                return Result.FAILED;
            }
            //启动2d比较
            rtn = Machine.Instance.Robot.Cmp2dStart(this.Chn);
            if (rtn != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
                return Result.FAILED;
            }
            return Result.OK;
        }
        public void Cmp2dStop()
        {
            //stop cmp2d
            if (Machine.Instance.Robot.Cmp2dStop(this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStop);
            }
        }

        /// <summary>
        /// 启动2d比较的一维模式
        /// </summary>
        /// <param name="cmp2dSrc"></param>
        /// <param name="cmp2dMaxErr"></param>
        public void Cmp2dMode1dStart(short cmp2dSrc, short cmp2dMaxErr)
        {
            short threshold = (short)this.Cmp2dThreshold;
            if (cmp2dMaxErr < threshold)
            {
                threshold = cmp2dMaxErr;
            }
            //初始化2d比较(一维模式)
            if (Machine.Instance.Robot.Cmp2dMode1dInit(this.Chn, cmp2dSrc, (short)this.PulseWidth, cmp2dMaxErr, threshold) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dInit);
            }
            //启动2d比较
            if (Machine.Instance.Robot.Cmp2dStart(this.Chn) != 0)
            {
                AlarmServer.Instance.Fire(Machine.Instance.Robot, AlarmInfoMotion.FatalCmp2dStart);
            }
        }
        public short CmpStart(short source, PointD[] points)
        {
            if (points == null || points.Length == 0)
            {
                return -10;
            }

            Axis axis = null;
            int[] buf1 = new int[points.Length];
            int[] buf2 = new int[1];
            int startPos, interval;

            Action updateBuf2 = () =>
            {
                if (buf1[0] == 0)
                {
                    buf1[0] += 1;
                    buf2[0] = 1000000;
                }
                else if (buf1[0] > 0)
                {
                    buf2[0] = 1000000;
                }
                else
                {
                    buf2[0] = -1000000;
                }
            };

            if (points.Length == 1)
            {
                axis = Machine.Instance.Robot.AxisX;
                buf1[0] = axis.ConvertPos2Card(points[0].X - Machine.Instance.Robot.PosX);
                updateBuf2();
                startPos = buf1[0];
                interval = 0;
            }
            else
            {
                double lx = points[points.Length - 1].X - points[0].X;
                double ly = points[points.Length - 1].Y - points[0].Y;
                if (Math.Abs(lx) >= Math.Abs(ly))
                {
                    axis = Machine.Instance.Robot.AxisX;
                    for (int i = 0; i < points.Length; i++)
                    {
                        buf1[i] = axis.ConvertPos2Card(points[i].X - Machine.Instance.Robot.PosX);
                        if (lx > 0 && buf1[i] <= 0)
                        {
                            buf1[i] = 1;
                        }
                        else if (lx < 0 && buf1[i] >= 0)
                        {
                            buf1[i] = -1;
                        }
                    }
                    updateBuf2();
                    startPos = buf1[0];
                    interval = axis.ConvertPos2Card(lx / (points.Length - 1));
                }
                else
                {
                    axis = Machine.Instance.Robot.AxisY;
                    for (int i = 0; i < points.Length; i++)
                    {
                        buf1[i] = axis.ConvertPos2Card(points[i].Y - Machine.Instance.Robot.PosY);
                        if (ly > 0 && buf1[i] <= 0)
                        {
                            buf1[i] = 1;
                        }
                        else if (ly < 0 && buf1[i] >= 0)
                        {
                            buf1[i] = -1;
                        }
                    }
                    updateBuf2();
                    startPos = buf1[0];
                    interval = axis.ConvertPos2Card(ly / points.Length);
                }
            }

            switch (this.Chn)
            {
                case 0:
                    short m;
                    m = this.Card.Executor.CmpData(axis.CardId, axis.AxisId, source, 0, 0, (short)this.PulseWidth, ref buf1, (short)buf1.Length, ref buf2, (short)buf2.Length);
                    //m = this.Card.Executor.CmpLinear(axis.CardId, axis.AxisId, (short)(this.Chn + 1), startPos, points.Length, interval, (short)this.Prm.OnTime, source);
                    return m;
                case 1:
                    return this.Card.Executor.CmpData(axis.CardId, axis.AxisId, source, 0, 0, (short)this.PulseWidth, ref buf2, (short)buf2.Length, ref buf1, (short)buf1.Length);
                    //return this.Card.Executor.CmpLinear(axis.CardId, axis.AxisId, (short)(this.Chn + 1), startPos, points.Length, interval, (short)this.Prm.OnTime, source);
            }
            return -10;
        }
        public short CmpStop()
        {
            return this.Card.Executor.CmpStop(this.Card.CardId);
        }
    }
}
