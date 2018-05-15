using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
    public partial class CategoryEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public CategoryInfo NewsCategory;

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

            NewsCategory = new BLL.News.Category().GetByID(ID);

            if (Tools.GetQueryString("action").ToLower() == "save" && NewsCategory != null)
            {
                string CategoryName = Tools.GetForm("CategoryName");
                string OrderID = Tools.GetForm("OrderID");
                if (CategoryName == string.Empty)
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
                    NewsCategory.CategoryName = CategoryName;
                    NewsCategory.OrderID = Convert.ToInt32(OrderID);
                    if (new BLL.News.Category().Save(NewsCategory) != 0)
                        ShowWindow(3, "系统提示", "新闻分类保存成功,点击 \\\"确定\\\" 按钮返回", "category.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "新闻分类保存失败", null, true);
                }
            }
        }
    }
}
