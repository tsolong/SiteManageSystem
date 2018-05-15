using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Links
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class LinksInfo
    {
        private int _ID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _SiteName;
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        private string _SiteDomain;
        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        private string _LogoAddress;
        /// <summary>
        /// //Logo地址
        /// </summary>
        public string LogoAddress
        {
            get { return _LogoAddress; }
            set { _LogoAddress = value; }
        }

        private string _LogoWidth;
        /// <summary>
        /// Logo宽度
        /// </summary>
        public string LogoWidth
        {
            get { return _LogoWidth; }
            set { _LogoWidth = value; }
        }

        private string _LogoHeight;
        /// <summary>
        /// Logo高度
        /// </summary>
        public string LogoHeight
        {
            get { return _LogoHeight; }
            set { _LogoHeight = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
