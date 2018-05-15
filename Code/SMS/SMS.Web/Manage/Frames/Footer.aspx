<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Footer.aspx.cs" Inherits="SMS.Web.Manage.Frames.Footer" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        window.setInterval(function(){
		    var today = new Date();
		    var todayInfo = "";
		    if (TL.browser.msie) 
			    todayInfo += today.getYear() + "年";
		    else 
			    todayInfo += today.getFullYear() + "年";
		    todayInfo += today.getMonth() + 1 + "月";
		    todayInfo += today.getDate() + "日 ";
		    todayInfo += today.getHours() + ":";
		    todayInfo += today.getMinutes() + ":";
		    todayInfo += today.getSeconds() + " ";
		    
		    switch (today.getDay()) {
			    case 0:
				    todayInfo += "星期日";
				    break;
			    case 1:
				    todayInfo += "星期一";
				    break;
			    case 2:
				    todayInfo += "星期二";
				    break;
			    case 3:
				    todayInfo += "星期三";
				    break;
			    case 4:
				    todayInfo += "星期四";
				    break;
			    case 5:
				    todayInfo += "星期五";
				    break;
			    case 6:
				    todayInfo += "星期六 ";
				    break;
		    }
		    
		    $("todayInfo").innerHTML = todayInfo;
	    }, 1000)
    })
    </script>
</head>

<body>

    <div id="footer">Copyright &copy; <%=System.DateTime.Now.Year.ToString() %> <%=SMS.Config.SysConfig.GetConfigValue("SystemName") + " " + SMS.Config.SysConfig.GetConfigValue("SystemVersion") %> 版权所有 技术支持 <a href="<%=SMS.Config.SysConfig.GetConfigValue("SuprrotDomain") %>" title="<%=SMS.Config.SysConfig.GetConfigValue("SuprrotCompany") %>" target="_blank"><%=SMS.Config.SysConfig.GetConfigValue("SuprrotCompany") %></a></div>
    <div id="todayInfo"></div>
    
</body>
</html>
