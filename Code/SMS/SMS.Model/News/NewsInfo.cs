using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.News
{
    /// <summary>
    /// ����
    /// </summary>
    public class NewsInfo
    {
        private int _ID;
        /// <summary>
        /// ���ű��
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _CategoryID;
        /// <summary>
        /// ��������������
        /// </summary>
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _Title;
        /// <summary>
        /// ���ű���
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
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

        private int _Hits;
        /// <summary>
        /// ���ŵ������
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// ���Ŵ�������
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
