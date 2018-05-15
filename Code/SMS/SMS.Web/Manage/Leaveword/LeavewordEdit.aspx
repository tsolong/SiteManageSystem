<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeavewordEdit.aspx.cs" Inherits="SMS.Web.Manage.Leaveword.LeavewordEdit" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    function checkEdit(){
        var RevertContent = $("RevertContent");
	    
	    if(RevertContent.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写回复内容",
			    closeEvent : function(){
			        RevertContent.focus();
			    }
		    })
		    return false;
	    }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span>查看/回复 留言</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">
    
        <%if (LInfo != null)
          { %>
        <form action="?action=save&id=<%=ID %>&p=<%=SMS.Common.Tools.GetQueryString("p") %>" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label>姓　　名：</label></th>
                    <td><%=LInfo.Name %></td>
                </tr>
                <tr>
                    <th><label>邮　　箱：</label></th>
                    <td><%=LInfo.Email %></td>
                </tr>
                <tr>
                    <th><label>电　　话：</label></th>
                    <td><%=LInfo.Phone %></td>
                </tr>
                <tr>
                    <th><label>留言内容：</label></th>
                    <td><%=LInfo.Content %></td>
                </tr>
                <tr>
                    <th><label for="RevertContent">回　　复：</label></th>
                    <td><textarea id="RevertContent" name="RevertContent" class="txtarea"><%=LInfo.RevertContent%></textarea></td>
                </tr>
                <tr>
                    <th><label>留言时间：</label></th>
                    <td><%=LInfo.CreateDate %></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">回 复</button></div>
            
        </form>
        <%}
          else
          { %>
          <div class="dataNotExist"></div>
        <%} %>
        
    </div>
    
</div>

</body>
</html>
