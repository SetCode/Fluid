using System;
using System.Collections.Generic;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Domain.FluProgram.Semantics;
using Anda.Fluid.Domain.FluProgram.Structure;
using Anda.Fluid.Infrastructure.Utils;
using Anda.Fluid.Drive;
using System.Threading;
using Anda.Fluid.Drive.Vision.ModelFind;
using Anda.Fluid.Infrastructure.Trace;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Anda.Fluid.Domain.FluProgram.Executant
{
    /// <summary>
    /// Author:liyi
    /// Date：2019/06/12
    /// 飞拍Mark执行指令
    /// 处理所有mark点的拍照与坐标
    /// </summary>
    [Serializable]
    public class FlyMarks : Directive
    {
        private List<MarkCmd> markCmdList = new List<MarkCmd>();
        /// <summary>
        /// 用于保存所有Mark点位的偏移量
        /// </summary>
        private Dictionary<MarkCmd, VectorD> markPointsOffset = new Dictionary<MarkCmd, VectorD>();

        private CoordinateCorrector coordinateCorrector;

        private bool imageDisposeFinish = false;

        private bool isRowFirst;
        private double flySpeed;
        private double flyAcc;
        private double flyCornerSpeed;
        private double flyPreDistance;
        private double flyDistance;
        /// <summary>
        /// 图像处理过程结果 -1：获取的图像不够，处理超时 0：正常完成
        /// </summary>
        public int Imageflag { get; set; } = 0;

        ///<summary>
        /// Description	:Description
        /// Author  	:liyi
        /// UpdateDate	:2019/06/11
        ///</summary>   
        /// <param name="markCmdList"></param>
        /// <param name="coordinateCorrector"></param>
        /// <param name="isRowSort"></param>
        public FlyMarks(List<MarkCmd> markCmdList, CoordinateCorrector coordinateCorrector)
        {
            this.markCmdList = markCmdList;
            //初始化所有提前量
            foreach (MarkCmd item in markCmdList)
            {
                markPointsOffset.Add(item, new VectorD());
            }
            this.Program = markCmdList[0].RunnableModule.CommandsModule.Program;
            this.coordinateCorrector = coordinateCorrector;
            this.isRowFirst = Program.RuntimeSettings.FlyIsRowFirst;
            this.flySpeed = Program.RuntimeSettings.FlySpeed;
            this.flyAcc = Program.RuntimeSettings.FlyAcc;
            this.flyCornerSpeed = Program.RuntimeSettings.FlyCornerSpeed;
            this.flyPreDistance = Program.RuntimeSettings.FlyPreDistance;
            //飞行的行或列之间的间距判定值（半个相机视野）
            PointD view = Machine.Instance.Camera.ToMachine(0, 0);
            double columnDistacnce = Math.Abs(view.X);
            double rowDistance = Math.Abs(view.Y);
            double distance = this.isRowFirst ? rowDistance : columnDistacnce;
            this.flyDistance = distance;
        }
        public override Result Execute()
        {
            if (Machine.Instance.Robot.IsSimulation)
            {
                return Result.OK;
            }
            //获取方向判断数组
            List<double> dirValueList = GetDirValueList();
            //Mark点行排序(从左到右)
            MarkPositionComparer comparer = new MarkPositionComparer(this.isRowFirst, this.flyDistance,dirValueList);
            markCmdList.Sort(comparer);
            //先蛇形排列，再判断同一行(同一列)的修正为同一直线
            List<List<PointD>> points = this.GetFixPointList();
            //设置启动位置比较(1维/2维)
            List<PointD> capture = new List<PointD>();
            foreach (List<PointD> PointList in points)
            {
                foreach (PointD item in PointList)
                {
                    capture.Add(item);
                }
            }
            Result result;
            //初始化飞拍相关设置
            //result = Machine.Instance.cameraMotion.FlyMarksInit(markCmdList[0].ModelFindPrm.LightType, markCmdList[0].ModelFindPrm.Gain, markCmdList[0].ModelFindPrm.ExposureTime);
            result = Machine.Instance.cameraMotion.FlyMarksInit(markCmdList[0].ModelFindPrm, markCmdList[0].ModelFindPrm.Gain, markCmdList[0].ModelFindPrm.ExposureTime);
            if (!result.IsOk)
            {
                return result;
            }
            result = Machine.Instance.cameraMotion.Cmp2dStart(1, 200, capture.ToArray());
            if (!result.IsOk)
            {
                return result;
            }
            //启动图像处理线程处理线程
            this.imageDisposeFinish = false;
            List<Task> imageTasks = new List<Task>();
            for (int i = 0; i < this.Program.RuntimeSettings.DisposeThreadCount; i++)
            {
                Task task = Task.Factory.StartNew(DisposeMarkImageThread);
                imageTasks.Add(task);
            }
            //移动到拍照高度 -- 开始飞行之前降z轴
            if (Machine.Instance.Setting.MachineSelect == MachineSelection.RTV)
            {
                result = Machine.Instance.Robot.MoveToMarkZAndReply();
                if (!result.IsOk)
                {
                    return result;
                }
            }
            //添加插补数据(增加空点),生成运动指令并执行 //上层传递速度及加速度参数
            result = Machine.Instance.cameraMotion.FlyMarksExecute(points, this.flySpeed, this.flyAcc, this.flyCornerSpeed, this.flyPreDistance, this.flyDistance, this.isRowFirst);
            if (!result.IsOk)
            {
                return result;
            }
            //判断Mark是否全部处理完成
            foreach (Task item in imageTasks)
            {
                item.Wait();
            }
            //完成后 关闭线程
            Machine.Instance.cameraMotion.FlyMarksEnd();
            if (Imageflag == -1)
            {
                return Result.FAILED;
            }
            return Result.OK;
        }

        ///<summary>
        /// Description	:飞拍图像线程处理函数
        /// Author  	:liyi
        /// Date		:2019/06/13
        ///</summary>   
        private void DisposeMarkImageThread()
        {
            this.Imageflag = 0;
            Stopwatch totalTime = new Stopwatch();
            totalTime.Start();
            while (true)
            {
                KeyValuePair<int, byte[]> imageBytes;
                bool canNotGetImage = false;
                bool otherThreadFinish = false;
                Stopwatch oneTime = new Stopwatch();
                oneTime.Start();
                while (!Machine.Instance.Camera.ImageByteBuffer.TryDequeue(out imageBytes))
                {
                    if (totalTime.ElapsedMilliseconds > markCmdList.Count * 1000 || oneTime.ElapsedMilliseconds > 15000)
                    {
                        Imageflag = -1;
                        canNotGetImage = true;
                        break;
                    }
                    if (this.imageDisposeFinish)
                    {
                        otherThreadFinish = true;
                        break;
                    }
                    Thread.Sleep(2);
                }
                if (canNotGetImage)//超时取不到图片
                {
                    Log.Dprint("Timeout:Can not get iamge");
                    totalTime.Stop();
                    break;
                }
                else if(otherThreadFinish)//在取不到图片且其他线程已处理完最后一张图片时结束
                {
                    Log.Dprint("other thread finish");
                    totalTime.Stop();
                    break;
                }
                else
                {
                    Log.Dprint("Get ImageIndex:" + imageBytes.Key);
                    int imageIndex = imageBytes.Key;
                    markCmdList[imageIndex].ModelFindPrm.ImgData = imageBytes.Value;
                    markCmdList[imageIndex].ModelFindPrm.ImgWidth = Machine.Instance.Camera.Executor.ImageWidth;
                    markCmdList[imageIndex].ModelFindPrm.ImgHeight = Machine.Instance.Camera.Executor.ImageHeight;
                    //先做跳过处理，完成后再考虑整体优化
                    if (!markCmdList[imageIndex].ModelFindPrm.Execute())
                    {
                        markCmdList[imageIndex].RunnableModule.Mode = ModuleMode.SkipMode;
                        Program.ModuleStructure.SetAllChildModuleMode(markCmdList[imageIndex].RunnableModule, ModuleMode.SkipMode);
                    }
                    else
                    {
                        //保存飞拍Mark实际坐标
                        //飞拍触发坐标+图像坐标+飞拍校准值(校准值的计算逻辑可能有问题，需要重写)
                        coordinateCorrector.SetMarkRealPosition(markCmdList[imageIndex], markCmdList[imageIndex].FlyPosition + Machine.Instance.Camera.ToMachine(markCmdList[imageIndex].ModelFindPrm.MarkInImg) + markCmdList[imageIndex].FlyOffset);
                        if (markCmdList[imageIndex].ModelFindPrm.IsUnStandard)
                        {
                            if (markCmdList[imageIndex].ModelFindPrm.UnStandardType == 1)
                            {
                                coordinateCorrector.SetASVMarkRealPosition2(markCmdList[imageIndex], markCmdList[imageIndex].FlyPosition + Machine.Instance.Camera.ToMachine(markCmdList[imageIndex].ModelFindPrm.MarkInImg2) + markCmdList[imageIndex].FlyOffset);
                            }
                            else
                            {
                                coordinateCorrector.SetASVMarkRealAngle(markCmdList[imageIndex], markCmdList[imageIndex].ModelFindPrm.Angle);
                            }
                        }
                    }
                    // 保存mark图片
                    if (this.Program.RuntimeSettings.SaveMarkImages)
                    {
                        markCmdList[imageIndex].ModelFindPrm.ImgData?.SaveMarkImage(
                             markCmdList[imageIndex].ModelFindPrm.ImgWidth,
                              markCmdList[imageIndex].ModelFindPrm.ImgHeight,
                              this.Program.Name, "Marks", "mark");
                    }
                    if (imageIndex == markCmdList.Count - 1)
                    {
                        this.imageDisposeFinish = true;
                        totalTime.Stop();
                        break;
                    }
                }
                Thread.Sleep(2);
            }
        }

        /// <summary>
        /// 获取排序并修正后的所有Mark拍照点位
        /// </summary>
        /// <returns></returns>
        public List<List<PointD>> GetFixPointList()
        {
            List<List<PointD>> fixPoints = new List<List<PointD>>();
            fixPoints.Add(new List<PointD>());
            int curList = 0;
            double curStrandPos;
            fixPoints[curList].Add(new PointD(markCmdList[0].Position.X, markCmdList[0].Position.Y));
            markCmdList[0].FlyPosition = new PointD(markCmdList[0].Position.X, markCmdList[0].Position.Y);
            //使用原始坐标飞行还是修正坐标飞行
            if (this.isRowFirst)
            {
                curStrandPos = fixPoints[curList][0].Y;
            }
            else
            {
                curStrandPos = fixPoints[curList][0].X;
            }

            for (int i = 1; i < this.markCmdList.Count; i++)
            {
                PointD FlyPoint;
                double curDistance;
                if (this.isRowFirst)
                {
                    curDistance = markCmdList[i].Position.Y - markCmdList[i - 1].Position.Y;
                    if (Math.Abs(curDistance) > this.flyDistance)
                    {
                        //拐角
                        fixPoints.Add(new List<PointD>());
                        curList++;
                        curStrandPos = markCmdList[i].Position.Y;
                    }
                    if (!Program.RuntimeSettings.FlyOriginPos)
                    {
                        FlyPoint = new PointD(markCmdList[i].Position.X, curStrandPos);
                    }
                    else
                    {
                        FlyPoint = new PointD(markCmdList[i].Position.X, markCmdList[i].Position.Y);
                    }
                }
                else
                {
                    curDistance = markCmdList[i].Position.X - markCmdList[i - 1].Position.X;
                    if (Math.Abs(curDistance) > this.flyDistance)
                    {
                        //拐角
                        fixPoints.Add(new List<PointD>());
                        curList++;
                        curStrandPos = markCmdList[i].Position.X;
                    }
                    if (!Program.RuntimeSettings.FlyOriginPos)
                    {
                        FlyPoint = new PointD(curStrandPos, markCmdList[i].Position.Y);
                    }
                    else
                    {
                        FlyPoint = new PointD(markCmdList[i].Position.X, markCmdList[i].Position.Y);
                    }
                }
                fixPoints[curList].Add(FlyPoint);
                //每个Mark保存对应飞拍触发坐标--用于图像处理时的计算坐标
                markCmdList[i].FlyPosition = FlyPoint;
            }
            return fixPoints;
        }
        /// <summary>
        /// 计算得出每一行的判定值，用于给后面的Mark比较器判断当前Mark是正向排列还是反向排列？
        /// </summary>
        /// <returns>返回每一行(列)的参考值</returns>
        private List<double> GetDirValueList()
        {
            List<double> dirValueList = new List<double>();
            bool isSave = true;
            foreach (MarkCmd item in markCmdList)
            {
                double value = isRowFirst ? item.Position.Y : item.Position.X;
                for (int i = 0; i < dirValueList.Count; i++)
                {
                    if (Math.Abs(dirValueList[i] - value) < this.flyDistance)
                    {
                        isSave = false;
                        break;
                    }
                    else
                    {
                        isSave = true;
                    }
                }
                if (isSave == true)
                {
                    dirValueList.Add(value);
                    isSave = false;
                }
            }
            dirValueList.Sort();
            return dirValueList;
        }
    }

    /// <summary>
    /// Author:liyi
    /// Date:2019/06/12
    /// Mark点行排序或列排序比较器
    /// </summary>
    public class MarkPositionComparer : IComparer<MarkCmd>
    {
        private bool isRowFirst;
        private double flyDistance;
        private List<double> dirValueList;
        /// <summary>
        /// 比较器构造函数
        /// </summary>
        /// <param name="isRowFirst">行模式还是列模式</param>
        public MarkPositionComparer(bool isRowFirst, double flyDistance,List<double> dirValueList)
        {
            this.isRowFirst = isRowFirst;
            this.flyDistance = flyDistance;
            this.dirValueList = dirValueList;
        }
        /// <summary>
        /// 先行后列排序
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public int CompareMarkPositionRowFirst(MarkCmd A, MarkCmd B)
        {
            
            if (A == null && B == null)
            {
                return 0;
            }
            else if (A == null)
            {
                return -1;
            }
            else if (B == null)
            {
                return 1;
            }
            else
            {
                double distance = A.Position.Y - B.Position.Y;
                if (Math.Abs(distance) < flyDistance)
                {
                    //判定为同一行的情况下需要判断方向
                    int dir = 1;
                    for (int i = 0; i < dirValueList.Count; i++)
                    {
                        if (Math.Abs(dirValueList[i] - A.Position.Y) < flyDistance)
                        {
                            //奇数行正向(左至右)
                            dir = (i % 2 == 0) ? 1 : -1;
                        }
                    }
                    if (A.Position.X < B.Position.X)
                    {
                        return -1*dir;
                    }
                    else if (A.Position.X > B.Position.X)
                    {
                        return 1*dir;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (distance < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
        /// <summary>
        /// 先列后行排序
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public int CompareMarkPositionColumnFirst(MarkCmd A, MarkCmd B)
        {
            if (A == null && B == null)
            {
                return 0;
            }
            else if (A == null)
            {
                return -1;
            }
            else if (B == null)
            {
                return 1;
            }
            else
            {

                double distance = A.Position.X - B.Position.X;
                if (Math.Abs(distance) < flyDistance)
                {
                    //判定为同一行的情况下需要判断方向
                    int dir = 1;
                    for (int i = 0; i < dirValueList.Count; i++)
                    {
                        if (Math.Abs(dirValueList[i] - A.Position.X) < flyDistance)
                        {
                            //奇数行正向(左至右)
                            dir = (i % 2 == 0) ? 1 : -1;
                        }
                    }
                    if (A.Position.Y < B.Position.Y)
                    {
                        return -1 * dir;
                    }
                    else if (A.Position.Y > B.Position.Y)
                    {
                        return 1 * dir;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (distance < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }

                //double degree = MathUtils.CalculateArc(A.Position, B.Position);
                //if (degree < 2 || Math.Abs(180 - degree) < 2)
                //{
                //    if (A.Position.X < B.Position.X)
                //    {
                //        return -1;
                //    }
                //    else if (A.Position.X > B.Position.X)
                //    {
                //        return 1;
                //    }
                //    else
                //    {
                //        return 0;
                //    }
                //}
                //else
                //{
                //    if (degree < 180)
                //    {
                //        return -1;
                //    }
                //    else
                //    {
                //        return 1;
                //    }
                //}
            }
        }
        /// <summary>
        /// 比较器接口实现
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(MarkCmd x, MarkCmd y)
        {
            if (isRowFirst)
            {
                return this.CompareMarkPositionRowFirst(x, y);
            }
            else
            {
                return this.CompareMarkPositionColumnFirst(x, y);
            }
        }
    }
}
