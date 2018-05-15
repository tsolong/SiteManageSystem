using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.News
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class NewsInfo
    {
        private int _ID;
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _CategoryID;
        /// <summary>
        /// 新闻所属分类编号
        /// </summary>
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _Title;
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Content;
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _Hits;
        /// <summary>
        /// 新闻点击次数
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 新闻创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
