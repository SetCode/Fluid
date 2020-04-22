using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.Cpk;
using Anda.Fluid.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Domain.Dialogs.Cpks
{ 
    public class CPKMgr
    {
        private readonly static CPKMgr intance = new CPKMgr();
        private CPKMgr()
        { }

        public static CPKMgr Instance => intance;

        public CpkPrm Prm { get; set; }
        public CpkPrm PrmBackUp { get; set; }
        private Dictionary<string, ICPKable> cpkDic = new Dictionary<string, ICPKable>();
        private int count;
        string path = SettingsPath.PathBusiness + "\\" + typeof(CpkPrm).Name;
        public void Initial()
        {
            WorkBook workBook =new  WorkBook();

            workBook.AddSheet(SheetName.Valve1Weight.ToString());
            workBook.AddSheet(SheetName.Valve2Weight.ToString());
            workBook.AddSheet(SheetName.X轴.ToString());
            workBook.AddSheet(SheetName.Y轴.ToString());
            workBook.AddSheet(SheetName.XY轴联动.ToString());
            workBook.AddSheet(SheetName.Z轴.ToString());
            
            ICPKable cpk;
             cpk = new Valve1WeightCPK(workBook.FindSheetByName(SheetName.Valve1Weight.ToString()), this.Prm);
            this.cpkDicAdd(typeof(Valve1WeightCPK).Name, cpk);

            cpk = new Valve2WeightCPK(workBook.FindSheetByName(SheetName.Valve2Weight.ToString()), this.Prm);
             this.cpkDicAdd(typeof(Valve2WeightCPK).Name, cpk);

            cpk = new AxisXCPK(workBook.FindSheetByName(SheetName.X轴.ToString()), this.Prm);
            this.cpkDicAdd(typeof(AxisXCPK).Name, cpk);

            cpk = new AxisYCPK(workBook.FindSheetByName(SheetName.Y轴.ToString()), this.Prm);
            this.cpkDicAdd(typeof(AxisYCPK).Name, cpk);

            cpk = new AxisXYCPK(workBook.FindSheetByName(SheetName.XY轴联动.ToString()), this.Prm);
            this.cpkDicAdd(typeof(AxisXYCPK).Name, cpk);

            cpk = new AxisZCPK(workBook.FindSheetByName(SheetName.Z轴.ToString()), this.Prm);
            this.cpkDicAdd(typeof(AxisZCPK).Name, cpk);           

        }

        #region 

        #endregion 

        private void cpkDicAdd(string key,ICPKable cpk)
        {
            if (cpkDic.ContainsKey(key))
            {
                return;
            }
            cpkDic.Add(key,cpk);
            
        }

        public void ExecuteAll()
        {
            foreach (ICPKable item in cpkDic.Values)
            {
                item.Execute(this.Prm);
            }
        }
        public void ExecuteOne(string cpkName)
        {
            
            if (cpkDic.ContainsKey(cpkName))
            {
                cpkDic[cpkName].Execute(this.Prm);
            }
        }
        public void StopOne(string cpkName)
        {
            if (cpkDic.ContainsKey(cpkName))
            {
                cpkDic[cpkName].Stop();
            }
        }

        public ICPKable FindByName(string cpkName)
        {
            if (!cpkDic.ContainsKey(cpkName))
            {
                return null;
            }
            return cpkDic[cpkName];
        }
        public bool LoadPrm()
        {
            Prm = JsonUtil.Deserialize<CpkPrm>(path);
            PrmBackUp = JsonUtil.Deserialize<CpkPrm>(path);
            return Prm != null;
        }

        public bool SavePrm()
        {
            return JsonUtil.Serialize<CpkPrm>(path, this.Prm);
             
        }
        /// <summary>
        /// 恢复默认参数
        /// </summary>
        public void ReestToDefault()
        {
            SettingUtil.ResetToDefault<CpkPrm>(this.Prm);
        }
    }
}
