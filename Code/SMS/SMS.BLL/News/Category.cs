using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.News;

namespace SMS.BLL.News
{
    public class Category
    {
        private SQLServerDAL.News.Category dal;
        public Category()
        {
            dal = new SMS.SQLServerDAL.News.Category();
        }

        public int Add(CategoryInfo NewsCategory)
        {
            return dal.Add(NewsCategory);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int Save(CategoryInfo NewsCategory)
        {
            return dal.Save(NewsCategory);
        }

        public IList<CategoryInfo> GetList()
        {
            return dal.GetList();
        }

        public string GetCategoryNameByID(int ID)
        {
            return dal.GetCategoryNameByID(ID);
        }

        public CategoryInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<CategoryInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
