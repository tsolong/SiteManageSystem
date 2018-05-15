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
using SMS.Model.Editor;

namespace SMS.Web.Manage.Editor
{
    public partial class Editor : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public EditorInfo EInfo;

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

            EInfo = new BLL.Editor.Editor().GetByID(ID);
            if (EInfo != null)
                MyEditor.Value = EInfo.Content;

            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string Content = HttpUtility.HtmlDecode(Request.Form["MyEditor"]);
                if (Content == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写内容", null, true);
                }
                else
                {
                    EInfo.Content = Content;
                    if (new BLL.Editor.Editor().Save(EInfo) != 0)
                        ShowWindow(3, "系统提示", "保存成功", "editor.aspx?id=" + ID, false);
                    else
                        ShowWindow(4, "系统提示", "保存失败", null, true);
                }
            }
        }
    }
}
