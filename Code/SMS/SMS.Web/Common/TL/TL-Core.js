/*******************************************
Name: TL-Core.js
Version: 1.0
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2008-11-25
Description:
	JS操作库
*******************************************/
/*
 * start
 * */

var TL = {
	//根据id获取元素
	$: function(){
		var elements = new Array();
		for (var i = 0; i < arguments.length; i++) {
			var element = arguments[i];
			if (typeof element == "string") 
				element = document.getElementById(element);
			if (arguments.length == 1) 
				return element;
			elements.push(element);
		}
		return elements;
	},
	//根据标签名获取元素集合
	$T: function(el, tagName){
		el = typeof(el) == "string" ? TL.$(el) : el;
		return el.getElementsByTagName(tagName);
	},
	//设置元素样式
	setStyle: function(el, style){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (typeof el != "object" || typeof style != "object") 
			return;
		for (var x in style) {
			if (x == "opacity" && TL.browser.msie) 
				el.style.filter = (style[x] == 1) ? "" : "alpha(opacity=" + (style[x] * 100) + ")";
			else 
				el.style[x] = style[x];
		}
	},
	//设置元素属性
	setAttr: function(el, attrName, attrValue){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (attrName == "class" && TL.browser.msie) 
			el.setAttribute("className", attrValue);
		else 
			el.setAttribute(attrName, attrValue);
	},
	//获取元素属性
	getAttr: function(el, attrName){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (attrName == "class" && TL.browser.msie) 
			return el.getAttribute("className");
		else 
			return el.getAttribute(attrName);
	},
	//移出元素属性
	removeAttr: function(el, attrName){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (attrName == "class" && TL.browser.msie) 
			el.removeAttribute("className");
		else 
			el.removeAttribute(attrName);
	},
	//添加事件
	addEvent: function(el, eventName, handler){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (TL.browser.msie) 
			el.attachEvent("on" + eventName, handler);
		else 
			el.addEventListener(eventName, handler, false);
	},
	//移出事件
	removeEvent: function(el, eventName, handler){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (TL.browser.msie) 
			el.detachEvent("on" + eventName, handler);
		else 
			el.removeEventListener(eventName, handler, false)
	},
	//获取event
	getEvent: function(e){
		return e ? e : window.event;
		/*if (typeof e == 'undefined') e = window.event;
		 if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
		 if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
		 return e;*/
	},
	//获取key
	getKey: function(e){
		return TL.getEvent(e).keyCode;
	},
	//获取当前页面的url（不带参数）
	getPageUrl: function(){
		var pageUrl = location.href.toLowerCase();
		var findIndex = pageUrl.indexOf("?");
		if (findIndex != -1) 
			pageUrl = pageUrl.substring(0, findIndex);
		return pageUrl;
	},
	//获取当前页面名称
	getPageName: function(){
		var pageName = TL.getPageUrl();
		var arrUrl = pageName.split("/");
		pageName = arrUrl[arrUrl.length - 1];
		return pageName;
	},
	//获取页面滚动条位置 返回数组 [横向,纵向]
	getPageScroll: function(){
		var xScroll;
		var yScroll;
		if (self.pageYOffset) {
			xScroll = self.pageXOffset;
			yScroll = self.pageYOffset;
		}
		else 
			if (document.documentElement && document.documentElement.scrollTop) { // Explorer 6 Strict
				xScroll = document.documentElement.scrollLeft;
				yScroll = document.documentElement.scrollTop;
			}
			else 
				if (document.body) {// all other Explorers
					xScroll = document.body.scrollLeft;
					yScroll = document.body.scrollTop;
				}
		return [xScroll, yScroll];
	},
	//获取页面尺寸 返回数组 [页面总的宽度，页面总的高度，当前可视窗口宽度，当前可视窗口高度]
	getPageSize: function(){
		var xScroll, yScroll;
		if (window.innerHeight && window.scrollMaxY) {
			xScroll = document.body.scrollWidth;
			yScroll = window.innerHeight + window.scrollMaxY;
		}
		else 
			if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
				xScroll = document.body.scrollWidth;
				yScroll = document.body.scrollHeight;
			}
			else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
				xScroll = document.body.offsetWidth;
				yScroll = document.body.offsetHeight;
			}
		var windowWidth, windowHeight;
		if (self.innerHeight) { // all except Explorer
			windowWidth = self.innerWidth;
			windowHeight = self.innerHeight;
		}
		else 
			if (document.documentElement && document.documentElement.clientHeight) { // Explorer 6 Strict Mode
				windowWidth = document.documentElement.clientWidth;
				windowHeight = document.documentElement.clientHeight;
			}
			else 
				if (document.body) { // other Explorers
					windowWidth = document.body.clientWidth;
					windowHeight = document.body.clientHeight;
				}
		// for small pages with total height less then height of the viewport
		if (yScroll < windowHeight) 
			pageHeight = windowHeight;
		else 
			pageHeight = yScroll;
		if (xScroll < windowWidth) 
			pageWidth = windowWidth;
		else 
			pageWidth = xScroll;
		return [pageWidth, pageHeight, windowWidth, windowHeight];
	},
	//获取元素只能在当前可视窗口拖动的最大x,y坐标 返回数组 [最大横向拖动坐标，最大纵向拖动坐标]
	getDrapMaxPos: function(el){
		el = typeof(el) == "string" ? TL.$(el) : el;
		var pageScroll = TL.getPageScroll();
		var pageSize = TL.getPageSize();
		//var x = pageSize[0] - el.offsetWidth;
		var x = document.documentElement.scrollWidth - el.offsetWidth;
		var y = pageSize[1] - el.offsetHeight;
		return [x, y];
	},
	//获取元素在页面中的当前坐标
	getPos: function(el){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (arguments.length != 1 || el == null) 
			return null;
		var offsetLeft = el.offsetLeft;
		var offsetTop = el.offsetTop;
		while (el = el.offsetParent) {
			offsetTop += el.offsetTop;
			offsetLeft += el.offsetLeft;
		}
		return [offsetLeft, offsetTop];
	},
	//获取元素在当前可视窗口中间的x,y坐标 返回数组 [横向坐标，纵向坐标]
	getWindowCenterPos: function(el){
		el = typeof(el) == "string" ? TL.$(el) : el;
		var pageScroll = TL.getPageScroll();
		var pageSize = TL.getPageSize();
		var x = (pageSize[2] - el.offsetWidth) / 2 + pageScroll[0];
		var y = (pageSize[3] - el.offsetHeight) / 2 + pageScroll[1];
		return [x, y];
	},
	//设置元素在当前可视窗口的中间
	setWindowCenter: function(el){
		var pos = TL.getWindowCenterPos(el);
		el.style.left = pos[0] + "px";
		el.style.top = pos[1] + "px";
	},
	//浏览器窗口最大化
	setWindowMax: function(target){
		target.moveTo(0, 0)
		target.resizeTo(window.screen.availWidth, window.screen.availHeight)
	},
	//设置元素是否可用
	disabledElement: function(el, flag){
		el = typeof(el) == "string" ? TL.$(el) : el;
		if (el.length) {
			for (var i = 0; i < el.length; i++) {
				el[i].disabled = flag ? "" : "disabled";
			}
		}
		else {
			el.disabled = flag ? "" : "disabled";
		}
	},
	//动态加载JS文件，有回调函数
	loadJS : function(id, url, callBack){
		if (TL.$(id) && callBack) {
			callBack();
			return;
		}
		var JSFile = document.createElement("script");
		TL.setAttr(JSFile, "type", "text/javascript");
		TL.setAttr(JSFile, "id", id);
		TL.setAttr(JSFile, "src", url);
		TL.$T(document, "head")[0].appendChild(JSFile);
		//ie下js只有onreadystatechange,非ie有onload和onreadystatechange
		if (callBack) {
			if (TL.browser.msie) {//ie
				JSFile.onreadystatechange = function(){
					if (JSFile.readyState == "loaded" || JSFile.readyState == "complete") {
						callBack();
					}
				}
			}
			else {//firefox opera safari chrome
				TL.addEvent(JSFile, "load", function(){
					callBack();
				})
			}
		}
	},
	//动态加载CSS文件，无回调函数
	loadCSS : function(id, url/*, callBack*/){
		if (TL.$(id) && callBack) {
			//callBack();
			return;
		}
		var CSSFile = document.createElement("link");
		TL.setAttr(CSSFile, "type", "text/css");
		TL.setAttr(CSSFile, "rel", "stylesheet");
		TL.setAttr(CSSFile, "id", id);
		TL.setAttr(CSSFile, "href", url);
		TL.$T(document, "head")[0].appendChild(CSSFile);
		//ie下css有onload和onreadystatechange,非ie两都均无
		/*if (callBack) {
			if (TL.browser.msie) {//ie
				CSSFile.onreadystatechange = function(){
					if (CSSFile.readyState == "loaded" || CSSFile.readyState == "complete") {
						callBack();
						alert("ie");
					}
				}
			}
			else {//firefox opera safari chrome
				TL.addEvent(CSSFile, "load", function(){
					callBack();
					alert("ff");
				})
			}
		}*/
	},
	//设为首页
	setHome : function(obj, vrl){
		try {
			obj.style.behavior = 'url(#default#homepage)';
			obj.setHomePage(vrl);
		} 
		catch (e) {
			if (window.netscape) {
				try {
					netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
				} 
				catch (e) {
					alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将[signed.applets.codebase_principal_support]设置为'true'");
				}
				var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
				prefs.setCharPref('browser.startup.homepage', vrl);
			}
		}
	},
	//加入收藏
	addFavorite : function(sURL, sTitle){
		try {
			window.external.addFavorite(sURL, sTitle);
		} 
		catch (e) {
			try {
				window.sidebar.addPanel(sTitle, sURL, "");
			} 
			catch (e) {
				alert("加入收藏失败，请使用Ctrl+D进行添加");
			}
		}
	}
}

/*
Create Date:2008-12-12
1.默认get提交方式
2.提交form时
	a.如果调用时没传method,则使用form元素上指定的method,如果form也未指定method，则使用get方式
	b.如果调用时没传url,则使用form元素上指定的url
*/
TL.ajax = function(_config){
	var xmlHttp = TL.ajax.getXMLHttp();
	if (typeof xmlHttp == "undefined") {
		alert("对不起,您的浏览器不支持ajax,请升级浏版本");
		return;
	}
	var config = {
		method: "get",
		url: "",
		par: "",
		formElement: null,
		onLoading: function(){},
		onComplete: function(){},
		onSuccess: function(){},
		onError: function(){}
	}
	for (var par in _config) {
		config[par] = _config[par]
	}
	if (config.formElement != null) {
		if (typeof _config.method == "undefined") 
			if (config.formElement.getAttribute("method")) 
				config.method = config.formElement.getAttribute("method");
		
		if (typeof _config.url == "undefined") 
			if (config.formElement.getAttribute("action")) 
				config.url = config.formElement.getAttribute("action");
		
		config.par = TL.ajax.getFormString(config.formElement);
	}
	else{
		config.par = encodeURI(config.par);
	}
	config.url += (config.url.indexOf("?") > -1 ? "&" : "?") + "now=" + new Date().getTime();
	if (config.method == "get") 
		if (config.par != "") 
			config.url += "&" + config.par;

	xmlHttp.open(config.method, config.url, true);
	xmlHttp.onreadystatechange = function(){
		if (xmlHttp.readyState == 1) {
			config.onLoading(xmlHttp);
		}
		if (xmlHttp.readyState == 4) {
			config.onComplete(xmlHttp);
			if (xmlHttp.status && xmlHttp.status >= 200 && xmlHttp.status < 300) {
				config.onSuccess(xmlHttp);
			}
			else {
				config.onError(xmlHttp);
			}
		}
	}
	if (config.method == "get") {
		xmlHttp.send(null);
	}
	else {
		xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		xmlHttp.send(config.par);
	}
}
//根据不同的浏览器创建XMLHttpRequest对象
TL.ajax.getXMLHttp = function(){
	var ver = [
		function(){
			return new XMLHttpRequest();
		}, function(){
			return new ActiveXObject("Microsoft.XMLHTTP");
		}, function(){
			return new ActiveXObject("MSXML2.XmlHttp.6.0");
		}, function(){
			return new ActiveXObject("MSXML2.XmlHttp.3.0");
		}
	];
	var xmlHttp;
	for (var i = 0; i < ver.length; i++) {
		try {
			xmlHttp = ver[i]();
			break;
		} 
		catch (e) {
		}
	}
	return xmlHttp;
}
//将指定的form表单所有值转换成string字符串
TL.ajax.getFormString = function(formElement){
	var str = "", and = "";
	var el;
	var elValue;
	for (var i = 0; i < formElement.length; i++) {
		el = formElement[i];
		if (el.name != "") {
			if (el.type == "select-one") {
				elValue = el.options[el.selectedIndex].value;
				//elValue = el.value;
			}
			else if (el.type == "checkbox" || el.type == "radio") {
				if (el.checked == false) {
					continue;
				}
				elValue = el.value;
			}
			else if (el.type == "button" || el.type == "submit" || el.type == "reset" || el.type == "image") {
				continue;
			}
			else {
				elValue = el.value;
			}
			elValue = encodeURIComponent(elValue);
			
			str += and + el.name + "=" + elValue;
			and = "&";
		}
	}
	return str;
}

//浏览器版本以即类型 如当前是ie浏览器访问页面 则TL.browser.msie返回true TL.browser.version返回ie的版本 其它则返回false
TL.browser = {
	version: (navigator.userAgent.toLowerCase().match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [])[1],
	msie: /msie/.test(navigator.userAgent.toLowerCase()) && !/opera/.test(navigator.userAgent.toLowerCase()),
	mozilla: /mozilla/.test(navigator.userAgent.toLowerCase()) && !/(compatible|webkit)/.test(navigator.userAgent.toLowerCase()),
	opera: /opera/.test(navigator.userAgent.toLowerCase()),
	safari: /webkit/.test(navigator.userAgent.toLowerCase())
}

//简化调用
var $ = $ || TL.$;
var $T = $T || TL.$T;
var setStyle = setStyle || TL.setStyle;
var setAttr = setAttr || TL.setAttr;
var getAttr = getAttr || TL.getAttr;
var removeAttr = removeAttr || TL.removeAttr;
var addEvent = addEvent || TL.addEvent;
var removeEvent = removeEvent || TL.removeEvent;
var getEvent = getEvent || TL.getEvent;
var getKey = getKey || TL.getKey;
var getPos = getPos || TL.getPos;
var disabledElement = disabledElement || TL.disabledElement;
var loadJS = loadJS || TL.loadJS;
var loadCSS = loadCSS || TL.loadCSS;
var setHome = setHome || TL.setHome;
var addFavorite =addFavorite || TL.addFavorite;
var ajax = ajax || TL.ajax;

/*
 * end
 * */





/*
Description:
	删除数组元素
Parameter:
	_dx:删除元素的下标
Example:
	Array.remove(dx)
*/
Array.prototype.remove = function(_dx){
	if (isNaN(_dx) || _dx > this.length) {
		return false;
	}
	for (var i = 0, n = 0; i < this.length; i++) {
		if (this[i] != this[_dx]) {
			this[n++] = this[i]
		}
	}
	this.length -= 1
}





/*----------摘自 json2.js----------*/
if (!this.JSON) {
    JSON = {};
}
(function(){

	function f(n){
		return n < 10 ? '0' + n : n;
	}
	
	if (typeof Date.prototype.toJSON !== 'function') {
	
		Date.prototype.toJSON = function(key){
		
			return this.getUTCFullYear() + '-' +
			f(this.getUTCMonth() + 1) +
			'-' +
			f(this.getUTCDate()) +
			'T' +
			f(this.getUTCHours()) +
			':' +
			f(this.getUTCMinutes()) +
			':' +
			f(this.getUTCSeconds()) +
			'Z';
		};
		
		String.prototype.toJSON = Number.prototype.toJSON = Boolean.prototype.toJSON = function(key){
			return this.valueOf();
		};
	}
	
	var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g, gap, indent, meta = { // table of character substitutions
		'\b': '\\b',
		'\t': '\\t',
		'\n': '\\n',
		'\f': '\\f',
		'\r': '\\r',
		'"': '\\"',
		'\\': '\\\\'
	}, rep;
	
	
	function quote(string){
	
	
		escapable.lastIndex = 0;
		return escapable.test(string) ? '"' +
		string.replace(escapable, function(a){
			var c = meta[a];
			return typeof c === 'string' ? c : '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
		}) +
		'"' : '"' + string + '"';
	}
	
	
	function str(key, holder){
	
	
		var i, k, v, length, mind = gap, partial, value = holder[key];
		
		
		if (value && typeof value === 'object' &&
		typeof value.toJSON === 'function') {
			value = value.toJSON(key);
		}
		
		
		if (typeof rep === 'function') {
			value = rep.call(holder, key, value);
		}
		
		
		switch (typeof value) {
			case 'string':
				return quote(value);
				
			case 'number':
				
				
				return isFinite(value) ? String(value) : 'null';
				
			case 'boolean':
			case 'null':
				
				
				return String(value);
				
				
			case 'object':
				
				
				if (!value) {
					return 'null';
				}
				
				
				gap += indent;
				partial = [];
				
				
				if (Object.prototype.toString.apply(value) === '[object Array]') {
				
				
					length = value.length;
					for (i = 0; i < length; i += 1) {
						partial[i] = str(i, value) || 'null';
					}
					
					
					v = partial.length === 0 ? '[]' : gap ? '[\n' + gap +
					partial.join(',\n' + gap) +
					'\n' +
					mind +
					']' : '[' + partial.join(',') + ']';
					gap = mind;
					return v;
				}
				
				
				if (rep && typeof rep === 'object') {
					length = rep.length;
					for (i = 0; i < length; i += 1) {
						k = rep[i];
						if (typeof k === 'string') {
							v = str(k, value);
							if (v) {
								partial.push(quote(k) + (gap ? ': ' : ':') + v);
							}
						}
					}
				}
				else {
				
				
					for (k in value) {
						if (Object.hasOwnProperty.call(value, k)) {
							v = str(k, value);
							if (v) {
								partial.push(quote(k) + (gap ? ': ' : ':') + v);
							}
						}
					}
				}
				
				v = partial.length === 0 ? '{}' : gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' +
				mind +
				'}' : '{' + partial.join(',') + '}';
				gap = mind;
				return v;
		}
	}
	
	
	if (typeof JSON.stringify !== 'function') {
		JSON.stringify = function(value, replacer, space){
		
		
			var i;
			gap = '';
			indent = '';
			
			
			if (typeof space === 'number') {
				for (i = 0; i < space; i += 1) {
					indent += ' ';
				}
				
				
			}
			else 
				if (typeof space === 'string') {
					indent = space;
				}
			
			
			rep = replacer;
			if (replacer && typeof replacer !== 'function' &&
			(typeof replacer !== 'object' ||
			typeof replacer.length !== 'number')) {
				throw new Error('JSON.stringify');
			}
			
			
			return str('', {
				'': value
			});
		};
	}
	
	
	if (typeof JSON.parse !== 'function') {
		JSON.parse = function(text, reviver){
		
		
			var j;
			
			function walk(holder, key){
			
				var k, v, value = holder[key];
				if (value && typeof value === 'object') {
					for (k in value) {
						if (Object.hasOwnProperty.call(value, k)) {
							v = walk(value, k);
							if (v !== undefined) {
								value[k] = v;
							}
							else {
								delete value[k];
							}
						}
					}
				}
				return reviver.call(holder, key, value);
			}
			
			cx.lastIndex = 0;
			if (cx.test(text)) {
				text = text.replace(cx, function(a){
					return '\\u' +
					('0000' + a.charCodeAt(0).toString(16)).slice(-4);
				});
			}
			
			
			if (/^[\],:{}\s]*$/.test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {
			
			
				j = eval('(' + text + ')');
				
				
				return typeof reviver === 'function' ? walk({
					'': j
				}, '') : j;
			}
			
			throw new SyntaxError('JSON.parse');
		};
	}
})();
/*----------摘自 json2.js----------*/
