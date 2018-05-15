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
                    ShowWindow(1, "ϵͳ��ʾ", "����д�û���", null, true);
                }
                else if (!Regex.IsMatch(UserName, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�û�����ʽ����", null, true);
                }
                else if (Password == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д����", null, true);
                }
                else if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�����ʽ����", null, true);
                }
                else if (Password != Tools.GetForm("ConfirmPassword").ToLower())
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����������д��һ��", null, true);
                }
                else
                {
                    if (new BLL.Sys.User().CheckUserNameIsExist(UserName))
                    {
                        ShowWindow(4, "ϵͳ��ʾ", "���û����ѱ�ʹ��,�뻻�ñ���û���", null, true);
                    }
                    else
                    {
                        UserInfo SysUser = new UserInfo();
                        SysUser.UserName = UserName;
                        SysUser.Password = Tools.MD5(Password);
                        if (new BLL.Sys.User().Add(SysUser) != 0)
                            ShowWindow(3, "ϵͳ��ʾ", "����Ա��ӳɹ�,��� \\\"ȷ��\\\" ��ť���ع���Ա�б�ҳ��", "user.aspx", false);
                        else
                            ShowWindow(4, "ϵͳ��ʾ", "����Ա���ʧ��", null, true);
                    }
                }
            }
        }
    }
}
