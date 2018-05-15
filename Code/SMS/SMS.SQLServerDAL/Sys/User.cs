using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model;
using SMS.Model.Sys;

namespace SMS.SQLServerDAL.Sys
{
    /// <summary>
    /// ϵͳ�û�
    /// </summary>
    public class User : BaseDB
    {
        /// <summary>
        /// ����¼
        /// </summary>
        /// <param name="SysUser">��¼����</param>
        /// <returns>��¼״̬</returns>
        public LoginState CheckLogin(UserInfo SysUser)
        {
            //��ѯ
            string sql = "select top 1 * from [" + Pre + "_Sys_User] where [UserName]=@UserName";
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = SysUser.UserName;
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar);

            bool isExist = false;
            bool Password = false;

            UserInfo TmpSysUser = null;

            while (dr.Read())
            {
                isExist = true;
                if (Convert.ToString(dr["Password"]) == SysUser.Password)
                {
                    Password = true;

                    SysUser.UserID = Convert.ToInt32(dr["UserID"]);

                    //��ȡ�ϴε�¼��Ϣ
                    TmpSysUser = new UserInfo();
                    if (Convert.IsDBNull(dr["LastLoginDate"])) TmpSysUser.LastLoginDate = null; else TmpSysUser.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
                    if (dr["LastLoginIP"] == DBNull.Value) TmpSysUser.LastLoginIP = ""; else TmpSysUser.LastLoginIP = dr["LastLoginIP"].ToString();
                }
            }
            dr.Close();

            if (!isExist) return LoginState.Err_NotUser;
            if (!Password) return LoginState.Err_Password;


            //������ε�¼��Ϣ
            UpdateUserLoginInfo(SysUser);

            //�����ϴε�¼��Ϣ

            SysUser.Password = "";
            SysUser.LastLoginDate = TmpSysUser.LastLoginDate;
            SysUser.LastLoginIP = TmpSysUser.LastLoginIP;

            return LoginState.Succeed;
        }

        /// <summary>
        /// ���µ�¼��Ϣ
        /// </summary>
        /// <param name="SysUser">Ҫ���µ��û�����</param>
        /// <returns>���¼�¼������</returns>
        private int UpdateUserLoginInfo(UserInfo SysUser)
        {
            String sql = "update [" + Pre + "_Sys_User] set [LastLoginDate]=@LastLoginDate,[LastLoginIP]=@LastLoginIP where [UserID]=@UserID";
            SqlParameter[] MyPar = new SqlParameter[3];
            MyPar[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            MyPar[0].Value = SysUser.UserID;
            MyPar[1] = new SqlParameter("@LastLoginDate", SqlDbType.DateTime, 8);
            MyPar[1].Value = SysUser.LastLoginDate;
            MyPar[2] = new SqlParameter("@LastLoginIP", SqlDbType.NVarChar, 15);
            MyPar[2].Value = SysUser.LastLoginIP;
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ����û����Ƿ��Ѵ���
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <returns>�Ѵ��ڷ���true �����ڷ���false</returns>
        public bool CheckUserNameIsExist(string UserName)
        {
            SqlParameter MyPar = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar.Value = UserName;

            string sql = "select count(*) from [" + Pre + "_Sys_User] where [UserName]=@UserName";
            if ((int)(SqlHelper.ExecuteScalar(ConnStr, CommandType.Text, sql, MyPar)) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="SysUser">�û�����</param>
        /// <returns>������Ӱ�������</returns>
        public int Add(UserInfo SysUser)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 16);
            MyPar[0].Value = SysUser.UserName;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = SysUser.Password;

            string sql = "insert into [" + Pre + "_Sys_User]([UserName],[Password]) values(@UserName,@Password)";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ɾ���û� ��ɾ����������
        /// </summary>
        /// <param name="UserID">�û���UserID</param>
        /// <returns>������Ӱ������</returns>
        public int Del(string UserID)
        {
            /*SqlParameter MyPar = new SqlParameter("@UserID", SqlDbType.NVarChar, 1000);
            MyPar.Value = UserID;*/
            string sql = "delete from [" + Pre + "_Sys_User] where [UserID] in(" + UserID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="UserID">�û���UserID</param>
        /// <param name="Password">�µ�����</param>
        /// <returns>������Ӱ������</returns>
        public int ChangePassword(int UserID, string Password)
        {
            SqlParameter[] MyPar = new SqlParameter[2];
            MyPar[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            MyPar[0].Value = UserID;
            MyPar[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 16);
            MyPar[1].Value = Password;

            string sql = "update [" + Pre + "_Sys_User] set [Password]=@Password where [UserID]=@UserID";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, MyPar);
        }

        /// <summary>
        /// ��DataReader�е�����ת��Ϊ���󼯺�
        /// </summary>
        /// <param name="dr">DataReader����</param>
        /// <returns>���󼯺�</returns>
        private IList<UserInfo> DrRead(SqlDataReader dr)
        {
            IList<UserInfo> SysUserList = new List<UserInfo>();
            while (dr.Read())
            {
                UserInfo SysUser = new UserInfo();
                SysUser.UserID = Convert.ToInt32(dr["UserID"]);
                SysUser.UserName = Convert.ToString(dr["UserName"]);
                SysUser.Password = Convert.ToString(dr["Password"]);
                //if (dr["LastLoginDate"].Equals(DBNull.Value))
                //if (dr["LastLoginDate"] == DBNull.Value)
                if (Convert.IsDBNull(dr["LastLoginDate"])) SysUser.LastLoginDate = null; else SysUser.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
                if (dr["LastLoginIP"] == DBNull.Value) SysUser.LastLoginIP = ""; else SysUser.LastLoginIP = dr["LastLoginIP"].ToString();
                SysUser.CreateDate = Convert.ToDateTime(dr["CreateDate"]);

                SysUserList.Add(SysUser);
            }
            dr.Close();
            return SysUserList;
        }

        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <param name="UserID">�û����</param>
        /// <returns>�û�����</returns>
        public UserInfo GetByID(int UserID)
        {
            string sql = "select top 1 * from [" + Pre + "_Sys_User] where [UserID]=@UserID";
            SqlParameter MyPar = new SqlParameter("@UserID", SqlDbType.Int, 4);
            MyPar.Value = UserID;

            IList<UserInfo> SysUserList = DrRead(SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, MyPar));
            if (SysUserList.Count > 0)
                return SysUserList[0];
            else
                return null;
        }

        /// <summary>
        /// ��ȡ�û������б�
        /// </summary>
        /// <param name="PageIndex">ҳ��</param>
        /// <param name="PageSize">ÿҳ��������</param>
        /// <param name="RecordTotal">�洢���̷��ؼ�¼����</param>
        /// <returns>�û������б�</returns>
        public IList<UserInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            SqlParameter[] MyPar = new SqlParameter[6];
            MyPar[0] = new SqlParameter("@TableName", SqlDbType.VarChar, 100);
            MyPar[0].Value = "[" + Pre + "_Sys_User]";
            MyPar[1] = new SqlParameter("@SelectColumnName", SqlDbType.VarChar, 1000);
            MyPar[1].Value = "*";
            /*MyPar[2] = new SqlParameter("@SelectWhere", SqlDbType.VarChar, 100);
            MyPar[2].Value = "[locked]=0";*/
            MyPar[2] = new SqlParameter("@OrderColumnName", SqlDbType.VarChar, 255);
            MyPar[2].Value = "UserID";
            MyPar[3] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
            MyPar[3].Value = PageSize;
            MyPar[4] = new SqlParameter("@PageIndex", SqlDbType.Int, 4);
            MyPar[4].Value = PageIndex;
            MyPar[5] = new SqlParameter("@RecordTotal", SqlDbType.Int, 4);
            MyPar[5].Direction = ParameterDirection.Output;

            IList<UserInfo> SysUserList = DrRead(SqlHelper.ExecuteReaderPage(ConnStr, CommandType.StoredProcedure, "DataPage", MyPar));
            RecordTotal = Convert.ToInt32(MyPar[5].Value);
            return SysUserList;
        }
    }
}
