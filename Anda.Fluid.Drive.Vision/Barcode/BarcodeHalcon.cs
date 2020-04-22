using Anda.Fluid.Infrastructure.Utils;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Drive.Vision.Barcode
{
    public class BarcodeHalcon
    {
        public HTuple codeType;

        public bool Execute(Bitmap bmp, out string result)
        {
            result = string.Empty;
            try
            {
                HObject hObj;
                HOperatorSet.GenEmptyObj(out hObj);
                hObj.Dispose();
                hObj = bmp.BitmapRGBToHImage();
                //hObj = bmp.BitmapToHImage();
                this.findCode(hObj, new HTuple("Data Matrix ECC 200"), out result);
                if (!string.IsNullOrEmpty(result))
                {
                    return true;
                }
                this.findCode(hObj, new HTuple("QR Code"), out result);
                if (string.IsNullOrEmpty(result))
                {
                    return false;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }


        }
        private void findCode(HObject hObj, HTuple codeType, out string result)
        {
            // Local iconic variables 
            HObject ho_Image,  ho_SymbolXLDs;
            //HObject ho_ImageZoomed = null;
            // Local control variables 
            HTuple hv_DataCodeHandle = null, hv_ResultHandles = null;
            HTuple hv_DecodedDataStrings = null;
            HTuple hv_Width = null, hv_Height = null;

            // Initialize local and output iconic variables   
            HOperatorSet.GenEmptyObj(out ho_Image);
            //HOperatorSet.GenEmptyObj(out ho_ImageZoomed);
            HOperatorSet.GenEmptyObj(out ho_SymbolXLDs);

            HOperatorSet.CreateDataCode2dModel(codeType, "default_parameters", "maximum_recognition", out hv_DataCodeHandle);
            ho_Image.Dispose();

            ho_Image = hObj.Clone();
            //HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            ////int width = hv_Width;
            //int height = hv_Height;
            //if (hv_Width>2000 || height>2000)
            //{
            //    ho_ImageZoomed.Dispose();
            //    HOperatorSet.ZoomImageFactor(ho_Image, out ho_ImageZoomed, 0.5, 0.5, "bilinear");
            //}
            //else
            //{
            //    ho_ImageZoomed.Dispose();
            //    HOperatorSet.ZoomImageFactor(ho_Image, out ho_ImageZoomed, 1, 1, "bilinear");
            //}
            //Read the symbol in the image
            ho_SymbolXLDs.Dispose();
            HOperatorSet.FindDataCode2d(ho_Image, out ho_SymbolXLDs, hv_DataCodeHandle, new HTuple(),
                new HTuple(), out hv_ResultHandles, out hv_DecodedDataStrings);
            //Clear the model
            HOperatorSet.ClearDataCode2dModel(hv_DataCodeHandle);
            ho_Image.Dispose();
            //ho_ImageZoomed.Dispose();
            ho_SymbolXLDs.Dispose();

            result = hv_DecodedDataStrings.ToString();
        }
    }
}
