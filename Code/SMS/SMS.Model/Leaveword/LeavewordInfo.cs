using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Leaveword
{
    /// <summary>
    /// ����
    /// </summary>
    public class LeavewordInfo
    {
        private int _ID;
        /// <summary>
        /// ���
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Email;
        /// <summary>
        /// ����
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Phone;
        /// <summary>
        /// �绰
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _Content;
        /// <summary>
        /// ��������
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private bool _View;
        /// <summary>
        /// �Ƿ�鿴
        /// </summary>
        public bool View
        {
            get { return _View; }
            set { _View = value; }
        }

        private bool _Revert;
        /// <summary>
        /// �Ƿ�ظ�
        /// </summary>
        public bool Revert
        {
            get { return _Revert; }
            set { _Revert = value; }
        }

        private string _RevertContent;
        /// <summary>
        /// �ظ�����
        /// </summary>
        public string RevertContent
        {
            get { return _RevertContent; }
            set { _RevertContent = value; }
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
