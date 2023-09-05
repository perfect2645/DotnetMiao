var n = getApp();

Page({
    data: {
        url: ""
    },
    onLoad: function(o) {
        var t = o.id, a = n.globalData.webUrlt + "index/index/detail/ids/" + t;
        this.setData({
            url: a
        });
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});