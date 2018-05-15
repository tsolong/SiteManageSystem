/****************************************
Name: UploadControl.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2009-08-13
Description:
	上传文件控件
****************************************/
function UploadControl(config){
	this.container = null;
	this.url = null;
	this.extNames = [];
	this.files = [];
	this.fileCount = 1;
	this.onSubmit = null;
	
	for (var par in config) {
		this[par] = config[par];
	}
	
	this.init();
}
UploadControl.prototype = {
	init: function(){
		var oThis = this;
		
		//iframe
		var uploadFileFrame;
		var frameName = "uploadFrame_" + Math.floor(Math.random() * 1000);
		try {//ie不能修改iframe的name
			uploadFileFrame = document.createElement("<iframe name=\"" + frameName + "\">");
		} 
		catch (e) {//ff
			uploadFileFrame = document.createElement("iframe");
			setAttr(uploadFileFrame, "name", frameName);
		}
		setAttr(uploadFileFrame, "id", frameName);
		setStyle(uploadFileFrame, {
			display: "none"
		});

		//上传表单
		var uploadFileForm
		try {//ie不能设置form的enctype
			uploadFileForm = document.createElement("<form enctype=\"multipart/form-data\">");
		} 
		catch (e) {//ff
			uploadFileForm = document.createElement("form");
			setAttr(uploadFileForm, "enctype", "multipart/form-data");
		}
		setAttr(uploadFileForm, "action", this.url);
		setAttr(uploadFileForm, "target", frameName);
		setAttr(uploadFileForm, "method", "post");
		uploadFileForm.onsubmit = function(){
			if (oThis.files.length <= 0) {
				alert("没有可上传的文件");
				return false;
			}
			for (var i = 0; i < oThis.files.length; i++) {
				if (oThis.files[i].value == "") {
					alert("您还未选择第 " + (i + 1) + " 个文件");
					return false;
				}
				if (!oThis.checkFileExt(oThis.files[i].value.substr(oThis.files[i].value.lastIndexOf(".") + 1, oThis.files[i].value.length - 1).toLowerCase())) {
					alert("第 " + (i + 1) + " 个文件格式不正确,请选择 " + oThis.extNames.join(",") + " 格式的文件");
					return false;
				}
			}
			if (oThis.onSubmit) 
				oThis.onSubmit();
			return true;
		}
		
		//添加按钮
		var addFileBtn = document.createElement("input")
		this.addFileBtn = addFileBtn;
		setAttr(addFileBtn, "type", "button");
		setAttr(addFileBtn, "class", "btn");
		setAttr(addFileBtn, "value", "增 加");
		setStyle(addFileBtn, {
			margin: "0 20px 0 0"
		});
		addEvent(addFileBtn, "click", function(){
			oThis.addFile();
		})
		
		//上传按钮
		var uploadFileBtn = document.createElement("input")
		this.uploadFileBtn = uploadFileBtn;
		setAttr(uploadFileBtn, "type", "submit");
		setAttr(uploadFileBtn, "class", "btn");
		setAttr(uploadFileBtn, "value", "上 传");
		
		//文件列表区域
		this.fileList = document.createElement("div");
		
		this.container.appendChild(uploadFileFrame);
		this.container.appendChild(uploadFileForm);
		uploadFileForm.appendChild(addFileBtn);
		uploadFileForm.appendChild(uploadFileBtn);
		uploadFileForm.appendChild(this.fileList);

		//初始化时默认添加一个上传控件
		this.addFile();
	},
	addFile: function(){
		var oThis = this;
		if (this.files.length < this.fileCount) {
			var div = document.createElement("div");
			setStyle(div, {
				margin: "10px 20px 0 0"
			});
			
			var file = document.createElement("input");
			setAttr(file, "name", "FileName");
			setAttr(file, "type", "file");
			setAttr(file, "size", "30");
			setStyle(file, {
				margin: "0 15px 0 0"
			});
			addEvent(file, "change", function(){
				if (!oThis.checkFileExt(file.value.substr(file.value.lastIndexOf(".") + 1, file.value.length - 1).toLowerCase())) {
					file.value = "";
					alert("文件格式不正确,请选择 " + oThis.extNames.join(",") + " 格式的文件");
				}
			})
			
			var delFileBtn = document.createElement("a");
			setAttr(delFileBtn, "href", "javascript:void(0);");
			delFileBtn.innerHTML = "删除";
			addEvent(delFileBtn, "click", function(){
				oThis.delFile(delFileBtn);
			})
			
			div.appendChild(file);
			div.appendChild(delFileBtn);
			this.fileList.appendChild(div);
			this.files.push(file);
				
			if (this.files.length > 0) 
				removeAttr(this.uploadFileBtn, "disabled");
			
			if (this.files.length >= this.fileCount) 
				setAttr(this.addFileBtn, "disabled", "disabled");
		}
	},
	delFile: function(delFileBtn){
		var file = delFileBtn.previousSibling;
		for (var i = 0, n = 0; i < this.files.length; i++) {
			if (this.files[i] != file) 
				this.files[n++] = this.files[i];
		}
		if (this.files.length > 0) 
			this.files.length -= 1;
		file.parentNode.removeChild(file);
		delFileBtn.parentNode.removeChild(delFileBtn);
		
		if (this.files.length <= 0) 
			setAttr(this.uploadFileBtn, "disabled", "disabled");
			
		if (this.files.length < this.fileCount) 
			removeAttr(this.addFileBtn, "disabled");
	},
	checkFileExt: function(extName){
		for (var i = 0; i < this.extNames.length; i++) {
			if (extName.toLowerCase() == this.extNames[i].toLowerCase()) {
				return true;
			}
		}
		return false;
	}
}






/*function del(id){
	if (!id || id == "") {
		new win({
			type: 1,
			title: "系统提示",
			msg: "请选择要删除的照片"
		})
	}
	else {
		new win({
			type: 5,
			title: "系统提示",
			msg: "删除店铺照片将无法恢复,你确定要删除吗?",
			confirmEvent: function(){
				location.href = "?action=del&id=" + id;
			}
		})
	}
}

function delAll(){
	if ($("_CurrentTotalPhoto").value == "0") {
		new win({
			type: 1,
			title: "系统提示",
			msg: "您还没有上传店铺照片"
		})
	}
	else {
		new win({
			type: 5,
			title: "系统提示",
			msg: "删除全部店铺照片将无法恢复,你确定要删除全部吗?",
			confirmEvent: function(){
				location.href = "?action=delall";
			}
		})
	}
}*/