<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SMS.Web.Manage.Login" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>登录 - <%=SMS.Config.SysConfig.GetConfigValue("SystemName") + " " + SMS.Config.SysConfig.GetConfigValue("SystemVersion") %></title>
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-More.css" />
    <link type="text/css" rel="stylesheet" href="<%=SystemManageDir %>Style/Page.css" />
    <script type="text/javascript" src="/Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="/Common/TL/TL-More.js"></script>
    <script type="text/javascript" src="<%=SystemManageDir %>Script/Login.js"></script>
    <style type="text/css">
    html,body{height:100%;}
    </style>
</head>
<body id="loginBody">　

    <form id="loginForm" onsubmit="return checkLogin();">
        <div id="login">
            <div>
                <label for="UserName">用户名</label>
                <input type="text" id="UserName" name="UserName" class="txt"/>
            </div>
            <div>
                <label for="Password">密　码</label>
                <input type="password" id="Password" name="Password" class="txt"/>
            </div>
            <div class="last">
                <button type="submit">登　录</button>
                <button type="reset">清　空</button>
            </div>
        </div>
    </form>

</body>
</html>
