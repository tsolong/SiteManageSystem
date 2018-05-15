using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Editor;

namespace SMS.SQLServerDAL.Editor
{
    public class Editor : BaseDB
    {
        public int Save(EditorInfo EInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar[0].Value = EInfo.ID;
            MyPar[1] = new SqlParameter("@Content", SqlDbType.NVarChar);
            MyPar[1].Value = EInfo.Content;
            string sql = "update [" + Pre + "_Editor] set [Content]=@Content where [ID]=@ID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        public EditorInfo GetByID(int ID)
        {
            string sql = "select * from [" + Pre + "_Editor] where [ID]=@ID";
            SqlParameter MyPar = new SqlParameter("@ID", SqlDbType.Int, 4);
            MyPar.Value = ID;

            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);
            EditorInfo EInfo = null;
            while (dr.Read())
            {
                EInfo = new EditorInfo();
                EInfo.ID = Convert.ToInt32(dr["ID"]);
                EInfo.Name = Convert.ToString(dr["Name"]);
                EInfo.Content = Convert.ToString(dr["Content"]);
            }
            dr.Close();
            return EInfo;
        }
    }
}
