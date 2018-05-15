<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="SMS.Web.Manage.Frames.Header" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="/Common/TL/TL-Base.css" />
    <link type="text/css" rel="stylesheet" href="<%=SystemManageDir %>Style/Page.css" />
    <script type="text/javascript" src="/Common/TL/TL-Core.js"></script>
    <script type="text/javascript" src="<%=SystemManageDir %>Script/Common.js"></script>
    <script type="text/javascript">
    addEvent(window, "load", function(){
        var a = $T("subNav", "a");
        for (var i = 0; i < a.length; i++) {
            setAttr(a[i], "target", "mainFrame");
        }
        
        var navLiItems = $T("nav", "li");
        for (var i = 0; i < navLiItems.length; i++) {
            navLiItems[i].onclick = function(){
                clickNav(this);
            }
        }
        
        function clickNav(clickObj){
            for (var i = 0; i < navLiItems.length; i++) {
                removeAttr(navLiItems[i], "class");
                if (navLiItems[i] == clickObj) {
                    setAttr(navLiItems[i], "class", "click");
                    showSubNav(i);
                }
            }
        }
        
        var subNavLiItems = $T("subNav", "li");
        for (var i = 0; i < subNavLiItems.length; i++) {
            subNavLiItems[i].onclick = function(){
                clickSubNav(this);
            }
        }
        
        function clickSubNav(clickObj){
            for (var i = 0; i < subNavLiItems.length; i++) {
                removeAttr(subNavLiItems[i], "class");
                if (subNavLiItems[i] == clickObj) {
                    setAttr(subNavLiItems[i], "class", "click");
                }
            }
        }
        
        var subNavItems = $T("subNav", "ul");
        function showSubNav(subNavIndex){
            for (var i = 0; i < subNavItems.length; i++) {
                subNavItems[i].style.display = "none";
                if (i == subNavIndex) {
                    subNavItems[i].style.display = "block";
                }
            }
        }
        
        if (navLiItems.length > 0) 
            clickNav(navLiItems[0])
        
        if (subNavLiItems.length > 0)
            clickSubNav(subNavLiItems[0])
    })

    </script>
</head>
<body>

    <div id="header">
    
        <div class="logo"><%=SMS.Config.SysConfig.GetConfigValue("SystemName") + " " + SMS.Config.SysConfig.GetConfigValue("SystemVersion") %></div>
    
        <div class="loginUserInfo">
            您好管理员，<b><%=Sys_User.UserName %></b>
            上次登录信息[
            <%
                if (Sys_User.LastLoginDate != null)
                {
                    Response.Write(Sys_User.LastLoginDate);
                }
                if (Sys_User.LastLoginIP != "")
                { 
                    Response.Write(" / "+Sys_User.LastLoginIP);
                }
            %>
            ]
            [ <a href="../loginout.aspx" target="_top" title="退出">退出</a> ]
        </div>
        
        <ul id="nav">
            <li><a href="javascript:aGoTo();">网站设置</a></li>
            <li><a href="javascript:aGoTo();">新闻管理</a></li>
            <li><a href="javascript:aGoTo();">产品管理</a></li>
            <li><a href="javascript:aGoTo();">招聘管理</a></li>
            <li><a href="javascript:aGoTo();">留言管理</a></li>
            <li><a href="javascript:aGoTo();">友情链接</a></li>
            <li><a href="javascript:aGoTo();">页面管理</a></li>
            <li><a href="javascript:aGoTo();">系统管理</a></li>
        </ul>
        
        <div id="subNav">
            
            <ul>
                <li><a href="../site/siteinfo.aspx">网站信息设置</a></li>
            </ul>
            
            <ul>
                <li><a href="../news/news.aspx">新闻列表</a></li>
                <li><a href="../news/category.aspx">新闻分类列表</a></li>
            </ul>
            
            <ul>
                <li><a href="../products/products.aspx">产品列表</a></li>
                <li><a href="../products/category.aspx">产品分类列表</a></li>
            </ul>
        
            <ul>
                <li><a href="../jobs/jobs.aspx">招聘列表</a></li>
            </ul>
        
            <ul>
                <li><a href="../leaveword/leaveword.aspx">留言列表</a></li>
            </ul>
        
            <ul>
                <li><a href="../links/links.aspx">友情链接</a></li>
            </ul>
        
            <ul>
                <li><a href="../editor/editor.aspx?id=1">关于我们</a></li>
                <li><a href="../editor/editor.aspx?id=2">联系我们</a></li>
            </ul>
        
            <ul>
                <li><a href="../sys/user.aspx">管理员列表</a></li>
                <li><a href="../loginout.aspx">退出系统</a></li>
            </ul>
            
        </div>
        
    </div>
    
</body>
</html>
