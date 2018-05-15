using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Products
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public class CategoryInfo
    {
        private int _ID;
        /// <summary>
        /// 分类编号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _ParentID;
        /// <summary>
        /// 父类编号
        /// </summary>
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        private string _CategoryName;
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private int _OrderID;
        /// <summary>
        /// 分类排序
        /// </summary>
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        private DateTime _CreateDate;
        /// <summary>
        /// 分类创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private int _Level = 0;
        /// <summary>
        /// 第n级分类
        /// </summary>
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
    }
}
