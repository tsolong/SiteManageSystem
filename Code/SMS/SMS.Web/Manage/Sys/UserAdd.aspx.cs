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
using SMS.Model.Sys;

namespace SMS.Web.Manage.Sys
{
    public partial class UserAdd : SMS.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "add")
            {
                string UserName = Tools.GetForm("UserName").ToLower();
                string Password = Tools.GetForm("Password").ToLower();
                if (UserName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写用户名", null, true);
                }
                else if (!Regex.IsMatch(UserName, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "系统提示", "用户名格式错误", null, true);
                }
                else if (Password == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写密码", null, true);
                }
                else if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "系统提示", "密码格式错误", null, true);
                }
                else if (Password != Tools.GetForm("ConfirmPassword").ToLower())
                {
                    ShowWindow(4, "系统提示", "两次密码填写不一致", null, true);
                }
                else
                {
                    if (new BLL.Sys.User().CheckUserNameIsExist(UserName))
                    {
                        ShowWindow(4, "系统提示", "此用户名已被使用,请换用别的用户名", null, true);
                    }
                    else
                    {
                        UserInfo SysUser = new UserInfo();
                        SysUser.UserName = UserName;
                        SysUser.Password = Tools.MD5(Password);
                        if (new BLL.Sys.User().Add(SysUser) != 0)
                            ShowWindow(3, "系统提示", "管理员添加成功,点击 \\\"确定\\\" 按钮返回管理员列表页面", "user.aspx", false);
                        else
                            ShowWindow(4, "系统提示", "管理员添加失败", null, true);
                    }
                }
            }
        }
    }
}
