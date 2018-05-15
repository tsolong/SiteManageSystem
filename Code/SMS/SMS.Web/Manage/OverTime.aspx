<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverTime.aspx.cs" Inherits="SMS.Web.Manage.OverTime" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title><%=SMS.Config.SysConfig.GetConfigValue("SystemName") + " " + SMS.Config.SysConfig.GetConfigValue("SystemVersion") %></title>
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-More.css" />
    <script type="text/javascript" src="/Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="/Common/TL/TL-More.js"></script>
    <script type="text/javascript">
    if (self != top)
        top.location.href = location.href;
        
    addEvent(window, "load", function(){
        new win({
            type: 4,
            title: "系统提示",
            msg: "您还未登录系统或登录已超时， <a href=\"login.aspx\">重新登录</a>",
            closeEvent: function(){
                location.href = "login.aspx";
            },
            isOverlay: false,
            dragOpacity: 1
        })
    })
    </script>
</head>
<body>　
</body>
</html>
