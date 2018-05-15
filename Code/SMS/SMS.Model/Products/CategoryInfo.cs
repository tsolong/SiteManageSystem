using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Products
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public class CategoryInfo
    {
        private int _ID;
        /// <summary>
        /// ������
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _ParentID;
        /// <summary>
        /// ������
        /// </summary>
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        private string _CategoryName;
        /// <summary>
        /// ��������
        /// </summary>
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private int _OrderID;
        /// <summary>
        /// ��������
        /// </summary>
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// ���ഴ������
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private int _Level = 0;
        /// <summary>
        /// ��n������
        /// </summary>
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
    }
}
