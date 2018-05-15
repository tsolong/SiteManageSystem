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
    public partial class NewsAdd : SMS.Web.UI.SysUserPage
    {
        public IList<CategoryInfo> NewsCategoryList = new SMS.BLL.News.Category().GetList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (NewsCategoryList.Count == 0)
                ShowWindow(1, "系统提示", "请先添加新闻分类", "categoryadd.aspx", false);

            if (Tools.GetQueryString("action").ToLower() == "add")
            {
                string Category = Tools.GetForm("Category");
                string Title = Tools.GetForm("Title");
                string Content = HttpUtility.HtmlDecode(Request.Form["MyEditor"]);
                if (!Tools.IsPositiveInt(Category))
                {
                    ShowWindow(1, "系统提示", "请选择新闻所属类别", null, true);
                }
                else if (Title == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写新闻标题", null, true);
                }
                else if (Content == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写新闻内容", null, true);
                }
                else
                {
                    NewsInfo NInfo = new NewsInfo();
                    NInfo.CategoryID = Convert.ToInt32(Category);
                    NInfo.Title = Title;
                    NInfo.Content = Content;
                    if (new BLL.News.News().Add(NInfo) != 0)
                        ShowWindow(3, "系统提示", "新闻添加成功,点击 \\\"确定\\\" 按钮返回新闻列表页面", "news.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "新闻添加失败", null, true);
                }
            }
        }
    }
}
