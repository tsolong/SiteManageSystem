using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.News;

namespace SMS.SQLServerDAL.News
{
    public class Category : BaseDB
    {
        public int Add(CategoryInfo NewsCategory)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 10);
            MyPar[0].Value = NewsCategory.CategoryName;
            MyPar[1] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            MyPar[1].Value = NewsCategory.OrderID;

            string sql = "insert into [" + Pre + "_News_Category]([CategoryName],[OrderID]) values(@CategoryName,@OrderID)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_News_Category] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(CategoryInfo NewsCategory)
        {
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = NewsCategory.ID;
            MyPar[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 10);
            MyPar[1].Value = NewsCategory.CategoryName;
            MyPar[2] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            MyPar[2].Value = NewsCategory.OrderID;

            string sql = "update [" + Pre + "_News_Category] set [CategoryName]=@CategoryName,[OrderID]=@OrderID where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<CategoryInfo> DrRead(SqlDataReader dr)
        {
            IList<CategoryInfo> NewsCategoryList = new List<CategoryInfo>();
            while (dr.Read())
            {
                CategoryInfo NewsCategory = new CategoryInfo();
                NewsCategory.ID = Convert.ToInt32(dr["ID"]);
                NewsCategory.CategoryName = Convert.ToString(dr["CategoryName"]);
                NewsCategory.OrderID = Convert.ToInt32(dr["OrderID"]);
                NewsCategory.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                NewsCategoryList.Add(NewsCategory);
            }
            dr.Close();
            return NewsCategoryList;
        }

        public string GetCategoryNameByID(int ID)
        {
            string CategoryName = null;
            string sql = "select [CategoryName] from [" + Pre + "_News_Category] where [ID]=@ID";
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
            string sql = "select * from [" + Pre + "_News_Category] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<CategoryInfo> NewsCategoryList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (NewsCategoryList.Count > 0)
                return NewsCategoryList[0];
            else
                return null;
        }

        public IList<CategoryInfo> GetList()
        {
            string sql = "select * from [" + Pre + "_News_Category] order by [OrderID]";
            return DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null));
        }

        /// <summary>
        /// 获取新闻分类对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>新闻分类对象列表</returns>
        public IList<CategoryInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[6];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_News_Category]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 1000);
            MyPar[1].Value = "*";
            MyPar[2] = new SqlParameter("@OrderColumnName", SqlDbType.VarChar, 255);
            MyPar[2].Value = "OrderID";
            MyPar[3] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
            MyPar[3].Value = PageSize;
            MyPar[4] = new SqlParameter("@PageIndex", SqlDbType.Int, 4);
            MyPar[4].Value = PageIndex;
            MyPar[5] = new SqlParameter("@RecordTotal", SqlDbType.Int, 4);
            MyPar[5].Direction = ParameterDirection.Output;

            IList<CategoryInfo> NewsCategoryList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[5].Value);
            return NewsCategoryList;
        }
    }
}
