using Anda.Fluid.Domain.FluProgram.Common;
using Anda.Fluid.Drive.ValveSystem.FluidTrace;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.FluProgram.MathTools
{
    /// <summary>
    /// 对总重模式下的多段线进行打胶点分配
    /// </summary>
    public static class AllocateDotCount
    {
        /// <summary>
        /// 执行分配(传入总点数和线段)
        /// </summary>
        /// <param name="totalShots"></param>
        /// <param name="lineCoordinateList"></param>
        public static void Allocate(int totalShots, List<LineCoordinate> lineCoordinateList)
        {
            //求得特殊点（点起、终点和拐角点）的总数
            int specialShots = 0;
            int corner = 0; //拐角点
            //统计拐角点数量
            foreach (var item in lineCoordinateList)
            {
                if (item.Repetition.StartIsRepeat)
                {
                    corner++;
                }
                if (item.Repetition.EndIsRepeat)
                {
                    corner++;
                }
            }
            specialShots = lineCoordinateList.Count * 2 - corner;

            //可用于分配的点
            int allocateShots = totalShots - specialShots;

            //先按各线段长度比例进行分配
            int[] lengthAllocate = new int[lineCoordinateList.Count];
            double sumLength = 0;
            double[] lineLength = new double[lineCoordinateList.Count];
            //求得各线段长度
            for (int i = 0; i < lineCoordinateList.Count; i++)
            {
                lineLength[i] = lineCoordinateList[i].CalculateDistance();
            }
            //求得总长
            sumLength = lineLength.Sum(); 
            //根据各线段长度占据的比例进行分配       
            for (int i = 0; i < lineCoordinateList.Count; i++)
            {
                lengthAllocate[i] = (int)(allocateShots * (lineLength[i] / sumLength));
            }

            //可用于分配的点
            allocateShots -= lengthAllocate.Sum();

            //按照线段长度进行选择分配
            int[] selectAllocate = new int[lineCoordinateList.Count];
            //如果还有剩下的点，则通过选择排序优先分配给线段长度更长的点
            if (allocateShots > 0)
            {
                Tuple<double[], int[]> sortResult = MathUtils.SelectSort(lineLength, true);

                //如果要分配的点比线数量还多，就给每条线都分配同等数量的点
                while (allocateShots >= lineCoordinateList.Count)
                {
                    for (int i = 0; i < selectAllocate.Length; i++)
                    {
                        selectAllocate[i]++;
                    }
                    allocateShots -= selectAllocate.Length;
                }

                //当要分配的点比线数量少时，则优先分配给更长的线段
                if (allocateShots < lineCoordinateList.Count)
                {
                    for (int i = 0; i < sortResult.Item1.Length; i++)
                    {
                        //具有更长长度的线段多分配一个点
                        selectAllocate[sortResult.Item2[i]]++;
                        allocateShots--;
                        //分配结束
                        if (allocateShots == 0)
                        {
                            break;
                        }
                    }
                }
            }

            //得到最终结果,并赋值到每条线段
            for (int i = 0; i < lineCoordinateList.Count; i++)
            {
                //每天线段的总点数=按长度分配的点数+首尾端点（与其他线段重合的拐角点会在执行时去掉）+按选择分配的点数
                lineCoordinateList[i].Dots = lengthAllocate[i] + 2 + selectAllocate[i];
            }
        }

        /// <summary>
        /// 执行分配(传入总点数和线段)
        /// </summary>
        /// <param name="totalShots"></param>
        /// <param name="lineCoordinateList"></param>
        public static void AllocateMultiTrace(int totalShots, List<TraceBase> traces)
        {
            //求得特殊点（点起、终点和拐角点）的总数
            int specialShots = 0;
            int corner = 0; //拐角点
            //统计拐角点数量
            //foreach (var item in traces)
            //{
            //    if (item.Repetition.StartIsRepeat)
            //    {
            //        corner++;
            //    }
            //    if (item.Repetition.EndIsRepeat)
            //    {
            //        corner++;
            //    }
            //}
            //specialShots = lineCoordinateList.Count * 2 - corner;

            ////可用于分配的点
            //int allocateShots = totalShots - specialShots;

            ////先按各线段长度比例进行分配
            //int[] lengthAllocate = new int[lineCoordinateList.Count];
            //double sumLength = 0;
            //double[] lineLength = new double[lineCoordinateList.Count];
            ////求得各线段长度
            //for (int i = 0; i < lineCoordinateList.Count; i++)
            //{
            //    lineLength[i] = lineCoordinateList[i].CalculateDistance();
            //}
            ////求得总长
            //sumLength = lineLength.Sum();
            ////根据各线段长度占据的比例进行分配       
            //for (int i = 0; i < lineCoordinateList.Count; i++)
            //{
            //    lengthAllocate[i] = (int)(allocateShots * (lineLength[i] / sumLength));
            //}

            ////可用于分配的点
            //allocateShots -= lengthAllocate.Sum();

            ////按照线段长度进行选择分配
            //int[] selectAllocate = new int[lineCoordinateList.Count];
            ////如果还有剩下的点，则通过选择排序优先分配给线段长度更长的点
            //if (allocateShots > 0)
            //{
            //    Tuple<double[], int[]> sortResult = MathUtils.SelectSort(lineLength, true);

            //    //如果要分配的点比线数量还多，就给每条线都分配同等数量的点
            //    while (allocateShots >= lineCoordinateList.Count)
            //    {
            //        for (int i = 0; i < selectAllocate.Length; i++)
            //        {
            //            selectAllocate[i]++;
            //        }
            //        allocateShots -= selectAllocate.Length;
            //    }

            //    //当要分配的点比线数量少时，则优先分配给更长的线段
            //    if (allocateShots < lineCoordinateList.Count)
            //    {
            //        for (int i = 0; i < sortResult.Item1.Length; i++)
            //        {
            //            //具有更长长度的线段多分配一个点
            //            selectAllocate[sortResult.Item2[i]]++;
            //            allocateShots--;
            //            //分配结束
            //            if (allocateShots == 0)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}

            ////得到最终结果,并赋值到每条线段
            //for (int i = 0; i < lineCoordinateList.Count; i++)
            //{
            //    //每天线段的总点数=按长度分配的点数+首尾端点（与其他线段重合的拐角点会在执行时去掉）+按选择分配的点数
            //    lineCoordinateList[i].Dots = lengthAllocate[i] + 2 + selectAllocate[i];
            //}
        }
    }
}
