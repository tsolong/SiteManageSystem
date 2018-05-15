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

namespace SMS.Web.Manage.Site
{
    public partial class SiteInfo : SMS.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "save")
            {
                string SiteName = Tools.GetForm("SiteName");
                string SiteDomain = Tools.GetForm("SiteDomain");
                string SiteEmail = Tools.GetForm("SiteEmail").ToLower();
                string SiteKeywords = Tools.GetForm("SiteKeywords");
                string SiteDescription = Tools.GetForm("SiteDescription");
                string SiteCopyright = Tools.GetForm("SiteCopyright");

                if (SiteName == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站名称", null, true);
                }
                else if (SiteDomain == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站域名", null, true);
                }
                else if (SiteEmail == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写网站邮箱", null, true);
                }
                else if (!Regex.IsMatch(SiteEmail, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                {
                    ShowWindow(4, "系统提示", "网站邮箱地址无效", null, true);
                }
                else
                {
                    Model.Site.SiteInfo SInfo = new Model.Site.SiteInfo();
                    SInfo.SiteName = SiteName;
                    SInfo.SiteDomain = SiteDomain;
                    SInfo.SiteEmail = SiteEmail;
                    SInfo.SiteKeywords = SiteKeywords;
                    SInfo.SiteDescription = SiteDescription;
                    SInfo.SiteCopyright = SiteCopyright;

                    if (new BLL.Site.Site().Save(SInfo) != 0)
                        ShowWindow(3, "系统提示", "保存存功,点击 \\\"确定\\\" 按钮返回", "siteinfo.aspx", false);
                    else
                        ShowWindow(4, "系统提示", "保存失败", null, true);
                }
            }
        }
    }
}
