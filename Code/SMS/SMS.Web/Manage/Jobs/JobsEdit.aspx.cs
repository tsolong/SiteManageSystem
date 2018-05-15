using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using SMS.Common;
using SMS.Model.Jobs;

namespace SMS.Web.Manage.Jobs
{
    public partial class JobsEdit : SMS.Web.UI.SysUserPage
    {
        public int ID;
        public JobsInfo JInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(Tools.GetQueryString("id"));
            }
            catch
            {
                ShowWindow(4, "系统提示", "参数类型不正确", null, true);
            }

            JInfo = new BLL.Jobs.Jobs().GetByID(ID);
            if (JInfo != null)
                MyEditor.Value = JInfo.Content;

            if (Tools.GetQueryString("action").ToLower() == "save" && JInfo != null)
            {
                string Name = Tools.GetForm("Name");
                string Dept = Tools.GetForm("Dept");
                string Email = Tools.GetForm("Email");
                string Address = Tools.GetForm("Address");
                string Num = Tools.GetForm("Num");
                string Sex = Tools.GetForm("Sex");
                string Experience = Tools.GetForm("Experience");
                string Language = Tools.GetForm("Language");
                string Salary = Tools.GetForm("Salary");
                string Education = Tools.GetForm("Education");
                string ResumeLanguage = Tools.GetForm("ResumeLanguage");
                string Content = HttpUtility.HtmlDecode(Request.Form["MyEditor"]);

                if (Name == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写职位名称", null, true);
                }
                else if (Dept == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写工作部门", null, true);
                }
                else if (Email == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写联系邮箱", null, true);
                }
                else if (!Regex.IsMatch(Email, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                {
                    ShowWindow(4, "系统提示", "联系邮箱地址无效", null, true);
                }
                else if (Address == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写工作地点", null, true);
                }
                else if (Num == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写招聘人数", null, true);
                }
                else if (Sex == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写性别要求", null, true);
                }
                else if (Experience == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写工作年限", null, true);
                }
                else if (Language == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写语言要求", null, true);
                }
                else if (Salary == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写薪水范围", null, true);
                }
                else if (Education == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写学历要求", null, true);
                }
                else if (ResumeLanguage == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写简历语言", null, true);
                }
                else if (Content == string.Empty)
                {
                    ShowWindow(1, "系统提示", "请填写职位描述", null, true);
                }
                else
                {
                    JInfo.Name = Name;
                    JInfo.Dept = Dept;
                    JInfo.Email = Email;
                    JInfo.Address = Address;
                    JInfo.Num = Num;
                    JInfo.Sex = Sex;
                    JInfo.Experience = Experience;
                    JInfo.Language = Language;
                    JInfo.Salary = Salary;
                    JInfo.Education = Education;
                    JInfo.ResumeLanguage = ResumeLanguage;
                    JInfo.Content = Content;
                    if (new BLL.Jobs.Jobs().Save(JInfo) != 0)
                        ShowWindow(3, "系统提示", "招聘保存成功,点击 \\\"确定\\\" 按钮返回", "jobs.aspx?p=" + Tools.GetQueryString("p"), false);
                    else
                        ShowWindow(4, "系统提示", "招聘保存失败", null, true);
                }
            }
        }
    }
}
