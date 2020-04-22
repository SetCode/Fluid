using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure
{
    public enum UnitType
    {
        Mm,
        Inch,
        Micron
    }

    public class Unit
    {
        private double mm = 0;

        public static UnitType Type = UnitType.Mm;

        public Unit(UnitType type, double value)
        {
            this.SetValue(type, value);    
        }

        public Unit(double value)
            : this(UnitType.Mm, value)
        {

        }

        public Unit()
            : this(UnitType.Mm, 0)
        {

        }

        public double Value
        {
            get
            {
                return this.GetValue(Type);
            }
            set
            {
                this.SetValue(Type, value);
            }
        }

        public double Mm => this.mm;

        public double Inch => Math.Round(this.mm / 25.4, 3);

        public int Micron => (int)this.mm * 1000;

        private void SetValue(UnitType type, double value)
        {
            switch (type)
            {
                case UnitType.Mm:
                    this.mm = Math.Round(value, 3);
                    break;
                case UnitType.Inch:
                    this.mm = Math.Round(25.4 * value, 3);
                    break;
                case UnitType.Micron:
                    this.mm = Math.Round(value / 1000, 3);
                    break;
            }
        }

        private double GetValue(UnitType type)
        {
            double value = 0;
            switch (type)
            {
                case UnitType.Mm:
                    value = this.Mm;
                    break;
                case UnitType.Inch:
                    value = this.Inch;
                    break;
                case UnitType.Micron:
                    value = this.Micron;
                    break;
            }
            return value;
        }

    }
}
