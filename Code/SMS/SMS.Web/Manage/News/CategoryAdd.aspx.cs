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
using SMS.Model.News;

namespace SMS.Web.Manage.News
{
    public partial class CategoryAdd : SMS.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "add")
            {
                string CategoryName = Tools.GetForm("CategoryName");
                string OrderID = Tools.GetForm("OrderID");
                if (CategoryName == string.Empty)
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
                    CategoryInfo NewsCategory = new CategoryInfo();
                    NewsCategory.CategoryName = CategoryName;
                    NewsCategory.OrderID = Convert.ToInt32(OrderID);
                    if (new BLL.News.Category().Add(NewsCategory) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "���ŷ�����ӳɹ�,��� \\\"ȷ��\\\" ��ť�������ŷ����б�ҳ��", "category.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "���ŷ������ʧ��", null, true);
                }
            }
        }
    }
}
