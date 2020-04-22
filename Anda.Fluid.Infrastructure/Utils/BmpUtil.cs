using Anda.Fluid.Infrastructure.Trace;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anda.Fluid.Infrastructure.Utils
{
    public static class BmpUtil
    {
        public static byte[] ToBytes(this Bitmap bmp)
        {
            try
            {
                BitmapData bmpdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpdata.Scan0;
                int byteLength = bmpdata.Stride * bmpdata.Height;
                byte[] bytes = new byte[byteLength];
                Marshal.Copy(ptr, bytes, 0, byteLength);
                bmp.UnlockBits(bmpdata);
                return bytes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Bitmap ToBitmap(this byte[] bytes, int width, int height)
        {
            try
            {
                Logger.DEFAULT.Info(LogCategory.RUNNING, "begin bytes to bitmap.");
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                // Lock the bits of the bitmap.
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptrBmp = bmpData.Scan0;
                //palette
                ColorPalette palette = bmp.Palette;
                for (int i = 0; i < 256; ++i)
                {
                    palette.Entries[i] = Color.FromArgb(255, i, i, i);
                }
                bmp.Palette = palette;
                //copy to bmp ptr
                Marshal.Copy(bytes, 0, ptrBmp, bytes.Length);
                bmp.UnlockBits(bmpData);
                Logger.DEFAULT.Info(LogCategory.RUNNING, "end bytes to bitmap.");
                return bmp;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap DeepClone(this Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }
            Bitmap dstBitmap = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mStream, bitmap);
                mStream.Seek(0, SeekOrigin.Begin);
                dstBitmap = (Bitmap)bf.Deserialize(mStream);
                mStream.Close();
            }
            return dstBitmap;
        }

        public static byte[] DeepClone(this byte[] bytes)
        {
            if(bytes == null)
            {
                return null;
            }
            byte[] dstBytes = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                dstBytes[i] = bytes[i];
            }
            return dstBytes;
        }
        #region bmp to HObject
        /// <summary>
        /// 对于高、宽非整除4的图像
        /// </summary>
        /// <param name="SrcImage"></param>
        /// <returns></returns>
        public static HObject BitmapToHImage(this Bitmap SrcImage)
        {
            try
            {
                HObject Hobj;
                HOperatorSet.GenEmptyObj(out Hobj);

                Point po = new Point(0, 0);
                Size so = new Size(SrcImage.Width, SrcImage.Height);//template.Width, template.Height
                Rectangle ro = new Rectangle(po, so);

                Bitmap DstImage = new Bitmap(SrcImage.Width, SrcImage.Height, PixelFormat.Format8bppIndexed);
                //DstImage = SrcImage.Clone(ro, PixelFormat.Format8bppIndexed);
                DstImage = SrcImage;
                int width = DstImage.Width;
                int height = DstImage.Height;

                Rectangle rect = new Rectangle(0, 0, width, height);
                System.Drawing.Imaging.BitmapData dstBmpData =
                    DstImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);//pImage.PixelFormat
                int PixelSize = Bitmap.GetPixelFormatSize(dstBmpData.PixelFormat) / 8;
                int stride = dstBmpData.Stride;

                //重点在此
                unsafe
                {
                    int count = height * width;
                    byte[] data = new byte[count];
                    byte* bptr = (byte*)dstBmpData.Scan0;
                    fixed (byte* pData = data)
                    {
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                            {
                                data[i * width + j] = bptr[i * stride + j];
                            }
                        HOperatorSet.GenImage1(out Hobj, "byte", width, height, new IntPtr(pData));
                    }
                }

                DstImage.UnlockBits(dstBmpData);

                return Hobj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            
        }
        /// <summary>
        /// Bitmap 24位 转 HObject
        /// </summary>
        /// <param name="SrcImage"></param>
        /// <returns></returns>
        public static HObject BitmapRGBToHImage(this Bitmap SrcImage)
        {
            HObject Hobj;
            HOperatorSet.GenEmptyObj(out Hobj);

            Bitmap DstImage = new Bitmap(SrcImage.Width, SrcImage.Height, PixelFormat.Format8bppIndexed);

            DstImage = SrcImage;
            int width = DstImage.Width;
            int height = DstImage.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            System.Drawing.Imaging.BitmapData dstBmpData =
                DstImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);//pImage.PixelFormat
            //int PixelSize = Bitmap.GetPixelFormatSize(dstBmpData.PixelFormat) / 8;
            int stride = dstBmpData.Stride;

            byte[] arrayR = new byte[dstBmpData.Width * dstBmpData.Height];//红色数组 
            byte[] arrayG = new byte[dstBmpData.Width * dstBmpData.Height];//绿色数组 
            byte[] arrayB = new byte[dstBmpData.Width * dstBmpData.Height];//蓝色数组 

            //重点在此
            unsafe
            {
                int count = height * width;
                byte[] data = new byte[count];
                byte* pBmp = (byte*)dstBmpData.Scan0;

                for (int R = 0; R < height; R++)
                {
                    for (int C = 0; C < width; C++)
                    {
                        //因为内存BitMap的储存方式，行宽用Stride算，C*3是因为这是三通道，另外BitMap是按BGR储存的 
                        byte* pBase = pBmp + stride * R + C * 3;
                        arrayR[R * width + C] = *(pBase + 2);
                        arrayG[R * width + C] = *(pBase + 1);
                        arrayB[R * width + C] = *(pBase);
                    }
                }

                fixed (byte* pR = arrayR, pG = arrayG, pB = arrayB)
                {
                    HOperatorSet.GenImage3(out Hobj, "byte", width, height, new IntPtr(pR), new IntPtr(pG), new IntPtr(pB));
                }
            }

            DstImage.UnlockBits(dstBmpData);

            return Hobj;
        }


        #endregion
    }
}
