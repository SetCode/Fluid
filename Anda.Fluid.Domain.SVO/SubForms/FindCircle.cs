using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.Common;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Domain.SVO.SubForms
{
    internal partial class FindCircle : UserControlEx
    {
        public FindCircle()
        {
            InitializeComponent();
            this.ReadLanguageResources();
        }
        /// <summary>
        /// 单点直接找圆心
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static PointD GetCenter(PointD center)
        {
            return center;
        }
        /// <summary>
        /// 隐藏所有单选按钮，同时显示提示文本框
        /// </summary>
        /// <param name="isHide">true 为隐藏单选显示文本，false 为显示单选隐藏文本</param>
        public void HideAllrdoAndShowMsglbl(bool isHide)
        {
            if (isHide)
            {
                this.rdoFindCircleOnePoint.Hide();
                this.rdoTeachByNeedle.Hide();
                this.rdoFindCircleThreePoint.Hide();
                this.lblMessage.Hide();
            }
            else
            {
                this.rdoFindCircleThreePoint.Show();
                this.rdoFindCircleOnePoint.Show();
                this.rdoTeachByNeedle.Show();
                this.lblMessage.Hide();
            }
        }
       
    }
}
