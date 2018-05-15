using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Products;

namespace SMS.BLL.Products
{
    public class Category
    {
        private SQLServerDAL.Products.Category dal;
        public Category()
        {
            dal = new SMS.SQLServerDAL.Products.Category();
        }

        public int Add(CategoryInfo ProductsCategory)
        {
            return dal.Add(ProductsCategory);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int Save(CategoryInfo ProductsCategory)
        {
            return dal.Save(ProductsCategory);
        }

        public string GetCategoryNameByID(int ID)
        {
            return dal.GetCategoryNameByID(ID);
        }

        public CategoryInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<CategoryInfo> GetList()
        {
            return dal.GetList();
        }

        public void GetList(IList<CategoryInfo> ProductsCategoryList)
        {
            WhileCategory(dal.GetList(), ProductsCategoryList, 0, 0);
        }

        private void WhileCategory(IList<CategoryInfo> OriginalList, IList<CategoryInfo> ResultList, int ID, int Level)
        {
            Level++;
            for (int i = 0; i < OriginalList.Count; i++)
            {
                if (OriginalList[i].ParentID == ID)
                {
                    OriginalList[i].Level = Level;
                    ResultList.Add(OriginalList[i]);
                    WhileCategory(OriginalList, ResultList, OriginalList[i].ID, Level);
                }
            }
        }

        public string AddTab(int Level)
        {
            string Str = "";
            for (int i = 1; i < Level; i++)
            {
                Str += "|¡¡";
            }
            return Str + "|£­";
        }

        public string GetChildCategoryIDListByID(int ID)
        {
            ArrayList CategoryIDList = new ArrayList();
            WhileCategory(GetList(), CategoryIDList, ID);
            string Str = "";
            for (int i = CategoryIDList.Count - 1; i > -1; i--)
            {
                if (Str == "")
                    Str = CategoryIDList[i].ToString();
                else
                    Str += "," + CategoryIDList[i].ToString();
            }
            Str += Str != string.Empty ? "," + ID.ToString() : ID.ToString();
            return Str;
        }

        private void WhileCategory(IList<CategoryInfo> OriginalList, ArrayList CategoryIDList, int ID)
        {
            for (int i = 0; i < OriginalList.Count; i++)
            {
                if (OriginalList[i].ParentID == ID)
                {
                    CategoryIDList.Add(OriginalList[i].ID.ToString());
                    WhileCategory(OriginalList, CategoryIDList, OriginalList[i].ID);
                }
            }
        }
    }
}
