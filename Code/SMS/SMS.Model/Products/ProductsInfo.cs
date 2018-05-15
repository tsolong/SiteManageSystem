using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Products
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public class ProductsInfo
    {
        private int _ID;
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _CategoryID;
        /// <summary>
        /// ��Ʒ����������
        /// </summary>
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _Name;
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Picture;
        /// <summary>
        /// ��ƷͼƬ
        /// </summary>
        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private string _Content;
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _Hits;
        /// <summary>
        /// ��Ʒ�������
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// ��Ʒ��������
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
