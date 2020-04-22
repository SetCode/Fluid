using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.App.CustomDataUI
{
    public partial class CustomControlBase : UserControlEx
    {
        public CustomControlBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载客户数据或Mes参数到界面
        /// </summary>
        /// <param name="program"></param>
        public virtual void LoadParam(FluidProgram program)
        {

        }

        /// <summary>
        /// 保存客户数据或参数到程序对象
        /// </summary>
        /// <param name="program"></param>
        public virtual void SetParam(FluidProgram program)
        {

        }
    }
}
