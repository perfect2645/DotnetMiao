$env.setDevMode("production" == "development" || $env.getUrlParam("wxDebug") == "true");
$env.setServerContextUrl("https://weixin.ngarihealth.com/weixin/");
$env.setCDNStaticServerUrl("https://weixinnode.ngarihealth.com/weixin/");
$env.setVersion(1654710216390);
window.onload = function () {
    var isJoinBtnPress = true;
    var isCountDownEnd = true;
    var isInitialFinish = false;
    var countDownInterval = null;
    var hasLoading = false;
    var hasClick = false;
    $manifest().then(loadBaseJs).then(initLogger).then(loadConsole).then(loadIndexTemplate).fail(function (e) {
        $env.errorCatch(e)
    }).then(loadCss).then(loadUtilJs).then(handleWxFontSetting).then(checkThirdUrl).fail(function (e) {
        $env.errorCatch(e)
    }).then(handleWX).then(handleCountDown).then(initConsultPageId).fail(function (e) {
        $env.errorCatch(e)
    });
    function handleCountDown() {
        isInitialFinish = true;
        if (isJoinBtnPress) {
            gotoProtal()
        }
    }
    function initConsultPageId() {
        var consultPageId = $env.getUrlParam("consultPageId");
        if (consultPageId) {
            $env.setConsultPageId(consultPageId)
        }
    }
    function loadBaseJs() {
        var js = ["lib.logger", "lib.crypto.crypto-js-min", "ngari.rmi.serviceAdapter", "lib.ionic.ionic-bundle-1-1-1-min", "ngari.ionicInit", "eh.wx.health.config", "lib.xss"];
        if ($env.vconsole) {
            js.push("lib.vconsole")
        }
        return $require(js, !0)
    }
    function initLogger() {
        Logger && Logger.useDefaults({
            project: "ehealth-weixin-h5",
            module: "home",
            defaultLevel: Logger.INFO,
            formatter: function (messages, context) {
                messages.unshift((new Date).toUTCString())
            },
            fetchError: _fetchError
        })
    }
    function _fetchError(info, cb) {
        if (info && info.envInfo && (info.envInfo.env == "premaster" || info.envInfo.env == "master")) {
            $ajax({
                headers: {
                    "X-Service-Id": "bus.buriedPointService",
                    "X-Service-Method": "saveFrontLog"
                },
                params: {
                    useAES: false
                },
                jsonData: [info]
            }).then(function () {
                cb && cb()
            }).fail(function (e) {
                console.error(e);
                cb && cb()
            })
        }
    }
    function loadConsole() {
        if ($env.vconsole) {
            var vConsole = new VConsole
        }
        var tasks = [$sys.getParams(), $sys.getIndexTemplateUserParams()];
        return $when.all(tasks)
    }
    function configIndexTemplate(config) {
        isJoinBtnPress = false;
        isCountDownEnd = false;
        hasLoading = false;
        hasClick = false;
        var defaultConfig = {
            enterHomePage: false,
            enterHomePageTip: "进入首页",
            countdown: false,
            countdownSecond: 5
        };
        var _config = Object.assign(defaultConfig, config);
        if (_config.countdown) {
            var countDownSecond = _config.countdownSecond;
            $(".countdown").text(countDownSecond + "秒");
            $(".countdown").show();
            countDownInterval = setInterval(() => {
                if (countDownSecond <= 0) {
                    isCountDownEnd = true;
                    gotoProtal()
                } else {
                    $(".countdown").text(countDownSecond + "秒")
                }
                countDownSecond--
            }
                , 1e3)
        } else {
            isCountDownEnd = true
        }
        if (_config.enterHomePage) {
            !_config.countdown && (isJoinBtnPress = true);
            $(".join-portal-btn").text(_config.enterHomePageTip);
            $(".join-portal-btn").show();
            $(".join-portal-btn").on("click", function () {
                isJoinBtnPress = true;
                if (isInitialFinish && !hasClick && !hasLoading) {
                    hasClick = true;
                    gotoProtal()
                } else {
                    $(".join-portal-btn").text("正在进入...");
                    hasLoading = true
                }
            })
        } else {
            !_config.countdown && (isJoinBtnPress = true)
        }
    }
    function loadIndexTemplate(result) {
        if (result == null || result[1] == null) {
            var tasks = [];
            return $when.all(tasks)
        }
        var fromThird = $env.getUrlParam("thirdExtraId");
        if (fromThird && fromThird !== "" && fromThird !== null) {
            $(".backdrop").css("background-color", "#fff");
            $sys.showLoading()
        } else {
            try {
                var templateObj = $decode(decodeURIComponent(result[1]));
                if (templateObj == null || templateObj.indexTempId == null) {
                    $(".defaultBg").css("display", "block")
                } else {
                    configIndexTemplate(templateObj.tempProperties);
                    switch (templateObj.indexTempId) {
                        case "indexTempId_1":
                            $(".template-1").css("display", "block");
                            $(".template-1 .img-auto").attr("src", $sys.thumb(templateObj.mode.pic_1));
                            break;
                        case "indexTempId_2":
                            $(".template-2").css("display", "block");
                            $(".template-2 .img-top").attr("src", $sys.thumb(templateObj.mode.pic_1));
                            $(".template-2 .img-bottom").attr("src", $sys.thumb(templateObj.mode.pic_2));
                            break;
                        case "indexTempId_3":
                            $(".template-3").css("display", "block");
                            $(".template-3 .img-top").attr("src", $sys.thumb(templateObj.mode.pic_1));
                            $(".template-3 .img-middle").attr("src", $sys.thumb(templateObj.mode.pic_2));
                            $(".template-3 .img-bottom").attr("src", $sys.thumb(templateObj.mode.pic_3));
                            break;
                        default:
                            $(".defaultBg").css("display", "block");
                            break
                    }
                }
            } catch (error) {
                $env.errorCatch(error)
            }
        }
        var tasks = [];
        return $when.all(tasks)
    }
    function loadCss() {
        return $styleSheet({
            css: ["font-awesome-min", "material-design-iconic-font-min", "ionic-1-1-1-min", "animate-min", "theme-mobile-min"],
            baseDir: "css",
            merge: true
        })
    }
    function loadUtilJs() {
        function audioAutoPlay() {
            var audio = new Audio;
            audio.crossorigin = "anonymous";
            audio.play();
            audio.pause();
            window.audio = audio
        }
        !!navigator.userAgent.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/) && document.addEventListener("WeixinJSBridgeReady", function () {
            audioAutoPlay()
        }, false);
        var js = ["lib.crypto.crypto-js-min", "ngari.rmi.serviceAdapter", "lib.antbridge-min", "ngari.utils.codec.md5", "lib.ionic.ionic-bundle-1-1-1-min", "ngari.ionicInit", "lib.jweixin-1-6-0", "eh.wx.util.filter", "eh.wx.health.config.ConfigCenter", "ngari.utils.DologMgr"];
        return $require(js, !0)
    }
    function checkThirdUrl() {
        var third = $env.getUrlParam("third");
        var tasks = [$env.timeStamp(), third ? $sys.getUserParams(3) : $sys.getUserParams(), $sys.getModuleRoute()];
        return $when.all(tasks)
    }
    function handleWX(values) {
        var appid = $sys.getLocationAppId()
            , appid = appid && appid[0];
        var WXApp = $env.getCookie("WXApp") ? $decode($decode($env.getCookie("WXApp"))) : {};
        $env.requestNoAES = !(!!+WXApp.openAES || $env.getUrlParam("useAes") == "true");
        $env.version = 2.9;
        $env.avatarDefault = $env.resourcesHome + "eh/wx/health/notify/avators/";
        $env.isIOS = $env.platform.name == "iOS";
        $env.api = $env.getCookie("api") || "";
        $env.api = $env.api == "ALILIFEMINI" ? "ALILIFE" : $env.api;
        $env.appName = $env.isAlipay ? "生活号" : "微信";
        $env.videoUrl = $env.appRootOffsetPath;
        $env.appkey = $env.getCookie("appkey") || "";
        var thirdArr = ["WEB", "WX_WEB", "APP_WEB", "ALILIFE_WEB", "WEB_MINI_APP", "WX_THIRD"];
        $env.isThirdParty = $env.api && thirdArr.indexOf($env.api) > -1 ? !0 : !1
    }
    function xssInputListener() {
        var inputFlag = false;
        var xssfilterFunc = function (e) {
            var targetVal = e.target.value;
            var filterValue = filterXSS(targetVal, {
                escapeHtml: function (html) {
                    var reg = /<(a|script).*\/>|<(a|script).*\/(a|script).*>/i;
                    return html.replace(reg, "").replace(/\[removed\]/, "")
                },
                stripIgnoreTag: true,
                stripIgnoreTagBody: ["script", "a"]
            });
            e.target.value = filterValue.replace(/\[removed\]/g, "")
        };
        $(document.body).on("input", "input", function (e) {
            if (inputFlag) {
                return
            }
            var target = e.target;
            var typeList = ["text", "password", "email", "url"];
            if (typeList.includes(target.type)) {
                xssfilterFunc(e)
            }
        });
        $(document.body).on("compositionstart", "input", function (e) {
            inputFlag = true
        });
        $(document.body).on("compositionend", "input", function (e) {
            inputFlag = false;
            if (!inputFlag) {
                var target = e.target;
                var typeList = ["text", "password", "email", "url"];
                if (typeList.includes(target.type)) {
                    xssfilterFunc(e)
                }
            }
        });
        $(document.body).on("blur", "input", function (e) {
            var target = e.target;
            var typeList = ["text", "password", "email", "url"];
            if (typeList.includes(target.type)) {
                xssfilterFunc(e)
            }
        });
        $(document.body).on("input", "textarea", function (e) {
            xssfilterFunc(e)
        });
        $(document.body).on("blur", "textarea", function (e) {
            xssfilterFunc(e)
        })
    }
    function gotoProtal() {
        clearInterval(countDownInterval);
        var f, st = $("#startup").css({
            position: "absolute",
            top: "0",
            right: "0",
            bottom: "0",
            left: "0",
            "z-index": "10"
        }), delay = $env.getUrlParam("delay");
        $(document.body).prepend('<div id="initBg" style="position: absolute;top: 0;bottom: 0;right: 0;left: 0;background: #f1f2f5;z-index: 7;"></div>');
        $create("eh.wx.health.portal.Portal").then(function (m) {
            m.on("ready", function () {
                if (delay) {
                    setTimeout(function () {
                        st.addClass("animated fadeOut");
                        st.remove()
                    }, 1e3)
                } else {
                    st.addClass("animated fadeOut");
                    st.remove()
                }
            }, st, !0);
            $env.homePage = m;
            $env.getCookie("initClz") && (f = function () {
                $(document.body).find("#initBg").hide();
                window.removeEventListener("popstate", f)
            }
                ,
                window.addEventListener("popstate", f));
            !$env.getCookie("initClz") && $(document.body).find("#initBg").hide();
            xssInputListener();
            m.appendTo($(document.body))
        }).fail(function (e) {
            $env.errorCatch(e)
        })
    }
    function handleWxFontSetting() {
        function handleFontSize() {
            WeixinJSBridge.invoke("setFontSizeCallback", {
                fontSize: 0
            });
            WeixinJSBridge.on("menu:setfont", function () {
                WeixinJSBridge.invoke("setFontSizeCallback", {
                    fontSize: 0
                })
            })
        }
        if (typeof WeixinJSBridge == "object" && typeof WeixinJSBridge.invoke == "function") {
            handleFontSize()
        } else {
            if (document.addEventListener) {
                document.addEventListener("WeixinJSBridgeReady", handleFontSize, false)
            } else if (document.attachEvent) {
                document.attachEvent("WeixinJSBridgeReady", handleFontSize);
                document.attachEvent("onWeixinJSBridgeReady", handleFontSize)
            }
        }
    }
}
    ;
