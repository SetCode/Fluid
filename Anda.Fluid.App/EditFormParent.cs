using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.App.EditCmdLineForms;
using Anda.Fluid.Domain.FluProgram;

namespace Anda.Fluid.App
{
    /// <summary>
    /// Description:  父级轨迹编辑窗口，常用轨迹命令在窗口侧边栏，方便新建轨迹
    /// Author:       Xuxixiao
    /// Date:         2019/08/29
    /// </summary>
    public partial class EditFormParent : Form
    {
        /// <summary>
        /// 窗口显示标志，一次只允许显示一个此类窗口
        /// </summary>
        public static bool IsShown = false;
        /// <summary>
        /// 当前显示窗口的引用
        /// </summary>
        public static EditFormParent Current = null;
        /// <summary>
        /// 当前编辑轨迹所在的Pattern
        /// </summary>
        private Pattern pattern;
        /// <summary>
        /// 轨迹命令按钮集合
        /// </summary>
        private List<Button> buttons;
        public EditFormParent(Pattern pattern)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.pattern = pattern;
            this.TopLevel = true;

            this.Load += EditFormParent_Load;
            this.FormClosed += EditFormParent_FormClosed;

            this.buttons = new List<Button>();
            this.buttons.Add(this.btnDot);
            this.buttons.Add(this.btnLine);
            this.buttons.Add(this.btnPolyLine);
            this.buttons.Add(this.btnArc);
            this.buttons.Add(this.btnCircle);
            this.buttons.Add(this.btnSnake);
            this.buttons.Add(this.btnArray);
            this.buttons.Add(this.btnMultiPatternArray);
            this.buttons.Add(this.btnNormalTimer);
            this.buttons.Add(this.btnFinishShot);

            this.ChangeForm(new EditDotForm1(pattern));
            this.selectButton(this.btnDot);

            Current = this;
        }

        private void EditFormParent_Load(object sender, EventArgs e)
        {
            IsShown = true;
        }

        private void EditFormParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsShown = false;
        }

        public void ChangeForm(Form form)
        {
            if(form == null)
            {
                return;
            }
            this.panel1.Controls.Clear();
            form.TopLevel = false;
            form.Parent = this.panel1;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Show();
        }

        private void selectButton(Button btn)
        {
            foreach (var item in buttons)
            {
                item.BackColor = Color.White;
            }
            btn.BackColor = Color.LightSkyBlue;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditDotForm1(pattern));
            this.selectButton(this.btnDot);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditSingleLineForm(this.pattern));
            this.selectButton(this.btnLine);
        }

        private void btnPolyLine_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditPolyLineForm(this.pattern));
            this.selectButton(this.btnPolyLine);
        }

        private void btnArc_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditArcForm1(this.pattern));
            this.selectButton(this.btnArc);
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditCircleForm1(this.pattern));
            this.selectButton(this.btnCircle);
        }

        private void btnSnake_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditSnakeLineForm1(this.pattern));
            this.selectButton(this.btnSnake);
        }

        private void btnArray_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditStepAndRepeatForm3(this.pattern));
            this.selectButton(this.btnArray);
        }

        private void btnMultiPatternArray_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditStepAndRepeatForm1(this.pattern));
            this.selectButton(this.btnMultiPatternArray);
        }

        private void btnNormalTimer_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditNormalTimerForm());
            this.selectButton(this.btnNormalTimer);
        }

        private void btnFinishShot_Click(object sender, EventArgs e)
        {
            this.ChangeForm(new EditFinishShotForm1(this.pattern));
            this.selectButton(this.btnFinishShot);
        }
    }
}
