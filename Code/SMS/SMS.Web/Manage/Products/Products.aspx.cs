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
    public partial class Products : SMS.Web.UI.SysUserPage
    {
        public int PageIndex;
        private int PageSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageSize"));
        private int PageNumTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageNumTotal"));
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";

        public int CategoryID = 0;
        public IList<ProductsInfo> PInfoList;
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
                    GetList();
                    break;
            }
            BPC.GetList(ProductsCategoryList);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        private void Del()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                ArrayList PictureList = BPP.GetPictureByID(ID);
                if (BPP.Del(ID) != 0)
                {
                    for (int i = 0; i < PictureList.Count; i++)
                    {
                        BPP.DelPicture(SMS.Config.SysConfig.GetConfigValue("UploadDir"), SMS.Config.SysConfig.GetConfigValue("ProductsPictureFolder"), PictureList[i].ToString().Substring(0, PictureList[i].ToString().IndexOf('/')));
                    }
                    ShowWindow(3, "系统提示", "删除产品成功,点击 \\\"确定\\\" 按钮返回", "products.aspx", false);
                }
                else
                {
                    ShowWindow(4, "系统提示", "删除产品失败", null, true);
                }
            }
            else
            {
                ShowWindow(1, "系统提示", "请选择要删除的产品", null, true);
            }
        }

        /// <summary>
        /// 获取产品对象列表
        /// </summary>
        private void GetList()
        {
            //检查PageIndex参数
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {
                PageIndex = Convert.ToInt32(tempIndex);

                try
                {
                    CategoryID = Convert.ToInt32(Tools.GetQueryString("categoryid"));
                    string CategoryIDList = BPC.GetChildCategoryIDListByID(Convert.ToInt32(CategoryID));
                    PInfoList = BPP.GetListByCategory(PageIndex, PageSize, out RecordTotal, CategoryIDList);
                }
                catch
                {
                    PInfoList = BPP.GetList(PageIndex, PageSize, out RecordTotal);
                }

                MyPageBar = new PageBar(PageIndex, PageSize, RecordTotal, PageNumTotal, "p");

                if (RecordTotal > 0)
                {
                    if (PageIndex > MyPageBar.PageTotal)
                        ShowWindow(4, "系统提示", "分页参数错误,点击 \\\"确定\\\" 按钮返回", null, true);
                    else
                        PageBarHtml = MyPageBar.GetHTML();
                }
            }
            else
            {
                ShowWindow(4, "系统提示", "分页参数错误,点击 \\\"确定\\\" 按钮返回", null, true);
            }
        }
    }
}
