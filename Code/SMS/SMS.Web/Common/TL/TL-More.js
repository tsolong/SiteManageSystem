/*******************************************
Name: TL-More.js
Version: 1.0
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2008-12-01
Last Update:2011-5-25
Description:
	JS效果库
*******************************************/
TL.zIndex = 10000
/****************************************
Description:
	拖动效果
	1.在页面中只要点击能拖动的对象就会把自身的zIndex置于最前面
Parameter:
	config:{
		dragElement: 鼠标所拖动的对象,
		moveElement: 拖动时移动的对象(可选参数,默认值dragElement,此参数为空,则移动的对象moveElement=拖动的对象dragElement),
		dragOpacity: 拖动时移动的对象的不透明度(可选参数,默认为不透明,取值范围0-1),
		position: 称动的范围[最小x坐标, 最小y坐标, 最大x坐标, 最大y坐标](可选参数,此参数为空,则对象可在页面上乱拖)
	}
Example:
	TL.drag({
		dragElement: TL.$("boxTitle"),
		moveElement: TL.$("box"),
		dragOpacity: 0.8,
		position: [100, 100, 500, 500]
	});//让对象能拖动,但此对象必须是绝对定位元素
	
	//如果要让一个框不能拖出网页以外的地方,请见下面代码
	var pos=TL.getDrapMaxPos(box);//此函数在此TL对象中
    TL.drag({
		dragElement: TL.$("boxTitle"),
		moveElement: TL.$("box"),
		dragOpacity: 0.8,
		position: [0, 0, pos[0], pos[1]]
	})
Create Date: 2008-12-01
****************************************/
TL.drag = function(config){
	config.moveElement = config.moveElement || config.dragElement;
	config.dragElement.config = config;
	config.dragElement.style.cursor = "move";
	TL.addEvent(config.moveElement, "mousedown", function(){
		config.moveElement.style.zIndex = TL.zIndex++;
	})
	config.dragElement.onmousedown = TL.drag.start;
	//TL.addEvent(config.dragElement, "mousedown", TL.drag.start);
}
TL.drag.start = function(e){
	e = TL.getEvent(e);
	TL.drag.dragElement = this;
	var o = TL.drag.dragElement;
	//var x = parseInt(o.config.moveElement.style.left);
	//var y = parseInt(o.config.moveElement.style.top);
	var x = parseInt(o.config.moveElement.offsetLeft);
	var y = parseInt(o.config.moveElement.offsetTop);
	o.config.moveElement.lastMouseX = e.clientX;
	o.config.moveElement.lastMouseY = e.clientY;
	if (typeof o.config.position != "undefined") {
		o.config.moveElement.minMouseX = e.clientX - x + o.config.position[0];
		o.config.moveElement.minMouseY = e.clientY - y + o.config.position[1];
		o.config.moveElement.maxMouseX = o.config.moveElement.minMouseX + o.config.position[2] - o.config.position[0];
		o.config.moveElement.maxMouseY = o.config.moveElement.minMouseY + o.config.position[3] - o.config.position[1];
	}
	document.onmousemove = TL.drag.move;
	document.onmouseup = TL.drag.end;
	return false;
}
TL.drag.move = function(e){
	e = TL.getEvent(e);
	var o = TL.drag.dragElement;
	if (typeof o.config.dragOpacity != "undefined") {
		TL.setStyle(o.config.moveElement, {
			opacity: o.config.dragOpacity
		});
	}
	var x = parseInt(o.config.moveElement.offsetLeft);
	var y = parseInt(o.config.moveElement.offsetTop);
	var ex = e.clientX;
	var ey = e.clientY;
	if (typeof o.config.position != "undefined") {
		ex = Math.max(ex, o.config.moveElement.minMouseX);
		ey = Math.max(ey, o.config.moveElement.minMouseY);
		ex = Math.min(ex, o.config.moveElement.maxMouseX);
		ey = Math.min(ey, o.config.moveElement.maxMouseY);
	}
	o.config.moveElement.style.left = x + (ex - o.config.moveElement.lastMouseX) + "px";
	o.config.moveElement.style.top = y + (ey - o.config.moveElement.lastMouseY) + "px";
	o.config.moveElement.lastMouseX = ex;
	o.config.moveElement.lastMouseY = ey;
	return false;
}
TL.drag.end = function(){
	var o = TL.drag.dragElement;
	if (typeof o.config.dragOpacity != "undefined") {
		TL.setStyle(o.config.moveElement, {
			opacity: 1
		});
	}
	document.onmousemove = null;
	document.onmouseup = null;
	TL.drag.dragElement = null;
}

/****************************************
Description:
	页面遮罩层
	1.可以通过new来创建多个遮罩层
	2.当页面大小改变或滚动时,遮罩层会自动调整为网页的高度
Example:
	var obj = new TL.overlay();//显示
	obj.close();//关闭(从页面中移出)
Create Date: 2008-12-04
****************************************/
TL.overlay = function(){
	var overlay = document.createElement("div");
	TL.setStyle(overlay, {
		width: "100%",
		height: TL.getPageSize()[1] + "px",
		position: "absolute",
		left: "0",
		top: "0",
		zIndex: TL.zIndex++,
		backgroundColor: "#000",
		opacity: 0
	});
	
	new TL.tween("linear", "", 50, 0, 10, 1, function(v){
		TL.setStyle(overlay, {
			opacity: parseFloat(v / 100)
		})
	});
	
	this.changeSize = function(){
		overlay.style.height = TL.getPageSize()[1] + "px";
	}
	this.show = function(){
		document.getElementsByTagName("body")[0].appendChild(overlay);
		TL.addEvent(window, "resize", this.changeSize);
		TL.addEvent(window, "scroll", this.changeSize);
	}
	this.close = function(){
		
		oThis = this;
	
		new TL.tween("linear", "", 50, 0, 10, 1, function(v){
			TL.setStyle(overlay, {
				opacity: parseFloat((50 - v) / 100)
			})
		}, function(){
			if (overlay) 
				overlay.parentNode.removeChild(overlay);
			TL.removeEvent(window, "resize", oThis.changeSize);
			TL.removeEvent(window, "scroll", oThis.changeSize);
			
			overlay = null;
			if (TL.browser.msie) 
				CollectGarbage();
		});
		
		
	}
	this.show();
}

/****************************************
Description:
	loading效果
	1.页面中永远只有一个loading元素,不可创建多个实例
	2.loading块下面会有遮罩层出现
	3.如果页面已存在loading元素,则只会替换提示的内容,不会创建新的元素
	4.当页面大小改变或滚动时,loading块自动调整居中显示
Parameter:
	config:{
		skin: 皮肤的名称(可选参数,默认为"default"),
		content: 内容(可以是html),
	}
Example:
	TL.loading({
		skin: "default",
		content: "加载数据中请稍后...",
	});//显示
	TL.loading.close();//关闭
Create Date: 2008-12-04
****************************************/
TL.loading = function(config){
	config.skin = config.skin || "default";
	TL.loading.show(config);
}
TL.loading.Center = function(){
	TL.setWindowCenter(TL.$("loading"));
}
TL.loading.show = function(config){
	var loading = TL.$("loading");
	if (loading) {
		loading.innerHTML = config.content;
		TL.loading.Center();
		return;
	}
	TL.loading.overlay = new TL.overlay();
	var loading = document.createElement("div");
	loading.setAttribute("id", "loading");
	loading.style.zIndex = TL.zIndex++;
	loading.className = config.skin;
	loading.innerHTML = config.content;
	TL.setStyle(win, {
		left: "-1000px",
		top: "-1000px"
	})
	document.getElementsByTagName("body")[0].appendChild(loading);
	TL.loading.Center();
	TL.addEvent(window, "resize", TL.loading.Center);
	TL.addEvent(window, "scroll", TL.loading.Center);
}
TL.loading.close = function(){
	var loading = TL.$("loading");
	if (loading) 
		loading.parentNode.removeChild(loading);
	if (TL.loading.overlay != null) 
		TL.loading.overlay.close();
	TL.loading.overlay = null;
	TL.removeEvent(window, "resize", TL.loading.Center);
	TL.removeEvent(window, "scroll", TL.loading.Center);
}

/****************************************
Description:
	window对话框
	1.可实例化多个对话框
	2.提供七种不同类型的对话框
	3.可自定义对话框参数
Example:
	new TL.win({
	  type: 1,
		title: "系统提示",
		msg: "请输入您的用户名"
	})//提示对话框
	new TL.win({
		type: 6,
		title: "自定义内容",
		html: "自定义内容"
	})//自定义对话框
	new TL.win({
		type: 7,
		width: 1000,
		height: 700,
		title: "TsoLong Blog -- http://www.tsolong.com/",
		url: "http://www.tsolong.com"
	})//加载网页
Create Date: 2008-12-05
Last Update:2011-5-25
****************************************/
TL.win = function(config){
	this.config = {
		type: 1, //对话框类型(可选参数,默认值为1) 1:提示对话框 2:警告对话框 3:正确对话框 4:错误对话框 5:询问对话框 6:自定义对话框 7:加载指定网页
		width: 350, //对话框宽度(可选参数,默认为350)
		height: "", //type设为6-7有效,这个height是指对话框去除标题栏剩余部分的高度
		position: [], //对话框位置position[left,top](可选参数,默认居中)
		title: "", //标题文字
		msg: "", //提示信息 type设为1-5有效
		html: "", //自定内容 type设为6有效(支持html)
		url: "", //网页地址 type设为7有效
		isOverlay: true, //显示遮罩层(可选参数,默认为true)
		isTransition: true, //对话框是否以渐变的方式显示出来(可选参数，默认为true)
		isTopControl: true, //对话框顶部右侧按钮 type设为6-7有效(可选参数,默认为true)
		isDrag: true, //拖动(可选参数,默认为true)
		dragOpacity: 0.7, //拖动时对话框的不透明度(可选参数,默认为0.7)
		closeEvent: null, //type设为1-4时,确定按钮和关闭图标的回调函数 type设为5时,取消按钮和关闭图标的回调函数 type设为6-7时,关闭图标的回调函数 (可选参数)
		confirmEvent: null //type设为5有效,确定按钮的回调函数(可选参数)
	}
	for (var par in config) {
		this.config[par] = config[par];
	}
	this.show();
}
TL.win.prototype = {
	show: function(){
		var config = this.config;
		var oThis = this;
		
		if (config.isOverlay) 
			this.overlay = new TL.overlay();
		
		var win = document.createElement("div");
		this.win = win;//保存元素对象
		win.className = "win";
		win.style.width = config.width + "px";
		win.style.zIndex = TL.zIndex++;
		
		var winTop = document.createElement("div");
		this.win.top = winTop;//保存元素对象
		winTop.innerHTML = config.title;
		winTop.className = "winTop";
		
		var winMiddle = document.createElement("div");
		this.win.middle = winMiddle;//保存元素对象
		winMiddle.className = "winMiddle";
		
		win.appendChild(winTop);
		win.appendChild(winMiddle);
		
		var winDrag = function(){
			var pos = TL.getDrapMaxPos(win);
			TL.drag({
				dragElement: winTop,
				moveElement: win,
				dragOpacity: config.dragOpacity,
				position: [0, 0, pos[0], pos[1]]
			})
		}
		var topControlClose = function(){
			var winClose = document.createElement("a");
			winClose.className = "winClose";
			winClose.title = "关闭";
			winClose.onclick = function(){
				oThis.close();
				if (config.closeEvent != null) 
					config.closeEvent();
			}
			winTop.appendChild(winClose);
		}
		var topControlMinMax = function(){
			var hide = function(){
				this.style.display = "none";
				this.nextSibling.style.display = "block";
				winMiddle.style.display = "none";
				if (config.isDrag) {
					winDrag();
				}
			}
			var show = function(){
				this.style.display = "none";
				this.previousSibling.style.display = "block";
				winMiddle.style.display = "block";
				if (config.isDrag) {
					winDrag();
				}
			}
			var winMin = document.createElement("a");
			winMin.className = "winMin";
			winMin.title = "收起";
			winMin.onclick = hide;
			winTop.appendChild(winMin);
			
			var winMax = document.createElement("a");
			winMax.className = "winMax";
			winMax.title = "展开";
			winMax.onclick = show;
			winTop.appendChild(winMax);
			
			winTop.ondblclick = function(){
				if (winMiddle.style.display == "block" || winMiddle.style.display == "") 
					hide.apply(winMin);
				else 
					show.apply(winMax);
			}
		}
		
		var winBtnOk, winBtnCancel;
		
		if (config.type > 0 && config.type <= 5) {
			topControlClose();
			
			var winMsg = document.createElement("div");
			this.win.middle.msg = winMsg;//保存元素对象
			winMsg.className = "winMsg msg" + config.type;
			winMsg.innerHTML = config.msg;
			winMiddle.appendChild(winMsg);
			
			var winControl = document.createElement("div");
			winControl.className = "winControl";
			
			winBtnOk = document.createElement("input");
			winBtnOk.type = "button";
			winBtnOk.value = "确 定";
			winBtnOk.className = "winBtn";
			winBtnOk.onmouseover = function(){
				this.className = "winBtnHover";
			}
			winBtnOk.onmouseout = function(){
				this.className = "winBtn";
			}
			winControl.appendChild(winBtnOk);
			if (config.type < 5) {
				winBtnOk.onclick = function(){
					oThis.close();
					if (config.closeEvent != null) 
						config.closeEvent();
				}
			}
			else {
				winBtnOk.onclick = function(){
					oThis.close();
					if (config.confirmEvent != null) 
						config.confirmEvent();
				}
				winBtnCancel = winBtnOk.cloneNode(false);
				winBtnCancel.value = "取 消";
				winBtnCancel.onmouseover = function(){
					this.className = "winBtnHover";
				}
				winBtnCancel.onmouseout = function(){
					this.className = "winBtn";
				}
				winBtnCancel.onclick = function(){
					oThis.close();
					if (config.closeEvent != null) 
						config.closeEvent();
				}
				winControl.appendChild(winBtnCancel);
			}
			
			winMiddle.appendChild(winControl);
		}
		else {
			if (config.isTopControl) {
				topControlMinMax();
				topControlClose();
			}
			if (config.type == 6) {
				if (config.height != "") 
					winMiddle.style.height = config.height + "px";
				winMiddle.innerHTML = config.html;
			}
			else {
				winMiddle.innerHTML = "<iframe src=\"" + config.url + "\" style=\"width:100%; height:" + config.height + "px; border:0px;\" frameborder=\"0\"></iframe>";
			}
		}
		
		TL.setStyle(win, {
			left: "-1000px",
			top: "-1000px"
		})
		
		document.getElementsByTagName("body")[0].appendChild(win);
		
		var lastBind = function(){
			if (config.isDrag) 
				winDrag();
			
			if (config.type > 0 && config.type <= 5) {
				if (config.type < 5) 
					winBtnOk.focus();
				else 
					winBtnCancel.focus();
			}
		}
		
		if (config.isTransition) {
			var pos;
			if (config.position.length == 0) 
				pos = TL.getWindowCenterPos(win);
			else 
				pos = config.position;
			
			new TL.tween("quartic", "easeOut", pos[1], 0, 10, 10, function(v){
				TL.setStyle(win, {
					left: pos[0] + "px",
					top: v + "px"
				})
			}, lastBind);
		}
		else {
			if (config.position.length == 0) {
				TL.setWindowCenter(win);
			}
			else {
				TL.setStyle(win, {
					left: config.position[0] + "px",
					top: config.position[1] + "px"
				})
			}
			lastBind();
		}
	},
	close: function(){
		var win = this.win;
		var config = this.config;
		win.parentNode.removeChild(win);
		if (config.isOverlay) 
			this.overlay.close();
	}
}

/****************************************
Description:
	缓动类
Example:
	xhtml:
		<div id="myBox" style="width:100px; height:100px; position:absolute; border:1px solid red; background:#ccc;"></div>
	JavaScript:
		new TL.tween("bounce", "easeOut", 500, 0, 80, 10, function(v){
			TL.setStyle("myBox", {
				top: v + "px"
			})
		}, function(){
			alert("缓动完成");
		});
Create Date: 2009-03-25
****************************************/
/*
 * 
 * @param {Object} tween			缓动种类(请参照TL.tween.list)
 * @param {Object} ease				缓动类型(easeIn、easeOut、easeInOut)
 * @param {Object} v				缓动值(从0开始到你设定的v,0-v)
 * @param {Object} startStep		缓动的起始步数
 * @param {Object} stepTotal		缓动的总步数
 * @param {Object} stepTime			缓动的步进时间(毫秒)
 * @param {Object} stepFunc			缓动时每步执行的函数
 * @param {Object} callBackFunc		缓动完成回调的函数
 */
TL.tween = function(tween, ease, v, startStep, stepTotal, stepTime, stepFunc, callBackFunc){
	var oInterval = window.setInterval(function(){
		if (startStep < stepTotal) {
			startStep++;
			var tmpFunc = tween == "linear" ? TL.tween.list["linear"] : TL.tween.list[tween][ease];
			stepFunc(Math.ceil(tmpFunc(startStep, 0, v, stepTotal)));
		}
		else {
			stepFunc(v);
			clearInterval(oInterval);
			if (callBackFunc) 
				callBackFunc();
		}
	}, stepTime)
}
TL.tween.list = {
	linear: function(t, b, c, d){
		return c * t / d + b;
	},
	quadratic: {
		easeIn: function(t, b, c, d){
			return c * (t /= d) * t + b;
		},
		easeOut: function(t, b, c, d){
			return -c * (t /= d) * (t - 2) + b;
		},
		easeInOut: function(t, b, c, d){
			if ((t /= d / 2) < 1) 
				return c / 2 * t * t + b;
			return -c / 2 * ((--t) * (t - 2) - 1) + b;
		}
	},
	cubic: {
		easeIn: function(t, b, c, d){
			return c * (t /= d) * t * t + b;
		},
		easeOut: function(t, b, c, d){
			return c * ((t = t / d - 1) * t * t + 1) + b;
		},
		easeInOut: function(t, b, c, d){
			if ((t /= d / 2) < 1) 
				return c / 2 * t * t * t + b;
			return c / 2 * ((t -= 2) * t * t + 2) + b;
		}
	},
	quartic: {
		easeIn: function(t, b, c, d){
			return c * (t /= d) * t * t * t + b;
		},
		easeOut: function(t, b, c, d){
			return -c * ((t = t / d - 1) * t * t * t - 1) + b;
		},
		easeInOut: function(t, b, c, d){
			if ((t /= d / 2) < 1) 
				return c / 2 * t * t * t * t + b;
			return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
		}
	},
	quintic: {
		easeIn: function(t, b, c, d){
			return c * (t /= d) * t * t * t * t + b;
		},
		easeOut: function(t, b, c, d){
			return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
		},
		easeInOut: function(t, b, c, d){
			if ((t /= d / 2) < 1) 
				return c / 2 * t * t * t * t * t + b;
			return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
		}
	},
	sinusoidal: {
		easeIn: function(t, b, c, d){
			return -c * Math.cos(t / d * (Math.PI / 2)) + c + b;
		},
		easeOut: function(t, b, c, d){
			return c * Math.sin(t / d * (Math.PI / 2)) + b;
		},
		easeInOut: function(t, b, c, d){
			return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b;
		}
	},
	exponential: {
		easeIn: function(t, b, c, d){
			return (t == 0) ? b : c * Math.pow(2, 10 * (t / d - 1)) + b;
		},
		easeOut: function(t, b, c, d){
			return (t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b;
		},
		easeInOut: function(t, b, c, d){
			if (t == 0) 
				return b;
			if (t == d) 
				return b + c;
			if ((t /= d / 2) < 1) 
				return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
			return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b;
		}
	},
	circular: {
		easeIn: function(t, b, c, d){
			return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b;
		},
		easeOut: function(t, b, c, d){
			return c * Math.sqrt(1 - (t = t / d - 1) * t) + b;
		},
		easeInOut: function(t, b, c, d){
			if ((t /= d / 2) < 1) 
				return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b;
			return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b;
		}
	},
	elastic: {
		easeIn: function(t, b, c, d, a, p){
			if (t == 0) 
				return b;
			if ((t /= d) == 1) 
				return b + c;
			if (!p) 
				p = d * .3;
			if (!a || a < Math.abs(c)) {
				a = c;
				var s = p / 4;
			}
			else 
				var s = p / (2 * Math.PI) * Math.asin(c / a);
			return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
		},
		easeOut: function(t, b, c, d, a, p){
			if (t == 0) 
				return b;
			if ((t /= d) == 1) 
				return b + c;
			if (!p) 
				p = d * .3;
			if (!a || a < Math.abs(c)) {
				a = c;
				var s = p / 4;
			}
			else 
				var s = p / (2 * Math.PI) * Math.asin(c / a);
			return (a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b);
		},
		easeInOut: function(t, b, c, d, a, p){
			if (t == 0) 
				return b;
			if ((t /= d / 2) == 2) 
				return b + c;
			if (!p) 
				p = d * (.3 * 1.5);
			if (!a || a < Math.abs(c)) {
				a = c;
				var s = p / 4;
			}
			else 
				var s = p / (2 * Math.PI) * Math.asin(c / a);
			if (t < 1) 
				return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b;
			return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
		}
	},
	back: {
		easeIn: function(t, b, c, d, s){
			if (s == undefined) 
				s = 1.70158;
			return c * (t /= d) * t * ((s + 1) * t - s) + b;
		},
		easeOut: function(t, b, c, d, s){
			if (s == undefined) 
				s = 1.70158;
			return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
		},
		easeInOut: function(t, b, c, d, s){
			if (s == undefined) 
				s = 1.70158;
			if ((t /= d / 2) < 1) 
				return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
			return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
		}
	},
	bounce: {
		easeIn: function(t, b, c, d){
			return c - Tween.Bounce.easeOut(d - t, 0, c, d) + b;
		},
		easeOut: function(t, b, c, d){
			if ((t /= d) < (1 / 2.75)) {
				return c * (7.5625 * t * t) + b;
			}
			else 
				if (t < (2 / 2.75)) {
					return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
				}
				else 
					if (t < (2.5 / 2.75)) {
						return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
					}
					else {
						return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
					}
		},
		easeInOut: function(t, b, c, d){
			if (t < d / 2) 
				return Tween.Bounce.easeIn(t * 2, 0, c, d) * .5 + b;
			else 
				return Tween.Bounce.easeOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
		}
	}
}

//简化调用
var drag = drag || TL.drag;
var ovarlay = ovarlay || TL.ovarlay;
var loading = loading || TL.loading;
var win = win || TL.win;
var tween = tween || TL.tween;