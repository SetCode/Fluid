using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Anda.Fluid.Infrastructure.Algo
{
    /// <summary>
    /// 给定一组点的坐标，指定从第一个点开始，遍历所有点，每个点不重复遍历，计算距离最短的路径
    /// </summary>
    public class OptimalRoute
    {
        public static int[] Calculate(List<PointD> points)
        {
            int[] routeIndexArr = null;
            if (points == null || points.Count <= 0)
            {
                routeIndexArr = new int[0];
                return routeIndexArr;
            }
            else if (points.Count <= 2)
            {
                routeIndexArr = new int[points.Count];
                for (int i = 0; i < routeIndexArr.Length; i++)
                {
                    routeIndexArr[i] = i;
                }
                return routeIndexArr;
            }
            double[] data = new double[points.Count * 2];
            for (int i = 0; i < points.Count; i++)
            {
                data[i * 2] = points[i].X;
                data[i * 2 + 1] = points[i].Y;
            }
            routeIndexArr = new int[points.Count];
            autoRunAntColony(data, points.Count, routeIndexArr);
            return routeIndexArr;
        }

        private static void autoRunAntColony(double[] points, int n, int[] reIndex)
        {
            if (Environment.Is64BitOperatingSystem)
            {
#if DEBUG
                        autoRunAntColonyx64d(points, n, reIndex);
#else
                autoRunAntColonyx64(points, n, reIndex);
#endif
            }
            else
            {
#if DEBUG
                        autoRunAntColonyx86d(points, n, reIndex);
#else
                autoRunAntColonyx86(points, n, reIndex);
#endif
            }
        }

        [DllImport("antcolony_x86d.dll", EntryPoint = "autoRunAntColony", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern void autoRunAntColonyx86d(double[] points, int n, int[] reIndex);

        [DllImport("antcolony_x64d.dll", EntryPoint = "autoRunAntColony", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern void autoRunAntColonyx64d(double[] points, int n, int[] reIndex);

        [DllImport("antcolony_x86.dll", EntryPoint = "autoRunAntColony", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern void autoRunAntColonyx86(double[] points, int n, int[] reIndex);

        [DllImport("antcolony_x64.dll", EntryPoint = "autoRunAntColony", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern void autoRunAntColonyx64(double[] points, int n, int[] reIndex);

        [DllImport("antcolony_x86.dll", EntryPoint = "initializeAll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern void initializeAll();
    }
}
