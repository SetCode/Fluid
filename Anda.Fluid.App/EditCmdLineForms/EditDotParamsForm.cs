using Anda.Fluid.Domain.FluProgram.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.Settings;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditDotParamsForm : FormEx
    {
        private IReadOnlyList<DotParam> dotParamList;
        private IReadOnlyList<DotParam> dotParamListBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditDotParamsForm()
        {
            InitializeComponent();           
        }
        public EditDotParamsForm(IReadOnlyList<DotParam> dotParamList)
        {
            InitializeComponent();
            this.Init();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;


            if (dotParamList == null || dotParamList.Count != 10)
            {
                //MessageBox.Show("Dot params number is not 10.");
                MessageBox.Show("胶点参数不可以是10.");
                Close();
                return;
            }
            this.dotParamList = dotParamList;
            // type 1:
            // pre dispense
            tbType1SettlingTime.Text = dotParamList[0].SettlingTime.ToString("0.000");
            tbType1DownSpeed.Text = dotParamList[0].DownSpeed.ToString("0.000");
            tbType1DownAccel.Text = dotParamList[0].DownAccel.ToString("0.000");
            tbType1Notes.Text = dotParamList[0].Notes;
            // during dispense
            tbType1DispenseGap.Text = dotParamList[0].DispenseGap.ToString("0.000");
            tbType1NumShots.Text = dotParamList[0].NumShots.ToString();
            tbType1MultiShotDelta.Text = dotParamList[0].MultiShotDelta.ToString("0.000");
            tbType1ValveOnTime.Text = dotParamList[0].ValveOnTime.ToString("0.000");
            // post dispense
            tbType1DwellTime.Text = dotParamList[0].DwellTime.ToString("0.000");
            tbType1RetractDis.Text = dotParamList[0].RetractDistance.ToString("0.000");
            tbType1RetractSpeed.Text = dotParamList[0].RetractSpeed.ToString("0.000");
            tbType1RetractAccel.Text = dotParamList[0].RetractAccel.ToString("0.000");
            tbType1SuckBackTime.Text = dotParamList[0].SuckBackTime.ToString("0.000");
            // type 2:
            // pre dispense
            tbType2SettlingTime.Text = dotParamList[1].SettlingTime.ToString("0.000");
            tbType2DownSpeed.Text = dotParamList[1].DownSpeed.ToString("0.000");
            tbType2DownAccel.Text = dotParamList[1].DownAccel.ToString("0.000");
            tbType2Notes.Text = dotParamList[1].Notes;
            // during dispense
            tbType2DispenseGap.Text = dotParamList[1].DispenseGap.ToString("0.000");
            tbType2NumShots.Text = dotParamList[1].NumShots.ToString();
            tbType2MultiShotDelta.Text = dotParamList[1].MultiShotDelta.ToString("0.000");
            tbType2ValveOnTime.Text = dotParamList[1].ValveOnTime.ToString("0.000");
            // post dispense
            tbType2DwellTime.Text = dotParamList[1].DwellTime.ToString("0.000");
            tbType2RetractDis.Text = dotParamList[1].RetractDistance.ToString("0.000");
            tbType2RetractSpeed.Text = dotParamList[1].RetractSpeed.ToString("0.000");
            tbType2RetractAccel.Text = dotParamList[1].RetractAccel.ToString("0.000");
            tbType2SuckBackTime.Text = dotParamList[1].SuckBackTime.ToString("0.000");
            // type 3:
            // pre dispense
            tbType3SettlingTime.Text = dotParamList[2].SettlingTime.ToString("0.000");
            tbType3DownSpeed.Text = dotParamList[2].DownSpeed.ToString("0.000");
            tbType3DownAccel.Text = dotParamList[2].DownAccel.ToString("0.000");
            tbType3Notes.Text = dotParamList[2].Notes;
            // during dispense
            tbType3DispenseGap.Text = dotParamList[2].DispenseGap.ToString("0.000");
            tbType3NumShots.Text = dotParamList[2].NumShots.ToString();
            tbType3MultiShotDelta.Text = dotParamList[2].MultiShotDelta.ToString("0.000");
            tbType3ValveOnTime.Text = dotParamList[2].ValveOnTime.ToString("0.000");
            // post dispense
            tbType3DwellTime.Text = dotParamList[2].DwellTime.ToString("0.000");
            tbType3RetractDis.Text = dotParamList[2].RetractDistance.ToString("0.000");
            tbType3RetractSpeed.Text = dotParamList[2].RetractSpeed.ToString("0.000");
            tbType3RetractAccel.Text = dotParamList[2].RetractAccel.ToString("0.000");
            tbType3SuckBackTime.Text = dotParamList[2].SuckBackTime.ToString("0.000");
            // type 4:
            // pre dispense
            tbType4SettlingTime.Text = dotParamList[3].SettlingTime.ToString("0.000");
            tbType4DownSpeed.Text = dotParamList[3].DownSpeed.ToString("0.000");
            tbType4DownAccel.Text = dotParamList[3].DownAccel.ToString("0.000");
            tbType4Notes.Text = dotParamList[3].Notes;
            // during dispense
            tbType4DispenseGap.Text = dotParamList[3].DispenseGap.ToString("0.000");
            tbType4NumShots.Text = dotParamList[3].NumShots.ToString();
            tbType4MultiShotDelta.Text = dotParamList[3].MultiShotDelta.ToString("0.000");
            tbType4ValveOnTime.Text = dotParamList[3].ValveOnTime.ToString("0.000");
            // post dispense
            tbType4DwellTime.Text = dotParamList[3].DwellTime.ToString("0.000");
            tbType4RetractDis.Text = dotParamList[3].RetractDistance.ToString("0.000");
            tbType4RetractSpeed.Text = dotParamList[3].RetractSpeed.ToString("0.000");
            tbType4RetractAccel.Text = dotParamList[3].RetractAccel.ToString("0.000");
            tbType4SuckBackTime.Text = dotParamList[3].SuckBackTime.ToString("0.000");
            // type 5:
            // pre dispense
            tbType5SettlingTime.Text = dotParamList[4].SettlingTime.ToString("0.000");
            tbType5DownSpeed.Text = dotParamList[4].DownSpeed.ToString("0.000");
            tbType5DownAccel.Text = dotParamList[4].DownAccel.ToString("0.000");
            tbType5Notes.Text = dotParamList[4].Notes;
            // during dispense
            tbType5DispenseGap.Text = dotParamList[4].DispenseGap.ToString("0.000");
            tbType5NumShots.Text = dotParamList[4].NumShots.ToString();
            tbType5MultiShotDelta.Text = dotParamList[4].MultiShotDelta.ToString("0.000");
            tbType5ValveOnTime.Text = dotParamList[4].ValveOnTime.ToString("0.000");
            // post dispense
            tbType5DwellTime.Text = dotParamList[4].DwellTime.ToString("0.000");
            tbType5RetractDis.Text = dotParamList[4].RetractDistance.ToString("0.000");
            tbType5RetractSpeed.Text = dotParamList[4].RetractSpeed.ToString("0.000");
            tbType5RetractAccel.Text = dotParamList[4].RetractAccel.ToString("0.000");
            tbType5SuckBackTime.Text = dotParamList[4].SuckBackTime.ToString("0.000");
            // type 6:
            // pre dispense
            tbType6SettlingTime.Text = dotParamList[5].SettlingTime.ToString("0.000");
            tbType6DownSpeed.Text = dotParamList[5].DownSpeed.ToString("0.000");
            tbType6DownAccel.Text = dotParamList[5].DownAccel.ToString("0.000");
            tbType6Notes.Text = dotParamList[5].Notes;
            // during dispense
            tbType6DispenseGap.Text = dotParamList[5].DispenseGap.ToString("0.000");
            tbType6NumShots.Text = dotParamList[5].NumShots.ToString();
            tbType6MultiShotDelta.Text = dotParamList[5].MultiShotDelta.ToString("0.000");
            tbType6ValveOnTime.Text = dotParamList[5].ValveOnTime.ToString("0.000");
            // post dispense
            tbType6DwellTime.Text = dotParamList[5].DwellTime.ToString("0.000");
            tbType6RetractDis.Text = dotParamList[5].RetractDistance.ToString("0.000");
            tbType6RetractSpeed.Text = dotParamList[5].RetractSpeed.ToString("0.000");
            tbType6RetractAccel.Text = dotParamList[5].RetractAccel.ToString("0.000");
            tbType6SuckBackTime.Text = dotParamList[5].SuckBackTime.ToString("0.000");
            // type 7:
            // pre dispense
            tbType7SettlingTime.Text = dotParamList[6].SettlingTime.ToString("0.000");
            tbType7DownSpeed.Text = dotParamList[6].DownSpeed.ToString("0.000");
            tbType7DownAccel.Text = dotParamList[6].DownAccel.ToString("0.000");
            tbType7Notes.Text = dotParamList[6].Notes;
            // during dispense
            tbType7DispenseGap.Text = dotParamList[6].DispenseGap.ToString("0.000");
            tbType7NumShots.Text = dotParamList[6].NumShots.ToString();
            tbType7MultiShotDelta.Text = dotParamList[6].MultiShotDelta.ToString("0.000");
            tbType7ValveOnTime.Text = dotParamList[6].ValveOnTime.ToString("0.000");
            // post dispense
            tbType7DwellTime.Text = dotParamList[6].DwellTime.ToString("0.000");
            tbType7RetractDis.Text = dotParamList[6].RetractDistance.ToString("0.000");
            tbType7RetractSpeed.Text = dotParamList[6].RetractSpeed.ToString("0.000");
            tbType7RetractAccel.Text = dotParamList[6].RetractAccel.ToString("0.000");
            tbType7SuckBackTime.Text = dotParamList[6].SuckBackTime.ToString("0.000");
            // type 8:
            // pre dispense
            tbType8SettlingTime.Text = dotParamList[7].SettlingTime.ToString("0.000");
            tbType8DownSpeed.Text = dotParamList[7].DownSpeed.ToString("0.000");
            tbType8DownAccel.Text = dotParamList[7].DownAccel.ToString("0.000");
            tbType8Notes.Text = dotParamList[7].Notes;
            // during dispense
            tbType8DispenseGap.Text = dotParamList[7].DispenseGap.ToString("0.000");
            tbType8NumShots.Text = dotParamList[7].NumShots.ToString();
            tbType8MultiShotDelta.Text = dotParamList[7].MultiShotDelta.ToString("0.000");
            tbType8ValveOnTime.Text = dotParamList[7].ValveOnTime.ToString("0.000");
            // post dispense
            tbType8DwellTime.Text = dotParamList[7].DwellTime.ToString("0.000");
            tbType8RetractDis.Text = dotParamList[7].RetractDistance.ToString("0.000");
            tbType8RetractSpeed.Text = dotParamList[7].RetractSpeed.ToString("0.000");
            tbType8RetractAccel.Text = dotParamList[7].RetractAccel.ToString("0.000");
            tbType8SuckBackTime.Text = dotParamList[7].SuckBackTime.ToString("0.000");
            // type 9:
            // pre dispense
            tbType9SettlingTime.Text = dotParamList[8].SettlingTime.ToString("0.000");
            tbType9DownSpeed.Text = dotParamList[8].DownSpeed.ToString("0.000");
            tbType9DownAccel.Text = dotParamList[8].DownAccel.ToString("0.000");
            tbType9Notes.Text = dotParamList[8].Notes;
            // during dispense
            tbType9DispenseGap.Text = dotParamList[8].DispenseGap.ToString("0.000");
            tbType9NumShots.Text = dotParamList[8].NumShots.ToString();
            tbType9MultiShotDelta.Text = dotParamList[8].MultiShotDelta.ToString("0.000");
            tbType9ValveOnTime.Text = dotParamList[8].ValveOnTime.ToString("0.000");
            // post dispense
            tbType9DwellTime.Text = dotParamList[8].DwellTime.ToString("0.000");
            tbType9RetractDis.Text = dotParamList[8].RetractDistance.ToString("0.000");
            tbType9RetractSpeed.Text = dotParamList[8].RetractSpeed.ToString("0.000");
            tbType9RetractAccel.Text = dotParamList[8].RetractAccel.ToString("0.000");
            tbType9SuckBackTime.Text = dotParamList[8].SuckBackTime.ToString("0.000");
            // type 10:
            // pre dispense
            tbType10SettlingTime.Text = dotParamList[9].SettlingTime.ToString("0.000");
            tbType10DownSpeed.Text = dotParamList[9].DownSpeed.ToString("0.000");
            tbType10DownAccel.Text = dotParamList[9].DownAccel.ToString("0.000");
            tbType10Notes.Text = dotParamList[9].Notes;
            // during dispense
            tbType10DispenseGap.Text = dotParamList[9].DispenseGap.ToString("0.000");
            tbType10NumShots.Text = dotParamList[9].NumShots.ToString();
            tbType10MultiShotDelta.Text = dotParamList[9].MultiShotDelta.ToString("0.000");
            tbType10ValveOnTime.Text = dotParamList[9].ValveOnTime.ToString("0.000");
            // post dispense
            tbType10DwellTime.Text = dotParamList[9].DwellTime.ToString("0.000");
            tbType10RetractDis.Text = dotParamList[9].RetractDistance.ToString("0.000");
            tbType10RetractSpeed.Text = dotParamList[9].RetractSpeed.ToString("0.000");
            tbType10RetractAccel.Text = dotParamList[9].RetractAccel.ToString("0.000");
            tbType10SuckBackTime.Text = dotParamList[9].SuckBackTime.ToString("0.000");
            if (FluidProgram.Current != null)
            {
                this.dotParamListBackUp = ((ProgramSettings)FluidProgram.Current.ProgramSettings.Clone()).DotParamList;

            }
            this.ReadLanguageResources();
        }

        private void Init()
        {
            if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.喷射阀)
            {

                this.lblValveOnTime.Hide();
                this.lblValveOnTimeUnit.Hide();
                this.tbType1ValveOnTime.Hide();
                this.tbType2ValveOnTime.Hide();
                this.tbType3ValveOnTime.Hide();
                this.tbType4ValveOnTime.Hide();
                this.tbType5ValveOnTime.Hide();
                this.tbType6ValveOnTime.Hide();
                this.tbType7ValveOnTime.Hide();
                this.tbType8ValveOnTime.Hide();
                this.tbType9ValveOnTime.Hide();
                this.tbType10ValveOnTime.Hide();


                this.lblSuckBackTime.Hide();
                this.lblSuckBackTimeUnit.Hide();
                this.tbType1SuckBackTime.Hide();
                this.tbType2SuckBackTime.Hide();
                this.tbType3SuckBackTime.Hide();
                this.tbType4SuckBackTime.Hide();
                this.tbType5SuckBackTime.Hide();
                this.tbType6SuckBackTime.Hide();
                this.tbType7SuckBackTime.Hide();
                this.tbType8SuckBackTime.Hide();
                this.tbType9SuckBackTime.Hide();
                this.tbType10SuckBackTime.Hide();
            }
        }

        private void tbType1SettlingTime_TextChanged(object sender, System.EventArgs e)
        {

            dotParamList[0].SettlingTime = tbType1SettlingTime.Value;
        }

        private void tbType2SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].SettlingTime = tbType2SettlingTime.Value;
        }

        private void tbType3SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].SettlingTime = tbType3SettlingTime.Value;
        }

        private void tbType4SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].SettlingTime = tbType4SettlingTime.Value;
        }

        private void tbType5SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].SettlingTime = tbType5SettlingTime.Value;
        }

        private void tbType6SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].SettlingTime = tbType6SettlingTime.Value;
        }

        private void tbType7SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].SettlingTime = tbType7SettlingTime.Value;
        }

        private void tbType8SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].SettlingTime = tbType8SettlingTime.Value;
        }

        private void tbType9SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].SettlingTime = tbType9SettlingTime.Value;
        }

        private void tbType10SettlingTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].SettlingTime = tbType10SettlingTime.Value;
        }

        private void tbType1DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].DownSpeed = tbType1DownSpeed.Value;
        }

        private void tbType2DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].DownSpeed = tbType2DownSpeed.Value;
        }

        private void tbType3DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].DownSpeed = tbType3DownSpeed.Value;
        }

        private void tbType4DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].DownSpeed = tbType4DownSpeed.Value;
        }

        private void tbType5DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].DownSpeed = tbType5DownSpeed.Value;
        }

        private void tbType6DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].DownSpeed = tbType6DownSpeed.Value;
        }

        private void tbType7DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].DownSpeed = tbType7DownSpeed.Value;
        }

        private void tbType8DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].DownSpeed = tbType8DownSpeed.Value;
        }

        private void tbType9DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].DownSpeed = tbType9DownSpeed.Value;
        }

        private void tbType10DownSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].DownSpeed = tbType10DownSpeed.Value;
        }

        private void tbType1DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].DownAccel = tbType1DownAccel.Value;
        }

        private void tbType2DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].DownAccel = tbType2DownAccel.Value;
        }

        private void tbType3DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].DownAccel = tbType3DownAccel.Value;
        }

        private void tbType4DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].DownAccel = tbType4DownAccel.Value;
        }

        private void tbType5DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].DownAccel = tbType5DownAccel.Value;
        }

        private void tbType6DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].DownAccel = tbType6DownAccel.Value;
        }

        private void tbType7DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].DownAccel = tbType7DownAccel.Value;
        }

        private void tbType8DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].DownAccel = tbType8DownAccel.Value;
        }

        private void tbType9DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].DownAccel = tbType9DownAccel.Value;
        }

        private void tbType10DownAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].DownAccel = tbType10DownAccel.Value;
        }

        private void tbType1Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].Notes = tbType1Notes.Text;
        }

        private void tbType2Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].Notes = tbType2Notes.Text;
        }

        private void tbType3Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].Notes = tbType3Notes.Text;
        }

        private void tbType4Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].Notes = tbType4Notes.Text;
        }

        private void tbType5Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].Notes = tbType5Notes.Text;
        }

        private void tbType6Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].Notes = tbType6Notes.Text;
        }

        private void tbType7Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].Notes = tbType7Notes.Text;
        }

        private void tbType8Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].Notes = tbType8Notes.Text;
        }

        private void tbType9Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].Notes = tbType9Notes.Text;
        }

        private void tbType10Notes_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].Notes = tbType10Notes.Text;
        }

        // during dispense:
        private void tbType1DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].DispenseGap = tbType1DispenseGap.Value;
        }

        private void tbType2DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].DispenseGap = tbType2DispenseGap.Value;
        }

        private void tbType3DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].DispenseGap = tbType3DispenseGap.Value;
        }

        private void tbType4DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].DispenseGap = tbType4DispenseGap.Value;
        }

        private void tbType5DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].DispenseGap = tbType5DispenseGap.Value;
        }

        private void tbType6DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].DispenseGap = tbType6DispenseGap.Value;
        }

        private void tbType7DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].DispenseGap = tbType7DispenseGap.Value;
        }

        private void tbType8DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].DispenseGap = tbType8DispenseGap.Value;
        }

        private void tbType9DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].DispenseGap = tbType9DispenseGap.Value;
        }

        private void tbType10DispenseGap_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].DispenseGap = tbType10DispenseGap.Value;
        }

        private void tbType1NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].NumShots = tbType1NumShots.Value;
        }

        private void tbType2NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].NumShots = tbType2NumShots.Value;
        }

        private void tbType3NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].NumShots = tbType3NumShots.Value;
        }

        private void tbType4NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].NumShots = tbType4NumShots.Value;
        }

        private void tbType5NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].NumShots = tbType5NumShots.Value;
        }

        private void tbType6NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].NumShots = tbType6NumShots.Value;
        }

        private void tbType7NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].NumShots = tbType7NumShots.Value;
        }

        private void tbType8NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].NumShots = tbType8NumShots.Value;
        }

        private void tbType9NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].NumShots = tbType9NumShots.Value;
        }

        private void tbType10NumShots_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].NumShots = tbType10NumShots.Value;
        }

        private void tbType1MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].MultiShotDelta = tbType1MultiShotDelta.Value;
        }

        private void tbType2MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].MultiShotDelta = tbType2MultiShotDelta.Value;
        }

        private void tbType3MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].MultiShotDelta = tbType3MultiShotDelta.Value;
        }

        private void tbType4MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].MultiShotDelta = tbType4MultiShotDelta.Value;
        }

        private void tbType5MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].MultiShotDelta = tbType5MultiShotDelta.Value;
        }

        private void tbType6MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].MultiShotDelta = tbType6MultiShotDelta.Value;
        }

        private void tbType7MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].MultiShotDelta = tbType7MultiShotDelta.Value;
        }

        private void tbType8MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].MultiShotDelta = tbType8MultiShotDelta.Value;
        }

        private void tbType9MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].MultiShotDelta = tbType9MultiShotDelta.Value;
        }

        private void tbType10MultiShotDelta_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].MultiShotDelta = tbType10MultiShotDelta.Value;
        }

        // post dispense:
        private void tbType1DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].DwellTime = tbType1DwellTime.Value;
        }

        private void tbType2DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].DwellTime = tbType2DwellTime.Value;
        }

        private void tbType3DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].DwellTime = tbType3DwellTime.Value;
        }

        private void tbType4DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].DwellTime = tbType4DwellTime.Value;
        }

        private void tbType5DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].DwellTime = tbType5DwellTime.Value;
        }

        private void tbType6DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].DwellTime = tbType6DwellTime.Value;
        }

        private void tbType7DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].DwellTime = tbType7DwellTime.Value;
        }

        private void tbType8DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].DwellTime = tbType8DwellTime.Value;
        }

        private void tbType9DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].DwellTime = tbType9DwellTime.Value;
        }

        private void tbType10DwellTime_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].DwellTime = tbType10DwellTime.Value;
        }

        private void tbType1RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].RetractDistance = tbType1RetractDis.Value;
        }

        private void tbType2RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].RetractDistance = tbType2RetractDis.Value;
        }

        private void tbType3RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].RetractDistance = tbType3RetractDis.Value;
        }

        private void tbType4RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].RetractDistance = tbType4RetractDis.Value;
        }

        private void tbType5RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].RetractDistance = tbType5RetractDis.Value;
        }

        private void tbType6RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].RetractDistance = tbType6RetractDis.Value;
        }

        private void tbType7RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].RetractDistance = tbType7RetractDis.Value;
        }

        private void tbType8RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].RetractDistance = tbType8RetractDis.Value;
        }

        private void tbType9RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].RetractDistance = tbType9RetractDis.Value;
        }

        private void tbType10RetractDis_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].RetractDistance = tbType10RetractDis.Value;
        }

        private void tbType1RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].RetractSpeed = tbType1RetractSpeed.Value;
        }

        private void tbType2RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].RetractSpeed = tbType2RetractSpeed.Value;
        }

        private void tbType3RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].RetractSpeed = tbType3RetractSpeed.Value;
        }

        private void tbType4RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].RetractSpeed = tbType4RetractSpeed.Value;
        }

        private void tbType5RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].RetractSpeed = tbType5RetractSpeed.Value;
        }

        private void tbType6RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].RetractSpeed = tbType6RetractSpeed.Value;
        }

        private void tbType7RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].RetractSpeed = tbType7RetractSpeed.Value;
        }

        private void tbType8RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].RetractSpeed = tbType8RetractSpeed.Value;
        }

        private void tbType9RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].RetractSpeed = tbType9RetractSpeed.Value;
        }

        private void tbType10RetractSpeed_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].RetractSpeed = tbType10RetractSpeed.Value;
        }

        private void tbType1RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[0].RetractAccel = tbType1RetractAccel.Value;
        }

        private void tbType2RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[1].RetractAccel = tbType2RetractAccel.Value;
        }

        private void tbType3RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[2].RetractAccel = tbType3RetractAccel.Value;
        }

        private void tbType4RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[3].RetractAccel = tbType4RetractAccel.Value;
        }

        private void tbType5RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[4].RetractAccel = tbType5RetractAccel.Value;
        }

        private void tbType6RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[5].RetractAccel = tbType6RetractAccel.Value;
        }

        private void tbType7RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[6].RetractAccel = tbType7RetractAccel.Value;
        }

        private void tbType8RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[7].RetractAccel = tbType8RetractAccel.Value;
        }

        private void tbType9RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[8].RetractAccel = tbType9RetractAccel.Value;
        }

        private void tbType10RetractAccel_TextChanged(object sender, System.EventArgs e)
        {
            dotParamList[9].RetractAccel = tbType10RetractAccel.Value;
        }

        private void EditDotParamsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FluidProgram.Current!=null&& this.dotParamList!=null && this.dotParamListBackUp!=null)
            {
                for (int i = 0; i < this.dotParamList.Count; i++)
                {
                    CompareObj.CompareField(this.dotParamList[i], this.dotParamListBackUp[i], null, this.GetType().Name);
                }
            }           

        }

        private void tbType1ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[0].ValveOnTime = tbType1ValveOnTime.Value;
        }

        private void tbType2ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[1].ValveOnTime = tbType2ValveOnTime.Value;
        }

        private void tbType3ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[2].ValveOnTime = tbType3ValveOnTime.Value;
        }

        private void tbType4ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[3].ValveOnTime = tbType4ValveOnTime.Value;
        }

        private void tbType5ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[4].ValveOnTime = tbType5ValveOnTime.Value;
        }

        private void tbType6ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[5].ValveOnTime = tbType6ValveOnTime.Value;
        }

        private void tbType7ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[6].ValveOnTime = tbType7ValveOnTime.Value;
        }

        private void tbType8ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[7].ValveOnTime = tbType8ValveOnTime.Value;
        }

        private void tbType9ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[8].ValveOnTime = tbType9ValveOnTime.Value;
        }

        private void tbType10ValveOnTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[9].ValveOnTime = tbType10ValveOnTime.Value;
        }

        private void tbType1SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[0].SuckBackTime = tbType1SuckBackTime.Value;
        }

        private void tbType2SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[1].SuckBackTime = tbType2SuckBackTime.Value;
        }

        private void tbType3SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[2].SuckBackTime = tbType3SuckBackTime.Value;
        }

        private void tbType4SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[3].SuckBackTime = tbType4SuckBackTime.Value;
        }

        private void tbType5SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[4].SuckBackTime = tbType5SuckBackTime.Value;
        }

        private void tbType6SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[5].SuckBackTime = tbType6SuckBackTime.Value;
        }

        private void tbType7SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[6].SuckBackTime = tbType7SuckBackTime.Value;
        }

        private void tbType8SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[7].SuckBackTime = tbType8SuckBackTime.Value;
        }

        private void tbType9SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[8].SuckBackTime = tbType9SuckBackTime.Value;
        }

        private void tbType10SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            dotParamList[9].SuckBackTime = tbType10SuckBackTime.Value;
        }
    }
}
