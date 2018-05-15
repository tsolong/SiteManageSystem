using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.News;

namespace SMS.SQLServerDAL.News
{
    public class News : BaseDB
    {
        public int Add(NewsInfo NInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@CategoryID", SqlDbType.Int, 4);
            MyPar[0].Value = NInfo.CategoryID;
            MyPar[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            MyPar[1].Value = NInfo.Title;
            MyPar[2] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[2].Value = NInfo.Content;

            string sql = "insert into [" + Pre + "_News]([CategoryID],[Title],[Content]) values(@CategoryID,@Title,@Content)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_News] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int DelByCategory(string CategoryID)
        {
            string sql = "delete from [" + Pre + "_News] where [CategoryID] in(" + CategoryID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(NewsInfo NInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = NInfo.ID;
            MyPar[1] = new SqlParameter("@CategoryID", SqlDbType.Int, 4);
            MyPar[1].Value = NInfo.CategoryID;
            MyPar[2] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            MyPar[2].Value = NInfo.Title;
            MyPar[3] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[3].Value = NInfo.Content;

            string sql = "update [" + Pre + "_News] set [CategoryID]=@CategoryID,[Title]=@Title,[Content]=@Content where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int UpdateHits(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_News] set [Hits]=[Hits]+1 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<NewsInfo> DrRead(SqlDataReader dr)
        {
            IList<NewsInfo> NInfoList = new List<NewsInfo>();
            while (dr.Read())
            {
                NewsInfo NInfo = new NewsInfo();
                NInfo.ID = Convert.ToInt32(dr["ID"]);
                NInfo.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                NInfo.Title = Convert.ToString(dr["Title"]);
                NInfo.Content = Convert.ToString(dr["Content"]);
                NInfo.Hits = Convert.ToInt32(dr["Hits"]);
                NInfo.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                NInfoList.Add(NInfo);
            }
            dr.Close();
            return NInfoList;
        }

        public NewsInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_News] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<NewsInfo> NInfoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (NInfoList.Count > 0)
                return NInfoList[0];
            else
                return null;
        }

        public IList<NewsInfo> GetListByCategory(int PageIndex, int PageSize, out int RecordTotal, int CategoryID)
        {
            SqlParameter[] MyPar = new SqlParameter[8];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_News]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 1000);
            MyPar[1].Value = "*";
            MyPar[2] = new SqlParameter("@SelectWhere", SqlDbType.VarChar, 1500);
            MyPar[2].Value = "[CategoryID] = " + CategoryID;
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

            IList<NewsInfo> NInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[7].Value);
            return NInfoList;
        }

        /// <summary>
        /// 获取新闻对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>新闻对象列表</returns>
        public IList<NewsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_News]";
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

            IList<NewsInfo> NInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[6].Value);
            return NInfoList;
        }
    }
}
