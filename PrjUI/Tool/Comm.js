
////公用脚本对象
var Comm = {
    //post提交
    AjaxGet: function (Url, JsonData, SuccessFun, ErrFun, preloader) {
        return Comm.Ajax(Url, "Get", JsonData, SuccessFun, ErrFun, preloader)
    },
    //post提交
    AjaxPost: function (Url, JsonData, SuccessFun, ErrFun, preloader) {
        return Comm.Ajax(Url, "post", JsonData, SuccessFun, ErrFun, preloader)
    },
    //ajax请求
    Ajax: function (Url, type, JsonData, SuccessFun, ErrFun, preloader) {
        return $.ajax(Comm.Ajaxoptbind({
            type: type,
            url: Url,
            data: JsonData,
            dataType: 'json',
            async: 'false',
            preloader: preloader,
            SuccessFun: SuccessFun,
            ErrFun: ErrFun
        })
        );
    },
    //ajax请求参数绑定
    Ajaxoptbind: function (opt) {
        opt.beforeSend = function () {
            //请求前处理比如：显示加载中。。
            if (this.preloader) {
                demo.init();
            }
        };
        opt.success = function (rdata) {

            //这里对返回信息的处理
            if (rdata.ResponseCode != '0') {
                if (this.ErrFun) {
                    this.ErrFun(rdata.ResponseContent);
                } else {
                    Comm.Alert(rdata.ResponseContent, '提示', '顶部全宽', '失败');
                }
            } else {
                this.SuccessFun(rdata.ResponseContent);
            }
            if (this.preloader) {
                demo.remove();
            }
        };
        opt.complete = function () {
            //请求成功或失败之后的处理
            if (this.preloader) {
                demo.remove();
            }
        };
        opt.error = function (XMLHttpRequest, textStatus, errorThrown) {
            if (this.preloader) {
                demo.remove();
            }
            Comm.Alert('数据请求失败！', '提示', '顶部全宽', '失败');
        };
        return opt;
    },
    //弹出提示
    Alert: function (msg, title, positionName, shortCutFunction, onclick) {
        if (positionName == undefined) {
            positionName = '右上';
        }
        if (shortCutFunction == undefined) {
            shortCutFunction = '成功';
        }
        var positionClassArr = { "右上": "toast-top-right", "右下": "toast-bottom-right", "左下": "toast-bottom-left", "左上": "toast-top-left", "顶部全宽": "toast-top-full-width", "底部全宽": "toast-bottom-full-width", "顶部居中": "toast-top-center", "底部居中": "toast-bottom-center", }
        var shortCutFunctionArr = { "成功": "success", "信息": "info", "警告": "warning", "失败": "error", };

        toastr.options = {
            closeButton: true,//关闭按钮
            debug: false,//Debug
            progressBar: true,//进度条
            positionClass: positionClassArr[positionName],//显示位置和样式
            showDuration: "1000",//显示持续时间
            hideDuration: "1000",//隐藏持续时间
            timeOut: "7000",//超时
            extendedTimeOut: "1000",//延长时间
            showEasing: "swing",//显示动画  swing, linear
            hideEasing: "linear",//隐藏动画 swing, linear
            showMethod: "fadeIn",//显示方法 show, fadeIn, slideDown
            hideMethod: "fadeOut",//隐藏方法 show, fadeIn, slideDown
            onclick: onclick//点击事件
        };
        var $toast = toastr[shortCutFunctionArr[shortCutFunction]](msg, title);
    },
    //清除弹出提示
    //clearAlert: function (msg) {
    //    if (msg=='所有') {
    //        oastr.clear();
    //    } else {
    //        toastr.clear(getLastToast());
    //    }
    //},
    //序列化表单对象
    serializestr: function (id) {
        return Comm.serializejson(Comm.serializeform(id));
    },
    //序列化表单
    serializeform: function (id) {
        var d = {};
        $($(id).serializeArray()).each(function (i, item) { var itemvalue = d[item.name]; if (itemvalue) d[item.name] = itemvalue + ',' + item.value; else d[item.name] = item.value; });
        return d;
    },
    //序列化json对象
    serializejson: function (j) {
        return JSON.stringify(j);
    },
    //解析JSON格式的字符串
    parsejson: function (j) {
        return JSON.parse(j);
    },
    //设置表单数据
    setformdata: function (id, data) {
        var curform = $(id);
        if (data) {
            $.each(data, function (name, value) {
                var formobjs = curform.find('[name="' + name + '"]');
                var type = $(formobjs).attr("type");
                if (type == "checkbox") formobjs.filter('[value="' + value + '"]').prop("checked", "checked").siblings().removeProp("checked");
                else if (type == "radio") formobjs.filter('[value="' + value + '"]').prop("checked", "checked");
                else if ($('[name="' + name + '"]').length > 0 && $('[name="' + name + '"]')[0].tagName == "LABEL") formobjs.text(value);
                //else if (formobjs[0] != null) { if (formobjs[0].type == 'select-one' || formobjs[0].type == 'select-multiple') $(formobjs).trigger("chosen:updated"); }
                else formobjs.val(value);
            });
        }
    },
    //清除集合数据
    ClearList: function (list) {
        var data  = list;
        for (var i = 0; i < data.length; i++) {
            data[i] = "";
        }
        return data;
    },
    //hours为空字符串时,cookie的生存期至浏览器会话结束。  
    //hours为数字0时,建立的是一个失效的cookie,  
    //这个cookie会覆盖已经建立过的同名、同path的cookie（如果这个cookie存在）。    
    setCookie: function (name, value, hours, path) {
        var name = escape(name);
        var value = escape(value);
        var expires = new Date();
        expires.setTime(expires.getTime() + hours * 3600000);
        path = path == "" ? "" : ";path=" + path;
        _expires = (typeof hours) == "string" ? "" : ";expires=" + expires.toUTCString();
        document.cookie = name + "=" + value + _expires + path;
    },
    //cookie名获取值
    getCookieValue: function (name) {
        var name = escape(name);
        //读cookie属性，这将返回文档的所有cookie     
        var allcookies = document.cookie;
        //查找名为name的cookie的开始位置     
        name += "=";
        var pos = allcookies.indexOf(name);
        //如果找到了具有该名字的cookie，那么提取并使用它的值     
        if (pos != -1) {    //如果pos值为-1则说明搜索"version="失败     
            var start = pos + name.length;   //cookie值开始的位置     
            var end = allcookies.indexOf(";", start); //从cookie值开始的位置起搜索第一个";"的位置,即cookie值结尾的位置     
            if (end == -1) end = allcookies.length; //如果end值为-1说明cookie列表里只有一个cookie     
            var value = allcookies.substring(start, end);  //提取cookie的值     
            return unescape(value);       //对它解码           
        }
        else return "-1";    //搜索失败，返回-1  
    },
    //通用绑定下拉框
    BindSelect: function (ids, url, data, isAll, BindFun) {
        var Array = ids.split(",");
        Comm.AjaxPost(url, data,
            function (data) {
                var josn = JSON.parse(data);
                $.each(Array, function (i, id) {
                    if (isAll) $(id).append("<option value=''>全部</option>");
                    $.each(josn, function (i, item) {
                        $(id).append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                    });
                })
                BindFun();
            });
    },
    //滚动条设置
    SlimScroll: function (ids) {
        $.each(ids, function (i, id) {
            $(id).slimScroll({
                height: '100%',//可滚动区域高度
                railOpacity: 0.4,//轨道透明度
                wheelStep: 10,//滚轮滚动量
                disableFadeOut: false,//是否 鼠标经过可滚动区域时显示组件，离开时隐藏组件
                alwaysVisible: false, //是否 始终显示组件
            });
        })
    },
    validateForm: function (vue, from) {
        var type = typeof (from);
        if (type == "function") {
            vue.$refs['rfData'].validate(function (valid) {
                if (valid) {
                    from();
                } else {
                    //vue.$Message.error('信息填写错误!');
                    Comm.Alert('错误信息！', '提示', '顶部全宽', '信息填写错误');
                }
            })
        }
    },
    //日期格式转换
    TimeStampFormat: function (value) {
        var date = new Date(parseInt(value));
        var Y = date.getFullYear() + '-';
        var M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
        var D = date.getDate() + ' ';
        //var h = date.getHours() + ':';
        //var m = date.getMinutes() + ':';
        //var s = date.getSeconds();
        return Y + M + D; 
    },
    //重构树形数据结构
    JsontoTree: function (jsondata, pid, id) {
        var Sonjsondata = [];
        var Fatjsondata = [];
        var zi = [];
        var Tempjsondata = [];
        for (var i = 0; i < jsondata.length; i++) {
            var datatestjson = jsondata[i];
            if (datatestjson[pid] != "0") {
                Sonjsondata.push(datatestjson);
            } else {
                Fatjsondata.push(datatestjson);
            }
        }
        for (var i = 0; i < Fatjsondata.length; i++) {
            zi = [];
            var data1 = Fatjsondata[i];
            for (var j = 0; j < Sonjsondata.length; j++) {
                var data2 = Sonjsondata[j];
                if (data1[id] == data2[pid]) {
                    zi.push(data2);
                    data1["children"] = zi;
                }
            }
            Tempjsondata.push(data1);
        }
        return Tempjsondata;
    },
    //重构3级树形数据结构(来源2个数据)
    JsontoTreePlus: function (prtjsondata, jsondata,checkeddata,ckid, prtid, pid, id) {
        var Tempjsondata = [];//最终返回数据结构
        var Tempjsondataone = []; //第一次重组从最小的子节点开始
        var Sonjsondata = [];//子节点数据
        var Fatjsondata = [];//父节点数据
        var prtzi = [];
        var zi = [];
        //将数据分成子数据和父数据
        for (var i = 0; i < jsondata.length; i++) {
            var datatestjson = jsondata[i];
            if (datatestjson[pid] != "") {
                Sonjsondata.push(datatestjson);
            } else {
                Fatjsondata.push(datatestjson);
            }
        }

        //数据勾选/反勾选
        //第3级数据
        for (var j = 0; j < checkeddata.length; j++) {
            var checkedjson = checkeddata[j];
            for (var i = 0; i < Sonjsondata.length; i++) {
                var checkeddatatestjson = Sonjsondata[i];
                if (checkedjson[ckid] == checkeddatatestjson[id]) {
                    checkeddatatestjson["checked"] = true;
                }
            }
        }
        //第2级数据
        for (var j = 0; j < checkeddata.length; j++) {
            var checkedjson = checkeddata[j];
            for (var i = 0; i < Fatjsondata.length; i++) {
                var checkeddatatestjson = Fatjsondata[i];
                if (checkedjson[ckid] == checkeddatatestjson[id]) {
                    checkeddatatestjson["checked"] = true;
                }
            }
        }

        //第1级数据
        for (var j = 0; j < checkeddata.length; j++) {
            var checkedjson = checkeddata[j];
            for (var i = 0; i < prtjsondata.length; i++) {
                var checkeddatatestjson = prtjsondata[i];
                if (checkedjson[ckid] == checkeddatatestjson[id]) {
                    checkeddatatestjson["checked"] = true;
                }
            }
        }
        //数据勾选/反勾选结束
        
        //将模块表的数据重组成2级树状结构
        for (var i = 0; i < Fatjsondata.length; i++) {
            zi = [];
            var data1 = Fatjsondata[i];
            data1["title"] = data1.Name;
            for (var j = 0; j < Sonjsondata.length; j++) {
                var data2 = Sonjsondata[j];
                if (data1[id] == data2[pid]) {
                    data2["title"] = data2.Name;
                    zi.push(data2);
                    data1["children"] = zi;
                }
            }
            Tempjsondataone.push(data1);
        }
        //将平台系统的数据和重组过后的数据重新组合成3级树状结构
        for (var i = 0; i < prtjsondata.length; i++) {
            var data = prtjsondata[i];
            data["title"] = data.ZName;
            data["id"] = data.ID;
            prtzi = [];
            for (var j = 0; j < Tempjsondataone.length; j++) {
                var data1 = Tempjsondataone[j];
                if (data.ID == data1[prtid]) {
                    data1["title"] = data1["Name"];
                    prtzi.push(data1);
                    data["children"] = prtzi;
                }
            }
            Tempjsondata.push(data);
        }
        
        return Tempjsondata;
    }
};
//加载中属性 初始化
const SETTINGS = {
    rebound: {
        tension: 14,
        friction: 10
    },
    spinner: {
        id: 'spinner',
        radius: 90,
        sides: 5,
        depth: 8,
        colors: {
            background: 'agba(61,170,233,0.7)',
            stroke: null,
            base: null,
            child: '#02C39A'
        },
        alwaysForward: true, // When false the spring will reverse normally.
        restAt: null, // A number from 0.1 to 0.9 || null for full rotation
        renderBase: false
    }
};

