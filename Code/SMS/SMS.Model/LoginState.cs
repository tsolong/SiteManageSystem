using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Model
{
    /// <summary>
    /// ��¼״̬
    /// </summary>
    public enum LoginState : int
    {
        /// <summary>
        /// �û����������ʽ����ȷ(��:�û���������Ϊ�գ����зǷ��ַ�)
        /// </summary>
        Err_Format = 1,

        /// <summary>
        /// �û�������
        /// </summary>
        Err_NotUser = 2,

        /// <summary>
        /// �������
        /// </summary>
        Err_Password = 3,

        /// <summary>
        /// �û������������
        /// </summary>
        Err_UserNameOrPassword = 4,

        /// <summary>
        /// ��¼�ɹ�
        /// </summary>
        Succeed = 10
    }
}
