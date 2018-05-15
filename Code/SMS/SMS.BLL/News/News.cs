using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.News;

namespace SMS.BLL.News
{
    public class News
    {
        private SQLServerDAL.News.News dal;
        public News()
        {
            dal = new SMS.SQLServerDAL.News.News();
        }

        public int Add(NewsInfo NInfo)
        {
            return dal.Add(NInfo);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int DelByCategory(string CategoryID)
        {
            return dal.DelByCategory(CategoryID);
        }

        public int Save(NewsInfo NInfo)
        {
            return dal.Save(NInfo);
        }

        public int UpdateHits(int ID)
        {
            return dal.UpdateHits(ID);
        }

        public NewsInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<NewsInfo> GetListByCategory(int PageIndex, int PageSize, out int RecordTotal, int CategoryID)
        {
            return dal.GetListByCategory(PageIndex, PageSize, out RecordTotal, CategoryID);
        }

        public IList<NewsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
