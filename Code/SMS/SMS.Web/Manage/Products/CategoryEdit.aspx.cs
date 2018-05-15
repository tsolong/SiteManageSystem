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
                ShowWindow(4, "ϵͳ��ʾ", "�������Ͳ���ȷ", null, true);
            }

            ProductsCategory = BPC.GetByID(ID);

            if (Tools.GetQueryString("action").ToLower() == "save" && ProductsCategory != null)
            {
                string Category = Tools.GetForm("Category");
                string CategoryName = Tools.GetForm("CategoryName");
                string OrderID = Tools.GetForm("OrderID");
                if (!Tools.IsPositiveInt(Category, true))
                {
                    ShowWindow(1, "ϵͳ��ʾ", "��ѡ���������", null, true);
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
                    ShowWindow(4, "ϵͳ��ʾ", "���������ѡ���Լ����Լ�������", null, true);
                }
                else if (CategoryName == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д��������", null, true);
                }
                else if (CategoryName.Length < 1 || CategoryName.Length > 10)
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�������Ƴ��ȴ���", null, true);
                }
                else if (OrderID == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д��������", null, true);
                }
                else if (!Tools.IsPositiveInt(OrderID))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��������ֻ����������", null, true);
                }
                else
                {
                    ProductsCategory.ParentID = Convert.ToInt32(Category);
                    ProductsCategory.CategoryName = CategoryName;
                    ProductsCategory.OrderID = Convert.ToInt32(OrderID);
                    if (BPC.Save(ProductsCategory) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "��Ʒ���ౣ��ɹ�,��� \\\"ȷ��\\\" ��ť����", "category.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "��Ʒ���ౣ��ʧ��", null, true);
                }
            }

            BPC.GetList(ProductsCategoryList);
        }
    }
}
