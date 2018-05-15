using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Links;

namespace SMS.BLL.Links
{
    public class Links
    {
        private SQLServerDAL.Links.Links dal;
        public Links()
        {
            dal = new SMS.SQLServerDAL.Links.Links();
        }

        public int Add(LinksInfo LInfo)
        {
            return dal.Add(LInfo);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int Save(LinksInfo LInfo)
        {
            return dal.Save(LInfo);
        }

        public LinksInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<LinksInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
