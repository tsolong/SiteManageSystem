/****************************************
Name: Login.js
Author: TsoLong
Email: tsolong@126.com
WebSite: http://www.tsolong.com/
Create Date: 2008-12-10
Last Update:2011-5-25
Description:
	系统管理登录验证JS
****************************************/
if (self != top)
	top.location.href = location.href;

function checkLogin(){
	var UserName = $("UserName");
	var Password = $("Password");
	var loginForm = $("loginForm");
	disabledElement(loginForm, false);
	
	if (UserName.value == "") {
		new win({
			title: "系统提示",
			msg: "请输入您的用户名",
			closeEvent: function(){
				disabledElement(loginForm, true);
				UserName.focus();
			}
		})
	}
	else 
		if (Password.value == "") {
			new win({
				title: "系统提示",
				msg: "请输入您的密码",
				closeEvent: function(){
					disabledElement(loginForm, true);
					Password.focus();
				}
			})
		}
		else {
			new ajax({
				method: "post",
				url: "?action=checklogin",
				formElement: $("loginForm"),
				onLoading: function(){
					loading({
						content: "正在登录中请稍后..."
					})
				},
				onSuccess: function(o){
					var result = o.responseText;
					if (result == "1") {
						setTimeout(function(){
							loading.close();
							new win({
								type: 4,
								title: "系统提示 - 登录失败",
								msg: "您输入的用户名或密码格式不正确，请输入正确的用户名和密码",
								closeEvent: function(){
									disabledElement(loginForm, true);
									UserName.focus();
									UserName.select();
								}
							})
						}, 2000)
					}
					else 
						if (result == "2") {
							setTimeout(function(){
								loading.close();
								new win({
									type: 4,
									title: "系统提示 - 登录失败",
									msg: "用户不存在",
									closeEvent: function(){
										disabledElement(loginForm, true);
										UserName.focus();
										UserName.select();
									}
								})
							}, 2000)
						}
						else 
							if (result == "3") {
								setTimeout(function(){
									loading.close();
									new win({
										type: 4,
										title: "系统提示 - 登录失败",
										msg: "密码错误",
										closeEvent: function(){
											disabledElement(loginForm, true);
											Password.focus();
											Password.select();
										}
									})
								}, 2000)
							}
							else 
								if (result == "10") {
									setTimeout(function(){
										loading({
											content: "登录成功,页面自动跳转中,请稍等..."
										})
									}, 2000)
									setTimeout(function(){
										location.href = "index.aspx";
									}, 5000)
								}
				},
				onError: function(){
					setTimeout(function(){
						loading.close();
						new win({
							type: 4,
							title: "系统提示",
							msg: "请求服务器出错,请与管理员联系",
							closeEvent: function(){
								disabledElement(loginForm, true);
							}
						})
					}, 2000)
				}
			})
		}
	return false;
}

addEvent(window, "load", function(){
	var login = $("login");
	var pos = TL.getWindowCenterPos(login);
	new tween("elastic", "easeOut", pos[1] + 300, 0, 80, 30, function(v){
		//new tween("bounce", "easeOut", pos[1]+300, 0, 80, 20, function(v){
		setStyle(login, {
			left: pos[0] + "px",
			top: -300 + v + "px"
		})
	}, function(){
		$("UserName").focus();
	});
})