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
using SMS.Model.News;

namespace SMS.Web.Manage.News
{
    public partial class News : SMS.Web.UI.SysUserPage
    {
        public int PageIndex;
        private int PageSize = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageSize"));
        private int PageNumTotal = Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ManagePageNumTotal"));
        public int RecordTotal = 0;
        private PageBar MyPageBar;
        public string PageBarHtml = "";

        public int CategoryID = 0;
        public IList<NewsInfo> NInfoList;
        public BLL.News.Category BNC = new SMS.BLL.News.Category();
        public BLL.News.News BNN = new SMS.BLL.News.News();
        public IList<CategoryInfo> NewsCategoryList = new SMS.BLL.News.Category().GetList();

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
        /// ɾ������
        /// </summary>
        private void Del()
        {
            string ID = Tools.GetQueryString("id");
            if (ID != string.Empty)
            {
                if (new BLL.News.News().Del(ID) != 0)
                    ShowWindow(3, "ϵͳ��ʾ", "ɾ�����ųɹ�,��� \\\"ȷ��\\\" ��ť����", "news.aspx", false);
                else
                    ShowWindow(4, "ϵͳ��ʾ", "ɾ������ʧ��", null, true);
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫɾ��������", null, true);
            }
        }

        /// <summary>
        /// ��ȡ���Ŷ����б�
        /// </summary>
        private void GetList()
        {
            //���PageIndex����
            string tempIndex = Tools.GetQueryString("p");
            if (tempIndex == "") tempIndex = "1";
            if (Tools.IsPositiveInt(tempIndex))
            {

                PageIndex = Convert.ToInt32(tempIndex);

                try
                {
                    CategoryID = Convert.ToInt32(Tools.GetQueryString("categoryid"));
                    NInfoList = BNN.GetListByCategory(PageIndex, PageSize, out RecordTotal, CategoryID);
                }
                catch
                {
                    NInfoList = BNN.GetList(PageIndex, PageSize, out RecordTotal);
                }

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
