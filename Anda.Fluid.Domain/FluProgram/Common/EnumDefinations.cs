namespace Anda.Fluid.Domain.FluProgram.Common
{
    public enum CheckThm
    {
        GrayScale,
        ModelFind
    }

    public enum NozzleCheckStyle
    {
        Valve1,
        Valve2,
        Both
    }

    public enum DotStyle : int
    {
        TYPE_1 = 0,
        TYPE_2,
        TYPE_3,
        TYPE_4,
        TYPE_5,
        TYPE_6,
        TYPE_7,
        TYPE_8,
        TYPE_9,
        TYPE_10,
    }

    public enum LineMethod
    {
        Multi,
        Single,
        Poly
    }

    public enum LineStyle : int
    {
        TYPE_1 = 0,
        TYPE_2,
        TYPE_3,
        TYPE_4,
        TYPE_5,
        TYPE_6,
        TYPE_7,
        TYPE_8,
        TYPE_9,
        TYPE_10,
    }

    /// <summary>
    /// 弧/圆命令中，指示以何种方式确定轨迹参数
    /// </summary>
    public enum ArcMethod
    {
        SME,
        CENTER
    }

    /// <summary>
    /// 移动到指定位置的命令中，指示是以相机还是喷嘴还是激光测高的中心点为准
    /// </summary>
    public enum MoveType
    {
        CAMERA,
        LASER,
        NEEDLE1,
        NEEDLE2,
    }
    /// <summary>
    /// 银宝山特殊指令中每段的类型
    /// </summary>
    public enum SymbolType
    {
        Line = 0,
        Arc
    }
    /// <summary>
    /// 语义指令类型枚举
    /// </summary>
    public enum CommandEnum
    {
        // 先仅添加一个，后续需要使用再添加
        TimerCmd = 0
    }
}
