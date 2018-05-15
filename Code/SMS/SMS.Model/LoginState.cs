using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum LoginState : int
    {
        /// <summary>
        /// 用户名或密码格式不正确(如:用户名或密码为空，含有非法字符)
        /// </summary>
        Err_Format = 1,

        /// <summary>
        /// 用户不存在
        /// </summary>
        Err_NotUser = 2,

        /// <summary>
        /// 密码错误
        /// </summary>
        Err_Password = 3,

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        Err_UserNameOrPassword = 4,

        /// <summary>
        /// 登录成功
        /// </summary>
        Succeed = 10
    }
}
