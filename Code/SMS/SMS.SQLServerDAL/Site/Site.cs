using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Model.Site;

namespace SMS.SQLServerDAL.Site
{
    /// <summary>
    /// 网站信息
    /// </summary>
    public class Site : BaseDB
    {
        /// <summary>
        /// 获取网站信息
        /// </summary>
        /// <returns>网站信息对象</returns>
        public SiteInfo Get()
        {
            SiteInfo SInfo = null;
            string sql = "select top 1 * from [" + Pre + "_SiteInfo]";
            SqlDataReader dr = SqlHelper.ExecuteReader(ConnStr, CommandType.Text, sql, null);
            while (dr.Read())
            {
                SInfo = new SiteInfo();
                SInfo.SiteName = Convert.ToString(dr["SiteName"]);
                SInfo.SiteDomain = Convert.ToString(dr["SiteDomain"]);
                SInfo.SiteEmail = Convert.ToString(dr["SiteEmail"]);
                SInfo.SiteKeywords = Convert.ToString(dr["SiteKeywords"]);
                SInfo.SiteDescription = Convert.ToString(dr["SiteDescription"]);
                SInfo.SiteCopyright = Convert.ToString(dr["SiteCopyright"]);
            }
            dr.Close();
            return SInfo;
        }

        /// <summary>
        /// 保存网站信息
        /// </summary>
        /// <param name="SInfo">网站信息对象</param>
        /// <returns>操作所影响行数</returns>
        public int Save(SiteInfo SInfo)
        {
            String sql = "update [" + Pre + "_SiteInfo] set [SiteName]=@SiteName,[SiteDomain]=@SiteDomain,[SiteEmail]=@SiteEmail,[SiteKeywords]=@SiteKeywords,[SiteDescription]=@SiteDescription,[SiteCopyright]=@SiteCopyright";
            return SqlHelper.ExecuteNonQuery(ConnStr, CommandType.Text, sql, GetParameters(SInfo));
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="SInfo">网站信息对象</param>
        /// <returns>参数集合</returns>
        private SqlParameter[] GetParameters(SiteInfo SInfo)
        {
            SqlParameter[] MyPar = new SqlParameter[6];
            MyPar[0] = new SqlParameter("@SiteName", SqlDbType.NVarChar, 50);
            MyPar[0].Value = SInfo.SiteName;
            MyPar[1] = new SqlParameter("@SiteDomain", SqlDbType.NVarChar, 50);
            MyPar[1].Value = SInfo.SiteDomain;
            MyPar[2] = new SqlParameter("@SiteEmail", SqlDbType.NVarChar, 50);
            MyPar[2].Value = SInfo.SiteEmail;
            MyPar[3] = new SqlParameter("@SiteKeywords", SqlDbType.NVarChar, 50);
            MyPar[3].Value = SInfo.SiteKeywords;
            MyPar[4] = new SqlParameter("@SiteDescription", SqlDbType.NVarChar, 50);
            MyPar[4].Value = SInfo.SiteDescription;
            MyPar[5] = new SqlParameter("@SiteCopyright", SqlDbType.NText);
            MyPar[5].Value = SInfo.SiteCopyright;
            return MyPar;
        }
    }
}
