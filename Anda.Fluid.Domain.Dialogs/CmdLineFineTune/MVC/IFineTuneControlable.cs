using Anda.Fluid.Domain.FluProgram.Grammar;
using Anda.Fluid.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.Dialogs.CmdLineFineTune.MVC
{
    public interface IFineTuneControlable
    {

        /// <summary>
        /// 自动向前跟踪
        /// </summary>
        bool AutoTrack { get; set; }

        /// <summary>
        /// 寸动移动
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="dis"></param>
        void Move(DirectionEnum direction, double dis);

        /// <summary>
        /// 对指定轨迹中指定点进行示教
        /// </summary>
        /// <param name="cmdLineNo"></param>
        /// <param name="pointNo"></param>
        void TeachPoint(PointD point);

        /// <summary>
        /// 移到到指定轨迹中的指定点
        /// </summary>
        /// <param name="cmdLineNo"></param>
        /// <param name="pointNo"></param>
        void MoveToPoint(int cmdLineNo, int pointNo);

        /// <summary>
        /// 向上跟踪，返回true成功
        /// </summary>
        bool PreTrack();

        /// <summary>
        /// 向下跟踪，返回true成功
        /// </summary>
        bool NextTrack();

        /// <summary>
        /// 设置指定轨迹中的指定点是否为跳过点
        /// </summary>
        /// <param name="cmdLineNo"></param>
        /// <param name="pointNo"></param>
        /// <param name="skip"></param>
        void SetSkip(int cmdLineNo, int pointNo,bool skip);

        /// <summary>
        /// 设置轨迹为需要处理True和不需要处理false
        /// </summary>
        /// <param name="cmdLineType"></param>
        /// <param name="enable"></param>
        void SetEnable(List<Tuple<CmdLineType,bool>> list);

        /// <summary>
        /// 设置模型
        /// </summary>
        /// <param name="model"></param>
        void SetModel(IFineTuneModelable model);

    }

    public enum DirectionEnum
    {
        Up,
        Down,
        Left,
        Right,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight
    }


}
