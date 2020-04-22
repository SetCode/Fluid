using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.DomainBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.App.LoadTrajectory
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ComponentEx:EntityBase<int>
    {
        public ComponentEx(int key):base(key)
        {

        }
        [JsonProperty]
        public Component component = new Component();

    }
    [JsonObject(MemberSerialization.OptIn)]
    public class Component 
    {        
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public double Width;
        
        [JsonProperty]
        public double Height;

        public Technology Tech=Technology.Adh;

        [JsonProperty]
        /// <summary>
        /// 红胶点
        /// </summary>
        /// 
        public List<GlueDot> AdhPoints = new List<GlueDot>();
        [JsonProperty]
        /// <summary>
        /// 锡膏点
        /// </summary>
        public List<GlueDot> SldPoints = new List<GlueDot>();

        public List<GlueDot> GetPoints(Technology tech)
        {
            return tech == Technology.Adh ? AdhPoints : SldPoints;
        }
        public void AddPoint(GlueDot dot,Technology tech)
        {
            List<GlueDot> points = tech == Technology.Adh ? AdhPoints : SldPoints;
            points.Add(dot);
        }

        public void DelDot(int index, Technology tech)
        {
            List<GlueDot> points = tech == Technology.Adh ? AdhPoints : SldPoints;
            foreach (GlueDot item in points)
            {
                if (item.index == index)
                {
                    points.Remove(item);
                }
            }
        }

        public void DelDot(GlueDot dot, Technology tech)
        {
            List<GlueDot> points = tech == Technology.Adh ? AdhPoints : SldPoints;
            if (points.Contains(dot))
            {
                points.Remove(dot);
            }           
        }

        public GlueDot FindDot(int index, Technology tech)
        {
            List<GlueDot> points = tech == Technology.Adh ? AdhPoints : SldPoints;
            foreach (GlueDot item in points)
            {
                if (item.index == index)
                {
                    return item;
                }
            }
            return null;
        }

        public Component DepCopy()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return JsonConvert.DeserializeObject<Component>(json);
        }
    }


}
