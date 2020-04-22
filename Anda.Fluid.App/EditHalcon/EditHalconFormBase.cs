using Anda.Fluid.Drive;
using Anda.Fluid.Drive.Vision.Halcon;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewROI;

namespace Anda.Fluid.App.EditHalcon
{
    public partial class EditHalconFormBase : Form
    {
        protected HWindowControl hWindow;
        /// <summary>Window control that manages visualization</summary>
        protected HWndCtrl mView;
        /// <summary>ROI control that manages ROI objects</summary>
		protected ROIController roiController;
        /// <summary>
        /// 当前图像：从文件或相机
        /// </summary>
        protected HImage currentImage;
        /// <summary>
        /// 参考图像：由当前图像注册
        /// </summary>
        protected HImage referenceImage;
        /// <summary>
        /// 临时图像：参考图像或当前图像
        /// </summary>
        protected HImage tempImage;
        /// <summary>
        /// 当前激活的ROI
        /// </summary>
        protected ROI activeROI;
        /// <summary>
        /// 选中即将新建的ROI
        /// </summary>
        protected ROI newROI;

        private Color createModelWindowMode;
        private Color trainModelWindowMode;

        private int slectedTabIndex;

        protected IHalconEditable halconEditable;

        public EditHalconFormBase(bool showResult)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            hWindow = new HWindowControl();
            hWindow.Dock = DockStyle.Fill;
            this.tabCurrent.Controls.Clear();
            this.tabCurrent.Controls.Add(hWindow);

            mView = new HWndCtrl(this.hWindow);
            mView.changeGraphicSettings(GraphicsContext.GC_LINESTYLE, new HTuple());

            createModelWindowMode = Color.RoyalBlue;
            trainModelWindowMode = Color.Chartreuse;

            roiController = new ROIController();
            roiController.ROISelected += RoiController_ROISelected;
            roiController.setROISign(ROIController.MODE_ROI_POS);

            mView.NotifyIconObserver = new IconicDelegate(UpdateViewData);
            roiController.NotifyRCObserver = new IconicDelegate(UpdateViewData);

            mView.useROIController(roiController);
            mView.setViewState(HWndCtrl.MODE_VIEW_ZOOM_MOVE);

            halconEditable = this as IHalconEditable;

            this.nudSettlingTime.Maximum = 1000;

            this.btnStop.Visible = showResult;
            this.btnIgnore.Visible = showResult;
            this.chxFixedROI.Checked = showResult;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hWindow == null) return;
            switch (this.tabControl1.SelectedIndex)
            {
                case 1:
                    this.tabCurrent.Controls.Clear();
                    this.tabCurrent.Controls.Add(hWindow);
                    HImage hImage = Machine.Instance.Camera.Executor.CurrentBytes.ToHImage(
                        Machine.Instance.Camera.Executor.ImageWidth,
                        Machine.Instance.Camera.Executor.ImageHeight);
                    this.currentImage = hImage;
                    this.tempImage = this.currentImage;
                    break;
                case 2:
                    this.tabReference.Controls.Clear();
                    this.tabReference.Controls.Add(hWindow);
                    this.tempImage = this.referenceImage;
                    break;
            }
            this.slectedTabIndex = this.tabControl1.SelectedIndex;
            mView.resetWindow();
            mView.HRegionList.Clear();
            mView.setImage(this.tempImage);
            mView.repaint();
        }

        private void RoiController_ROISelected(int obj)
        {
            this.activeROI = roiController.getActiveROI();
            this.halconEditable?.SelectROI(this.activeROI);
        }

        private void UpdateViewData(int val)
        {
            switch (val)
            {
                case ROIController.EVENT_CHANGED_ROI_SIGN:
                case ROIController.EVENT_DELETED_ACTROI:
                case ROIController.EVENT_DELETED_ALL_ROIS:
                case ROIController.EVENT_CREATED_ROI:
                    if (this.newROI != null)
                        this.halconEditable?.CreateROI(this.newROI);
                    break;
                case ROIController.EVENT_UPDATE_ROI:
                    this.showROIResult();
                    break;
                case ROIController.EVENT_RESET_ROI:
                    this.clearROIResult();
                    break;
                case HWndCtrl.ERR_READING_IMG:
                    MessageBox.Show("Problem occured while reading file! \n" + mView.exceptionText,
                        "Matching assistant",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }

        private void showROIResult()
        {
            if (this.tempImage != null && this.activeROI != null)
            {
                HRegion hRegion = this.halconEditable?.Execute(this.activeROI);
                mView.HRegionList.Clear();
                if (hRegion != null)
                {
                    mView.HRegionList.Add(hRegion);
                }
                mView.repaint();
            }
        }

        private void clearROIResult()
        {
            mView.HRegionList.Clear();
            mView.repaint();
        }

        private void btnFileImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mView.clearAllObjsAndRegions();
                mView.resetWindow();
                //mView.clearAllROI();
                this.currentImage = new HImage(ofd.FileName);
                this.tempImage = this.currentImage;
                mView.setImage(this.tempImage);
                mView.repaint();
            }
        }

        private void btnZoomReset_Click(object sender, EventArgs e)
        {
            mView.resetWindow();
            mView.repaint();
        }

        private void btnClearAllROI_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清空所有ROI？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                mView.clearAllROI();
                mView.repaint();
            }
        }

        private void btnRect1_Click(object sender, EventArgs e)
        {
            this.newROI = new ROIRectangle1();
            roiController.setROIShape(this.newROI);
        }

        private void btnRect2_Click(object sender, EventArgs e)
        {
            this.newROI = new ROIRectangle2();
            roiController.setROIShape(this.newROI);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            mView.HRegionList.Clear();
            List<HRegion> regions = this.halconEditable?.ExecuteAll();
            foreach (var region in regions)
            {
                mView.HRegionList.Add(region);
            }
            mView.repaint();
        }

        private void btnRegistImage_Click(object sender, EventArgs e)
        {
            if (this.currentImage == null)
            {
                MessageBox.Show("当前图像为空！");
                return;
            }
            this.referenceImage = this.currentImage;
            this.halconEditable?.RegisterRefImage(this.referenceImage);
        }

        private void chxFixedROI_CheckedChanged(object sender, EventArgs e)
        {
            mView.setROIEnabled(this.chxFixedROI.Checked);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.halconEditable?.OnOkClicked();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
    }
}
