using Anda.Fluid.Drive.Vision.Barcode;
using Anda.Fluid.Infrastructure.Utils;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.App.Test
{
    public partial class BarCode : Form
    {
        private Bitmap bmp;
        private BarcodeHalcon barcode = new BarcodeHalcon();
        public BarCode()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
        string pathStr;
        private void btnRead_Click(object sender, EventArgs e)
        {
            string path = @"E:\Img\8m.png";
            if (pathStr==path)
            {
                return;
                
            }
            this.pathStr = path;
           
            FileStream fs = new FileStream(pathStr, FileMode.Open);
            this.bmp = Bitmap.FromStream(fs) as Bitmap;
            fs.Close();
            this.pictureBox1.Image = this.bmp;

            HObject ho_Image;
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            //HOperatorSet.ReadImage(out ho_Image, "E:/Img/281.png");
            //ho_Image = Barcode_Halcon.getGrayHImageFromBitmap(this.bmp);
            //ho_Image = Barcode_Halcon.getRGBHImageFromBitmap(this.bmp);

            //ho_Image = Barcode_Halcon.BitmapToHImage(this.bmp);
            ho_Image=this.bmp.BitmapRGBToHImage();
            HTuple width, height;
            HOperatorSet.GetImageSize(ho_Image, out width, out height);
            HOperatorSet.SetPart(this.hWindowControl1.HalconWindow,0, 0, height, width);
            HOperatorSet.DispObj(ho_Image, this.hWindowControl1.HalconWindow);
        }
        /// <summary>
        /// 解析条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecode_Click(object sender, EventArgs e)
        {
            
            string result = string.Empty;
            barcode.Execute(this.bmp, out result);
            this.lblContent.Text = result;

        }
        
        
    }
}
