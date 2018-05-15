using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model.Jobs;

namespace SMS.BLL.Jobs
{
    public class Jobs
    {
        private SQLServerDAL.Jobs.Jobs dal;
        public Jobs()
        {
            dal = new SMS.SQLServerDAL.Jobs.Jobs();
        }

        public int Add(JobsInfo JInfo)
        {
            return dal.Add(JInfo);
        }

        public int Del(string ID)
        {
            return dal.Del(ID);
        }

        public int Save(JobsInfo JInfo)
        {
            return dal.Save(JInfo);
        }

        public int UpdateHits(int ID)
        {
            return dal.UpdateHits(ID);
        }

        public int Start(int ID)
        {
            return dal.Start(ID);
        }

        public int Stop(int ID)
        {
            return dal.Stop(ID);
        }

        public JobsInfo GetByID(int ID)
        {
            return dal.GetByID(ID);
        }

        public IList<JobsInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
