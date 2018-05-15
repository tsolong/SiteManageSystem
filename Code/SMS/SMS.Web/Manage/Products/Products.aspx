<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="SMS.Web.Manage.Products.Products" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	    bindRowsEvent("productsList");
	    var Category = $("Category");
	    addEvent(Category, "change", function(){
	        if (Category.value == "")
	            location.href = "products.aspx";
	        else
	            location.href = "?categoryid=" + Category.value;
	    })
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的产品"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除产品将无法恢复,你确定要删除吗?",
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
        <span>产品列表</span>
        <a href="productsadd.aspx" class="add">添加</a>
        <a href="javascript:del(getSelectedValue('productsList'));" class="del">删除</a>
        <a href="javascript:changeSelect('productsList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('productsList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('productsList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
        <div>
            <select id="Category" name="Category">
                <option value="">全部产品</option>
                <%for (int i = 0; i < ProductsCategoryList.Count; i++)
                  { %>
                <option value="<%=ProductsCategoryList[i].ID %>" <%if(ProductsCategoryList[i].ID == CategoryID) Response.Write("selected"); %>><%=BPC.AddTab(ProductsCategoryList[i].Level) + ProductsCategoryList[i].CategoryName%></option>
                <%} %>
            </select>
        </div>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="productsList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>产品名称</th>
	                <th>点击次数</th>
	                <th>产品所属分类</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < PInfoList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=PInfoList[i].ID %>"></td>
	                <td><%=PInfoList[i].ID %></td>
	                <td><%=PInfoList[i].Name %></td>
	                <td><%=PInfoList[i].Hits %></td>
	                <td><%=BPC.GetCategoryNameByID(PInfoList[i].CategoryID) %></td>
	                <td><%=PInfoList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
				                <li><a href="productsedit.aspx?id=<%=PInfoList[i].ID %>&p=<%=PageIndex %>"><div>编辑</div></a></li>
			                    <li><a href="javascript:del(<%=PInfoList[i].ID %>);"><div>删除</div></a></li>
			                </ul>
		                </div>
	                </td>
	            </tr>
            <%} %>
            </tbody>
        </table>
        <%
            Response.Write(PageBarHtml);
        }
        else
        { %>
        <div class="noData"></div>
        <%} %>
        
    </div>
    
</div>

</body>
</html>
