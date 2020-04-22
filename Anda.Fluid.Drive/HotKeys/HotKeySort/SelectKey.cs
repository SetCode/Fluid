using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.HotKeying;
using System.Drawing;

namespace Anda.Fluid.Drive.HotKeys.HotKeySort
{
    public class SelectKey : IHotKeySortable
    {
        private List<Button> buttons;
        private int selectedIndex;

        private HotKey preButton = new HotKey(Keys.PageUp);
        private HotKey nextButton = new HotKey(Keys.PageDown);
        private HotKey okButton = new HotKey(Keys.Enter);
        private List<HotKey> keyList;
        public Color SelectColor { get; set; } = Color.Red;
        public Color NormalColor { get; set; } = Color.White;
        public bool Enable { get; set; }
        public List<HotKey> KeyList => this.keyList;

        public HotKeySortEnum SortName => HotKeySortEnum.SelectKey;

        public SelectKey()
        {
            this.Enable = true;
            this.keyList = new List<HotKey>();
            this.keyList.Add(this.preButton);
            this.keyList.Add(this.nextButton);
            this.keyList.Add(this.okButton);
        }

        /// <summary>
        /// 设置可供选择的按钮的顺序集合，默认第一个为选中状态,供外部调用
        /// </summary>
        /// <param name="buttonList"></param>
        public void SetButtons(List<Button> buttonList)
        {
            this.buttons = buttonList;
            this.selectedIndex = 0;

            this.buttons[0].Select();
            this.buttons[0].BackColor = this.SelectColor;
        }

        /// <summary>
        /// 清除相应选择键的按钮，供外部调用
        /// </summary>
        public void ClearButtons()
        {
            if (this.buttons == null)
                return;
            this.buttons.Clear();
        }

        public void OnKeyDownEvent(KeyEventArgs e)
        {
            if (!this.Enable)
                return;
            if (this.buttons == null || this.buttons.Count == 0 ) 
                return;
            switch (e.KeyData)
            {
                case Keys.PageUp:
                    this.SelectPreButton();
                    this.buttons[this.selectedIndex].Select();
                    break;
                case Keys.PageDown:
                    this.SelectNextButton();
                    this.buttons[this.selectedIndex].Select();
                    break;
                case Keys.Enter:
                    this.buttons[selectedIndex].FindForm().BeginInvoke(new Action(() =>
                    {
                        this.buttons[selectedIndex].PerformClick();
                        this.SelectNextButton();
                    }));
                    break;
            }
        }

        public void OnKeyUpEvent(KeyEventArgs e )
        {
            
        }
        private void SelectNextButton()
        {
            if (this.selectedIndex == this.buttons.Count - 1)
            {
                this.buttons[selectedIndex].BackColor = this.NormalColor;
                this.selectedIndex = 0;
                this.buttons[0].BackColor = this.SelectColor;
            }

            else if (0 <= this.selectedIndex && this.selectedIndex < this.buttons.Count - 1)
            {
                this.buttons[selectedIndex].BackColor = this.NormalColor;
                this.selectedIndex++;
                while (!this.buttons[selectedIndex].Enabled)
                {
                    this.selectedIndex++;
                }
                this.buttons[selectedIndex].BackColor = this.SelectColor;
            }
        }

        private void SelectPreButton()
        {
            if (this.selectedIndex == 0)
            {
                this.buttons[selectedIndex].BackColor = this.NormalColor;
                this.selectedIndex = this.buttons.Count - 1;
                this.buttons[this.selectedIndex].BackColor = this.SelectColor;
            }

            else if (0 < this.selectedIndex && this.selectedIndex <= this.buttons.Count)
            {
                this.buttons[selectedIndex].BackColor = this.NormalColor;
                this.selectedIndex--;
                while (!this.buttons[selectedIndex].Enabled)
                {
                    this.selectedIndex--;
                }
                this.buttons[selectedIndex].BackColor = this.SelectColor;
            }
        }

    }
    public interface ICanSelectButton
    {
        void SetSelectButtons();

    }
}

