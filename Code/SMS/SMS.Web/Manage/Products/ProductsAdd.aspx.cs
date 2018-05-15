using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SMS.Common;
using SMS.Model.Products;

namespace SMS.Web.Manage.Products
{
    public partial class ProductsAdd : SMS.Web.UI.SysUserPage
    {
        public BLL.Products.Category BPC = new SMS.BLL.Products.Category();
        public BLL.Products.Products BPP = new SMS.BLL.Products.Products();
        public IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();

        //Ŀ¼
        public string UploadDir = SMS.Config.SysConfig.GetConfigValue("UploadDir");
        public string TempUploadFolder = SMS.Config.SysConfig.GetConfigValue("TempUploadFolder");
        public string ProductsPictureFolder = SMS.Config.SysConfig.GetConfigValue("ProductsPictureFolder");

        protected void Page_Load(object sender, EventArgs e)
        {
            BPC.GetList(ProductsCategoryList);
            if (ProductsCategoryList.Count == 0)
                ShowWindow(1, "ϵͳ��ʾ", "������Ӳ�Ʒ����", "categoryadd.aspx", false);

            string action = Tools.GetQueryString("action").ToLower();
            switch (action)
            {
                case "add":
                    Add();
                    break;
                case "upload":
                    UploadProductsPicture();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ��Ӳ�Ʒ
        /// </summary>
        private void Add()
        {
            string Category = Tools.GetForm("Category");
            string Name = Tools.GetForm("Name");
            string Picture = Tools.GetForm("Picture");
            string Content = HttpUtility.HtmlDecode(Request.Form["MyEditor"]);
            if (!Tools.IsPositiveInt(Category))
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ���Ʒ�������", null, true);
            }
            else if (Name == string.Empty)
            {
                ShowWindow(1, "ϵͳ��ʾ", "����д��Ʒ����", null, true);
            }
            else if (Picture == string.Empty)
            {
                ShowWindow(1, "ϵͳ��ʾ", "���ϴ���ƷͼƬ", null, true);
            }
            else if (Content == string.Empty)
            {
                ShowWindow(1, "ϵͳ��ʾ", "����д��Ʒ����", null, true);
            }
            else
            {
                //�ƶ���ƷͼƬ
                BPP.MovePicture(UploadDir, TempUploadFolder, ProductsPictureFolder, Picture.Substring(0, Picture.IndexOf('/')));

                ProductsInfo PInfo = new ProductsInfo();
                PInfo.CategoryID = Convert.ToInt32(Category);
                PInfo.Name = Name;
                PInfo.Picture = Picture;
                PInfo.Content = Content;
                if (BPP.Add(PInfo) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "��Ʒ��ӳɹ�,��� \\\"ȷ��\\\" ��ť���ز�Ʒ�б�ҳ��", "products.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "��Ʒ���ʧ��", null, true);
            }
        }


        /// <summary>
        /// �ϴ���ƷͼƬ
        /// </summary>
        private void UploadProductsPicture()
        {
            string Msg = "";

            if (Request.Files.Count <= 0)
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"û�п��ϴ����ļ�\\\"}";
            }
            else
            {
                HttpPostedFile CurrentFile = Request.Files[0];

                string[] PictureExt = SMS.Config.SysConfig.GetConfigValue("PictureExt").Split(',');
                string[] ProductsPictureSize = SMS.Config.SysConfig.GetConfigValue("ProductsPictureSize").Split('-');

                if (CurrentFile.ContentLength == 0)
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"����δѡ���ļ�\\\"}";
                else if (!Upload.CheckExt(System.IO.Path.GetExtension(CurrentFile.FileName).ToLower(), PictureExt))
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ļ���ʽ����ȷ\\\"}";
                else if (CurrentFile.ContentLength / 1024 < Convert.ToInt32(ProductsPictureSize[0]) || CurrentFile.ContentLength / 1024 > Convert.ToInt32(ProductsPictureSize[1]))
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ļ���С����ȷ���ļ�����ֻ����  " + ProductsPictureSize[0] + " - " + ProductsPictureSize[1] + "kb ֮��\\\"}";
                else
                {
                    try
                    {
                        //ͼƬĿ¼
                        string PictureFolder = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(99, 1000);
                        string FullDir = Server.MapPath(UploadDir + TempUploadFolder + "/" + PictureFolder) + "/";
                        Upload.CreateDirectory(FullDir);

                        //�ļ���
                        string FileExt = System.IO.Path.GetExtension(CurrentFile.FileName).ToLower();
                        string FileName = "o" + FileExt;

                        //�����ļ�·��
                        string Path = FullDir + FileName;

                        //�����ļ�
                        CurrentFile.SaveAs(Path);

                        //��������ͼ
                        string[] ProductsPictureThumbnail = SMS.Config.SysConfig.GetConfigValue("ProductsPictureThumbnail").Split(',');
                        for (int j = 0; j < ProductsPictureThumbnail.Length; j++)
                        {
                            //����ͼ�Ŀ�͸�
                            string[] CurrentThumbnail = ProductsPictureThumbnail[j].Replace("[", "").Replace("]", "").Split('|');
                            int Width = Convert.ToInt32(CurrentThumbnail[0]);
                            int Height = Convert.ToInt32(CurrentThumbnail[1]);

                            //��������ͼ�ļ�·��
                            string ThumbnailFileName = Width + "x" + Height + FileExt;
                            string ThumbnailFilePath = FullDir + ThumbnailFileName;

                            //��������ͼ �������ˮӡ
                            if (CurrentThumbnail[2] == "True")
                            {
                                string[] ProductsPictureWaterImageOffset = SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageOffset").Split(',');
                                ImageTools.MakeThumbnailImageAndWaterImage(Path, ThumbnailFilePath, Width, Height, Server.MapPath(SMS.Config.SysConfig.GetConfigValue("WaterImagePath")), ImageTools.ConvertToWaterImageDirection(SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageDiretion")), Convert.ToInt32(ProductsPictureWaterImageOffset[0]), Convert.ToInt32(ProductsPictureWaterImageOffset[1]));
                            }
                            //ֻ��������ͼ �����ˮӡ
                            else
                            {
                                ImageTools.MakeThumbnailImage(Path, ThumbnailFilePath, Width, Height);
                            }
                        }

                        Msg = "{\\\"type\\\":true,\\\"msg\\\":\\\"�ϴ��ɹ�\\\",\\\"picture\\\":\\\"" + PictureFolder + "/" + FileName + "\\\"}";
                    }
                    catch
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ϴ������г���\\\"}";
                    }
                }
            }

            Response.Write("<script type=\"text/javascript\">" +
                "\r\tparent.uploadEnd(\"" + Msg + "\")" +
                "\r</script>");
            Response.End();
        }

        /*/// <summary>
        /// �ϴ���ƷͼƬ
        /// </summary>
        private void UploadProductsPicture()
        {
            string Msg = "";
            int FilesCount = Request.Files.Count;

            int ProductsPictureBatchTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsPictureBatchTotal"));
            string[] ProductsPictureExt = SMS.Config.SysConfig.GetConfigValue("ProductsPictureExt").Split(',');
            int ProductsPictureMinSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsPictureMinSize"));
            int ProductsPictureMaxSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsPictureMaxSize"));


            if (FilesCount <= 0)
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"û�п��ϴ����ļ�\\\"}";
            }
            else if (FilesCount > ProductsPictureBatchTotal)
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"���ֻ��ͬʱ�ϴ� " + ProductsPictureBatchTotal + " ���ļ�\\\"}";
            }
            else
            {
                bool flag = false;
                for (int i = 0; i < FilesCount; i++)
                {
                    HttpPostedFile CurrentFile = Request.Files[i];
                    if (CurrentFile.ContentLength == 0)
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"����δѡ��� " + (i + 1) + " ���ļ�\\\"}";
                        flag = true;
                        break;
                    }
                    if (!Upload.CheckExt(System.IO.Path.GetExtension(CurrentFile.FileName).Substring(1).ToLower(), ProductsPictureExt))
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�� " + (i + 1) + " ���ļ���ʽ����ȷ\\\"}";
                        flag = true;
                        break;
                    }
                    if (CurrentFile.ContentLength < ProductsPictureMinSize || CurrentFile.ContentLength > ProductsPictureMaxSize)
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�� " + (i + 1) + " ���ļ���С����ȷ�������ļ�����ֻ����  " + ProductsPictureMinSize / 1024 + "kb - " + ProductsPictureMaxSize / 1024 + "kb ֮��\\\"}";
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    try
                    {
                        string UploadFilesDir = SMS.Config.SysConfig.GetConfigValue("UploadFilesDir");
                        string ProductsFolder = SMS.Config.SysConfig.GetConfigValue("ProductsFolder");

                        string Name = "";
                        for (int i = 0; i < FilesCount; i++)
                        {
                            string RandDir = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(99, 1000) + "/";
                            string FullDir = Server.MapPath(UploadFilesDir + "/" + ProductsFolder + "/" + RandDir);

                            Upload.CreateDirectory(FullDir);

                            HttpPostedFile CurrentFile = Request.Files[i];

                            string FileExt = System.IO.Path.GetExtension(CurrentFile.FileName).ToLower();
                            string Path = FullDir + "originally" + FileExt;

                            CurrentFile.SaveAs(Path);


                            string ProductsPictureThumbnailSize = SMS.Config.SysConfig.GetConfigValue("ProductsPictureThumbnailSize");

                            if (ProductsPictureThumbnailSize != "")
                            {
                                string[] ThumbnailSize = ProductsPictureThumbnailSize.Split(',');

                                for (int j = 0; j < ThumbnailSize.Length; j++)
                                {
                                    string[] CurrentSize = ThumbnailSize[j].Split('|');
                                    int Width = Convert.ToInt32(CurrentSize[0].Replace("[", ""));
                                    int Height = Convert.ToInt32(CurrentSize[1]);

                                    string ThumbnailFileName = Width + "x" + Height + FileExt;
                                    string ThumbnailFilePath = FullDir + ThumbnailFileName;

                                    if (CurrentSize[2].Replace("]", "") == "True")
                                    {
                                        ImageTools.WaterImageDirection Direction = ImageTools.ConvertToWaterImageDirection(SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageDiretion"));
                                        ImageTools.MakeThumbnailImageAndWaterImage(Path, ThumbnailFilePath, Width, Height, Server.MapPath(SMS.Config.SysConfig.GetConfigValue("WaterImagePath")), Direction, Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageOffsetX")), Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageOffsetY")));
                                    }
                                    else
                                    {
                                        ImageTools.MakeThumbnailImage(Path, ThumbnailFilePath, Width, Height);
                                    }
                                }
                            }
                            if (Name == "")
                                Name = RandDir;
                            else
                                Name += "|" + RandDir;
                        }
                        Msg = "{\\\"type\\\":true,\\\"msg\\\":\\\"�ϴ��ɹ�\\\",\\\"name\\\":\\\"" + Name + "\\\"}";
                    }
                    catch
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ϴ������г���\\\"}";
                    }
                }
            }

            Response.Write("<script type=\"text/javascript\">" +
                "\r\tparent.uploadEnd(\"" + Msg + "\")" +
                "\r</script>");
            Response.End();
        }*/
    }
}
