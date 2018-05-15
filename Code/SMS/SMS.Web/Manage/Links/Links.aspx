<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="SMS.Web.Manage.Links.Links" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	    bindRowsEvent("linksList");
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的友情链接"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除友情链接将无法恢复,你确定要删除吗?",
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
        <span>友情链接列表</span>
        <a href="linksadd.aspx" class="add">添加</a>
        <a href="javascript:del(getSelectedValue('linksList'));" class="del">删除</a>
        <a href="javascript:changeSelect('linksList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('linksList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('linksList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="linksList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>网站名称</th>
	                <th>网站域名</th>
	                <th>链接类型</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < LInfoList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=LInfoList[i].ID %>"></td>
	                <td><%=LInfoList[i].ID %></td>
	                <td><%=LInfoList[i].SiteName %></td>
	                <td><%=LInfoList[i].SiteDomain %></td>
	                <td><%if (LInfoList[i].LogoAddress == string.Empty) Response.Write("文字链接"); else Response.Write("图片链接"); %></td>
	                <td><%=LInfoList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
				                <li><a href="linksedit.aspx?id=<%=LInfoList[i].ID %>&p=<%=PageIndex %>"><div>编辑</div></a></li>
			                    <li><a href="javascript:del(<%=LInfoList[i].ID %>);"><div>删除</div></a></li>
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
