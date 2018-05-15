using System;
using System.Collections.Generic;
using System.Text;

using SMS.Model;
using SMS.Model.Sys;

namespace SMS.BLL.Sys
{
    public class User
    {
        private SMS.SQLServerDAL.Sys.User dal;
        public User()
        {
            dal = new SMS.SQLServerDAL.Sys.User();
        }

        public LoginState CheckLogin(UserInfo SysUser)
        {
            return dal.CheckLogin(SysUser);
        }

        public bool CheckUserNameIsExist(string UserName)
        {
            return dal.CheckUserNameIsExist(UserName);
        }

        public int Add(UserInfo SysUser)
        {
            return dal.Add(SysUser);
        }

        public int Del(string UserID)
        {
            return dal.Del(UserID);
        }

        public int ChangePassword(int UserID, string Password)
        {
            return dal.ChangePassword(UserID, Password);
        }

        public UserInfo GetByID(int UserID)
        {
            return dal.GetByID(UserID);
        }

        public IList<UserInfo> GetList(int PageIndex, int PageSize, out int RecordTotal)
        {
            return dal.GetList(PageIndex, PageSize, out RecordTotal);
        }
    }
}
