using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Site
{
    /// <summary>
    /// ��վ��Ϣ
    /// </summary>
    public class SiteInfo
    {
        private string _SiteName;
        /// <summary>
        ///��վ����
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _SiteDomain;
        /// <summary>
        /// ��վ����
        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        private string _SiteEmail;
        /// <summary>
        /// ��վ����
        /// </summary>
        public string SiteEmail
        {
            get { return _SiteEmail; }
            set { _SiteEmail = value; }
        }

        private string _SiteKeywords;
        /// <summary>
        /// ��վ�ؼ���
        /// </summary>
        public string SiteKeywords
        {
            get { return _SiteKeywords; }
            set { _SiteKeywords = value; }
        }

        private string _SiteDescription;
        /// <summary>
        /// ��վ����
        /// </summary>
        public string SiteDescription
        {
            get { return _SiteDescription; }
            set { _SiteDescription = value; }
        }

        private string _SiteCopyright;
        /// <summary>
        /// ��վ��Ȩ��Ϣ
        /// </summary>
        public string SiteCopyright
        {
            get { return _SiteCopyright; }
            set { _SiteCopyright = value; }
        }
    }
}
