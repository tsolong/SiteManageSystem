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
    public partial class ProductsEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public ProductsInfo PInfo;
        public BLL.Products.Category BPC = new SMS.BLL.Products.Category();
        public BLL.Products.Products BPP = new SMS.BLL.Products.Products();
        public IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();

        //Ŀ¼
        public string UploadDir = SMS.Config.SysConfig.GetConfigValue("UploadDir");
        public string TempUploadFolder = SMS.Config.SysConfig.GetConfigValue("TempUploadFolder");
        public string ProductsPictureFolder = SMS.Config.SysConfig.GetConfigValue("ProductsPictureFolder");

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Tools.GetQueryString("action").ToLower();
            if (action == "upload")
                UploadProductsPicture();

            try
            {
                ID = Convert.ToInt32(Tools.GetQueryString("id"));
            }
            catch
            {
                ShowWindow(4, "ϵͳ��ʾ", "�������Ͳ���ȷ", null, true);
            }

            PInfo = new BLL.Products.Products().GetByID(ID);
            if (PInfo != null)
                MyEditor.Value = PInfo.Content;

            if (action == "save" && PInfo != null)
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
                    if (PInfo.Picture != Picture)
                    {
                        //�ƶ���ƷͼƬ
                        BPP.MovePicture(UploadDir, TempUploadFolder, ProductsPictureFolder, Picture.Substring(0, Picture.IndexOf('/')));
                        //ɾ��ԭ����ƷͼƬ
                        BPP.DelPicture(UploadDir, ProductsPictureFolder, PInfo.Picture.Substring(0, PInfo.Picture.IndexOf('/')));
                    }

                    PInfo.CategoryID = Convert.ToInt32(Category);
                    PInfo.Name = Name;
                    PInfo.Picture = Picture;
                    PInfo.Content = Content;
                    if (new BLL.Products.Products().Save(PInfo) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "��Ʒ����ɹ�,��� \\\"ȷ��\\\" ��ť����", "products.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "��Ʒ����ʧ��", null, true);
                }
            }

            BPC.GetList(ProductsCategoryList);
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
    }
}
