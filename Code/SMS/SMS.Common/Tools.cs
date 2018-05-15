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
    /// ����
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// �����ַ�
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
        /// �ж��ַ����Ƿ�ȫ
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
        /// ����Ƿ���SqlΣ���ַ�
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        #region ��ȡUrl�����ָ���Ĳ���ֵ
        /// <summary>
        /// ��ȡָ��Url������ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string ParName)
        {
            if (HttpContext.Current.Request.QueryString[ParName] == null)
                return "";
            return FilterStr(HttpContext.Current.Request.QueryString[ParName].Trim());
        }

        /// <summary>
        /// ��ȡָ����Ԫ�ص�ֵ
        /// </summary>
        /// <param name="strName">��Ԫ������</param>
        /// <returns>Ԫ�ص�ֵ</returns>
        public static string GetForm(string ParName)
        {
            if (HttpContext.Current.Request.Form[ParName] == null)
                return "";
            return FilterStr(HttpContext.Current.Request.Form[ParName].Trim());
        }
        #endregion

        #region ��ȡURL�еĲ���
        /// <summary>
        /// ��ȡURL�еĲ���
        /// </summary>
        /// <returns>�ַ���</returns>
        public static string GetUrlPar()
        {
            return GetUrlPar(null);
        }

        /// <summary>
        /// ��ȡURL�еĲ���
        /// </summary>
        /// <param name="DebarParName">Ҫ�ų��Ĳ���������</param>
        /// <returns>�ַ���</returns>
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
        /// MD5�����ַ�������
        /// </summary>
        /// /// <param name="Input">�������ַ���</param>
        /// <param name="Half">������16λ����32λ�����ΪtrueΪ16λ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)//16λMD5���ܣ�ȡ32λ���ܵ�9~25�ַ���
                output = output.Substring(8, 16);
            return output;
        }

        /// <summary>
        /// MD5���ܺ���(16λ)
        /// </summary>
        /// <param name="Input">�������ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string MD5(string Input)
        {
            return MD5(Input, true);
        }

        #region ����Ƿ�Ϊ����

        /// <summary>
        /// ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="StrValue">Ҫ�����ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsInt(string StrValue)
        {
            return Regex.IsMatch(StrValue, @"^-?[1-9]\d*$");
        }

        /// <summary>
        /// ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="StrValue">Ҫ�����ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsPositiveInt(string StrValue)
        {
            return IsPositiveInt(StrValue, false);
        }

        /// <summary>
        /// ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="StrValue">Ҫ�����ַ���</param>
        /// <param name="IsInZero">�Ƿ����0</param>
        /// <returns>�жϽ��</returns>
        /// <summary>
        public static bool IsPositiveInt(string StrValue, bool IsInZero)
        {
            if (IsInZero)
                return Regex.IsMatch(StrValue, @"^[1-9]\d*|0$");
            else
                return Regex.IsMatch(StrValue, @"^[1-9]\d*$");
        }

        /// <summary>
        /// ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="StrValue">Ҫ�����ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsNegativeInt(string StrValue)
        {
            return IsNegativeInt(StrValue, false);
        }

        /// <summary>
        /// ����Ƿ�Ϊ������
        /// </summary>
        /// <param name="StrValue">Ҫ�����ַ���</param>
        /// <param name="IsInZero">�Ƿ����0</param>
        /// <returns>�жϽ��</returns>
        public static bool IsNegativeInt(string StrValue, bool IsInZero)
        {
            if (IsInZero)
                return Regex.IsMatch(StrValue, @"^-[1-9]\d*|0$");
            else
                return Regex.IsMatch(StrValue, @"^-[1-9]\d*$");
        }

        #endregion

        /// <summary>
        /// ����Ƿ����email��ʽ
        /// </summary>
        /// <param name="strEmail">Ҫ�жϵ�email�ַ���</param>
        /// <returns>�жϽ��</returns>
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
        /// �滻�س����з�Ϊhtml���з�
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
        /// �ж��ַ����Ƿ���yy-mm-dd�ַ���
        /// </summary>
        /// <param name="str">���ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// �Ƴ�Html���
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
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
        /// �Ƿ�Ϊip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// ��õ�ǰ����Url��ַ
        /// </summary>
        /// <returns>��ǰ����Url��ַ</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// ����URL�н�β���ļ���
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
        /// ��õ�ǰҳ�������
        /// </summary>
        /// <returns>��ǰҳ�������</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// �õ�����ͷ
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host.ToLower();
        }

        /// <summary>
        /// �õ���ǰ��������ͷ
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
        /// ������һ��ҳ��ĵ�ַ
        /// </summary>
        /// <returns>��һ��ҳ��ĵ�ַ</returns>
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
