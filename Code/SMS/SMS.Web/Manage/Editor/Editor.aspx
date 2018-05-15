<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="SMS.Web.Manage.Editor.Editor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        var Content = FCKeditorAPI.GetInstance("MyEditor").GetXHTML(true);
	    
	    if(Content == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写内容"
		    })
		    return false;
	    }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span><%=EInfo != null ? EInfo.Name : "" %></span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">
    
        <%if (EInfo != null)
          { %>
        <form action="?action=save&id=<%=ID %>" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label>内　　容：</label></th>
                    <td><FCKeditorV2:FCKeditor ID="MyEditor" Height="400px" Width="800px" runat="server"></FCKeditorV2:FCKeditor></td>
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
