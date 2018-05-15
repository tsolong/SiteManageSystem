using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.Common
{
    /// <summary>
    /// 分页条
    /// </summary>
    public class PageBar
    {
        private int _PageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set { this._PageIndex = value; }
        }

        private int _PageSize;
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { this._PageSize = value; }
        }

        private int _RecordTotal;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int RecordTotal
        {
            get { return _RecordTotal; }
            set { this._RecordTotal = value; }
        }

        private int _PageTotal;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal
        {
            get { return _PageTotal; }
            set { this._PageTotal = value; }
        }

        private int _PageNumTotal;
        /// <summary>
        /// 显示页码总数量
        /// </summary>
        public int PageNumTotal
        {
            get { return _PageNumTotal; }
            set { this._PageNumTotal = value; }
        }

        private string _UrlName;
        /// <summary>
        /// 分页参数名称
        /// </summary>
        public string UrlName
        {
            get { return _UrlName; }
            set { this._UrlName = value; }
        }

        private bool _Flag = true;
        /// <summary>
        /// 是否拼连当前url中的参数到分页的url中
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
        /// 生成分页Html代码
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            string html = "";
            if (this.RecordTotal > 0 && this.PageIndex > 0 && this.PageIndex <= this.PageTotal)
            {
                html += "<div class=\"pageBar\">\r";

                html += "\t<span>共 " + this.RecordTotal + " 条数据</span>\r";
                html += "\t<span>每页 " + this.PageSize + " 条</span>\r";
                html += "\t<span>共 " + this.PageTotal + " 页</span>\r";

                if (this.PageIndex == 1)
                {
                    html += "\r";
                    html += "\t<a title=\"本页已经是首页\" class=\"disabled\">首页</a>\r";
                    html += "\t<a title=\"本页已经是首页\" class=\"disabled\">前页</a>\r";
                    html += GetNumHTML();
                    if (this.PageTotal > 1)
                    {
                        if (this.Flag)
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"后页\">后页</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"末页\">末页</a>\r";
                        }
                        else
                        {
                            html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"后页\">后页</a>\r";
                            html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"末页\">末页</a>\r";
                        }
                    }
                    else
                    {
                        html += "\t<a title=\"本页已经是末页\" class=\"disabled\">后页</a>\r";
                        html += "\t<a title=\"本页已经是末页\" class=\"disabled\">末页</a>\r";
                    }
                }
                else if (this.PageIndex == this.PageTotal)
                {
                    html += "\r";
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"前页\">前页</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"前页\">前页</a>\r";
                    }
                    html += GetNumHTML();
                    html += "\t<a title=\"本页已经是末页\" class=\"disabled\">后页</a>\r";
                    html += "\t<a title=\"本页已经是末页\" class=\"disabled\">末页</a>\r";
                }
                else
                {
                    html += "\r";

                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + Tools.GetUrlPar(this.UrlName) + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"前页\">前页</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + Tools.GetUrlPar(this.UrlName) + "\" title=\"后页\">后页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + Tools.GetUrlPar(this.UrlName) + "\" title=\"末页\">末页</a>\r";
                    }
                    else
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + 1 + "\" title=\"首页\">首页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex - 1) + "\" title=\"前页\">前页</a>\r";
                        html += GetNumHTML();
                        html += "\t<a href=\"?" + this.UrlName + "=" + (this.PageIndex + 1) + "\" title=\"后页\">后页</a>\r";
                        html += "\t<a href=\"?" + this.UrlName + "=" + this.PageTotal + "\" title=\"末页\">末页</a>\r";
                    }
                }

                html += "</div>";
            }
            return html;
        }

        /// <summary>
        /// 生成数字页码导航
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
                    html += "\t<a class=\"currentPage\" title=\"你正在浏览本页\">" + i + "</a>\r";
                }
                else
                {
                    if (this.Flag)
                    {
                        html += "\t<a href=\"?" + this.UrlName + "=" + i + Tools.GetUrlPar(this.UrlName) + "\" title=\"第" + i + "页\">" + i + "</a>\r";
                    }
                }
            }
            html += "\r";
            return html;
        }
    }
}
