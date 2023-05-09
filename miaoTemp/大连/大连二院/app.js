require("./runtime"), require("./common"), require("./vendors"), require("./taro"), 
(wx["webpackJsonp"] = wx["webpackJsonp"] || []).push([ [ 13 ], {
    679: function(e, t, a) {},
    692: function(e, t, a) {},
    761: function(e, t, a) {
        "use strict";
        a.r(t);
        var n = {};
        a.r(n), a.d(n, "global", function() {
            return w;
        }), a.d(n, "search", function() {
            return D;
        }), a.d(n, "docInfo", function() {
            return P;
        }), a.d(n, "pharmacistDocInfo", function() {
            return C;
        }), a.d(n, "netnurseVisiting", function() {
            return E;
        }), a.d(n, "confirm", function() {
            return N;
        }), a.d(n, "modulesContentDetails", function() {
            return B;
        }), a.d(n, "modulesContentDesign", function() {
            return q;
        });
        a(645);
        var r = a(12), i = a(473), c = a(3), s = a.n(c), o = a(17), u = a(18), d = a(23), p = a(19), l = a(20), f = a(10), v = a(2), g = a(14), b = a(7), x = a(13), h = a(28), m = (a(679), 
        a(25)), y = a(154), j = a(4), O = a(8), w = {
            namespace: "global",
            state: {
                appLaunchQuery: {},
                systemTips: {}
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                }
            },
            effects: {
                getSystemTips: function(e, t) {
                    var a = t.put, n = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, r, i, c, s, o;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, b["b"]("/api/cfg/getpagereminder", {}, {
                                    hideError: !0,
                                    hideLoading: !0,
                                    noAuthOn999: !0,
                                    noThrowError: !0
                                });

                              case 2:
                                if (t = e.sent, r = t.code, i = t.data, 0 !== r) {
                                    e.next = 12;
                                    break;
                                }
                                return c = n(function(e) {
                                    return e.global.appLaunchQuery;
                                }), s = {}, o = "trial" === c.version ? "preContent" : "content", i.forEach(function(e) {
                                    var t;
                                    s[e.remindKey] = null !== (t = e[o]) && void 0 !== t ? t : "";
                                }), e.next = 12, a({
                                    type: "save",
                                    payload: {
                                        systemTips: s
                                    }
                                });

                              case 12:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        }, D = {
            namespace: "search",
            state: {
                keyWord: "",
                depts: [],
                doctors: [],
                pageParam: {},
                deptPageCount: 1,
                doctorPageCount: 1
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                }
            },
            effects: {
                getSearchResult: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, s, o, u, d, p, l, f, v, g, x, h, m, y, O, w, D, S, k, L, I, P;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                if ("" === a.val) {
                                    e.next = 7;
                                    break;
                                }
                                return D = [ "INQUIRY", "1", "2", "14" ].includes(a.systemCode) ? "INQUIRY" : a.systemCode, 
                                e.next = 4, r(function() {
                                    return b["b"]("/api/mix/patient/search", {
                                        systemCode: D,
                                        inputData: a.val,
                                        deptPageNum: a.deptPageNum,
                                        doctorPageNum: a.doctorPageNum,
                                        deptPageSize: a.deptPageSize || 10,
                                        doctorPageSize: a.doctorPageSize || 10
                                    });
                                });

                              case 4:
                                S = e.sent, k = S.data, w = k;

                              case 7:
                                return e.next = 9, i(function(e) {
                                    return e.search;
                                });

                              case 9:
                                return L = e.sent, I = L.depts, P = L.doctors, e.next = 14, n({
                                    type: "save",
                                    payload: {
                                        keyWord: a.val,
                                        depts: a.deptPageNum > 1 ? I.concat(null !== (t = null === (c = w) || void 0 === c ? void 0 : c.depts) && void 0 !== t ? t : []) : null !== (s = null === (o = w) || void 0 === o ? void 0 : o.depts) && void 0 !== s ? s : [],
                                        doctors: a.doctorPageNum > 1 ? P.concat(null !== (u = null === (d = w) || void 0 === d ? void 0 : d.doctors) && void 0 !== u ? u : []) : null !== (p = null === (l = w) || void 0 === l ? void 0 : l.doctors) && void 0 !== p ? p : [],
                                        pageParam: {
                                            deptPageCount: null !== (f = null === (v = w) || void 0 === v ? void 0 : v.deptPageCount) && void 0 !== f ? f : 1,
                                            doctorPageCount: null !== (g = null === (x = w) || void 0 === x ? void 0 : x.doctorPageCount) && void 0 !== g ? g : 1,
                                            deptPageNum: null !== (h = null === (m = w) || void 0 === m ? void 0 : m.deptPageNum) && void 0 !== h ? h : 1,
                                            doctorPageNum: null !== (y = null === (O = w) || void 0 === O ? void 0 : O.doctorPageNum) && void 0 !== y ? y : 1
                                        }
                                    }
                                });

                              case 14:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        }, S = a(235), k = a(53), L = [ "regBizType" ], I = [ "regBizType" ], P = {
            namespace: "docInfo",
            state: {
                internetDeptId: "",
                bizTypeArr: [],
                tabList: [],
                docDetail: {},
                appraiseList: [],
                impressData: [],
                appraisalTag: [],
                collectStatus: "",
                nullDateArr: [],
                scheduleList: {},
                sourcePool: {},
                pSelectedDay: "",
                doctorArticleList: [ {
                    contentName: "项目分类一",
                    id: 1,
                    type: 1
                }, {
                    contentName: "项目分类一",
                    id: 2,
                    type: 2
                }, {
                    contentName: "项目分类一",
                    id: 3,
                    type: 3
                } ]
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                },
                clear: function() {
                    return {
                        internetDeptId: "",
                        bizTypeArr: [],
                        tabList: [],
                        docDetail: {},
                        appraiseList: [],
                        impressData: [],
                        appraisalTag: [],
                        collectStatus: "",
                        nullDateArr: [],
                        scheduleList: {},
                        sourcePool: {},
                        pSelectedDay: "",
                        doctorArticleList: []
                    };
                }
            },
            effects: {
                changeFav: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/mix/patient/user/collect/doctor", Object(O["a"])({}, a));
                                });

                              case 2:
                                return e.next = 4, n({
                                    type: "save",
                                    payload: {
                                        collectStatus: a.status
                                    }
                                });

                              case 4:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getDocDetail: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, s, o, u, d, p, l;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/mix/patient/doctor/detail", a);
                                });

                              case 2:
                                return s = e.sent, o = s.data, u = null !== (t = o.deptDoctorRespList) && void 0 !== t ? t : [], 
                                d = (null !== (c = u.find(function(e) {
                                    return "WZ" === e.deptType;
                                })) && void 0 !== c ? c : {}).deptNo || "", console.log("jjj", o, k["d"]), p = k["d"].filter(function(e) {
                                    return "1" === o[e.type];
                                }), l = [ {
                                    title: 1 === p.length ? p[0].title : "服务"
                                }, {
                                    title: "介绍"
                                } ], e.t0 = x["C"], e.t1 = JSON, e.next = 13, i(function(e) {
                                    return e.docInfo.doctorArticleList;
                                });

                              case 13:
                                return e.t2 = e.sent, e.t3 = e.t1.stringify.call(e.t1, e.t2), (0, e.t0)(e.t3), e.next = 18, 
                                n({
                                    type: "save",
                                    payload: {
                                        docDetail: null !== o && void 0 !== o ? o : {},
                                        bizTypeArr: p,
                                        tabList: l,
                                        internetDeptId: d,
                                        collectStatus: o.collectStatus || "0"
                                    }
                                });

                              case 18:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getAppraiseList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/appraisalList", Object(O["a"])({}, a));
                                });

                              case 2:
                                return i = e.sent, c = i.data, e.next = 6, n({
                                    type: "save",
                                    payload: {
                                        appraiseList: null !== (t = null === c || void 0 === c ? void 0 : c.recordList) && void 0 !== t ? t : []
                                    }
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getImpress: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s, o, u, d, p;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/getDoctorSignAndCount", Object(O["a"])({}, a), {
                                        returnDataType: "array"
                                    });
                                });

                              case 2:
                                for (o in t = e.sent, i = t.data, c = g["K"].impressArr, s = {}, c) s[o] = c[o];
                                return u = Object.keys(s).map(function(e) {
                                    return {
                                        tagName: s[e],
                                        signCount: 0
                                    };
                                }), d = i || [], p = u.map(function(e) {
                                    var t = (d.find(function(t) {
                                        return t.tagName == e.tagName;
                                    }) || {}).signCount || 0;
                                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                                        signCount: t
                                    });
                                }), e.next = 12, n({
                                    type: "save",
                                    payload: {
                                        impressData: null !== p && void 0 !== p ? p : u
                                    }
                                });

                              case 12:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getEvaluationCount: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/getEvaluationCount", Object(O["a"])({}, a), {
                                        returnDataType: "array"
                                    });
                                });

                              case 2:
                                return t = e.sent, i = t.data, c = [], s = [ "全部评价 ".concat(i.all), "好评 ".concat(i[1]), "中评 ".concat(i[2]), "差评 ".concat(i[3]) ], 
                                s.map(function(e, t) {
                                    var a = c[t] = 0 == t ? "".concat(e, "  ").concat(i.all) : "".concat(e, "  ").concat(i[t]);
                                    return a;
                                }), e.next = 9, n({
                                    type: "save",
                                    payload: {
                                        appraisalTag: s
                                    }
                                });

                              case 9:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getScheduleList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, o, u, d, p, l, v, g, h, m, y;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return t = a.regBizType, c = Object(S["a"])(a, L), o = s.a.getStorageSync("scheduleHisMode"), 
                                u = "2" == o && "REGISTER" !== t ? "/api/register/hlwyy/dateschedulelist" : "/api/register/dateschedulelist", 
                                e.next = 5, r(function() {
                                    return b["b"](u, Object(O["a"])({}, c), {
                                        isRouteApi: !0
                                    });
                                });

                              case 5:
                                return d = e.sent, p = d.data, l = p.scheduleList, v = void 0 === l ? [] : l, 0 === v.length && (v = Object(k["f"])().days), 
                                g = {
                                    "一": 0,
                                    "二": 1,
                                    "三": 2,
                                    "四": 3,
                                    "五": 4,
                                    "六": 5,
                                    "日": 6
                                }, h = (v[0] || {}).weekDate ? g[v[0].weekDate] : 0, m = [ 1, 2, 3, 4, 5, 6, 7 ].slice(0, h), 
                                "YY" === a.ghType && (v = v.slice(1)), e.t0 = x["C"], e.t1 = JSON, e.next = 17, 
                                i(function(e) {
                                    return e.docInfo.scheduleList;
                                });

                              case 17:
                                return e.t2 = e.sent, e.t3 = e.t1.stringify.call(e.t1, e.t2), y = (0, e.t0)(e.t3), 
                                y = Object(O["a"])(Object(O["a"])({}, y), {}, Object(f["a"])({}, a.deptId, v)), 
                                e.next = 23, n({
                                    type: "save",
                                    payload: {
                                        nullDateArr: m,
                                        scheduleList: Object(O["a"])(Object(O["a"])({}, y), {}, Object(f["a"])({}, a.deptId, v))
                                    }
                                });

                              case 23:
                                return e.abrupt("return", v);

                              case 24:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getDocListByDate: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, o, u, d, p, l, v, x, h, m, y;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return t = a.regBizType, c = Object(S["a"])(a, I), o = s.a.getStorageSync("scheduleHisMode"), 
                                u = "2" == o && "REGISTER" !== t ? "/api/register/hlwyy/schedulelist?_route=h".concat(g["A"].hisId) : "/api/register/schedulelist", 
                                e.next = 5, r(function() {
                                    return b["b"](u, Object(O["a"])({}, c), {
                                        isRouteApi: !0
                                    });
                                });

                              case 5:
                                if (d = e.sent, p = d.data, l = p.scheduleDate, v = void 0 === l ? "" : l, x = p.itemList, 
                                h = void 0 === x ? [] : x, a.scheduleDate === v) {
                                    e.next = 10;
                                    break;
                                }
                                return e.abrupt("return", !1);

                              case 10:
                                return e.next = 12, i(function(e) {
                                    return e.docInfo;
                                });

                              case 12:
                                return m = e.sent, y = {}, y[v] = h, e.next = 17, n({
                                    type: "save",
                                    payload: {
                                        sourcePool: Object(O["a"])(Object(O["a"])({}, m.sourcePool), {}, Object(f["a"])({}, a.deptId, y))
                                    }
                                });

                              case 17:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getWZScheduleList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, s, o;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, i(function(e) {
                                    return e.docInfo;
                                });

                              case 2:
                                return t = e.sent, e.next = 5, r(function() {
                                    return b["b"]("/api/ehis/schedule/scheduledoctorinfo", Object(O["a"])({}, a));
                                });

                              case 5:
                                return c = e.sent, s = c.data, o = Object(k["f"])(s, g["z"].SCHEDULE_DATE, t.docDetail, a.deptId), 
                                e.next = 10, n({
                                    type: "save",
                                    payload: {
                                        scheduleList: Object(x["C"])(JSON.stringify(Object(O["a"])(Object(O["a"])({}, t.scheduleList), {}, Object(f["a"])({}, a.deptId, o.days)))),
                                        sourcePool: Object(O["a"])(Object(O["a"])({}, t.sourcePool), {}, Object(f["a"])({}, a.deptId, o.tempSourcePool))
                                    }
                                });

                              case 10:
                                return e.abrupt("return", o);

                              case 11:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getInquiryConfigs: function(e, t) {
                    var a = e.payload, n = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, r, i;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, n(function() {
                                    return b["b"]("/api/ehis/health/api/inquiryConfig/inquiryConfigs", Object(O["a"])({}, a));
                                });

                              case 2:
                                return t = e.sent, r = t.data, i = void 0 === r ? [] : r, e.abrupt("return", i);

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getDoctorArticleaList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/educationContent/getDoctorContent", Object(O["a"])({}, a));
                                });

                              case 2:
                                return t = e.sent, i = t.data, c = void 0 === i ? [] : i, e.next = 7, n({
                                    type: "save",
                                    payload: {
                                        doctorArticleList: c || []
                                    }
                                });

                              case 7:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        }, C = {
            namespace: "pharmacistDocInfo",
            state: {
                docDetail: {},
                internetDeptId: "",
                appraiseList: [],
                impressData: [],
                appraisalTag: [],
                collectStatus: ""
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                },
                clear: function() {
                    return {
                        docDetail: {},
                        internetDeptId: "",
                        appraiseList: [],
                        impressData: [],
                        appraisalTag: [],
                        collectStatus: ""
                    };
                }
            },
            effects: {
                getDocInfo: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/doctor/doctor", Object(O["a"])({}, a));
                                });

                              case 2:
                                return c = e.sent, s = c.data, e.next = 6, n({
                                    type: "save",
                                    payload: {
                                        docDetail: Object(O["a"])(Object(O["a"])({}, s), {}, {
                                            tags: ((null === s || void 0 === s ? void 0 : s.goodAtLabelList) || []).map(function(e) {
                                                return e.name;
                                            }).join(",")
                                        }),
                                        internetDeptId: null !== (t = null === (i = s.doctor) || void 0 === i ? void 0 : i.deptId) && void 0 !== t ? t : "",
                                        tabList: [ {
                                            title: "服务"
                                        }, {
                                            title: "介绍"
                                        }, {
                                            title: "评价"
                                        } ]
                                    }
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getDocCollectStatus: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/mix/patient/pharmacist/detail", Object(O["a"])({}, a));
                                });

                              case 2:
                                return t = e.sent, i = t.data, e.next = 6, n({
                                    type: "save",
                                    payload: {
                                        collectStatus: i.collectStatus
                                    }
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                changeFav: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/mix/patient/user/collect/doctor", Object(O["a"])({}, a));
                                });

                              case 2:
                                return e.next = 4, n({
                                    type: "save",
                                    payload: {
                                        collectStatus: a.status
                                    }
                                });

                              case 4:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getAppraiseList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/appraisalList", Object(O["a"])({}, a));
                                });

                              case 2:
                                return i = e.sent, c = i.data, e.next = 6, n({
                                    type: "save",
                                    payload: {
                                        appraiseList: null !== (t = null === c || void 0 === c ? void 0 : c.recordList) && void 0 !== t ? t : []
                                    }
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getImpress: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s, o, u, d, p;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/getDoctorSignAndCount", Object(O["a"])({}, a), {
                                        returnDataType: "array"
                                    });
                                });

                              case 2:
                                for (o in t = e.sent, i = t.data, c = g["K"].impressArr, s = {}, c) s[o] = c[o];
                                return u = Object.keys(s).map(function(e) {
                                    return {
                                        tagName: s[e],
                                        signCount: 0
                                    };
                                }), d = i || [], p = u.map(function(e) {
                                    var t = (d.find(function(t) {
                                        return t.tagName == e.tagName;
                                    }) || {}).signCount || 0;
                                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                                        signCount: t
                                    });
                                }), e.next = 12, n({
                                    type: "save",
                                    payload: {
                                        impressData: null !== p && void 0 !== p ? p : u
                                    }
                                });

                              case 12:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getEvaluationCount: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/health/api/appraisal/getEvaluationCount", Object(O["a"])({}, a), {
                                        returnDataType: "array"
                                    });
                                });

                              case 2:
                                return t = e.sent, i = t.data, c = [], s = [ "全部评价", "好评", "中评", "差评" ], s.map(function(e, t) {
                                    var a = c[t] = 0 == t ? "".concat(e, "  ").concat(i.all) : "".concat(e, "  ").concat(i[t]);
                                    return a;
                                }), e.next = 9, n({
                                    type: "save",
                                    payload: {
                                        appraisalTag: s
                                    }
                                });

                              case 9:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        };
        function T(e) {
            if (null == e) throw new TypeError("Cannot destructure " + e);
        }
        function A(e) {
            var t = Object(O["a"])(Object(O["a"])({}, e), {}, {
                intro: Object(x["m"])(e.intro) || "用于常规或者特殊检测、辅助诊断疾病等",
                amount: "￥".concat(Object(x["p"])(e.amount || 0)),
                amountInfo: e.netProjectMaterialsList.map(function(e) {
                    return e.name;
                }).join("，")
            });
            return t.amountInfo && (t.amountInfo = "（含".concat(t.amountInfo, "）")), t;
        }
        var E = {
            namespace: "netnurseVisiting",
            state: {
                categoryList: [],
                serviceList: [],
                serviceDetail: {},
                addressList: {
                    use: [],
                    unUse: []
                },
                addressDetail: {},
                orderDetail: {}
            },
            reducers: {
                setCategoryList: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        categoryList: [].concat(a)
                    });
                },
                setServiceList: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        serviceList: [].concat(a)
                    });
                },
                setServiceDetail: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        serviceDetail: Object(O["a"])({}, a)
                    });
                },
                setAddressList: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        addressList: Object(O["a"])({}, a)
                    });
                },
                setAddressDetail: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        addressDetail: Object(O["a"])({}, a)
                    });
                },
                setOrderDetail: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), {}, {
                        orderDetail: Object(O["a"])({}, a)
                    });
                }
            },
            effects: {
                getCategoryList: function(e, t) {
                    T(e);
                    var a = t.put, n = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, r;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, n(function() {
                                    return b["b"]("/api/ehis/net/classify/getList", {
                                        type: "NET_NURSE"
                                    });
                                });

                              case 2:
                                return t = e.sent, r = t.data, e.next = 6, a({
                                    type: "setCategoryList",
                                    payload: (r || []).map(function(e) {
                                        return Object(O["a"])(Object(O["a"])({}, e), {}, {
                                            value: e.name,
                                            image: e.img
                                        });
                                    })
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getServiceList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/net/project/getListPageNetProject", Object(O["a"])({}, a));
                                });

                              case 2:
                                return t = e.sent, i = t.data, c = (null === i || void 0 === i ? void 0 : i.recordList) || [], 
                                e.next = 7, n({
                                    type: "setServiceList",
                                    payload: c.map(A)
                                });

                              case 7:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getServiceDetail: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/net/project/getNetProjectDetail", {
                                        id: a.id
                                    }, {
                                        showBack: !0
                                    });
                                });

                              case 2:
                                return t = e.sent, i = t.data, e.next = 6, n({
                                    type: "setServiceDetail",
                                    payload: Object(O["a"])({}, A(i))
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                clearServiceDetail: function(e, t) {
                    T(e);
                    var a = t.put;
                    return Object(j["a"])().mark(function e() {
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, a({
                                    type: "setServiceDetail",
                                    payload: {}
                                });

                              case 2:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getAddressList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/ehis/net/address/queryNetUserAddressSmall", {
                                        netProjectId: a.serviceId
                                    });
                                });

                              case 2:
                                return t = e.sent, i = t.data, e.next = 6, n({
                                    type: "setAddressList",
                                    payload: {
                                        use: i.useAddress,
                                        unUse: i.unUseAddress
                                    }
                                });

                              case 6:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                updateAddressDetail: function(e, t) {
                    var a = e.payload, n = t.put;
                    return Object(j["a"])().mark(function e() {
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, n({
                                    type: "setAddressDetail",
                                    payload: a
                                });

                              case 2:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                updateOrderDetail: function(e, t) {
                    var a = e.payload, n = t.put;
                    return Object(j["a"])().mark(function e() {
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, n({
                                    type: "setOrderDetail",
                                    payload: a
                                });

                              case 2:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        }, N = {
            namespace: "confirm",
            state: {
                leftBindNum: 0,
                cardList: [],
                activePatient: {},
                confirmData: {},
                scheduleList: [],
                sourcePool: {},
                detailData: {
                    pics: [],
                    content: "",
                    patientMobile: "",
                    purpose: "",
                    prescription: []
                },
                prescriptionImgs: [],
                uploadPrescriceImgFlag: [],
                freshen: !0
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                },
                clear: function() {
                    return {
                        cardList: [],
                        activePatient: {},
                        confirmData: {},
                        scheduleList: [],
                        sourcePool: {},
                        hisSourcePool: {},
                        detailData: {
                            pics: [],
                            content: "",
                            patientMobile: "",
                            purpose: "",
                            prescription: []
                        },
                        freshen: !0
                    };
                }
            },
            effects: {
                getCardList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call;
                    return Object(j["a"])().mark(function e() {
                        var t, i, c, s, o, u, d;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, r(function() {
                                    return b["b"]("/api/user/patientslist", Object(O["a"])({}, a));
                                });

                              case 2:
                                return i = e.sent, c = i.data, s = c || {}, o = s.cardList, u = s.leftBindNum, d = null !== (t = o.find(function(e) {
                                    return 1 == e.isDefault;
                                })) && void 0 !== t ? t : {}, e.next = 8, n({
                                    type: "save",
                                    payload: {
                                        cardList: o,
                                        activePatient: d,
                                        leftBindNum: u
                                    }
                                });

                              case 8:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getConfirmData: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, s;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.next = 2, i(function(e) {
                                    return e.confirm;
                                });

                              case 2:
                                return t = e.sent, e.next = 5, r(function() {
                                    return b["b"]("/api/register/registerconfirm", Object(O["a"])({}, a));
                                });

                              case 5:
                                return c = e.sent, s = c.data, e.next = 9, n({
                                    type: "save",
                                    payload: {
                                        confirmData: Object(O["a"])(Object(O["a"])({}, t.confirmData), s)
                                    }
                                });

                              case 9:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getHisDocListByDate: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, s, o, u, d, p, l, f;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return t = "/api/register/hlwyy/schedulelist", e.next = 3, r(function() {
                                    return b["b"](t, Object(O["a"])({}, a), {
                                        isRouteApi: !0
                                    });
                                });

                              case 3:
                                if (c = e.sent, s = c.data, o = s.scheduleDate, u = void 0 === o ? "" : o, d = s.itemList, 
                                p = void 0 === d ? [] : d, a.scheduleDate === u) {
                                    e.next = 8;
                                    break;
                                }
                                return e.abrupt("return", !1);

                              case 8:
                                return e.next = 10, i(function(e) {
                                    return e.docInfo;
                                });

                              case 10:
                                return l = e.sent, f = {}, f[u] = p, e.next = 15, n({
                                    type: "save",
                                    payload: {
                                        hisSourcePool: Object(O["a"])(Object(O["a"])({}, l.sourcePool), f),
                                        sourcePool: Object(O["a"])(Object(O["a"])({}, l.sourcePool), f)
                                    }
                                });

                              case 15:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                },
                getWzScheduleList: function(e, t) {
                    var a = e.payload, n = t.put, r = t.call, i = t.select;
                    return Object(j["a"])().mark(function e() {
                        var t, c, o, u, d, p, l, f, v, h, m, y, w, D, S;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return t = a.bizType, c = a.deptId, o = a.doctorId, u = a.subHisId, d = s.a.getStorageSync("scheduleHisMode"), 
                                p = "2" == d && "REGISTER" !== t ? "/api/register/hlwyy/dateschedulelist" : "/api/ehis/schedule/scheduledoctorinfo", 
                                l = "2" == d && "REGISTER" !== t ? {
                                    deptId: c,
                                    doctorId: o,
                                    subHisId: u
                                } : a, e.next = 6, r(function() {
                                    return b["b"](p, Object(O["a"])({}, l), {
                                        isRouteApi: "2" == d && "REGISTER" !== t
                                    });
                                });

                              case 6:
                                return f = e.sent, v = f.data, m = [], "2" == d && "REGISTER" !== t ? (y = v.scheduleList, 
                                w = void 0 === y ? [] : y, null === w || void 0 === w || w.map(function(e) {
                                    e.month = e.scheduleDate.slice(5, 7), e.year = e.scheduleDate.slice(0, 4);
                                }), D = {
                                    day: "monthDay",
                                    isSelect: "selected",
                                    week: "weekDate"
                                }, Object(x["a"])(w, D), m = w) : h = Object(k["f"])(v, g["z"].SCHEDULE_DATE), e.next = 12, 
                                i(function(e) {
                                    return e.confirm;
                                });

                              case 12:
                                return S = e.sent, e.next = 15, n({
                                    type: "save",
                                    payload: {
                                        scheduleList: "2" == d && "REGISTER" !== t ? m : h.days,
                                        sourcePool: "2" == d && "REGISTER" !== t ? S.hisSourcePool : h.tempSourcePool
                                    }
                                });

                              case 15:
                              case "end":
                                return e.stop();
                            }
                        }, e);
                    })();
                }
            }
        }, R = a(184), B = {
            namespace: "modulesContentDetails",
            state: {
                details: {}
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                },
                clear: function() {
                    return {};
                }
            },
            effects: {
                getContentDetails: function(e, t) {
                    var a = e.payload, n = void 0 === a ? {} : a, r = t.call, i = t.put;
                    return Object(j["a"])().mark(function e() {
                        var t, a, c;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.prev = 0, e.next = 3, r(R["c"], n);

                              case 3:
                                if (t = e.sent, a = t.code, c = t.data, 0 !== a) {
                                    e.next = 10;
                                    break;
                                }
                                return e.next = 9, i({
                                    type: "save",
                                    payload: {
                                        details: c
                                    }
                                });

                              case 9:
                                return e.abrupt("return", c || null);

                              case 10:
                                return e.abrupt("return", null);

                              case 13:
                                e.prev = 13, e.t0 = e["catch"](0);

                              case 15:
                              case "end":
                                return e.stop();
                            }
                        }, e, null, [ [ 0, 13 ] ]);
                    })();
                }
            }
        }, z = function() {
            var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
            return Object(b["b"])("/api/mix/patient/recommend/custom-page/detail", e, {
                showLoading: !1
            });
        }, q = {
            namespace: "modulesContentDesign",
            state: {
                shareInfo: {}
            },
            reducers: {
                save: function(e, t) {
                    var a = t.payload;
                    return Object(O["a"])(Object(O["a"])({}, e), a);
                },
                clear: function() {
                    return {};
                }
            },
            effects: {
                getDetail: function(e, t) {
                    var a = e.payload, n = void 0 === a ? {} : a, r = t.call, i = t.put;
                    return Object(j["a"])().mark(function e() {
                        var t, a, c, s, o;
                        return Object(j["a"])().wrap(function(e) {
                            while (1) switch (e.prev = e.next) {
                              case 0:
                                return e.prev = 0, e.next = 3, r(z, n);

                              case 3:
                                if (t = e.sent, a = t.code, c = t.data, 0 !== a) {
                                    e.next = 10;
                                    break;
                                }
                                return s = JSON.parse(c.customPage), o = s.pageInfo, e.next = 10, i({
                                    type: "save",
                                    payload: {
                                        shareInfo: {
                                            shareTitle: o.model.shareTitle,
                                            shareDesc: o.model.shareDesc
                                        }
                                    }
                                });

                              case 10:
                                e.next = 14;
                                break;

                              case 12:
                                e.prev = 12, e.t0 = e["catch"](0);

                              case 14:
                              case "end":
                                return e.stop();
                            }
                        }, e, null, [ [ 0, 12 ] ]);
                    })();
                }
            }
        }, _ = (a(692), a(0)), M = y["a"].createApp({
            initialState: {},
            models: Object.values(n),
            onError: function() {}
        }), U = M.getStore(), W = function(e) {
            Object(p["a"])(a, e);
            var t = Object(l["a"])(a);
            function a() {
                var e;
                Object(o["a"])(this, a);
                for (var n = arguments.length, r = new Array(n), i = 0; i < n; i++) r[i] = arguments[i];
                return e = t.call.apply(t, [ this ].concat(r)), Object(f["a"])(Object(d["a"])(e), "compareVersion", function(e, t) {
                    e = e.split("."), t = t.split(".");
                    var a = Math.max(e.length, t.length);
                    while (e.length < a) e.push("0");
                    while (t.length < a) t.push("0");
                    for (var n = 0; n < a; n++) {
                        var r = parseInt(e[n]), i = parseInt(t[n]);
                        if (r > i) return 1;
                        if (r < i) return -1;
                    }
                    return 0;
                }), e;
            }
            return Object(u["a"])(a, [ {
                key: "componentWillMount",
                value: function() {
                    var e = s.a.getSystemInfoSync().SDKVersion;
                    if (!e) return !1;
                    var t = !1, a = !1;
                    t = this.compareVersion(e, "2.8.3") >= 0;
                    var n = s.a.getSystemInfoSync().version;
                    a = this.compareVersion(n, "7.0.0") >= 0, Object(h["h"])("isSDKVersion", t), Object(h["h"])("isVersion", a), 
                    Object(h["h"])("platform", s.a.getSystemInfoSync().platform);
                }
            }, {
                key: "componentDidMount",
                value: function() {
                    wx.onAppRoute(function() {
                        for (var e in b["c"]) {
                            var t, a = null === (t = s.a.getCurrentInstance().router) || void 0 === t ? void 0 : t.path;
                            !e.includes("_".concat(a, "_")) && b["c"][e].abort;
                        }
                        var n = s.a.getCurrentPages(), r = n[n.length - 1];
                        s.a.showShareMenu({
                            withShareTicket: !0
                        }), r.onShareAppMessage = function() {
                            var e;
                            return g["C"].includes(r.route) ? {
                                title: (null === (e = r.config) || void 0 === e ? void 0 : e.navigationBarTitleText) || "大连市第二人民医院",
                                path: "".concat(r.route, "?").concat(Object(x["x"])(r.options))
                            } : {
                                title: "大连市第二人民医院",
                                path: "/pages/extra/home/index",
                                imageUrl: "".concat("https://static.med.gzhc365.com/miniprogram-static/fe-his-mixapp/p2342", "/share-bg.png")
                            };
                        };
                    });
                }
            }, {
                key: "componentDidShow",
                value: function() {
                    var e = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
                    if (s.a.getStorageSync("openWeiXinNewPage")) return console.log("back from wx pages"), 
                    void s.a.removeStorageSync("openWeiXinNewPage");
                    console.log("mini program did show"), Object(h["c"])();
                    var t = s.a.getStorageSync("".concat(s.a.getEnv(), "_login_access_token_").concat(g["A"].hisId));
                    t ? Object(h["a"])() : Object(h["b"])();
                    var a = e.query, n = void 0 === a ? {} : a, r = y["a"].getDispatch();
                    r({
                        type: "global/save",
                        payload: {
                            appLaunchQuery: n
                        }
                    }), r({
                        type: "global/getSystemTips"
                    });
                }
            }, {
                key: "componentDidHide",
                value: function() {
                    Object(h["b"])();
                }
            }, {
                key: "componentDidCatchError",
                value: function() {}
            }, {
                key: "render",
                value: function() {
                    return Object(_["jsx"])(m["a"], {
                        store: U,
                        children: this.props.children
                    });
                }
            } ]), a;
        }(v["Component"]), H = W, G = a(328), V = {
            pages: [ "pages/extra/home/index", "pages/usercenter/bindcard/index", "pages/usercenter/ocrbindcard/index", "pages/usercenter/userinfo/index", "pages/usercenter/userlist/index", "pages/usercenter/main/index", "pages/usercenter/regorderlist/index", "pages/usercenter/orderlist/index", "pages/usercenter/inquirylist/list/index", "pages/search/main/index", "pages/search/alldept/index", "pages/search/alldoctor/index", "pages/extra/auth/index", "pages/extra/develop/index", "pages/extra/readme/index", "pages/register/deptlist/index", "pages/register/doclist/index", "pages/register/docinfo/index", "pages/register/collect/index", "pages/register/order/index", "pages/register/confirm/index", "pages/register/hosplist/index", "pages/waiting/waiting/index", "pages/waiting/abnormal/index", "pages/pay/index", "pages/messagecenter/home/index", "pages/messagecenter/noticelist/index", "pages/messagecenter/informationlist/index", "pages/inquiry/chat/index", "pages/inquiry/video/index", "pages/inquiry/detail/index", "pages/extra/vlog/index", "pages/survey/list/index", "pages/survey/detail/index", "pages/survey/riskArea/index", "pages/visitguide/index", "pages/preinquiry/index", "pages/microsite/home/index", "pages/microsite/floorlayout/index", "pages/microsite/dynamic/list/index", "pages/microsite/dynamic/info/index", "pages/microsite/deptlist/index", "pages/microsite/deptinfo/index", "pages/microsite/doctorinfo/index", "pages/microsite/deptlayout/index" ],
            window: {
                backgroundTextStyle: "light",
                navigationBarTitleText: "",
                navigationBarTextStyle: "black",
                navigationBarBackgroundColor: "#f6f7f9"
            },
            subPackages: [ {
                root: "pkg1",
                name: "pkg1",
                pages: [ "charge/index/index", "charge/detail/index", "queue/index/index", "treat/untreatlist/index", "treat/untreatdetail/index", "treat/recorddetail/index", "inpatient/home/index", "inpatient/in/reg/index", "inpatient/binduser/index", "inpatient/userlist/index", "inpatient/userinfo/index", "inpatient/amount/index", "inpatient/recorddetail/index", "inpatient/dailylist/index", "pharmacist/doclist/index", "pharmacist/docinfo/index", "pharmacist/confirm/index", "pharmacist/collectinfo/index", "pharmacist/medicallist/index", "pharmacist/collectdetail/index", "consultreferral/consultation/index", "consultreferral/referral/detail/index", "consultreferral/referral/list/index", "precisebooking/deptlist/index", "precisebooking/explain/index", "precisebooking/detail/index", "precisebooking/applyinfo/index", "agreement/index", "webview/index", "feedback/list/index", "feedback/submit/index" ]
            }, {
                root: "pkg2",
                name: "pkg2",
                pages: [ "takeno/index/index", "takeno/detail/index", "medicalrecordfolder/recordlist/index", "medicalrecordfolder/recorddetail/index", "medicalrecordfolder/recordbookdetail/index", "pastprescribe/index", "prescription/detail/index", "prescription/paydetail/index", "prescription/address/index", "express/index", "prescription/orderdetail/index", "evaluate/index", "evaluationdetails/index", "report/reportlist/index", "report/examine/index", "report/analysis/index", "report/analysiscontrast/index", "netnurse/visitingservice/home/index", "netnurse/visitingservice/servicelist/index", "netnurse/visitingservice/servicedetail/index", "netnurse/visitingservice/serviceorder/index", "netnurse/visitingservice/orderdetail/index", "netnurse/visitingservice/orderdetail/subpages/illnessdescription/index", "netnurse/visitingservice/orderdetail/subpages/extrafee/index", "netnurse/visitingservice/orderdetail/subpages/message/index", "netnurse/visitingservice/orderhistory/index", "netnurse/visitingservice/addressconfig/list/index", "netnurse/visitingservice/addressconfig/edit/index", "netnurse/visitingservice/agreement/index", "netnurse/visitingservice/orderdetail/subpages/assessment/index", "netnurse/visitingservice/orderdetail/subpages/assessmentresult/index", "netnurse/consult/nurselist/index", "netnurse/consult/nurseinfo/index", "visitrecord/index", "patienteducation/contentdetail/index", "patienteducation/contentsubscription/index", "doctorrecommend/index" ]
            }, {
                root: "modules/content",
                name: "content",
                pages: [ "details/index", "feedback/index", "design/index", "outerlink/index", "transfer/index", "list/index" ]
            } ],
            tabBar: {
                backgroundColor: "#ffffff",
                color: "#666666",
                selectedColor: "#3ECEB6",
                borderStyle: "white",
                list: [ {
                    pagePath: "pages/extra/home/index",
                    text: "首页",
                    iconPath: "./resources/images/home-off.png",
                    selectedIconPath: "./resources/images/home-on.png"
                }, {
                    pagePath: "pages/microsite/home/index",
                    text: "医院信息",
                    iconPath: "./resources/images/msg-off.png",
                    selectedIconPath: "./resources/images/msg-on.png"
                }, {
                    pagePath: "pages/usercenter/main/index",
                    text: "个人中心",
                    iconPath: "./resources/images/user-off.png",
                    selectedIconPath: "./resources/images/user-on.png"
                } ]
            },
            usingComponents: {
                iconfont: "./components/iconfont/weapp/weapp"
            },
            plugins: {
                WechatSI: {
                    version: "0.3.4",
                    provider: "wx069ba97219f66d99"
                }
            },
            permission: {
                "scope.userLocation": {
                    desc: "取号功能需要获取您的地理位置信息"
                }
            }
        };
        r["window"].__taroAppConfig = V;
        App(Object(i["a"])(H, v, G["a"], V));
        Object(c["initPxTransform"])({
            designWidth: 750,
            deviceRatio: {
                640: 1.17,
                750: 1,
                828: .905
            }
        });
    }
}, [ [ 761, 0, 3, 2, 1 ] ] ]);