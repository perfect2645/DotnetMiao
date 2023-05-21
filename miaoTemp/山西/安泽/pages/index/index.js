var t = getApp(), a = require("../../wxParse/wxParse.js");

Page({
    data: {
        makeId: 0,
        kefuAct: 0,
        ids: 1,
        type: [],
        banner: [],
        notice: [],
        makeList: [],
        indicatorDots: !0,
        vertical: !1,
        autoplay: !1,
        interval: 2e3,
        duration: 500,
        text: "",
        imgCode: "",
        isSuccess: !0
    },
    getTypeTab: function(t) {
        var a = t.currentTarget.dataset.id;
        this.setData({
            ids: a
        }), this.getMakeList();
    },
    getUrl: function(t) {
        var a = t.currentTarget.dataset.id;
        1 == t.currentTarget.dataset.status && this.setData({
            kefuAct: 1,
            makeId: a
        });
    },
    getYzm: function(t) {
        var a = this.data.text;
        console.log(a.toLowerCase());
        var e = this.data.imgCode;
        e || wx.showToast({
            title: "请输入图形验证码",
            icon: "none"
        }), e != a ? wx.showToast({
            title: "请输入正确的验证码",
            icon: "none"
        }) : this.setData({
            isSuccess: !1
        });
    },
    getZx: function(t) {
        var a = this.data.makeId;
        wx.navigateTo({
            url: "../makeDetail/makeDetail?id=" + a
        });
    },
    codeImg: function(t) {
        this.setData({
            imgCode: t.detail.value
        });
    },
    getWebUrl: function(t) {
        var a = t.currentTarget.dataset.id;
        wx.navigateTo({
            url: "../webUrl/webUrl?id=" + a
        });
    },
    getLinkurl: function(t) {
        wx.navigateTo({
            url: "../webUrl/webUrl?id=6"
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
    onLoad: function(t) {
        this.getType(), this.getBanner(), this.getNotice(), this.getGsNotice(), this.getMakeList();
        drawPic(this);
    },
    change: function() {
        drawPic(this);
    },
    getType: function(a) {
        var e = this;
        wx.request({
            url: t.globalData.apiUrl + "index/type",
            data: {},
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data), e.setData({
                    type: t.data.data
                });
            },
            complete: function() {}
        });
    },
    getBanner: function(a) {
        var e = this;
        this.data.pid;
        wx.request({
            url: t.globalData.apiUrl + "index/bannerlist",
            data: {},
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data), e.setData({
                    banner: t.data.data
                });
            },
            complete: function() {}
        });
    },
    getNotice: function(e) {
        var n = this;
        wx.request({
            url: t.globalData.apiUrl + "index/newslist",
            data: {},
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data);
                var e = t.data.data.content;
                a.wxParse("article", "html", e, n, 0);
            },
            complete: function() {}
        });
    },
    getGsNotice: function(e) {
        var n = this;
        wx.request({
            url: t.globalData.apiUrl + "index/gglist",
            data: {},
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data);
                var e = t.data.data.content;
                a.wxParse("article2", "html", e, n, 0);
            },
            complete: function() {}
        });
    },
    getMakeList: function(a) {
        wx.showLoading({
            title: "正在加载"
        });
        var e = this, n = this.data.ids;
        wx.request({
            url: t.globalData.apiUrl + "index/yimiaolist",
            data: {
                ids: n
            },
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data), e.setData({
                    makeList: t.data.data
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    onReady: function() {},
    onShow: function() {
        this.setData({
            kefuAct: 0
        });
    },
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});