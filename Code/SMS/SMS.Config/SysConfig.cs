using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace SMS.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SysConfig
    {
        public static readonly string XmlPath = HttpContext.Current.Server.MapPath("/Config/Sys.config");

        /// <summary>
        /// 获取配置文件指定节点列表
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <returns>节点列表</returns>
        public static XmlNodeList GetConfigList(string NodeName)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(XmlPath);
            XmlElement Root = XmlDoc.DocumentElement;
            return Root.GetElementsByTagName(NodeName);
        }

        /// <summary>
        /// 获取配置文件指定节点列表中的第一个节点值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <returns>节点值</returns>
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
