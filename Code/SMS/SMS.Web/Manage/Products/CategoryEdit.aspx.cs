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
    public partial class CategoryEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public CategoryInfo ProductsCategory;
        public BLL.Products.Category BPC = new SMS.BLL.Products.Category();
        public IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(Tools.GetQueryString("id"));
            }
            catch
            {
                ShowWindow(4, "系统提示", "参数类型不正确", null, true);
            }

            ProductsCategory = BPC.GetByID(ID);

            if (Tools.GetQueryString("action").ToLower() == "save" && ProductsCategory != null)
            {
                string Category = Tools.GetForm("Category");
                string CategoryName = Tools.GetForm("CategoryName");
                string OrderID = Tools.GetForm("OrderID");
                if (!Tools.IsPositiveInt(Category, true))
                {
                    ShowWindow(1, "系统提示", "请选择所属类别", null, true);
                }

                bool flag = false;
                string[] CategoryIDList = BPC.GetChildCategoryIDListByID(ProductsCategory.ID).Split(',');
                for (int i = 0; i < CategoryIDList.Length; i++)
                {
                    if (CategoryIDList[i] == Category)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    ShowWindow(4, "系统提示", "所属类别不能选择自己或自己的子类", null, true);
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
                    ProductsCategory.ParentID = Convert.ToInt32(Category);
                    ProductsCategory.CategoryName = CategoryName;
                    ProductsCategory.OrderID = Convert.ToInt32(OrderID);
                    if (BPC.Save(ProductsCategory) != 0)
                        ShowWindow(3, "系统提示", "产品分类保存成功,点击 \\\"确定\\\" 按钮返回", "category.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "产品分类保存失败", null, true);
                }
            }

            BPC.GetList(ProductsCategoryList);
        }
    }
}
