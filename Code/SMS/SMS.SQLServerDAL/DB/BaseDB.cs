using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SMS.Config;
//using Newtonsoft.Json;

namespace SMS.SQLServerDAL
{
    /// <summary>
    /// 数据处理层->基础公用类
    /// </summary>
    public class BaseDB
    {
        //数据库连接字符串
        protected string ConnStr = SysConfig.GetConfigValue("CoreConnectionString");

        //表前缀
        protected string Pre = SysConfig.GetConfigValue("TableNamePrefix");

        public BaseDB()
        {
        }

        /*/// <summary>
        /// 将Ilist类型转换为Json字符串
        /// </summary>
        /// <typeparam name="T">list类型</typeparam>
        /// <param name="IL">要转换的Ilist</param>
        /// <returns>Json字符串</returns>
        public string IlistToJson<T>(IList<T> IL)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            for (int i = 0; i < IL.Count; i++)
            {
                if (i != 0) Json.Append(",");
                Json.Append(JavaScriptConvert.SerializeObject(IL[i]));
            }
            Json.Append("]");
            return Json.ToString();
        }*/

        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="jsonObject">对象</param>
        /// <returns>Json字符串</returns>
        /*public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            return Json.DeleteLast(jsonString) + "}";
        }*/
        /// <summary>
        /// 对象集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        /*public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += Json.ToJson(item) + ",";
            }
            return Json.DeleteLast(jsonString) + "]";
        }*/
        /// <summary>
        /// 普通集合转换Json
        /// </summary>
        /// <param name="array">集合对象</param>
        /// <returns>Json字符串</returns>
        /*public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            return Json.DeleteLast(jsonString) + "]";
        }*/
        /// <summary>
        /// 删除结尾字符
        /// </summary>
        /// <param name="str">需要删除的字符</param>
        /// <returns>完成后的字符串</returns>
        public static string DeleteLast(string str)
        {
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataTable table)
        {
            string jsonString = "[";
            DataRowCollection drc = table.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString += "{";
                foreach (DataColumn column in table.Columns)
                {
                    jsonString += "\"" + ToJson(column.ColumnName) + "\":";
                    if (column.DataType == typeof(DateTime) || column.DataType == typeof(string))
                    {
                        jsonString += "\"" + ToJson(drc[i][column.ColumnName].ToString()) + "\",";
                    }
                    else
                    {
                        jsonString += ToJson(drc[i][column.ColumnName].ToString()) + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// DataReader转换为Json
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(SqlDataReader dataReader)
        {
            string jsonString = "[";
            while (dataReader.Read())
            {
                jsonString += "{";

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    jsonString += "\"" + ToJson(dataReader.GetName(i)) + "\":";
                    if (dataReader.GetFieldType(i) == typeof(DateTime) || dataReader.GetFieldType(i) == typeof(string))
                    {
                        jsonString += "\"" + ToJson(dataReader[i].ToString()) + "\",";
                    }
                    else if (dataReader.GetFieldType(i) == typeof(bool))
                    {
                        jsonString += ToJson((dataReader[i].ToString()).ToLower()) + ",";
                    }
                    else
                    {
                        jsonString += ToJson(dataReader[i].ToString()) + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            dataReader.Close();
            return DeleteLast(jsonString) + "]";
        }
        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dataSet">DataSet对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + ToJson(table.TableName) + "\":" + ToJson(table) + ",";
            }
            return jsonString = DeleteLast(jsonString) + "}";
        }
        /// <summary>
        /// String转换为Json
        /// </summary>
        /// <param name="value">String对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string temstr;
            temstr = value;
            temstr = temstr.Replace("{", "｛").Replace("}", "｝").Replace(":", "：").Replace(",", "，").Replace("[", "【").Replace("]", "】").Replace(";", "；").Replace("\n", "<br/>").Replace("\r", "");

            temstr = temstr.Replace("\t", "   ");
            temstr = temstr.Replace("'", "\'");
            temstr = temstr.Replace(@"\", @"\\");
            temstr = temstr.Replace("\"", "\"\"");
            return temstr;
        }







    }
}
