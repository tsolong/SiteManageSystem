<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobsEdit.aspx.cs" Inherits="SMS.Web.Manage.Jobs.JobsEdit" %>
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
        var Name = $("Name");
        var Dept = $("Dept");
        var Email = $("Email");
        var Address = $("Address");
        var Num = $("Num");
        var Sex = $("Sex");
        var Experience = $("Experience");
        var Language = $("Language");
        var Salary = $("Salary");
        var Education = $("Education");
        var ResumeLanguage = $("ResumeLanguage");
	    var Content = FCKeditorAPI.GetInstance("MyEditor").GetXHTML(true);
	    
	    if(Name.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写职位名称",
			    closeEvent : function(){
			        Name.focus();
			    }
		    })
		    return false;
	    }
	    else if(Dept.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写工作部门",
			    closeEvent : function(){
			        Dept.focus();
			    }
		    })
		    return false;
	    }
	    else if(Email.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写联系邮箱",
			    closeEvent : function(){
			        Email.focus();
			    }
		    })
		    return false;
	    }
	    else if (!/^[\w\.-]+@[\w\.-]+\.\w+$/i.test(Email.value.toLowerCase())) {
			new win({
			    type : 4,
			    title: "系统提示",
			    msg: "联系邮箱地址无效",
			    closeEvent : function(){
			        Email.focus();
			    }
		    })
		    return false;
		}
	    else if(Address.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写工作地点",
			    closeEvent : function(){
			        Address.focus();
			    }
		    })
		    return false;
	    }
	    else if(Num.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写招聘人数",
			    closeEvent : function(){
			        Num.focus();
			    }
		    })
		    return false;
	    }
	    else if(Sex.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写性别要求",
			    closeEvent : function(){
			        Sex.focus();
			    }
		    })
		    return false;
	    }
	    else if(Experience.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写工作年限",
			    closeEvent : function(){
			        Experience.focus();
			    }
		    })
		    return false;
	    }
	    else if(Language.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写语言要求",
			    closeEvent : function(){
			        Language.focus();
			    }
		    })
		    return false;
	    }
	    else if(Salary.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写薪水范围",
			    closeEvent : function(){
			        Salary.focus();
			    }
		    })
		    return false;
	    }
	    else if(Education.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写学历要求",
			    closeEvent : function(){
			        Education.focus();
			    }
		    })
		    return false;
	    }
	    else if(ResumeLanguage.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写简历语言",
			    closeEvent : function(){
			        ResumeLanguage.focus();
			    }
		    })
		    return false;
	    }
	    else if (Content == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写职位描述"
		    })
		    return false;
	    }
    }
    </script>
</head>
<body>

<div class="panel">

    <div class="toolBar">
        <span>编辑招聘</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">
    
        <%if (JInfo != null)
          { %>
        <form action="?action=save&id=<%=ID %>&p=<%=SMS.Common.Tools.GetQueryString("p") %>" method="post" onsubmit="return checkEdit();">
        
            <table class="editTab">
                <tr>
					<td><label for="Name">职位名称：</label></td>
					<td><input type="text" name="Name" id="Name" class="txt" value="<%=JInfo.Name %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Dept">工作部门：</label></td>
					<td><input type="text" name="Dept" id="Dept" class="txt" value="<%=JInfo.Dept %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Email">联系邮箱：</label></td>
					<td><input type="text" name="Email" id="Email" class="txt" value="<%=JInfo.Email %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Address">工作地点：</label></td>
					<td><input type="text" name="Address" id="Address" class="txt" value="<%=JInfo.Address %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Num">招聘人数：</label></td>
					<td><input type="text" name="Num" id="Num" class="txt" value="<%=JInfo.Num %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Sex">性别要求：</label></td>
					<td><input type="text" name="Sex" id="Sex" class="txt" value="<%=JInfo.Sex %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Experience">工作年限：</label></td>
					<td><input type="text" name="Experience" id="Experience" class="txt" value="<%=JInfo.Experience %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Language">语言要求：</label></td>
					<td><input type="text" name="Language" id="Language" class="txt" value="<%=JInfo.Language %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Salary">薪水范围：</label></td>
					<td><input type="text" name="Salary" id="Salary" class="txt" value="<%=JInfo.Salary %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="Education">学历要求：</label></td>
					<td><input type="text" name="Education" id="Education" class="txt" value="<%=JInfo.Education %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
				<tr>
					<td><label for="ResumeLanguage">简历语言：</label></td>
					<td><input type="text" name="ResumeLanguage" id="ResumeLanguage" class="txt" value="<%=JInfo.ResumeLanguage %>" /></td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
				</tr>
                <tr>
                    <th><label>职位描述：</label></th>
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
