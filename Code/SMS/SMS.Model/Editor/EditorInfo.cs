using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model.Editor
{
    /// <summary>
    /// �༭��ҳ��
    /// </summary>
    public class EditorInfo
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
        /// ����
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Content;
        /// <summary>
        /// ����
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
