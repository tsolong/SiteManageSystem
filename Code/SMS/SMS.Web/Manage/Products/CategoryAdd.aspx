<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryAdd.aspx.cs" Inherits="SMS.Web.Manage.Products.CategoryAdd" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        var CategoryName = $("CategoryName");
	    var OrderID = $("OrderID");
	    
	    if(Category.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请选择所属类别",
			    closeEvent : function(){
			        Category.focus();
			    }
		    })
		    return false;
	    }
	    else if(CategoryName.value == "") {
	        new win({
			    title: "系统提示",
			    msg: "请填写分类名称",
			    closeEvent : function(){
			        CategoryName.focus();
			    }
		    })
		    return false;
	    }
	    else if (CategoryName.value.length < 1 || CategoryName.value.length > 10) {
	        new win({
		        type: 4,
		        title: "系统提示",
		        msg: "分类名称长度错误",
		        closeEvent: function(){
			        CategoryName.focus();
		        }
	        })
	        return false;
        }
	    else if (OrderID.value == "") {
		    new win({
			    title: "系统提示",
			    msg: "请填写分类排序",
			    closeEvent : function(){
			        OrderID.focus();
			    }
		    })
		    return false;
	    }
	    else if (!/^[1-9]\d*$/i.test(OrderID.value)) {
	         new win({
	            type : 4,
			    title: "系统提示",
			    msg: "分类排序只能是正整数",
			    closeEvent : function(){
			        OrderID.focus();
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
        <span>添加产品分类</span>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>

    <div class="content">

        <form action="?action=add" method="post" onsubmit="return checkAdd();">
        
            <table class="addTab">
                <tr>
                    <th><label for="Category">所属类别：</label></th>
                    <td>
                        <select id="Category" name="Category">
                            <option value="0">作为顶级分类</option>
                            <%for (int i = 0; i < ProductsCategoryList.Count; i++)
                              {
                                  if (ProductsCategoryList[i].Level >= Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsCategoryLevel")))
                                      continue;
                                   %>
                            <option value="<%=ProductsCategoryList[i].ID %>" <%if(ProductsCategoryList[i].ID == CategoryID) Response.Write("selected"); %>><%=BPC.AddTab(ProductsCategoryList[i].Level) + ProductsCategoryList[i].CategoryName%></option>
                            <%} %>
                        </select>
                    </td>
                    <td class="rowMsg"><p><span class="required">*</span></p></td>
                </tr>
                <tr>
                    <th><label for="CategoryName">分类名称：</label></th>
                    <td><input type="text" id="CategoryName" name="CategoryName" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 分类名称最大长度是10个字。</p></td>
                </tr>
                <tr>
                    <th><label for="OrderID">分类排序：</label></th>
                    <td><input type="text" id="OrderID" name="OrderID" class="txt" /></td>
                    <td class="rowMsg"><p><span class="required">*</span> 分类排序只能是正整数，分类的顺序由此值决定。</p></td>
                </tr>
            </table>
            
            <div class="formBtn"><button type="submit">添 加</button></div>
            
        </form>

    </div>

</div>

</body>
</html>
