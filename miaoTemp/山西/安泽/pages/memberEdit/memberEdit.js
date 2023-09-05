var t = getApp();

Page({
    data: {
        result: [],
        username: "",
        phone: "",
        idNumber: "",
        facePhoto: [],
        backPhoto: [],
        fileList: [],
        columns: [ "杭州", "宁波", "温州", "嘉兴", "湖州" ],
        gender: "0",
        address: "",
        numberlimitnutips: "您好，个人资料只允许修改3次，请谨慎修改",
        islocal: "0"
    },
    onChange: function(t) {
        this.setData({
            gender: t.detail
        });
    },
    onChangelocal: function(t) {
        this.setData({
            islocal: t.detail
        });
    },
    onLoad: function(t) {
        this.getSuccess(), wx.getStorageSync("uid") && this.getMerberInfo();
    },
    getSuccess: function() {
        var a = this;
        wx.request({
            url: t.globalData.apiUrl + "index/numberlimittips",
            data: {},
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                console.log(t.data), a.setData({
                    numberlimitnutips: t.data.data
                });
            },
            complete: function() {}
        });
    },
    getMerberInfo: function(a) {
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
                    username: t.data.data.truename,
                    phone: t.data.data.phoneNumber,
                    idNumber: t.data.data.idcard,
                    facePhoto: t.data.data.images1,
                    backPhoto: t.data.data.imagesf1,
                    fileList: t.data.data.image1,
                    address: t.data.data.address,
                    gender: t.data.data.gender,
                    islocal: t.data.data.islocal
                });
            },
            complete: function() {
                wx.hideLoading();
            }
        });
    },
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
    bindAddressInput: function(t) {
        this.setData({
            address: t.detail.value
        });
    },
    signSubmit: function(a) {
        var e = this.data.username, i = this.data.gender, o = this.data.islocal, n = this.data.phone, s = this.data.idNumber, d = this.data.address, l = this.data.facePhoto, c = this.data.backPhoto, u = this.data.fileList;
        if (!e) return this.tishi(e, "请输入姓名"), !1;
        if (1 != i && 2 != i) return this.tishi(i, "请选择性别"), !1;
        if (1 != o && 2 != o) return this.tishi(o, "请选择是否为本地"), !1;
        if (!s) return this.tishi(s, "请输入身份证号"), !1;
        if (!d) return this.tishi(d, "请输入工作单位或者家庭住址"), !1;
        if (0 == l.length) return this.tishi(l, "请上传本人正面照"), !1;
        if (0 == c.length) return this.tishi(c, "请上传本人国徽面照"), !1;
        new Object().facePhoto = l, new Object().backPhoto = c, new Object().fileList = u;
        var r = JSON.stringify(l), h = JSON.stringify(c), f = JSON.stringify(u), g = wx.getStorageSync("uid");
        wx.request({
            url: t.globalData.apiUrl + "index/edit",
            data: {
                uid: g,
                truename: e,
                gender: i,
                islocal: o,
                phoneNumber: n,
                idcard: s,
                images: r,
                imagesf: h,
                image: f,
                address: d
            },
            method: "POST",
            header: {
                "content-type": "application/json"
            },
            success: function(t) {
                wx.hideLoading(), console.log(t.data);
                t.data.code;
                var a = t.data.msg;
                wx.showToast({
                    title: a,
                    icon: "none",
                    duration: 2e3,
                    success: function() {
                        setTimeout(function() {
                            wx.switchTab({
                                url: "../member/member"
                            });
                        }, 2e3);
                    }
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
        }), console.log(this.data.facePhoto);
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
    onReachBottom: function() {}
});