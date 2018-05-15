if (self == top)
	location.href = "/";

function aGoTo(){
	//return false;
}

//去掉页面上所有A标记点上去的虚线框
addEvent(window, "load", function(){
	var a = $T(document, "a");
	for (var i = 0; i < a.length; i++) {
		a[i].onfocus = function(){
			this.blur();
		}
	}
})

//为表格中的行添加移上去变色，和选中变色的事件
function bindRowsEvent(tab){
	var tab = $(tab);
	if (!tab) return;
	for (var i = 1; i < tab.rows.length; i++) {
		if (!arguments[1]) {
			//var chk = TL.browser.msie ? tab.rows[i].cells[0].firstChild : tab.rows[i].cells[0].firstChild.nextSibling;
			var chk = tab.rows[i].cells[0].firstChild;
			if (chk) {
				if (getAttr(chk, "type") == "checkbox") {
					chk.onclick = function(){
						if (this.checked) {
							setAttr(this.parentNode.parentNode, "class", "click");
						}
						else {
							removeAttr(this.parentNode.parentNode, "class");
						}
					}
				}
			}
		}
		//移上去切换样式变颜色
		tab.rows[i].onmouseover = function(){
			if (getAttr(this, "class") != "click") 
				setAttr(this, "class", "hover");
		}
		tab.rows[i].onmouseout = function(){
			if (getAttr(this, "class") != "click") 
				removeAttr(this, "class");
		}
	}
}

//全选(type=1)，反选(type=2)，不选(type=3) 查找el下面的table元素中的checkbox
function changeSelect(tab, type){
	var tab = $(tab);
	if(!tab) return;
	for (var i = 1; i < tab.rows.length; i++) {
		var chk = tab.rows[i].cells[0].firstChild;
		if (chk) {
			if (getAttr(chk, "type") == "checkbox") {
				switch (type) {
					case 1:
						chk.checked = true;
						setAttr(chk.parentNode.parentNode, "class", "click");
						
						break;
					case 2:
						if (chk.checked) {
							chk.checked = false;
							removeAttr(chk.parentNode.parentNode, "class");
						}
						else {
							chk.checked = true;
							setAttr(chk.parentNode.parentNode, "class", "click");
						}
						break;
					case 3:
						chk.checked = false;
						removeAttr(chk.parentNode.parentNode, "class");
						break;
					default:
						break;
				}
			}
		}
	}
}

//获取选中的check的值，返回以,号分隔每个值的字符串(如:1,2,3,4)
function getSelectedValue(tab){
	var tab = $(tab);
	if(!tab) return;
	var arr = [];
	for (var i = 1; i < tab.rows.length; i++) {
		var chk = tab.rows[i].cells[0].firstChild;
		if (chk) {
			if (chk.type == "checkbox" && chk.checked) 
				arr.push(chk.value);
		}
	}
	return arr.toString();
}