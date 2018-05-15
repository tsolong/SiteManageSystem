using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;

namespace SMS.Common
{
    /// <summary>
    /// ��֤��
    /// </summary>
    public class ValidateImage
    {
        private string Code;
        private int CodeType;
        private int CodeLength;
        private int Width;
        private int Height;

        private Random Rand = new Random();

        public ValidateImage(int CodeLength, int Width, int Height)
        {
            init(1, CodeLength, Width, Height);
        }

        public ValidateImage(int CodeType, int CodeLength, int Width, int Height)
        {
            init(CodeType, CodeLength, Width, Height);
        }

        public void init(int CodeType, int CodeLength, int Width, int Height)
        {
            this.CodeType = CodeType;
            this.CodeLength = CodeLength;
            this.Width = Width;
            this.Height = Height;

            GetCode();
            GraphicsImage();
        }

        public static bool CheckCode(string Code)
        {
            return System.Web.HttpContext.Current.Session["ValidateCode"] != null && System.Web.HttpContext.Current.Session["ValidateCode"].ToString().Equals(Code);
        }

        public static void ClearCode()
        {
            System.Web.HttpContext.Current.Session["ValidateCode"] = null;
        }

        private void GetCode()
        {
            string Code = "";
            if (this.CodeType < 4)
            {
                string str = "";

                if (this.CodeType == 1)
                    str = "0123456789";
                else if (this.CodeType == 2)
                    str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                else if (this.CodeType == 3)
                    str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                else
                    str = "0123456789";

                for (int i = 0; i < this.CodeLength; i++)
                {
                    Code += str[Rand.Next(0, str.Length)];
                }
            }
            else
            {
                for (int i = 0; i < this.CodeLength; i++)
                {
                    byte[] bytes = new byte[2];
                    //��һ���ֽ�ֵ��0xb0, 0xf7֮��
                    bytes[0] = (byte)Rand.Next(0xb0, 0xf8);
                    //�ڶ����ֽ�ֵ��0xa1, 0xfe֮��
                    bytes[1] = (byte)Rand.Next(0xa1, 0xff);

                    //���ݺ��ֱ�����ֽ������������ĺ���
                    Code += Encoding.GetEncoding("gb2312").GetString(bytes);

                }
            }
            this.Code = Code;
            System.Web.HttpContext.Current.Session["ValidateCode"] = this.Code;
        }

        private void GraphicsImage()
        {
            //Bitmap Image = new System.Drawing.Bitmap((int)Math.Ceiling((Code.Length * 22.5)), 50);
            Bitmap Image = new System.Drawing.Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(Image);  //��������

            //�������������
            Random random = new Random();

            //���ͼƬ����ɫ
            g.Clear(Color.White);

            //��ͼƬ�ı���������
            for (int i = 0; i < 2; i++)
            {
                int x1 = random.Next(Image.Width);
                int x2 = random.Next(Image.Width);
                int y1 = random.Next(Image.Height);
                int y2 = random.Next(Image.Height);

                g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
            }

            Font font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            /*System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush
                (new Rectangle(0, 0, Image.Width, Image.Height), Color.Red, Color.Red, 1.2f, true);*/

            SolidBrush brush = new SolidBrush(Color.Red);

            //System.Drawing.Drawing2D.
            g.DrawString(this.Code, font, brush, 2, 2);

            //��ͼƬ��ǰ��������
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Image.Width);
                int y = random.Next(Image.Height);

                Image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //��ͼƬ�ı߿���
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, Image.Width - 1, Image.Height - 1);
            Image.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            System.Web.HttpContext.Current.Response.ContentType = "image/pjpeg";
        }
    }
}
