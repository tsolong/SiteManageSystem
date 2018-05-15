using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        private int _UserID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private Nullable<DateTime> _LastLoginDate;
        /// <summary>
        /// 最后一次登录日期
        /// </summary>
        public Nullable<DateTime> LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        private string _LastLoginIP;
        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        public string LastLoginIP
        {
            get { return _LastLoginIP; }
            set { _LastLoginIP = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
