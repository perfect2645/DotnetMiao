var e = getApp();

Page({
    data: {
        canIUse: wx.canIUse("button.open-type.getUserInfo"),
        canIUseGetUserProfile: !1,
        canIUseOpenData: wx.canIUse("open-data.type.userAvatarUrl") && wx.canIUse("open-data.type.userNickName"),
        isHide: !1,
        showphonebtn: !1,
        uid: wx.getStorageSync("uid"),
        session_key: wx.getStorageSync("session_key"),
        phoneNumber: wx.getStorageSync("phoneNumber")
    },
    onLoad: function(e) {
        var t = this;
        wx.getSetting({
            success: function(e) {
                e.authSetting["scope.userInfo"] ? t.setData({
                    isHide: !1
                }) : t.setData({
                    isHide: !0
                });
            }
        });
    },
    getUserInfo3: function() {
        wx.login({
            success: function(t) {
                wx.getUserInfo({
                    desc: "获取你的昵称、头像、地区及性别",
                    success: function(a) {
                        wx.request({
                            url: e.globalData.apiUrl + "portal/user/WxDecode",
                            method: "POST",
                            header: {
                                "content-type": "application/x-www-form-urlencoded"
                            },
                            data: {
                                encryptedData: a.encryptedData,
                                signature: a.signature,
                                rawData: a.rawData,
                                iv: a.iv,
                                code: t.code
                            },
                            success: function(e) {
                                if (0 == e.data.status) {
                                    var t = JSON.parse(e.data.msg);
                                    console.log(t), that.setData({
                                        isHide: !1
                                    });
                                }
                            },
                            fail: function() {
                                that.showModal("获取授权信息失败");
                            }
                        });
                    }
                });
            }
        });
    },
    getPhoneNumber: function(t) {
        var a = this, o = wx.getStorageSync("myurl");
        console.log(t), "getPhoneNumber:fail" == t.detail.errMsg ? a.showModal("未获取到手机号码") : null != t.detail.iv && t.detail.iv ? wx.request({
            url: e.globalData.apiUrl + "index/getphoneNumber",
            method: "POST",
            header: {
                "content-type": "application/x-www-form-urlencoded"
            },
            data: {
                uid: wx.getStorageSync("uid"),
                encryptedData: t.detail.encryptedData,
                iv: t.detail.iv,
                session_key: wx.getStorageSync("session_key")
            },
            success: function(e) {
                console.log(e);
                var t = e.data.data;
                console.log(e.data), wx.setStorageSync("phoneNumber", t.phoneNumber), o ? (wx.redirectTo({
                    url: o
                }), wx.setStorageSync("myurl", "")) : wx.reLaunch({
                    url: "../index/index"
                });
            },
            fail: function() {
                a.showModal("获取手机号失败");
            }
        }) : a.showModal("授权失败");
    },
    getUserInfo: function() {
        var t = this, a = wx.getStorageSync("myurl");
        wx.getUserProfile({
            desc: "获取你的昵称、头像、地区及性别",
            success: function(o) {
                console.log(o), o.userInfo ? wx.login({
                    success: function(n) {
                        console.log(n), wx.request({
                            url: e.globalData.apiUrl + "index/WxDecode",
                            method: "POST",
                            header: {
                                "content-type": "application/x-www-form-urlencoded"
                            },
                            data: {
                                encryptedData: o.encryptedData,
                                signature: o.signature,
                                rawData: o.rawData,
                                iv: o.iv,
                                code: n.code
                            },
                            success: function(e) {
                                var o = e.data.data;
                                console.log(e.data), wx.setStorageSync("uid", o.uid), wx.setStorageSync("avatarUrl", o.avatarUrl), 
                                wx.setStorageSync("random_number", o.random_number), wx.setStorageSync("session_key", o.sessionKey), 
                                o.phoneNumber ? (wx.setStorageSync("phoneNumber", o.phoneNumber), console.log("登录：" + a), 
                                a ? (wx.redirectTo({
                                    url: a
                                }), wx.setStorageSync("myurl", "")) : wx.reLaunch({
                                    url: "../index/index"
                                })) : t.setData({
                                    showphonebtn: !0
                                });
                            },
                            fail: function() {
                                t.showModal("获取授权信息失败");
                            }
                        });
                    }
                }) : t.showModal("请授权后使用小程序");
            }
        });
    },
    bindGetUserInfo: function(t) {
        var a = this;
        console.log(t), t.detail.userInfo ? wx.login({
            success: function(o) {
                wx.request({
                    url: e.globalData.apiUrl + "portal/user/WxDecode",
                    method: "POST",
                    header: {
                        "content-type": "application/x-www-form-urlencoded"
                    },
                    data: {
                        encryptedData: t.detail.encryptedData,
                        signature: t.detail.signature,
                        rawData: t.detail.rawData,
                        iv: t.detail.iv,
                        code: o.code
                    },
                    success: function(e) {
                        if (0 == e.data.status) {
                            var t = JSON.parse(e.data.msg);
                            console.log(t), a.setData({
                                isHide: !1
                            });
                        }
                    },
                    fail: function() {
                        a.showModal("获取授权信息失败");
                    }
                });
            }
        }) : a.showModal("请授权后使用小程序");
    },
    showModal: function(e) {
        wx.showModal({
            title: "提示",
            content: e,
            showCancel: !1,
            confirmText: "返回授权",
            success: function(e) {
                e.confirm && console.log("用户点击了“返回授权”");
            }
        });
    },
    getUserInfo2: function(e) {
        console.log(e), this.setData({
            userInfo: e.detail.userInfo,
            hasUserInfo: !0
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