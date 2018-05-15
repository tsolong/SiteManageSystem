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
using SMS.Model.Links;

namespace SMS.Web.Manage.Links
{
    public partial class LinksEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public LinksInfo LInfo;

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

            LInfo = new BLL.Links.Links().GetByID(ID);

            if (Tools.GetQueryString("action").ToLower() == "save" && LInfo != null)
            {
                string LinkType = Tools.GetForm("LinkType");
                string SiteName = Tools.GetForm("SiteName");
                string SiteDomain = Tools.GetForm("SiteDomain");
                string LogoAddress = Tools.GetForm("LogoAddress");
                string LogoWidth = Tools.GetForm("LogoWidth");
                string LogoHeight = Tools.GetForm("LogoHeight");

                if (SiteName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站名称", null, true);
                }
                else if (SiteDomain == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站域名", null, true);
                }
                else if (LinkType == "2" && LogoAddress == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写Logo地址", null, true);
                }
                else if (LinkType == "2" && LogoWidth == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写Logo宽度", null, true);
                }
                else if (LinkType == "2" && LogoHeight == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写Logo高度", null, true);
                }
                else
                {
                    if (LinkType == "1")
                    {
                        LogoAddress = "";
                        LogoWidth = "";
                        LogoHeight = "";
                    }
                    LInfo.SiteName = SiteName;
                    LInfo.SiteDomain = SiteDomain;
                    LInfo.LogoAddress = LogoAddress;
                    LInfo.LogoWidth = LogoWidth;
                    LInfo.LogoHeight = LogoHeight;
                    if (new BLL.Links.Links().Save(LInfo) != 0)
                        ShowWindow(3, "系统提示", "友情链接保存成功,点击 \\\"确定\\\" 按钮返回", "links.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "友情链接保存失败", null, true);
                }
            }
        }
    }
}
