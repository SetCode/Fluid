using Anda.Fluid.Drive.Sensors.Lighting;
using Anda.Fluid.Drive.Vision.Halcon;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewROI;

namespace Anda.Fluid.App.EditHalcon
{
    public partial class EditBlobsForm : EditHalconFormBase, IHalconEditable
    {
        private const int EVENT_THRESHOLD_MIN_CHANGED = 0;
        private const int EVENT_THRESHOLD_MAX_CHANGED = 1;
        private const int EVENT_AREA_MIN_CHANGED = 2;
        private const int EVENT_AREA_MAX_CHANGED = 3;
        private const int EVENT_FILLED_CHANGED = 4;

        private BlobsTool blobs;

        private Blob selectedBlob;

        public EditBlobsForm(BlobsTool blobs, bool showResult = false) : base(showResult)
        {
            InitializeComponent();
            this.tbrThresholdMin.Maximum = 255;
            this.tbrThresholdMax.Maximum = 255;
            this.lvwBlobs.Columns.Add("Area", -1, HorizontalAlignment.Left);
            if (blobs == null)
            {
                this.blobs = new BlobsTool();
            }
            else
            {
                this.blobs = blobs;
            }
            this.setupBlobs(showResult);
        }

        public EditBlobsForm() : this(null)
        {

        }

        private void setupBlobs(bool showResult)
        {
            //参数
            this.nudSettlingTime.Value = this.blobs.SettlingTime > this.nudSettlingTime.Maximum ? this.nudSettlingTime.Maximum : this.blobs.SettlingTime;
            if (this.blobs.ExecutePrm == null) this.blobs.ExecutePrm = new ExecutePrm();
            this.andaLightControl1.Setup(this.blobs.ExposureTime, this.blobs.Gain, this.blobs.ExecutePrm.LightType);
            //ROI显示
            mView.clearAllObjsAndRegions();
            mView.clearAllROI();
            mView.resetWindow();
            foreach (var item in this.blobs.List)
            {
                roiController.setROIShape(item.ROI);
                roiController.addROI(item.ROI);
            }
            this.referenceImage = this.blobs.ReferenceImageData?.ToHImage(this.blobs.ImgWidth, this.blobs.ImgHeight);
            //结果显示
            if (showResult)
            {
                mView.HRegionList.Clear();
                foreach (var blob in this.blobs.List)
                {
                    mView.HRegionList.Add(blob.ResultRegion);
                }
            }
            //当前图像显示
            if (this.blobs.CurrentHImage != null)
            {
                mView.setImage(this.blobs.CurrentHImage);
            }
            mView.repaint();
        }

        public BlobsTool GetBlobs() => this.blobs;

        public void CreateROI(ROI roi)
        {
            this.blobs.List.Add(new Blob() { ROI = roi });
        }

        public void RegisterRefImage(HImage image)
        {
            if (this.referenceImage == null) return;
            this.blobs.ReferenceImageData = this.referenceImage.ToBytes();
            HTuple w, h;
            this.referenceImage.GetImageSize(out w, out h);
            this.blobs.ImgWidth = w;
            this.blobs.ImgHeight = h;
        }

        public HRegion Execute(ROI roi)
        {
            Blob blob = this.blobs.List.Find(x => x.ROI == roi);
            if (blob == null) return null;
            blob.CurrentImage = this.tempImage;
            if (blob.Execute())
            {
                return blob.ResultRegion;
            }  
            return null;
        }

        public List<HRegion> ExecuteAll()
        {
            List<HRegion> regions = new List<HRegion>();
            foreach (var blob in this.blobs.List)
            {
                blob.CurrentImage = this.tempImage;
                if (blob.Execute())
                {
                    regions.Add(blob.ResultRegion);
                }
            }
            return regions;
        }

        public void SelectROI(ROI roi)
        {
            this.selectedBlob = this.blobs.List.Find(x => x.ROI == roi);
            if (this.selectedBlob == null) return;

            this.chxFilled.Checked = selectedBlob.Filled;

            this.tbrThresholdMin.Value = selectedBlob.MinThreshold;
            this.tbrThresholdMax.Value = selectedBlob.MaxThreshold;
            this.lblThresholdMin.Text = this.tbrThresholdMin.Value.ToString();
            this.lblThresholdMax.Text = this.tbrThresholdMax.Value.ToString();

            this.tbrAreaMin.Maximum = selectedBlob.ROI.getRegion().Area;
            this.tbrAreaMax.Maximum = selectedBlob.ROI.getRegion().Area;
            this.tbrAreaMin.Value = selectedBlob.MinArea > this.tbrAreaMin.Maximum ? this.tbrAreaMin.Maximum : selectedBlob.MinArea;
            this.tbrAreaMax.Value = selectedBlob.MaxArea > this.tbrAreaMax.Maximum ? this.tbrAreaMax.Maximum : selectedBlob.MaxArea;
            this.lblAreaMin.Text = this.tbrAreaMin.Value.ToString();
            this.lblAreaMax.Text = this.tbrAreaMax.Value.ToString();
        }

        public void OnOkClicked()
        {
            this.blobs.ExposureTime = this.andaLightControl1.ExposureTime;
            this.blobs.Gain = this.andaLightControl1.Gain;
            if (this.blobs.ExecutePrm == null) this.blobs.ExecutePrm = new ExecutePrm();
            this.blobs.ExecutePrm.LightType = this.andaLightControl1.LightType;
            this.blobs.SettlingTime = (int)this.nudSettlingTime.Value;
        }

        private void tbrThresholdMin_Scroll(object sender, EventArgs e)
        {
            this.sendEvent(EVENT_THRESHOLD_MIN_CHANGED);
        }

        private void tbrThresholdMax_Scroll(object sender, EventArgs e)
        {
            this.sendEvent(EVENT_THRESHOLD_MAX_CHANGED);
        }

        private void tbrAreaMin_Scroll(object sender, EventArgs e)
        {
            this.sendEvent(EVENT_AREA_MIN_CHANGED);
        }

        private void tbrAreaMax_Scroll(object sender, EventArgs e)
        {
            this.sendEvent(EVENT_AREA_MAX_CHANGED);
        }

        private void chxFilled_CheckedChanged(object sender, EventArgs e)
        {
            this.sendEvent(EVENT_FILLED_CHANGED);
        }

        private void sendEvent(int eventId)
        {
            if (this.selectedBlob == null) return;
            switch (eventId)
            {
                case EVENT_THRESHOLD_MIN_CHANGED:
                    this.selectedBlob.MinThreshold = this.tbrThresholdMin.Value;
                    this.lblThresholdMin.Text = this.tbrThresholdMin.Value.ToString();
                    break;
                case EVENT_THRESHOLD_MAX_CHANGED:
                    this.selectedBlob.MaxThreshold = this.tbrThresholdMax.Value;
                    this.lblThresholdMax.Text = this.tbrThresholdMax.Value.ToString();
                    break;
                case EVENT_AREA_MIN_CHANGED:
                    this.selectedBlob.MinArea = this.tbrAreaMin.Value;
                    this.lblAreaMin.Text = this.tbrAreaMin.Value.ToString();
                    break;
                case EVENT_AREA_MAX_CHANGED:
                    this.selectedBlob.MaxArea = this.tbrAreaMax.Value;
                    this.lblAreaMax.Text = this.tbrAreaMax.Value.ToString();
                    break;
                case EVENT_FILLED_CHANGED:
                    this.selectedBlob.Filled = this.chxFilled.Checked;
                    break;
            }
            this.roiController?.NotifyRCObserver(ROIController.EVENT_UPDATE_ROI);

            this.lvwBlobs.Clear();
            if (this.selectedBlob.ResultRegion == null) return;
            string[] resultStrings = this.selectedBlob.ResultRegion.Area.ToString().Split(';');
            foreach (var item in resultStrings)
            {
                this.lvwBlobs.Items.Add(item);
            }
        }

        private void btnLoadBlobs_Click(object sender, EventArgs e)
        {
            this.loadBlobs("E:\\blobs");
            this.setupBlobs(false);
        }

        private void btnSaveBlobs_Click(object sender, EventArgs e)
        {
            this.saveBlobs("E:\\blobs");
        }

        private bool saveBlobs(string filePath)
        {
            Stream fstream = null;
            try
            {
                fstream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryFormatter binFormat = new BinaryFormatter();
                binFormat.Serialize(fstream, this.blobs);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                fstream.Close();
            }
        }

        private bool loadBlobs(string filePath)
        {
            Stream fstream = null;
            try
            {
                fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryFormatter binFormat = new BinaryFormatter();
                this.blobs = (BlobsTool)binFormat.Deserialize(fstream);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                fstream.Close();
            }
        }
    }
}
