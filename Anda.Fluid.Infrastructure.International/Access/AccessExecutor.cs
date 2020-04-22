using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.International.Access
{
    public class AccessExecutor
    {
       
        IAccessControllable accessContainer;
        private Control control;       
       
        public Action UpdateAccessInner;
        public AccessExecutor(IAccessControllable accessCtrl)
        {
            this.accessContainer = accessCtrl;
            this.control = accessCtrl.Control;         
           
        }


        public void LoadAccess()
        {
            this.setUpUserAccess();
            if (this.control == null)
            {
                return;
            }
            this.accessContainer.CurrContainerAccess = AccessControlMgr.Instance.FindContainerAccessByName(this.control.GetType().Name);
        }

        private void setUpUserAccess()
        {
            this.accessContainer.SetupUserAccessControl();
        }

        /// <summary>
        /// 根据权限更新窗体显示
        /// </summary>
        public void UpdateUIByAccess()
        {
            if (this.accessContainer.CurrContainerAccess==null)
            {
                this.LoadAccess();
            }
            this.checkContainerEnable(this.control);
            if (this.control.Enabled)
            {
                this.UpdateWindowAccess(this.control);
            }
            
        }
        private List<AccessMap> maps = new List<AccessMap>();
        private void UpdateWindowAccess(Control control)
        {
            if (control==null)
            {
                return;
            }
            if (this.accessContainer.CurrContainerAccess==null)
            {
                return;
            }
            maps.Clear();
            maps.AddRange(this.accessContainer.CurrContainerAccess.GetAllAccessMaps()) ;
            foreach (Control item in control.Controls)
            {
                this.checkControlEnable(item);
                if (item.HasChildren && !(item is IAccessControllable))
                {
                    this.UpdateWindowAccess(item);
                }
            }
            this.UpdateUIByUserAccessControl();

        }
        private void UpdateUIByUserAccessControl()
        {
            //if (this.accessControl.UserAccessControls == null)
            //{
            //    return;
            //}
            //foreach (AccessObj item in this.accessControl.UserAccessControls)
            //{
            //    if (item == null)
            //    {
            //        continue;
            //    }
            //    if (this.accessControl.WindAccess == null)
            //    {
            //        return;
            //    }
            //    ControlAccess access = this.accessControl.WindAccess.controlAccessList.Find(p => p.ControlName == item.Name);
            //    if (access != null)
            //    {
            //        if ((int)AccessControlMgr.Instance.CurrRole >= (int)access.RoleLevel)
            //        {
            //            item.Enabled = true;
            //        }
            //        else
            //        {
            //            item.Enabled = false;
            //        }
            //    }
            //}
            //UpdateAccessInner?.Invoke();
        }

        private void checkControlEnable(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (this.accessContainer.CurrContainerAccess == null)
            {
                return;
            }

            AccessMap access = this.maps.Find(p => p.ControlName == control.Name);
            if (access != null)
            {
                if ((int)AccessControlMgr.Instance.CurrRole >= (int)access.RoleLevel)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
        }

        private void checkContainerEnable(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (this.accessContainer.CurrContainerAccess == null)
            {
                return;
            }

            AccessMap access = this.maps.Find(p => p.ControlName == control.GetType().Name);
            if (access != null)
            {
                if ((int)AccessControlMgr.Instance.CurrRole >= (int)access.RoleLevel)
                {
                    control.Enabled = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
        }
    }
}
