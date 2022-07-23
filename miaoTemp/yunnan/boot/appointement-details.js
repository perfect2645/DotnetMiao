$class("eh.wx.health.person.UserAppointDetail", {
    extend: "eh.wx.base.BaseContainer",
    mixins: ["eh.wx.health.hospitalization.MaintenanceTimeControl", "eh.wx.base.PayWeb", "eh.wx.base.ThirdLink", "eh.wx.health.doctFinder.appointConsult"],
    deps: ["lib.code", "lib.qrcode", "eh.wx.health.person.BindAndFile", "eh.wx.health.person.accountSelect", "eh.wx.util.directive.BackHomeDrag"],
    hts: true,
    title: "我的预约单",
    css: ["eh.wx.health.person.css.user", "eh.wx.wap.person.css.barcode"],
    hook: ["showDoctIndexPop", "showBackHomeBtn"],
    isSignOnPay: !1,
    sourceLevelToPay: !1,
    barcodeNoneCardType: !1,
    showPayTip: !1,
    shanghaiSixCss: !1,
    isZJFC: !1,
    template: '<div style="text-align:center;background:#fff;">' + '<div><img src="' + $env.resourcesHome + 'eh/wx/health/portal/images/load.gif"/></div>' + '<div id="progress"> <div id="bar"></div> </div>' + '<div id="settleTitle">跳转中,请耐心等待....</div>' + "</div>",
    config: {
        organ_1001309: {
            isSignOnPay: !0
        },
        organ_1000899: {
            cardTips: !0,
            shSixHospital: !0,
            shanghaiSixCss: !0
        },
        organ_1004140: {
            cardTips: !0,
            shSixHospital: !0,
            shanghaiSixCss: !0
        },
        organ_1001994: {
            isSignOnPay: !0,
            sourceLevelToPay: !0
        },
        organ_1005137: {
            barcodeNoneCardType: !0
        },
        organ_1000872: {
            noShowCancelBtn: !0,
            showPayTip: !0
        },
        organ_1005826: {
            showNumTime: !0
        },
        organ_1000073: {
            isZJFC: !0
        },
        organ_1003040: {
            showFailUI: !0
        },
        organ_1001281: {
            showFailUI: !0,
            jinJiangUi: !0
        },
        organ_1005002: {
            isHSFive: !0
        },
        organ_1003040: {
            isSHSZ: !0
        }
    },
    enableDoLog: true,
    themeName: "appoint.appointDetailTheme",
    doLogConfig: {
        url: "userappointdetail",
        eventName: "详情页",
        serviceModule: "预约挂号详情",
        eventTypes: ["sw", "st"]
    },
    afterInitComponent: function() {
        var me = this
          , el = me.el;
        me.popovers = {};
        me.modules = {};
        me.weiningBack = !1;
        this.initController(function($scope, $timeout, $filter) {
            $scope.shanghaiSixCss = me.shanghaiSixCss;
            $scope.showNumTime = me.showNumTime;
            $scope.sourceLevelToPay = me.sourceLevelToPay;
            $scope.customerTel = $AppContext.WXApp.customerTelForShow;
            $scope.cancelStatus = false;
            $scope.photo = "";
            $scope.myActiveSlide = 0;
            $scope.goBackHomeFlag = false;
            $scope.isShowAllText = false;
            $scope.tip = !0;
            $scope.ageText = "";
            me.timeout = $timeout;
            me.filter = $filter;
            $scope.appointStatusObj = {};
            $scope.rotateFlag = [!1, !1, !1];
            $scope.menuShowFlag = !1;
            $scope.isPayBtnShow = !0;
            $scope.judgePayBtnShow = !1;
            $scope.showMoveTips = !1;
            $scope.count = 0;
            $scope.titleDescLines = 4;
            $scope.restMinute = "0";
            $scope.restSecond = "0";
            $scope.restTime = 1;
            $scope.payTime = 0;
            $scope.hasBtnWrapChild = false;
            $scope.payAhead = -2;
            $scope.showPayRestTime = !1;
            $scope.zeroRegStatus = false;
            $scope.showMedInsureEntry = $AppContext.WXApp.organId == "1005790";
            $scope.shSixHospital = me.shSixHospital;
            $scope.clickAlreadyPay = !0;
            $scope.sourcePage = "";
            $scope.isGoPaymentRecord = !0;
            $scope.isRefreshDetail = !0;
            $scope.banner = !1;
            $scope.isWeixin = $env.isWeixin;
            $scope.isShowQrcode = !1;
            $scope.isZJFC = me.isZJFC;
            $scope.isHSFive = me.isHSFive;
            $scope.isSHSZ = me.isSHSZ;
            $scope.isDeY = $env.appkey == "jkdy" && $env.hybrid || $AppContext && $AppContext.wxConfig && $AppContext.wxConfig.appId == "wx9a3ac965fb75b2fe" || $env.appkey == "tfyjt-jkdy" || $env.appkey == "smt-test";
            $scope.isFontRed = !1;
            $env.hybrid && $env.appkey == "zzrmyy" && $env.callNativeMethod("api_showGoHomeBtn");
            $scope.TongueTextArr = {
                1: "去舌诊检查",
                2: "查看舌诊报告"
            };
            $scope.switchSelect = function() {
                if ($scope.listInfoType == "specialAppoint") {
                    return
                }
                me.doSwitch()
            }
            ;
            me.resetScroll = function() {
                $sys.getScroll("myAppointintScroll").resize()
            }
            ;
            $scope.showBarcodeInfo = function(isMzId) {
                isMzId ? me.goBarcodeInfoPage(isMzId, $scope.item.appointRecord.clinicMzId) : me.showBarcodeInfo(isMzId)
            }
            ;
            $scope.showElecHealthCardInfo = function() {
                me.showElecHealthCardInfo()
            }
            ;
            $scope.showAllTitleDesc = function() {
                me.showAllTitleDesc()
            }
            ;
            $scope.refreshDetail = function() {
                me.refreshDetailData()
            }
            ;
            $scope.cancelApply = function(item) {
                if (item.appointRecord.appointStatus == 5 && item.appointRecord.outTradeNo) {
                    me.getOrganConfigInfo(el.scope().item.appointRecord.organId).then(function(res) {
                        me.isMaintenanceControl(res, "showSystemSuggestPop")
                    }).fail(function(e) {
                        $sys.showFail(e)
                    })
                } else {
                    !$scope.zeroRegStatus && me.showSystemSuggestPop()
                }
            }
            ;
            $scope.reApply = function() {
                me.reAppointApply()
            }
            ;
            $scope.payNow = function() {
                if (!$scope.zeroRegStatus && $scope.clickAlreadyPay) {
                    $sys.showLoading();
                    $scope.clickAlreadyPay = !1;
                    me.payOrderNow()
                }
            }
            ;
            $scope.closeTip = function() {
                me.refresh(function($scope) {
                    $scope.showCancelScrollTip = false
                });
                clearInterval($scope.scrollTipTimer)
            }
            ;
            $scope.goDoctorIndex = function(id) {
                $sys.openModule({
                    title: "医生主页",
                    cache: me.popovers,
                    manualLoading: !0,
                    url: "eh.wx.health.doctor.SingleDoctIndex"
                }).then(function(p) {
                    p.module.getDoctInfo(id, "user")
                })
            }
            ;
            $scope.switchRotate = function(flag) {
                $scope.rotateFlag[flag] = !$scope.rotateFlag[flag];
                me.resetScroll()
            }
            ;
            $scope.onApilyClose = function() {
                $scope.menuShowFlag = !1;
                if (!$scope.clickAlreadyPay) {
                    $scope.clickAlreadyPay = !0;
                    $scope.$applyAsync()
                }
            }
            ;
            $scope.onApilyTypeSelected = function(accountType) {
                me.onApilyTypeSelected(accountType)
            }
            ;
            $scope.goIndexClick = function() {
                me._dologMgr.dolog({
                    eventId: "weixin_userappointdetail_backhome_ck",
                    eventName: "详情页-点击返回首页按钮",
                    eventType: "ck"
                });
                clearInterval($scope.scrollTipTimer);
                $emitter.emit("indexGoIndex")
            }
            ;
            $scope.showQrcode = function() {
                me.showQrcodeFunc()
            }
            ;
            $scope.showEInvoice = function() {
                me.showElecInvoiceFunc()
            }
            ;
            $scope.showElecInvoiceQrcode = function() {
                me.showElecInvoiceQrcodeFunc()
            }
            ;
            $scope.goInsuranceCard = function() {
                $sys.goLink("https://mp.weixin.qq.com/insurance/card/creditview?cityid=310000&from=kEOqu5C0GCrWFSAs96zlnw.%3D&uin=&key=&devicetype=Windows+10+x64&version=6300002f&lang=zh_CN&ascene=0&fontgear=2#wechat_redirect")
            }
            ;
            $scope.$watch("thirdUrlLink", function(newValue, oldValue) {
                if (newValue == oldValue) {
                    return
                }
                me.goThirdUrlMethod(newValue)
            });
            $scope.goConsult = function() {
                $env.setConsultPageId("zx_gh_004");
                $sys.goLink(el.scope().consultUrl)
            }
            ;
            $scope.refundApply = function() {
                me.refundApply()
            }
            ;
            $scope.queryRefundStatus = function() {
                me.queryRefundStatus()
            }
        });
        el.on("click", "#topdocintro", function() {
            me.showDoctIndexPop()
        });
        el.on("click", "#J-goBackHome", function() {
            var scope = el.scope();
            clearInterval(scope.scrollTipTimer);
            $emitter.emit("indexGoIndex")
        });
        el.on("click", "#J-userCdrOtherdocs li", function() {
            var index = this.dataset.index;
            me.previewCdrImagePop(index)
        });
        el.on("click", ".appointdetaildesc .allTextBtn", function() {
            me.showAllText();
            me.resetScroll()
        });
        el.on("click", "#J-checkCurNum", function() {
            me.checkCurNum()
        });
        el.on("click", "#Preconsultation", function() {
            me.showPreconsultation()
        });
        el.on("click", "#J-doctEvaluate", function() {
            me.showEvaluatePop()
        });
        el.on("click", ".tongueButton", function(e) {
            e.preventDefault();
            window.event ? window.event.cancelBubble = true : e.stopPropagation();
            setTimeout(function() {
                me.goTongueConsult()
            }, 500)
        });
        me.setupService([{
            beanName: "eh.unLoginSevice",
            method: "getAllClientConfigs"
        }, {
            beanName: "appoint.appointConfigService",
            method: "getOrganConfig"
        }, {
            beanName: "eh.organ",
            method: "getByOrganId"
        }, {
            beanName: "eh.healthCard",
            method: "queryHealthCardFromHisAndMerge"
        }, {
            beanName: "appoint.queryAppointRecordController",
            method: ["getPatAppointRecordByIdWithStar", "getOrganPayTimeSetting"]
        }, {
            beanName: "ts.transfer",
            method: "getPatTransferById"
        }, {
            beanName: "eh.serviceConfig",
            method: "canAppointAndCanFile"
        }, {
            beanName: "eh.healthCard",
            method: "bindCard"
        }, {
            beanName: "appoint.settleService",
            method: ["simpleSettleWithoutInterrupt", "settlePreBillForBus", "settlePreBillForBusNew"]
        }, {
            beanName: "appoint.requestAppointRecordService",
            method: "getDoctorDeptInfo"
        }, {
            beanName: "basic.patientService",
            method: "getPatientInfoDS"
        }, {
            beanName: "eh.configurationCenterUtils",
            method: "findConfigurations"
        }, {
            beanName: "appoint.appointUnLoginService",
            method: "getAppointOrganConfig"
        }, {
            beanName: "appoint.preConsultService",
            method: ["canPreConsult", "preConsultURL"]
        }, {
            beanName: "appoint.askQuestionConfigService",
            method: "getQuestionUrls"
        }, {
            beanName: "appoint.settleService",
            method: "fastZeroPay"
        }, {
            beanName: "mi.authExtraService",
            method: "getSignNoAndSaveData"
        }, {
            beanName: "appoint.queryAppointRecordService",
            method: "getShanghai6NCovUrl"
        }, {
            beanName: "appoint.queryAppointRecordController",
            method: ["applyAppointRecordRefund", "queryAppointRefundStatus"]
        }, {
            beanName: "treatmentguide.electronicBillController",
            method: "getBillByBusOrderNo"
        }, {
            beanName: "infra.cardOperationService",
            method: "cardEncryption"
        }, {
            beanName: "infra.antForestService",
            method: "getAntForestEnergyValueInfo"
        }])
    },
    payOrderNow: function() {
        var me = this
          , el = me.el
          , scope = el.scope();
        if (scope.args && !scope.args.paySource && scope.args.insuranceCardUpgrade && scope.item && scope.item.appointRecord && scope.item.appointRecord.cancelResean) {
            $sys.showMsg("因医保卡升级，请在候诊开始时间前，前往医院窗口支付挂号费，给您带来不便敬请谅解!", 2e3, "toast-auto");
            $sys.hideLoading();
            scope.clickAlreadyPay = !0;
            scope.$applyAsync();
            return
        }
        if (scope.item.appointRecord && scope.item.appointRecord.appointSourceId == -1) {
            if (scope.appointOfflinePayOnline && scope.OfflineAppointPayCancel && scope.payTime > 0 && scope.payAhead != -2 && scope.restTime <= 0) {
                $sys.showMsg({
                    template: "倒计时结束，暂不能支付",
                    cssClass: "default",
                    buttons: [{
                        text: "确定",
                        type: "button-positive",
                        onTap: function() {
                            me.getSourceAppointDetail(scope.appointId)
                        }
                    }]
                });
                scope.clickAlreadyPay = !0;
                scope.$applyAsync();
                $sys.hideLoading();
                return
            }
        } else if (scope.payTime > 0 && scope.payAhead != -2 && scope.restTime <= 0) {
            $sys.showMsg({
                template: "倒计时结束，暂不能支付",
                cssClass: "default",
                buttons: [{
                    text: "确定",
                    type: "button-positive",
                    onTap: function() {
                        me.getSourceAppointDetail(scope.appointId)
                    }
                }]
            });
            scope.clickAlreadyPay = !0;
            scope.$applyAsync();
            $sys.hideLoading();
            return
        }
        $sys.showLoading();
        me.service.getOrganConfig(scope.item.appointRecord.organId).then(function(res) {
            me.isMaintenanceControl(res, "insuranceCardOrSelfPay")
        }).fail(function(e) {
            scope.clickAlreadyPay = !0;
            scope.$applyAsync();
            $sys.hideLoading();
            $sys.showFail(e)
        })
    },
    showAllText: function() {
        var me = this
          , el = me.el
          , scope = el.scope();
        me.refresh(function($scope) {
            $scope.patientLeaveMsg = scope.item.transfer.patientCondition;
            $scope.isShowAllText = false
        })
    },
    resetCdrImgStyle: function() {
        var me = this, el = me.el, scope = el.scope(), lis = el.find("#J-userCdrOtherdocs img"), liW;
        liW = Math.ceil(document.body.clientWidth / 4 - 20);
        lis.css("height", liW + "px");
        function getByteLen(val, lineNum, rowWidth) {
            var len = 0;
            for (var i = 0; i < val.length; i++) {
                if (len < rowWidth * lineNum) {
                    var cur = val.charAt(i);
                    if (/[^\x00-\xff]/gi.test(cur)) {
                        len += 1
                    } else {
                        len += .5
                    }
                } else {
                    return i
                }
            }
        }
        var winWidth = document.body.clientWidth - 30
          , rowWidth = Math.floor(winWidth / 15)
          , maxLength = Math.ceil(getByteLen(scope.item.transfer.patientCondition, 4, rowWidth))
          , realLength = scope.item.transfer.patientCondition.length;
        if (realLength > maxLength) {
            me.refresh(function($scope) {
                $scope.patientLeaveMsg = scope.item.transfer.patientCondition.substring(0, maxLength - 5) + "...";
                $scope.isShowAllText = true
            })
        } else {
            me.refresh(function($scope) {
                $scope.patientLeaveMsg = scope.item.transfer.patientCondition;
                $scope.isShowAllText = false
            })
        }
    },
    doSwitch: function() {
        var me = this
          , el = me.el
          , scope = el.scope()
          , switchFlag = !scope.switchFlag;
        scope.switchFlag = switchFlag;
        me.resetScroll()
    },
    getAppointDetailInfo: function(type, appointId, rType, source) {
        var me = this, el = me.el, scope = el.scope(), timer;
        $sys.hideLoading();
        scope.appointId = appointId;
        scope.rType = rType;
        scope.source = source;
        scope.sourcePage = source;
        scope.listInfoType = type;
        scope.barcodeId = "";
        scope.tip = !0;
        scope.menuShowFlag = !1;
        scope.preBill = undefined;
        scope.count = 0;
        scope.banner = !1;
        $sys.getScroll("myAppointintScroll").scrollTop();
        scope.isPayBtnShow = $env.getCookie("appkey") == "aliAppointDetail" ? !1 : $env.appkey == "jins_jiankt" ? !1 : !0;
        if (type == "sourceAppoint") {
            me.getSourceAppointDetail(appointId)
        } else if (type == "specialAppoint") {
            me.getSpecialAppointDetail(appointId)
        }
        me.resetScroll()
    },
    judgeMentTime: function() {
        var me = this
          , el = me.el
          , scope = el.scope()
          , appointRecord = scope.item.appointRecord
          , defer = new $Defer
          , getConsultFlag = !1
          , appointDate = parseInt(appointRecord.endTime.substr(0, 10).replace(/-/g, ""), 10)
          , currentDate = parseInt($AppContext.serverDate.substr(0, 10).replace(/-/g, ""), 10)
          , beforeDate = +new Date(new Date(appointRecord.endTime.replace(/-/g, "/")) - 864e5).Format("yyyyMMdd");
        getConsultFlag = scope.isSHSZ ? currentDate == beforeDate || currentDate == appointDate : currentDate <= appointDate;
        if (getConsultFlag) {
            me.service.canPreConsult(appointRecord.appointRecordId).then(function(res) {
                me.refresh(function($scope) {
                    $scope.showPreconsultbtn = res
                });
                if (res) {
                    me.service.preConsultURL(appointRecord.appointRecordId).then(function(preconsultUrl) {
                        me.refresh(function($scope) {
                            $scope.preconsultUrl = preconsultUrl
                        });
                        defer.resolve()
                    }).fail(function(e) {
                        $sys.showFail(e);
                        defer.reject(e)
                    })
                } else {
                    defer.resolve()
                }
            }).fail(function(e) {
                $sys.showFail(e);
                defer.reject(e)
            })
        } else {
            me.refresh(function($scope) {
                $scope.showPreconsultbtn = false
            });
            defer.resolve()
        }
        return defer.promise
    },
    previewCdrImagePop: function(index) {
        var me = this
          , el = me.el
          , scope = el.scope()
          , cdrDocArr = scope.item.cdrOtherdocs || scope.item.illSummaryBeforeAppoint;
        if (!cdrDocArr.length) {
            return false
        }
        $sys.openModule({
            title: "查看大图",
            cache: me.popovers,
            url: "eh.wx.health.person.ViewBigImage",
            events: {
                close: {
                    autoclose: !0
                }
            }
        }).then(function(p) {
            p.module.setCdrImageArray(index, cdrDocArr)
        })
    },
    getSourceAppointDetail: function(appointId, showEva, refresh) {
        var me = this, el = me.el, scope = el.scope(), liW = Math.ceil(document.body.clientWidth / 4 - 20), zeroRegStatus, showQrcodeStatus;
        clearInterval(scope.scrollTipTimer);
        clearInterval(scope.appointTimer);
        scope.isPayBtnShow = $env.getCookie("appkey") == "aliAppointDetail" ? !1 : $env.appkey == "jins_jiankt" ? !1 : !0;
        scope.showMoveTips = !$env.hybrid && !$env.getCookie("appkey") && !$env.isAlipay && $env.isWeixin;
        scope.preBill = undefined;
        scope.judgePayBtnShow = !1;
        me.service.getPatAppointRecordByIdWithStar(appointId).then(function(detail) {
            $sys.hideLoading();
            zeroRegStatus = detail.appointRecord.appointRecordExt && (detail.appointRecord.appointRecordExt.zeroRegStatus == 0 || detail.appointRecord.appointRecordExt.zeroRegStatus == 4);
            showQrcodeStatus = detail.appointRecord && (detail.appointRecord.appointStatus == 1 || detail.appointRecord.appointStatus == 5 || detail.appointRecord.appointStatus == 6 || detail.appointRecord.appointStatus == 7 || detail.appointRecord.appointStatus == 8 || detail.appointRecord.appointStatus == 10 || detail.appointRecord.appointStatus == 12 || detail.appointRecord.appointStatus == 14);
            me.refresh(function($scope) {
                $scope.clickAlreadyPay = !0;
                var record = detail.appointRecord
                  , extInfo = record.appointRecordExt;
                $scope.jinJiangUi = !1;
                $scope.listInfoType = "sourceAppoint";
                $scope.liW = liW;
                $scope.noShowCancelBtn = me.noShowCancelBtn;
                $scope.rotateFlag = [!1, !1, !1];
                detail.displayIllSummaryBeforeAppoint && (extInfo || (record.appointRecordExt = extInfo = {
                    illSummaryTxt: ""
                }));
                $scope.detailFalg = 0;
                $scope.item = detail;
                $scope.item.appointRecord.appointDepartName = $scope.item.appointRecord.appointDepartName && $scope.item.appointRecord.appointDepartName.replace(/<[^>]*>|<\/[^>]*>/gm, "").trim();
                $scope.photo = $scope.getAva(detail.doctor);
                $scope.appointId = appointId;
                $scope.payBtnText = me.isSignOnPay ? "签到支付" : !$scope.shSixHospital ? "立即支付" : detail.appointRecord && detail.appointRecord.medInsureCarId ? "医保支付" : "自费支付";
                $scope.ageText = me.countAge(record.appointDate, detail.patient.birthday);
                refresh && $scope.$broadcast("scroll.refreshComplete");
                document.title = record.recordType && record.recordType != 2 ? "我的挂号单" : "我的预约单";
                $env.isIOS && $sys.iosTitleReset();
                $scope.invoiceUrl = extInfo.invoiceUrl || "";
                $scope.invoiceCode = extInfo.invoiceCode || "";
                $scope.hasBtnWrapChild = false;
                $scope.showPayRestTime = !1;
                $scope.titleDescFold = undefined;
                $scope.titleDescLines = 4;
                $scope.zeroRegStatus = zeroRegStatus;
                $scope.refundApplyNo = detail.appointRecord && detail.appointRecord.appointRecordExt && detail.appointRecord.appointRecordExt.refundApplyNo || "";
                $scope.showRefundBtn = detail.refundBtn;
                $scope.cancelStatus = detail.cancelBtn;
                $scope.appointFailShow = detail.appointRecord.appointStatus == 2 && detail.appointRecord.cancelType == 4 && me.showFailUI ? !0 : !1;
                $scope.canShowTongueConsultBtn = detail.canShowTongueConsultBtn;
                $scope.tongueConsultUrl = detail.canShowTongueConsultBtn && detail.tongueConsultUrl
            });
            (scope.sourcePage == "appointApply" || scope.sourcePage == "pay") && scope.isRefreshDetail && (detail.appointRecord.appointStatus == 9 || detail.appointRecord.appointStatus == 11 || zeroRegStatus) ? timer = setTimeout(function() {
                scope.isRefreshDetail = !1;
                me.refreshDetailData();
                clearTimeout(timer)
            }, 2e3) : null;
            Promise.all([me.judgeMentTime(), me.initOrganInfo(detail), me.initClientConfigs(), me.getClinicMzIdInstructions()]).then(function() {
                me.initBarcodeInfo();
                scope.regQrCode = "";
                scope.electronicInvoiceSwitch == "2" && showQrcodeStatus && me.getQrcode(detail);
                if (detail.appointRecord && (detail.appointRecord.appointStatus == 2 || detail.appointRecord.appointStatus == 6)) {
                    me.showConsult()
                }
                detail.appointRecord.appointRecordExt && detail.appointRecord.appointRecordExt.appointDetailQrNum && detail.appointRecord.statusName != "doc" && me.getClinicMzCode();
                if (detail.appointRecord.appointStatus == 0 || detail.appointRecord.appointStatus == 4) {
                    if (detail.appointRecord.appointSourceId == -1) {
                        scope.appointOfflinePayOnline && scope.OfflineAppointPayCancel && me.showRestTime()
                    } else {
                        me.showRestTime()
                    }
                }
                if (detail.appointRecord.appointStatus == 10) {
                    me.showStatusWrap()
                } else {
                    me.canShowPayBtn()
                }
                me.getMaYiEngrency();
                me.showError();
                me.goPaymentRecord(detail)
            });
            showEva && me.showEvaluatePop();
            me.showDepartInfo(detail);
            $sys.getClientConfigs().then(function(config) {
                config.gotoQuestionUrls[0] == "ngari_znwz_url" && scope.args && scope.args.uniqueIndex && ($sys.showLoading(),
                me.payNowValidateDetail())
            });
            me.goThirdUrlSixHospital(detail)
        }).fail(function(e) {
            $sys.showFail(e)
        })
    },
    getMaYiEngrency: function() {
        var me = this
          , scope = me.el.scope()
          , record = scope.item.appointRecord;
        console.log(record.recordType, scope.args && scope.args.sourcePage, "=== recordType");
        console.log(record.appointStatus, scope.sourcePage, "=== appointStatus");
        if (scope.organInfo.GetAntForestEnergy && ($env.isAlipay && ($env.ngariMiniAppForWeb || $env.ngariWeApp)) && (scope.args && scope.args.sourcePage == "appointApply" && record.appointStatus == 0 || record.recordType == 1 && scope.sourcePage == "pay" && record.appointStatus == 1)) {
            var accessToken = $env.getCookie("aliAk");
            console.log(accessToken, "===getMaYiEngrency");
            accessToken && me.service.getAntForestEnergyValueInfo(accessToken, "horegister", scope.appointId).then(function(engencyData) {
                scope.total_energy = engencyData.total_energy;
                console.log("输出scope.total_energy", scope.total_energy);
                engencyData.total_energy && $sys.showMsg({
                    template: '<div class="nenglSuccessTip">本次预约挂号得蚂蚁森林能量<span>{{total_energy}}g</span></div>',
                    scope: scope
                }, !0, "maYiNengliang")
            }).fail(function(e) {
                console.log(e, "===getAntForestEnergyValueInfo error");
                $sys.showFail(e)
            })
        }
    },
    showConsult: function() {
        var me = this
          , scope = me.el.scope()
          , doctorId = !scope.item.doctor.jobNumber || scope.item.doctor.jobNumber && scope.item.doctor.jobNumber.slice(0, 3) != "PT_" ? scope.item.doctor.doctorId : "";
        me.showConsultEntry(scope.organInfo, "appointcanceldetail", "appoint_detail", doctorId).then(function(consult) {
            me.refresh(function($scope) {
                $scope.banner = consult && consult.banner;
                $scope.consultUrl = consult && consult.url
            })
        })
    },
    goThirdUrlSixHospital: function(detail) {
        var me = this
          , scope = me.el.scope();
        if (detail.appointRecord && detail.appointRecord.payFlag == 1 && detail.appointRecord.appointdepartId == 3607 && detail.appointRecord.organId == 1000899) {
            me.getThirdUrl(detail.appointRecord.organId).then(function(url) {
                me.refresh(function($scope) {
                    $scope.thirdUrlLink = url
                })
            })
        }
    },
    goPaymentRecord: function(detail) {
        var me = this
          , scope = me.el.scope();
        if (!(scope.sourcePage == "appointApply" || scope.sourcePage == "pay")) {
            return
        }
        if (detail.appointRecord && detail.appointRecord.appointStatus != 5) {
            return
        }
        if ((detail.appointRecord.organId == 1006601 && detail.appointRecord.appointDepartId == 680 || scope.jumpToOutPaymentSupportDept && scope.jumpToOutPaymentSupportDept.indexOf(detail.appointRecord.appointDepartId) > -1) && scope.isGoPaymentRecord) {
            $sys.openModule({
                url: "eh.wx.health.payment.NoPaymentRecord",
                cache: me.popovers,
                title: "缴费"
            }).then(function(p) {
                scope.isGoPaymentRecord = !1;
                p.module.doHandle({
                    OrganID: detail.appointRecord.organId,
                    mpiId: detail.appointRecord.mpiid,
                    CardID: detail.appointRecord.cardId || "",
                    CardType: detail.appointRecord.cardId ? detail.appointRecord.cardType : "",
                    CardOrgan: detail.appointRecord.cardId || ""
                })
            })
        }
    },
    showError: function() {
        var me = this, scope = me.el.scope(), errorText, detail = scope.item;
        errorText = "检测到就诊人的医保已经升级，为了在本平台可以使用医保卡办理业务，或进行医保结算，请允许本次升级";
        scope.seconds = 3;
        if (detail.appointRecord.cancelResean && scope.showErrorMsg) {
            $sys.showMsg({
                title: detail.appointRecord.cancelResean.indexOf("M001-medCardError") > -1 ? "医保升级" : "",
                template: detail.appointRecord.cancelResean.indexOf("M001-medCardError") > -1 ? "<div>" + errorText + '<div class="tipClass">注:该更新仅对微信账号对应的就诊人有效，其他就诊人请至医院窗口更新</div>' + "</div>" : detail.appointRecord.cancelResean,
                cssClass: "default inpoppa",
                buttons: [{
                    text: detail.appointRecord.cancelResean.indexOf("M001-medCardError") > -1 ? "点击升级" : "确定",
                    type: "button-positive",
                    onTap: function() {
                        detail.appointRecord.cancelResean.indexOf("M001-medCardError") > -1 && me.doUpdateInsuranceCard()
                    }
                }]
            })
        } else if (scope.args && scope.args.insuranceCardUpgrade) {
            $sys.showMsg({
                title: "医保升级",
                template: errorText,
                cssClass: "default inpopp",
                scope: scope,
                buttons: [{
                    text: "升级完成(" + scope.seconds + "S)",
                    type: "button-positive",
                    onTap: function() {}
                }]
            }, 4e3);
            var timer = setInterval(function() {
                scope.seconds--;
                $(".inpopp button").text("升级完成(" + scope.seconds + "S)");
                if (scope.seconds == 0) {
                    $(".inpopp button").text("升级完成");
                    clearInterval(timer)
                }
            }, 1e3)
        }
    },
    getThirdUrl: function(organId) {
        var me = this
          , defer = new $Defer;
        me.service.getShanghai6NCovUrl(organId).then(function(url) {
            defer.resolve(url)
        }).fail(function(e) {
            $sys.showFail(e)
        });
        return defer.promise
    },
    goThirdUrlMethod: function(url) {
        var me = this
          , scope = me.el.scope();
        setTimeout(function() {
            $sys.goLink(url)
        }, 2e3)
    },
    doUpdateInsuranceCard: function() {
        var me = this, scope = me.el.scope(), extraData, paramsWX;
        $sys.showLoading();
        extraData = {
            moduleUrl: "eh.wx.health.person.UserAppointDetail",
            appointId: scope.appointId,
            insuranceCardUpgrade: !0,
            paySource: 0
        };
        paramsWX = [scope.item.patient.mpiId || $AppContext.urt && $AppContext.urt.properties.patient.mpiId, 3, scope.item.appointRecord.organId, extraData];
        me.goThirdLink("insuranceCardUpgrade", paramsWX, extraData)
    },
    formThirdCallBack: function(arg) {
        var me = this;
        arg.extraData.appointId && me.doHandle({
            aid: arg.extraData.appointId,
            insuranceCardUpgrade: arg.extraData.insuranceCardUpgrade
        })
    },
    showStatusWrap: function() {
        var me = this, h = 0, m = 0, t, scope = me.scope(), record = scope.item.appointRecord, wxConfig = $AppContext && $AppContext.wxConfig;
        scope.appointStatusObj = {
            statusTitle: "",
            statusTitleDesc: "",
            statusTitleDescPlus: "",
            secondStatus: "",
            btnTxt: "",
            btnTxtType: ""
        };
        scope.isFontRed = !1;
        if (record.appointStatus == 0 || record.appointStatus == 1 || record.appointStatus == 5 || record.appointStatus == 10) {
            scope.appointStatusObj.statusTitle = record.appointStatusText;
            if (record.appointStatus == 5) {
                scope.appointStatusObj.statusTitle = record.recordType == 1 ? "挂号成功" : "预约成功";
                scope.appointStatusObj.secondStatus = "已支付";
                $AppContext.WXApp && $AppContext.WXApp.preDiagnosisData && $AppContext.WXApp.preDiagnosisData.preDiagnosisEnterFlag && scope.source == "appointApply" && me.showPreDiagnosis()
            }
            scope.appointStatusObj.statusTitleDesc = "请准时于就诊当天前往医院门诊就诊。";
            if (record.appointStatus == 0 && scope.payAhead == -2 || record.appointStatus == 1 || record.appointStatus == 5) {
                scope.appointStatusObj.statusTitleDesc = scope.organInfo.appointSuccessTip
            }
            if (record.organId == "1001281" && (record.appointStatus == 0 || record.appointStatus == 1)) {
                scope.appointStatusObj.statusTitleDescPlus = "预约就诊时间为候诊时间，就诊时间以现场为准。"
            }
            if (record.appointStatus == 0) {
                if ((record.clinicPrice || scope.preBill || scope.organInfo.applyZeroPayFlag) && scope.payAhead != -2 && !record.evaStatus) {
                    scope.appointStatusObj.statusTitle = "待支付";
                    scope.appointStatusObj.secondStatus = "预约成功"
                }
                if (record.organId == "1003366") {
                    scope.appointStatusObj.statusTitle = "提交成功";
                    scope.appointStatusObj.statusTitleDesc = "号源已为您锁定，请在5分钟内完成支付。"
                }
                if (record.organId == "1001865") {
                    scope.appointStatusObj.statusTitleDesc = "同时，来院前完成“在线预约流调”，将使就医更快捷。"
                }
                if (record.organId == "1004539") {
                    scope.appointStatusObj.statusTitleDesc = "请于就诊当日提前通过自助机或收费窗口办理取号后就诊"
                }
                if (record.organId == "1002835") {
                    me.service.getOrganPayTimeSetting(record.organId).then(function(res) {
                        me.refresh(function($scope) {
                            $scope.payTime = res.payTime;
                            $scope.payAhead = res.payAhead;
                            $scope.appointStatusObj.statusTitle = "待支付";
                            if (res.payTime == 0 && (res.payAhead == -1 || res.payAhead == "null" || res.payAhead == "undefined")) {
                                $scope.appointStatusObj.statusTitleDesc = "请在就诊前支付，以免影响您本次就诊。"
                            } else if (res.payTime > 0 && (res.payAhead == -1 || res.payAhead == "null" || res.payAhead == "undefined")) {
                                h = record.appointDate.substr(11, 2);
                                m = record.appointDate.substr(14, 2);
                                m = +m + +res.payTime;
                                if (m >= 60) {
                                    h = +h + 1;
                                    m = m % 60;
                                    if (h >= 24) {
                                        h = "00"
                                    }
                                }
                                h = +h < 10 && +h > 0 ? "0" + +h : h;
                                m = +m < 10 ? "0" + +m : m;
                                t = h + ":" + m;
                                $scope.appointStatusObj.statusTitleDesc = "请于" + t + "前支付，超时系统自动取消。"
                            } else if (res.payAhead == 0) {
                                $scope.appointStatusObj.statusTitleDesc = "请于预约当天完成支付，超时系统自动取消。"
                            } else if (res.payAhead > 0) {
                                $scope.appointStatusObj.statusTitleDesc = "请于就诊日期前" + res.payAhead + "天内支付"
                            } else if (res.payAhead == -2) {
                                $scope.appointStatusObj.statusTitle = record.recordType == 1 ? "挂号成功" : "预约成功";
                                if (record.connectCallNumberSystem) {
                                    $scope.appointStatusObj.statusTitleDesc = "点击查看门诊排队叫号信息 "
                                } else {
                                    $scope.appointStatusObj.statusTitleDesc = "请准时于就诊当天前往医院门诊就诊 "
                                }
                            }
                        });
                        me.countDetailBox()
                    }).fail(function(e) {
                        $sys.showFail(e)
                    })
                }
                if (scope.payAhead != -2 && scope.isDeY && (scope.organInfo.applyZeroPayFlag && !record.clinicPrice || record.clinicPrice)) {
                    scope.appointStatusObj.statusTitleDesc = "锁号成功，锁号时间约15分钟，请及时完成支付。"
                }
            }
        }
        if (record.appointStatus == 2 || record.appointStatus == 6 || record.appointStatus == 7 || record.appointStatus == 8) {
            scope.appointStatusObj.statusTitle = record.cancelType == 4 ? "预约失败" : record.cancelTypeTextTitle;
            scope.appointStatusObj.statusTitleDesc = record.cancelType == 4 || !record.cancelResean ? "" : "取消原因：";
            scope.appointStatusObj.statusTitleDesc += record.cancelResean || "";
            if (record.appointStatus == 6) {
                scope.appointStatusObj.secondStatus = "已退款";
                if (scope.shSixHospital) {
                    scope.appointStatusObj.statusTitleDesc = "您的费用已退回您的账户，请查收！"
                }
            } else if (record.appointStatus == 7) {
                scope.appointStatusObj.secondStatus = "退款失败";
                scope.appointStatusObj.statusTitleDescPlus = scope.refundFailDescription;
                scope.isFontRed = scope.isDeY
            } else if (record.appointStatus == 8) {
                scope.appointStatusObj.secondStatus = "退款中"
            } else if (record.appointStatus == 2 && record.cancelType == 4 && me.showFailUI) {
                scope.appointStatusObj.statusTitle = "预约失败";
                if (!me.jinJiangUi) {
                    scope.appointStatusObj.statusTitleDesc = "";
                    scope.appointStatusObj.secondStatus = "";
                    scope.appointStatusObj.statusTitleDescPlus = ""
                }
                scope.jinJiangUi = me.jinJiangUi;
                scope.$apply();
                return
            } else {
                scope.appointStatusObj.secondStatus = ""
            }
        }
        if (!record.evaStatus && record.appointStatus == 9 || record.appointStatus == 11) {
            scope.appointStatusObj.statusTitle = record.recordType == 1 ? "挂号确认中" : "预约确认中";
            scope.appointStatusObj.statusTitleDesc = record.appointStatus == 9 ? "若长时间未反应，请尝试下拉刷新" : scope.organInfo.appointmentConfirmationDoc || ""
        }
        if (record.appointStatus == 3) {
            scope.appointStatusObj.statusTitle = record.appointStatusText;
            scope.appointStatusObj.statusTitleDesc = "未获取到您的医院就诊记录"
        }
        if (record.connectCallNumberSystem && (record.appointStatus == 0 || record.appointStatus == 1 || record.appointStatus == 4 || record.appointStatus == 5 || record.appointStatus == 10)) {
            scope.appointStatusObj.statusTitleDesc = "点击查看门诊排队叫号信息";
            scope.appointStatusObj.btnTxtType = 3;
            scope.appointStatusObj.btnTxt = "查看叫号"
        }
        if ($AppContext.WXApp.organId == "1005569" && record.appointStatus == 0) {
            scope.appointStatusObj.statusTitle = "提交成功";
            scope.appointStatusObj.statusTitleDesc = "号源已为您锁定，请在2分钟内完成支付"
        }
        if (record.appointStatus == 4) {
            scope.appointStatusObj.statusTitle = record.appointStatusText;
            scope.appointStatusObj.statusTitleDesc = scope.waitPayDescription
        }
        if (record.appointStatus == 12) {
            scope.appointStatusObj.statusTitle = "就诊已结束"
        }
        if (record.appointStatus == 13) {
            scope.appointStatusObj.statusTitle = "已过期";
            scope.appointStatusObj.secondStatus = "预约成功";
            scope.appointStatusObj.statusTitleDesc = "您的预约已过期，请重新预约挂号，如您已就诊，请忽略。"
        }
        if (record.evaStatus) {
            if (record.appointStatus != 3) {
                scope.appointStatusObj.statusTitle = "就诊已结束";
                scope.appointStatusObj.statusTitleDesc = "";
                scope.appointStatusObj.secondStatus = ""
            }
            if (record.evaStatus == 2) {
                scope.appointStatusObj.btnTxtType = 2;
                scope.appointStatusObj.btnTxt = "查看评价"
            } else {
                scope.appointStatusObj.btnTxtType = 1;
                scope.appointStatusObj.btnTxt = "立即评价"
            }
        }
        if (record.appointStatus == 14) {
            scope.appointStatusObj.statusTitle = "就诊已完成"
        }
        scope.$apply();
        if ($env.isWeixin && scope.organInfo.toastCancelReason && record.appointStatus == 2 && record.cancelType == 4 && scope.appointStatusObj.statusTitleDesc) {
            me.refresh(function($scope) {
                $scope.showCancelScrollTip = true
            });
            me.scrollTip()
        } else {
            me.refresh(function($scope) {
                $scope.showCancelScrollTip = false
            })
        }
        me.countDetailBox()
    },
    countDetailBox: function() {
        var me = this
          , el = me.el
          , titleDescLines = 4
          , h = el.find(".userappointdetail .titleDesc").height();
        if (h == 0) {
            titleDescLines = 0
        } else if (h <= 21) {
            titleDescLines = 1
        } else if (h <= 42) {
            titleDescLines = 2
        } else if (h <= 63) {
            titleDescLines = 3
        } else {
            titleDescLines = 4
        }
        me.refresh(function($scope) {
            $scope.titleDescLines = titleDescLines;
            $scope.titleDescFold = titleDescLines == 4;
            $scope.hasBtnWrapChild = el.find(".appoint-btn-box").children().length > 0
        })
    },
    showAllTitleDesc: function() {
        var me = this
          , scope = me.el.scope();
        if (scope.titleDescLines > 3) {
            me.refresh(function($scope) {
                $scope.titleDescFold = !$scope.titleDescFold
            })
        }
    },
    showRestTime: function() {
        var me = this, scope = me.el.scope(), record = scope.item.appointRecord, appointDate = record.appointDate, sysDate = scope.item.systemDate, curDate = new Date(sysDate.replace(/-/g, "/")), endDate = new Date(appointDate.replace(/-/g, "/")), differDate;
        if (scope.payAhead != -2 && !record.evaStatus && (record.clinicPrice || scope.preBill || scope.organInfo.applyZeroPayFlag)) {
            if (scope.payTime > 0) {
                differDate = parseInt((curDate.getTime() - endDate.getTime()) / 1e3, 10);
                scope.restTime = scope.payTime * 60 - differDate;
                scope.restTime = scope.restTime < 0 ? 0 : scope.restTime;
                scope.restMinute = (Math.floor(scope.restTime / 60) >= 10 ? "" : "0") + Math.floor(scope.restTime / 60);
                scope.restSecond = (parseInt(scope.restTime % 60, 10) >= 10 ? "" : "0") + parseInt(scope.restTime % 60, 10);
                scope.showPayRestTime = !0;
                scope.appointTimer && clearInterval(scope.appointTimer);
                scope.appointTimer = setInterval(function() {
                    scope.restTime--;
                    if (scope.restTime <= 0) {
                        scope.restMinute = "00";
                        scope.restSecond = "00";
                        console.log("进入结束");
                        clearInterval(scope.appointTimer)
                    } else {
                        scope.restMinute = (Math.floor(scope.restTime / 60) >= 10 ? "" : "0") + Math.floor(scope.restTime / 60);
                        scope.restSecond = (parseInt(scope.restTime % 60, 10) >= 10 ? "" : "0") + parseInt(scope.restTime % 60, 10)
                    }
                    scope.$apply()
                }, 1e3);
                scope.$apply()
            }
        }
    },
    showPreDiagnosis: function() {
        var me = this, scope = me.el.scope(), param;
        $sys.showMsg({
            title: "",
            template: '<div style="text-align: center">为缩短患儿在院等候时间，我院对就医流程进一步优化，为就诊患儿提供诊前常规检验服务。快去申请吧。</div>',
            scope: scope,
            cssClass: "default",
            buttons: [{
                text: "暂时不用",
                type: "button-default"
            }, {
                text: "预约诊前检验",
                type: "button-positive",
                onTap: function() {
                    $sys.openModule({
                        url: "eh.wx.health.preDiagnosis.SelectInspectItem",
                        cache: me.popovers,
                        destroy: !0,
                        title: "选择检验项目"
                    }).then(function(p) {
                        param = scope.item && scope.item.appointRecord;
                        param.patientSexText = scope.item && scope.item.patient && scope.item.patient.patientSexText;
                        param.age = scope.item && scope.item.patient && scope.item.patient.age;
                        p.module.initSelectInspectItem(param, scope.organInfo)
                    })
                }
            }]
        })
    },
    scrollTip: function() {
        var me = this, el = me.el, scope = el.scope(), left, noticeWidth = el.find(".bd")[0] && el.find(".bd")[0].offsetWidth || 0, outerWidth = el.find(".tip-box")[0] && el.find(".tip-box")[0].offsetWidth || 0;
        if (noticeWidth > outerWidth) {
            el.find(".bd").css("left", 0);
            scope.scrollTipTimer = setInterval(function() {
                left = parseInt(el.find(".bd").css("left"), 10);
                el.find(".bd").css("left", (left > -noticeWidth ? left - 1 : outerWidth) + "px")
            }, 40)
        }
    },
    getSpecialAppointDetail: function(appointId, showEva, refresh) {
        var me = this, switchFlag;
        me.service.getPatTransferById(appointId).then(function(detail) {
            $sys.hideLoading();
            switchFlag = false;
            me.refresh(function($scope) {
                $scope.clickAlreadyPay = !0;
                $scope.detailFalg = 1;
                $scope.item = detail;
                $scope.photo = $scope.getAva(detail.doctor);
                $scope.switchFlag = switchFlag;
                $scope.cdrOtherdocsLen = detail.cdrOtherdocs.length;
                $scope.appointId = appointId;
                $scope.listInfoType = "specialAppoint";
                $scope.showMoveTips = !$env.hybrid && !$env.getCookie("appkey") && !$env.isAlipay && $env.isWeixin;
                refresh && $scope.$broadcast("scroll.refreshComplete")
            });
            me.resetCdrImgStyle();
            showEva && me.showEvaluatePop()
        }).fail(function(e) {
            $sys.showFail(e)
        })
    },
    showDepartInfo: function(obj) {
        var me = this;
        me.service.getDoctorDeptInfo(obj.appointRecord.appointDepartId, obj.appointRecord.organId).then(function(e) {
            me.refresh(function($scope) {
                $scope.organProfessionText = e.length && e[0][0];
                $scope.organText = e.length && e[0][1]
            })
        }).fail(function(e) {
            $sys.showFail(e)
        })
    },
    canShowPayBtn: function() {
        var me = this
          , scope = me.scope()
          , appointInfo = scope.item
          , record = scope.item.appointRecord
          , appointAutoPay = scope.appointAutoPay;
        me.refresh(function($scope) {
            $scope.showPayBtn = appointInfo.payBtn;
            $scope.judgePayBtnShow = (record.appointStatus == 0 || record.appointStatus == 4) && scope.isPayBtnShow && scope.showPayBtn && !record.evaStatus && (record.clinicPrice || scope.preBill || scope.organInfo.applyZeroPayFlag)
        });
        me.showStatusWrap();
        if (record.recordType != 2 && appointInfo.payBtn && record.preSettleFlag) {
            me.getAppointTitle()
        }
        scope.source == "appointApply" && scope.judgePayBtnShow && (appointAutoPay.length ? appointAutoPay.length == 2 ? 1 : appointAutoPay[0] == "1" ? 1 : record.appointDate.slice(0, 10) == record.workDate ? 1 : 0 : 0) && !scope.zeroRegStatus && me.payOrderNow()
    },
    getAppointTitle: function() {
        var me = this
          , scope = me.scope()
          , record = scope.item.appointRecord;
        if (record.appointStatus == 4) {
            scope.appointStatusObj.statusTitle = record.appointStatusText + ":";
            if (record.clinicPrice != undefined && record.clinicPrice != record.regAmt) {
                scope.appointStatusObj.statusTitle += record.clinicPrice ? record.clinicPrice + "元" : scope.zeroDisplay || "无"
            }
            if (record.clinicPrice == undefined || record.clinicPrice != undefined && record.clinicPrice == record.regAmt) {
                scope.appointStatusObj.statusTitle += +record.regAmt || +record.clinicPrice ? (record.regAmt || record.clinicPrice) + "元" : scope.zeroDisplay || "无"
            }
        }
        me.refresh(function($scope) {
            $scope.appointStatusObj.statusTitle = scope.appointStatusObj.statusTitle;
            $scope.preBill = record.clinicPrice
        })
    },
    showDoctIndexPop: function() {
        var me = this
          , scope = me.el.scope()
          , targetDoct = scope.item.doctor
          , moduleInfo = $sys.getDoctIndexPageInfo(targetDoct);
        $sys.openModule({
            title: moduleInfo.title,
            cache: me.popovers,
            manualLoading: !0,
            url: moduleInfo.url
        }).then(function(p) {
            p.module.getDoctInfo(targetDoct.doctorId, "user")
        })
    },
    showDoctIndexPopWeb: function() {
        var me = this
          , scope = me.el.scope()
          , targetDoct = scope.item.doctor
          , doctid = targetDoct.doctorId;
        $sys.openModule({
            title: targetDoct.teams ? "团队医生介绍" : "医生介绍",
            cache: me.popovers,
            url: "eh.wx.health.doctor.DoctDetail",
            destroy: !0,
            events: {
                doctIndexFollowChanged: {
                    callback: function() {
                        me.emit("doctListFollowChanged")
                    }
                }
            }
        }).then(function(p) {
            p.module.getDoctDetail(doctid)
        })
    },
    reAppointApply: function() {
        var me = this, scope = me.el.scope(), args;
        $sys.openModule({
            url: "eh.wx.health.doctFinder.DateSelectDoct",
            cache: me.popovers,
            manualLoading: !0,
            args: {
                organId: scope.item.appointRecord && scope.item.appointRecord.organId
            }
        }).then(function(popover) {
            args = {
                deptId: scope.item.department && scope.item.department.deptId,
                deptName: scope.item.department && scope.item.department.name,
                organId: scope.item.appointRecord && scope.item.appointRecord.organId,
                shortName: scope.item.appointRecord && scope.item.appointRecord.organIdText
            };
            popover.module.doHandle(args)
        })
    },
    showSystemSuggestPop: function() {
        var me = this
          , scope = me.el.scope()
          , setInfo = {
            head: "取消预约",
            title: "请选择取消原因",
            placeholder: "",
            name: scope.item.patient.patientName,
            mpiId: scope.item.patient.mpiId,
            appointId: scope.item.appointRecord ? scope.item.appointRecord.appointRecordId : scope.item.appointRecordId,
            cancelAppointRemindText: scope.organInfo.cancelAppointRemindText
        }
          , status = scope.item
          , doc = status.appointRecord || status.transfer;
        if (doc.statusName == "doc") {
            setInfo.docAppoint = true
        }
        $sys.openModule({
            title: "取消预约",
            cache: me.popovers,
            url: "eh.wx.health.person.SystemSuggest",
            events: {
                cancelAppoint: {
                    autoclose: !0,
                    callback: function(obj) {
                        setInfo.appointId = obj ? obj : setInfo.appointId;
                        scope.item.appointRecord.appointStatus = 2;
                        scope.source = "";
                        me.getSourceAppointDetail(setInfo.appointId);
                        me.emit("cancelAppoint");
                        $sys.getScroll("myAppointintScroll").scrollTo(0, 0, !1)
                    }
                }
            }
        }).then(function(p) {
            p.module.setPageInfo("cancelAppoint", setInfo)
        })
    },
    checkCurNum: function() {
        var me = this
          , scope = me.el.scope()
          , appointId = scope.item.appointRecord && scope.item.appointRecord.appointRecordId || scope.item.transfer && scope.item.appointRecordId
          , organId = scope.item.appointRecord && scope.item.appointRecord.organId || scope.item.transfer && scope.item.transfer.confirmOrgan;
        appointId && $sys.openModule({
            url: "eh.wx.health.person.CallNumber",
            cache: me.popovers
        }).then(function(p) {
            p.module.showInfo({
                aid: appointId
            }, organId)
        })
    },
    showPreconsultation: function() {
        var me = this
          , scope = me.scope()
          , mpiId = scope.item.patient.mpiId
          , url = scope.preconsultUrl;
        $sys.goLink(url)
    },
    payNowValidateDetail: function() {
        $sys.hideLoading();
        var me = this
          , scope = me.scope();
        if (scope.organInfo.MedicalInsuranceWindow) {
            me.refresh(function($scope) {
                $scope.menuShowFlag = !0
            })
        } else {
            me.payNowDetail()
        }
    },
    payNowDetail: function() {
        var me = this, scope = me.el.scope(), firstFlag, record = scope.item.appointRecord ? scope.item.appointRecord : scope.item;
        me.showLoading();
        scope.special = scope.item.transfer ? "1" : "0";
        me.loadProcess();
        (scope.accountType ? me.service.settlePreBillForBusNew(+record.appointRecordId, $AppContext.WXApp.payWay, {
            medicalInsuranceSupport: "1"
        }) : me.service.settlePreBillForBus(+record.appointRecordId, $AppContext.WXApp.payWay)).then(function(res) {
            me.hideLoading();
            clearInterval(me.processTimer);
            firstFlag = $AppContext.WXApp.organId == "1000874" && res.firstRegisterPatient;
            if (res.errorCode == "canPay" && !firstFlag) {
                var data = {
                    appointRecordId: +record.appointRecordId,
                    busType: "appointPay"
                };
                scope.isWeiningPay ? me.weiNingFormData(data) : me.goConfirm(res.preBill, record)
            } else {
                $sys.showMsg({
                    template: firstFlag ? "您好，本单位首诊患者须先到收费窗口建档支付！" : res.errorMsg,
                    cssClass: "default",
                    buttons: [{
                        text: "确定",
                        type: "button-positive"
                    }]
                });
                scope.clickAlreadyPay = !0;
                scope.$applyAsync()
            }
        }).fail(function(e) {
            scope.clickAlreadyPay = !0;
            scope.$applyAsync();
            me.hideLoading();
            clearInterval(me.processTimer);
            $sys.showFail(e)
        })
    },
    applySocialCardCallBack: function(callBackData) {
        var me = this
          , el = me.el
          , scope = el.scope()
          , data = callBackData.extraData;
        scope.source = "";
        data && data.appointRecordId && (data.detailType == "specialAppoint" ? me.getSpecialAppointDetail(data.appointRecordId) : me.getSourceAppointDetail(data.appointRecordId))
    },
    weiNingFormData: function(data) {
        var me = this
          , scope = me.el.scope()
          , param = {}
          , extraDataWX = {
            extraData: {
                appointRecordId: data.appointRecordId,
                detailType: scope.listInfoType
            },
            fromModuleUrl: "eh.wx.health.person.UserAppointDetail"
        };
        param = {
            mpiId: scope.item.patient.mpiId,
            busType: "appointpay",
            busId: data.appointRecordId,
            extraData: extraDataWX
        };
        me.service.getSignNoAndSaveData(param).then(function(res) {
            me.showPayTip ? $sys.showMsg({
                template: "您好！当前挂号为线上支付，如需退号请当日到医院窗口。",
                cssClass: "default",
                buttons: [{
                    text: "取消",
                    type: "button-default",
                    onTap: function() {
                        scope.clickAlreadyPay = !0;
                        scope.$applyAsync()
                    }
                }, {
                    text: "确认",
                    type: "button-positive",
                    onTap: function() {
                        me.payOnCurrentPage(data.appointRecordId, data.busType, null, null, {
                            moduleUrl: "eh.wx.health.person.UserAppointDetail",
                            appointId: data.appointRecordId
                        })
                    }
                }]
            }) : me.payOnCurrentPage(data.appointRecordId, data.busType, null, null, {
                moduleUrl: "eh.wx.health.person.UserAppointDetail",
                appointId: data.appointRecordId
            })
        }).fail(function(e) {
            scope.clickAlreadyPay = !0;
            scope.$applyAsync();
            $sys.showFail(e)
        })
    },
    payFirstHandle: function() {
        var me = this
          , scope = me.el.scope()
          , record = scope.item.appointRecord ? scope.item.appointRecord : scope.item
          , param = {};
        $sys.getClientConfigs().then(function(res) {
            if (res.gotoQuestionUrls[0] == "ngari_znwz_url" && record.appointRecordExt.thirdSelfDiagnosisInquriyId) {
                param = {
                    aid: +record.appointRecordId,
                    uniqueIndex: record.appointRecordExt.thirdSelfDiagnosisInquriyId
                };
                me.service.getQuestionUrls("appointDetail", null, param).then(function(data) {
                    $sys.goLink(data)
                }).fail(function(e) {
                    scope.clickAlreadyPay = !0;
                    scope.$applyAsync();
                    $sys.showFail(e)
                })
            } else {
                me.payNowValidateDetail()
            }
        }).fail(function() {
            scope.clickAlreadyPay = !0;
            scope.$applyAsync()
        })
    },
    loadProcess: function() {
        var me = this
          , el = me.el
          , iSpeed = 1;
        me.processTimer = setInterval(function() {
            iSpeed += 1;
            if (iSpeed >= 90) {
                clearInterval(me.processTimer)
            }
            document.getElementById("bar").style.width = iSpeed + "%"
        }, 500)
    },
    insuranceCardOrSelfPay: function() {
        var me = this
          , scope = me.scope();
        if (scope.barcodeId) {
            switch (+scope.item.healthCardType) {
            case 1:
                me.payFirstHandle();
                break;
            case 2:
                $sys.hideLoading();
                if (!scope.isWeiningPay) {
                    if (scope.organInfo.supportInsuranceCardSelfPay) {
                        $sys.showMsg({
                            template: !me.cardTips ? "尊敬的医保病人，目前暂不支持医保支付，只能自费结算，是否继续？" : "因医保要求，目前仅支持手机用户本人的医保账户进行在线支付，如非手机用户本人，请前往医院自助机或窗口结算。",
                            cssClass: "default",
                            buttons: [{
                                text: "继续",
                                type: "button-positive",
                                onTap: function() {
                                    $sys.showLoading();
                                    me.payFirstHandle()
                                }
                            }, {
                                text: "取消",
                                type: "button-default",
                                onTap: function() {
                                    scope.clickAlreadyPay = !0;
                                    scope.$applyAsync()
                                }
                            }]
                        })
                    } else {
                        $sys.showMsg("尊敬的医保病人，目前暂不支持医保支付，请线下支付", 2e3, "toast-auto");
                        scope.clickAlreadyPay = !0;
                        scope.$applyAsync()
                    }
                } else {
                    me.payFirstHandle()
                }
                break;
            default:
                me.payFirstHandle();
                break
            }
        } else {
            me.payFirstHandle()
        }
    },
    onApilyTypeSelected: function(accountType) {
        var me = this
          , scope = me.el.scope();
        scope.menuShowFlag = !1;
        scope.accountType = accountType;
        me.payNowDetail()
    },
    goConfirm: function(preBill, record) {
        var me = this, scope = me.el.scope(), args, mpiId = $AppContext.urt.properties.patient.mpiId;
        if ($env.isMinapp() && ($env.ngariMiniAppForWeb || $env.ngariWeApp)) {
            var url = "/pages/appointPayment/index?id=" + (+record.appointRecordId || scope.appointId) + "&busType=appointPay";
            if ($env.isAliapp) {
                my.navigateTo({
                    url: url
                })
            } else {
                wx.miniProgram.navigateTo({
                    url: url
                })
            }
            return
        }
        args = {
            mpiId: mpiId,
            appointId: +record.appointRecordId || scope.appointId,
            organId: +record.organId,
            price: preBill,
            busType: "appointPay",
            couponType: 3,
            special: scope.special,
            transferId: record.transfer ? record.transfer.transferId : "",
            appointDepartId: record.appointDepartId,
            cardId: record.cardId || "",
            cardType: record.cardType ? record.cardType : record.cardId ? 1 : "",
            cardOrgan: record.cardId ? record.organId : ""
        };
        $sys.openModule({
            url: "eh.wx.health.doctor.AppointApplyOrderConfirm",
            cache: me.popovers,
            title: "确认订单",
            events: {
                backClose: {
                    autoclose: !0
                },
                refreshAppointDetail: {
                    callback: function(appointId) {
                        scope.isGoPaymentRecord = !1;
                        scope.appointId = appointId || scope.appointId;
                        scope.appointId && me.refreshDetailData()
                    }
                }
            }
        }).then(function(popover) {
            popover.module.showAppointDeConfirmPayDetail(args)
        })
    },
    refreshDetailData: function() {
        var me = this
          , scope = me.el.scope()
          , evaFlag = scope.args ? scope.args.evaflag : "";
        if (scope.listInfoType == "sourceAppoint" && scope.appointId) {
            scope.count = 0;
            scope.source = "";
            me.getSourceAppointDetail(scope.appointId, evaFlag, !0)
        } else if (scope.listInfoType == "specialAppoint" && scope.appointId) {
            me.getSpecialAppointDetail(scope.appointId, evaFlag, !0)
        }
    },
    showEvaluatePop: function() {
        var me = this
          , scope = me.el.scope()
          , items = scope.item.appointRecord ? scope.item.appointRecord : scope.item.transfer
          , args = {
            feedbackId: items.feedbackId,
            isEva: items.evaStatus,
            consultId: items.appointRecordId || items.transferId,
            doctor: items.doctorId || items.confirmDoctor,
            uid: $AppContext.urt.id,
            typeCode: scope.item.appointRecord ? 4 : 1,
            type: scope.item.transfer ? "specialAppoint" : "sourceAppoint",
            organId: items.organId || items.confirmOrgan,
            departmentId: items.appointDepartId || items.confirmDepart,
            info: items
        };
        if (args.isEva == 2) {
            $sys.openModule({
                title: "医生评价",
                cache: me.popovers,
                url: "eh.wx.health.evaluationCenter.EvaluationList"
            }).then(function(p) {
                p.module.initEvaList(args.doctor)
            })
        } else {
            $sys.openModule({
                url: "eh.wx.health.evaluate.PublishEvaForAppoint",
                cache: me.popovers,
                title: "发布评价",
                destroy: !0,
                events: {
                    updateAppointEva: {
                        autoclose: !0,
                        callback: function(res) {
                            me.getAppointDetailInfo(res.type, res.consultId, scope.rType || 0, "")
                        }
                    },
                    updateAppointEva2: {
                        callback: function(res) {
                            me.getAppointDetailInfo(res.type, res.consultId, scope.rType || 0, "")
                        }
                    }
                }
            }).then(function(popover) {
                popover.module.initEva(args)
            })
        }
    },
    showBackHomeBtn: function() {
        var me = this
          , scope = me.el.scope();
        scope.goBackHomeFlag = false;
        scope.$apply()
    },
    initOrganInfo: function(detail) {
        var me = this, scope = me.el.scope(), timeSolt, temp, defer = new $Defer, workType = "", record = scope.item.appointRecord || scope.item, startTime;
        me.getOrganConfigInfo(record.organId).then(function(organCfgInfo) {
            scope.organInfo = organCfgInfo;
            scope.payTime = organCfgInfo.payTime || 0;
            scope.payAhead = organCfgInfo.payAhead;
            scope.RegisterToAppoint = organCfgInfo.RegisterToAppoint;
            scope.showReAppointButton = organCfgInfo.showReAppointButton ? !0 : !1;
            scope.zeroDisplay = organCfgInfo.zeroDisplay[0];
            scope.showOrderNumber = +organCfgInfo.appointSourceDisplayRule == 0 || scope.shSixHospital && (record.appointStatus == 5 || record.appointStatus == 12) && record.appointId && record.organAppointId && record.appointId != record.organAppointId;
            scope.appointOrderText = organCfgInfo.appointOrderText && organCfgInfo.appointOrderText.split("\n") || [];
            scope.isWeiningPay = record.recordType ? organCfgInfo.supportAppointTodayWeiNingPay : organCfgInfo.supportAppointWeiNingPay;
            scope.jumpToOutPaymentSupportDept = organCfgInfo.jumpToOutPaymentSupportDept;
            scope.OfflineAppointPayCancel = organCfgInfo.OfflineAppointPayCancel;
            scope.appointOfflinePayOnline = organCfgInfo.appointOfflinePayOnline;
            scope.electronicInvoiceSwitch = organCfgInfo.electronicInvoiceSwitch && organCfgInfo.electronicInvoiceSwitch[0];
            me.getSourceLevelText(detail);
            scope.waitingTimeInfo = "";
            workType = (organCfgInfo.HisWorkType && record.appointRecordExt && record.appointRecordExt.hisWorkType ? record.appointRecordExt.hisWorkType : record.workTypeText) || "";
            timeSolt = me.filter("customDate")(record.startTime, "hh:mm") + "~" + me.filter("customDate")(record.endTime, "hh:mm");
            if (!scope.showNumTime) {
                temp = [null, me.filter("shortDate")(record.workDate), workType, me.filter("customDate")(record.startTime, "hh:mm"), timeSolt]
            } else {
                temp = [null, me.filter("shortDate")(record.workDate), "", "", ""]
            }
            if (!record.recordType || record.recordType == 1 && scope.RegisterToAppoint) {
                me.waitingTime(organCfgInfo.appointTimeRule.sort(), temp)
            } else if (record.recordType == 2) {
                me.waitingTime(["1", "2", "3"], temp)
            } else {
                me.waitingTime(organCfgInfo.registerTimeRule.sort(), temp)
            }
            if (scope.item.preciseAppointmentTime) {
                scope.waitingTimeInfo = scope.item.preciseAppointmentTime
            }
            if (scope.showNumTime) {
                scope.callingNumTime = "";
                startTime = new Date(record.startTime.replace(/-/g, "/"));
                startTime.setMinutes(startTime.getMinutes() - 30);
                scope.callingNumTime = me.filter("customDate")(startTime, "hh:mm") + "~" + me.filter("customDate")(record.startTime, "hh:mm")
            }
            scope.$apply();
            defer.resolve()
        }).fail(function(e) {
            $sys.showFail(e);
            defer.reject()
        });
        return defer.promise
    },
    getSourceLevelText: function(detail) {
        var me = this
          , scope = me.el.scope()
          , organCfgInfo = scope.organInfo;
        if (!organCfgInfo.showHisSourceLevelText || organCfgInfo.showHisSourceLevelText && !detail.appointRecord.hisSourceLevelText) {
            scope.showSourceLevelText = detail.appointRecord.sourceLevelText;
            if (detail.appointRecord.sourceLevel == 1) {
                scope.showSourceLevelText = "普通门诊"
            } else if (detail.appointRecord.sourceLevel == 2) {
                scope.showSourceLevelText = "专家门诊"
            } else if (detail.appointRecord.sourceLevel == 3) {
                scope.showSourceLevelText = "特需门诊"
            }
        } else {
            scope.showSourceLevelText = detail.appointRecord.hisSourceLevelText
        }
        if (detail.appointRecord.recordType == 2) {
            scope.showSourceLevelText = "远程联合门诊";
            detail.appointRecord.statusName = "remoteClinic"
        }
    },
    waitingTime: function(arr, temp) {
        var me = this
          , i = 0
          , scope = me.el.scope();
        for (i; i < arr.length; i++) {
            scope.waitingTimeInfo += temp[arr[i]] + " "
        }
    },
    initClientConfigs: function() {
        var me = this
          , scope = me.el.scope()
          , defer = new $Defer;
        me.getAllClientConfigs().then(function(clientCfgInfo) {
            scope.supportWholeSomeCard = clientCfgInfo.supportWholeSomeCard;
            scope.isDisplayHomeBox = clientCfgInfo.isDisplayHomeBox;
            scope.$apply();
            defer.resolve()
        }).fail(function(e) {
            $sys.showFail(e);
            defer.reject()
        });
        return defer.promise
    },
    initBarcodeInfo: function() {
        var me = this, scope = me.el.scope(), cardId, record = scope.item.appointRecord || scope.item;
        if (scope.supportWholeSomeCard || scope.organInfo.organHealthCardSupport) {
            cardId = record.cardId || record.medInsureCarId;
            scope.barcodeId = scope.barcodeName = me.barcodeNoneCardType && cardId && cardId.indexOf("_") > 0 ? cardId.substring(0, cardId.indexOf("_")) : cardId
        }
        scope.$apply()
    },
    showBarcodeInfo: function(isMzId) {
        var me = this
          , scope = me.el.scope()
          , record = scope.item.appointRecord ? scope.item.appointRecord : scope.item;
        me.service.queryHealthCardFromHisAndMerge(record.organId, record.mpiid, !0).then(function(e) {
            if (e.length > 0) {
                me.goBarcodeInfoPage(isMzId, record.cardId)
            }
        })
    },
    goBarcodeInfoPage: function(isMzId, barcodeId) {
        var me = this, record, cardInfo, scope = me.el.scope();
        record = scope.item.appointRecord ? scope.item.appointRecord : scope.item;
        cardInfo = {
            cardId: isMzId ? barcodeId : scope.barcodeId,
            cardOrgan: record.organId,
            cardTextName: isMzId ? record.appointRecordExt.clinicMzIdName || "门诊号" : "就诊卡号"
        };
        $sys.openModule({
            title: isMzId ? record.appointRecordExt.clinicMzIdName || "门诊号" : "就诊卡号",
            cache: me.popovers,
            url: "eh.wx.health.person.BarCodeInfo"
        }).then(function(p) {
            p.module.initBar({
                cardInfo: cardInfo,
                it: "",
                curMember: scope.item.patient,
                entrance: "",
                prevPage: "userAppointDetail",
                notMedical: !0,
                isMzId: isMzId
            })
        })
    },
    showBackHomeBtnWeb: function() {
        var me = this
          , scope = me.el.scope();
        scope.goBackHomeFlag = true;
        scope.$apply()
    },
    countAge: function(appointDate, birthday) {
        var a = new Date(birthday.substr(0, 10))
          , b = new Date(appointDate.substr(0, 10))
          , day = (b - a) / 1e3 / 3600 / 24
          , text = "";
        if (day > 0 && day <= 100) {
            text += day + "天"
        } else if (day > 100 && day < 365) {
            text += Math.floor(day / 30) + "个月"
        } else if (day >= 365) {
            text += Math.floor(day / 365) + "岁"
        } else {
            text = ""
        }
        return text
    },
    showElecHealthCardInfo: function() {
        var me = this;
        me.getPatientInfoByMpiid().then(function(patientInfo) {
            $sys.openModule({
                url: "eh.wx.health.pubElecHealthCard.ElecHealthCardInfo",
                cache: me.popovers,
                title: "健康卡"
            }).then(function(p) {
                p.module.initHealthCardInfo(patientInfo.mpiId, "appointDetail")
            })
        })
    },
    getPatientInfoByMpiid: function() {
        var me = this
          , scope = me.scope()
          , mpiId = scope.item.patient.mpiId
          , defer = new $Defer;
        me.service.getPatientInfoDS(mpiId).then(function(patientInfo) {
            defer.resolve(patientInfo)
        }).fail(function(e) {
            $sys.showFail(e)
        });
        return defer.promise
    },
    getAllClientConfigs: function() {
        var me = this
          , defer = new $Defer;
        me.service.getAllClientConfigs().then(function(res) {
            defer.resolve(res)
        }).fail(function(e) {
            $sys.showFail(e)
        });
        return defer.promise
    },
    getClinicMzIdInstructions: function() {
        var me = this
          , scope = me.scope()
          , defer = new $Defer
          , organId = scope.item.appointRecord ? scope.item.appointRecord.organId : scope.item.organId;
        me.service.findConfigurations(organId, ["healthCardInstructions", "appointAutoPay", "waitPayDescription", "refundFailDescription"]).then(function(res) {
            me.refresh(function($scope) {
                $scope.clinicMzIdTips = res.healthCardInstructions;
                $scope.appointAutoPay = res.appointAutoPay;
                $scope.waitPayDescription = res.waitPayDescription;
                $scope.refundFailDescription = res.refundFailDescription
            });
            defer.resolve()
        }).fail(function(e) {
            defer.reject();
            $sys.showFail(e)
        });
        return defer.promise
    },
    getClinicMzCode: function() {
        var me = this
          , el = me.el
          , scope = el.scope()
          , appointRecord = scope.item.appointRecord
          , qrNum = appointRecord.appointRecordExt.appointDetailQrNum
          , appointDate = parseInt(appointRecord.endTime.substr(0, 10).replace(/-/g, ""), 10)
          , currentDate = parseInt($AppContext.serverDate.substr(0, 10).replace(/-/g, ""), 10);
        if (scope.isHSFive && qrNum && qrNum.length == 9 && currentDate == appointDate) {
            me.service.cardEncryption(qrNum, appointRecord.organId).then(function(res) {
                me.initQrCode(res)
            }).fail(function(e) {
                $sys.hideLoading();
                $sys.showFail(e)
            })
        } else {
            me.initQrCode(qrNum)
        }
    },
    initQrCode: function(qrNum) {
        var me = this, el = me.el, qrcode;
        me.barCode = new eh.wx.wap.portal.code;
        el.find("#barCode").html(me.barCode.code128(qrNum, "B"));
        if (el.find("#qrcode")[0]) {
            el.find("#qrcode")[0].innerHTML = "";
            qrcode = new QRCode(el.find("#qrcode")[0],{
                text: qrNum,
                width: 168,
                height: 168,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            })
        }
        me.resetScroll()
    },
    getOrganConfigInfo: function(organId) {
        var me = this
          , scope = me.el.scope()
          , defer = new $Defer;
        $sys.showLoading();
        me.service.getOrganConfig(organId).then(function(res) {
            $sys.hideLoading();
            defer.resolve(res)
        }).fail(function(e) {
            $sys.hideLoading();
            defer.reject(e)
        });
        return defer.promise
    },
    showElecInvoiceFunc: function() {
        var me = this
          , scope = me.scope();
        if (scope.invoiceUrl) {
            $sys.goLink(scope.invoiceUrl)
        } else {
            $sys.openModule({
                url: "eh.wx.health.payment.ElecInvoice",
                cache: me.popovers,
                title: "电子票据"
            }).then(function(popover) {})
        }
    },
    showElecInvoiceQrcodeFunc: function() {
        var me = this
          , scope = me.scope();
        $sys.openModule({
            url: "eh.wx.health.payment.ElecInvoiceQrcode",
            cache: me.popovers,
            title: "电子票据",
            manualLoading: !0
        }).then(function(popover) {
            popover.module.initElecInvoiceQrcode(scope.invoiceCode)
        })
    },
    showQrcodeFunc: function() {
        var me = this
          , scope = me.el.scope()
          , params = {
            qrcodeStr: scope.regQrCode,
            isBase64: !0
        };
        $sys.openModule({
            url: "eh.wx.health.payment.showQrcodePage",
            cache: me.popovers
        }).then(function(popover) {
            popover.module.initQrcode(params)
        })
    },
    getQrcode: function(detail) {
        var me = this
          , scope = me.el.scope()
          , defer = new $Defer
          , recode = detail.appointRecord;
        $sys.showLoading();
        me.service.getBillByBusOrderNo(recode.organId, detail.patient.mpiId, recode.appointRecordId).then(function(res) {
            me.refresh(function($scope) {
                $scope.regQrCode = res || ""
            });
            $sys.hideLoading();
            defer.resolve(res)
        }).fail(function(e) {
            $sys.hideLoading();
            defer.reject(e)
        });
        return defer.promise
    },
    refundApply: function() {
        var me = this
          , scope = me.el.scope();
        $sys.showMsg({
            title: "取消预约并发起退费申请",
            template: scope.organInfo.refundApplyText,
            cssClass: "default",
            buttons: [{
                text: "取消",
                type: "button-default"
            }, {
                text: "确定",
                type: "button-positive",
                onTap: function() {
                    $sys.showLoading();
                    me.service.applyAppointRecordRefund(scope.appointId).then(function(res) {
                        $sys.hideLoading();
                        $sys.showMsg({
                            title: "退费申请成功",
                            template: res || "",
                            cssClass: "default",
                            buttons: [{
                                text: "确定",
                                type: "button-positive",
                                onTap: function() {
                                    me.getSourceAppointDetail(scope.appointId)
                                }
                            }]
                        })
                    }).fail(function(e) {
                        $sys.hideLoading();
                        if (e.code == 609) {
                            $sys.showMsg({
                                title: "退费申请失败",
                                template: e.msg || "",
                                cssClass: "default",
                                buttons: [{
                                    text: "确定",
                                    type: "button-positive"
                                }]
                            })
                        } else {
                            $sys.showFail(e)
                        }
                    })
                }
            }]
        })
    },
    queryRefundStatus: function() {
        var me = this, scope = me.el.scope(), applyStatus, applyStatusInfo = ["您的费用正在审核中，请耐心等待！", "你的费用已审核通过，稍后会原路退回，请您注意查看！", "您的费用审核不通过，请至线下办理退费", "挂号费用已退回您的账户。"];
        $sys.showLoading();
        me.service.queryAppointRefundStatus(scope.appointId).then(function(res) {
            $sys.hideLoading();
            if (res && res.length) {
                applyStatus = res[0].applyStatus;
                $sys.showMsg({
                    title: res[0].applyStatusText || "",
                    template: res[0].applyStatusMessage || applyStatusInfo[applyStatus] || "",
                    cssClass: "default",
                    buttons: [{
                        text: "确定",
                        type: "button-positive",
                        onTap: function() {
                            if (applyStatus == 1) {
                                me.getSourceAppointDetail(scope.appointId)
                            }
                        }
                    }]
                })
            } else {
                $sys.showMsg("暂未查询到审核进度")
            }
        }).fail(function(e) {
            $sys.hideLoading();
            $sys.showFail(e)
        })
    },
    goTongueConsult: function() {
        var me = this
          , scope = me.el.scope();
        history.replaceState($apply({}, {
            title: "我的预约单"
        }, history.state), null, me.funcUrlDel("module"));
        $sys.goLink(scope.tongueConsultUrl)
    },
    funcUrlDel: function() {
        var me = this
          , scope = me.el.scope()
          , wLocal = window.location
          , baseUrl = wLocal.origin + wLocal.pathname + "?"
          , query = wLocal.search.substr(1);
        var obj = {};
        obj["module"] = "appoint";
        obj["aid"] = scope.appointId;
        var arr = query.split("&");
        for (var i = 0; i < arr.length; i++) {
            arr[i] = arr[i].split("=");
            if (arr[i][0] == "module" || arr[i][0] == "aid") {
                continue
            }
            obj[arr[i][0]] = arr[i][1]
        }
        var url = baseUrl + JSON.stringify(obj).replace(/[\"\{\}]/g, "").replace(/\:/g, "=").replace(/\,/g, "&");
        return url
    },
    doHandle: function(args) {
        var me = this, appointId, scope = me.el.scope(), requestDate = new Date;
        me.weiningBack = !1;
        $sys.hideLoading();
        if (args.paySource == -1) {
            scope.showErrorMsg = !0
        } else if (args.paySuccessFlag == 1 && ($env.isAlipay && ($env.ngariMiniAppForWeb || $env.ngariWeApp))) {
            me.api.aliSuccessLog("挂号缴费", requestDate)
        }
        if ($env.hybrid && $env.appkey == "zzrmyy") {
            me.weiningBack = !0
        }
        scope.isPayBtnShow = $env.getCookie("appkey") == "aliAppointDetail" ? !1 : $env.appkey == "jins_jiankt" ? !1 : !0;
        scope.args = args;
        scope.source = "";
        appointId = args.aid;
        $AppContext.WXApp.isFromYc = args && args.pageSource == "pageYC" ? (!0,
        document.title = me.title) : !1;
        appointId && (args.special == "1" ? me.getSpecialAppointDetail(appointId, args.evaflag) : me.getSourceAppointDetail(appointId, args.evaflag))
    },
    emitBack: function(e) {
        var me = this
          , scope = me.el.scope()
          , status = scope.item.appointRecord ? scope.item.appointRecord.appointStatus : scope.item.appointStatus
          , record = scope.item.appointRecord && scope.item.appointRecord.recordType;
        clearInterval(me.processTimer);
        scope.clickAlreadyPay = !0;
        scope.$apply();
        if (e) {
            clearInterval(scope.scrollTipTimer);
            clearInterval(scope.appointTimer);
            me.emit("updateGuidance");
            scope.isGoPaymentRecord = !0;
            scope.isRefreshDetail = !0
        }
        if (me.weiningBack) {
            $env.callNativeMethod.apply(me, ["api_closeWindow"])
        } else if (e && (scope.source == "remoteClinic" || scope.source == "pay" && (status == 2 || status == 6))) {
            $emitter.emit("indexGoIndex")
        } else {
            e && me.emit("updateList", scope.rType);
            if (scope.source == "appointListrecord") {
                document.title = record != 2 ? "我的挂号单" : "我的预约单";
                $env.isIOS && $sys.iosTitleReset()
            }
        }
    },
    getJumpStatus: function() {
        var me = this
          , scope = me.el.scope();
        scope.clickAlreadyPay = !0;
        scope.$apply()
    }
});
