using Anda.Fluid.Infrastructure;
using Anda.Fluid.Infrastructure.DomainBase;

namespace Anda.Fluid.Drive.GlueManage
{
    /// <summary>
    /// 胶水管控参数管理类
    /// </summary>
    public class GlueManagePrmMgr : EntityMgr<GlueManagePrm,int>
    {
        private readonly static GlueManagePrmMgr instance = new GlueManagePrmMgr(SettingsPath.PathBusiness);
        private GlueManagePrmMgr(string path) : base(path)
        {
            this.Add(new GlueManagePrm(1));
            this.Add(new GlueManagePrm(2));
        }

        public static GlueManagePrmMgr Instance => instance;
    }
}
