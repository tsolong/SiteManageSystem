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
        /// 创建指定路径中的所有目录
        /// </summary>
        /// <param name="Path">路径</param>
        public static void CreateDirectory(string Path)
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// 检查扩展名（检查ExtNames中是否包含ExtName）
        /// </summary>
        /// <param name="ExtName">要检查的单个扩展名</param>
        /// <param name="ExtNames">扩展名集合</param>
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
