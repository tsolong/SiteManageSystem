using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Sys
{
    /// <summary>
    /// ϵͳ�û�
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        private int _UserID;
        /// <summary>
        /// �û����
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _UserName;
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Password;
        /// <summary>
        /// ����
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private Nullable<DateTime> _LastLoginDate;
        /// <summary>
        /// ���һ�ε�¼����
        /// </summary>
        public Nullable<DateTime> LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        private string _LastLoginIP;
        /// <summary>
        /// ���һ�ε�¼IP
        /// </summary>
        public string LastLoginIP
        {
            get { return _LastLoginIP; }
            set { _LastLoginIP = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
