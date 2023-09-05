var t = getApp();

Page({
    data: {
        status: 1,
        tab: 1,
        makeList: [],
        count: 0
    },
    getTab: function(t) {
        var a = t.currentTarget.dataset.status;
        this.setData({
            status: a
        }), this.getMakeList();
    },
    onLoad: function(t) {
        this.getMakeList();
    },
    getMakeList: function(a) {
        wx.showLoading({
            title: "正在加载"
        });
        var n = this, e = this.data.status, o = wx.getStorageSync("uid");
        wx.request({
            url: t.globalData.apiUrl + "index/userorderlist",
            data: {
                status: e,
                uid: o
            },
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data.data);
                var a = t.data.data.length;
                n.setData({
                    makeList: t.data.data,
                    count: a
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
    getUrl: function() {
        wx.switchTab({
            url: "../index/index"
        });
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {}
});