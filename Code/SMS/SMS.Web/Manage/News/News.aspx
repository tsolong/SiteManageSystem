<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="SMS.Web.Manage.News.News" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=SystemManageDir %>Style/Page.css" />
    <script type="text/javascript" src="/Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="/Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=SystemManageDir %>Script/Common.js"></script>
    <script type="text/javascript">
    addEvent(window, "load", function(){
	    bindRowsEvent("newsList");
	    var Category = $("Category");
	    addEvent(Category, "change", function(){
	        if (Category.value == "")
	            location.href = "news.aspx";
	        else
	            location.href = "?categoryid=" + Category.value;
	    })
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的新闻"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除新闻将无法恢复,你确定要删除吗?",
			    confirmEvent: function(){
				    location.href = "?action=del&id=" + id
			    }
		    })
	    }
    }
    </script>
</head>
<body>

<div class="panel">
    
    <div class="toolBar">
        <span>新闻列表</span>
        <a href="newsadd.aspx" class="add">添加</a>
        <a href="javascript:del(getSelectedValue('newsList'));" class="del">删除</a>
        <a href="javascript:changeSelect('newsList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('newsList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('newsList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
        <div>
            <select id="Category" name="Category">
                <option value="">全部新闻</option>
                <%for (int i = 0; i < NewsCategoryList.Count; i++)
                  { %>
                <option value="<%=NewsCategoryList[i].ID %>" <%if(NewsCategoryList[i].ID == CategoryID) Response.Write("selected"); %>><%=NewsCategoryList[i].CategoryName%></option>
                <%} %>
            </select>
        </div>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="newsList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>新闻标题</th>
	                <th>点击次数</th>
	                <th>新闻所属分类</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < NInfoList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=NInfoList[i].ID %>"></td>
	                <td><%=NInfoList[i].ID %></td>
	                <td><%=NInfoList[i].Title %></td>
	                <td><%=NInfoList[i].Hits %></td>
	                <td><%=BNC.GetCategoryNameByID(NInfoList[i].CategoryID) %></td>
	                <td><%=NInfoList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
				                <li><a href="newsedit.aspx?id=<%=NInfoList[i].ID %>&p=<%=PageIndex %>"><div>编辑</div></a></li>
			                    <li><a href="javascript:del(<%=NInfoList[i].ID %>);"><div>删除</div></a></li>
			                </ul>
		                </div>
	                </td>
	            </tr>
            <%} %>
            </tbody>
        </table>
        <%
            Response.Write(PageBarHtml);
        }
        else
        { %>
        <div class="noData"></div>
        <%} %>
        
    </div>
    
</div>

</body>
</html>
