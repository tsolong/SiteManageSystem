<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="SMS.Web.Manage.Products.Category" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    addEvent(window, "load", function(){
        bindRowsEvent("productsCategoryList", 'noCheck');
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的产品分类"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除产品分类同时会删除分类中所有的子类和产品,数据将无法恢复,你确定要删除吗?",
			    confirmEvent: function(){
				    location.href = "?action=del&id=" + id
			    }
		    })
	    }
    }
    </script>
</head>
<body>

<div class="panel">
    
    <div class="toolBar">
        <span>产品分类列表</span>
        <a href="categoryadd.aspx" class="add">添加</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>
    
    <div class="content">

        <%if (ProductsCategoryList.Count > 0)
          { %>
        <table id="productsCategoryList" class="listTab">
            <thead>
                <tr>
	                <th>编号</th>
	                <th>分类名称</th>
	                <th>分类排序</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%
            for (int i = 0; i < ProductsCategoryList.Count; i++)
            { %>
	            <tr>
	                <td><%=ProductsCategoryList[i].ID%></td>
	                <td><%=BPC.AddTab(ProductsCategoryList[i].Level)%><%=ProductsCategoryList[i].CategoryName%></td>
	                <td><%=ProductsCategoryList[i].OrderID%></td>
	                <td><%=ProductsCategoryList[i].CreateDate%></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
			                <%
                                if (ProductsCategoryList[i].Level < Convert.ToInt32(SMS.Config.SysConfig.GetConfigValue("ProductsCategoryLevel")))
                                {
			                 %>
			                    <li><a href="categoryadd.aspx?categoryid=<%=ProductsCategoryList[i].ID %>"><div>添加子分类</div></a></li>
			                    <%} %>
				                <li><a href="categoryedit.aspx?id=<%=ProductsCategoryList[i].ID %>"><div>编辑</div></a></li>
			                    <li><a href="javascript:del(<%=ProductsCategoryList[i].ID %>);"><div>删除</div></a></li>
			                </ul>
		                </div>
	                </td>
	            </tr>
            <%} %>
            </tbody>
        </table>
        <%
        }
        else
        { %>
        <div class="noData"></div>
        <%} %>
        
    </div>
    
</div>

</body>
</html>
