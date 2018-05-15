using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Editor
{
    /// <summary>
    /// ±à¼­Æ÷Ò³Ãæ
    /// </summary>
    public class EditorInfo
    {
        private int _ID;
        /// <summary>
        /// ±àºÅ
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _Name;
        /// <summary>
        /// Ãû³Æ
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Content;
        /// <summary>
        /// ÄÚÈÝ
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
