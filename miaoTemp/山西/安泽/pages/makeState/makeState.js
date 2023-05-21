Page({
    data: {},
    urlTab: function(n) {
        var o = n.currentTarget.dataset.url;
        wx.switchTab({
            url: o
        });
    },
    onLoad: function(n) {},
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});