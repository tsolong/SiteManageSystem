using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Jobs;

namespace SMS.SQLServerDAL.Jobs
{
    public class Jobs : BaseDB
    {
        public int Add(JobsInfo JInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[12];
            MyPar[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
            MyPar[0].Value = JInfo.Name;
            MyPar[1] = new SqlParameter("@Dept", SqlDbType.NVarChar, 10);
            MyPar[1].Value = JInfo.Dept;
            MyPar[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[2].Value = JInfo.Email;
            MyPar[3] = new SqlParameter("@Address", SqlDbType.NVarChar, 50);
            MyPar[3].Value = JInfo.Address;
            MyPar[4] = new SqlParameter("@Num", SqlDbType.NVarChar, 10);
            MyPar[4].Value = JInfo.Num;
            MyPar[5] = new SqlParameter("@Sex", SqlDbType.NVarChar, 10);
            MyPar[5].Value = JInfo.Sex;
            MyPar[6] = new SqlParameter("@Experience", SqlDbType.NVarChar, 10);
            MyPar[6].Value = JInfo.Experience;
            MyPar[7] = new SqlParameter("@Language", SqlDbType.NVarChar, 10);
            MyPar[7].Value = JInfo.Language;
            MyPar[8] = new SqlParameter("@Salary", SqlDbType.NVarChar, 10);
            MyPar[8].Value = JInfo.Salary;
            MyPar[9] = new SqlParameter("@Education", SqlDbType.NVarChar, 10);
            MyPar[9].Value = JInfo.Education;
            MyPar[10] = new SqlParameter("@ResumeLanguage", SqlDbType.NVarChar, 10);
            MyPar[10].Value = JInfo.ResumeLanguage;
            MyPar[11] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[11].Value = JInfo.Content;

            string sql = "insert into [" + Pre + "_Jobs]([Name],[Dept],[Email],[Address],[Num],[Sex],[Experience],[Language],[Salary],[Education],[ResumeLanguage],[Content]) values(@Name,@Dept,@Email,@Address,@Num,@Sex,@Experience,@Language,@Salary,@Education,@ResumeLanguage,@Content)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Del(string ID)
        {
            string sql = "delete from [" + Pre + "_Jobs] where [ID] in(" + ID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        public int Save(JobsInfo JInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[13];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = JInfo.ID;
            MyPar[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 10);
            MyPar[1].Value = JInfo.Name;
            MyPar[2] = new SqlParameter("@Dept", SqlDbType.NVarChar, 10);
            MyPar[2].Value = JInfo.Dept;
            MyPar[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 30);
            MyPar[3].Value = JInfo.Email;
            MyPar[4] = new SqlParameter("@Address", SqlDbType.NVarChar, 50);
            MyPar[4].Value = JInfo.Address;
            MyPar[5] = new SqlParameter("@Num", SqlDbType.NVarChar, 10);
            MyPar[5].Value = JInfo.Num;
            MyPar[6] = new SqlParameter("@Sex", SqlDbType.NVarChar, 10);
            MyPar[6].Value = JInfo.Sex;
            MyPar[7] = new SqlParameter("@Experience", SqlDbType.NVarChar, 10);
            MyPar[7].Value = JInfo.Experience;
            MyPar[8] = new SqlParameter("@Language", SqlDbType.NVarChar, 10);
            MyPar[8].Value = JInfo.Language;
            MyPar[9] = new SqlParameter("@Salary", SqlDbType.NVarChar, 10);
            MyPar[9].Value = JInfo.Salary;
            MyPar[10] = new SqlParameter("@Education", SqlDbType.NVarChar, 10);
            MyPar[10].Value = JInfo.Education;
            MyPar[11] = new SqlParameter("@ResumeLanguage", SqlDbType.NVarChar, 10);
            MyPar[11].Value = JInfo.ResumeLanguage;
            MyPar[12] = new SqlParameter("@Content", SqlDbType.NText);
            MyPar[12].Value = JInfo.Content;

            string sql = "update [" + Pre + "_Jobs] set [Name]=@Name,[Dept]=@Dept,[Email]=@Email,[Address]=@Address,[Num]=@Num,[Sex]=@Sex,[Experience]=@Experience,[Language]=@Language,[Salary]=@Salary,[Education]=@Education,[ResumeLanguage]=@ResumeLanguage,[Content]=@Content where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int UpdateHits(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_Jobs] set [Hits]=[Hits]+1 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Start(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_Jobs] set [State]=1 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public int Stop(int ID)
        {
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            string sql = "update [" + Pre + "_Jobs] set [State]=0 where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        private IList<JobsInfo> DrRead(SqlDataReader dr)
        {
            IList<JobsInfo> JInfoList = new List<JobsInfo>();
            while (dr.Read())
            {
                JobsInfo JInfo = new JobsInfo();
                JInfo.ID = Convert.ToInt32(dr["ID"]);
                JInfo.Name = Convert.ToString(dr["Name"]);
                JInfo.Dept = Convert.ToString(dr["Dept"]);
                JInfo.Email = Convert.ToString(dr["Email"]);
                JInfo.Address = Convert.ToString(dr["Address"]);
                JInfo.Num = Convert.ToString(dr["Num"]);
                JInfo.Sex = Convert.ToString(dr["Sex"]);
                JInfo.Experience = Convert.ToString(dr["Experience"]);
                JInfo.Language = Convert.ToString(dr["Language"]);
                JInfo.Salary = Convert.ToString(dr["Salary"]);
                JInfo.Education = Convert.ToString(dr["Education"]);
                JInfo.ResumeLanguage = Convert.ToString(dr["ResumeLanguage"]);
                JInfo.Content = Convert.ToString(dr["Content"]);
                JInfo.Hits = Convert.ToInt32(dr["Hits"]);
                JInfo.State = Convert.ToBoolean(dr["State"]);
                JInfo.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                JInfoList.Add(JInfo);
            }
            dr.Close();
            return JInfoList;
        }

        public JobsInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Jobs] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            IList<JobsInfo> JInfoList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (JInfoList.Count > 0)
                return JInfoList[0];
            else
                return null;
        }

        /// <summary>
        /// 获取招聘对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>招聘对象列表</returns>
        public IList<JobsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[7];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Jobs]";
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

            IList<JobsInfo> JInfoList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[6].Value);
            return JInfoList;
        }
    }
}
