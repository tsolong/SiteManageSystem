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

        //目录
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
                ShowWindow(4, "系统提示", "参数类型不正确", null, true);
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
                    ShowWindow(1, "系统提示", "请选择产品所属类别", null, true);
                }
                else if (Name == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写产品名称", null, true);
                }
                else if (Picture == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请上传产品图片", null, true);
                }
                else if (Content == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写产品介绍", null, true);
                }
                else
                {
                    if (PInfo.Picture != Picture)
                    {
                        //移动产品图片
                        BPP.MovePicture(UploadDir, TempUploadFolder, ProductsPictureFolder, Picture.Substring(0, Picture.IndexOf('/')));
                        //删除原来产品图片
                        BPP.DelPicture(UploadDir, ProductsPictureFolder, PInfo.Picture.Substring(0, PInfo.Picture.IndexOf('/')));
                    }

                    PInfo.CategoryID = Convert.ToInt32(Category);
                    PInfo.Name = Name;
                    PInfo.Picture = Picture;
                    PInfo.Content = Content;
                    if (new BLL.Products.Products().Save(PInfo) != 0)
                        ShowWindow(3, "系统提示", "产品保存成功,点击 \\\"确定\\\" 按钮返回", "products.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "产品保存失败", null, true);
                }
            }

            BPC.GetList(ProductsCategoryList);
        }

        /// <summary>
        /// 上传产品图片
        /// </summary>
        private void UploadProductsPicture()
        {
            string Msg = "";

            if (Request.Files.Count <= 0)
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"没有可上传的文件\\\"}";
            }
            else
            {
                HttpPostedFile CurrentFile = Request.Files[0];

                string[] PictureExt = SMS.Config.SysConfig.GetConfigValue("PictureExt").Split(',');
                string[] ProductsPictureSize = SMS.Config.SysConfig.GetConfigValue("ProductsPictureSize").Split('-');

                if (CurrentFile.ContentLength == 0)
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"您还未选择文件\\\"}";
                else if (!Upload.CheckExt(System.IO.Path.GetExtension(CurrentFile.FileName).ToLower(), PictureExt))
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"文件格式不正确\\\"}";
                else if (CurrentFile.ContentLength / 1024 < Convert.ToInt32(ProductsPictureSize[0]) || CurrentFile.ContentLength / 1024 > Convert.ToInt32(ProductsPictureSize[1]))
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"文件大小不正确，文件容量只能在  " + ProductsPictureSize[0] + " - " + ProductsPictureSize[1] + "kb 之间\\\"}";
                else
                {
                    try
                    {
                        //图片目录
                        string PictureFolder = System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(99, 1000);
                        string FullDir = Server.MapPath(UploadDir + TempUploadFolder + "/" + PictureFolder) + "/";
                        Upload.CreateDirectory(FullDir);

                        //文件名
                        string FileExt = System.IO.Path.GetExtension(CurrentFile.FileName).ToLower();
                        string FileName = "o" + FileExt;

                        //完整文件路径
                        string Path = FullDir + FileName;

                        //保存文件
                        CurrentFile.SaveAs(Path);

                        //生成缩略图
                        string[] ProductsPictureThumbnail = SMS.Config.SysConfig.GetConfigValue("ProductsPictureThumbnail").Split(',');
                        for (int j = 0; j < ProductsPictureThumbnail.Length; j++)
                        {
                            //缩略图的宽和高
                            string[] CurrentThumbnail = ProductsPictureThumbnail[j].Replace("[", "").Replace("]", "").Split('|');
                            int Width = Convert.ToInt32(CurrentThumbnail[0]);
                            int Height = Convert.ToInt32(CurrentThumbnail[1]);

                            //完整缩略图文件路径
                            string ThumbnailFileName = Width + "x" + Height + FileExt;
                            string ThumbnailFilePath = FullDir + ThumbnailFileName;

                            //生成缩略图 并且添加水印
                            if (CurrentThumbnail[2] == "True")
                            {
                                string[] ProductsPictureWaterImageOffset = SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageOffset").Split(',');
                                ImageTools.MakeThumbnailImageAndWaterImage(Path, ThumbnailFilePath, Width, Height, Server.MapPath(SMS.Config.SysConfig.GetConfigValue("WaterImagePath")), ImageTools.ConvertToWaterImageDirection(SMS.Config.SysConfig.GetConfigValue("ProductsPictureWaterImageDiretion")), Convert.ToInt32(ProductsPictureWaterImageOffset[0]), Convert.ToInt32(ProductsPictureWaterImageOffset[1]));
                            }
                            //只生成缩略图 不添加水印
                            else
                            {
                                ImageTools.MakeThumbnailImage(Path, ThumbnailFilePath, Width, Height);
                            }
                        }

                        Msg = "{\\\"type\\\":true,\\\"msg\\\":\\\"上传成功\\\",\\\"picture\\\":\\\"" + PictureFolder + "/" + FileName + "\\\"}";
                    }
                    catch
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"上传过程中出错\\\"}";
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
