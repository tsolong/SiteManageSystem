using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace SMS.Config
{
    /// <summary>
    /// ϵͳ����
    /// </summary>
    public class SysConfig
    {
        public static readonly string XmlPath = HttpContext.Current.Server.MapPath("/Config/Sys.config");

        /// <summary>
        /// ��ȡ�����ļ�ָ���ڵ��б�
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        /// <returns>�ڵ��б�</returns>
        public static XmlNodeList GetConfigList(string NodeName)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlPath);
            XmlElement Root = XmlDoc.DocumentElement;
            return Root.GetElementsByTagName(NodeName);
        }

        /// <summary>
        /// ��ȡ�����ļ�ָ���ڵ��б��еĵ�һ���ڵ�ֵ
        /// </summary>
        /// <param name="NodeName">�ڵ�����</param>
        /// <returns>�ڵ�ֵ</returns>
        public static string GetConfigValue(string NodeName)
        {
            try
            {
                return GetConfigList(NodeName)[0].InnerText;
            }
            catch
            {
                return null;
            }
        }
    }
}
