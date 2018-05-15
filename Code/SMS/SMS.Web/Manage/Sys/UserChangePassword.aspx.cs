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

namespace SMS.Web.Manage.Sys
{
    public partial class UserChangePassword : SMS.Web.UI.SysUserPage
    {
        public int UserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserID = Convert.ToInt32(Tools.GetQueryString("userid"));
            }
            catch
            {
                ShowWindow(4, "ϵͳ��ʾ", "�������Ͳ���ȷ", null, true);
            }

            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string Password = Tools.GetForm("Password").ToLower();
                if (Password == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д������", null, true);
                }
                else if (!Regex.IsMatch(Password, @"^[a-zA-Z0-9_]{4,16}$"))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "�������ʽ����", null, true);
                }
                else if (Password != Tools.GetForm("ConfirmPassword").ToLower())
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����������д��һ��", null, true);
                }
                else
                {
                    if (new BLL.Sys.User().ChangePassword(UserID, Tools.MD5(Password)) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "�����뱣��ɹ�,��� \\\"ȷ��\\\" ��ť����", "user.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "�����뱣��ʧ��", null, true);
                }
            }
        }
    }
}
