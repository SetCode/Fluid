using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.AngleHeightPoseCorrect.TestType
{

    internal interface ITestCtlable
    {
        /// <summary>
        /// 设置控件（阶段二传入倾斜姿态、角度/阶段三传入倾斜姿态、打胶Z轴位置、角度、阀组相机偏差、侧高点位置/
        /// 阶段四传入倾斜姿态、角度、阀组相机偏差、标准高度、距板高度）
        /// </summary>
        /// <param name="objs"></param>
        void Setup(object[] objs);

        /// <summary>
        /// 重置控件
        /// </summary>
        void Reset();

        /// <summary>
        /// 查看该阶段校正是否完成
        /// </summary>
        /// <returns></returns>
        bool IsDone();
    }
}
