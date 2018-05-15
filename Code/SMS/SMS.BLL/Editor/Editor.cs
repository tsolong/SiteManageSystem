using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Editor;

namespace SMS.BLL.Editor
{
    public class Editor
    {
        private SQLServerDAL.Editor.Editor dal;
        public Editor()
        {
            dal = new SMS.SQLServerDAL.Editor.Editor();
        }

        public int Save(EditorInfo EInfo)
        {
            return dal.Save(EInfo);
        }

        public EditorInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }
    }
}
