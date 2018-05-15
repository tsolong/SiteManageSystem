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
    public partial class JobsAdd : SMS.Web.UI.SysUserPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tools.GetQueryString("action").ToLower() == "add")
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
                    ShowWindow(1, "ϵͳ��ʾ", "����дְλ����", null, true);
	            }
                else if (Dept == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д��������", null, true);
	            }
                else if (Email == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д��ϵ����", null, true);
                }
                else if (!Regex.IsMatch(Email, @"^[\w\.-]+@[\w\.-]+\.\w+$"))
                {
                    ShowWindow(4, "ϵͳ��ʾ", "��ϵ�����ַ��Ч", null, true);
                }
                else if (Address == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д�����ص�", null, true);
	            }
                else if (Num == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д��Ƹ����", null, true);
	            }
                else if (Sex == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д�Ա�Ҫ��", null, true);
	            }
                else if (Experience == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д��������", null, true);
	            }
                else if (Language == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д����Ҫ��", null, true);
	            }
                else if (Salary == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����днˮ��Χ", null, true);
	            }
                else if (Education == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����дѧ��Ҫ��", null, true);
	            }
                else if (ResumeLanguage == string.Empty)
                {
	                ShowWindow(1, "ϵͳ��ʾ", "����д��������", null, true);
	            }
                else if (Content == string.Empty)
                {
                    ShowWindow(1, "ϵͳ��ʾ", "����дְλ����", null, true);
                }
                else
                {
                    JobsInfo JInfo = new JobsInfo();
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
                    if (new BLL.Jobs.Jobs().Add(JInfo) != 0)
                        ShowWindow(3, "ϵͳ��ʾ", "��Ƹ��ӳɹ�,��� \\\"ȷ��\\\" ��ť������Ƹ�б�ҳ��", "jobs.aspx", false);
                    else
                        ShowWindow(4, "ϵͳ��ʾ", "��Ƹ���ʧ��", null, true);
                }
            }
        }
    }
}
