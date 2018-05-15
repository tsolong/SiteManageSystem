using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Links;

namespace SMS.SQLServerDAL.Links
{
    public class Links : BaseDB
    {
        public int Add(LinksInfo LInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[5];
            MyPar[0] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
            MyPar[0].Value = LInfo.SiteName;
            MyPar[1] = new SqlParameter("@SiteDomain", SqlDbType.NVarChar, 50);
            MyPar[1].Value = LInfo.SiteDomain;
            MyPar[2] = new SqlParameter("@LogoAddress", SqlDbType.NVarChar, 50);
            MyPar[2].Value = LInfo.LogoAddress;
            MyPar[3] = new SqlParameter("@LogoWidth", SqlDbType.NVarChar, 10);
            MyPar[3].Value = LInfo.LogoWidth;
            MyPar[4] = new SqlParameter("@LogoHeight", SqlDbType.NVarChar, 10);
            MyPar[4].Value = LInfo.LogoHeight;

            string sql = "insert into [" + Pre + "_Links]([SiteName],[SiteDomain],[LogoAddress],[LogoWidth],[LogoHeight]) values(@SiteName,@SiteDomain,@LogoAddress,@LogoWidth,@LogoHeight)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_Links] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(LinksInfo LInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[6];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = LInfo.ID;
            MyPar[1] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
            MyPar[1].Value = LInfo.SiteName;
            MyPar[2] = new SqlParameter("@SiteDomain", SqlDbType.NVarChar, 50);
            MyPar[2].Value = LInfo.SiteDomain;
            MyPar[3] = new SqlParameter("@LogoAddress", SqlDbType.NVarChar, 50);
            MyPar[3].Value = LInfo.LogoAddress;
            MyPar[4] = new SqlParameter("@LogoWidth", SqlDbType.NVarChar, 10);
            MyPar[4].Value = LInfo.LogoWidth;
            MyPar[5] = new SqlParameter("@LogoHeight", SqlDbType.NVarChar, 10);
            MyPar[5].Value = LInfo.LogoHeight;

            string sql = "update [" + Pre + "_Links] set [SiteName]=@SiteName,[SiteDomain]=@SiteDomain,[LogoAddress]=@LogoAddress,[LogoWidth]=@LogoWidth,[LogoHeight]=@LogoHeight where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<LinksInfo> DrRead(SqlDataReader dr)
        {
            IList<LinksInfo> LInfoList = new List<LinksInfo>();
            while (dr.Read())
            {
                LinksInfo LInfo = new LinksInfo();
                LInfo.ID = Convert.ToInt32(dr["ID"]);
                LInfo.SiteName = Convert.ToString(dr["SiteName"]);
                LInfo.SiteDomain = Convert.ToString(dr["SiteDomain"]);
                LInfo.LogoAddress = Convert.ToString(dr["LogoAddress"]);
                LInfo.LogoWidth = Convert.ToString(dr["LogoWidth"]);
                LInfo.LogoHeight = Convert.ToString(dr["LogoHeight"]);
                LInfo.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                LInfoList.Add(LInfo);
            }
            dr.Close();
            return LInfoList;
        }

        public LinksInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Links] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<LinksInfo> LInfoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (LInfoList.Count > 0)
                return LInfoList[0];
            else
                return null;
        }

        /// <summary>
        /// 获取友情链接对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>友情链接对象列表</returns>
        public IList<LinksInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Links]";
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

            IList<LinksInfo> LInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[6].Value);
            return LInfoList;
        }
    }
}
