using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Common
{
    /// <summary>
    /// ��ҳ��
    /// </summary>
    public class PageBar
    {
        private int _PageIndex;
        /// <summary>
        /// ��ǰҳ��
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set { this._PageIndex = value; }
        }

        private int _PageSize;
        /// <summary>
        /// ÿҳ��������
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { this._PageSize = value; }
        }

        private int _RecordTotal;
        /// <summary>
        /// ��������
        /// </summary>
        public int RecordTotal
        {
            get { return _RecordTotal; }
            set { this._RecordTotal = value; }
        }

        private int _PageTotal;
        /// <summary>
        /// ��ҳ��
        /// </summary>
        public int PageTotal
        {
            get { return _PageTotal; }
            set { this._PageTotal = value; }
        }

        private int _PageNumTotal;
        /// <summary>
        /// ��ʾҳ��������
        /// </summary>
        public int PageNumTotal
        {
            get { return _PageNumTotal; }
            set { this._PageNumTotal = value; }
        }

        private string _UrlName;
        /// <summary>
        /// ��ҳ��������
        /// </summary>
        public string UrlName
        {
            get { return _UrlName; }
            set { this._UrlName = value; }
        }

        private bool _Flag = true;
        /// <summary>
        /// �Ƿ�ƴ����ǰurl�еĲ�������ҳ��url��
        /// </summary>
        public bool Flag
        {
            get { return _Flag; }
            set { this._Flag = value; }
        }

        public PageBar()
        {
        }

        public PageBar(int PageIndex, int PageSize, int RecordTotal, int PageNumTotal, string UrlName)
        {
            init(PageIndex, PageSize, RecordTotal, PageNumTotal, UrlName, true);
        }

        public PageBar(int PageIndex, int PageSize, int RecordTotal, int PageNumTotal, string UrlName, bool Flag)
        {
            init(PageIndex, PageSize, RecordTotal, PageNumTotal, UrlName, Flag);
        }

        public void init(int PageIndex, int PageSize, int RecordTotal, int PageNumTotal, string UrlName, bool Flag)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.RecordTotal = RecordTotal;
            this.PageNumTotal = PageNumTotal;
            this.UrlName = UrlName;
            this.Flag = Flag;
            this.PageTotal = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.RecordTotal) / this.PageSize));
        }

        /// <summary>
        /// ���ɷ�ҳHtml����
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            string html = "";
            if (this.RecordTotal > 0 && this.PageIndex > 0 && this.PageIndex <= this.PageTotal)
            {
                html += "<div class=\"pageBar\">\r";

                html += "\t<span>�� " + this.RecordTotal + " ������</span>\r";
                html += "\t<span>ÿҳ " + this.PageSize + " ��</span>\r";
                html += "\t<span>�� " + this.PageTotal + " ҳ</span>\r";

                if (this.PageIndex == 1)
                {
                    html += "\r";
                    html += "\t<a title=\"��ҳ�Ѿ�����ҳ\" class=\"disabled\">��ҳ</a>\r";
                    html += "\t<a title=\"��ҳ�Ѿ�����ҳ\" class=\"disabled\">ǰҳ</a>\r";
                    html += GetNumHTML();
                    if (this.PageTotal > 1)
                    {
                        if (this.Flag)
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                        }
                        else
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"��ҳ\">��ҳ</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                        }
                    }
                    else
                    {
                        html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">��ҳ</a>\r";
                        html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">ĩҳ</a>\r";
                    }
                }
                else if (this.PageIndex == this.PageTotal)
                {
                    html += "\r";
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"ǰҳ\">ǰҳ</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"ǰҳ\">ǰҳ</a>\r";
                    }
                    html += GetNumHTML();
                    html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">��ҳ</a>\r";
                    html += "\t<a title=\"��ҳ�Ѿ���ĩҳ\" class=\"disabled\">ĩҳ</a>\r";
                }
                else
                {
                    html += "\r";

                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"ǰҳ\">ǰҳ</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"ǰҳ\">ǰҳ</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"��ҳ\">��ҳ</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"ĩҳ\">ĩҳ</a>\r";
                    }
                }

                html += "</div>";
            }
            return html;
        }

        /// <summary>
        /// ��������ҳ�뵼��
        /// </summary>
        /// <returns></returns>
        private string GetNumHTML()
        {
            int NumStart = Math.Max(1, this.PageIndex - Convert.ToInt32(this.PageNumTotal / 2));
            int NumEnd = Math.Min(this.PageTotal, NumStart + this.PageNumTotal - 1);
            NumStart = Math.Max(1, NumEnd - this.PageNumTotal + 1);

            string html = "\r";

            for (int i = NumStart; i <= NumEnd; i++)
            {
                if (i == this.PageIndex)
                {
                    html += "\t<a class=\"currentPage\" title=\"�����������ҳ\">" + i + "</a>\r";
                }
                else
                {
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + i + Tools.GetUrlPar(this.UrlName) + "\" title=\"��" + i + "ҳ\">" + i + "</a>\r";
                    }
                }
            }
            html += "\r";
            return html;
        }
    }
}
