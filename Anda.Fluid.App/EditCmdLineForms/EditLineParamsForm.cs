using Anda.Fluid.Domain.FluProgram.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.International;
using Anda.Fluid.Controls;
using Anda.Fluid.Drive;
using Anda.Fluid.Infrastructure.Reflection;
using Anda.Fluid.Domain.FluProgram;
using Anda.Fluid.Domain.Settings;

namespace Anda.Fluid.App.EditCmdLineForms
{
    public partial class EditLineParamsForm : FormEx
    {
        private IReadOnlyList<LineParam> lineParamList;
        private IReadOnlyList<LineParam> lineParamListBackUp;
        /// <summary>
        /// 仅用于生成语言文本
        /// </summary>
        private EditLineParamsForm()
        {
            InitializeComponent();
        }

        public EditLineParamsForm(IReadOnlyList<LineParam> lineParamList)
        {
            InitializeComponent();
            this.Init();
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            if (lineParamList == null || lineParamList.Count != 10)
            {
                //MessageBox.Show("Line param list number is not 10.");
                MessageBox.Show("线参数集合的大小不是10.");
                Close();
                return;
            }
            this.lineParamList = lineParamList;

            #region PreDispense
            tbType1DownSpeed.Text = lineParamList[0].DownSpeed.ToString("0.000");
            tbType2DownSpeed.Text = lineParamList[1].DownSpeed.ToString("0.000");
            tbType3DownSpeed.Text = lineParamList[2].DownSpeed.ToString("0.000");
            tbType4DownSpeed.Text = lineParamList[3].DownSpeed.ToString("0.000");
            tbType5DownSpeed.Text = lineParamList[4].DownSpeed.ToString("0.000");
            tbType6DownSpeed.Text = lineParamList[5].DownSpeed.ToString("0.000");
            tbType7DownSpeed.Text = lineParamList[6].DownSpeed.ToString("0.000");
            tbType8DownSpeed.Text = lineParamList[7].DownSpeed.ToString("0.000");
            tbType9DownSpeed.Text = lineParamList[8].DownSpeed.ToString("0.000");
            tbType10DownSpeed.Text = lineParamList[9].DownSpeed.ToString("0.000");

            tbType1DownAccel.Text = lineParamList[0].DownAccel.ToString("0.000");
            tbType2DownAccel.Text = lineParamList[1].DownAccel.ToString("0.000");
            tbType3DownAccel.Text = lineParamList[2].DownAccel.ToString("0.000");
            tbType4DownAccel.Text = lineParamList[3].DownAccel.ToString("0.000");
            tbType5DownAccel.Text = lineParamList[4].DownAccel.ToString("0.000");
            tbType6DownAccel.Text = lineParamList[5].DownAccel.ToString("0.000");
            tbType7DownAccel.Text = lineParamList[6].DownAccel.ToString("0.000");
            tbType8DownAccel.Text = lineParamList[7].DownAccel.ToString("0.000");
            tbType9DownAccel.Text = lineParamList[8].DownAccel.ToString("0.000");
            tbType10DownAccel.Text = lineParamList[9].DownAccel.ToString("0.000");

            tbType1Offset.Text = lineParamList[0].Offset.ToString("0.000");
            tbType2Offset.Text = lineParamList[1].Offset.ToString("0.000");
            tbType3Offset.Text = lineParamList[2].Offset.ToString("0.000");
            tbType4Offset.Text = lineParamList[3].Offset.ToString("0.000");
            tbType5Offset.Text = lineParamList[4].Offset.ToString("0.000");
            tbType6Offset.Text = lineParamList[5].Offset.ToString("0.000");
            tbType7Offset.Text = lineParamList[6].Offset.ToString("0.000");
            tbType8Offset.Text = lineParamList[7].Offset.ToString("0.000");
            tbType9Offset.Text = lineParamList[8].Offset.ToString("0.000");
            tbType10Offset.Text = lineParamList[9].Offset.ToString("0.000");

            tbType1PreMoveDelay.Text = (lineParamList[0].PreMoveDelay * 1000).ToString("0.000");
            tbType2PreMoveDelay.Text = (lineParamList[1].PreMoveDelay * 1000).ToString("0.000");
            tbType3PreMoveDelay.Text = (lineParamList[2].PreMoveDelay * 1000).ToString("0.000");
            tbType4PreMoveDelay.Text = (lineParamList[3].PreMoveDelay * 1000).ToString("0.000");
            tbType5PreMoveDelay.Text = (lineParamList[4].PreMoveDelay * 1000).ToString("0.000");
            tbType6PreMoveDelay.Text = (lineParamList[5].PreMoveDelay * 1000).ToString("0.000");
            tbType7PreMoveDelay.Text = (lineParamList[6].PreMoveDelay * 1000).ToString("0.000");
            tbType8PreMoveDelay.Text = (lineParamList[7].PreMoveDelay * 1000).ToString("0.000");
            tbType9PreMoveDelay.Text = (lineParamList[8].PreMoveDelay * 1000).ToString("0.000");
            tbType10PreMoveDelay.Text = (lineParamList[9].PreMoveDelay * 1000).ToString("0.000");

            tbType1Notes.Text = lineParamList[0].Notes;
            tbType2Notes.Text = lineParamList[1].Notes;
            tbType3Notes.Text = lineParamList[2].Notes;
            tbType4Notes.Text = lineParamList[3].Notes;
            tbType5Notes.Text = lineParamList[4].Notes;
            tbType6Notes.Text = lineParamList[5].Notes;
            tbType7Notes.Text = lineParamList[6].Notes;
            tbType8Notes.Text = lineParamList[7].Notes;
            tbType9Notes.Text = lineParamList[8].Notes;
            tbType10Notes.Text = lineParamList[9].Notes;

            #endregion

            #region DuringDispense
            tbType1DispenseGap.Text = lineParamList[0].DispenseGap.ToString("0.000");
            tbType2DispenseGap.Text = lineParamList[1].DispenseGap.ToString("0.000");
            tbType3DispenseGap.Text = lineParamList[2].DispenseGap.ToString("0.000");
            tbType4DispenseGap.Text = lineParamList[3].DispenseGap.ToString("0.000");
            tbType5DispenseGap.Text = lineParamList[4].DispenseGap.ToString("0.000");
            tbType6DispenseGap.Text = lineParamList[5].DispenseGap.ToString("0.000");
            tbType7DispenseGap.Text = lineParamList[6].DispenseGap.ToString("0.000");
            tbType8DispenseGap.Text = lineParamList[7].DispenseGap.ToString("0.000");
            tbType9DispenseGap.Text = lineParamList[8].DispenseGap.ToString("0.000");
            tbType10DispenseGap.Text = lineParamList[9].DispenseGap.ToString("0.000");

            tbType1Speed.Text = lineParamList[0].Speed.ToString("0.000");
            tbType2Speed.Text = lineParamList[1].Speed.ToString("0.000");
            tbType3Speed.Text = lineParamList[2].Speed.ToString("0.000");
            tbType4Speed.Text = lineParamList[3].Speed.ToString("0.000");
            tbType5Speed.Text = lineParamList[4].Speed.ToString("0.000");
            tbType6Speed.Text = lineParamList[5].Speed.ToString("0.000");
            tbType7Speed.Text = lineParamList[6].Speed.ToString("0.000");
            tbType8Speed.Text = lineParamList[7].Speed.ToString("0.000");
            tbType9Speed.Text = lineParamList[8].Speed.ToString("0.000");
            tbType10Speed.Text = lineParamList[9].Speed.ToString("0.000");

            cbType1WtCtrlSpeed.Items.Add("Compute");
            cbType1WtCtrlSpeed.Items.Add(lineParamList[0].WtCtrlSpeed.ToString("0.000"));
            cbType1WtCtrlSpeed.SelectedIndex = lineParamList[0].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType2WtCtrlSpeed.Items.Add("Compute");
            cbType2WtCtrlSpeed.Items.Add(lineParamList[1].WtCtrlSpeed.ToString("0.000"));
            cbType2WtCtrlSpeed.SelectedIndex = lineParamList[1].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType3WtCtrlSpeed.Items.Add("Compute");
            cbType3WtCtrlSpeed.Items.Add(lineParamList[2].WtCtrlSpeed.ToString("0.000"));
            cbType3WtCtrlSpeed.SelectedIndex = lineParamList[2].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType4WtCtrlSpeed.Items.Add("Compute");
            cbType4WtCtrlSpeed.Items.Add(lineParamList[3].WtCtrlSpeed.ToString("0.000"));
            cbType4WtCtrlSpeed.SelectedIndex = lineParamList[3].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType5WtCtrlSpeed.Items.Add("Compute");
            cbType5WtCtrlSpeed.Items.Add(lineParamList[4].WtCtrlSpeed.ToString("0.000"));
            cbType5WtCtrlSpeed.SelectedIndex = lineParamList[4].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType6WtCtrlSpeed.Items.Add("Compute");
            cbType6WtCtrlSpeed.Items.Add(lineParamList[5].WtCtrlSpeed.ToString("0.000"));
            cbType6WtCtrlSpeed.SelectedIndex = lineParamList[5].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType7WtCtrlSpeed.Items.Add("Compute");
            cbType7WtCtrlSpeed.Items.Add(lineParamList[6].WtCtrlSpeed.ToString("0.000"));
            cbType7WtCtrlSpeed.SelectedIndex = lineParamList[6].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType8WtCtrlSpeed.Items.Add("Compute");
            cbType8WtCtrlSpeed.Items.Add(lineParamList[7].WtCtrlSpeed.ToString("0.000"));
            cbType8WtCtrlSpeed.SelectedIndex = lineParamList[7].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType9WtCtrlSpeed.Items.Add("Compute");
            cbType9WtCtrlSpeed.Items.Add(lineParamList[8].WtCtrlSpeed.ToString("0.000"));
            cbType9WtCtrlSpeed.SelectedIndex = lineParamList[8].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType10WtCtrlSpeed.Items.Add("Compute");
            cbType10WtCtrlSpeed.Items.Add(lineParamList[9].WtCtrlSpeed.ToString("0.000"));
            cbType10WtCtrlSpeed.SelectedIndex = lineParamList[9].WtCtrlSpeedValueType == LineParam.ValueType.COMPUTE ? 0 : 1;

            cbType1AccelDistance.Items.Add("Compute");
            cbType1AccelDistance.Items.Add(lineParamList[0].AccelDistance.ToString("0.000"));
            cbType1AccelDistance.SelectedIndex = lineParamList[0].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType2AccelDistance.Items.Add("Compute");
            cbType2AccelDistance.Items.Add(lineParamList[1].AccelDistance.ToString("0.000"));
            cbType2AccelDistance.SelectedIndex = lineParamList[1].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType3AccelDistance.Items.Add("Compute");
            cbType3AccelDistance.Items.Add(lineParamList[2].AccelDistance.ToString("0.000"));
            cbType3AccelDistance.SelectedIndex = lineParamList[2].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType4AccelDistance.Items.Add("Compute");
            cbType4AccelDistance.Items.Add(lineParamList[3].AccelDistance.ToString("0.000"));
            cbType4AccelDistance.SelectedIndex = lineParamList[3].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType5AccelDistance.Items.Add("Compute");
            cbType5AccelDistance.Items.Add(lineParamList[4].AccelDistance.ToString("0.000"));
            cbType5AccelDistance.SelectedIndex = lineParamList[4].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType6AccelDistance.Items.Add("Compute");
            cbType6AccelDistance.Items.Add(lineParamList[5].AccelDistance.ToString("0.000"));
            cbType6AccelDistance.SelectedIndex = lineParamList[5].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType7AccelDistance.Items.Add("Compute");
            cbType7AccelDistance.Items.Add(lineParamList[6].AccelDistance.ToString("0.000"));
            cbType7AccelDistance.SelectedIndex = lineParamList[6].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType8AccelDistance.Items.Add("Compute");
            cbType8AccelDistance.Items.Add(lineParamList[7].AccelDistance.ToString("0.000"));
            cbType8AccelDistance.SelectedIndex = lineParamList[7].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType9AccelDistance.Items.Add("Compute");
            cbType9AccelDistance.Items.Add(lineParamList[8].AccelDistance.ToString("0.000"));
            cbType9AccelDistance.SelectedIndex = lineParamList[8].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType10AccelDistance.Items.Add("Compute");
            cbType10AccelDistance.Items.Add(lineParamList[9].AccelDistance.ToString("0.000"));
            cbType10AccelDistance.SelectedIndex = lineParamList[9].AccelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;

            cbType1DecelDistance.Items.Add("Compute");
            cbType1DecelDistance.Items.Add(lineParamList[0].DecelDistance.ToString("0.000"));
            cbType1DecelDistance.SelectedIndex = lineParamList[0].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType2DecelDistance.Items.Add("Compute");
            cbType2DecelDistance.Items.Add(lineParamList[1].DecelDistance.ToString("0.000"));
            cbType2DecelDistance.SelectedIndex = lineParamList[1].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType3DecelDistance.Items.Add("Compute");
            cbType3DecelDistance.Items.Add(lineParamList[2].DecelDistance.ToString("0.000"));
            cbType3DecelDistance.SelectedIndex = lineParamList[2].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType4DecelDistance.Items.Add("Compute");
            cbType4DecelDistance.Items.Add(lineParamList[3].DecelDistance.ToString("0.000"));
            cbType4DecelDistance.SelectedIndex = lineParamList[3].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType5DecelDistance.Items.Add("Compute");
            cbType5DecelDistance.Items.Add(lineParamList[4].DecelDistance.ToString("0.000"));
            cbType5DecelDistance.SelectedIndex = lineParamList[4].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType6DecelDistance.Items.Add("Compute");
            cbType6DecelDistance.Items.Add(lineParamList[5].DecelDistance.ToString("0.000"));
            cbType6DecelDistance.SelectedIndex = lineParamList[5].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType7DecelDistance.Items.Add("Compute");
            cbType7DecelDistance.Items.Add(lineParamList[6].DecelDistance.ToString("0.000"));
            cbType7DecelDistance.SelectedIndex = lineParamList[6].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType8DecelDistance.Items.Add("Compute");
            cbType8DecelDistance.Items.Add(lineParamList[7].DecelDistance.ToString("0.000"));
            cbType8DecelDistance.SelectedIndex = lineParamList[7].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType9DecelDistance.Items.Add("Compute");
            cbType9DecelDistance.Items.Add(lineParamList[8].DecelDistance.ToString("0.000"));
            cbType9DecelDistance.SelectedIndex = lineParamList[8].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;
            cbType10DecelDistance.Items.Add("Compute");
            cbType10DecelDistance.Items.Add(lineParamList[9].DecelDistance.ToString("0.000"));
            cbType10DecelDistance.SelectedIndex = lineParamList[9].DecelDistanceValueType == LineParam.ValueType.COMPUTE ? 0 : 1;

            tbType1ShutoffDistance.Text = lineParamList[0].ShutOffDistance.ToString("0.000");
            tbType2ShutoffDistance.Text = lineParamList[1].ShutOffDistance.ToString("0.000");
            tbType3ShutoffDistance.Text = lineParamList[2].ShutOffDistance.ToString("0.000");
            tbType4ShutoffDistance.Text = lineParamList[3].ShutOffDistance.ToString("0.000");
            tbType5ShutoffDistance.Text = lineParamList[4].ShutOffDistance.ToString("0.000");
            tbType6ShutoffDistance.Text = lineParamList[5].ShutOffDistance.ToString("0.000");
            tbType7ShutoffDistance.Text = lineParamList[6].ShutOffDistance.ToString("0.000");
            tbType8ShutoffDistance.Text = lineParamList[7].ShutOffDistance.ToString("0.000");
            tbType9ShutoffDistance.Text = lineParamList[8].ShutOffDistance.ToString("0.000");
            tbType10ShutoffDistance.Text = lineParamList[9].ShutOffDistance.ToString("0.000");

            tbType1SuckBackTime.Text = lineParamList[0].SuckBackTime.ToString("0.000");
            tbType2SuckBackTime.Text = lineParamList[1].SuckBackTime.ToString("0.000");
            tbType3SuckBackTime.Text = lineParamList[2].SuckBackTime.ToString("0.000");
            tbType4SuckBackTime.Text = lineParamList[3].SuckBackTime.ToString("0.000");
            tbType5SuckBackTime.Text = lineParamList[4].SuckBackTime.ToString("0.000");
            tbType6SuckBackTime.Text = lineParamList[5].SuckBackTime.ToString("0.000");
            tbType7SuckBackTime.Text = lineParamList[6].SuckBackTime.ToString("0.000");
            tbType8SuckBackTime.Text = lineParamList[7].SuckBackTime.ToString("0.000");
            tbType9SuckBackTime.Text = lineParamList[8].SuckBackTime.ToString("0.000");
            tbType10SuckBackTime.Text = lineParamList[9].SuckBackTime.ToString("0.000");

            tbType1DwellTIme.Text = (lineParamList[0].Dwell * 1000).ToString("0.0");
            tbType2DwellTIme.Text = (lineParamList[1].Dwell * 1000).ToString("0.0");
            tbType3DwellTIme.Text = (lineParamList[2].Dwell * 1000).ToString("0.0");
            tbType4DwellTIme.Text = (lineParamList[3].Dwell * 1000).ToString("0.0");
            tbType5DwellTIme.Text = (lineParamList[4].Dwell * 1000).ToString("0.0");
            tbType6DwellTIme.Text = (lineParamList[5].Dwell * 1000).ToString("0.0");
            tbType7DwellTIme.Text = (lineParamList[6].Dwell * 1000).ToString("0.0");
            tbType8DwellTIme.Text = (lineParamList[7].Dwell * 1000).ToString("0.0");
            tbType9DwellTIme.Text = (lineParamList[8].Dwell * 1000).ToString("0.0");
            tbType10DwellTIme.Text = (lineParamList[9].Dwell * 1000).ToString("0.0");
            #endregion

            #region PostDispense
            tbType1RetractDis.Text = lineParamList[0].RetractDistance.ToString("0.000");
            tbType2RetractDis.Text = lineParamList[1].RetractDistance.ToString("0.000");
            tbType3RetractDis.Text = lineParamList[2].RetractDistance.ToString("0.000");
            tbType4RetractDis.Text = lineParamList[3].RetractDistance.ToString("0.000");
            tbType5RetractDis.Text = lineParamList[4].RetractDistance.ToString("0.000");
            tbType6RetractDis.Text = lineParamList[5].RetractDistance.ToString("0.000");
            tbType7RetractDis.Text = lineParamList[6].RetractDistance.ToString("0.000");
            tbType8RetractDis.Text = lineParamList[7].RetractDistance.ToString("0.000");
            tbType9RetractDis.Text = lineParamList[8].RetractDistance.ToString("0.000");
            tbType10RetractDis.Text = lineParamList[9].RetractDistance.ToString("0.000");

            tbType1RetractSpeed.Text = lineParamList[0].RetractSpeed.ToString("0.000");
            tbType2RetractSpeed.Text = lineParamList[1].RetractSpeed.ToString("0.000");
            tbType3RetractSpeed.Text = lineParamList[2].RetractSpeed.ToString("0.000");
            tbType4RetractSpeed.Text = lineParamList[3].RetractSpeed.ToString("0.000");
            tbType5RetractSpeed.Text = lineParamList[4].RetractSpeed.ToString("0.000");
            tbType6RetractSpeed.Text = lineParamList[5].RetractSpeed.ToString("0.000");
            tbType7RetractSpeed.Text = lineParamList[6].RetractSpeed.ToString("0.000");
            tbType8RetractSpeed.Text = lineParamList[7].RetractSpeed.ToString("0.000");
            tbType9RetractSpeed.Text = lineParamList[8].RetractSpeed.ToString("0.000");
            tbType10RetractSpeed.Text = lineParamList[9].RetractSpeed.ToString("0.000");

            tbType1RetractAccel.Text = lineParamList[0].RetractAccel.ToString("0.000");
            tbType2RetractAccel.Text = lineParamList[1].RetractAccel.ToString("0.000");
            tbType3RetractAccel.Text = lineParamList[2].RetractAccel.ToString("0.000");
            tbType4RetractAccel.Text = lineParamList[3].RetractAccel.ToString("0.000");
            tbType5RetractAccel.Text = lineParamList[4].RetractAccel.ToString("0.000");
            tbType6RetractAccel.Text = lineParamList[5].RetractAccel.ToString("0.000");
            tbType7RetractAccel.Text = lineParamList[6].RetractAccel.ToString("0.000");
            tbType8RetractAccel.Text = lineParamList[7].RetractAccel.ToString("0.000");
            tbType9RetractAccel.Text = lineParamList[8].RetractAccel.ToString("0.000");
            tbType10RetractAccel.Text = lineParamList[9].RetractAccel.ToString("0.000");

            tbType1BackGap.Text = lineParamList[0].BackGap.ToString("0.000");
            tbType2BackGap.Text = lineParamList[1].BackGap.ToString("0.000");
            tbType3BackGap.Text = lineParamList[2].BackGap.ToString("0.000");
            tbType4BackGap.Text = lineParamList[3].BackGap.ToString("0.000");
            tbType5BackGap.Text = lineParamList[4].BackGap.ToString("0.000");
            tbType6BackGap.Text = lineParamList[5].BackGap.ToString("0.000");
            tbType7BackGap.Text = lineParamList[6].BackGap.ToString("0.000");
            tbType8BackGap.Text = lineParamList[7].BackGap.ToString("0.000");
            tbType9BackGap.Text = lineParamList[8].BackGap.ToString("0.000");
            tbType10BackGap.Text = lineParamList[9].BackGap.ToString("0.000");

            tbType1BacktrackEndGap.Text = lineParamList[0].BacktrackEndGap.ToString("0.000");
            tbType2BacktrackEndGap.Text = lineParamList[1].BacktrackEndGap.ToString("0.000");
            tbType3BacktrackEndGap.Text = lineParamList[2].BacktrackEndGap.ToString("0.000");
            tbType4BacktrackEndGap.Text = lineParamList[3].BacktrackEndGap.ToString("0.000");
            tbType5BacktrackEndGap.Text = lineParamList[4].BacktrackEndGap.ToString("0.000");
            tbType6BacktrackEndGap.Text = lineParamList[5].BacktrackEndGap.ToString("0.000");
            tbType7BacktrackEndGap.Text = lineParamList[6].BacktrackEndGap.ToString("0.000");
            tbType8BacktrackEndGap.Text = lineParamList[7].BacktrackEndGap.ToString("0.000");
            tbType9BacktrackEndGap.Text = lineParamList[8].BacktrackEndGap.ToString("0.000");
            tbType10BacktrackEndGap.Text = lineParamList[9].BacktrackEndGap.ToString("0.000");


            tbType1BacktrackGap.Text = lineParamList[0].BacktrackGap.ToString("0.000");
            tbType2BacktrackGap.Text = lineParamList[1].BacktrackGap.ToString("0.000");
            tbType3BacktrackGap.Text = lineParamList[2].BacktrackGap.ToString("0.000");
            tbType4BacktrackGap.Text = lineParamList[3].BacktrackGap.ToString("0.000");
            tbType5BacktrackGap.Text = lineParamList[4].BacktrackGap.ToString("0.000");
            tbType6BacktrackGap.Text = lineParamList[5].BacktrackGap.ToString("0.000");
            tbType7BacktrackGap.Text = lineParamList[6].BacktrackGap.ToString("0.000");
            tbType8BacktrackGap.Text = lineParamList[7].BacktrackGap.ToString("0.000");
            tbType9BacktrackGap.Text = lineParamList[8].BacktrackGap.ToString("0.000");
            tbType10BacktrackGap.Text = lineParamList[9].BacktrackGap.ToString("0.000");

            tbType1BacktrackDistance.Text = lineParamList[0].BacktrackDistance.ToString("0.00");
            tbType2BacktrackDistance.Text = lineParamList[1].BacktrackDistance.ToString("0.00");
            tbType3BacktrackDistance.Text = lineParamList[2].BacktrackDistance.ToString("0.00");
            tbType4BacktrackDistance.Text = lineParamList[3].BacktrackDistance.ToString("0.00");
            tbType5BacktrackDistance.Text = lineParamList[4].BacktrackDistance.ToString("0.00");
            tbType6BacktrackDistance.Text = lineParamList[5].BacktrackDistance.ToString("0.00");
            tbType7BacktrackDistance.Text = lineParamList[6].BacktrackDistance.ToString("0.00");
            tbType8BacktrackDistance.Text = lineParamList[7].BacktrackDistance.ToString("0.00");
            tbType9BacktrackDistance.Text = lineParamList[8].BacktrackDistance.ToString("0.00");
            tbType10BacktrackDistance.Text = lineParamList[9].BacktrackDistance.ToString("0.00");

            tbType1BacktrackSpeed.Text = lineParamList[0].BacktrackSpeed.ToString("0.000");
            tbType2BacktrackSpeed.Text = lineParamList[1].BacktrackSpeed.ToString("0.000");
            tbType3BacktrackSpeed.Text = lineParamList[2].BacktrackSpeed.ToString("0.000");
            tbType4BacktrackSpeed.Text = lineParamList[3].BacktrackSpeed.ToString("0.000");
            tbType5BacktrackSpeed.Text = lineParamList[4].BacktrackSpeed.ToString("0.000");
            tbType6BacktrackSpeed.Text = lineParamList[5].BacktrackSpeed.ToString("0.000");
            tbType7BacktrackSpeed.Text = lineParamList[6].BacktrackSpeed.ToString("0.000");
            tbType8BacktrackSpeed.Text = lineParamList[7].BacktrackSpeed.ToString("0.000");
            tbType9BacktrackSpeed.Text = lineParamList[8].BacktrackSpeed.ToString("0.000");
            tbType10BacktrackSpeed.Text = lineParamList[9].BacktrackSpeed.ToString("0.000");

            tbType1PressDistance.Text = lineParamList[0].PressDistance.ToString("0.000");
            tbType2PressDistance.Text = lineParamList[1].PressDistance.ToString("0.000");
            tbType3PressDistance.Text = lineParamList[2].PressDistance.ToString("0.000");
            tbType4PressDistance.Text = lineParamList[3].PressDistance.ToString("0.000");
            tbType5PressDistance.Text = lineParamList[4].PressDistance.ToString("0.000");
            tbType6PressDistance.Text = lineParamList[5].PressDistance.ToString("0.000");
            tbType7PressDistance.Text = lineParamList[6].PressDistance.ToString("0.000");
            tbType8PressDistance.Text = lineParamList[7].PressDistance.ToString("0.000");
            tbType9PressDistance.Text = lineParamList[8].PressDistance.ToString("0.000");
            tbType10PressDistance.Text = lineParamList[9].PressDistance.ToString("0.000");

            tbType1PressSpeed.Text = lineParamList[0].PressSpeed.ToString("0.000");
            tbType2PressSpeed.Text = lineParamList[1].PressSpeed.ToString("0.000");
            tbType3PressSpeed.Text = lineParamList[2].PressSpeed.ToString("0.000");
            tbType4PressSpeed.Text = lineParamList[3].PressSpeed.ToString("0.000");
            tbType5PressSpeed.Text = lineParamList[4].PressSpeed.ToString("0.000");
            tbType6PressSpeed.Text = lineParamList[5].PressSpeed.ToString("0.000");
            tbType7PressSpeed.Text = lineParamList[6].PressSpeed.ToString("0.000");
            tbType8PressSpeed.Text = lineParamList[7].PressSpeed.ToString("0.000");
            tbType9PressSpeed.Text = lineParamList[8].PressSpeed.ToString("0.000");
            tbType10PressSpeed.Text = lineParamList[9].PressSpeed.ToString("0.000");

            tbType1PressAccel.Text = lineParamList[0].PressAccel.ToString("0.000");
            tbType2PressAccel.Text = lineParamList[1].PressAccel.ToString("0.000");
            tbType3PressAccel.Text = lineParamList[2].PressAccel.ToString("0.000");
            tbType4PressAccel.Text = lineParamList[3].PressAccel.ToString("0.000");
            tbType5PressAccel.Text = lineParamList[4].PressAccel.ToString("0.000");
            tbType6PressAccel.Text = lineParamList[5].PressAccel.ToString("0.000");
            tbType7PressAccel.Text = lineParamList[6].PressAccel.ToString("0.000");
            tbType8PressAccel.Text = lineParamList[7].PressAccel.ToString("0.000");
            tbType9PressAccel.Text = lineParamList[8].PressAccel.ToString("0.000");
            tbType10PressAccel.Text = lineParamList[9].PressAccel.ToString("0.000");

            tbType1PressTime.Text = lineParamList[0].PressTime.ToString("0.000");
            tbType2PressTime.Text = lineParamList[1].PressTime.ToString("0.000");
            tbType3PressTime.Text = lineParamList[2].PressTime.ToString("0.000");
            tbType4PressTime.Text = lineParamList[3].PressTime.ToString("0.000");
            tbType5PressTime.Text = lineParamList[4].PressTime.ToString("0.000");
            tbType6PressTime.Text = lineParamList[5].PressTime.ToString("0.000");
            tbType7PressTime.Text = lineParamList[6].PressTime.ToString("0.000");
            tbType8PressTime.Text = lineParamList[7].PressTime.ToString("0.000");
            tbType9PressTime.Text = lineParamList[8].PressTime.ToString("0.000");
            tbType10PressTime.Text = lineParamList[9].PressTime.ToString("0.000");

            tbType1RaiseDistance.Text = lineParamList[0].RaiseDistance.ToString("0.000");
            tbType2RaiseDistance.Text = lineParamList[1].RaiseDistance.ToString("0.000");
            tbType3RaiseDistance.Text = lineParamList[2].RaiseDistance.ToString("0.000");
            tbType4RaiseDistance.Text = lineParamList[3].RaiseDistance.ToString("0.000");
            tbType5RaiseDistance.Text = lineParamList[4].RaiseDistance.ToString("0.000");
            tbType6RaiseDistance.Text = lineParamList[5].RaiseDistance.ToString("0.000");
            tbType7RaiseDistance.Text = lineParamList[6].RaiseDistance.ToString("0.000");
            tbType8RaiseDistance.Text = lineParamList[7].RaiseDistance.ToString("0.000");
            tbType9RaiseDistance.Text = lineParamList[8].RaiseDistance.ToString("0.000");
            tbType10RaiseDistance.Text = lineParamList[9].RaiseDistance.ToString("0.000");

            tbType1RaiseSpeed.Text = lineParamList[0].RaiseSpeed.ToString("0.000");
            tbType2RaiseSpeed.Text = lineParamList[1].RaiseSpeed.ToString("0.000");
            tbType3RaiseSpeed.Text = lineParamList[2].RaiseSpeed.ToString("0.000");
            tbType4RaiseSpeed.Text = lineParamList[3].RaiseSpeed.ToString("0.000");
            tbType5RaiseSpeed.Text = lineParamList[4].RaiseSpeed.ToString("0.000");
            tbType6RaiseSpeed.Text = lineParamList[5].RaiseSpeed.ToString("0.000");
            tbType7RaiseSpeed.Text = lineParamList[6].RaiseSpeed.ToString("0.000");
            tbType8RaiseSpeed.Text = lineParamList[7].RaiseSpeed.ToString("0.000");
            tbType9RaiseSpeed.Text = lineParamList[8].RaiseSpeed.ToString("0.000");
            tbType10RaiseSpeed.Text = lineParamList[9].RaiseSpeed.ToString("0.000");

            tbType1RaiseAccel.Text = lineParamList[0].RaiseAccel.ToString("0.000");
            tbType2RaiseAccel.Text = lineParamList[1].RaiseAccel.ToString("0.000");
            tbType3RaiseAccel.Text = lineParamList[2].RaiseAccel.ToString("0.000");
            tbType4RaiseAccel.Text = lineParamList[3].RaiseAccel.ToString("0.000");
            tbType5RaiseAccel.Text = lineParamList[4].RaiseAccel.ToString("0.000");
            tbType6RaiseAccel.Text = lineParamList[5].RaiseAccel.ToString("0.000");
            tbType7RaiseAccel.Text = lineParamList[6].RaiseAccel.ToString("0.000");
            tbType8RaiseAccel.Text = lineParamList[7].RaiseAccel.ToString("0.000");
            tbType9RaiseAccel.Text = lineParamList[8].RaiseAccel.ToString("0.000");
            tbType10RaiseAccel.Text = lineParamList[9].RaiseAccel.ToString("0.000");

            #endregion

            #region Control
            addControlModeList(cbType1ControlMode, lineParamList[0].ControlMode);
            addControlModeList(cbType2ControlMode, lineParamList[1].ControlMode);
            addControlModeList(cbType3ControlMode, lineParamList[2].ControlMode);
            addControlModeList(cbType4ControlMode, lineParamList[3].ControlMode);
            addControlModeList(cbType5ControlMode, lineParamList[4].ControlMode);
            addControlModeList(cbType6ControlMode, lineParamList[5].ControlMode);
            addControlModeList(cbType7ControlMode, lineParamList[6].ControlMode);
            addControlModeList(cbType8ControlMode, lineParamList[7].ControlMode);
            addControlModeList(cbType9ControlMode, lineParamList[8].ControlMode);
            addControlModeList(cbType10ControlMode, lineParamList[9].ControlMode);

            tbType1Spacing.Text = lineParamList[0].Spacing.ToString("0.000");
            tbType2Spacing.Text = lineParamList[1].Spacing.ToString("0.000");
            tbType3Spacing.Text = lineParamList[2].Spacing.ToString("0.000");
            tbType4Spacing.Text = lineParamList[3].Spacing.ToString("0.000");
            tbType5Spacing.Text = lineParamList[4].Spacing.ToString("0.000");
            tbType6Spacing.Text = lineParamList[5].Spacing.ToString("0.000");
            tbType7Spacing.Text = lineParamList[6].Spacing.ToString("0.000");
            tbType8Spacing.Text = lineParamList[7].Spacing.ToString("0.000");
            tbType9Spacing.Text = lineParamList[8].Spacing.ToString("0.000");
            tbType10Spacing.Text = lineParamList[9].Spacing.ToString("0.000");

            tbType1ShotTimeInterval.Text = lineParamList[0].ShotTimeInterval.ToString("0.000");
            tbType2ShotTimeInterval.Text = lineParamList[1].ShotTimeInterval.ToString("0.000");
            tbType3ShotTimeInterval.Text = lineParamList[2].ShotTimeInterval.ToString("0.000");
            tbType4ShotTimeInterval.Text = lineParamList[3].ShotTimeInterval.ToString("0.000");
            tbType5ShotTimeInterval.Text = lineParamList[4].ShotTimeInterval.ToString("0.000");
            tbType6ShotTimeInterval.Text = lineParamList[5].ShotTimeInterval.ToString("0.000");
            tbType7ShotTimeInterval.Text = lineParamList[6].ShotTimeInterval.ToString("0.000");
            tbType8ShotTimeInterval.Text = lineParamList[7].ShotTimeInterval.ToString("0.000");
            tbType9ShotTimeInterval.Text = lineParamList[8].ShotTimeInterval.ToString("0.000");
            tbType10ShotTimeInterval.Text = lineParamList[9].ShotTimeInterval.ToString("0.000");

            tbType1TotalOfDots.Text = lineParamList[0].TotalOfDots.ToString();
            tbType2TotalOfDots.Text = lineParamList[1].TotalOfDots.ToString();
            tbType3TotalOfDots.Text = lineParamList[2].TotalOfDots.ToString();
            tbType4TotalOfDots.Text = lineParamList[3].TotalOfDots.ToString();
            tbType5TotalOfDots.Text = lineParamList[4].TotalOfDots.ToString();
            tbType6TotalOfDots.Text = lineParamList[5].TotalOfDots.ToString();
            tbType7TotalOfDots.Text = lineParamList[6].TotalOfDots.ToString();
            tbType8TotalOfDots.Text = lineParamList[7].TotalOfDots.ToString();
            tbType9TotalOfDots.Text = lineParamList[8].TotalOfDots.ToString();
            tbType10TotalOfDots.Text = lineParamList[9].TotalOfDots.ToString();
            #endregion
            if (FluidProgram.Current != null)
            {
                this.lineParamListBackUp = ((ProgramSettings)FluidProgram.Current.ProgramSettings.Clone()).LineParamList;
            }
            this.ReadLanguageResources();
        }
        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            base.ReadLanguageResources(skipButton, skipRadioButton, skipCheckBox, skipLabel);
            if (this.HasLngResources())
            {
                this.lblNotes.Text = this.ReadKeyValueFromResources(this.lblNotes.Name);
                this.lblPreMoveDelay.Text = this.ReadKeyValueFromResources(this.lblPreMoveDelay.Name);
                this.lblWtCtrlSpeed.Text = this.ReadKeyValueFromResources(this.lblWtCtrlSpeed.Name);
                this.lblAccelDistance.Text = this.ReadKeyValueFromResources(this.lblAccelDistance.Name);
                this.lblDecelDistance.Text = this.ReadKeyValueFromResources(this.lblDecelDistance.Name);
                this.lblShutoffDistance.Text = this.ReadKeyValueFromResources(this.lblShutoffDistance.Name);
                this.lblDwellTime.Text = this.ReadKeyValueFromResources(this.lblDwellTime.Name);
                this.lblSuckBackTime.Text = this.ReadKeyValueFromResources(this.lblSuckBackTime.Name);
                this.lblRetractDistance.Text = this.ReadKeyValueFromResources(this.lblRetractDistance.Name);
                this.lblRetractSpeed.Text = this.ReadKeyValueFromResources(this.lblRetractSpeed.Name);
                this.lblRetractSpeed.Text = this.ReadKeyValueFromResources(this.lblRetractSpeed.Name);
                this.lblRetractAccel.Text = this.ReadKeyValueFromResources(this.lblRetractAccel.Name);
                this.lblBacktrackGap.Text = this.ReadKeyValueFromResources(this.lblBacktrackGap.Name);
                this.label72.Text = this.ReadKeyValueFromResources(this.label72.Name);
                this.lblBacktrackDistance.Text = this.ReadKeyValueFromResources(this.lblBacktrackDistance.Name);
                this.lblBacktrackSpeed.Text = this.ReadKeyValueFromResources(this.lblBacktrackSpeed.Name);
                this.label2.Text = this.ReadKeyValueFromResources(this.label2.Name);
                this.label37.Text = this.ReadKeyValueFromResources(this.label37.Name);
                this.label25.Text = this.ReadKeyValueFromResources(this.label25.Name);
                this.label71.Text = this.ReadKeyValueFromResources(this.label71.Name);
                this.label60.Text = this.ReadKeyValueFromResources(this.label60.Name);
                this.label58.Text = this.ReadKeyValueFromResources(this.label58.Name);
                this.label54.Text = this.ReadKeyValueFromResources(this.label54.Name);
                this.lblControlMode.Text = this.ReadKeyValueFromResources(this.lblControlMode.Name);
                this.label74.Text= this.ReadKeyValueFromResources(this.label74.Name);
            }
        }

        private void Init()
        {
            this.Size = new System.Drawing.Size(1006, this.Size.Height);
            if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.喷射阀)
            {
                this.pnlPreMoveDelay.Hide();
                this.pnlNotes.Location = this.pnlPreMoveDelay.Location;
                this.pnlSvGearValveDuringParam.Hide();
                this.pnlSvValvePostParam.Hide();
            }
            else if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.螺杆阀)
            {
                this.pnlJtValveDuringParam.Hide();
                this.pnlSvGearValveDuringParam.Location = this.pnlJtValveDuringParam.Location;
                this.pnlSvValveDuringParam.Location = new System.Drawing.Point(this.pnlSvGearValveDuringParam.Location.X + this.pnlSvGearValveDuringParam.Width, this.pnlSvGearValveDuringParam.Location.Y);

                this.pnlJtValvePostParam.Hide();
                this.pnlGearValvePostParam.Hide();
                this.pnlSvValvePostParam.Location = this.pnlJtValvePostParam.Location;
            }

            else if (Machine.Instance.Valve1.ValveSeries == Drive.ValveSystem.ValveSeries.齿轮泵阀)
            {
                this.pnlJtValveDuringParam.Hide();
                this.pnlSvValveDuringParam.Hide();
                this.pnlSvGearValveDuringParam.Location = this.pnlJtValveDuringParam.Location;

                this.pnlJtValvePostParam.Hide();
                this.pnlSvValvePostParam.Location = this.pnlJtValvePostParam.Location;
                //this.pnlGearValvePostParam.Location = this.pnlJtValvePostParam.Location;
                this.pnlGearValvePostParam.Hide();

            }
        }

        private void addControlModeList(ComboBox comboBox, LineParam.CtrlMode ctrlMode)
        {
            comboBox.Items.Add("Pos-based spacing");
            comboBox.Items.Add("Time-based spacing");
            comboBox.Items.Add("Total # of dots");
            comboBox.Items.Add("No dispense");
            switch (ctrlMode)
            {
                case LineParam.CtrlMode.POS_BASED_SPACING:
                    comboBox.SelectedIndex = 0;
                    break;
                case LineParam.CtrlMode.TIME_BASED_SPACING:
                    comboBox.SelectedIndex = 1;
                    break;
                case LineParam.CtrlMode.TOTAL_OF_DOTS:
                    comboBox.SelectedIndex = 2;
                    break;
                case LineParam.CtrlMode.NO_DISPENSE:
                    comboBox.SelectedIndex = 3;
                    break;
            }
        }

        private void tbType1DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].DownSpeed = tbType1DownSpeed.Value;
        }

        private void tbType2DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].DownSpeed = tbType2DownSpeed.Value;
        }

        private void tbType3DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].DownSpeed = tbType3DownSpeed.Value;
        }

        private void tbType4DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].DownSpeed = tbType4DownSpeed.Value;
        }

        private void tbType5DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].DownSpeed = tbType5DownSpeed.Value;
        }

        private void tbType6DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].DownSpeed = tbType6DownSpeed.Value;
        }

        private void tbType7DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].DownSpeed = tbType7DownSpeed.Value;
        }

        private void tbType8DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].DownSpeed = tbType8DownSpeed.Value;
        }

        private void tbType9DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].DownSpeed = tbType9DownSpeed.Value;
        }

        private void tbType10DownSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].DownSpeed = tbType10DownSpeed.Value;
        }

        private void tbType1DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].DownAccel = tbType1DownAccel.Value;
        }

        private void tbType2DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].DownAccel = tbType2DownAccel.Value;
        }

        private void tbType3DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].DownAccel = tbType3DownAccel.Value;
        }

        private void tbType4DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].DownAccel = tbType4DownAccel.Value;
        }

        private void tbType5DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].DownAccel = tbType5DownAccel.Value;
        }

        private void tbType6DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].DownAccel = tbType6DownAccel.Value;
        }

        private void tbType7DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].DownAccel = tbType7DownAccel.Value;
        }

        private void tbType8DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].DownAccel = tbType8DownAccel.Value;
        }

        private void tbType9DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].DownAccel = tbType9DownAccel.Value;
        }

        private void tbType10DownAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].DownAccel = tbType10DownAccel.Value;
        }

        private void tbType1Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].Notes = tbType1Notes.Text;
        }

        private void tbType2Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].Notes = tbType2Notes.Text;
        }

        private void tbType3Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].Notes = tbType3Notes.Text;
        }

        private void tbType4Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].Notes = tbType4Notes.Text;
        }

        private void tbType5Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].Notes = tbType5Notes.Text;
        }

        private void tbType6Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].Notes = tbType6Notes.Text;
        }

        private void tbType7Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].Notes = tbType7Notes.Text;
        }

        private void tbType8Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].Notes = tbType8Notes.Text;
        }

        private void tbType9Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].Notes = tbType9Notes.Text;
        }

        private void tbType10Notes_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].Notes = tbType10Notes.Text;
        }

        private void tbType1DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].DispenseGap = tbType1DispenseGap.Value;
        }

        private void tbType2DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].DispenseGap = tbType2DispenseGap.Value;
        }

        private void tbType3DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].DispenseGap = tbType3DispenseGap.Value;
        }

        private void tbType4DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].DispenseGap = tbType4DispenseGap.Value;
        }

        private void tbType5DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].DispenseGap = tbType5DispenseGap.Value;
        }

        private void tbType6DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].DispenseGap = tbType6DispenseGap.Value;
        }

        private void tbType7DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].DispenseGap = tbType7DispenseGap.Value;
        }

        private void tbType8DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].DispenseGap = tbType8DispenseGap.Value;
        }

        private void tbType9DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].DispenseGap = tbType9DispenseGap.Value;
        }

        private void tbType10DispenseGap_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].DispenseGap = tbType10DispenseGap.Value;
        }

        private void tbType1Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].Speed = tbType1Speed.Value;
        }

        private void tbType2Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].Speed = tbType2Speed.Value;
        }

        private void tbType3Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].Speed = tbType3Speed.Value;
        }

        private void tbType4Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].Speed = tbType4Speed.Value;
        }

        private void tbType5Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].Speed = tbType5Speed.Value;
        }

        private void tbType6Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].Speed = tbType6Speed.Value;
        }

        private void tbType7Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].Speed = tbType7Speed.Value;
        }

        private void tbType8Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].Speed = tbType8Speed.Value;
        }

        private void tbType9Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].Speed = tbType9Speed.Value;
        }

        private void tbType10Speed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].Speed = tbType10Speed.Value;
        }

        /// <summary>
        /// 对 Wt-Ctrl Speed ComboBox内容发生变化事件的处理
        /// </summary>
        private void processWtCtrlSpeedTextChanged(ComboBox comboBox, int index)
        {
            if (comboBox.Text == "Compute")
            {
                lineParamList[index].WtCtrlSpeedValueType = LineParam.ValueType.COMPUTE;
                if (comboBox.SelectedIndex != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            else
            {
                try
                {
                    lineParamList[index].WtCtrlSpeedValueType = LineParam.ValueType.USER_EDIT;
                    lineParamList[index].WtCtrlSpeed = double.Parse(comboBox.Text);
                    if ((comboBox.Items[1] as string) != comboBox.Text)
                    {
                        comboBox.Items[1] = comboBox.Text;
                    }
                    if (comboBox.SelectedIndex != 1)
                    {
                        comboBox.SelectedIndex = 1;
                    }
                    comboBox.Select(comboBox.Text.Length, 0);
                }
                catch (Exception)
                {
                    // exception ignore.
                }
            }
        }

        private void cbType1WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType1WtCtrlSpeed, 0);
        }

        private void cbType2WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType2WtCtrlSpeed, 1);
        }

        private void cbType3WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType3WtCtrlSpeed, 2);
        }

        private void cbType4WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType4WtCtrlSpeed, 3);
        }

        private void cbType5WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType5WtCtrlSpeed, 4);
        }

        private void cbType6WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType6WtCtrlSpeed, 5);
        }

        private void cbType7WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType7WtCtrlSpeed, 6);
        }

        private void cbType8WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType8WtCtrlSpeed, 7);
        }

        private void cbType9WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType9WtCtrlSpeed, 8);
        }

        private void cbType10WtCtrlSpeed_TextChanged(object sender, EventArgs e)
        {
            processWtCtrlSpeedTextChanged(cbType10WtCtrlSpeed, 9);
        }

        /// <summary>
        /// 对 Accel Distance ComboBox内容发生变化事件的处理
        /// </summary>
        private void processAccelDistanceTextChanged(ComboBox comboBox, int index)
        {
            if (comboBox.Text == "Compute")
            {
                lineParamList[index].AccelDistanceValueType = LineParam.ValueType.COMPUTE;
                if (comboBox.SelectedIndex != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            else
            {
                try
                {
                    lineParamList[index].AccelDistance = double.Parse(comboBox.Text);
                    lineParamList[index].AccelDistanceValueType = LineParam.ValueType.USER_EDIT;
                    if ((comboBox.Items[1] as string) != comboBox.Text)
                    {
                        comboBox.Items[1] = comboBox.Text;
                    }
                    if (comboBox.SelectedIndex != 1)
                    {
                        comboBox.SelectedIndex = 1;
                    }
                    comboBox.Select(comboBox.Text.Length, 0);
                }
                catch (Exception)
                {
                    // exception ignore.
                }
            }
        }

        private void cbType1AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType1AccelDistance, 0);
        }

        private void cbType2AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType2AccelDistance, 1);
        }

        private void cbType3AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType3AccelDistance, 2);
        }

        private void cbType4AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType4AccelDistance, 3);
        }

        private void cbType5AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType5AccelDistance, 4);
        }

        private void cbType6AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType6AccelDistance, 5);
        }

        private void cbType7AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType7AccelDistance, 6);
        }

        private void cbType8AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType8AccelDistance, 7);
        }

        private void cbType9AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType9AccelDistance, 8);
        }

        private void cbType10AccelDistance_TextChanged(object sender, EventArgs e)
        {
            processAccelDistanceTextChanged(cbType10AccelDistance, 9);
        }

        /// <summary>
        /// 对 Decel Distance ComboBox内容发生变化事件的处理
        /// </summary>
        private void processDecelDistanceTextChanged(ComboBox comboBox, int index)
        {
            if (comboBox.Text == "Compute")
            {
                lineParamList[index].DecelDistanceValueType = LineParam.ValueType.COMPUTE;
                if (comboBox.SelectedIndex != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
            else
            {
                try
                {
                    lineParamList[index].DecelDistance = double.Parse(comboBox.Text);
                    lineParamList[index].DecelDistanceValueType = LineParam.ValueType.USER_EDIT;
                    if ((comboBox.Items[1] as string) != comboBox.Text)
                    {
                        comboBox.Items[1] = comboBox.Text;
                    }
                    if (comboBox.SelectedIndex != 1)
                    {
                        comboBox.SelectedIndex = 1;
                    }
                    comboBox.Select(comboBox.Text.Length, 0);
                }
                catch (Exception)
                {
                    // exception ignore.
                }
            }
        }

        private void cbType1DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType1DecelDistance, 0);
        }

        private void cbType2DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType2DecelDistance, 1);
        }

        private void cbType3DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType3DecelDistance, 2);
        }

        private void cbType4DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType4DecelDistance, 3);
        }

        private void cbType5DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType5DecelDistance, 4);
        }

        private void cbType6DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType6DecelDistance, 5);
        }

        private void cbType7DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType7DecelDistance, 6);
        }

        private void cbType8DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType8DecelDistance, 7);
        }

        private void cbType9DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType9DecelDistance, 8);
        }

        private void cbType10DecelDistance_TextChanged(object sender, EventArgs e)
        {
            processDecelDistanceTextChanged(cbType10DecelDistance, 9);
        }

        private void tbType1RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].RetractDistance = tbType1RetractDis.Value;
        }

        private void tbType2RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].RetractDistance = tbType2RetractDis.Value;
        }

        private void tbType3RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].RetractDistance = tbType3RetractDis.Value;
        }

        private void tbType4RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].RetractDistance = tbType4RetractDis.Value;
        }

        private void tbType5RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].RetractDistance = tbType5RetractDis.Value;
        }

        private void tbType6RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].RetractDistance = tbType6RetractDis.Value;
        }

        private void tbType7RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].RetractDistance = tbType7RetractDis.Value;
        }

        private void tbType8RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].RetractDistance = tbType8RetractDis.Value;
        }

        private void tbType9RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].RetractDistance = tbType9RetractDis.Value;
        }

        private void tbType10RetractDis_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].RetractDistance = tbType10RetractDis.Value;
        }

        private void tbType1RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].RetractSpeed = tbType1RetractSpeed.Value;
        }

        private void tbType2RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].RetractSpeed = tbType2RetractSpeed.Value;
        }

        private void tbType3RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].RetractSpeed = tbType3RetractSpeed.Value;
        }

        private void tbType4RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].RetractSpeed = tbType4RetractSpeed.Value;
        }

        private void tbType5RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].RetractSpeed = tbType5RetractSpeed.Value;
        }

        private void tbType6RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].RetractSpeed = tbType6RetractSpeed.Value;
        }

        private void tbType7RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].RetractSpeed = tbType7RetractSpeed.Value;
        }

        private void tbType8RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].RetractSpeed = tbType8RetractSpeed.Value;
        }

        private void tbType9RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].RetractSpeed = tbType9RetractSpeed.Value;
        }

        private void tbType10RetractSpeed_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].RetractSpeed = tbType10RetractSpeed.Value;
        }

        private void tbType1RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].RetractAccel = tbType1RetractAccel.Value;
        }

        private void tbType2RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].RetractAccel = tbType2RetractAccel.Value;
        }

        private void tbType3RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].RetractAccel = tbType3RetractAccel.Value;
        }

        private void tbType4RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].RetractAccel = tbType4RetractAccel.Value;
        }

        private void tbType5RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].RetractAccel = tbType5RetractAccel.Value;
        }

        private void tbType6RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].RetractAccel = tbType6RetractAccel.Value;
        }

        private void tbType7RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].RetractAccel = tbType7RetractAccel.Value;
        }

        private void tbType8RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].RetractAccel = tbType8RetractAccel.Value;
        }

        private void tbType9RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].RetractAccel = tbType9RetractAccel.Value;
        }

        private void tbType10RetractAccel_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].RetractAccel = tbType10RetractAccel.Value;
        }

        private void cbType1ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType1ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[0].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType1Spacing.Enabled = true;
                    tbType1ShotTimeInterval.Enabled = false;
                    tbType1TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[0].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType1Spacing.Enabled = false;
                    tbType1ShotTimeInterval.Enabled = true;
                    tbType1TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[0].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType1Spacing.Enabled = false;
                    tbType1ShotTimeInterval.Enabled = false;
                    tbType1TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[0].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType1Spacing.Enabled = false;
                    tbType1ShotTimeInterval.Enabled = false;
                    tbType1TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType2ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType2ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[1].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType2Spacing.Enabled = true;
                    tbType2ShotTimeInterval.Enabled = false;
                    tbType2TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[1].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType2Spacing.Enabled = false;
                    tbType2ShotTimeInterval.Enabled = true;
                    tbType2TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[1].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType2Spacing.Enabled = false;
                    tbType2ShotTimeInterval.Enabled = false;
                    tbType2TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[1].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType2Spacing.Enabled = false;
                    tbType2ShotTimeInterval.Enabled = false;
                    tbType2TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType3ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType3ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[2].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType3Spacing.Enabled = true;
                    tbType3ShotTimeInterval.Enabled = false;
                    tbType3TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[2].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType3Spacing.Enabled = false;
                    tbType3ShotTimeInterval.Enabled = true;
                    tbType3TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[2].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType3Spacing.Enabled = false;
                    tbType3ShotTimeInterval.Enabled = false;
                    tbType3TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[2].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType3Spacing.Enabled = false;
                    tbType3ShotTimeInterval.Enabled = false;
                    tbType3TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType4ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType4ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[3].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType4Spacing.Enabled = true;
                    tbType4ShotTimeInterval.Enabled = false;
                    tbType4TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[3].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType4Spacing.Enabled = false;
                    tbType4ShotTimeInterval.Enabled = true;
                    tbType4TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[3].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType4Spacing.Enabled = false;
                    tbType4ShotTimeInterval.Enabled = false;
                    tbType4TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[3].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType4Spacing.Enabled = false;
                    tbType4ShotTimeInterval.Enabled = false;
                    tbType4TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType5ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType5ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[4].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType5Spacing.Enabled = true;
                    tbType5ShotTimeInterval.Enabled = false;
                    tbType5TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[4].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType5Spacing.Enabled = false;
                    tbType5ShotTimeInterval.Enabled = true;
                    tbType5TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[4].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType5Spacing.Enabled = false;
                    tbType5ShotTimeInterval.Enabled = false;
                    tbType5TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[4].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType5Spacing.Enabled = false;
                    tbType5ShotTimeInterval.Enabled = false;
                    tbType5TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType6ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType6ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[5].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType6Spacing.Enabled = true;
                    tbType6ShotTimeInterval.Enabled = false;
                    tbType6TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[5].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType6Spacing.Enabled = false;
                    tbType6ShotTimeInterval.Enabled = true;
                    tbType6TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[5].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType6Spacing.Enabled = false;
                    tbType6ShotTimeInterval.Enabled = false;
                    tbType6TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[5].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType6Spacing.Enabled = false;
                    tbType6ShotTimeInterval.Enabled = false;
                    tbType6TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType7ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType7ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[6].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType7Spacing.Enabled = true;
                    tbType7ShotTimeInterval.Enabled = false;
                    tbType7TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[6].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType7Spacing.Enabled = false;
                    tbType7ShotTimeInterval.Enabled = true;
                    tbType7TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[6].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType7Spacing.Enabled = false;
                    tbType7ShotTimeInterval.Enabled = false;
                    tbType7TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[6].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType7Spacing.Enabled = false;
                    tbType7ShotTimeInterval.Enabled = false;
                    tbType7TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType8ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType8ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[7].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType8Spacing.Enabled = true;
                    tbType8ShotTimeInterval.Enabled = false;
                    tbType8TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[7].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType8Spacing.Enabled = false;
                    tbType8ShotTimeInterval.Enabled = true;
                    tbType8TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[7].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType8Spacing.Enabled = false;
                    tbType8ShotTimeInterval.Enabled = false;
                    tbType8TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[7].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType8Spacing.Enabled = false;
                    tbType8ShotTimeInterval.Enabled = false;
                    tbType8TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType9ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType9ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[8].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType9Spacing.Enabled = true;
                    tbType9ShotTimeInterval.Enabled = false;
                    tbType9TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[8].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType9Spacing.Enabled = false;
                    tbType9ShotTimeInterval.Enabled = true;
                    tbType9TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[8].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType9Spacing.Enabled = false;
                    tbType9ShotTimeInterval.Enabled = false;
                    tbType9TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[8].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType9Spacing.Enabled = false;
                    tbType9ShotTimeInterval.Enabled = false;
                    tbType9TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void cbType10ControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType10ControlMode.SelectedIndex)
            {
                case 0:
                    lineParamList[9].ControlMode = LineParam.CtrlMode.POS_BASED_SPACING;
                    tbType10Spacing.Enabled = true;
                    tbType10ShotTimeInterval.Enabled = false;
                    tbType10TotalOfDots.Enabled = false;
                    break;
                case 1:
                    lineParamList[9].ControlMode = LineParam.CtrlMode.TIME_BASED_SPACING;
                    tbType10Spacing.Enabled = false;
                    tbType10ShotTimeInterval.Enabled = true;
                    tbType10TotalOfDots.Enabled = false;
                    break;
                case 2:
                    lineParamList[9].ControlMode = LineParam.CtrlMode.TOTAL_OF_DOTS;
                    tbType10Spacing.Enabled = false;
                    tbType10ShotTimeInterval.Enabled = false;
                    tbType10TotalOfDots.Enabled = true;
                    break;
                case 3:
                    lineParamList[9].ControlMode = LineParam.CtrlMode.NO_DISPENSE;
                    tbType10Spacing.Enabled = false;
                    tbType10ShotTimeInterval.Enabled = false;
                    tbType10TotalOfDots.Enabled = false;
                    break;
            }
        }

        private void tbType1Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].Spacing = tbType1Spacing.Value;
        }

        private void tbType2Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].Spacing = tbType2Spacing.Value;
        }

        private void tbType3Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].Spacing = tbType3Spacing.Value;
        }

        private void tbType4Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].Spacing = tbType4Spacing.Value;
        }

        private void tbType5Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].Spacing = tbType5Spacing.Value;
        }

        private void tbType6Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].Spacing = tbType6Spacing.Value;
        }

        private void tbType7Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].Spacing = tbType7Spacing.Value;
        }

        private void tbType8Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].Spacing = tbType8Spacing.Value;
        }

        private void tbType9Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].Spacing = tbType9Spacing.Value;
        }

        private void tbType10Spacing_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].Spacing = tbType10Spacing.Value;
        }

        private void tbType1ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].ShotTimeInterval = tbType1ShotTimeInterval.Value;
        }

        private void tbType2ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].ShotTimeInterval = tbType2ShotTimeInterval.Value;
        }

        private void tbType3ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].ShotTimeInterval = tbType3ShotTimeInterval.Value;
        }

        private void tbType4ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].ShotTimeInterval = tbType4ShotTimeInterval.Value;
        }

        private void tbType5ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].ShotTimeInterval = tbType5ShotTimeInterval.Value;
        }

        private void tbType6ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].ShotTimeInterval = tbType6ShotTimeInterval.Value;
        }

        private void tbType7ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].ShotTimeInterval = tbType7ShotTimeInterval.Value;
        }

        private void tbType8ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].ShotTimeInterval = tbType8ShotTimeInterval.Value;
        }

        private void tbType9ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].ShotTimeInterval = tbType9ShotTimeInterval.Value;
        }

        private void tbType10ShotTimeInterval_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].ShotTimeInterval = tbType10ShotTimeInterval.Value;
        }

        private void tbType1TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].TotalOfDots = tbType1TotalOfDots.Value;
        }

        private void tbType2TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].TotalOfDots = tbType2TotalOfDots.Value;
        }

        private void tbType3TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].TotalOfDots = tbType3TotalOfDots.Value;
        }

        private void tbType4TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].TotalOfDots = tbType4TotalOfDots.Value;
        }

        private void tbType5TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].TotalOfDots = tbType5TotalOfDots.Value;
        }

        private void tbType6TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].TotalOfDots = tbType6TotalOfDots.Value;
        }

        private void tbType7TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].TotalOfDots = tbType7TotalOfDots.Value;
        }

        private void tbType8TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].TotalOfDots = tbType8TotalOfDots.Value;
        }

        private void tbType9TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].TotalOfDots = tbType9TotalOfDots.Value;
        }

        private void tbType10TotalOfDots_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].TotalOfDots = tbType10TotalOfDots.Value;
        }

        private void tbType1Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[0].Offset = tbType1Offset.Value;
        }

        private void tbType2Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[1].Offset = tbType2Offset.Value;
        }

        private void tbType3Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[2].Offset = tbType3Offset.Value;
        }

        private void tbType4Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[3].Offset = tbType4Offset.Value;
        }

        private void tbType5Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[4].Offset = tbType5Offset.Value;
        }

        private void tbType6Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[5].Offset = tbType6Offset.Value;
        }

        private void tbType7Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[6].Offset = tbType7Offset.Value;
        }

        private void tbType8Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[7].Offset = tbType8Offset.Value;
        }

        private void tbType9Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[8].Offset = tbType9Offset.Value;
        }

        private void tbType10Offset_TextChanged(object sender, EventArgs e)
        {
            lineParamList[9].Offset = tbType10Offset.Value;
        }

        private void PreMoveDelay_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 25);
            lineParamList[index].PreMoveDelay = tx.Value/1000;
        }

        private void ShutOffDistance_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 14);
            lineParamList[index].ShutOffDistance = tx.Value;
        }

        private void SuckBackTime_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 26);
            lineParamList[index].SuckBackTime = tx.Value;
        }

        private void DwellTime_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 38);
            lineParamList[index].Dwell = tx.Value / 1000;
        }

        private void BackTrackGap_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 50);
            lineParamList[index].BacktrackGap = tx.Value;
        }

        private void BackTrackDistance_TextChanged(object sender, EventArgs e)
        {
            PercentTextBox tx = (PercentTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 65);
            lineParamList[index].BacktrackDistance = Convert.ToDouble(tx.Text);
        }

        private void BackTrackSpeed_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 62);
            lineParamList[index].BacktrackSpeed = tx.Value;
        }

        private void PressDistance_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 50);
            lineParamList[index].PressDistance = tx.Value;
        }

        private void PressSpeed_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 76);
            lineParamList[index].PressSpeed = tx.Value;
        }

        private void PressAccel_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 86);
            lineParamList[index].PressAccel = tx.Value;
        }

        private void PressTime_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 134);
            lineParamList[index].PressTime = tx.Value;
        }

        private void RaiseDistance_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 98);
            lineParamList[index].RaiseDistance = tx.Value;
        }

        private void RaiseSpeed_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 112);
            lineParamList[index].RaiseSpeed = tx.Value;
        }

        private void RaiseAccel_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 122);
            lineParamList[index].RaiseAccel = tx.Value;
        }

        private void BacktrackEndGap_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 86);
            lineParamList[index].BacktrackEndGap = tx.Value;
        }

        private void BackGap_TextChanged(object sender, EventArgs e)
        {
            DoubleTextBox tx = (DoubleTextBox)sender;
            int index = Math.Abs(tx.TabIndex - 89);
            lineParamList[index].BackGap = tx.Value;
        }

        private void EditLineParamsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.lineParamList != null && this.lineParamListBackUp != null)
            {
                for (int i = 0; i < this.lineParamList.Count; i++)
                {
                    CompareObj.CompareField(this.lineParamList[i], this.lineParamListBackUp[i], null, null);
                }
            }

        }
    }
}
