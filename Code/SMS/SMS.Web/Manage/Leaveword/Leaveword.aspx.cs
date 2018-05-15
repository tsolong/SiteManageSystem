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
using SMS.Model.Leaveword;

namespace SMS.Web.Manage.Leaveword
{
    public partial class Leaveword : SMS.Web.UI.SysUserPage
    {
        public int PageIndex;
        private int PageSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageSize"));
        private int PageNumTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageNumTotal"));
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";
        public IList<LeavewordInfo> LInfoList;

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
        }

        /// <summary>
        /// 删除留言
        /// </summary>
        private void Del()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                if (new BLL.Leaveword.Leaveword().Del(ID) != 0)
                    ShowWindow(3, "系统提示", "删除留言成功,点击 \\\"确定\\\" 按钮返回", "leaveword.aspx", false);
                else
                    ShowWindow(4, "系统提示", "删除留言失败", null, true);
            }
            else
            {
                ShowWindow(1, "系统提示", "请选择要删除的留言", null, true);
            }
        }

        /// <summary>
        /// 获取留言对象列表
        /// </summary>
        private void GetList()
        {
            //检查PageIndex参数
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {
                PageIndex = Convert.ToInt32(tempIndex);

                LInfoList = new BLL.Leaveword.Leaveword().GetList(PageIndex, PageSize, out RecordTotal);
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
