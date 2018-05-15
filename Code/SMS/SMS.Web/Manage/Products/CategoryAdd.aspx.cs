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
    public partial class CategoryAdd : SMS.Web.UI.SysUserPage
    {
        public int CategoryID;
        public BLL.Products.Category BPC = new SMS.BLL.Products.Category();
        public IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "add")
            {
                string Category = Tools.GetForm("Category");
                string CategoryName = Tools.GetForm("CategoryName");
                string OrderID = Tools.GetForm("OrderID");
                if (!Tools.IsPositiveInt(Category, true))
                {
                    ShowWindow(1, "系统提示", "请选择所属类别", null, true);
                }
                else if (CategoryName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写分类名称", null, true);
                }
                else if (CategoryName.Length < 1 || CategoryName.Length > 10)
                {
                    ShowWindow(4, "系统提示", "分类名称长度错误", null, true);
                }
                else if (OrderID == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写分类排序", null, true);
                }
                else if (!Tools.IsPositiveInt(OrderID))
                {
                    ShowWindow(4, "系统提示", "分类排序只能是正整数", null, true);
                }
                else
                {
                    CategoryInfo ProductsCategory = new CategoryInfo();
                    ProductsCategory.ParentID = Convert.ToInt32(Category);
                    ProductsCategory.CategoryName = CategoryName;
                    ProductsCategory.OrderID = Convert.ToInt32(OrderID);
                    if (BPC.Add(ProductsCategory) != 0)
                        ShowWindow(3, "系统提示", "产品分类添加成功,点击 \\\"确定\\\" 按钮返回产品分类列表页面", "category.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "产品分类添加失败", null, true);
                }
            }

            BPC.GetList(ProductsCategoryList);

            try
            {
                CategoryID = Convert.ToInt32(Tools.GetQueryString("categoryid"));
            }
            catch
            {
                CategoryID = 0;
            }
        }
    }
}
