using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Site
{
    /// <summary>
    /// ÍøÕ¾ĞÅÏ¢
    /// </summary>
    public class SiteInfo
    {
        private string _SiteName;
        /// <summary>
        ///ÍøÕ¾Ãû³Æ
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _SiteDomain;
        /// <summary>
        /// ÍøÕ¾ÓòÃû
        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        private string _SiteEmail;
        /// <summary>
        /// ÍøÕ¾ÓÊÏä
        /// </summary>
        public string SiteEmail
        {
            get { return _SiteEmail; }
            set { _SiteEmail = value; }
        }

        private string _SiteKeywords;
        /// <summary>
        /// ÍøÕ¾¹Ø¼ü´Ê
        /// </summary>
        public string SiteKeywords
        {
            get { return _SiteKeywords; }
            set { _SiteKeywords = value; }
        }

        private string _SiteDescription;
        /// <summary>
        /// ÍøÕ¾ÃèÊö
        /// </summary>
        public string SiteDescription
        {
            get { return _SiteDescription; }
            set { _SiteDescription = value; }
        }

        private string _SiteCopyright;
        /// <summary>
        /// ÍøÕ¾°æÈ¨ĞÅÏ¢
        /// </summary>
        public string SiteCopyright
        {
            get { return _SiteCopyright; }
            set { _SiteCopyright = value; }
        }
    }
}
