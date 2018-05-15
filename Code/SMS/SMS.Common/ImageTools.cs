using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SMS.Common
{
    public class ImageTools
    {
        /// <summary>
        /// 生成缩略图片
        /// </summary>
        /// <param name="OriginallyImagePath">原始图片路径</param>
        /// <param name="NewImagePath">缩略图片保存路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        public static void MakeThumbnailImage(string OriginallyImagePath, string NewImagePath, int ThumbnailWidth, int ThumbnailHeight)
        {
            Bitmap OriginallyImage = new Bitmap(OriginallyImagePath);
            Image ThumbnailImage = MakeThumbnailImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight);
            ThumbnailImage.Save(NewImagePath);
            ThumbnailImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static void MakeThumbnailImage(Bitmap OriginallyImage, string NewImagePath, int ThumbnailWidth, int ThumbnailHeight)
        {
            Image ThumbnailImage = MakeThumbnailImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight);
            ThumbnailImage.Save(NewImagePath);
            ThumbnailImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static Image MakeThumbnailImage(string OriginallyImagePath, int ThumbnailWidth, int ThumbnailHeight)
        {
            return MakeThumbnailImageMethod(new Bitmap(OriginallyImagePath), ThumbnailWidth, ThumbnailHeight);
        }

        public static Image MakeThumbnailImage(Bitmap OriginallyImage, int ThumbnailWidth, int ThumbnailHeight)
        {
            return MakeThumbnailImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight);
        }

        private static Image MakeThumbnailImageMethod(Bitmap OriginallyImage, int ThumbnailWidth, int ThumbnailHeight)
        {
            double NewWidth, NewHeight;
            if (OriginallyImage.Width < ThumbnailWidth && OriginallyImage.Height < ThumbnailHeight)
            {
                NewWidth = OriginallyImage.Width;
                NewHeight = OriginallyImage.Height;
            }
            else
            {
                if (OriginallyImage.Width > OriginallyImage.Height)
                {
                    NewWidth = ThumbnailWidth;
                    NewHeight = OriginallyImage.Height * (NewWidth / OriginallyImage.Width);
                }
                else
                {
                    NewHeight = ThumbnailHeight;
                    NewWidth = (NewHeight / OriginallyImage.Height) * OriginallyImage.Width;
                }
                if (NewWidth > ThumbnailWidth)
                {
                    NewWidth = ThumbnailWidth;
                }
                if (NewHeight > ThumbnailHeight)
                {
                    NewHeight = ThumbnailHeight;
                }
            }

            //生成高质量缩略图
            Size size = new Size((int)NewWidth, (int)NewHeight);
            Image ThumbnailImage = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(ThumbnailImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White);
            g.DrawImage(OriginallyImage, new System.Drawing.Rectangle(0, 0, ThumbnailImage.Width, ThumbnailImage.Height), new System.Drawing.Rectangle(0, 0, OriginallyImage.Width, OriginallyImage.Height), System.Drawing.GraphicsUnit.Pixel);
            return ThumbnailImage;

            //生成低质量缩略图
            /*Image.GetThumbnailImageAbort MyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallBack);
            return OriginallyImage.GetThumbnailImage((int)NewWidth, (int)NewHeight, MyCallBack, IntPtr.Zero);*/
        }

        private static bool ThumbnailCallBack()
        {
            return false;
        }


        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <param name="OriginallyImagePath">原始图片路径</param>
        /// <param name="NewImagePath">加好水印图片保存路径</param>
        /// <param name="WaterImagePath">水印图片路径</param>
        /// <param name="Direction">水印图片在原始图片上位置</param>
        /// <param name="OffsetX">水印图片在指定的位置水平偏移量</param>
        /// <param name="OffsetY">水印图片在指定的位置垂直偏移量</param>
        public static void MakeWaterImage(string OriginallyImagePath, string NewImagePath, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            Bitmap OriginallyImage = new Bitmap(OriginallyImagePath);
            Bitmap WaterImage = MakeWaterImageMethod(OriginallyImage, WaterImagePath, Direction, OffsetX, OffsetY);
            WaterImage.Save(NewImagePath);
            WaterImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static void MakeWaterImage(Bitmap OriginallyImage, string NewImagePath, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            Bitmap WaterImage = MakeWaterImageMethod(OriginallyImage, WaterImagePath, Direction, OffsetX, OffsetY);
            WaterImage.Save(NewImagePath);
            WaterImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static Bitmap MakeWaterImage(string OriginallyImagePath, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            return MakeWaterImageMethod(new Bitmap(OriginallyImagePath), WaterImagePath, Direction, OffsetX, OffsetY);
        }

        public static Bitmap MakeWaterImage(Bitmap OriginallyImage, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            return MakeWaterImageMethod(OriginallyImage, WaterImagePath, Direction, OffsetX, OffsetY);
        }

        /// <summary>
        /// 水印图片方向
        /// </summary>
        public enum WaterImageDirection
        {
            /// <summary>
            /// 中间
            /// </summary>
            Center,
            /// <summary>
            /// 上面中间
            /// </summary>
            TopCenter,
            /// <summary>
            /// 右边中间
            /// </summary>
            RightCenter,
            /// <summary>
            /// 下面中间
            /// </summary>
            BottomCenter,
            /// <summary>
            /// 左边中间
            /// </summary>
            LeftCenter,
            /// <summary>
            /// 左上角
            /// </summary>
            LeftTop,
            /// <summary>
            /// 右上角
            /// </summary>
            RightTop,
            /// <summary>
            /// 右下角
            /// </summary>
            RightBottm,
            /// <summary>
            /// 左下角
            /// </summary>
            LeftBottom
        }

        /// <summary>
        /// 将字符串转换为WaterImageDirection类型,默为WaterImageDirection.RightBottom
        /// </summary>
        /// <param name="Str">是转换的字符串，必须是(Center,TopCenter,RightCenter,BottomCenter,LeftCenter,LeftTop,RightTop,RightBottm,LeftBottom)其中的一个</param>
        /// <returns>WaterImageDirection类型的值</returns>
        public static WaterImageDirection ConvertToWaterImageDirection(string Str)
        {
            WaterImageDirection Direction;
            switch (Str)
            {
                case "Center":
                    Direction = ImageTools.WaterImageDirection.Center;
                    break;
                case "TopCenter":
                    Direction = ImageTools.WaterImageDirection.TopCenter;
                    break;
                case "RightCenter":
                    Direction = ImageTools.WaterImageDirection.RightCenter;
                    break;
                case "BottomCenter":
                    Direction = ImageTools.WaterImageDirection.BottomCenter;
                    break;
                case "LeftCenter":
                    Direction = ImageTools.WaterImageDirection.LeftCenter;
                    break;
                case "LeftTop":
                    Direction = ImageTools.WaterImageDirection.LeftTop;
                    break;
                case "RightTop":
                    Direction = ImageTools.WaterImageDirection.RightTop;
                    break;
                case "RightBottm":
                    Direction = ImageTools.WaterImageDirection.RightBottm;
                    break;
                case "LeftBottom":
                    Direction = ImageTools.WaterImageDirection.LeftBottom;
                    break;
                default:
                    Direction = ImageTools.WaterImageDirection.RightBottm;
                    break;
            }
            return Direction;
        }

        private static Bitmap MakeWaterImageMethod(Bitmap OriginallyImage, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            Image WaterImage = Image.FromFile(WaterImagePath);
            Graphics g = Graphics.FromImage(OriginallyImage);

            int x = 0;
            int y = 0;
            switch (Direction.ToString())
            {
                case "Center":
                    x = (OriginallyImage.Width - WaterImage.Width) / 2;
                    y = (OriginallyImage.Height - WaterImage.Height) / 2;
                    break;
                case "TopCenter":
                    x = (OriginallyImage.Width - WaterImage.Width) / 2;
                    break;
                case "RightCenter":
                    x = OriginallyImage.Width - WaterImage.Width;
                    y = (OriginallyImage.Height - WaterImage.Height) / 2;
                    break;
                case "BottomCenter":
                    x = (OriginallyImage.Width - WaterImage.Width) / 2;
                    y = OriginallyImage.Height - WaterImage.Height;
                    break;
                case "LeftCenter":
                    y = (OriginallyImage.Height - WaterImage.Height) / 2;
                    break;
                case "LeftTop":
                    break;
                case "RightTop":
                    x = OriginallyImage.Width - WaterImage.Width;
                    break;
                case "RightBottm":
                    x = OriginallyImage.Width - WaterImage.Width;
                    y = OriginallyImage.Height - WaterImage.Height;
                    break;
                case "LeftBottom":
                    y = OriginallyImage.Height - WaterImage.Height;
                    break;
                default:
                    x = OriginallyImage.Width - WaterImage.Width;
                    y = OriginallyImage.Height - WaterImage.Height;
                    break;
            }

            x += OffsetX;
            y += OffsetY;

            g.DrawImage(WaterImage, new Rectangle(x, y, WaterImage.Width, WaterImage.Height), 0, 0, WaterImage.Width, WaterImage.Height, GraphicsUnit.Pixel);
            WaterImage.Dispose();
            g.Dispose();
            return OriginallyImage;
        }


        /// <summary>
        /// 生成缩略图与水印
        /// </summary>
        /// <param name="OriginallyImagePath">原始图片路径</param>
        /// <param name="NewImagePath">生成好缩略图与水印的图片保存路径</param>
        /// <param name="ThumbnailWidth">缩略图的宽</param>
        /// <param name="ThumbnailHeight">缩略图的高</param>
        /// <param name="WaterImagePath">水印图片路径</param>
        /// <param name="Direction">水印图片在原始图片上位置</param>
        /// <param name="OffsetX">水印图片在指定的位置水平偏移量</param>
        /// <param name="OffsetY">水印图片在指定的位置垂直偏移量</param>
        public static void MakeThumbnailImageAndWaterImage(string OriginallyImagePath, string NewImagePath, int ThumbnailWidth, int ThumbnailHeight, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            Bitmap OriginallyImage = new Bitmap(OriginallyImagePath);
            Bitmap WaterImage = MakeThumbnailImageAndWaterImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight, WaterImagePath, Direction, OffsetX, OffsetY);
            WaterImage.Save(NewImagePath);
            WaterImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static void MakeThumbnailImageAndWaterImage(Bitmap OriginallyImage, string NewImagePath, int ThumbnailWidth, int ThumbnailHeight, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            Bitmap WaterImage = MakeThumbnailImageAndWaterImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight, WaterImagePath, Direction, OffsetX, OffsetY);
            WaterImage.Save(NewImagePath);
            WaterImage.Dispose();
            OriginallyImage.Dispose();
        }

        public static Bitmap MakeThumbnailImageAndWaterImage(string OriginallyImagePath, int ThumbnailWidth, int ThumbnailHeight, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            return MakeThumbnailImageAndWaterImageMethod(new Bitmap(OriginallyImagePath), ThumbnailWidth, ThumbnailHeight, WaterImagePath, Direction, OffsetX, OffsetY);
        }

        public static Bitmap MakeThumbnailImageAndWaterImage(Bitmap OriginallyImage, int ThumbnailWidth, int ThumbnailHeight, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            return MakeThumbnailImageAndWaterImageMethod(OriginallyImage, ThumbnailWidth, ThumbnailHeight, WaterImagePath, Direction, OffsetX, OffsetY);
        }

        private static Bitmap MakeThumbnailImageAndWaterImageMethod(Bitmap OriginallyImage, int ThumbnailWidth, int ThumbnailHeight, string WaterImagePath, WaterImageDirection Direction, int OffsetX, int OffsetY)
        {
            return MakeWaterImage(new Bitmap(MakeThumbnailImage(OriginallyImage, ThumbnailWidth, ThumbnailHeight)), WaterImagePath, Direction, OffsetX, OffsetY);
        }
    }
}
