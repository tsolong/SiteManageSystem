using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Products;

namespace SMS.SQLServerDAL.Products
{
    public class Category : BaseDB
    {
        public int Add(CategoryInfo ProductsCategory)
        {
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            MyPar[0].Value = ProductsCategory.ParentID;
            MyPar[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 10);
            MyPar[1].Value = ProductsCategory.CategoryName;
            MyPar[2] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            MyPar[2].Value = ProductsCategory.OrderID;

            string sql = "insert into [" + Pre + "_Products_Category]([ParentID],[CategoryName],[OrderID]) values(@ParentID,@CategoryName,@OrderID)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_Products_Category] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(CategoryInfo ProductsCategory)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = ProductsCategory.ID;
            MyPar[1] = new SqlParameter("@ParentID", SqlDbType.Int, 4);
            MyPar[1].Value = ProductsCategory.ParentID;
            MyPar[2] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 10);
            MyPar[2].Value = ProductsCategory.CategoryName;
            MyPar[3] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            MyPar[3].Value = ProductsCategory.OrderID;

            string sql = "update [" + Pre + "_Products_Category] set [ParentID]=@ParentID,[CategoryName]=@CategoryName,[OrderID]=@OrderID where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<CategoryInfo> DrRead(SqlDataReader dr)
        {
            IList<CategoryInfo> ProductsCategoryList = new List<CategoryInfo>();
            while (dr.Read())
            {
                CategoryInfo ProductsCategory = new CategoryInfo();
                ProductsCategory.ID = Convert.ToInt32(dr["ID"]);
                ProductsCategory.ParentID = Convert.ToInt32(dr["ParentID"]);
                ProductsCategory.CategoryName = Convert.ToString(dr["CategoryName"]);
                ProductsCategory.OrderID = Convert.ToInt32(dr["OrderID"]);
                ProductsCategory.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                ProductsCategoryList.Add(ProductsCategory);
            }
            dr.Close();
            return ProductsCategoryList;
        }

        public string GetCategoryNameByID(int ID)
        {
            string CategoryName = null;
            string sql = "select [CategoryName] from [" + Pre + "_Products_Category] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);
            while (dr.Read())
            {
                CategoryName = Convert.ToString(dr["CategoryName"]);
            }
            dr.Close();
            return CategoryName;
        }

        public CategoryInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Products_Category] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<CategoryInfo> ProductsCategoryList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (ProductsCategoryList.Count > 0)
                return ProductsCategoryList[0];
            else
                return null;
        }

        public IList<CategoryInfo> GetList()
        {
            string sql = "select * from [" + Pre + "_Products_Category] order by [OrderID],[ID]";
            return DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null));
        }
    }
}
