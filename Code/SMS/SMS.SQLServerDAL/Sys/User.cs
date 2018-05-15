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
    /// 系统用户
    /// </summary>
    public class User : BaseDB
    {
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="SysUser">登录对象</param>
        /// <returns>登录状态</returns>
        public LoginState CheckLogin(UserInfo SysUser)
        {
            //查询
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

                    //获取上次登录信息
                    TmpSysUser = new UserInfo();
                    if (Convert.IsDBNull(dr["LastLoginDate"])) TmpSysUser.LastLoginDate = null; else TmpSysUser.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
                    if (dr["LastLoginIP"] == DBNull.Value) TmpSysUser.LastLoginIP = ""; else TmpSysUser.LastLoginIP = dr["LastLoginIP"].ToString();
                }
            }
            dr.Close();

            if (!isExist) return LoginState.Err_NotUser;
            if (!Password) return LoginState.Err_Password;


            //更新这次登录信息
            UpdateUserLoginInfo(SysUser);

            //更新上次登录信息

            SysUser.Password = "";
            SysUser.LastLoginDate = TmpSysUser.LastLoginDate;
            SysUser.LastLoginIP = TmpSysUser.LastLoginIP;

            return LoginState.Succeed;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="SysUser">要更新的用户对象</param>
        /// <returns>更新记录的条数</returns>
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
        /// 检查用户名是否已存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>已存在返回true 不存在返回false</returns>
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
        /// 添加用户
        /// </summary>
        /// <param name="SysUser">用户对象</param>
        /// <returns>操作所影响的行数</returns>
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
        /// 删除用户 可删除单个或多个
        /// </summary>
        /// <param name="UserID">用户的UserID</param>
        /// <returns>操作所影响行数</returns>
        public int Del(string UserID)
        {
            /*SqlParameter MyPar = new SqlParameter("@UserID", SqlDbType.NVarChar, 1000);
            MyPar.Value = UserID;*/
            string sql = "delete from [" + Pre + "_Sys_User] where [UserID] in(" + UserID + ")";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserID">用户的UserID</param>
        /// <param name="Password">新的密码</param>
        /// <returns>操作所影响行数</returns>
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
        /// 将DataReader中的数据转换为对象集合
        /// </summary>
        /// <param name="dr">DataReader对象</param>
        /// <returns>对象集合</returns>
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
        /// 获取单个用户对象
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>用户对象</returns>
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
        /// 获取用户对象列表
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="RecordTotal">存储过程返回记录总数</param>
        /// <returns>用户对象列表</returns>
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
