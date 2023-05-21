var e = getApp();

Page({
    data: {
        result: []
    },
    onLoad: function(e) {
        wx.getStorageSync("uid") && this.getMerberInfo();
    },
    getUrl: function() {
        wx.navigateTo({
            url: "../memberEdit/memberEdit"
        });
    },
    getMerberInfo: function(t) {
        wx.showLoading({
            title: "正在加载"
        });
        var n = this;
        wx.request({
            url: e.globalData.apiUrl + "index/getMemberInfo",
            data: {
                uid: wx.getStorageSync("uid")
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(e) {
                console.log(e.data), 2 == e.data.data.isdisable ? wx.showToast({
                    title: "您的账号被锁定",
                    icon: "none",
                    duration: 1e3,
                    success: function() {
                        setTimeout(function() {
                            wx.switchTab({
                                url: "../member/member"
                            });
                        }, 3e3);
                    }
                }) : 1 == e.data.data.checked && 2 == e.data.data.islocal ? wx.showToast({
                    title: "您的账号正在审核中",
                    icon: "none",
                    duration: 1e3,
                    success: function() {
                        setTimeout(function() {
                            wx.switchTab({
                                url: "../member/member"
                            });
                        }, 3e3);
                    }
                }) : 3 == e.data.data.checked && wx.showToast({
                    title: "您的账号审核未通过",
                    icon: "none",
                    duration: 1e3,
                    success: function() {
                        setTimeout(function() {
                            wx.switchTab({
                                url: "../member/member"
                            });
                        }, 3e3);
                    }
                }), n.setData({
                    result: e.data.data
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    onReady: function() {},
    onShow: function() {
        wx.getStorageSync("uid") && this.getMerberInfo();
    },
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {}
});