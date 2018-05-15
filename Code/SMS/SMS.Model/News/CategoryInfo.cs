using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.News
{
    /// <summary>
    /// ���ŷ���
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
    }
}
