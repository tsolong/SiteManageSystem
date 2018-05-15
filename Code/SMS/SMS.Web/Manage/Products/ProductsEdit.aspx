<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsEdit.aspx.cs" Inherits="SMS.Web.Manage.Products.ProductsEdit" %>
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
    <script type="text/javascript" src="<%=SystemManageDir %>Script/UploadControl.js"></script>
    <script type="text/javascript">
    function checkEdit(){
        var Category = $("Category");
	    var Name = $("Name");
	    var Picture = $("Picture");
	    var Content = FCKeditorAPI.GetInstance("MyEditor").GetXHTML(true);
	    
	    if(Category.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请选择产品所属类别",
			    closeEvent : function(){
			        Category.focus();
			    }
		    })
		    return false;
	    }
	    else if (Name.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写产品名称",
			    closeEvent : function(){
			        Name.focus();
			    }
		    })
		    return false;
	    }
	    else if (Picture.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请上传产品图片",
			    closeEvent : function(){
			        showUploadControl();
			    }
		    })
		    return false;
	    }
	    else if (Content == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写产品介绍"
		    })
		    return false;
	    }
    }
    
    var winUploadControl;
    function showUploadControl(){
	    winUploadControl = new win({
		    type: 6,
		    height: 200,
		    title: "上传",
		    html: ""
	    })
    	
	    setStyle(winUploadControl.win.middle, {
		    padding: "10px"
	    });
    	
	    new UploadControl({
		    container: winUploadControl.win.middle,
		    url: "?action=upload&id=<%=ID %>",
		    extNames: ["gif", "jpeg", "jpg", "png"],
		    fileCount: 5,
		    onSubmit: function(){
			    loading({
				    content: "文件正在上传中，请不要关闭浏览器 ..."
			    })
		    }
	    })
    }
    
    function uploadEnd(result){
	    loading.close();
	    var result = JSON.parse(result);
	    if (result.type) {
	        $("Picture").value = result.picture;
		    $("preview").innerHTML = "<img src=\"<%=UploadDir + TempUploadFolder + "/" %>" + result.picture.replace("o", "100x100") + "\">";
		    winUploadControl.close();
	    }
	    new win({
		    type: result.type ? 3 : 4,
		    title: "系统提示",
		    msg: result.msg
	    })
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span>编辑产品</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">
    
        <%if (PInfo != null)
          { %>
        <div class="pageMsg msgHint">
            <ul>
                <div>提示：</div>
                <li>产品图片只能在 <%=SMS.Config.SysConfig.GetConfigValue("ProductsPictureSize") %> kb 之间</li>
                <li>只能上传 <%=SMS.Config.SysConfig.GetConfigValue("PictureExt")%> 格式的文件</li>
            </ul>
        </div>
        
        <form action="?action=save&id=<%=ID %>&p=<%=SMS.Common.Tools.GetQueryString("p") %>" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
                    <th><label for="Category">所属类别：</label></th>
                    <td>
                        <select id="Category" name="Category">
                            <option value="">请选择产品所属类别</option>
                            <%for (int i = 0; i < ProductsCategoryList.Count; i++)
                              { %>
                            <option value="<%=ProductsCategoryList[i].ID %>" <%if(ProductsCategoryList[i].ID == PInfo.CategoryID) Response.Write("selected"); %>><%=BPC.AddTab(ProductsCategoryList[i].Level) + ProductsCategoryList[i].CategoryName%></option>
                            <%} %>
                        </select>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Name">产品名称：</label></th>
                    <td><input type="text" id="Name" name="Name" class="txt" value="<%=PInfo.Name %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Picture">产品图片：</label></th>
                    <td><input type="hidden" id="Picture" name="Picture" class="txt" value="<%=PInfo.Picture %>" /><div id="preview"><img src="<%=UploadDir + ProductsPictureFolder + "/" + PInfo.Picture.Replace("o","100x100") %>"></div><button type="button" onclick="showUploadControl();">上 传</button></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="Content">产品介绍：</label></th>
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
