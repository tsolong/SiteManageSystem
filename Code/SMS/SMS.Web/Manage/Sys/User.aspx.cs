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
        /// UserID���Ƿ������ǰ��¼�Ĺ���Ա
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
        /// ɾ������Ա
        /// </summary>
        private void Del()
        {
            string UserID = Tools.GetQueryString("userid");
            if (UserID != string.Empty)
            {
                if (IsCurrentUser(UserID))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "����ɾ���Լ�", null, true);
                }
                else
                {
                    if (new BLL.Sys.User().Del(UserID) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "ɾ������Ա�ɹ�,��� \\\"ȷ��\\\" ��ť����", "user.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "ɾ������Աʧ��", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫɾ���Ĺ���Ա", null, true);
            }
        }

        /// <summary>
        /// ��ȡ����Ա�û������б�
        /// </summary>
        private void GetList()
        {
            //���PageIndex����
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
                        ShowWindow(4, "ϵͳ��ʾ", "��ҳ��������,��� \\\"ȷ��\\\" ��ť����", null, true);
                    else
                        PageBarHtml = MyPageBar.GetHTML();
                }
            }
            else
            {
                ShowWindow(4, "ϵͳ��ʾ", "��ҳ��������,��� \\\"ȷ��\\\" ��ť����", null, true);
            }
        }
    }
}
