<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jobs.aspx.cs" Inherits="SMS.Web.Manage.Jobs.Jobs" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
	    bindRowsEvent("jobsList");
    })
    
    function del(id){
	    if (!id || id == "") {
		    new win({
			    type: 1,
			    title: "系统提示",
			    msg: "请选择要删除的招聘"
		    })
	    }
	    else {
		    new win({
			    type: 5,
			    title: "系统提示",
			    msg: "删除招聘将无法恢复,你确定要删除吗?",
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
        <span>招聘列表</span>
        <a href="jobsadd.aspx" class="add">添加</a>
        <a href="javascript:del(getSelectedValue('jobsList'));" class="del">删除</a>
        <a href="javascript:changeSelect('jobsList',1);" class="changeSelect1">全选</a>
        <a href="javascript:changeSelect('jobsList',2);" class="changeSelect2">反选</a>
        <a href="javascript:changeSelect('jobsList',3);" class="changeSelect3">不选</a>
        <a href="javascript:location.reload();" class="refresh">刷新</a>
    </div>
    
    <div class="content">
    
        <%if (RecordTotal > 0)
          { %>
        <table id="jobsList" class="listTab">
            <thead>
                <tr>
	                <th>选择</th>
	                <th>编号</th>
	                <th>职位名称</th>
	                <th>工作部门</th>
	                <th>点击次数</th>
	                <th>状态</th>
	                <th>创建时间</th>
	                <th>操作</th>
                </tr>
            </thead>
            <tbody>
            <%for (int i = 0; i < JInfoList.Count; i++)
              { %>
	            <tr>
	                <td><input type="checkbox" value="<%=JInfoList[i].ID %>"></td>
	                <td><%=JInfoList[i].ID %></td>
	                <td><%=JInfoList[i].Name %></td>
	                <td><%=JInfoList[i].Dept %></td>
	                <td><%=JInfoList[i].Hits %></td>
	                <td><%if (JInfoList[i].State) Response.Write("<span class=\"fontGreen\">启用</span>"); else Response.Write("<span class=\"fontRed\">停用</span>"); %></td>
	                <td><%=JInfoList[i].CreateDate %></td>
	                <td onmouseout='this.className="oper";' onmouseover='this.className="operHover";' class="oper">
		                <div>操作
			                <ul>
			                    <%if (JInfoList[i].State){%>
				                <li><a href="jobs.aspx?action=stop&id=<%=JInfoList[i].ID %>&p=<%=PageIndex %>"><div>停止</div></a></li>
				                <%}else{ %>
				                <li><a href="jobs.aspx?action=start&id=<%=JInfoList[i].ID %>&p=<%=PageIndex %>"><div>启用</div></a></li>
				                <%} %>
				                <li><a href="jobsedit.aspx?id=<%=JInfoList[i].ID %>&p=<%=PageIndex %>"><div>编辑</div></a></li>
			                    <li><a href="javascript:del(<%=JInfoList[i].ID %>);"><div>删除</div></a></li>
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
