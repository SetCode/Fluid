using System;
using System.Text;

namespace Anda.Fluid.Domain.FluProgram.Grammar
{
    [Serializable]
    public class CircleCmdLine : ArcCmdLine
    {
        public CircleCmdLine()
        {
            Start.X = 2f;
            Start.Y = 1f;
            Middle.X = 0f;
            Middle.Y = 1f;
            End.X = 2f;
            End.Y = 1f;
            Center.X = 1f;
            Center.Y = 1f;
            Degree = 360f;
        }

        public override object Clone()
        {
            return base.Clone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!Enabled)
            {
                sb.Append("DISABLE : ");
            }
            sb.Append("CIRCLE : ");

            if (this.TrackNumber != null)
            {
                sb.Append(this.TrackNumber + ",");
            }
            sb.Append((int)LineStyle + 1);

            if (IsWeightControl)
            {
                sb.Append(", ").Append(Weight.ToString("0.000"));
            }
            sb.Append(", ")
                .Append(MachineRel(Start)).Append(", ")
                .Append(MachineRel(Middle)).Append(", ")
                .Append(MachineRel(End)).Append(", ")
                .Append(MachineRel(Center));
            return sb.ToString();
        }

      
    }
}