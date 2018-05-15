using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Leaveword
{
    /// <summary>
    /// 留言
    /// </summary>
    public class LeavewordInfo
    {
        private int _ID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Email;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Phone;
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Content;
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private bool _View;
        /// <summary>
        /// 是否查看
        /// </summary>
        public bool View
        {
            get { return _View; }
            set { _View = value; }
        }

        private bool _Revert;
        /// <summary>
        /// 是否回复
        /// </summary>
        public bool Revert
        {
            get { return _Revert; }
            set { _Revert = value; }
        }

        private string _RevertContent;
        /// <summary>
        /// 回复内容
        /// </summary>
        public string RevertContent
        {
            get { return _RevertContent; }
            set { _RevertContent = value; }
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
