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

using SMS.Common;
using SMS.Model.Products;

namespace SMS.Web.Manage.Products
{
    public partial class Category : SMS.Web.UI.SysUserPage
    {
        public BLL.Products.Category BPC = new SMS.BLL.Products.Category();
        public BLL.Products.Products BPP = new SMS.BLL.Products.Products();
        public IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Tools.GetQueryString("action").ToLower())
            {
                case "del":
                    Del();
                    break;
                default:
                    BPC.GetList(ProductsCategoryList);
                    break;
            }
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        private void Del()
        {
            int ID;
            try
            {
                ID = Convert.ToInt32(Tools.GetQueryString("id"));
            }
            catch
            {
                ID = 0;
                ShowWindow(1, "系统提示", "请选择要删除的产品分类", null, true);
            }

            string CategoryIDList = BPC.GetChildCategoryIDListByID(ID);
            if (BPC.Del(CategoryIDList) != 0)
            {
                ArrayList PictureList = BPP.GetPictureByCategory(CategoryIDList);
                BPP.DelByCategory(CategoryIDList);
                for (int i = 0; i < PictureList.Count; i++)
                {
                    BPP.DelPicture(SMS.Config.SysConfig.GetConfigValue("UploadDir"), SMS.Config.SysConfig.GetConfigValue("ProductsPictureFolder"), PictureList[i].ToString().Substring(0, PictureList[i].ToString().IndexOf('/')));
                }
                ShowWindow(3, "系统提示", "删除产品分类成功,点击 \\\"确定\\\" 按钮返回", "category.aspx", false);
            }
            else
            {
                ShowWindow(4, "系统提示", "删除产品分类失败", null, true);
            }
        }
    }
}
