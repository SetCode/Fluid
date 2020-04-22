using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;
using Anda.Fluid.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.ASV
{
    public sealed class InspectionMgr 
    {
        private static readonly InspectionMgr instance = new InspectionMgr();
        private InspectionMgr() { }
        public static InspectionMgr Instance => instance;

        public List<Inspection> List { get; private set; } = new List<Inspection>();

        public Inspection FindBy(int key)
        {
            foreach (var item in this.List)
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
            return default(Inspection);
        }

        public bool Save()
        {
            string path = SettingsPath.PathBusiness+"\\"+typeof(InspectionMgr).Name + ".xml";
            return XmlUtils.Serialize<List<Inspection>>(path, List);
        }

        public bool Load()
        {
            string path = SettingsPath.PathBusiness + "\\" + typeof(InspectionMgr).Name + ".xml";
            if(!File.Exists(path))
            {
                return false;
            }
            List<Inspection> list = XmlUtils.Derialize<List<Inspection>>(path);
            if(list == null)
            {
                return false;
            }
            this.List = list;
            return true;
        }
    }
}
