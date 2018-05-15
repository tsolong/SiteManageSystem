using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;

namespace SMS.Common
{
    /// <summary>
    /// 工具
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 过滤字符
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string FilterStr(string Str)
        {
            if (Str != null)
            {
                Str = Str.Replace("'", "");
                Str = Str.Replace("\"", "");
                Str = Str.Replace("&", "");
                Str = Str.Replace("*", "");
                Str = Str.Replace("or", "");
                Str = Str.Replace("and", "");
                Str = Str.Replace("exec", "");
                Str = Str.Replace("drop", "");
                Str = Str.Replace("insert", "");
                Str = Str.Replace("delete", "");
                Str = Str.Replace("update", "");
                Str = Str.Replace("select", "");
                Str = Str.Replace("count", "");
                Str = Str.Replace("master", "");
                Str = Str.Replace("truncate", "");
                Str = Str.Replace("dec", "");
                Str = Str.Replace("declare", "");
                Str = Str.Replace("char(", "");
                Str = Str.Replace("mid(", "");
                Str = Str.Replace("chr(", "");
            }
            return Str;
        }

        /// <summary>
        /// 判断字符串是否安全
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool IsSafeStr(string Str)
        {
            if (Str != null)
            {
                int oStrLeng = Str.Length;
                int StrLeng = Tools.FilterStr(Str).Length;
                if (oStrLeng == StrLeng)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        #region 获取Url或表单中指定的参数值
        /// <summary>
        /// 获取指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string ParName)
        {
            if (HttpContext.Current.Request.QueryString[ParName] == null)
                return "";
            return FilterStr(HttpContext.Current.Request.QueryString[ParName].Trim());
        }

        /// <summary>
        /// 获取指定表单元素的值
        /// </summary>
        /// <param name="strName">表单元素名称</param>
        /// <returns>元素的值</returns>
        public static string GetForm(string ParName)
        {
            if (HttpContext.Current.Request.Form[ParName] == null)
                return "";
            return FilterStr(HttpContext.Current.Request.Form[ParName].Trim());
        }
        #endregion

        #region 获取URL中的参数
        /// <summary>
        /// 获取URL中的参数
        /// </summary>
        /// <returns>字符串</returns>
        public static string GetUrlPar()
        {
            return GetUrlPar(null);
        }

        /// <summary>
        /// 获取URL中的参数
        /// </summary>
        /// <param name="DebarParName">要排除的参数的名称</param>
        /// <returns>字符串</returns>
        public static string GetUrlPar(string DebarParName)
        {
            string UrlPar = "";
            HttpRequest Request = System.Web.HttpContext.Current.Request;
            foreach (string Par in Request.QueryString)
            {
                if (DebarParName != null)
                {
                    if (Par != DebarParName)
                        UrlPar += "&" + Par + "=" + Request.QueryString["" + Par + ""];
                }
                else
                {
                    UrlPar += "&" + Par + "=" + Request.QueryString["" + Par + ""];
                }
            }
            return UrlPar;
        }
        #endregion

        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// /// <param name="Input">待加密字符串</param>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)//16位MD5加密（取32位加密的9~25字符）
                output = output.Substring(8, 16);
            return output;
        }

        /// <summary>
        /// MD5加密函数(16位)
        /// </summary>
        /// <param name="Input">待加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string Input)
        {
            return MD5(Input, true);
        }

        #region 检测是否为整数

        /// <summary>
        /// 检测是否为整数
        /// </summary>
        /// <param name="StrValue">要检测的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsInt(string StrValue)
        {
            return Regex.IsMatch(StrValue, @"^-?[1-9]\d*$");
        }

        /// <summary>
        /// 检测是否为正整数
        /// </summary>
        /// <param name="StrValue">要检测的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsPositiveInt(string StrValue)
        {
            return IsPositiveInt(StrValue, false);
        }

        /// <summary>
        /// 检测是否为正整数
        /// </summary>
        /// <param name="StrValue">要检测的字符串</param>
        /// <param name="IsInZero">是否包括0</param>
        /// <returns>判断结果</returns>
        /// <summary>
        public static bool IsPositiveInt(string StrValue, bool IsInZero)
        {
            if (IsInZero)
                return Regex.IsMatch(StrValue, @"^[1-9]\d*|0$");
            else
                return Regex.IsMatch(StrValue, @"^[1-9]\d*$");
        }

        /// <summary>
        /// 检测是否为负整数
        /// </summary>
        /// <param name="StrValue">要检测的字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNegativeInt(string StrValue)
        {
            return IsNegativeInt(StrValue, false);
        }

        /// <summary>
        /// 检测是否为负整数
        /// </summary>
        /// <param name="StrValue">要检测的字符串</param>
        /// <param name="IsInZero">是否包括0</param>
        /// <returns>判断结果</returns>
        public static bool IsNegativeInt(string StrValue, bool IsInZero)
        {
            if (IsInZero)
                return Regex.IsMatch(StrValue, @"^-[1-9]\d*|0$");
            else
                return Regex.IsMatch(StrValue, @"^-[1-9]\d*$");
        }

        #endregion

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !Tools.IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;

        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 返回URL中结尾的文件名
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host.ToLower();
        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            if (!HttpContext.Current.Request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString());
            }
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }
    }
}
