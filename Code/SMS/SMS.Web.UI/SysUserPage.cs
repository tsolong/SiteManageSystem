using System;
using System.Collections.Generic;
using System.Text;

using SMS.Config;
using SMS.Model.Sys;

namespace SMS.Web.UI
{
    /// <summary>
    /// Web��->ϵͳ�û�������
    /// </summary>
    public class SysUserPage : BasePage
    {
        //��ǰ�ѵ�¼����ϵͳ���û�
        protected UserInfo Sys_User;

        public SysUserPage()
        {
            this.Load += new EventHandler(SysUserPage_Load);
        }

        /// <summary>
        /// ��֤�Ƿ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SysUserPage_Load(object sender, EventArgs e)
        {
            if (Session["SysUser"] != null)
                Sys_User = (UserInfo)(Session["SysUser"]);
            else
                Response.Redirect(SystemManageDir.ToLower() + "overtime.aspx");
        }
    }
}
