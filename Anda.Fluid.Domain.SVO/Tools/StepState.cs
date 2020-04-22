using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Domain.SVO
{
    internal class StepState
    {
        private DateTime currentTime = DateTime.Now;
        private DateTime prevTime = DateTime.MinValue;

        public StepState(int step)
        {
            this.Step = step;
        }

        public int Step { get; private set; }

        public bool IsDoubleClicked { get; private set; }

        public bool IsDone { get; set; }

        public void UpdateClick(DateTime time)
        {
            this.currentTime = time;
            if(!this.IsDoubleClicked && this.currentTime - this.prevTime < TimeSpan.FromSeconds(1))
            {
                this.IsDoubleClicked = true;
                this.prevTime = this.currentTime;
            }
            else
            {
                this.IsDoubleClicked = false;
                this.prevTime = this.currentTime;
            }
        }

        public void IsChecked( )
        {
            ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
            switch (this.Step)
            {
                case 0:
                    checkedChange.Task0Checked();
                    break;
                case 1:
                    checkedChange.Task1Checked();
                    break;
                case 2:
                    checkedChange.Task2Checked();
                    break;
                case 3:
                    checkedChange.Task3Checked();
                    break;
                case 4:
                    checkedChange.Task4Checked();
                    break;
                case 5:
                    checkedChange.Task5Checked();
                    break;
                case 6:
                    checkedChange.Task6Checked();
                    break;
                case 7:
                    checkedChange.Task7Checked();                   
                    break;
                case 8:
                    checkedChange.Task8Checked();
                    break;
                case 9:
                    checkedChange.Task9Checked();                    
                    break;
                case 10:
                    checkedChange.Task10Checked();
                    checkedChange.Vavel1CompleteChecked();
                    break;
                case 11:
                    checkedChange.Task11Checked();
                    break;
                case 12:
                    checkedChange.Task12Checked();
                    break;
                case 13:
                    checkedChange.Task13Checked();                 
                    break;
                case 14:
                    checkedChange.Task14Checked();
                    checkedChange.Vavel2CompleteChecked();
                    break;
            }
        }
        public void IsUnChecked( )
        {
            ICheckedChangable checkedChange = SVOForm.Instance as ICheckedChangable;
            switch (this.Step)
            {
                case 0:
                    checkedChange.Task0UnChecked();
                    break;
                case 1:
                    checkedChange.Task1UnChecked();
                    break;
                case 2:
                    checkedChange.Task2UnChecked();
                    break;
                case 3:
                    checkedChange.Task3UnChecked();
                    break;
                case 4:
                    checkedChange.Task4UnChecked();
                    break;
                case 5:
                    checkedChange.Task5UnChecked();
                    break;
                case 6:
                    checkedChange.Task6UnChecked();
                    break;
                case 7:
                    checkedChange.Task7UnChecked();
                    break;
                case 8:
                    checkedChange.Task8UnChecked();
                    break;
                case 9:
                    checkedChange.Task9UnChecked();
                    break;
                case 10:
                    checkedChange.Task10UnChecked();
                    break;
                case 11:
                    checkedChange.Task11UnChecked();
                    break;
                case 12:
                    checkedChange.Task12UnChecked();
                    break;
                case 13:
                    checkedChange.Task13UnChecked();                   
                    break;
                case 14:
                    checkedChange.Task14UnChecked();
                    checkedChange.Vavel2CompleteUnChecked();
                    break;
            }
        }

    }
}
