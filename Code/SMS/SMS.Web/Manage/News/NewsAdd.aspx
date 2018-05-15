<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAdd.aspx.cs" Inherits="SMS.Web.Manage.News.NewsAdd" %>
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
    function checkAdd(){
        var Category = $("Category");
	    var Title = $("Title");
	    var Content = FCKeditorAPI.GetInstance("MyEditor").GetXHTML(true);
	    
	    if(Category.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请选择新闻所属类别",
			    closeEvent : function(){
			        Category.focus();
			    }
		    })
		    return false;
	    }
	    else if (Title.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写新闻标题",
			    closeEvent : function(){
			        Title.focus();
			    }
		    })
		    return false;
	    }
	    else if (Content == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写新闻内容"
		    })
		    return false;
	    }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span>添加新闻</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">

        <form action="?action=add" method="post" onsubmit="return checkAdd();">
        
            <table class="addTab">
                <tr>
                    <th><label for="Category">所属类别：</label></th>
                    <td>
                        <select id="Category" name="Category">
                            <option value="">请选择新闻所属类别</option>
                            <%
                                for (int i = 0; i < NewsCategoryList.Count; i++)
                                {
                            %>
                                <option value="<%=NewsCategoryList[i].ID %>"><%=NewsCategoryList[i].CategoryName %></option>
                            <%
                                }
                            %>
                        </select>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Title">新闻标题：</label></th>
                    <td><input type="text" id="Title" name="Title" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label>新闻内容：</label></th>
                    <td><FCKeditorV2:FCKeditor ID="MyEditor" Height="400px" Width="800px" runat="server"></FCKeditorV2:FCKeditor></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">添 加</button></div>
            
        </form>

    </div>

</div>

</body>
</html>
