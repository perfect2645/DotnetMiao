var t, e = getApp(), a = require("../../wxParse/wxParse.js");

function n(t, e) {
    return Math.floor(Math.random() * (e - t) + t);
}

function o(t, e) {
    return "rgb(" + n(t, e) + "," + n(t, e) + "," + n(t, e) + ")";
}

function i(e) {
    (t = wx.createCanvasContext("canvas")).fillStyle = o(180, 240), t.fillRect(0, 0, 90, 28);
    for (var a = "", i = "ABCEFGHJKLMNPQRSTWXY123456789", s = 0; s < 4; s++) {
        var c = i[n(0, i.length)];
        t.fillStyle = o(50, 160), t.font = n(20, 26) + "px SimHei";
        var r = 5 + 20 * s, l = n(20, 25), u = n(-20, 20);
        t.translate(r, l), t.rotate(u * Math.PI / 180), t.fillText(c, 5, 0), a += c, t.rotate(-u * Math.PI / 180), 
        t.translate(-r, -l);
    }
    for (s = 0; s < 4; s++) t.strokeStyle = o(40, 180), t.beginPath(), t.moveTo(n(0, 90), n(0, 28)), 
    t.lineTo(n(0, 90), n(0, 28)), t.stroke();
    for (s = 0; s < 20; s++) t.fillStyle = o(0, 255), t.beginPath(), t.arc(n(0, 90), n(0, 28), 1, 0, 2 * Math.PI), 
    t.fill();
    t.draw(!1, function() {
        e.setData({
            text: a
        });
    });
}

Page({
    data: {
        successInfo: "恭喜您预约成功,请尽快到指定地点打疫苗。",
        code: 10,
        msg: "",
        indicatorDots: !0,
        indicatorColor: "#ffffff",
        indicatorActiveColor: "#a2c4fc",
        vertical: !1,
        autoplay: !0,
        interval: 5e3,
        duration: 500,
        circular: !0,
        Height: "",
        statusBarHeight: 42,
        nheight_: 32,
        lineTop: 60,
        text: "",
        imgCode: "",
        isSuccess: !1,
        result: [],
        truename: ""
    },
    getUrl: function(t) {
        wx.navigateTo({
            url: "../makeState/makeState"
        });
    },
    getYzm: function(t) {
        var a = this.data.text.toLowerCase(), n = this.data.imgCode;
        if (n || wx.showToast({
            title: "请输入图形验证码",
            icon: "none"
        }), n != a) wx.showToast({
            title: "请输入正确的验证码",
            icon: "none"
        }); else {
            this.setData({
                isSuccess: !1
            });
            var o = t.currentTarget.dataset.id, i = wx.getStorageSync("uid"), s = "../makeDetail/makeDetail?id=" + o, c = this.data.truename;
            if (!wx.getStorageSync("uid")) return wx.showModal({
                title: "温馨提示",
                content: "您还未授权登录，请先授权登录",
                success: function(t) {
                    t.confirm ? (wx.setStorageSync("myurl", s), wx.navigateTo({
                        url: "../login/login"
                    })) : t.cancel && console.log("用户点击取消");
                }
            }), !1;
            if (!c) return wx.showModal({
                title: "温馨提示",
                content: "您未完善资料，请先完善资料",
                success: function(t) {
                    t.confirm ? wx.navigateTo({
                        url: "../memberEdit/memberEdit"
                    }) : t.cancel && console.log("用户点击取消");
                }
            }), !1;
            var r = this;
            wx.request({
                url: e.globalData.apiUrl + "index/addorder",
                data: {
                    ids: o,
                    uid: i
                },
                method: "POST",
                header: {
                    "content-type": "application/json"
                },
                success: function(t) {
                    console.log(t.data), r.setData({
                        code: t.data.code
                    }), wx.showToast({
                        title: t.data.msg,
                        icon: "none",
                        duration: 5e3
                    });
                },
                complete: function() {}
            });
        }
    },
    codeImg: function(t) {
        this.setData({
            imgCode: t.detail.value
        });
    },
    changeIndicatorDots: function() {
        this.setData({
            indicatorDots: !this.data.indicatorDots
        });
    },
    changeAutoplay: function() {
        this.setData({
            autoplay: !this.data.autoplay
        });
    },
    intervalChange: function(t) {
        this.setData({
            interval: t.detail.value
        });
    },
    durationChange: function(t) {
        this.setData({
            duration: t.detail.value
        });
    },
    imgHeight: function(t) {
        var e = wx.getSystemInfoSync().windowWidth * t.detail.height / t.detail.width * 1 + "px";
        this.setData({
            Height: e
        });
    },
    onLoad: function(t) {
        var e = t.id;
        this.getNewsOne(e), this.getSuccess();
        i(this), wx.getStorageSync("uid") && this.getMerberInfo();
    },
    change: function() {
        i(this);
    },
    getMerberInfo: function(t) {
        var a = this;
        wx.request({
            url: e.globalData.apiUrl + "index/getMemberInfo",
            data: {
                uid: wx.getStorageSync("uid")
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data), a.setData({
                    truename: t.data.data.truename
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    getAddInfo: function(t) {
        this.setData({
            isSuccess: !0
        });
    },
    getNewsOne: function(t) {
        wx.showLoading({
            title: "正在加载"
        });
        var n = this;
        wx.request({
            url: e.globalData.apiUrl + "index/yimiaodetails",
            data: {
                ids: t
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data), n.setData({
                    result: t.data.data
                });
                var e = t.data.data.content;
                a.wxParse("article", "html", e, n, 0);
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    getSuccess: function() {
        var t = this;
        wx.request({
            url: e.globalData.apiUrl + "index/wenzitips",
            data: {},
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(e) {
                console.log(e.data), t.setData({
                    successInfo: e.data.data
                });
            },
            complete: function() {}
        });
    },
    urlTab: function(t) {
        var e = t.currentTarget.dataset.url;
        wx.switchTab({
            url: e
        });
    },
    onReady: function() {
        console.log(wx.getMenuButtonBoundingClientRect());
        var t = wx.getMenuButtonBoundingClientRect(), e = t.height, a = t.top, n = a + e + 15;
        this.setData({
            statusBarHeight: a,
            nheight: e,
            lineTop: n
        });
    },
    onShow: function() {
        wx.getStorageSync("uid") && this.getMerberInfo();
    },
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {}
});