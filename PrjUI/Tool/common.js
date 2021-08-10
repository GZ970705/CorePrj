var apiDomain = "/";//"http://localhost:58915/";

var kToken = "token";

var PostDataFun = {
    New: function (data) {

        return {
            PostData: data
        }
    }
}

var GuidEmpty = "00000000-0000-0000-0000-000000000000";
function GuidIsEmpty(value){
	
	if (value) {
		
		if (value == GuidEmpty) {
			
			return true;
		}
	} else {
		
		return true;
	}
	
	return false;
}


var layer;
function Init(callback) {
	
	layui.use('layer', function() {
		layer = layui.layer;
		
		callback();
	});	
	
}

var loader;
function ShowLoading(){
	
	if (loader) {
		layer.close(loader);
	}
	
	loader = layer.load(2, {
		shade: [0.1,'#000']//0.1透明度的黑色背景
//		,time: 10*1000 //设定最长等待10秒 
	});
}
function HideLoading(){
	
	if (loader) {
		layer.close(loader);
		loader = null;
	}
}

var api = {
	admin: {
		login: API("admin/login"),
	}
}

function API(url){
	
	return apiDomain + "api/" + url;
}

jQuery.support.cors = true;//针对IE低版本跨域
function ApiRequestPOST(url, param, isOAuth, callback,isAsync) {
	
    var ajax = $.ajax({
        type: 'POST',
        url: url,
        data: param ? JSON.stringify(param) : null,
        dataType: "json",
        contentType: 'application/json',
        async: isAsync ? false : true,
		//必须有这项的配置，不然cookie无法发送至服务端
      	xhrFields: {
            withCredentials: true
        },
        crossDomain: true,
        success: function (data) {

            if (callback) {
				
				var tData;
				
				if (typeof(data)=="string") {
					tData = JSON.parse(data);
				} else {
					tData = data;
				}
				
				if (tData.MessageCode == 1 ) {
					
            		callback(tData);
                } else if (tData.MessageCode == 401) {

				} else {
					
					LayerAlert(tData.Message, null, true);
				}
            }
        },
        error: function (data, status, e) {
        	
			if (data && data.status == 401) {
				
				//GotoPage2Replace("../admin/login.html", top);
			} else {
				
				LayerAlert("请求失败", null, true);
			}
        },
        complete: function (data, status) { //请求完成后最终执行参数

            if (status == 'timeout') {//超时,status还有success,error等值的情况

                ajax.abort();
            }
        }
    });
    
    return ajax;
}

//FormData表单方式 上传，含其他参数
function UploadPost(url, formdata, isOAuth, callback, isAsync){
	
//	var formData = new FormData($("#form")[0]);
//	UploadPost(API("upload/test"),formData, true, function(data){
//		alert(JSON.stringify(data));
//	});
	
	var ajax = $.ajax({
        url: url,
        type: 'post',
        data: formdata,
        dataType: "json",
        contentType: false,
        processData: false,
        async: isAsync ? false : true,
        beforeSend: function (xhr) {
        	if (isOAuth) {
	        	var token = ReadLocalStorage(kToken);
	    		if (token) {
	    			xhr.setRequestHeader("Authorization", "Bearer " + token);
	    		}
        	}
        },
		//必须有这项的配置，不然cookie无法发送至服务端
      	xhrFields: {
            withCredentials: true
        },
        crossDomain: true,
        success: function (data) {

            if (callback) {
				
				var tData;
				
				if (typeof(data)=="string") {
					tData = JSON.parse(data);
				} else {
					tData = data;
				}
				
				if (tData.MessageCode == 1 ) {
					
            		callback(tData);
				} else if (tData.MessageCode == 401) {
					
					GotoPage2Replace("../admin/login.html", top);
				} else {
					
					LayerAlert(tData.Message, null, true);
				}
            }
        },
        error: function (data, status, e) {
        	
			if (data && data.status == 401) {
				
				GotoPage2Replace("../admin/login.html", top);
			} else {
				
				LayerAlert("status：" + status + "，error：" + e + "，data：" + JSON.stringify(data.responseJSON), null, true);
			}
        },
        complete: function (data, status) { //请求完成后最终执行参数

            if (status == 'timeout') {//超时,status还有success,error等值的情况

                ajax.abort();
            }
        }
    });
    
    return ajax;
}

//弹框提示
function LayerAlert(msg, callback, isTop) {
	
	if (isTop) {
		if (window.top == window) {
			layer.alert(msg, {
				skin: 'layui-layer-molv', //样式类名
				closeBtn: 0
			}, function(index){
				
				layer.close(index);
				
				if (callback) {
					callback();
				}
			});
		} else {
			//window.top.LayerAlert(msg, callback);
		}
	} else {
		layer.alert(msg, {
			skin: 'layui-layer-molv', //样式类名
			closeBtn: 0
		}, function(index){
			
			layer.close(index);
			
			if (callback) {
				callback();
			}
		});
	}
}

function ShowConfirm(msg, callback, isTop){
	
	if (isTop) {
		if (window.top == window) {
			layer.confirm(msg, function(index){
				
				layer.close(index);
				
				if (callback) {
					callback();
				}
			});
		} else {
			window.top.ShowConfirm(msg, callback);
		}
	} else {
		layer.confirm(msg, function(index){
			
			layer.close(index);
			
			if (callback) {
				callback();
			}
		});
	}
}

function TimeStampFormat(value){
	var date = new Date(parseInt(value)); 
	var Y = date.getFullYear() + '-'; 
	var M = (date.getMonth()+1 < 10 ? '0'+(date.getMonth()+1) : date.getMonth()+1) + '-'; 
	var D= date.getDate() + ' '; 
	var h= date.getHours() + ':'; 
	var m= date.getMinutes() + ':'; 
	var s= date.getSeconds(); 
	return Y+M+D+h+m+s; 
}

function GetRequest() {
    var url = location.search; //获取url中"?"符后的字串   
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
} 
