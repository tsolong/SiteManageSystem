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
using SMS.Model.Jobs;

namespace SMS.Web.Manage.Jobs
{
    public partial class Jobs : SMS.Web.UI.SysUserPage
    {
        public int PageIndex;
        private int PageSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageSize"));
        private int PageNumTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageNumTotal"));
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";
        public IList<JobsInfo> JInfoList;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Tools.GetQueryString("action").ToLower())
            {
                case "del":
                    Del();
                    break;
                case "start":
                    Start();
                    break;
                case "stop":
                    Stop();
                    break;
                default:
                    GetList();
                    break;
            }
        }

        /// <summary>
        /// ɾ����Ƹ
        /// </summary>
        private void Del()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                if (new BLL.Jobs.Jobs().Del(ID) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "ɾ����Ƹ�ɹ�,��� \\\"ȷ��\\\" ��ť����", "jobs.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "ɾ����Ƹʧ��", null, true);
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫɾ������Ƹ", null, true);
            }
        }

        /// <summary>
        /// ������Ƹ
        /// </summary>
        private void Start()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                if (new BLL.Jobs.Jobs().Start(Convert.ToInt32(ID)) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "������Ƹ�ɹ�,��� \\\"ȷ��\\\" ��ť����", "jobs.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "������Ƹʧ��", null, true);
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫ���õ���Ƹ", null, true);
            }
        }

        /// <summary>
        /// ֹͣ��Ƹ
        /// </summary>
        private void Stop()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                if (new BLL.Jobs.Jobs().Stop(Convert.ToInt32(ID)) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "ֹͣ��Ƹ�ɹ�,��� \\\"ȷ��\\\" ��ť����", "jobs.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "ֹͣ��Ƹʧ��", null, true);
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫֹͣ����Ƹ", null, true);
            }
        }

        /// <summary>
        /// ��ȡ��Ƹ�����б�
        /// </summary>
        private void GetList()
        {
            //���PageIndex����
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {
                PageIndex = Convert.ToInt32(tempIndex);

                JInfoList = new BLL.Jobs.Jobs().GetList(PageIndex, PageSize, out RecordTotal);
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
