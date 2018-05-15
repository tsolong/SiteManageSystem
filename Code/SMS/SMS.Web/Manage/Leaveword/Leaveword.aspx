<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leaveword.aspx.cs" Inherits="SMS.Web.Manage.Leaveword.Leaveword" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	    bindRowsEvent("leavewordList");
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的留言"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除留言将无法恢复,你确定要删除吗?",
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
        <span>留言列表</span>
        <a href="javascript:del(getSelectedValue('leavewordList'));" class="del">删除</a>
        <a href="javascript:changeSelect('leavewordList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('leavewordList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('leavewordList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="leavewordList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>姓名</th>
	                <th>邮箱</th>
	                <th>电话</th>
	                <th>状态</th>
	                <th>留言时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < LInfoList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=LInfoList[i].ID %>"></td>
	                <td><%=LInfoList[i].ID %></td>
	                <td><%=LInfoList[i].Name %></td>
	                <td><%=LInfoList[i].Email %></td>
	                <td><%=LInfoList[i].Phone %></td>
	                <td><%if (LInfoList[i].View) Response.Write("<span class=\"fontGreen\">已查看</span>"); else Response.Write("<span class=\"fontRed\">未查看</span>"); %>/<%if (LInfoList[i].Revert) Response.Write("<span class=\"fontGreen\">已回复</span>"); else Response.Write("<span class=\"fontRed\">未回复</span>"); %></td>
	                <td><%=LInfoList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
				                <li><a href="leavewordedit.aspx?id=<%=LInfoList[i].ID %>&p=<%=PageIndex %>"><div>查看/回复</div></a></li>
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
