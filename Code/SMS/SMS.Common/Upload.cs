using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace SMS.Common
{
    public class Upload
    {
        /// <summary>
        /// ����ָ��·���е�����Ŀ¼
        /// </summary>
        /// <param name="Path">·��</param>
        public static void CreateDirectory(string Path)
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// �����չ�������ExtNames���Ƿ����ExtName��
        /// </summary>
        /// <param name="ExtName">Ҫ���ĵ�����չ��</param>
        /// <param name="ExtNames">��չ������</param>
        /// <returns></returns>
        public static bool CheckExt(string ExtName, string[] ExtNames)
        {
            for (int i = 0; i < ExtNames.Length; i++)
            {
                if (ExtName.ToLower() == ExtNames[i].ToLower())
                    return true;
            }
            return false;
        }

        public static bool Picture(HttpPostedFile CurrentFile, string Path)
        {
            try
            {
                Upload.CreateDirectory(Path);
                CurrentFile.SaveAs(Path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
