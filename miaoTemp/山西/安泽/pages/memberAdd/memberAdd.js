var t = getApp();

Page({
    data: {
        username: "",
        phone: "",
        idNumber: "",
        facePhoto: [],
        backPhoto: [],
        fileList: []
    },
    onLoad: function(t) {},
    bindUsernameInput: function(t) {
        console.log(t.detail.value), this.setData({
            username: t.detail.value
        });
    },
    bindPhoneInput: function(t) {
        this.setData({
            phone: t.detail.value
        });
    },
    bindIdNumberInput: function(t) {
        this.setData({
            idNumber: t.detail.value
        });
    },
    signSubmit: function(a) {
        var e = this.data.username, i = this.data.phone, o = this.data.idNumber, n = this.data.facePhoto, s = this.data.backPhoto, l = this.data.fileList;
        if (!e) return this.tishi(e, "请输入姓名"), !1;
        if (!i) return this.tishi(i, "请输入手机号"), !1;
        if (11 != i.length) return this.tishi(i, "请输入正确的手机号"), !1;
        if (!o) return this.tishi(o, "请输入身份证号"), !1;
        if (0 == n.length) return this.tishi(n, "请上传本人正面照"), !1;
        if (0 == s.length) return this.tishi(s, "请上传本人国徽面照"), !1;
        new Object().facePhoto = n, new Object().backPhoto = s, new Object().fileList = l;
        var c = JSON.stringify(n), d = JSON.stringify(s), u = JSON.stringify(l), r = wx.getStorageSync("uid");
        wx.request({
            url: t.globalData.apiUrl + "index/edit",
            data: {
                uid: r,
                truename: e,
                phoneNumber: i,
                idcard: o,
                images: c,
                imagesf: d,
                image: u
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                wx.hideLoading(), console.log(t.data);
                var a = t.data.code, e = t.data.msg;
                1 == a ? wx.showToast({
                    title: e,
                    icon: "none",
                    duration: 2e3,
                    success: function() {
                        setTimeout(function() {
                            wx.redirectTo({
                                url: "../memberInfo/memberInfo"
                            });
                        }, 2e3);
                    }
                }) : wx.showToast({
                    title: e,
                    icon: "none",
                    duration: 2e3
                });
            },
            complete: function() {}
        });
    },
    tishi: function(t, a) {
        wx.showToast({
            title: a,
            icon: "none",
            duration: 2e3
        });
    },
    afterReadFace: function(a) {
        var e = this, i = a.detail.file;
        wx.uploadFile({
            url: t.globalData.apiUrl + "index/uploadimg",
            filePath: i.url,
            name: "file",
            formData: {
                user: "test"
            },
            success: function(t) {
                console.log(t.data);
                var a = e.data.facePhoto, i = JSON.parse(t.data);
                a.push(i.data), e.setData({
                    facePhoto: a
                });
            }
        });
    },
    afterReadBack: function(a) {
        var e = this, i = a.detail.file;
        wx.uploadFile({
            url: t.globalData.apiUrl + "index/uploadimg",
            filePath: i.url,
            name: "file",
            formData: {
                user: "test"
            },
            success: function(t) {
                console.log(t.data);
                var a = e.data.backPhoto, i = JSON.parse(t.data);
                a.push(i.data), e.setData({
                    backPhoto: a
                });
            }
        });
    },
    afterReadFile: function(a) {
        var e = this, i = a.detail.file;
        wx.uploadFile({
            url: t.globalData.apiUrl + "index/uploadimg",
            filePath: i.url,
            name: "file",
            formData: {
                user: "test"
            },
            success: function(t) {
                console.log(t.data);
                var a = e.data.fileList, i = JSON.parse(t.data);
                a.push(i.data), e.setData({
                    fileList: a
                });
            }
        });
    },
    deleteFileClick: function(t) {
        var a = t.detail.index, e = this.data.fileList;
        e.splice(a, 1), console.log("删除图片的", e), this.setData({
            fileList: e
        }), console.log(this.data.fileList);
    },
    deleteBackClick: function(t) {
        var a = t.detail.index, e = this.data.backPhoto;
        e.splice(a, 1), console.log("删除图片的", e), this.setData({
            backPhoto: e
        }), console.log(this.data.facePhoto);
    },
    deleteFaceClick: function(t) {
        var a = t.detail.index, e = this.data.facePhoto;
        e.splice(a, 1), console.log("删除图片的", e), this.setData({
            facePhoto: e
        }), console.log(this.data.facePhoto);
    },
    onReady: function() {},
    onShow: function() {},
    onHide: function() {},
    onUnload: function() {},
    onPullDownRefresh: function() {},
    onReachBottom: function() {},
    onShareAppMessage: function() {}
});