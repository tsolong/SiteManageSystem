using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Products;

namespace SMS.SQLServerDAL.Products
{
    public class Products : BaseDB
    {
        public int Add(ProductsInfo PInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            MyPar[0] = new SqlParameter("@CategoryID", SqlDbType.Int, 4);
            MyPar[0].Value = PInfo.CategoryID;
            MyPar[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            MyPar[1].Value = PInfo.Name;
            MyPar[2] = new SqlParameter("@Picture", SqlDbType.NVarChar, 40);
            MyPar[2].Value = PInfo.Picture;
            MyPar[3] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[3].Value = PInfo.Content;

            string sql = "insert into [" + Pre + "_Products]([CategoryID],[Name],[Picture],[Content]) values(@CategoryID,@Name,@Picture,@Content)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_Products] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int DelByCategory(string CategoryID)
        {
            string sql = "delete from [" + Pre + "_Products] where [CategoryID] in(" + CategoryID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(ProductsInfo PInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[5];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = PInfo.ID;
            MyPar[1] = new SqlParameter("@CategoryID", SqlDbType.Int, 4);
            MyPar[1].Value = PInfo.CategoryID;
            MyPar[2] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
            MyPar[2].Value = PInfo.Name;
            MyPar[3] = new SqlParameter("@Picture", SqlDbType.NVarChar, 40);
            MyPar[3].Value = PInfo.Picture;
            MyPar[4] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[4].Value = PInfo.Content;

            string sql = "update [" + Pre + "_Products] set [CategoryID]=@CategoryID,[Name]=@Name,[Picture]=@Picture,[Content]=@Content where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int UpdateHits(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_Products] set [Hits]=[Hits]+1 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<ProductsInfo> DrRead(SqlDataReader dr)
        {
            IList<ProductsInfo> PInfoList = new List<ProductsInfo>();
            while (dr.Read())
            {
                ProductsInfo PInfo = new ProductsInfo();
                PInfo.ID = Convert.ToInt32(dr["ID"]);
                PInfo.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                PInfo.Name = Convert.ToString(dr["Name"]);
                PInfo.Picture = Convert.ToString(dr["Picture"]);
                PInfo.Content = Convert.ToString(dr["Content"]);
                PInfo.Hits = Convert.ToInt32(dr["Hits"]);
                PInfo.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                PInfoList.Add(PInfo);
            }
            dr.Close();
            return PInfoList;
        }

        public ProductsInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Products] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<ProductsInfo> PInfoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (PInfoList.Count > 0)
                return PInfoList[0];
            else
                return null;
        }

        public IList<ProductsInfo> GetListByCategory(int PageIndex, int PageSize, out int RecordTotal, string CategoryIDList)
        {
            SqlParameter[] MyPar = new SqlParameter[8];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Products]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 1000);
            MyPar[1].Value = "*";
            MyPar[2] = new SqlParameter("@SelectWhere", SqlDbType.VarChar, 1500);
            MyPar[2].Value = "[CategoryID] in (" + CategoryIDList + ")";
            MyPar[3] = new SqlParameter("@OrderColumnName", SqlDbType.VarChar, 255);
            MyPar[3].Value = "[ID]";
            MyPar[4] = new SqlParameter("@OrderType", SqlDbType.Bit);
            MyPar[4].Value = true;
            MyPar[5] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
            MyPar[5].Value = PageSize;
            MyPar[6] = new SqlParameter("@PageIndex", SqlDbType.Int, 4);
            MyPar[6].Value = PageIndex;
            MyPar[7] = new SqlParameter("@RecordTotal", SqlDbType.Int, 4);
            MyPar[7].Direction = ParameterDirection.Output;

            IList<ProductsInfo> PInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[7].Value);
            return PInfoList;
        }

        /// <summary>
        /// 获取产品对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>产品对象列表</returns>
        public IList<ProductsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Products]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 1000);
            MyPar[1].Value = "*";
            MyPar[2] = new SqlParameter("@OrderColumnName", SqlDbType.VarChar, 255);
            MyPar[2].Value = "[ID]";
            MyPar[3] = new SqlParameter("@OrderType", SqlDbType.Bit);
            MyPar[3].Value = true;
            MyPar[4] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
            MyPar[4].Value = PageSize;
            MyPar[5] = new SqlParameter("@PageIndex", SqlDbType.Int, 4);
            MyPar[5].Value = PageIndex;
            MyPar[6] = new SqlParameter("@RecordTotal", SqlDbType.Int, 4);
            MyPar[6].Direction = ParameterDirection.Output;

            IList<ProductsInfo> PInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[6].Value);
            return PInfoList;
        }

        public ArrayList GetPictureByID(string ID)
        {
            ArrayList PictureList = new ArrayList();

            string sql = "select [Picture] from [" + Pre + "_Products] where [ID] in(" + ID + ") order by [ID] desc";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);
            while (dr.Read())
            {
                PictureList.Add(Convert.ToString(dr["Picture"]));
            }
            dr.Close();

            return PictureList;
        }

        public ArrayList GetPictureByCategory(string CategoryID)
        {
            ArrayList PictureList = new ArrayList();

            string sql = "select [Picture] from [" + Pre + "_Products] where [CategoryID] in(" + CategoryID + ") order by [ID] desc";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);
            while (dr.Read())
            {
                PictureList.Add(Convert.ToString(dr["Picture"]));
            }
            dr.Close();

            return PictureList;
        }
    }
}
