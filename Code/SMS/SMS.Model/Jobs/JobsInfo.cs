using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Jobs
{
    /// <summary>
    /// ��Ƹ
    /// </summary>
    public class JobsInfo
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
        /// ְλ����
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Dept;
        /// <summary>
        /// ��������
        /// </summary>
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }

        private string _Email;
        /// <summary>
        /// ��ϵ����
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Address;
        /// <summary>
        /// �����ص�
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Num;
        /// <summary>
        /// ��Ƹ����
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set { _Num = value; }
        }

        private string _Sex;
        /// <summary>
        /// �Ա�Ҫ��
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private string _Experience;
        /// <summary>
        /// ��������
        /// </summary>
        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }

        private string _Language;
        /// <summary>
        /// ����Ҫ��
        /// </summary>
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        private string _Salary;
        /// <summary>
        /// нˮ��Χ
        /// </summary>
        public string Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }

        private string _Education;
        /// <summary>
        /// ѧ��Ҫ��
        /// </summary>
        public string Education
        {
            get { return _Education; }
            set { _Education = value; }
        }

        private string _ResumeLanguage;
        /// <summary>
        /// ��������
        /// </summary>
        public string ResumeLanguage
        {
            get { return _ResumeLanguage; }
            set { _ResumeLanguage = value; }
        }

        private string _Content;
        /// <summary>
        /// ְλ����
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _Hits;
        /// <summary>
        /// �������
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private bool _State;
        /// <summary>
        /// ְλ״̬
        /// </summary>
        public bool State
        {
            get { return _State; }
            set { _State = value; }
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
