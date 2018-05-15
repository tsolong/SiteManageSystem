using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Leaveword;

namespace SMS.SQLServerDAL.Leaveword
{
    public class Leaveword : BaseDB
    {
        public int Add(LeavewordInfo LInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[4];
            MyPar[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
            MyPar[0].Value = LInfo.Name;
            MyPar[1] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[1].Value = LInfo.Email;
            MyPar[2] = new SqlParameter("@Phone", SqlDbType.NVarChar, 20);
            MyPar[2].Value = LInfo.Phone;
            MyPar[3] = new SqlParameter("@Content", SqlDbType.NVarChar);
            MyPar[3].Value = LInfo.Content;
            string sql = "insert into [" + Pre + "_Leaveword]([Name],[Email],[Phone],[Content]) values(@Name,@Email,@Phone,@Content)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_Leaveword] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Revert(int ID, string RevertContent)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = ID;
            MyPar[1] = new SqlParameter("@RevertContent", SqlDbType.NVarChar);
            MyPar[1].Value = RevertContent;

            string sql = "update [" + Pre + "_Leaveword] set [Revert]=1,[RevertContent]=@RevertContent where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int UpdateView(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_Leaveword] set [View]=1 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<LeavewordInfo> DrRead(SqlDataReader dr)
        {
            IList<LeavewordInfo> LInfoList = new List<LeavewordInfo>();
            while (dr.Read())
            {
                LeavewordInfo LInfo = new LeavewordInfo();
                LInfo.ID = Convert.ToInt32(dr["ID"]);
                LInfo.Name = Convert.ToString(dr["Name"]);
                LInfo.Email = Convert.ToString(dr["Email"]);
                LInfo.Phone = Convert.ToString(dr["Phone"]);
                LInfo.Content = Convert.ToString(dr["Content"]);
                LInfo.View = Convert.ToBoolean(dr["View"]);
                LInfo.Revert = Convert.ToBoolean(dr["Revert"]);
                if (dr["RevertContent"] == DBNull.Value) LInfo.RevertContent = ""; else LInfo.RevertContent = Convert.ToString(dr["RevertContent"]);
                LInfo.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                LInfoList.Add(LInfo);
            }
            dr.Close();
            return LInfoList;
        }

        public LeavewordInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Leaveword] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<LeavewordInfo> LInfoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (LInfoList.Count > 0)
                return LInfoList[0];
            else
                return null;
        }

        /// <summary>
        /// 获取留言对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>留言对象列表</returns>
        public IList<LeavewordInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Leaveword]";
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

            IList<LeavewordInfo> LInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[6].Value);
            return LInfoList;
        }
    }
}
