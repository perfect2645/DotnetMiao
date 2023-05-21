var t = require("../../utils/util.js");

Page({
    data: {
        logs: []
    },
    onLoad: function() {
        this.setData({
            logs: (wx.getStorageSync("logs") || []).map(function(a) {
                return {
                    date: t.formatTime(new Date(a)),
                    timeStamp: a
                };
            })
        });
    }
});