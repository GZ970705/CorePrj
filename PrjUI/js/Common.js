//封装一些公用的方法
//Create Date 2020/06/08 GZ
var host="http://10.87.18.9:8090";
var apiHost={
	ip:"http://10.87.18.9:8090",
	tip:"https://localhost:44344"//测试本地调试
}
var apiserver={
	UserLogin:host+"/api/Login/Login"
}
var api = {
    //object转换成json对象
    ObjectToJson: function (value) {
        var reulst = null;
        try {
            reulst = JSON.stringify(value);
        } catch (e) {
            reulst = null;
        }
        if (!reulst) {
            try {
                reulst = window.JSON.stringify(value);
            } catch (e) {
                reulst = null;
            }
        }
        return reulst;
    },
    //json对象转换成object
    JsonToObject: function (json) {
        var reulst = null;
        if (json && typeof (json) != "object") {
            try {
                reulst = JSON.parse(json);
            } catch (e) {
                reulst = null;
            }
        }
        if (!reulst || typeof (reulst) != "object") {
            try {
                reulst = window.JSON.parse(json);
            } catch (e) {
                reulst = null;
            }
        }

        return reulst;
    },
    //ajax请求 url:请求路径,type:请求类型(post get),data:传入的参数,async:是否异步,callback:请求成功后放回的回调函数
    ApiajaxHttp: function (url, type, data, async, callback) {
      var ajax=$.ajax({
            url: url,
            type: type,
           	contentType:"application/json;charset=utf-8",
           	dataType:"json",
            data: data,
            async:async ? false:true,
            success: function (data) {
                callback(data);
            }
      })
      return ajax;
    },
    //获取当前日期时间
    GetNowDateTime: function (datetype,IsShowTime) {
        if (!datetype) {
            datetype = "-";
        }
        var nowdate = new Date();

        var year = nowdate.getFullYear();        //获取当前年
        var month = nowdate.getMonth() + 1;   //获取当前月
        var date = nowdate.getDate();            //获取当前日
        var h = nowdate.getHours();              //获取当前小时数(0-23)
        var m = nowdate.getMinutes();          //获取当前分钟数(0-59)
        var s = nowdate.getSeconds();
        var reulst = "";
        if (IsShowTime) {
            reulst = year + datetype + api.DateUpNow(month) + datetype + api.DateUpNow(date) + " " + api.DateUpNow(h) + ':' + api.DateUpNow(m) + ":" + api.DateUpNow(s);
        } else {
            reulst = year + datetype + api.DateUpNow(month) + datetype + api.DateUpNow(date);
        }
        return reulst;
    },
    //获取指定表单的值序列化json
    GetFormToJson: function (fromname) {
        var reulst = $("#" + fromname).serializeArray();
        var reulststr = $("#" + fromname).serialize();
        var serializeObj = {};
        $(reulst).each(function () {
            if(serializeObj[this.name]){  
                    if($.isArray(serializeObj[this.name])){  
                            serializeObj[this.name].push(this.value);  
                        }else{  
                            serializeObj[this.name]=[serializeObj[this.name],this.value];  
                        }  
                }else{  
                    serializeObj[this.name]=this.value;   
                }  
        }); 
        return serializeObj;
    },
    //获取指定表单的值序列化字符串
    GetFromToObject: function (fromname) {
        var reulst = $("#" + fromname).serialize();
        return reulst;
    },
    //获取请求路径
    GetUrl: function () {
        return document.location.href.toLocaleLowerCase();
    },
    //获取url中"?"符后的字符串
    GetRequestUrl: function () {
        var url = location.search;
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split('&');
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    },
    //获取端口
    GetHostToPort: function () {
        return document.location.port;
    },
    //获取域名
    GetDomainName: function () {
        return document.location.host.toLocaleLowerCase().replace("http://","");
    },
    //验证非空
    CheckNullByClass: function (name, title) {
        var flag = true;
        $(name).each(function () {
            var content = $(this).attr(title)
            if ($(this).val() == '') {
                alert(content + "不可为空！");
                $(this).focus();
                flag = false;
                return false;
            }
        })
        if (!flag) {
            return false;
        }
    },
    //验证手机号码
    RegCheckPhone: function (phone) {
        var RegPhone = /^1[0-9]\d{9}$/;
        if (phone) {
            if (RegPhone.test(phone)) {
                return true;
            }
        }
        return false;
    },
    // 验证中文名称
    RegCheckChinaName: function (name) {
        var RegName = /^[\u4E00-\u9FA5]{1,6}$/;
        if (name) {
            if (RegName.test(name)) {
                return true;
            }
        }
        return false;
    },
    //验证身份证
    RegCardNo: function (card) {
        var Regcard = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
        if (card) {
            if (Regcard.test(card)) {
                return true;
            }
        }
        return false;
    },
    //验证邮箱
    Regemaill: function (maill) {
        var Regema = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if (maill) {
            if (Regema.test(maill)) {
                return true;
            }
        }
        return false;
    },
    //去除所有空格
    StrTrim: function (str) {
        return str.replace(/^\s+|\s+$/g, "");
    },
    //去除空格,去除换行符
    StrTrimAndSpaces: function (str) {
        if (str) {
            str = str.trim().replace("\t", "").replace("\r", "").replace("\n", "");
        }
        return str;
    },
    //页面跳转(不可后退)
    GotoPageReplace: function (url, sender) {
        if (sender) {
            sender.location.replace(url);
        } else {
            self.location.replace(url);
        }
    },
    //页面跳转
    GotoPageHref: function (url,sender) {
        if (sender) {
            sender.location.href = url;
        } else {
            self.location.href = url;
        }
    },
    //打开新窗口
    GotoPageOpen: function (url) {
        window.open(url);
    },
    //刷新页面
    GotoPageRefresh: function (url) {
        if (url) {
            api.GotoPageReplace(url, self);
        } else {
            location.reload();
        }
    },
    //回车事件
    EventToEnter: function (sender,func) {
        if (sender && func) {
            sender.keyup(function () {
                if (event.keyCode == 13) {
                    func();
                }
            })
        }
    },
    //将文本框的值类型转换成Money类型
    InputTypeToMonery: function (sender,isinput) {
        if (sender) {
            if (isinput) {
                if (!$(sender).val() || !$(sender).val() && parseInt($(sender).val()) > 0) {
                    $(sender).val("");
                } else {
                    if (!$(sender).val()) {
                        $(sender).val("0.00")
                    } else {
                        if (parseFloat($(sender).val()) > parseInt($(sender).val())) {
                            if (!(parseFloat($(sender).val()).toString().split('.')[1].length > 1)) {
                                $(sender).val(parseFloat($(sender).val()) + ".0");
                            }
                        } else {
                            $(sender).val(parseInt($(sender).val()) + ".00");
                        }
                    }
                }
            }
        }
    },
    //将数据存储到本地(持久保存,页面关闭数据也不会消失)
    SaveDateLocal: function (key, value, isJson) {
        if (isJson) {
            value = JSON.stringify(value);
        }
        localStorage.setItem(key, value);
        return true;
    },
    //将数据从本地存储获取出来
    GetDateLocal: function (key,isJson) {
        var reulst = localStorage.getItem(key);
        if (reulst == null || reulst == undefined || reulst == "") {
            return "";
        } else {
            if (isJson) {
                reulst = JSON.parse(reulst);
            }
        }
        return reulst;
    },
    //从本地存储的数据移除特定项
    RremoveDateLocal: function (key) {
        var reulst = localStorage.getItem(key);
        if (reulst == null || reulst == undefined) {
            return false;
        } else {
            localStorage.removeItem(key);
            return true;
        }
    },
    //清除本地所有存储的数据
    RremoveAllDateLocal: function () {
        localStorage.clear();
    },
    //将数据存储到本地(临时保存,页面关闭或刷新,数据也会消失)
    SaveDateSession: function (key, value, isJson) {
        if (isJson) {
            value = JSON.stringify(value);
        }
        sessionStorage.setItem(key, value);
        return true;
    },
    //将数据从本地存储获取出来
    GetDateSession: function (key, isJson) {
        var reulst = sessionStorage.getItem(key);
        if (reulst == null || reulst == undefined || reulst == "") {
            return "";
        } else {
            if (isJson) {
                reulst = JSON.parse(reulst);
            }
        }
        return reulst;
    },
    //从本地存储的数据移除特定项
    RremoveDateSession: function (key) {
        var reulst = sessionStorage.getItem(key);
        if (reulst == null || reulst == undefined) {
            return false;
        } else {
            sessionStorage.removeItem(key);
            return true;
        }
    },
    //清除本地所有存储的数据
    RremoveAllDateSession: function () {
        sessionStorage.clear();
    },
    //生成随机验证码 len:长度自定义
    ResString: function (len) {
        var jscharens = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        var res = "";
        for (var i = 0; i < len ; i++) {
            var id = Math.ceil(Math.random() * 36);
            res += jscharens[id].toUpperCase();
        }
        return res;
    },
    //用于时间格式拼接
    DateUpNow: function (date) {
        return date < 10 ? '0' + date : date;
    }
}

    