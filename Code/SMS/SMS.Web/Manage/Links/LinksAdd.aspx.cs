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
    public partial class LinksAdd : SMS.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "add")
            {
                string LinkType = Tools.GetForm("LinkType");
                string SiteName = Tools.GetForm("SiteName");
                string SiteDomain = Tools.GetForm("SiteDomain");
                string LogoAddress = Tools.GetForm("LogoAddress");
                string LogoWidth = Tools.GetForm("LogoWidth");
                string LogoHeight = Tools.GetForm("LogoHeight");

                if (SiteName == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д��վ����", null, true);
                }
                else if (SiteDomain == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����д��վ����", null, true);
                }
                else if (LinkType == "2" && LogoAddress == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����дLogo��ַ", null, true);
                }
                else if (LinkType == "2" && LogoWidth == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����дLogo���", null, true);
                }
                else if (LinkType == "2" && LogoHeight == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����дLogo�߶�", null, true);
                }
                else
                {
                    LinksInfo LInfo = new LinksInfo();
                    LInfo.SiteName = SiteName;
                    LInfo.SiteDomain = SiteDomain;
                    LInfo.LogoAddress = LogoAddress;
                    LInfo.LogoWidth = LogoWidth;
                    LInfo.LogoHeight = LogoHeight;
                    if (new BLL.Links.Links().Add(LInfo) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "����������ӳɹ�,��� \\\"ȷ��\\\" ��ť�������������б�ҳ��", "links.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "�����������ʧ��", null, true);
                }
            }
        }
    }
}
