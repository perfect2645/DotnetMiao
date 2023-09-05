var t = getApp();

Page({
    data: {
        result: [],
        code: 0
    },
    getUrl: function(t) {
        var n = t.currentTarget.dataset.url;
        if (1 == t.currentTarget.dataset.status) {
            if (!wx.getStorageSync("uid")) return wx.showModal({
                title: "温馨提示",
                content: "您还未授权登录，请先授权登录",
                success: function(t) {
                    t.confirm ? wx.reLaunch({
                        url: "../login/login"
                    }) : t.cancel && console.log("用户点击取消");
                }
            }), !1;
            wx.navigateTo({
                url: n
            });
        } else wx.navigateTo({
            url: n
        });
    },
    onLoad: function(t) {
        wx.getStorageSync("uid") && this.getMerberInfo();
    },
    getMerberInfo: function(n) {
        wx.showLoading({
            title: "正在加载"
        });
        var e = this;
        wx.request({
            url: t.globalData.apiUrl + "index/getMemberInfo",
            data: {
                uid: wx.getStorageSync("uid")
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data), e.setData({
                    result: t.data.data,
                    code: t.data.code
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    getLogin: function(t) {
        if (!wx.getStorageSync("uid")) return wx.reLaunch({
            url: "../login/login"
        }), !1;
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});