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
using SMS.Model.Leaveword;

namespace SMS.Web.Manage.Leaveword
{
    public partial class LeavewordEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public LeavewordInfo LInfo;

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

            LInfo = new BLL.Leaveword.Leaveword().GetByID(ID);
            if (LInfo != null && !LInfo.View)
                new BLL.Leaveword.Leaveword().UpdateView(LInfo.ID);

            if (Tools.GetQueryString("action").ToLower() == "save" && LInfo != null)
            {
                string RevertContent = Tools.GetForm("RevertContent");
                if (RevertContent == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写回复内容", null, true);
                }
                else
                {
                    if (new BLL.Leaveword.Leaveword().Revert(LInfo.ID, RevertContent) != 0)
                        ShowWindow(3, "系统提示", "回复成功,点击 \\\"确定\\\" 按钮返回", "leaveword.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "回复失败", null, true);
                }
            }
        }
    }
}
