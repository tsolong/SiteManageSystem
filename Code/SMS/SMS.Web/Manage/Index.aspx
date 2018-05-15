<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SMS.Web.Manage.Index" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title><%=SMS.Config.SysConfig.GetConfigValue("SystemName") + " " + SMS.Config.SysConfig.GetConfigValue("SystemVersion") %></title>
    <script type="text/javascript">
    if(self!=top)
        top.location.href=location.href;
    </script>
</head>
<frameset rows="135,*,36" cols="*" frameborder="no" border="0" framespacing="0">
    <frame src="frames/header.aspx" id="headerFrame" name="headerFrame" scrolling="No" noresize="noresize" />
    <!--[if IE 6]>
    <frame src="site/siteinfo.aspx" id="mainFrame" name="mainFrame" scrolling="Yes" noresize="noresize" />
    <![endif]-->
    <!--[if gte IE 7]>
    <frame src="site/siteinfo.aspx" id="mainFrame" name="mainFrame" noresize="noresize" />
    <![endif]-->
    <!--[if !IE]><!-->
    <frame src="site/siteinfo.aspx" id="mainFrame" name="mainFrame" style="overflow:auto" noresize="noresize" />
    <!--<![endif]-->
    <frame src="frames/footer.aspx" id="footerFrame" name="footerFrame" scrolling="No" noresize="noresize" />
</frameset>
<noframes><body></body></noframes>
</html>