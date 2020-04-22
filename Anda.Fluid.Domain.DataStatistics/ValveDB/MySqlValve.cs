using Anda.Fluid.Domain.Data;
using Anda.Fluid.Drive;
using Anda.Fluid.Drive.ValveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.DataStatistics.ValveDB
{
    public class MySqlValve
    {
       
        public static void InitValveTb()
        {
            using (afmdbEntities ctx = new afmdbEntities())
            {                
                //查询
                if (!queryValve(ctx, ValveType.Valve1))
                {
                    valve valve1 = new valve();
                    valve1.Name = ValveType.Valve1.ToString();
                    valve1.ValveType = (int)ValveType.Valve1;
                    valve1.MachineId = 1;
                    valve1.ValveSeries = (int)Machine.Instance.Valve1.Prm.ValveSeires;
                    ctx.valve.Add(valve1);
                }
                if (!queryValve(ctx, ValveType.Valve2))
                {
                    valve valve2 = new valve();
                    valve2.Name = ValveType.Valve2.ToString();
                    valve2.ValveType = (int)ValveType.Valve2;
                    valve2.MachineId = 1;
                    valve2.ValveSeries = (int)Machine.Instance.Valve2.Prm.ValveSeires;
                    ctx.valve.Add(valve2);
                }

            }

        }

        private static bool queryValve(afmdbEntities ctx, ValveType type)
        {
            var valves = from v in ctx.valve where v.ValveType == (int)type select v;
            valve vFind = valves.First();
            return vFind != null;
        }
        
    }
}
