using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.Calib
{
    /// <summary>
    ///=======================================================================================
        //完整使用流程：
        //1）buildRBFnetwork(400);//创建网络
        //2）for (...) { readinSample(realX,realY,deltaX,deltaY);}//循环传入数据
        //3）startLearning();//进行学习
        //4）calcCoordinate(inputX,inputY,outX,outY);//传入期望坐标即可得到经网络修正后的机台坐标
        //5）saveRBFprms();//参数储存到程序目录下
        //或已有参数文件时:
        //1）loadRBFprms();//读入固定文件名的参数文件
        //2）calcCoordinate(inputX,inputY,outX,outY);//传入期望坐标即可得到经网络修正后的机台坐标
    /// </summary>
    public class CalibNet
    {
        /// <summary>
        /// 创建RBF网络
        /// </summary>
        /// <param name="hiddenNum"></param>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "buildRBFnetwork", CallingConvention = CallingConvention.Cdecl)]
        public static extern int buildRBFnetwork(int hiddenNum);

        /// <summary>
        /// 进行网络运算
        /// invCalc为False，输入的期望位置，输出机械位置；
        /// invCalc为True，输入机械位置，输出期望位置
        /// </summary>
        /// <param name="inputX"></param>
        /// <param name="inputY"></param>
        /// <param name="outputX"></param>
        /// <param name="outputY"></param>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "calcCoordinate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int calcCoordinate(double inputX, double inputY, ref double outputX, ref double outputY, bool invCalc = false);

        /// <summary>
        /// 样本读入一条数据，分别为实际测量位置与差值
        /// </summary>
        /// <param name="realX"></param>
        /// <param name="realY"></param>
        /// <param name="outputDX"></param>
        /// <param name="outputDY"></param>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "readinSample", CallingConvention = CallingConvention.Cdecl)]
        public static extern int readinSample(double realX, double realY, double outputDX, double outputDY);

        /// <summary>
        /// 样本全部输入完成后以此指令开始学习
        /// </summary>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "startLearning", CallingConvention = CallingConvention.Cdecl)]
        public static extern int startLearning();

        /// <summary>
        /// 参数以csv文件形式储存到程序目录
        /// </summary>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "saveRBFprms", CallingConvention = CallingConvention.Cdecl)]
        public static extern int saveRBFprms();

        /// <summary>
        /// 以csv文件形式从程序目录读取参数，原构建的网络会被直接替换为读取参数得出的新网络
        /// </summary>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "loadRBFprms", CallingConvention = CallingConvention.Cdecl)]
        public static extern int loadRBFprms();

        /// <summary>
        /// 初始化所有网络参数（隐节点数保持不变）,重复学习会用到
        /// </summary>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "initRBFprms", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initRBFprms();

        /// <summary>
        /// 设置参数文件的存取路径
        /// </summary>
        /// <param name="pchar"></param>
        /// <param name="pathLength"></param>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "setRBFfilepath", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setRBFfilepath(byte[] pchar, int pathLength, bool invCalc = false);

        /// <summary>
        /// 向外传递整个网络的参数
        /// </summary>
        /// <param name="hddNo"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="sigma"></param>
        /// <param name="weightX"></param>
        /// <param name="weightY"></param>
        /// <param name="rePass"></param>
        /// <returns></returns>
        [DllImport("rbf2D_x86.dll", EntryPoint = "passRBFprms", CallingConvention = CallingConvention.Cdecl)]
        public static extern int passRBFprms(ref int hddNo, ref double posX, ref double posY, ref double sigma, ref double weightX, ref double weightY, bool rePass = false, bool invCalc = false);
    }
}
