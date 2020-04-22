using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.UI
{
    public class FormMgr
    {
        private static Dictionary<string, Form> forms = new Dictionary<string, Form>();

        public static T GetForm<T>()
            where T : Form
        {
            T t = default(T);
            string formType = typeof(T).Name;
            if(!forms.ContainsKey(formType))
            {
                t = Activator.CreateInstance(typeof(T)) as T;
                forms.Add(formType, t);
            }
            else
            {
                if (forms[formType].IsDisposed)
                {
                    forms[formType] = Activator.CreateInstance(typeof(T)) as T;
                }
                t = forms[formType] as T;
            }

            return t;
        }     
        
        public static void Show<T>(IWin32Window owner)
            where T : Form
        {
            if(GetForm<T>().Visible)
            {
                return;
            }
            if (owner != null)
            {
            GetForm<T>().Show(owner);
        }   
            else
            {
                GetForm<T>().Show();
            }
            
        }   
    }
}
