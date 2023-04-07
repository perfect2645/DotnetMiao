
/**
 * 定义变量
 */
//var dateokey = NewGuid();
var hosSpecialName = "";
var hospitalId = getUrlParam('hospitalId');
var deptId = getUrlParam('deptId');
if(hospitalId==''){
	hospitalId = getUrlParam('sourceId');
}
if(deptId==""){
	deptId = getUrlParam('depId');
}
var isCooperate = "1";
var IsLoadDoctorList = false;
var IsLoadDoctorInfo = false;
var DoctorInfoList = [];
var totH = $(window).height();
var remarkHight=0;
var showChildDeptInSecondArr=['1424','1255'];
var isSHowChildDeptInSecond=false;
if(deptId==""||deptId=="0"){//兼容原逻辑
	location.href = '../hospital/ghDeptList.html?platformType=' + platformType + "&hospitalId=" + hospitalId+'&exConsult='+ getUrlParam('exConsult')+'&consultHosId='+getUrlParam('consultHosId');
}
document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
$(function(){
	if ($.inArray(hospitalId, showChildDeptInSecondArr) > -1) {
		isSHowChildDeptInSecond=true;
	}
	getDeptRemark();
	getIsCooperate(hospitalId);
    //下拉加载更多
    ////绑定账号提示-移到医生主页
    //BindAccountTip();
    //获取是否多院区医院
    MutiHospitalService.getBranchHospital();
    if (isCooperate) {
        if (showPageTop) {
            $(".doctor-hold1").height(totH - 98-remarkHight);
            $(".doctor-hold2").height(totH - 143-remarkHight);
        } else {
            $(".doctor-hold1").height(totH - 55-remarkHight);
            $(".doctor-hold2").height(totH - 100-remarkHight);
        }

        //医生方式
        ByDoctorService.iscrollObj =
            iscrollAssist.newVerScrollForPull($('#pull-iscroll-wrap1'), null, ByDoctorService.GetMore);
        ByDoctorService.iscrollObj.refresh();
        //日期方式
        ByDateService.iscrollObj =
            iscrollAssist.newVerScrollForPull($('#pull-iscroll-wrap2'), null, ByDateService.GetMore);
        ByDateService.iscrollObj.refresh();
        LvTongService.showLvTong();
    } else {
        if (showPageTop) {
            $(".doctor-hold1").height(totH - 44-remarkHight);
        } else {
            $(".doctor-hold1").height(totH-remarkHight);
        }
    }

    //全部
    ByAllService.iscrollObj = iscrollAssist.newVerScrollForPull($('#pull-iscroll-wrap3'), null, ByAllService.GetMore);
    ByAllService.iscrollObj.refresh();
    //选择列表方式
    $(".doc-suggst-lab li").click(function () {
        var index = $(this).index();
        $(".doc-suggst-lab li").find("a").removeClass("curr");
        $(this).find("a").addClass("curr");
        $(".labcon").hide().eq(index).show();
        if (index === 0 && ByDoctorService.pageindex <= 0) {
            ByDoctorService.GetDoctorsList();
        } else if (index === 1) {
            ByDateService.pageindex = 0;
            ByDateService.GetGhtChnnelRule();
            //ByDateService.GetInitLoadData();
        } else if (ByAllService.pageindex <= 0) {
            ByAllService.GetDoctorsList();
        }
        //ByDoctorService.iscrollObj.refresh();
        //ByDateService.iscrollObj.refresh();
        ByAllService.iscrollObj.refresh();

        //记录状态
        localStorage.setItem("YYGHSelectDoctor_DocOrDate_History", index);

        //埋点
        switch (index) {
            case 0:
            	saveSiteTrack("ghbtn1018", { "hosid": hospitalId, "deptid": deptId });
                break;
            case 1:
            	saveSiteTrack("ghbtn1019", { "hosid": hospitalId, "deptid": deptId });
                break;
            case 2:
            	saveSiteTrack("ghbtn1020", { "hosid": hospitalId, "deptid": deptId });
                break;
        }
    });
    if (isCooperate) {
    	if(isSHowChildDeptInSecond){
            $(".doc-suggst-lab li").eq(1).click();
    	}else{
            $(".doc-suggst-lab li").eq(0).click();
    	}
    } else {
        ByAllService.GetDoctorsList();
        ByAllService.iscrollObj.refresh();
        $(".doc-suggst-lab li").eq(2).click();
        $(".doc-suggst-lab").hide();
    }
    //选择日期控件
    var timeliClick = false;
    $("#time-choo-hold ul").on("click", "li", function (e) {
        if (!timeliClick) {
            timeliClick = true;
            $("#time-choo-hold ul li").removeClass("curr");
            $(this).addClass("curr");
            //重置变量
            ByDateService.pageindex = 0;
            ByDateService.total = 0;
            ByDateService.choosedate = $(this).data("date");
            ByDateService.showCountDown = $(this).data("showcountdown");
            ByDateService.openTime = $(this).data("opentime");
            ByDateService.GetDoctorsList();
            setTimeout(function () {
                timeliClick = false;
            }, 1000);
        }
    });
    // showGuideChannel();

    jkgjdbService.init(hospitalId);

});

function getIsCooperate(hospitalId){
	$.ajax({
    	    url: "../do/doctor/isCooperate",
            timeout: 8000,
            type: "POST",
	        dataType: "json",
	        async: false,
	        cache: true,
            data: {
            t: new Date().getTime(),
            hospitalId: hospitalId
        },
        beforeSend: function () {
        },
        success: function (json) {
        	if(json.Code==10000){
        		isCooperate = "1";
        	}else{
        		isCooperate = "";
        	}
        }, error: function (xmlHttpRequest, textStatus) {
        	myLayer.clear();
			myLayer.alert("网络繁忙，请稍后再试...");
        }
    });

}
/**
 * 按医生
 */
var ByDoctorService = {
    pageindex: 0,
    pagesize: 15,
    total: 0,
    iscrollObj: null,
    type: 0,
    GetDoctorsList: function () {
        var that = this;
        $.ajax({
        	url: "../do/doctor/getDocListByDeptId",
            timeout: 8000,
            type: "POST",
  	        dataType: "json",
  	        async: true,
  	        cache: true,
            data: {
                t: new Date().getTime(),
                hosId: hospitalId,
                deptId: deptId,
                pageIndex: that.pageindex+1,
                pageSize: that.pagesize,
                multiHosDeptId: isSHowChildDeptInSecond?deptId:''
            },
            beforeSend: function () {
                if (that.pageindex <= 0) {
                    localLoad("doctor-box1");
                    $(".pullUp").hide();
                }
            },
            success: function (json) {
                FormatDate(json, that);
            }, error: function (xmlHttpRequest, textStatus) {
                if (textStatus === "timeout") {
                	localReload({ pack: 'doctor-box1', con: '医生列表加载失败', btn: function () { that.GetDoctorsList(); }, btnValue: '重新加载' });
                }
                else { localReload({ pack: 'doctor-box1', con: '网络异常，请检查您的网络或刷新重试' }); }
            }
        });
    },
    GetMore: function () {
        var that = ByDoctorService;
        if (that.total >= (that.pagesize * (that.pageindex + 1))) {
            that.pageindex++;
            that.GetDoctorsList();
        } else {
            $(".pullUp").removeClass("loading");
            $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
        }
    }
};
/**
 * 按日期
 */
var ByDateService = {
    pageindex: 0,
    pagesize: 100,
    total: 0,
    type: 1,
    iscrollObj: null,
    choosedate: "",
    GetDoctorsList: function () {
        var that = this;
        $.ajax({
        	 url: "../do/doctor/getGhDoctorListWater",
   	         timeout: 8000,
   	         type: "POST",
   	         dataType: "json",
   	         async: true,
   	         cache: true,
             data: {
                t: new Date().getTime(),
                hosID: hospitalId,
                deptId: deptId,
                reDate: that.choosedate,
                pageIndex: that.pageindex,
                pageSize: that.pagesize,
                multiHosDeptId: isSHowChildDeptInSecond?deptId:'',
                 channelId:channelId
            },
            beforeSend: function () {
                if (that.pageindex <= 0) {
                    localLoad("doctor-box2");
                    $(".pullUp").hide();
                }
            },
            success: function (json) {
                FormatDate(json, that);
            }, error: function (xmlHttpRequest, textStatus) {
                if (textStatus === "timeout") {
                	localReload({ pack: 'doctor-box2', con: '医生列表加载失败', btn: function () { that.GetDoctorsList(); }, btnValue: '重新加载' });
                }
                else { localReload({ pack: 'doctor-box2', con: '网络异常，请检查您的网络或刷新重试' }); }
            }
        });
    },
    GetMore: function () {
        var that = ByDateService;
        if (that.total >= (that.pagesize * (that.pageindex + 1))) {
            that.pageindex++;
            that.GetDoctorsList();
        } else {
            $(".pullUp").removeClass("loading");
            $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
        }
    },
    //查询放号规则
    GetGhtChnnelRule: function () {
        var that = this;
        $.ajax({
        	url: "../do/doctor/getGhtChnnelMaxRule",
  	        timeout: 8000,
  	        type: "POST",
  	        dataType: "json",
  	        async: true,
  	        cache: true,
            data: {
                t: new Date().getTime(),
                hosId: hospitalId,
                deptId: deptId,
                channelId:channelId
            },
            beforeSend: function () {
                if (that.pageindex <= 0) {
                    localLoad("doctor-box2");
                    $(".pullUp").hide();
                }
            },
            success: function (json) {
                if (parseInt(json.Code) === 10000) {
                    var vhtml = '';
                    $.each(json.Result, function (i, item) {
                        vhtml += '<li class="hasset" data-opentime="' + item.openTime + '" data-showcountdown="' + item.showCountDown + '" data-date="' + item.date + '"><p class="c-f13 c-333">' + item.week + '</p><p>' + item.showdate + '</p></li>';
                    });
                    $("#time-choo-hold ul").html(vhtml);
                    that.GetInitLoadData();
                }
            }, error: function (xmlHttpRequest, textStatus) { }
        });
    },
    //日期加载后显示第二天排班
    GetInitLoadData: function () {
        var that = this;
        timeList();
        //给reDate赋值,选中明天的
        var defaultTimeHoldObj = $("#time-choo-hold li").eq(0);
        $(defaultTimeHoldObj).addClass("curr");
        that.choosedate = $(defaultTimeHoldObj).data("date");
        that.GetDoctorsList();
    }
};

/**
 * 全部医生
 */
var ByAllService = {
    pageindex: 0,
    pagesize: 15,
    total: 0,
    type: 2,
    iscrollObj: null,
    GetDoctorsList: function () {
        var that = this;
        $.ajax({
        	    url: "../do/doctor/getDeptDoctorList",
      	        timeout: 8000,
      	        type: "POST",
      	        dataType: "json",
      	        async: false,
      	        cache: true,
            data: {
                t: new Date().getTime(),
                hosId: hospitalId,
                deptId: deptId,
                pageIndex: that.pageindex + 1,
                pageSize: that.pagesize,
                multiHosDeptId: isSHowChildDeptInSecond?deptId:'',
                channelId:channelId
            },
            beforeSend: function () {
                if (that.pageindex <= 0) {
                    localLoad("doctor-box3");
                    $(".pullUp").hide();
                }
            },
            success: function (json) {
                FormatDate(json, that);
            }, error: function (xmlHttpRequest, textStatus) {
                if (textStatus === "timeout") {
                	localReload({ pack: 'doctor-box3', con: '医生列表加载失败', btn: function () { that.GetDoctorsList(); }, btnValue: '重新加载' });
                }
                else { localReload({ pack: 'doctor-box3', con: '网络异常，请检查您的网络或刷新重试' }); }
            }
        });
    },
    GetMore: function () {
        var that = ByAllService;
        if (that.total >= (that.pagesize * (that.pageindex + 1))) {
            that.pageindex++;
            that.GetDoctorsList();
        } else {
            $(".pullUp").removeClass("loading");
            $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
        }
    }
};

//渲染页面
function FormatDate(jsonData, tservice) {
    var fcontainer = "doctor-box" + (tservice.type + 1);
    if (jsonData) {
        if (parseInt(jsonData.Code) === 10000) {
            var ret = jsonData.Result;
            if(isNotNull(ret)&&ret.length>0&&hospitalId=="1096998"&&deptId=="7221597"){ //武汉云之翎个性化SJ202105100105
                ret = ret.filter(function(item) {
                    return tservice.type==1?item.doctorid!="710671404":item.doctorUid != "710671404";
                });
            }
            if (ret == null || ret.length <= 0) {
                if (tservice.pageindex <= 0) {
                    tservice.iscrollObj.scrollTo(0, 0);
                    if (tservice.type === 0) {
                        comNomess({ pack: fcontainer, con: "尚未有医生开通预约挂号" });
                    } else if (tservice.type === 1) {
                    	if(isNotNull(ByDateService.showCountDown)&&ByDateService.showCountDown==true&&isNotNull(ByDateService.openTime)&&judgeTimeDiffer(ByDateService.openTime)<60){
                    		if(judgeTimeDiffer(ByDateService.openTime)>0){
                    			$('.doctor-time').html("正在同步排班中，请等待");
                    			setTimeout(function () {
                         	    	ByDateService.GetDoctorsList();
                    	         }, 3000);
                    		}else{
                    			$('#openTime').html(formatOpenTime(ByDateService.openTime));
                        		//倒计时
                        	    $('.doctor-time').downCount({
                        	        date: ByDateService.openTime.replace(/-/g,"/"),
                        	        offset: +8
                        	    }, function() {
                             	    ByDateService.GetDoctorsList();
                        	    });
                    		}

                    		$('.doctor-hold2').hide();
                    		$('.doctor-time').show();
                    	}else{
                    		$('.doctor-hold2').show();
                    		$('.doctor-time').hide();
                            comNomess({ pack: fcontainer, con: "当天暂无排班医生" });
                    	}
                    } else {
                        comNomess({ pack: fcontainer, con: "该科室暂无医生" });
                    }
                } else {
                    $(".pullUp").removeClass("loading");
                    $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
                }
                return;
            }
            var vhtml = "";
            var count = ret.length;
            var wxfirstpicurl = "";

            if (tservice.type === 0) {
                IsLoadDoctorList = true;
                //这里获取一次
//                MutiHospitalService.getBranchHospital();
//                var fddxEmIdArr = [];
//                var isfddx = MutiHospitalService.isFddxHos;
                wxfirstpicurl = ret[0].photoUri;
                for (var j = 0; j < count; j++) {
                    var empid = typeof ret[j].doctorNumberList != "undefined" ? ret[j].doctorNumberList.split(',')[0] : "";
                    vhtml += '<li onclick="GotoGh(\'' + ret[j].doctorUid + '\',2,' + tservice.type + ')" class="auto-li-clduid-' + ret[j].doctorUid + '">';
                    vhtml += '<div class="doc-avatar">';
                    vhtml += '<img  onerror="javascript:this.src=\'//f1.yihuimg.com/concat/mobile/cross/images/d-male.png\';" src="' + (ret[j].photoUri ? getThumbnailUrl(ret[j].photoUri, 0, 75) : '//f1.yihuimg.com/concat/mobile/cross/images/d-male.png') + '" alt=""></div>';
                    vhtml += '<div class="doc-info pr10">';
                    vhtml += '<h4><span class="c-333 c-f16 mr10">' + ret[j].doctorName + '</span>' + ret[j].lczcName;
                    if (ret[j].numCount != null && typeof ret[j].numCount != "undefined" && parseInt(ret[j].numCount) > 0) {
                            vhtml += '<i class="rel-bq bgc-1ec24c">可挂号</i>';
                        }
//                    else {
//                            if (empid != "" && $.inArray(empid, fddxEmIdArr) < 0) {
//                                fddxEmIdArr.push(empid);
//                            }
//                        }
                    if((channelId=='1000007' || channelId=='1000025' ||channelId=='1000006' ||channelId=='9000723'||channelId=='9000920'||channelId=='9000705' ||channelId=='9000241')&&(ret[j].doctorService_phone=='1' || ret[j].doctorService_text =='1')){
                    	vhtml+='<i class="rel-bq bgc-ff8100">可咨询</i>';
                    }
//                    if(getSimpleDoctorByUid(ret[j].doctorUid)){
//                    	vhtml+='<i class="rel-bq bgc-ff8100">可咨询</i>';
//                    }
                    vhtml += '</h4>';
                    var docSkill = (ret[j].skill == '' ? '暂无' : ret[j].skill);
                    if (IsLoadDoctorInfo && DoctorInfoList.length > 0) {
                        for (var lk = 0, lklen = DoctorInfoList.length; lk < lklen; lk++) {
                            var lkItem = DoctorInfoList[lk];
                            if (lkItem.doctorUid == ret[j].doctorUid) {
                                docSkill = (lkItem.skill == '' ? '暂无' : lkItem.skill);
                                break;
                            }
                        }
                    }
                    vhtml += '<div class="c-909090 c-nowrap-multi">擅长：' + docSkill + '</div>';
                    vhtml += '</div>';
                    vhtml += '<div id="docnumcountstate-' + ret[j].doctorSn + '" class="state-hold">';
                    /*if (ret[j].numCount != null &&
                        typeof ret[j].numCount != "undefined" &&
                        parseInt(ret[j].numCount) > 0) {
                        vhtml += '<div><span>可预约</span></div>';
                    } else {
                        if (empid != "" && $.inArray(empid, fddxEmIdArr) < 0) {
                            fddxEmIdArr.push(empid);
                        }
                    }*/
                    vhtml += '</div>';
                    vhtml += '</li>';
                }
//                MutiHospitalService.empDoctor.docemidarr = fddxEmIdArr;
//                if (isfddx && fddxEmIdArr.length > 0) {
//                    MutiHospitalService.GetDoctorGhNum();
//                }

            } else if (tservice.type === 2) {
                IsLoadDoctorList = true;
                wxfirstpicurl = ret[0].photoUri;
                for (var r = 0; r < count; r++) {
                    vhtml += '<li onclick="GotoGh(\'' + ret[r].doctorUid + '\',1,' + tservice.type + ')" class="auto-li-clduid-' + ret[r].doctorUid + '">';
                    vhtml += '<div class="doc-avatar">';
                    vhtml += '<img  onerror="javascript:this.src=\'//f1.yihuimg.com/concat/mobile/cross/images/d-male.png\';" src="' + (ret[r].photoUri ? getThumbnailUrl(ret[r].photoUri, 0, 75) : '//f1.yihuimg.com/concat/mobile/cross/images/d-male.png') + '" alt=""></div>';
                    vhtml += '<div class="doc-info pr10">';
                    vhtml += '<h4><span class="c-333 c-f16 mr10">' + ret[r].doctorName + '</span>' + ret[r].lczcName;
                    if((channelId=='1000007' || channelId=='1000025' ||channelId=='1000006' ||channelId=='9000723'||channelId=='9000920'||channelId=='9000705' ||channelId=='9000241')&&(ret[r].doctorService_phone=='1' || ret[r].doctorService_text =='1')){
                        vhtml+='<i class="rel-bq bgc-ff8100">可咨询</i>';
                    }
                    vhtml += '</h4>';
                    var docSkill1 = (ret[r].skill == '' ? '暂无' : ret[r].skill);
                    if (IsLoadDoctorInfo && DoctorInfoList.length > 0) {
                        for (var lk1 = 0, lklen1 = DoctorInfoList.length; lk1 < lklen1; lk1++) {
                            var lkItem1 = DoctorInfoList[lk1];
                            if (lkItem1.doctorUid == ret[r].doctorUid) {
                                docSkill1 = (lkItem1.skill == '' ? '暂无' : lkItem1.skill);
                                break;
                            }
                        }
                    }
                    vhtml += '<div class="c-909090 c-nowrap-multi">擅长：' + docSkill1 + '</div>';
                    vhtml += '</div>';
                    vhtml += '</li>';
                }
            } else {
                wxfirstpicurl = ret[0].doctorpic;
                for (var i = 0; i < count; i++) {
                    var arrageStatus = parseInt(ret[i].ArrangeStatus);
                    var apptype = parseInt(ret[i].apptype);
                    var availablenum = parseInt(ret[i].availablenum);
                    var btnCol = "", btnClick = "", btnText = "";

                    if (arrageStatus === 1) { //正常
                        //号源
                        if (availablenum <= 0) {
                            if (parseInt(ret[i].houBuClosed) === 1 || ret[i].registerdate == CurentDate() || platformType == "9000278" || parseInt(platformType) >= 9000381) {
                                btnCol = " state-full";
                                btnClick = '';
                                btnText = '已约满';
                            }
                            else {
                                btnCol = " state-full";
                                btnClick = btnClick = 'onclick="GotoGh(\'' + ret[i].doctorid + '\',2,' + tservice.type + ')"'; //onclick ="DoctorGH(' + obj1 + ',' + parameObj + ',\'HB\')
                                btnText = '候补';
                            }
                        } else {
                            btnCol = "";
                            btnClick = 'onclick="GotoGh(\'' + ret[i].doctorid + '\',2,' + tservice.type + ')"';  //onclick ="DoctorGH(' + obj1 + ',' + parameObj + ')"
                            if (String(ret[i].UnOpened) == 'true') {
                                btnText = ret[i].FHTimes + '放号';
                            }
                            else if (parseInt(ret[i].numCountVisible) === 1) {
                                btnText = '预约';
                            } else {
                                btnText = '剩' + availablenum + '个';
                            }
                        }
                    } else if (arrageStatus === 4) { //暂停预约
                        btnCol = "state-none";
                        btnText = '暂停预约';
                    } else { //停诊
                        btnCol = "state-none";
                        btnText = '停诊';
                    }
                    var feeRemark = parseInt(ret[i].ghfeeway) === 0 ? "(到院付)" : "";
                    vhtml += '      <li class="spec-nhod" ' + btnClick + ' data-docsn="' + ret[i].doctorsn + '"  data-docname="' + ret[i].doctorname + '" data-hosid="' + ret[i].hosid + '" data-hosname="' + ret[i].hosname + '" data-deptid="' + ret[i].deptid + '" data-deptname="' + ret[i].deptname + '">';
                    vhtml += '          <div class="spec-nbox">';
                    vhtml += '           <div class="doc-avatar"><img onerror="javascript:this.src=\'//f1.yihuimg.com/concat/mobile/cross/images/d-male.png\';" src="' + (ret[i].doctorpic ? getThumbnailUrl(ret[i].doctorpic, 0, 75) : '//f1.yihuimg.com/concat/mobile/cross/images/d-male.png') + '" alt=""></div>';
                    vhtml += '            <div class="doc-info pr10">';
                    vhtml += '                <h4>';
                    vhtml += '                <span class="c-333 c-f16">' + ret[i].doctorname + '</span>';
                    vhtml += '                <span class="c-909090 ml10">'+ret[i].levelname+'</span>';
                    vhtml += '                </h4>';
                    if(isSHowChildDeptInSecond){
                        vhtml += '<span class="c-909090">'+ret[i].deptname+'</span>';
                    }
                    vhtml += '                <p>' + ret[i].TypeName + '<span class="timebq tbq' + ret[i].timeid + '"></span>';
                    if(isNotNull(ret[i].specialprice)&&ret[i].specialprice>0){
                        vhtml += '                    <i class="c-ff8100 ml10 c-f12">' + (ret[i].specialprice / 100) + '元</i>' + feeRemark ;
                    }
                    vhtml += '                </p>';
                    vhtml += '            </div>';
                    vhtml += '            <div class="tsel-state">';
                    vhtml += '                <a class=\"' + btnCol + '\">' + btnText + '</a>';
                    vhtml += '            </div>';
                    vhtml += '        </div>';
                    if (ret[i].special != null) {//ret[i].skill
                        vhtml += '        <div class="spec-nbot">';
                        vhtml += '         <p class="c-nowrap-multi">擅长：' + (ret[i].special == '' ? '暂无' : ret[i].special) + '</p>';
                        vhtml += '        </div>';
                    }
                    vhtml += '    </li>';
                }
            }
            //console.info(vhtml);

            $(".pullUp").show();
            if (tservice.pageindex <= 0) {
                tservice.total = 0;
                $("#" + fcontainer).html(vhtml);
            } else {
                $("#" + fcontainer).append(vhtml);
            }
            if (tservice.type === 1) {
            	$('.doctor-hold2').show();
            	$('.doctor-time').hide();
            }
            tservice.total = tservice.total + count;
            if (tservice.total < (tservice.pagesize * (tservice.pageindex + 1))) {
                if (tservice.pageindex <= 0) {
                    $(".pullUp").hide();
                } else {
                    $(".pullUp").removeClass("loading");
                    $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
                }
            }

            tservice.iscrollObj.refresh();

            //微信分享第一张
            try {
                if (wxfirstpicurl != "") {
                    __WxShareParam.shareParam.imgUrl = wxfirstpicurl;
                }
            } catch (e) { }

        } else if (parseInt(jsonData.Code) === -111111) {
            comNomess({ pack: fcontainer, con: jsonData.Message });
        } else if (jsonData.Message == "查询失败,就诊日期大于渠道放号日期") {
            comNomess({ pack: fcontainer, con: tservice.type === 0 ? "尚未有医生开通预约挂号" : "当天暂无排班医生" });
        }
        else {
            if (tservice.pageindex > 0) {
                $(".pullUp").removeClass("loading");
                $(".pullUp").find("span.pullUpLabel").html("没有更多医生");
            } else {
                localReload({ pack: fcontainer, con: tservice.type === 0 ? "尚未有医生开通预约挂号" : "当天暂无排班医生" });
            }
        }
    }
    else {
        localReload({
            pack: fcontainer, con: '医生列表加载失败，请刷新重试', btn: function () {
                tservice.GetDoctorsList();
            }, btnValue: '重新加载'
        });
    }
}
//当前日期
function CurentDate() {
    var now = new Date();
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日
    if (month < 10) { month = "0" + month }
    if (day < 10) { day = "0" + day }
    return year + "-" + month + "-" + day;
}

/**
* 多院区医院
*/
var MutiHospitalService = {
    branchHosptal: {
        storagekey: 'FILTER_HOSPITAL_ARR_',
        isload: false,
        data: [],
        mtilhos: []
    },
    isFddxHos: false,
    empDoctor: {
        isload: false,
        docemidarr: []
    },
    getBranchHospital: function () {
        var that = this;
        if (that.branchHosptal.isload)
            return;

        that.branchHosptal.isload = true;

        if (localStorage) {
            var jstr = localStorage.getItem(that.branchHosptal.storagekey + hospitalId);
            if (jstr && typeof jstr != "undefined" && jstr.length > 5) {
                that.branchHosptal.data = JSON.parse(jstr);
            }
        }
        if (that.branchHosptal.data.length == 0) {
            $.ajax({
            	url: "../do/hospital/queryBranchHospitalList",
                timeout: 8000,
                dataType: "json",
                type: "POST",
                data: {
                    t: new Date().getTime(),
                    hosId: hospitalId
                },
                async: false,
                success: function (objJson) {
                    if (objJson && objJson.Code == 10000 && objJson.Result && objJson.Result.length > 0) {
                        that.branchHosptal.data = objJson.Result;
                    }
                }
            });
        }

        if (that.branchHosptal.data.length > 0) {
            for (var j = 0, l = that.branchHosptal.data.length; j < l; j++) {
                var hosid = that.branchHosptal.data[j].hospitalId;
                if (hosid == hospitalId) {
                    that.isFddxHos = true;
                }
                that.branchHosptal.mtilhos.push(hosid);
            }
        }

    },
    GetDoctorGhNum: function () {
        var that = this;
        if (that.empDoctor.isload)
            return;

        that.isLoadGenum = true;
        if (that.empDoctor.docemidarr.length > 0) {
            for (var i = 0; i < that.empDoctor.docemidarr.length; i++) {
                $.ajax({
                	url: "../do/doctor/getGhDoctorListByEmpId",
                    timeout: 15000,
                    dataType: "json",
                    type: "POST",
                    data: {
                        t: new Date().getTime(),
                        doctorEmpId: that.empDoctor.docemidarr[i],
                        hospitalIds: MutiHospitalService.branchHosptal.mtilhos.join(',')
                    },
                    beforeSend: function () { },
                    success: function (json) {
                        if (json) {
                            if (json.Code == 10000) {
                                var ret = json.Result;
                                if (ret == null || ret.length <= 0) {
                                    return;
                                }
                                for (var i = 0, count = ret.length; i < count; i++) {
                                    var doemid = ret[i].doctorNumberList;
                                    var num = ret[i].numCount;
                                    var skey = $(".auto-li-cldemid-" + doemid);
                                    if (num != null && parseInt(num) > 0) {
                                    	if($(skey).find('h4').find('i').length>0){
                                    	    $(skey).find('h4').find('i').before('<i class="rel-bq bgc-1ec24c">可挂号</i>');
                                    	}
                                    	else{
                                    		$(skey).find('h4').append('<i class="rel-bq bgc-1ec24c">可挂号</i>');
                                    	}
                                       return;
                                    }
                                    else {
                                        //$(skey).find(".state-hold").remove();
                                    }
                                }
                            }
                        }
                    }
                });
            }
        }

    }
};
///进入预约页面-医生主页
function GotoGh(doctorid, type) {
	var doctorIndexUrl="../doctor/doctorIndex.html?gotopage=1&deptId="  + deptId + "&doctorId="+doctorid+"&hospitalInternal=1"+"&showMultiDept=0"+ "&platformType="+platformType+extendParam;
	$.ajax({
		type : 'post',
		async : false,
		url : getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
		data : {
			hospitalId : hospitalId,
			type : 50,
			time : new Date().getTime()
		},
		beforeSend : function() {},
		error : function() {

		},
		success : function(d) {
			myLayer.clear();
			if (d.Code == 10000) {
				if (isNotNull(d.Result.value)&&d.Result.value==1) {
					doctorIndexUrl=StrCloudClinic+'/patient/html/doctorHome.html?doctorUid='+doctorid+'&channelId='+channelId+'&deptId='+deptId+'&showMultiDept=0';
				}
			}
		}
	});
	window.location.href = doctorIndexUrl;
}
var LvTongService = {
	showLvTong : function() {
		$.ajax({
			type : 'post',
	        async: true,
			url :  "../do/baseInfo/getYuYueChannelConfigs",
			data : {
				channelId: channelId,
				type : 'LvtongChannel',
			},
			success : function(d) {
				if (d.Code == 10000 && isNotNull(d.value)) {
					var html='<div class="bgc-fff c-border-tb jylt-reser mt10">';
					html+='<div class="jylt-reser-lt">';
					html+='<span class="lt-icon"><i class="iconfont icon-jiuyilvtong"></i></span>';
					html+='<div class="lt-tit">';
					html+='<p class="c-333 c-f14 mb5">专家门诊安排</p>';
					html+='<span class="c-999 c-f12">便捷就诊安排可申请</span>';
					html+='</div>';
					html+='</div>';
					html+='<a onclick="LvTongService.toLvTong(\''+d.value+'\');" class="jylt-reser-btn">立即申请</a>';
					html+='</div>';
					$('.pull-iscroll-box').prepend(html);
			    }
			}
		});

	},
	toLvTong:function(value){
		$(".doc-suggst-lab li").find("a").each(function(){
			if($(this).hasClass("curr")){
				var siteTag='';
			    var nowTitle=$(this).text();
			    if(nowTitle=='按医生预约'){
			    	siteTag='ghbtn1036';
			    }else if(nowTitle=='按日期预约'){
			    	siteTag='ghbtn1037';
			    }else if(nowTitle=='全部医生'){
			    	siteTag='ghbtn1038';
			    }
			    if(isNotNull(siteTag)){
		            saveSiteTrack(siteTag,{ "hosid":hospitalId, "deptid":deptId});
			    }
			}
		 });
		var ltUrl='';
		if(value==2){
			ltUrl=StrTradeUrl+'/trade/fabinfo/hospitalizeGreenV2.html?channelId='+channelId+'&busType=1&scene=1&hospitalId='+hospitalId+'&hosDeptId='+deptId;
		}else{
			ltUrl=StrTradeUrl+'/trade/fabinfo/hospitalizeGreen.html?referCode=yw_zgrs_1003&platformType='+platformType+'&sourceType=0&sourceId=0&HosId='+hospitalId+'&DeptId='+deptId+'&openid='+loginUserInfo.loginId;
		}
		location.href=ltUrl;
	}
};

function getDeptRemark(){
	$.ajax({
    	url: '../do/doctorService/getDoctorRemark',
        timeout: 8000,
        dataType: "json",
        type: 'post',
        async: false,
  	    cache: true,
        data: {
        	typeId: 2,
            resId: deptId
        },
        beforeSend: function () { },
        success: function (objJson) {
        	var remark;
            if (objJson) {
                if (parseInt(objJson.Code) === 10000) {
                    remark = objJson.remark;
                    if (typeof remark != "undefined" && $.trim(remark).length > 0) {
                    	remarkHight=35;
                    	//内容上新
                        $("#deptRemarkBox").html(formatHtml(remark,true));
                        $("#deptRemarkBoxDd").html(formatHtml(remark,false));
                        ShowHosArticle(deptId,objJson.opertime,objJson.keepPop);
                        if (isNotNull(objJson.title)) {
                            $("#deptRemarkTitle").text(objJson.title);
                        }
                        $(".hos-notice").show();
                        $('.hos-notice-con div').marquee();
                    }
                }
            }

        },
        error: function () { $("#hos-notice").hide(); }
    });
}

//显示公告
function ShowHosArticle(deptid,artTime,keepPop) {
  //读取storage中的记录
	var storageName = 'HosDoctorList_Record_Remark';
  var ishasshow = false;
  var showArticleList = localStorage.getItem(storageName);
  if (showArticleList != undefined && $.trim(showArticleList).length > 3) {
      showArticleList = eval('(' + unescape(showArticleList) + ')');
      //console.log(showArticleList);
      $.each(showArticleList, function (i, item) {
          if (item.d == deptid && item.t == artTime) {
              ishasshow = true;
              return false;
          }
          return true;
      });
  } else {
      showArticleList = [];
  }

  if (!ishasshow && $.trim(artTime) != "" && $.trim(deptid) != "") {
      var hisitme = { "d": deptid, "t": artTime };
      showArticleList.push(hisitme);
      //保存
      var infojson = escape(JSON.stringify(showArticleList));
      try {
      	localStorage.setItem(storageName, infojson);
      }
      catch (e) {
      	localStorage.removeItem(storageName);
          localStorage.setItem(storageName, infojson);
      }
  }
  if(isNotNull(keepPop)&&keepPop==1){
	  ishasshow = false;
  }

  if (!ishasshow) {
	  noticePop();
  }
}

//公告
function noticePop(){
	$('.agreemb').show();
	setTimeout(function(){
		$('.agreepop').show();
		$(".agree-box").height($(".agreepop").height());
	    $('.agree-box').on('touchmove', function (e) { e.stopPropagation(); });
	},100);
}
//过滤
function formatHtml(description, hp) {
  if (hp) {
      description = description.replace(/<p>/g, "&&p&&");
      description = description.replace(/<\/p>/g, "&&yp&&");
      description = description.replace(/<font color=red>/g, "&&font&color=red&&");//红色字体
      description = description.replace(/<\/font>/g, "&&yfont&&");
      description = description.replace(/<strong>/g, "&&strong&&");  //加粗字体
      description = description.replace(/<\/strong>/g, "&&ystrong&&");
  }
  description = description.replace(/(\n)/g, "");
  description = description.replace(/(\t)/g, "");
  description = description.replace(/(\r)/g, "");
  description = description.replace(/<\/?[^>]*>/g, "");
  description = description.replace(/\s*/g, "");
  if (hp) {
      description = description.replace(/&&p&&/g, "<p>");
      description = description.replace(/&&yp&&/g, "<\/p>");
      description = description.replace(/&&font&color=red&&/g,"<span style=color:red>");
      description = description.replace(/&&yfont&&/g, "<\/span>");
      description = description.replace(/&&strong&&/g,"<span style=font-weight:bold>");
      description = description.replace(/&&ystrong&&/g, "<\/span>");
  }
  return description;
}


function judgeTimeDiffer(endTime) {
    var startTime =new Date();
    var endTime = new Date(endTime.replace(/-/g,"/"));
    return parseInt((startTime.getTime() - endTime.getTime()) / 1000);
}

function formatOpenTime(openTimeStr) {
    var openTime = new Date(openTimeStr.replace(/-/g,"/"));
    var month = openTime.getMonth() + 1;
	var date = openTime.getDate();
	var hours=openTime.getHours();
	var minutes=openTime.getMinutes();
	var times=openTimeStr.substr(11,5);

    return month+"月"+date+"日"+times;
}

//倒计时
(function($) {
    $.fn.downCount = function(options, callback) {
        var settings = $.extend({
            date: null,
            offset: null
        }, options);
        if (!settings.date) {
            $.error('Date is not defined.');
        }
        if (!Date.parse(settings.date)) {
            $.error('Incorrect date format, it should look like this, 12/24/2012 12:00:00.');
        }
        var container = this;
        var currentDate = function() {
            var date = new Date();
            var utc = date.getTime() + (date.getTimezoneOffset() * 60000);
            var new_date = new Date(utc + (3600000 * settings.offset));
            return new_date;
        };

        function countdown() {
            var target_date = new Date(settings.date),
                current_date = currentDate();
            var difference = target_date - current_date;
            if (difference < 0) {
                clearInterval(interval);
                if (callback && typeof callback === 'function') callback();
                return;
            }
            var _second = 1000,
                _minute = _second * 60,
                _hour = _minute * 60;
            var hours = Math.floor(difference / _hour),
                minutes = Math.floor((difference % _hour) / _minute),
                seconds = Math.floor((difference % _minute) / _second);
            hours = (String(hours).length >= 2) ? hours : '0' + hours;
            minutes = (String(minutes).length >= 2) ? minutes : '0' + minutes;
            seconds = (String(seconds).length >= 2) ? seconds : '0' + seconds;
            container.find('.dt-hours').text(hours);
            container.find('.dt-minutes').text(minutes);
            container.find('.dt-seconds').text(seconds);
        };
        var interval = setInterval(countdown, 1000);
    };
})(jQuery);
