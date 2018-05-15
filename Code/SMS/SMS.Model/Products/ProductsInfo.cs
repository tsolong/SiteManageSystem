using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Products
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProductsInfo
    {
        private int _ID;
        /// <summary>
        /// 产品编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _CategoryID;
        /// <summary>
        /// 产品所属分类编号
        /// </summary>
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _Name;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Picture;
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private string _Content;
        /// <summary>
        /// 产品介绍
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private int _Hits;
        /// <summary>
        /// 产品点击次数
        /// </summary>
        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 产品创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
