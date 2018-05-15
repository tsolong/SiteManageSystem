<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinksEdit.aspx.cs" Inherits="SMS.Web.Manage.Links.LinksEdit" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        var LinkType = $("LinkType");
        var SiteName = $("SiteName");
        var SiteDomain = $("SiteDomain");
        var LogoAddress = $("LogoAddress");
        var LogoWidth = $("LogoWidth");
        var LogoHeight = $("LogoHeight");
	    
        if(SiteName.value == "") {
            new win({
		        title: "系统提示",
		        msg: "请填写网站名称",
		        closeEvent : function(){
		            SiteName.focus();
		        }
	        })
	        return false;
        }
        else if(SiteDomain.value == "") {
            new win({
		        title: "系统提示",
		        msg: "请填写网站域名",
		        closeEvent : function(){
		            SiteDomain.focus();
		        }
	        })
	        return false;
        }
	    
	    if(LinkType.value=="2"){
	        if(LogoAddress.value == "") {
	            new win({
			        title: "系统提示",
			        msg: "请填写Logo地址",
			        closeEvent : function(){
			            LogoAddress.focus();
			        }
		        })
		        return false;
	        }
	        else if(LogoWidth.value == "") {
	            new win({
			        title: "系统提示",
			        msg: "请填写Logo宽度",
			        closeEvent : function(){
			            LogoWidth.focus();
			        }
		        })
		        return false;
	        }
	        else if(LogoHeight.value == "") {
	            new win({
			        title: "系统提示",
			        msg: "请填写Logo高度",
			        closeEvent : function(){
			            LogoHeight.focus();
			        }
		        })
		        return false;
	        }
	    }
    }
    
    function changeLinkType(type){
        if(type == "1"){
            setStyle($("LinkOption"), {
			    display: "none"
		    });
        }
        else if(type == "2"){
            setStyle($("LinkOption"), {
			    display: "block"
		    });
        }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span>编辑友情链接</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">
    
        <%if (LInfo != null)
          { %>
        <form action="?action=save&id=<%=ID %>&p=<%=SMS.Common.Tools.GetQueryString("p") %>" method="post" onsubmit="return checkEdit();">
        
            <table class="addTab">
				<tr>
					<td><label for="Name">链接类型：</label></td>
					<td>
					    <select name="LinkType" id="LinkType" onchange="changeLinkType(this.value)">
					        <option value="1" <%if (LInfo.LogoAddress == string.Empty) Response.Write("selected"); %>>文字链接</option>
					        <option value="2" <%if (LInfo.LogoAddress != string.Empty) Response.Write("selected"); %>>图片链接</option>
					    </select>
					</td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
                <tr>
					<td><label for="SiteName">网站名称：</label></td>
					<td><input type="text" name="SiteName" id="SiteName" class="txt" value="<%=LInfo.SiteName %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
                <tr>
					<td><label for="SiteDomain">网站域名：</label></td>
					<td><input type="text" name="SiteDomain" id="SiteDomain" class="txt" value="<%=LInfo.SiteDomain %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
            </table>
            
            <table class="addTab" id="LinkOption" style="margin:10px 0 0 0; <%if (LInfo.LogoAddress == string.Empty) Response.Write("display:none;"); %>">
				<tr>
					<td><label for="LogoAddress">Logo地址：</label></td>
					<td><input type="text" name="LogoAddress" id="LogoAddress" class="txt" value="<%=LInfo.LogoAddress %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="LogoWidth">Logo宽度：</label></td>
					<td><input type="text" name="LogoWidth" id="LogoWidth" class="txt" value="<%=LInfo.LogoWidth %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="LogoHeight">Logo高度：</label></td>
					<td><input type="text" name="LogoHeight" id="LogoHeight" class="txt" value="<%=LInfo.LogoHeight %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
            </table>
            
            <div class="formBtn"><button type="submit">保 存</button></div>
            
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
