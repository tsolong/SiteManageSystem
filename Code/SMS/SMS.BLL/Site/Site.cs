using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Site;

namespace SMS.BLL.Site
{
    public class Site
    {
        private SMS.SQLServerDAL.Site.Site dal;
        public Site()
        {
            dal = new SMS.SQLServerDAL.Site.Site();
        }

        public SiteInfo Get()
        {
            return dal.Get();
        }

        public int Save(SiteInfo SInfo)
        {
            return dal.Save(SInfo);
        }
    }
}
