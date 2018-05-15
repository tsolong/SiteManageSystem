<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="SMS.Web.Manage.Sys.User" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	    bindRowsEvent("sysUserList");
    })
    
    function del(userId){
	    if (!userId || userId == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的管理员"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除管理员将无法恢复,你确定要删除吗?",
			    confirmEvent: function(){
				    location.href = "?action=del&userid=" + userId
			    }
		    })
	    }
    }
    </script>
</head>
<body>

<div class="panel">
    
    <div class="toolBar">
        <span>管理员列表</span>
        <a href="useradd.aspx" class="add">添加</a>
        <a href="javascript:del(getSelectedValue('sysUserList'));" class="del">删除</a>
        <a href="javascript:changeSelect('sysUserList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('sysUserList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('sysUserList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="sysUserList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>用户名</th>
	                <th>最后一次登录日期</th>
	                <th>最后一次登录IP</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < SysUserList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=SysUserList[i].UserID %>"></td>
	                <td><%=SysUserList[i].UserID %></td>
	                <td><%=SysUserList[i].UserName %></td>
	                <td><%=SysUserList[i].LastLoginDate %></td>
	                <td><%=SysUserList[i].LastLoginIP %></td>
	                <td><%=SysUserList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
				                <li><a href="userchangepassword.aspx?userid=<%=SysUserList[i].UserID %>&p=<%=PageIndex %>"><div>修改密码</div></a></li>
			                    <li><a href="javascript:del(<%=SysUserList[i].UserID %>);"><div>删除</div></a></li>
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
