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
using SMS.Model.Sys;

namespace SMS.Web.Manage.Sys
{
    public partial class User : SMS.Web.UI.SysUserPage
    {
        public int PageIndex;
        private int PageSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageSize"));
        private int PageNumTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageNumTotal"));
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";
        public IList<UserInfo> SysUserList;

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
        /// UserID中是否包含当前登录的管理员
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private bool IsCurrentUser(string UserID)
        {
            bool flag = false;
            string[] UserIdArr = UserID.Split(',');
            for (int i = 0; i < UserIdArr.Length; i++)
            {
                if (UserIdArr[i] == Sys_User.UserID.ToString())
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        private void Del()
        {
            string UserID = Tools.GetQueryString("userid");
            if (UserID != string.Empty)
            {
                if (IsCurrentUser(UserID))
                {
                    ShowWindow(4, "系统提示", "不能删除自己", null, true);
                }
                else
                {
                    if (new BLL.Sys.User().Del(UserID) != 0)
                        ShowWindow(3, "系统提示", "删除管理员成功,点击 \\\"确定\\\" 按钮返回", "user.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "删除管理员失败", null, true);
                }
            }
            else
            {
                ShowWindow(1, "系统提示", "请选择要删除的管理员", null, true);
            }
        }

        /// <summary>
        /// 获取管理员用户对象列表
        /// </summary>
        private void GetList()
        {
            //检查PageIndex参数
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {
                PageIndex = Convert.ToInt32(tempIndex);

                SysUserList = new BLL.Sys.User().GetList(PageIndex, PageSize, out RecordTotal);
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
