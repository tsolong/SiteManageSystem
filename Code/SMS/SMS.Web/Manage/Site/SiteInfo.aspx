<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteInfo.aspx.cs" Inherits="SMS.Web.Manage.Site.SiteInfo" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        var SiteName = $("SiteName");
	    var SiteDomain = $("SiteDomain");
	    var SiteEmail = $("SiteEmail");
	    
	    if (SiteName.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站名称",
			    closeEvent : function(){
			        SiteName.focus();
			    }
		    })
		    return false;
	    }
	    else if (SiteDomain.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站域名",
			    closeEvent : function(){
			        SiteDomain.focus();
			    }
		    })
		    return false;
	    }
	    else if (SiteEmail.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写网站邮箱",
			    closeEvent : function(){
			        SiteEmail.focus();
			    }
		    })
		    return false;
	    }
		else if (!/^[\w\.-]+@[\w\.-]+\.\w+$/i.test(SiteEmail.value.toLowerCase())) {
			new win({
			    type : 4,
			    title: "系统提示",
			    msg: "网站邮箱地址无效",
			    closeEvent : function(){
			        SiteEmail.focus();
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
        <span>网站信息设置</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">

        <form action="?action=save" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label for="SiteName">网站名称：</label></th>
                    <td><input type="text" id="SiteName" name="SiteName" class="txt" value="<%=SI.SiteName %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="SiteDomain">网站域名：</label></th>
                    <td><input type="text" id="SiteDomain" name="SiteDomain" class="txt" value="<%=SI.SiteDomain %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 域名前请加上 "http://"。</p></td>
                </tr>
                <tr>
                    <th><label for="SiteEmail">网站邮箱：</label></th>
                    <td><input type="text" id="SiteEmail" name="SiteEmail" class="txt" value="<%=SI.SiteEmail %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="SiteKeywords">关 键 词：</label></th>
                    <td><textarea id="SiteKeywords" name="SiteKeywords" class="txtarea"><%=SI.SiteKeywords %></textarea></td>
                    <td class="rowMsg"></td>
                </tr>
                <tr>
                    <th><label for="SiteDescription">网页描述：</label></th>
                    <td><textarea id="SiteDescription" name="SiteDescription" class="txtarea"><%=SI.SiteDescription%></textarea></td>
                    <td class="rowMsg"></td>
                </tr>
                <tr>
                    <th><label for="SiteCopyright">版权信息：</label></th>
                    <td><textarea id="SiteCopyright" name="SiteCopyright" class="txtarea"><%=SI.SiteCopyright %></textarea></td>
                    <td class="rowMsg"></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">保 存</button></div>
            
        </form>
        
    </div>
    
</div>

</body>
</html>
