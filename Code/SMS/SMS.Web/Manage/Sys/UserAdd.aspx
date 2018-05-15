<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="SMS.Web.Manage.Sys.UserAdd" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    function checkAdd(){
        var UserName = $("UserName");
	    var Password = $("Password");
	    var ConfirmPassword = $("ConfirmPassword");
	    
	    if(UserName.value.toLowerCase() == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写用户名",
			    closeEvent : function(){
			        UserName.focus();
			    }
		    })
		    return false;
	    }
	    else if (!/^[a-zA-Z0-9_]{4,16}$/i.test(UserName.value.toLowerCase())) {
	         new win({
	            type : 4,
			    title: "系统提示",
			    msg: "用户名格式错误",
			    closeEvent : function(){
			        UserName.focus();
			    }
		    })
		    return false;
	    }
	    else if (Password.value.toLowerCase() == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写密码",
			    closeEvent : function(){
			        Password.focus();
			    }
		    })
		    return false;
	    }
	    else if (!/^[a-zA-Z0-9_]{4,16}$/i.test(Password.value.toLowerCase())) {
	         new win({
	            type : 4,
			    title: "系统提示",
			    msg: "密码格式错误",
			    closeEvent : function(){
			        Password.focus();
			    }
		    })
		    return false;
	    }
	    else if (Password.value.toLowerCase() != ConfirmPassword.value.toLowerCase()) {
		    new win({
	            type : 4,
			    title: "系统提示",
			    msg: "两次密码填写不一致",
			    closeEvent : function(){
			        ConfirmPassword.focus();
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
        <span>添加管理员</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">

        <form action="?action=add" method="post" onsubmit="return checkAdd();">
        
            <table class="addTab">
                <tr>
                    <th><label for="UserName">用 户 名：</label></th>
                    <td><input type="text" id="UserName" name="UserName" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 用户名只能由英文字母a～z(不区分大小写)、数字0～9、下划线组成，长度为4-16个字符。</p></td>
                </tr>
                <tr>
                    <th><label for="Password">密　　码：</label></th>
                    <td><input type="password" id="Password" name="Password" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 密码只能由英文字母a～z(不区分大小写)、数字0～9、下划线组成，长度为4-16个字符。</p></td>
                </tr>
                <tr>
                    <th><label for="ConfirmPassword">确认密码：</label></th>
                    <td><input type="password" id="ConfirmPassword" name="ConfirmPassword" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 请再填写一次密码。</p></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">添 加</button></div>
            
        </form>

    </div>

</div>

</body>
</html>
