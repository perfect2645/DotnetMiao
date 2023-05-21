App({
    onLaunch: function() {
        var t = wx.getStorageSync("logs") || [];
        t.unshift(Date.now()), wx.setStorageSync("logs", t), wx.login({
            success: function(t) {}
        });
    },
    globalData: {
        userInfo: null,
        apiUrl: "https://yuyue.azjkzx.cn/api/",
        webUrlt: "https://yuyue.azjkzx.cn/"
    }
});