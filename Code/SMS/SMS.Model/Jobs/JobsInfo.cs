using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Jobs
{
    /// <summary>
    /// 招聘
    /// </summary>
    public class JobsInfo
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
        /// 职位名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Dept;
        /// <summary>
        /// 工作部门
        /// </summary>
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }

        private string _Email;
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Address;
        /// <summary>
        /// 工作地点
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _Num;
        /// <summary>
        /// 招聘人数
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set { _Num = value; }
        }

        private string _Sex;
        /// <summary>
        /// 性别要求
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private string _Experience;
        /// <summary>
        /// 工作年限
        /// </summary>
        public string Experience
        {
            get { return _Experience; }
            set { _Experience = value; }
        }

        private string _Language;
        /// <summary>
        /// 语言要求
        /// </summary>
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }

        private string _Salary;
        /// <summary>
        /// 薪水范围
        /// </summary>
        public string Salary
        {
            get { return _Salary; }
            set { _Salary = value; }
        }

        private string _Education;
        /// <summary>
        /// 学历要求
        /// </summary>
        public string Education
        {
            get { return _Education; }
            set { _Education = value; }
        }

        private string _ResumeLanguage;
        /// <summary>
        /// 简历语言
        /// </summary>
        public string ResumeLanguage
        {
            get { return _ResumeLanguage; }
            set { _ResumeLanguage = value; }
        }

        private string _Content;
        /// <summary>
        /// 职位描述
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _Hits;
        /// <summary>
        /// 点击次数
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private bool _State;
        /// <summary>
        /// 职位状态
        /// </summary>
        public bool State
        {
            get { return _State; }
            set { _State = value; }
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
