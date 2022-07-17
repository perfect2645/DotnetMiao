(function(g) {
    var ns = function(s) {
        if (s === "") {
            return g
        }
        var pkgs = s.split(".");
        var obj = g;
        for (var i = 0; i < pkgs.length; i++) {
            var p = pkgs[i];
            if (!obj[p]) {
                obj[p] = {}
            } else {
                if (obj[p].$isClass === true) {
                    throw new Error("package name[" + p + "] is defined class.")
                }
            }
            obj = obj[p]
        }
        return obj
    };
    g.$package = ns;
    var findClass = function(s) {
        var pkgs = s.split(".");
        var obj = g, p;
        for (var i = 0, n = pkgs.length; i < n; i++) {
            p = pkgs[i];
            if (obj[p]) {
                obj = obj[p]
            } else {
                return null
            }
        }
        if (is.Class(obj)) {
            return obj
        }
        return null
    };
    var removeClass = function(className) {
        var pkgs = className.split(".");
        var obj = g, p;
        for (var i = 0, n = pkgs.length; i < n; i++) {
            p = pkgs[i];
            if (obj[p]) {
                if (i == n - 1 && is.Class(obj[p])) {
                    delete obj[p];
                    return
                } else {
                    obj = obj[p]
                }
            }
        }
    };
    g.$findClass = findClass;
    g.$removeClass = removeClass;
    var emptyFunction = function() {};
    g.$emptyFunction = emptyFunction;
    g.$AppContext = {};
    var serverContextUrl = "";
    var htmlHead = document.getElementsByTagName("head")[0] || document.documentElement;
    var getUrlParam = function(name, targetUrl) {
        var url = targetUrl || window.location.href
          , results = new RegExp("[?&]" + name + "=([^&#]*)").exec(url);
        if (results == null) {
            return null
        } else {
            return decodeURI(results[1]) || 0
        }
    };
    var getUrlParams = function(curUrl) {
        var params = {};
        var urls = (curUrl || window.location.href).split("?");
        if (urls && urls.length > 1) {
            var arr = urls[1].split("&");
            for (var i = 0, l = arr.length; i < l; i++) {
                var a = arr[i].split("=");
                params[a[0]] = decodeURIComponent(a[1])
            }
        }
        return params
    };
    var paramsToUrl = function(param, key, encode) {
        if (param == null)
            return "";
        var paramStr = "";
        var t = typeof param;
        if (t == "string" || t == "number" || t == "boolean") {
            paramStr += key + "=" + (encode == null || encode ? encodeURIComponent(param) : param)
        } else {
            var index = 0;
            for (var i in param) {
                var k = key == null ? i : key + (param instanceof Array ? "[" + i + "]" : "." + i);
                if (index == 0) {
                    paramStr += paramsToUrl(param[i], k, encode)
                } else {
                    paramStr += "&" + paramsToUrl(param[i], k, encode)
                }
                index++
            }
        }
        return paramStr
    };
    var env = {
        nativeApi: {},
        getUrlParam: getUrlParam,
        getUrlParams: getUrlParams,
        paramsToUrl: paramsToUrl
    };
    window.onerror = function(errorMessage, scriptURI, lineNumber, columnNumber, errorObj) {
        var errTxt = "报错内容:" + errorMessage + "\n 报错js文件:" + scriptURI + "\n 报错行列数:" + lineNumber + "/" + columnNumber;
        window.Logger && Logger.error(errTxt)
    }
    ;
    window.addEventListener("error", function(e) {
        var domTarget = e.target || e.srcElement;
        var hasLinkDom = domTarget instanceof HTMLScriptElement || domTarget instanceof HTMLLinkElement || domTarget instanceof HTMLImageElement;
        if (hasLinkDom) {
            window.Logger && Logger.warn("资源加载错误：" + domTarget.src || domTarget.href)
        } else {
            window.Logger && Logger.error("资源报错", e)
        }
    }, true);
    window.addEventListener("unhandledrejection", function(e) {
        console.log("promise捕获到异常： ", e);
        window.Logger && Logger.error("promise捕获到异常： ", e)
    });
    var appRelativePath = function() {
        var pathname = window.location.pathname;
        var relativePath = "";
        var fds = pathname.split("/");
        var path = "";
        for (var i = 0; i < fds.length; i++) {
            path = fds[i];
            if (path.indexOf("html") >= 0) {
                continue
            }
            relativePath += "/" + path
        }
        return relativePath
    }();
    env.appAbsolutePath = "";
    env.resourcesHome = "";
    env.appResourcesUrl = "";
    env.cdnUrl = function(url) {
        if (url.indexOf("http") >= 0)
            return url;
        return env.appResourcesUrl + url
    }
    ;
    env.setConsultPageId = function(pageId) {
        if (pageId) {
            var days = 3;
            var exp = new Date;
            exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1e3);
            env.setCookie("_consultPageId", pageId, exp)
        }
    }
    ;
    env.getConsultPageId = function() {
        try {
            var _consultPageId = $env.getUrlParam("consultPageId");
            if (_consultPageId) {
                return _consultPageId
            } else {
                return env.getCookie("_consultPageId")
            }
        } catch (error) {
            console.log(error)
        }
    }
    ;
    env.setBodycheckExplain = function(tag) {
        if (tag) {
            var exp = new Date;
            exp.setTime(exp.getTime() + 6 * 60 * 60 * 1e3);
            env.setCookie("_BodycheckExplain", tag, exp)
        }
    }
    ;
    env.getBodycheckExplain = function() {
        return env.getCookie("_BodycheckExplain")
    }
    ;
    env.getServerContextUrl = function() {
        return serverContextUrl
    }
    ;
    env.setServerContextUrl = function(e) {
        if (!$env.hybrid && !env.wxDebug) {
            e = e.replace("https:", location.protocol)
        }
        env.appRootOffsetPath = serverContextUrl = e
    }
    ;
    env.setCDNStaticServerUrl = function(url) {
        if (!url || url == "") {
            url = serverContextUrl
        }
        if (!env.wxDebug && !$env.hybrid) {
            url = url.replace("https:", location.protocol);
            env.appResourcesUrl = url;
            env.resourcesHome = url
        }
    }
    ;
    env.fileVersion = "";
    env.setVersion = function(fileVersion) {
        env.fileVersion = fileVersion
    }
    ;
    env.appRootOffsetPath = env.getServerContextUrl();
    var API_TOKEN = "api_token";
    var API_REQUIRE = "api_require";
    env._cookie = "";
    env.callNativeMethod = function(methodName, args) {
        if (env.isIOS) {
            console.log("callNativeMethod");
            window.webkit.messageHandlers[methodName].postMessage(args || null);
            return
        }
        if (env.platform.name == "Android") {
            if (window.Android && window.Android[methodName] != null) {
                args ? window.Android[methodName](args) : window.Android[methodName]()
            } else if (window[methodName] != null && window[methodName].postMessage != null) {
                window[methodName].postMessage(args)
            } else {
                console.error("没有注册native方法:" + methodName)
            }
        }
    }
    ;
    g.RequireResourceLoaded = function(rs) {
        is.Object(rs) && (/.*\.js$/.test(rs.cls) && eval(rs.content),
        globalEmitter.emit("require." + rs.cls, rs.content))
    }
    ;
    g.RequireToken = function(e) {
        is.Object(e) && globalEmitter.emit("require.token", e)
    }
    ;
    g.AppCallH5Auth = function(e) {
        $sys.showLoading();
        $env.nativeApi.requireToken().then(function(res) {
            $sys.token().then(function(json) {
                $AppContext.urt = json.body;
                $sys.hideLoading()
            }).fail(function(e) {
                console.log(e);
                $sys.hideLoading()
            })
        }).fail(function(e) {
            console.log(e);
            $sys.hideLoading()
        })
    }
    ;
    env.nativeApi.goToHealthNavLogin = function() {
        var e = new $Defer;
        console.log("跳到健康导航APP登录注册页");
        return globalEmitter.on("unLoginShouldGotoLogin", function(r) {}, this, !0),
        env.callNativeMethod.apply(this, ["unLoginShouldGotoLogin"]),
        e.promise
    }
    ;
    env.nativeApi.requireToken = function() {
        var e = new $Defer;
        return globalEmitter.on("require.token", function(r) {
            if (r.token) {
                window.document.cookie = "tk=" + r.token + ";"
            }
            env.currentToken = r,
            r.serverUrl && (serverContextUrl = r.serverUrl),
            r.api && (env.api = r.api),
            e.resolve(r)
        }, this, !0),
        env.callNativeMethod.apply(this, [API_TOKEN]),
        e.promise
    }
    ;
    env.nativeApi.requireResource = function(e, r) {
        var n = new $Defer;
        return globalEmitter.on("require." + e, function(e) {
            "error" == e ? n.reject("error") : n.resolve(e)
        }, this, !0),
        env.callNativeMethod.apply(this, [API_REQUIRE, e]),
        n.promise
    }
    ;
    var defaultExpiresDate = new Date((new Date).getTime() + 365 * 24 * 60 * 60 * 1e3).toGMTString();
    var getCookieVal = function(offset) {
        var cookie = $env.hybrid ? env._cookie : document.cookie;
        var endstr = cookie.indexOf(";", offset);
        if (endstr == -1) {
            endstr = cookie.length
        }
        return decodeURIComponent(cookie.substring(offset, endstr))
    };
    env.setCookie = function(name, value) {
        var argv = arguments
          , argc = arguments.length
          , expires = argc > 2 ? argv[2] : defaultExpiresDate
          , path = argc > 3 ? argv[3] : "/"
          , domain = argc > 4 ? argv[4] : null
          , secure = argc > 5 ? argv[5] : false;
        var cookie = name + "=" + encodeURIComponent(value) + (expires === null ? defaultExpiresDate : "; expires=" + expires) + (path === null ? "" : "; path=" + path) + (domain === null ? "" : "; domain=" + domain) + (secure === true ? "; secure" : "");
        if ($env.hybrid) {
            env._cookie += "; " + cookie
        } else {
            document.cookie = cookie
        }
    }
    ;
    env.setCookieZLB = function(name, value) {
        var argv = arguments
          , argc = arguments.length
          , expires = argc > 2 ? argv[2] !== "" ? argv[2] : defaultExpiresDate : defaultExpiresDate
          , path = argc > 3 ? argv[3] : "/"
          , domain = argc > 4 ? argv[4] : null
          , secure = argc > 5 ? argv[5] : false;
        var cookie = name + "=" + encodeURIComponent(value) + (expires === null ? defaultExpiresDate : "; expires=" + expires) + (path === null ? "" : "; path=" + path) + (domain === null ? "" : "; domain=" + domain) + (secure === true ? "; secure" : "");
        env._cookie += "; " + cookie
    }
    ;
    env.setCookies = function(data, path) {
        if (env.hybrid) {
            env._cookie += "; " + data
        } else if (is.Object(data)) {
            var nm;
            for (nm in data) {
                if (data.hasOwnProperty(nm))
                    env.setCookie(nm, data[nm])
            }
        }
    }
    ;
    env.setCookiesEx = function(data) {
        if (env.hybrid) {
            env._cookie += "; " + data
        } else if (is.Object(data)) {
            var nm;
            for (nm in data) {
                if (data.hasOwnProperty(nm))
                    env.setCookieEx(nm, data[nm])
            }
        }
    }
    ;
    env.setCookieEx = function(name, value) {
        var argv = arguments
          , argc = arguments.length
          , expires = argc > 2 ? argv[2] : defaultExpiresDate
          , path = argc > 3 ? argv[3] : "/"
          , domain = argc > 4 ? argv[4] : null
          , secure = argc > 5 ? argv[5] : false;
        var cookie = name + "=" + value + (expires === null ? defaultExpiresDate : "; expires=" + expires) + (path === null ? "" : "; path=" + path) + (domain === null ? "" : "; domain=" + domain) + (secure === true ? "; secure" : "");
        if ($env.hybrid) {
            env._cookie += "; " + cookie
        } else {
            document.cookie = cookie
        }
    }
    ;
    env.setCookiesZLB = function(data) {
        var nm;
        for (nm in data) {
            if (data.hasOwnProperty(nm))
                env.setCookieZLB(nm, data[nm])
        }
    }
    ;
    var getCookie = function(name) {
        try {
            var cookie = $env.hybrid ? env._cookie : document.cookie;
            if ($sys.isWeixin() && name == "WXApp" && env.isWeixinCookieOpen) {
                console.log("cookie open");
                return $encode(decodeURIComponent(window.WXApp))
            }
            var arg = name + "="
              , alen = arg.length
              , clen = cookie.length
              , i = 0
              , j = 0;
            while (i < clen) {
                j = i + alen;
                if (cookie.substring(i, j) == arg) {
                    return getCookieVal(j)
                }
                i = cookie.indexOf(" ", i) + 1;
                if (i === 0) {
                    break
                }
            }
        } catch (error) {
            console.error(error)
        }
        return null
    };
    var getCookieHybrid = function(e) {
        for (var r = e + "=", n = r.length, t = env._cookie.length, o = 0, s = 0; o < t; ) {
            if (s = o + n,
            env._cookie.substring(o, s) == r) {
                var i = env._cookie.indexOf(";", s);
                return i == -1 && (i = env._cookie.length),
                decodeURI(env._cookie.substring(s, i))
            }
            if (o = env._cookie.indexOf(" ", o) + 1,
            0 === o)
                break
        }
        return null
    };
    var getCookieZlb = function(e) {
        for (var r = e + "=", n = r.length, t = env._cookie.length, o = 0, s = 0; o < t; ) {
            if (s = o + n,
            env._cookie.substring(o, s) == r) {
                var i = env._cookie.indexOf(";", s);
                return i == -1 && (i = env._cookie.length),
                decodeURIComponent(env._cookie.substring(s, i))
            }
            if (o = env._cookie.indexOf(" ", o) + 1,
            0 === o)
                break
        }
        return null
    };
    env.getCookie = function(name) {
        if (env.checkIOSZlb()) {
            return getCookieZlb(name)
        } else {
            return env.hybrid ? getCookieHybrid(name) : getCookie(name)
        }
    }
    ;
    env.clearCookie = function(name, path) {
        if (is.Array(name)) {
            for (var i = 0; i < name.length; i++) {
                env.clearCookie(name[i], path)
            }
            return
        }
        if (env.getCookie(name)) {
            path = env.wxDebug ? "/" : path || "/";
            var cookie = name + "=" + "; expires=Thu, 01-Jan-70 00:00:01 GMT; path=" + path;
            if ($env.hybrid) {
                env._cookie = cookie
            } else {
                document.cookie = cookie
            }
        }
    }
    ;
    env.clearCookieEx = function(name, path) {
        if (is.Array(name)) {
            for (var i = 0; i < name.length; i++) {
                env.clearCookieEx(name[i], path)
            }
            return
        }
        path = env.wxDebug ? "/" : path || "/";
        var cookie = name + "=" + "; expires=Thu, 01-Jan-70 00:00:01 GMT; path=" + path;
        if ($env.hybrid) {
            env._cookie = cookie
        } else {
            document.cookie = cookie
        }
    }
    ;
    (function() {
        var ua = navigator.userAgent;
        var names = {
            ios: "iOS",
            android: "Android",
            windowsPhone: "WindowsPhone",
            webos: "webOS",
            blackberry: "BlackBerry",
            rimTablet: "RIMTablet",
            chrome: "ChromeOS",
            mac: "MacOS",
            win: "Windows",
            linux: "Linux",
            unknow: "Unknow"
        };
        var prefixes = {
            ios: "i(?:Pad|Phone|Pod)(?:.*)CPU(?: iPhone)? OS ",
            android: "(Android |HTC_|Silk/)",
            windowsPhone: "Windows Phone ",
            blackberry: "(?:BlackBerry|BB)(?:.*)Version/",
            rimTablet: "RIM Tablet OS ",
            webos: "(?:webOS|hpwOS)/",
            bada: "Bada/",
            chrome: "CrOS "
        };
        var i, prefix, m, m1, name, version;
        for (i in prefixes) {
            if (prefixes.hasOwnProperty(i)) {
                prefix = prefixes[i];
                m = ua.match(new RegExp("(?:" + prefix + ")([^\\s;]+)"));
                if (m) {
                    name = names[i];
                    m1 = m[1];
                    if (m1 === "HTC_" || m1 === "Silk/") {
                        version = "2.3"
                    } else {
                        version = m[m.length - 1]
                    }
                    break
                }
            }
        }
        if (!name) {
            name = names[(ua.toLocaleLowerCase().match(/mac|win|linux/) || ["unknow"])[0]];
            switch (name) {
            case "MacOS":
                env.isMac = true;
                m = ua.match(/OS X (\d+)_(\d+)/);
                if (m) {
                    version = m[1] + "." + m[2]
                }
                break;
            case "Windows":
                env.isWin = true;
                m = ua.match(/NT ([\\.\d]+)/);
                if (m) {
                    switch (m[1]) {
                    case "5.1":
                        version = "XP";
                        break;
                    case "6.0":
                        version = "Vista";
                        break;
                    case "6.1":
                        version = "7";
                        break;
                    case "6.2":
                        version = "8";
                        break;
                    case "6.3":
                        version = "8.1";
                        break;
                    case "10.0":
                        version = "10";
                        break
                    }
                }
                break
            }
        }
        env.platform = {
            name: name,
            version: version
        }
    }
    )();
    env.checkIOSZlb = function() {
        var zlbFlag = env.getUrlParam("appkey") && (env.getUrlParam("appkey").indexOf("zlb-") > -1 || env.getUrlParam("appkey").indexOf("zlbJT-") > -1) || env.getUrlParam("appkey") == "zjzwfw";
        return env.platform.name == "iOS" && zlbFlag
    }
    ;
    var arraySlice = Array.prototype.slice;
    var globalEvents = {};
    var globalEmitter = {
        on: function(ename, fn, scope, once) {
            if (!is.Function(fn)) {
                return
            }
            var listeners = globalEvents[ename];
            if (!listeners) {
                listeners = [];
                globalEvents[ename] = listeners
            }
            if (listeners.length) {
                for (var i = 0; i < listeners.length; i++) {
                    var l = listeners[i];
                    if (l.fn === fn && (!scope || l.scope == scope)) {
                        return
                    }
                }
            }
            listeners.push({
                fn: fn,
                scope: scope || this,
                once: once
            })
        },
        un: function(ename, fn, scope) {
            var listeners = globalEvents[ename];
            if (fn) {
                if (listeners && listeners.length) {
                    for (var i = 0; i < listeners.length; i++) {
                        var l = listeners[i];
                        if (l.fn === fn && (!scope || l.scope == scope)) {
                            listeners.splice(i, 1);
                            return
                        }
                    }
                }
            } else {
                delete globalEvents[ename]
            }
        },
        fireEvent: function() {
            if (!arguments.length) {
                return
            }
            var ename = arguments[0];
            var listeners = globalEvents[ename];
            if (listeners) {
                for (var i = 0; i < listeners.length; i++) {
                    var listener = listeners[i];
                    var fn = listener.fn;
                    var scope = listener.scope;
                    fn.apply(scope, arraySlice.call(arguments, 1));
                    if (listener.once === true) {
                        listeners.splice(i, 1);
                        i--
                    }
                }
            }
        }
    };
    globalEmitter.addListener = globalEmitter.on;
    globalEmitter.removeListener = globalEmitter.un;
    globalEmitter.off = globalEmitter.un;
    globalEmitter.emit = globalEmitter.fireEvent;
    g.$emitter = globalEmitter;
    (function() {
        var ua = navigator.userAgent.toLowerCase();
        var check = function(r) {
            return r.test(ua)
        };
        var version = function(is, regex) {
            var m;
            return is && (m = regex.exec(ua)) ? parseFloat(m[1]) : 0
        };
        var isOpera = check(/opera/);
        var isWebKit = check(/webkit/);
        var isChrome = check(/\bchrome\b/);
        var isSafari = !isChrome && check(/safari/);
        var isIE = !isOpera && check(/msie/);
        var isIE6 = isIE && check(/msie 6/);
        var isIE11 = check(/like gecko/) && !isWebKit;
        var isGecko = !isWebKit && check(/gecko/);
        var isFirefox = check(/\bfirefox/);
        var isNodeWebkit = g.process && process.versions && process.versions["node-webkit"];
        env.isWebKit = isWebKit;
        env.isOpera = isOpera;
        env.isChrome = isChrome;
        env.isSafari = isSafari;
        env.isIE = isIE || isIE11;
        env.isIE6 = isIE6;
        env.isGecko = isGecko;
        env.isFirefox = isFirefox;
        env.isNodeWebkit = isNodeWebkit;
        env.isWeixin = check(/micromessenger/);
        env.isAlipay = check(/alipayclient/);
        var chromeVersion = version(isChrome, /\bchrome\/(\d+\.\d+)/);
        var firefoxVersion = version(isFirefox, /\bfirefox\/(\d+\.\d+)/);
        var ieVersion = version(isIE & !isIE11, /msie (\d+\.\d+)/);
        var ie11Version = version(isIE11, /rv:([\d.]+)\) like gecko/);
        var safariVersion = version(isSafari, /version\/(\d+\.\d+)/);
        env.chromeVersion = chromeVersion;
        env.firefoxVersion = firefoxVersion;
        env.ieVersion = ieVersion || ie11Version;
        env.safariVersion = safariVersion;
        env.originalHeight = document.documentElement.clientHeight || document.body.clientHeight;
        env.isWeixinCookieOpen = true
    }
    )();
    g.$env = env;
    if (typeof console == "undefined") {
        g.console = {
            log: emptyFunction,
            debug: emptyFunction,
            info: emptyFunction,
            warn: emptyFunction,
            error: emptyFunction,
            trace: emptyFunction
        }
    }
    if (!Function.prototype.bind) {
        Function.prototype.bind = function(o) {
            if (!is.Function(this)) {
                throw new TypeError("Function.prototype.bind - arg must be function")
            }
            var args = Array.prototype.slice.call(arguments, 1)
              , fToBind = this
              , fNOP = function() {}
              , fBound = function() {
                return fToBind.apply(this instanceof fNOP && o ? this : o, args.concat(Array.prototype.slice.call(arguments)))
            };
            fNOP.prototype = this.prototype;
            fBound.prototype = new fNOP;
            return fBound
        }
    }
    g.$sys = {};
    var types = ["Array", "Boolean", "Date", "Number", "Object", "Function", "RegExp", "String", "Window", "HTMLDocument", "Undefined"];
    var is = {};
    var obj2Str = Object.prototype.toString;
    for (var i = 0; i < types.length; i++) {
        var t = types[i];
        is[t] = function(tp) {
            return function(obj) {
                return obj2Str.call(obj) == "[object " + tp + "]"
            }
        }(t)
    }
    is.Class = function(obj) {
        return is.Function(obj) && obj.$isClass === true
    }
    ;
    g.$is = is;
    var Defer;
    var when = {};
    if (is.Function(window.Promise)) {
        Promise.prototype.fail = Promise.prototype["catch"];
        Promise.prototype.success = Promise.prototype.then;
        Defer = function() {
            var me = this;
            me.promise = new Promise(function(resolve, reject) {
                me.resolve = resolve;
                me.reject = reject
            }
            )
        }
        ;
        when.all = function(tasks) {
            return Promise.all(tasks)
        }
        ;
        when.any = function(tasks) {
            return Promise.race(tasks)
        }
        ;
        g.$when = when
    } else {
        (function() {
            var STATES = {
                pending: void 0,
                resolved: 1,
                rejected: 2
            };
            var thenable = function(o) {
                return is.Object(o) && is.Function(o.then)
            };
            var Promise = function() {
                this.resolves = [];
                this.rejects = [];
                this.state = STATES.pending;
                this.value = void 0
            };
            Promise.prototype = {
                then: function(resolve, reject) {
                    var deferred = new Defer;
                    var resolveWrapper = function(value) {
                        try {
                            var ret = is.Function(resolve) ? resolve(value) : value;
                            if (thenable(ret)) {
                                ret.then(function(newValue) {
                                    deferred.resolve(newValue)
                                }, function(newReason) {
                                    deferred.reject(newReason)
                                })
                            } else {
                                deferred.resolve(ret)
                            }
                        } catch (e) {
                            deferred.reject(e)
                        }
                    };
                    var rejectWrapper = function(reason) {
                        if (is.Function(reject)) {
                            try {
                                reject(reason)
                            } catch (e) {
                                deferred.reject(e)
                            }
                        } else {
                            deferred.reject(reason)
                        }
                    };
                    var me = this;
                    switch (me.state) {
                    case STATES.pending:
                        me.resolves.push(resolveWrapper);
                        me.rejects.push(rejectWrapper);
                        break;
                    case STATES.resolved:
                        setTimeout(function() {
                            resolveWrapper(me.value)
                        }, 0);
                        break;
                    case STATES.rejected:
                        setTimeout(function() {
                            rejectWrapper(me.value)
                        }, 0)
                    }
                    return deferred.promise
                },
                success: function(resolve) {
                    return this.then(resolve, null)
                },
                fail: function(reject) {
                    return this.then(null, reject)
                }
            };
            Defer = function(promise) {
                if (promise) {
                    this.promise = promise
                } else {
                    this.promise = new Promise
                }
            }
            ;
            Defer.prototype = {
                resolve: function(value) {
                    var promise = this.promise;
                    if (promise.state != STATES.pending) {
                        return
                    }
                    promise.state = STATES.resolved;
                    promise.value = value;
                    var timer = setTimeout(function() {
                        var resolves = promise.resolves;
                        for (var i = 0; i < resolves.length; i++) {
                            resolves[i](value)
                        }
                    }, 0)
                },
                reject: function(reason) {
                    var promise = this.promise;
                    if (promise.state != STATES.pending) {
                        return
                    }
                    promise.state = STATES.rejected;
                    promise.value = reason;
                    var timer = setTimeout(function() {
                        var rejects = promise.rejects;
                        for (var i = 0; i < rejects.length; i++) {
                            rejects[i](reason)
                        }
                    }, 0)
                }
            };
            when.all = Promise.prototype.all = function(tasks) {
                var defer = new Defer;
                var results = [];
                var count = 0;
                var n = tasks.length;
                var resolved = 0;
                var nextThen = function(i) {
                    return function(v) {
                        results[i] = v;
                        count++;
                        if (count == n) {
                            defer.resolve(results)
                        }
                    }
                };
                for (var i = 0; i < n; i++) {
                    var p = tasks[i];
                    if (thenable(p)) {
                        p.then(nextThen(i), function(e) {
                            defer.reject(e)
                        })
                    } else {
                        nextThen(i)(p)
                    }
                }
                return defer.promise
            }
            ;
            when.any = function(tasks) {
                var defer = new Defer;
                for (var i = 0; i < tasks.length; i++) {
                    var p = tasks[i];
                    if (thenable(p)) {
                        p.then(function(v) {
                            defer.resolve(v)
                        }, function(e) {
                            defer.reject(e)
                        })
                    } else {
                        defer.resolve(v)
                    }
                }
                return defer.promise
            }
            ;
            g.$when = when
        }
        )()
    }
    g.$Defer = function() {
        return new Defer
    }
    ;
    var isNativeJson = window.JSON && JSON.toString() == "[object JSON]";
    var jsonDecode, jsonEncode;
    if (isNativeJson) {
        jsonDecode = JSON.parse;
        jsonEncode = JSON.stringify
    } else {
        jsonDecode = function(json) {
            return eval("(" + json + ")")
        }
        ;
        jsonEncode = function() {
            var toString = Object.prototype.toString
              , charToReplace = /[\\"\x00-\x1f\x7f-\uffff]/g
              , m = {
                "\b": "\\b",
                "\t": "\\t",
                "\n": "\\n",
                "\f": "\\f",
                "\r": "\\r",
                '"': '\\"',
                "\\": "\\\\",
                "\v": "\\u000b"
            }
              , useHasOwn = !!{}.hasOwnProperty
              , isDate = is.Date
              , isObject = toString.call(null) === "[object Object]" ? function(value) {
                return value !== null && value !== undefined && toString.call(value) === "[object Object]" && value.ownerDocument === undefined
            }
            : is.Object
              , isBoolean = is.Boolean
              , isArray = is.Array
              , encodeString = function(s) {
                return '"' + s.replace(charToReplace, function(a) {
                    var c = m[a];
                    return typeof c === "string" ? c : "\\u" + ("0000" + a.charCodeAt(0).toString(16)).slice(-4)
                }) + '"'
            }
              , pad = function(n) {
                return n < 10 ? "0" + n : n
            }
              , encodeDate = function(o) {
                return '"' + o.getFullYear() + "-" + pad(o.getMonth() + 1) + "-" + pad(o.getDate()) + "T" + pad(o.getHours()) + ":" + pad(o.getMinutes()) + ":" + pad(o.getSeconds()) + '"'
            }
              , encodeArray = function(o) {
                var a = ["[", ""], len = o.length, i;
                for (i = 0; i < len; i += 1) {
                    a.push(doEncode(o[i]), ",")
                }
                a[a.length - 1] = "]";
                return a.join("")
            }
              , encodeObject = function(o) {
                var a = ["{", ""], i;
                for (i in o) {
                    if (!useHasOwn || o.hasOwnProperty(i)) {
                        a.push(doEncode(i), ":", doEncode(o[i]), ",")
                    }
                }
                a[a.length - 1] = "}";
                return a.join("")
            }
              , doEncode = function(o) {
                if (o === null || o === undefined) {
                    return "null"
                } else if (isDate(o)) {
                    return encodeDate(o)
                } else if (typeof o === "string") {
                    return encodeString(o)
                } else if (typeof o === "number") {
                    return isFinite(o) ? String(o) : "null"
                } else if (isBoolean(o)) {
                    return String(o)
                } else if (o.toJSON) {
                    return o.toJSON()
                } else if (isArray(o)) {
                    return encodeArray(o)
                } else if (isObject(o)) {
                    return encodeObject(o)
                } else if (typeof o === "function") {
                    return "null"
                }
                return "undefined"
            };
            return doEncode
        }()
    }
    g.$decode = jsonDecode;
    g.$encode = jsonEncode;
    var _randomKey = 0;
    env.cryptoValue = "vss7db748e839799";
    env.CAKEY = "ngari-wx";
    env.CASECRET = "a9d4eb7841b1ba47";
    var getCryptoValue = function() {
        return CryptoJS.enc.Utf8.parse($env.cryptoValue)
    };
    var encryptData = function(data) {
        data = JSON.stringify(data);
        var cipher = CryptoJS.AES.encrypt(data, getCryptoValue(), {
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7,
            iv: ""
        });
        return cipher.ciphertext.toString(CryptoJS.enc.Base64)
    };
    var dcryptData = function(base64Cipher) {
        var decipher = CryptoJS.AES.decrypt(base64Cipher, getCryptoValue(), {
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7,
            iv: ""
        });
        return CryptoJS.enc.Utf8.stringify(decipher)
    };
    var getUuid = function() {
        if ($env.uuidForAES) {
            return $env.uuidForAES + getTimeStamp() + ++_randomKey
        } else {
            var str = "xxxxxxxxxxxx4xxxyxxxxxxxxxxxxxxx".replace(/[xy]/g, function(c) {
                var r = Math.random() * 16 | 0
                  , v = c == "x" ? r : r & 3 | 8;
                return v.toString(16) + ++_randomKey
            });
            return str.substring(str.length - 45)
        }
    };
    var getSignHeader = function(headers) {
        var textToSign = "";
        var headerArr = ["X-Ca-Key", "X-Ca-Nonce", "X-Ca-Timestamp", "X-Content-MD5", "X-Service-Id", "X-Service-Method"];
        for (var i = 0; i < headerArr.length; i++) {
            var it = headerArr[i];
            var name = it.toLowerCase();
            var value = headers[it];
            textToSign += name + ":" + value + "&"
        }
        textToSign = textToSign.substring(0, textToSign.length - 1);
        var hash = CryptoJS.HmacSHA256(textToSign, $env.CASECRET);
        var signature = hash.toString(CryptoJS.enc.Base64);
        return signature
    };
    var getMd5 = function(data) {
        var md5 = CryptoJS.MD5(data);
        return md5.toString(CryptoJS.enc.Base64)
    };
    var getTimeStamp = function() {
        if ($env.localTimeOffset) {
            return +new Date + $env.localTimeOffset
        } else {
            return +new Date
        }
    };
    var getSecureHeader = function(jsonData) {
        return {
            "X-Ca-Key": $env.CAKEY,
            "X-Ca-Nonce": getUuid(),
            "X-Ca-Timestamp": getTimeStamp(),
            "X-Content-MD5": getMd5(jsonData)
        }
    };
    var APPLICATION_JSON_TYPE = "application/json";
    var TEXT_PLAIN_TYPE = "text/plain;charset=utf-8";
    var JAVASCRIPT_TYPE = "text/javascript";
    var X_CODED_JSON_MESSAGE = "X-Coded-JSON-Message";
    var X_CODED_JSON_CODE = "X-Auth-Failed-Code";
    var CONTENT_TYPE = "Content-Type";
    var CHARSET = "utf-8";
    var isJsonContentType = function(type) {
        if (is.String(type)) {
            return type.substring(0, 16).toLowerCase() == APPLICATION_JSON_TYPE
        }
    };
    var isTextPlainType = function(type) {
        if (is.String(type)) {
            return type.substring(0, 24).toLowerCase() == TEXT_PLAIN_TYPE
        }
    };
    var parseResponse = function(xhr, conf) {
        var result = {
            code: 200,
            msg: "Success"
        };
        var status = xhr.status;
        if (status == 0) {
            result.code = 401;
            result.msg = "ConnectFailed"
        } else if (status < 400) {
            var contentType = xhr.getResponseHeader(CONTENT_TYPE);
            if (isJsonContentType(contentType)) {
                try {
                    var json;
                    if (xhr.responseType == "json") {
                        json = xhr.response
                    } else {
                        json = $decode(xhr.responseText)
                    }
                    var isCodedMessage = xhr.getResponseHeader(X_CODED_JSON_MESSAGE) || json.code;
                    if (isCodedMessage) {
                        apply(result, json)
                    } else {
                        result.body = json
                    }
                } catch (e) {
                    result.code = 509;
                    result.msg = "ParseResponseError"
                }
            } else if (isTextPlainType(contentType)) {
                try {
                    result = $decode(dcryptData(xhr.response))
                } catch (e) {
                    result.code = 509;
                    result.msg = "ParseResponseError"
                }
            } else if (conf.responseType == "blob") {
                result.body = new Blob([xhr.response],{
                    type: contentType
                })
            } else if (conf.responseType == "arraybuffer") {
                result.body = xhr.response
            } else {
                result.body = xhr.responseText
            }
        } else if (status == 403) {
            var isCodedCode = xhr.getResponseHeader(X_CODED_JSON_CODE) || 403;
            if (isCodedCode == 404) {
                isCodedCode = 409
            }
            result.code = isCodedCode;
            result.msg = "AccessDenied"
        } else {
            result.code = status;
            result.msg = "ServerError"
        }
        return result
    };
    var createAjaxUrl = function(conf) {
        if (!conf.url) {
            if (conf.params && conf.params.hasOwnProperty("useAES")) {
                if ($sys.isWeixin()) {
                    window.Logger && Logger.setReportParams({
                        appid: $sys.getWxAppid()
                    });
                    return serverContextUrl + "wx/mp/" + $sys.getWxAppid() + "/" + "gateway"
                } else if ($sys.isAli()) {
                    window.Logger && Logger.setReportParams({
                        appid: $sys.getAliAppid()
                    });
                    return serverContextUrl + "ali/mp/" + +$sys.getAliAppid() + "/" + "gateway"
                }
                return serverContextUrl + "gateway"
            } else {
                return serverContextUrl + "*.jsonRequest"
            }
        }
        var url = conf.url;
        var params = conf.params || {};
        var q = [];
        for (var nm in params) {
            if (params.hasOwnProperty(nm) && nm != "useAES") {
                var v = params[nm];
                if (is.Undefined(v)) {
                    v = ""
                }
                q.push(nm + "=" + v)
            }
        }
        if (q.length) {
            url += (url.indexOf("?") === -1 ? "?" : "&") + q.join("&")
        }
        if (conf.useLocal) {
            url = url;
            return url
        }
        if (serverContextUrl && url.indexOf("http") < 0) {
            url = serverContextUrl + url
        }
        return url
    };
    var ajaxErrorLog = function(logMsg) {
        console.log("网络请求失败: " + JSON.stringify(logMsg))
    };
    var isRetry = false;
    var ajax = function(conf) {
        var url = createAjaxUrl(conf);
        var method = conf.method || "POST";
        var xhr = createNewTransport(conf);
        var async = !conf.sync;
        var defer = new Defer;
        var result;
        try {
            xhr.open(method, url, async);
            if (conf.responseType) {
                xhr.responseType = conf.responseType
            }
            if (is.Function(conf.outboundProgress)) {
                xhr.upload.onprogress = conf.outboundProgress
            }
            if (is.Function(conf.inboundProgress)) {
                xhr.onprogress = conf.inboundProgress
            }
            if (async) {
                xhr.onreadystatechange = function() {
                    if (xhr.readyState == 4) {
                        xhr.onreadystatechange = $emptyFunction;
                        var result = parseResponse(xhr, conf);
                        if ((result.code == 403 || result.code == 409) && !isRetry) {
                            var notLogonCall = $AppContext.notLogonCallback;
                            if (is.Function(notLogonCall)) {
                                notLogonCall(result.code).then(function() {
                                    isRetry = true;
                                    ajax(conf).then(function(v) {
                                        isRetry = false;
                                        defer.resolve(v)
                                    }).fail(function(v) {
                                        isRetry = false;
                                        defer.reject(v)
                                    })
                                }).fail(function() {
                                    ajaxErrorLog({
                                        method: method,
                                        url: url,
                                        async: async,
                                        headers: headers,
                                        "X-Access-Token": env.currentToken ? env.currentToken.token : "",
                                        "X-Client-Id": env.currentToken ? env.currentToken.clientId : "",
                                        jsonData: jsonData,
                                        formData: formData,
                                        errorInfo: result
                                    });
                                    defer.reject(result)
                                })
                            } else {
                                ajaxErrorLog({
                                    method: method,
                                    url: url,
                                    async: async,
                                    headers: headers,
                                    "X-Access-Token": env.currentToken ? env.currentToken.token : "",
                                    "X-Client-Id": env.currentToken ? env.currentToken.clientId : "",
                                    jsonData: jsonData,
                                    formData: formData,
                                    errorInfo: result
                                });
                                defer.reject(result)
                            }
                        } else if (result.code < 300) {
                            defer.resolve(result)
                        } else {
                            ajaxErrorLog({
                                method: method,
                                url: url,
                                async: async,
                                headers: headers,
                                "X-Access-Token": env.currentToken ? env.currentToken.token : "",
                                "X-Client-Id": env.currentToken ? env.currentToken.clientId : "",
                                jsonData: jsonData,
                                formData: formData,
                                errorInfo: result
                            });
                            defer.reject(result)
                        }
                    }
                }
            }
            var jsonData = conf.jsonData;
            var formData = conf.formData;
            var headers = conf.headers;
            if (conf.params && conf.params.hasOwnProperty("useAES")) {
                var encrypt = conf.params && conf.params.useAES;
                if (encrypt) {
                    !conf.headers && (conf.headers = {});
                    !conf.headers["X-Service-Encrypt"] && (conf.headers["X-Service-Encrypt"] = 1);
                    jsonData = encryptData(jsonData)
                }
                var secureHeader = getSecureHeader($encode(jsonData));
                apply(headers, secureHeader);
                headers["X-Ca-Signature"] = getSignHeader(headers)
            }
            if (headers) {
                for (var h in headers) {
                    if (headers.hasOwnProperty(h)) {
                        var hv = headers[h];
                        if (is.Undefined(hv))
                            continue;
                        xhr.setRequestHeader(h, hv)
                    }
                }
            }
            if (env.currentToken) {
                xhr.setRequestHeader("X-Access-Token", env.currentToken.token);
                xhr.setRequestHeader("X-Client-Id", env.currentToken.clientId)
            }
            if (jsonData) {
                xhr.setRequestHeader("encoding", CHARSET);
                xhr.setRequestHeader("content-Type", APPLICATION_JSON_TYPE);
                xhr.send($encode(jsonData))
            } else if (formData) {
                xhr.send(formData)
            } else {
                xhr.send()
            }
        } catch (e) {
            result = {
                code: 400,
                msg: "Unknow error",
                err: e
            };
            ajaxErrorLog({
                method: method,
                url: url,
                async: async,
                headers: headers,
                "X-Access-Token": env.currentToken ? env.currentToken.token : "",
                "X-Client-Id": env.currentToken ? env.currentToken.clientId : "",
                jsonData: jsonData,
                formData: formData,
                errorInfo: e
            });
            if (async) {
                defer.reject(result)
            } else {
                return result
            }
        }
        if (async) {
            return defer.promise
        } else {
            if (xhr.readyState == 4) {
                result = parseResponse(xhr, conf)
            } else {
                result = {
                    code: 401,
                    msg: "ConnectFailed",
                    readyState: xhr.readyState
                }
            }
            if (result.code !== 200) {
                ajaxErrorLog({
                    method: method,
                    url: url,
                    async: async,
                    headers: headers,
                    "X-Access-Token": env.currentToken ? env.currentToken.token : "",
                    "X-Client-Id": env.currentToken ? env.currentToken.clientId : "",
                    jsonData: jsonData,
                    formData: formData,
                    errorInfo: result
                })
            }
            return result
        }
    };
    g.$ajax = ajax;
    var jsonpInvokeSeq = 0;
    var jsonp = function(conf) {
        var deferred = new Defer;
        var count = ++jsonpInvokeSeq;
        var jsonpCallbackName = "jsonpCallback" + count;
        var url = conf.url;
        if (url.indexOf("?") == -1) {
            url += "?callback="
        } else {
            url += "&callback="
        }
        url += jsonpCallbackName;
        var script = document.createElement("script");
        script.setAttribute("type", JAVASCRIPT_TYPE);
        script.setAttribute("language", "javascript");
        script.setAttribute("src", encodeURI(url));
        script.async = true;
        script.onerror = function(e) {
            htmlHead.removeChild(script);
            window[jsonpCallbackName] = null;
            window.Logger && Logger.log("jsonp请求失败: " + jsonpCallbackName, e);
            deferred.reject(null, e)
        }
        ;
        window[jsonpCallbackName] = function(json) {
            htmlHead.removeChild(script);
            window[jsonpCallbackName] = null;
            var callback = conf.callback;
            if (is.Function(callback)) {
                callback.call(this || conf.scope, json)
            }
            deferred.resolve(json)
        }
        ;
        window.Logger && Logger.log("jsonp请求: " + jsonpCallbackName);
        htmlHead.appendChild(script);
        return deferred.promise
    };
    g.$jsonp = jsonp;
    var scriptLoaded = {};
    var scriptLoading = {};
    var markCache = function(cls) {
        var cn;
        if (is.Array(cls)) {
            for (var i = 0; i < cls.length; i++) {
                cn = resolveLocalClsName(cls[i]);
                scriptLoaded[cn] = true
            }
        } else {
            cn = resolveLocalClsName(cls);
            scriptLoaded[cn] = true
        }
    };
    var clearLoadingPromise = function(cls) {
        var cn;
        if (is.Array(cls)) {
            for (var i = 0; i < cls.length; i++) {
                cn = resolveLocalClsName(cls[i]);
                delete scriptLoading[cn]
            }
        } else {
            cn = resolveLocalClsName(cls);
            delete scriptLoading[cn]
        }
    };
    var clearCache = function(cls) {
        var cn;
        if (is.Array(cls)) {
            for (var i = 0; i < cls.length; i++) {
                cn = resolveLocalClsName(cls[i]);
                delete scriptLoaded[cn]
            }
        } else {
            cn = resolveLocalClsName(cls);
            delete scriptLoaded[cn]
        }
    };
    env.clearCache = clearCache;
    var createNewTransport = function(conf) {
        var transport = new XMLHttpRequest;
        var withCredentials = true;
        if (conf && conf.withCredentials == false) {
            withCredentials = conf.withCredentials
        }
        try {
            transport.withCredentials = withCredentials;
            transport.setDisableHeaderCheck(true)
        } catch (e) {}
        return transport
    };
    var DOT_JSC = function() {
        return ".js"
    };
    var jscRegex = /^(?:,?[a-zA-Z$_][\w$-_]*(?:\.[a-zA-Z$_][\w$-_]*)*)+$/;
    var MEGER_FLAG = "??";
    var createScript = function(url) {
        if (!env.wxDebug && url.indexOf(MEGER_FLAG) > 0) {
            urls = url.split(MEGER_FLAG);
            if (urls && urls.length > 1 && urls[1] != null) {
                var newfiles = [];
                var files = urls[1].split(",");
                files.forEach(function(file) {
                    newfiles.push(manifest(file))
                });
                url = urls[0] + MEGER_FLAG + newfiles.join(",")
            }
        }
        var script = document.createElement("script");
        script.setAttribute("type", JAVASCRIPT_TYPE);
        script.setAttribute("language", "javascript");
        script.setAttribute("src", $env.cdnUrl(manifest(url)));
        script.async = true;
        return script
    };
    var createStyleLink = function(url) {
        if (!env.wxDebug && url.indexOf(MEGER_FLAG) > 0) {
            urls = url.split(MEGER_FLAG);
            if (urls && urls.length > 1 && urls[1] != null) {
                var newfiles = [];
                var files = urls[1].split(",");
                files.forEach(function(file) {
                    newfiles.push(manifest(file))
                });
                url = urls[0] + MEGER_FLAG + newfiles.join(",")
            }
        }
        var ss = document.createElement("Link");
        ss.setAttribute("href", $env.cdnUrl(manifest(url)));
        ss.setAttribute("rel", "stylesheet");
        ss.setAttribute("type", "text/css");
        return ss
    };
    var resolveLocalClsName = function(c) {
        if (c.charAt(0) == "/") {
            return c.substring(1)
        }
        return c
    };
    var relpaceLocalPathName = function(url) {
        return url.replace(/[.]/gi, "/")
    };
    var api = function(cls, alias) {
        var deferred = new Defer;
        $require(cls).then(function() {
            var nm = alias || cls;
            var api = findClass(nm);
            if (api) {
                deferred.resolve(api)
            } else {
                deferred.reject()
            }
        }, deferred.reject);
        return deferred.promise
    };
    g.$api = api;
    var create = function(cls, config) {
        var deferred = new Defer;
        var s = (new Date).getTime();
        $require(cls).then(function() {
            var C, m, cn = resolveLocalClsName(cls);
            C = findClass(cn);
            m = new C(config);
            m.$duration = (new Date).getTime() - s;
            deferred.resolve(m)
        }).fail(deferred.reject);
        return deferred.promise
    };
    g.$create = create;
    var removeStylesheet = function(cls) {
        if (stylesheetRefCount[cls]) {
            var count = --stylesheetRefCount[cls];
            if (count > 0) {
                stylesheetRefCount[cls] = count;
                return
            }
            delete stylesheetRefCount[cls];
            var existing = document.getElementById(cls);
            if (existing) {
                existing.parentNode.removeChild(existing)
            }
        }
    };
    g.$rStyleSheet = removeStylesheet;
    var newLoadNextScriptFunc = function(cls, order, attrs) {
        return function() {
            return loadScript(cls, order, attrs)
        }
    };
    var loadScriptByMerge = function(cls, order, attrs) {
        var url, i, j = 0, cn, p;
        var ncs;
        var lds;
        var deferred = new Defer;
        if (is.String(cls)) {
            cn = resolveLocalClsName(cls);
            p = classDefining[cn] || scriptLoading[cn];
            if (scriptLoaded[cn] || findClass(cn) || p) {
                if (p) {
                    p.then(function() {
                        deferred.resolve()
                    }, function(e) {
                        deferred.reject(e)
                    })
                } else {
                    deferred.resolve()
                }
                return deferred.promise
            }
            scriptLoading[cn] = deferred.promise;
            ncs = cn;
            url = relpaceLocalPathName(cn);
            url += DOT_JSC()
        } else {
            if (is.Array(cls)) {
                ncs = [];
                lds = [];
                ncs_new = [];
                for (i = 0; i < cls.length; i++) {
                    cn = resolveLocalClsName(cls[i]);
                    p = classDefining[cn] || scriptLoading[cn];
                    if (scriptLoaded[cn] || findClass(cn) || p) {
                        if (p) {
                            lds.push(p)
                        }
                    } else {
                        scriptLoading[cn] = deferred.promise;
                        ncs.push(cn)
                    }
                }
                if (!ncs.length) {
                    if (lds.length) {
                        when.all(lds).then(function() {
                            deferred.resolve()
                        }, function(e) {
                            deferred.reject(e)
                        })
                    } else {
                        deferred.resolve()
                    }
                    return deferred.promise
                }
                for (index = 0; index < ncs.length; index++) {
                    ncs_new.push(relpaceLocalPathName(ncs[index]))
                }
                url = env.appAbsolutePath + "/" + MEGER_FLAG + ncs_new.join(".js,");
                url += DOT_JSC()
            } else {
                deferred.reject(new Error("classname[" + cls + "] is valid."));
                return deferred.promise
            }
        }
        var script = createScript(url);
        if (is.Object(attrs)) {
            var attr;
            for (attr in attrs) {
                if (attrs.hasOwnProperty(attr)) {
                    script.setAttribute(attr, attrs[attr])
                }
            }
        }
        var onerror = function(e) {
            clearLoadingPromise(ncs);
            clearCache(ncs);
            htmlHead.removeChild(script);
            deferred.reject(e)
        };
        script.onload = function() {
            var loadings = [], i, cp, p;
            console.log("load url:" + url);
            clearLoadingPromise(ncs);
            if (is.String(cls)) {
                p = classDefining[ncs]
            } else {
                for (i = 0; i < cls.length; i++) {
                    cn = resolveLocalClsName(cls[i]);
                    cp = classDefining[cn] || scriptLoading[cn];
                    if (cp) {
                        loadings.push(cp)
                    }
                }
                if (loadings.length) {
                    p = when.all(loadings)
                }
            }
            if (p) {
                p.then(function() {
                    markCache(ncs);
                    deferred.resolve();
                    htmlHead.removeChild(script)
                }, onerror)
            } else {
                markCache(ncs);
                deferred.resolve();
                htmlHead.removeChild(script)
            }
        }
        ;
        script.onerror = onerror;
        htmlHead.appendChild(script);
        return deferred.promise
    };
    var loadScriptBySingle = function(cls, order, attrs) {
        var url, i, j = 0, cn, p;
        var ncs;
        var lds;
        var deferred = new Defer;
        if (is.String(cls)) {
            cn = resolveLocalClsName(cls);
            p = classDefining[cn] || scriptLoading[cn];
            if (scriptLoaded[cn] || findClass(cn) || p) {
                if (p) {
                    p.then(function() {
                        deferred.resolve()
                    }, function(e) {
                        deferred.reject(e)
                    })
                } else {
                    deferred.resolve()
                }
                return deferred.promise
            }
            scriptLoading[cn] = deferred.promise;
            ncs = cn;
            url = cn
        } else {
            if (is.Array(cls)) {
                ncs = [];
                lds = [];
                for (i = 0; i < cls.length; i++) {
                    cn = resolveLocalClsName(cls[i]);
                    p = classDefining[cn] || scriptLoading[cn];
                    if (scriptLoaded[cn] || findClass(cn) || p) {
                        if (p) {
                            lds.push(p)
                        }
                    } else {
                        ncs.push(cn)
                    }
                }
                if (!ncs.length) {
                    if (lds.length) {
                        when.all(lds).then(function() {
                            deferred.resolve()
                        }, function(e) {
                            deferred.reject(e)
                        })
                    } else {
                        deferred.resolve()
                    }
                    return deferred.promise
                }
            } else {
                deferred.reject(new Error("classname[" + cls + "] is valid."));
                return deferred.promise
            }
        }
        if (is.Array(ncs)) {
            var p;
            var next = ncs[0];
            if (lds.length > 0) {
                p = lds[0];
                for (i = 1; i < lds.length; i++) {
                    p = p.then(function() {
                        return lds[i]
                    })
                }
                p = p.then(newLoadNextScriptFunc(next, order, attr))
            } else {
                p = loadScript(next, order, attr)
            }
            for (i = 1; i < ncs.length; i++) {
                next = ncs[i];
                p = p.then(newLoadNextScriptFunc(next, order, attr))
            }
            p.then(function() {
                deferred.resolve()
            }, function(e) {
                deferred.reject(e)
            });
            return deferred.promise
        } else {
            if (jscRegex.test(url)) {
                url = relpaceLocalPathName(url) + DOT_JSC()
            }
            var script = createScript(url);
            if (is.Object(attrs)) {
                var attr;
                for (attr in attrs) {
                    if (attrs.hasOwnProperty(attr)) {
                        script.setAttribute(attr, attrs[attr])
                    }
                }
            }
            var onerror = function(e) {
                clearLoadingPromise(ncs);
                clearCache(ncs);
                htmlHead.removeChild(script);
                deferred.reject(e)
            };
            script.onload = function() {
                var loadings = [], i, cp, p;
                console.log("load url:" + url);
                clearLoadingPromise(ncs);
                if (is.String(cls)) {
                    p = classDefining[ncs]
                } else {
                    for (i = 0; i < cls.length; i++) {
                        cn = resolveLocalClsName(cls[i]);
                        cp = classDefining[cn] || scriptLoading[cn];
                        if (cp) {
                            loadings.push(cp)
                        }
                    }
                    if (loadings.length) {
                        p = when.all(loadings)
                    }
                }
                if (p) {
                    p.then(function() {
                        markCache(ncs);
                        deferred.resolve();
                        htmlHead.removeChild(script)
                    }, onerror)
                } else {
                    markCache(ncs);
                    deferred.resolve();
                    htmlHead.removeChild(script)
                }
            }
            ;
            script.onerror = onerror;
            htmlHead.appendChild(script)
        }
        return deferred.promise
    };
    var loadScript = function(cls, order, attrs) {
        if ($env.wxDebug || $env.hybrid) {
            return loadScriptBySingle(cls, order, attrs)
        } else {
            return loadScriptByMerge(cls, order, attrs)
        }
    };
    var manifest = function(cls) {
        if (is.String(cls)) {
            cls = resolveLocalClsName(cls);
            if (env.fileVersion == null || env.fileVersion == "" || cls.indexOf("??") > 0) {
                return cls
            }
            var index = cls.lastIndexOf(".");
            var ext = cls.substring(index, cls.length);
            cls = $env.manifest[cls] ? $env.manifest[cls] : cls;
            return cls
        } else if (is.Array(cls)) {
            for (i = 0; i < cls.length; i++) {
                cls[i] = manifest(cls[i])
            }
            return cls
        }
    };
    g.$require = loadScript;
    var loadHts = function(cls) {
        var cn = resolveLocalClsName(cls);
        cn = relpaceLocalPathName(cn);
        var p = cn.lastIndexOf(".");
        if (p == -1) {
            cn += ".html"
        } else {
            var postfix = cn.substring(p, p + 4);
            if (postfix != ".html") {
                cn += ".html"
            }
        }
        if (env.hybrid) {
            return env.nativeApi.requireResource(cn)
        } else {
            var defer = new Defer;
            ajax({
                method: "GET",
                url: $env.cdnUrl(manifest(cn)),
                useLocal: true,
                withCredentials: false
            }).then(function(json) {
                var contentStr = json.body;
                contentStr.replace(".png");
                defer.resolve(json.body)
            }, function(e) {
                defer.reject(e)
            });
            return defer.promise
        }
    };
    g.$loadHts = loadHts;
    var stylesheetRefCount = {};
    var newLoadNextStyleSheetFunc = function(ne) {
        return function() {
            return loadStylesheet(ne)
        }
    };
    var loadStylesheet = function(cls) {
        var deferred = new Defer, baseDir, merge, newCls, cn, url, count, i;
        if (!cls) {
            deferred.resolve();
            return deferred.promise
        }
        if (is.Object(cls)) {
            baseDir = cls.baseDir;
            merge = cls.merge;
            cls = cls.css
        }
        if (env.wxDebug || $env.hybrid) {
            merge = false
        }
        if (is.Array(cls)) {
            var j = 0;
            newCls = [];
            for (i = 0; i < cls.length; i++) {
                cn = resolveLocalClsName(cls[i]);
                if (stylesheetRefCount[cn]) {
                    count = stylesheetRefCount[cn];
                    stylesheetRefCount[cn] = ++count
                } else {
                    stylesheetRefCount[cn] = 1;
                    newCls[j] = cn;
                    j++
                }
            }
            if (j == 0) {
                deferred.resolve();
                return deferred.promise
            }
            cls = j == 1 ? newCls[0] : newCls
        } else {
            cn = resolveLocalClsName(cls);
            if (stylesheetRefCount[cn]) {
                count = stylesheetRefCount[cn];
                stylesheetRefCount[cn] = ++count;
                deferred.resolve();
                return deferred.promise
            }
            stylesheetRefCount[cn] = 1;
            cls = cn
        }
        if (is.Array(cls)) {
            if (merge) {
                url = "";
                for (i = 0; i < cls.length; i++) {
                    if (baseDir) {
                        url += baseDir + "/" + cls[i] + (i == cls.length - 1 ? ".css" : ".css,")
                    } else {
                        url += "/" + cls[i] + (i == cls.length - 1 ? ".css" : ".css,")
                    }
                }
                url = env.appAbsolutePath + "/" + MEGER_FLAG + url
            } else {
                var next = cls[0];
                stylesheetRefCount[next] = 0;
                var p = loadStylesheet({
                    css: next,
                    baseDir: baseDir
                });
                for (i = 1; i < cls.length; i++) {
                    next = cls[i];
                    stylesheetRefCount[next] = 0;
                    p = p.then(newLoadNextStyleSheetFunc({
                        css: next,
                        baseDir: baseDir
                    }))
                }
                p.then(function() {
                    deferred.resolve()
                }, function(e) {
                    deferred.reject(e)
                });
                return deferred.promise
            }
        } else {
            if (baseDir) {
                cls = baseDir + "." + cls
            }
            url = cls.replace(/[.]/gi, "/") + ".css"
        }
        var ss = createStyleLink(url);
        ss.onload = function() {
            deferred.resolve()
        }
        ;
        ss.onerror = function(e) {
            deferred.reject(e)
        }
        ;
        htmlHead.appendChild(ss);
        return deferred.promise
    };
    g.$styleSheet = loadStylesheet;
    var TemplateClass = function() {};
    var chain = function(object) {
        TemplateClass.prototype = object;
        var result = new TemplateClass;
        TemplateClass.prototype = null;
        return result
    };
    var noArgs = [];
    var apply = function(object, config, defaults) {
        if (defaults) {
            apply(object, defaults)
        }
        if (object && config && is.Object(config)) {
            var i;
            for (i in config) {
                if (config.hasOwnProperty(i))
                    object[i] = config[i]
            }
            return object
        }
    };
    g.$apply = apply;
    var Base = function() {};
    apply(Base, {
        $isClass: true,
        getName: function() {
            return this.$classname
        },
        addMembers: function(members) {
            var me = this, prototype = me.prototype, name, member;
            for (name in members) {
                if (members.hasOwnProperty(name)) {
                    member = members[name];
                    if (is.Function(member) && !member.$isClass) {
                        if (prototype.hasOwnProperty(name)) {
                            member.$previous = prototype[name]
                        }
                        member.$owner = me;
                        member.$name = name
                    }
                    prototype[name] = member
                }
            }
            return this
        },
        extend: function(superClass) {
            var me = this, superPrototype = superClass.prototype, basePrototype, prototype, name;
            prototype = me.prototype = chain(superPrototype);
            this.superclass = prototype.superclass = superPrototype;
            if (!superClass.$isClass) {
                basePrototype = Base.prototype;
                for (name in basePrototype) {
                    if (name in prototype) {
                        prototype[name] = basePrototype[name]
                    }
                }
            }
        },
        mixin: function(mixinClass) {
            var me = this
              , prototype = me.prototype;
            var c = mixinClass;
            if (typeof mixinClass == "string") {
                mixinClass = findClass(resolveLocalClsName(mixinClass))
            }
            if (is.Array(mixinClass)) {
                for (var i = 0; i < mixinClass.length; i++) {
                    me.mixin(mixinClass[i])
                }
                return
            }
            var mixin = mixinClass.prototype;
            for (var key in mixin) {
                if (key == "superclass") {
                    continue
                }
                prototype[key] = mixin[key]
            }
        },
        toString: function() {
            return "[class " + this.prototype.$className + "]"
        }
    });
    Base.addMembers({
        $isInstance: true,
        $className: "Base",
        callParent: function(args) {
            var method, superMethod = (method = this.callParent.caller) && (method.$previous || (method = method.$owner ? method : method.caller) && method.$owner.superclass[method.$name]);
            if (!superMethod) {
                throw "[" + method.$owner.superclass.$className + "] has no superMethod[ " + method.$name + "()]."
            }
            return superMethod.apply(this, args || noArgs)
        },
        constructor: function() {
            return this
        }
    });
    var makeCtor = function() {
        function constructor() {
            return this.constructor.apply(this, arguments) || null
        }
        return constructor
    };
    var createCls = function(newClass, overrides, classname) {
        var basePrototype = Base.prototype, newClassExtend = overrides.extend, mixinClass = overrides.mixins, superClass, superPrototype, name;
        delete overrides.extend;
        delete overrides.mixins;
        if (newClassExtend && newClassExtend !== Object) {
            if (is.String(newClassExtend)) {
                newClassExtend = resolveLocalClsName(newClassExtend);
                superClass = findClass(newClassExtend)
            } else {
                superClass = newClassExtend
            }
        } else {
            superClass = Base
        }
        superPrototype = superClass.prototype;
        if (!superClass.$isClass) {
            for (name in basePrototype) {
                if (!superPrototype[name]) {
                    superPrototype[name] = basePrototype[name]
                }
            }
        }
        newClass.extend(superClass);
        if (mixinClass) {
            newClass.mixin(mixinClass)
        }
        if (classname) {
            var n = classname.lastIndexOf(".");
            var pkgName = classname.substring(0, n);
            var clz = classname.substring(n + 1);
            var pkg = ns(pkgName);
            pkg[clz] = newClass;
            newClass.prototype.$className = classname
        }
        newClass.addMembers(overrides);
        return newClass
    };
    var classDefining = {};
    var depsMapNodes = {};
    var registerDepsMapNode = function(cn, pn) {
        var node = depsMapNodes[cn];
        if (!node) {
            node = depsMapNodes[cn] = {
                deps: [],
                name: cn
            }
        }
        if (pn) {
            var pnode = depsMapNodes[pn];
            pnode.deps.push(node);
            node.parent = pnode
        }
        return node
    };
    var cyclicDependencyCheck = function(cn, pn) {
        var node = depsMapNodes[pn];
        while (node) {
            if (node.name == cn) {
                return true
            }
            node = node.parent
        }
        return false
    };
    var oldPersonLoadeds = [];
    var setOldPersonLoaded = function(url) {
        if (!oldPersonLoadeds)
            oldPersonLoadeds = [];
        oldPersonLoadeds.push(url)
    };
    env.oldPersonLoadeds = function() {
        return oldPersonLoadeds
    }
    ;
    env.clearOldPersonLoadeds = function() {
        oldPersonLoadeds = []
    }
    ;
    var define = function(classname, overrides) {
        var defer = new Defer;
        classDefining[classname] = defer.promise;
        clearLoadingPromise(classname);
        if (!is.String(classname) || !classname) {
            defer.reject(new Error("classname required."));
            return
        }
        if (is.Function(overrides)) {
            overrides = overrides.call(this)
        }
        if (!is.Object(overrides)) {
            defer.reject(new Error("class[" + classname + "] overrides not found."));
            return
        }
        var newClass = makeCtor(), name;
        for (name in Base) {
            newClass[name] = Base[name]
        }
        var reqs = [], loadings = [], i;
        registerDepsMapNode(classname);
        var reqOrLoad = function(c) {
            var cn = resolveLocalClsName(c), p;
            if (cyclicDependencyCheck(cn, classname)) {
                console.warn("class[" + classname + "] -> [" + cn + "] cyclic dependency found.");
                return
            }
            registerDepsMapNode(cn, classname);
            p = classDefining[cn];
            if (p) {
                loadings.push(p)
            } else {
                if (scriptLoaded[cn] || findClass(cn)) {
                    return
                }
                reqs.push(c)
            }
        };
        var extend = overrides.extend;
        if (extend) {
            if (is.String(extend)) {
                reqOrLoad(extend)
            } else if (!is.Class(extend)) {
                defer.reject(new Error("class[" + classname + "] extend type invalid."));
                return
            }
        }
        var mixins = overrides.mixins;
        if (mixins) {
            if (is.Array(mixins)) {
                for (i = 0; i < mixins.length; i++) {
                    var mi = mixins[i];
                    if (is.String(mi)) {
                        reqOrLoad(mi)
                    } else if (!is.Class(mi)) {
                        defer.reject(new Error("class[" + classname + "] mixins type invalid."));
                        return
                    }
                }
            } else {
                if (is.String(mixins)) {
                    reqOrLoad(mixins)
                } else if (!is.Class(mixins)) {
                    defer.reject(new Error("class[" + classname + "] mixins type invalid."));
                    return
                }
            }
        }
        var deps = overrides.deps;
        var depsOrder = overrides.depsOrder;
        if (is.Array(deps)) {
            for (i = 0; i < deps.length; i++) {
                reqOrLoad(deps[i])
            }
        } else if (is.String(deps)) {
            reqOrLoad(deps)
        }
        delete overrides.deps;
        var css = overrides.css;
        if (overrides.themeName) {
            if (!$is.Array(css)) {
                css = []
            }
            var themFolder = $sys.chooseThem();
            if (themFolder) {
                css.push("eh.wx.health.themes." + themFolder + "." + overrides.themeName)
            }
        }
        if (css) {
            loadings.push(loadStylesheet(css));
            delete overrides.css
        }
        var hts = overrides.hts;
        var oldFlag = overrides.oldFlag;
        if (hts) {
            if (is.Boolean(hts)) {
                hts = classname
            } else if (hts == "/") {
                hts += classname
            }
            if (oldFlag) {
                setOldPersonLoaded(classname)
            }
            oldFlag && $sys.hook && (hts = $sys.hook.oldPersonHtml(classname));
            $sys.hook && $sys.hook.html(classname) && (hts = $sys.hook.html(classname));
            loadings.push(loadHts(hts).then(function(s) {
                overrides.html = s
            }))
        }
        if (reqs.length) {
            if (reqs.length == 1) {
                reqs = reqs[0]
            }
            loadings.push(loadScript(reqs, depsOrder))
        }
        if (loadings.length) {
            when.all(loadings).then(function() {
                var c = createCls(newClass, overrides, classname);
                delete classDefining[classname];
                delete depsMapNodes[classname];
                defer.resolve(c)
            }).fail(function(e) {
                defer.reject(e)
            })
        } else {
            try {
                var c = createCls(newClass, overrides, classname);
                delete classDefining[classname];
                delete depsMapNodes[classname];
                defer.resolve(c)
            } catch (e) {
                defer.reject(e)
            }
        }
        return defer.promise
    };
    g.$class = define;
    var override = function(cls, overrides) {
        cls.addMembers(overrides)
    };
    g.$override = override;
    env.timeStamp = function(e) {
        var defer = new $Defer;
        $ajax({
            url: $env.getServerContextUrl() + "/api/timestamp",
            method: "GET"
        }).then(function(json) {
            if (json) {
                json.body && ($env.localTimeOffset = json.body - +new Date);
                json.properties && json.properties.uuid && ($env.uuidForAES = json.properties.uuid)
            }
            defer.resolve(json)
        }).fail(function(error) {
            defer.reject(e)
        });
        return defer.promise
    }
    ;
    env.wxDebug = false;
    env.setDevMode = function(isDev) {
        env.wxDebug = isDev
    }
    ;
    env.getModuleRoute = function(moduleName) {
        if (!env.routes)
            return null;
        return env.routes[moduleName]
    }
    ;
    var fetchManifest = function() {
        var url = "manifest-" + env.fileVersion + ".json";
        if (!env.wxDebug && !$env.hybrid) {
            url = env.appResourcesUrl + url
        }
        return fetch(url, {}).then(function(res) {
            return res.json()
        }).then(function(json) {
            $env.manifest = json
        })
    };
    var loadManifest = function() {
        var tasks = [$env.timeStamp(), fetchManifest()];
        return $when.all(tasks)
    };
    g.$manifest = loadManifest;
    $env.isMockServices = function(beanName, method) {
        var i = 0, list = $env.mockServices || [], service;
        if (!$env.wxDebug || !$env.mockDataUrl) {
            return false
        }
        if (!list.length) {
            return false
        }
        for (; i < list.length; i++) {
            service = list[i];
            if (service.beanName != beanName) {
                continue
            }
            if ($is.String(service.method)) {
                service.method = [service.method]
            }
            if (service.method.indexOf(method) == -1) {
                continue
            } else {
                return true
            }
        }
        return false
    }
    ;
    $env.errorCatch = function(e) {
        var error = e.msg || e.toString();
        window.Logger ? Logger.error("错误", e.stack) : console.error($env.vconsole ? error : e);
        $sys && $sys.showMsg ? $sys.showMsg({
            template: error,
            cssClass: "default",
            buttons: [{
                text: "知道了",
                type: "button-positive"
            }]
        }) : alert(e)
    }
    ;
    $env.vconsole = $env.getUrlParam("vconsole");
    $env.isWeApp = function() {
        var ua = navigator.userAgent.toLowerCase()
          , flag = false;
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            wx.miniProgram.getEnv(res=>{
                flag = res.miniprogram ? true : false
            }
            )
        }
        return flag
    }
    ;
    $env.isMinapp = function() {
        var ua = navigator.userAgent.toLowerCase()
          , flag = false;
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            wx.miniProgram.getEnv(res=>{
                flag = res.miniprogram ? true : false
            }
            )
        } else if ($env.getCookie("api") == "ALILIFEMINI") {
            flag = true
        }
        return flag
    }
    ;
    $env.gotoMiniAppWithPermission = function() {
        var url = "/pages/permission/index?uuid=" + $env.minappKeyId;
        if ($env.isAliapp) {
            my.navigateTo({
                url: url
            })
        } else {
            wx.miniProgram.navigateTo({
                url: url
            })
        }
        return
    }
    ;
    $env.makeUuid = function() {
        return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
            var r = Math.random() * 16 | 0
              , v = c == "x" ? r : r & 3 | 8;
            return v.toString(16)
        })
    }
    ,
    $env.requireMinappLoginAuth = function(unBind) {
        var minAppdefer = new $Defer;
        var isUserLogin = !!$sys.mpi();
        if (unBind || isUserLogin || !$env.isMinapp()) {
            minAppdefer.resolve(false);
            return minAppdefer.promise
        }
        if ($env.minappKeyId) {
            return ngari.rmi.serviceAdapter.bindMethod({
                useHttpHeader: true,
                beanName: "consult.consultInfoService",
                method: "getLoginCode"
            })($env.minappKeyId).then(function(res) {
                var tempminAppdefer = new $Defer;
                if (!res) {
                    $env.minappKeyId = $env.makeUuid();
                    $env.gotoMiniAppWithPermission();
                    tempminAppdefer.resolve(true);
                    return tempminAppdefer.promise
                } else {
                    return $sys.loginInMiniApp(res).then(function(f) {
                        tempminAppdefer.resolve(f);
                        return tempminAppdefer.promise
                    })
                }
            }).fail(function(e) {
                console.log("error", e)
            })
        } else {
            $env.minappKeyId = $env.makeUuid();
            $env.gotoMiniAppWithPermission();
            minAppdefer.resolve(true);
            return minAppdefer.promise
        }
    }
    ;
    $env.checkIsMinappLogin = function() {
        var tempDefer = new $Defer;
        if ($env.isMinapp() && $env.minappKeyId) {
            return ngari.rmi.serviceAdapter.bindMethod({
                useHttpHeader: true,
                beanName: "consult.consultInfoService",
                method: "getLoginCode"
            })($env.minappKeyId).then(function(res) {
                if (!res) {
                    $env.minappKeyId = $env.makeUuid();
                    $env.gotoMiniAppWithPermission();
                    tempDefer.resolve(true);
                    return tempDefer.promise
                } else {
                    return $sys.loginInMiniApp(res).then(function(f) {
                        tempDefer.resolve(f);
                        return tempDefer.promise
                    })
                }
            }).fail(function(e) {
                console.log("error", e);
                tempDefer.resolve(false);
                return tempDefer.promise
            })
        } else {
            tempDefer.resolve(false);
            return tempDefer.promise
        }
    }
    ;
    $env.xmlescape = function(text) {
        if (text && typeof text == "string") {
            text = text.replace(/</g, "&lt;");
            text = text.replace(/>/g, "&gt;");
            text = text.replace(/'/g, "&apos;");
            text = text.replace(/"/g, "&quot;")
        }
        return text
    }
}
)(this);
