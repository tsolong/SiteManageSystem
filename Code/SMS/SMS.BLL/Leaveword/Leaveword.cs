using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Leaveword;

namespace SMS.BLL.Leaveword
{
    public class Leaveword
    {
        private SQLServerDAL.Leaveword.Leaveword dal;
        public Leaveword()
        {
            dal = new SMS.SQLServerDAL.Leaveword.Leaveword();
        }

        public int Add(LeavewordInfo LInfo)
        {
            return dal.Add(LInfo);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int Revert(int ID, string RevertContent)
        {
            return dal.Revert(ID, RevertContent);
        }

        public int UpdateView(int ID)
        {
            return dal.UpdateView(ID);
        }

        public LeavewordInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<LeavewordInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
