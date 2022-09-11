var doctorSn = getUrlParam('doctorSn');
var hospitalId = getUrlParam('hospitalId');
var deptId = getUrlParam('deptId');
var arrangeId = getUrlParam('arrangeId');
$('#reqMemSn').val(getUrlParam('reqMemSn'));
var doctorInfo = null;
var arrangeInfo = null;
var Ismust = new Array();    //表单必填字段
var FormCon = new Array();
var FormConKv = new Array();  //预约表单字段 [{"keyValue":"0","keyName":"isChildren"},{"keyValue":"-1","keyName":"communityhos"}]
var isloadhy = 0; //是否已加载号源
var isloadcy = 0; //是否已加载成员
var MemList; //成员列表
var HasPacsServer = false;   //
var memChoo, numChoo;
var DoctorRegOrder = {};
var selMemTimes = 0;
var checkcode = false;
var doctorOfficeName=getUrlParam('doctorOfficeName');
doctorOfficeName=isNotNull(doctorOfficeName)?decodeURIComponent(doctorOfficeName):'';
var retId = getUrlParam('retid');
var isShowDbjzk = false;
var delayRegister=false;
$(function () {
	getDbjzkServiceConfig();
	checkLoginUser();
	$.when(getDoctorInfo,getDeptInfo,getArrangeInfo).then(function(){
		checkNcov();
//		var canreg = getUrlParam('canreg');
//		if(isNotNull(canreg)&&canreg==0){
//			if(hospitalId==2134||hospitalId==1026278||(isDebug&&hospitalId==1024727)){
//				ConfDialog('提示', '基于您的身体状况，我们暂时无法为您提供线上预约服务，您可以携带本人有效证件现场预约。', '确定');
//	            $("#surebtn").click(function () {
//	            	if(isXiongzhanghao()){
//		                location.href=StrYyghUrl+'/appoint/doctor/xzhDoctorIndex.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
//	            	}else{
//		                location.href=StrYyghUrl+'/appoint/doctor/doctorArrange.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
//	            	}
//	            });
//			}
//		}

		 LoadPage();             //加载页面基本内容
		 GetInputFormControls(); //查询挂号参数表单并载入成员
		 GetLockOrderInfo();  //锁号信息处理
		 GetNumberWaterList(1);   //获取号源 1.第一次加载数据，没有提示，异步默默加载
	     if (arrangeInfo.ArrRemark != "") {//显示备注
	        $('#divRemarkBlock').show();
	        $('#divRemark').html(arrangeInfo.ArrRemark);
	     }
	     ShowPz(hospitalId, arrangeInfo.registerdate);//陪诊
	     getAccountManageConfig();

        //app分享小程序
        shareDoctorService.load(doctorInfo.doctorUid,doctorInfo.hosDeptId);
	});
    memChoo = new iScroll('memlist');
    numChoo = new iScroll('numDiv');
    if (hospitalId == '1284') { //默认展开/中山大学附属第一医院
        ShowMoreInfo($('.editinto'), true);
    }
    $('.editinto').on('click', function () {
        ShowMoreInfo(this);
    });
    //搜索疾病
    $('#diseaseChoo').on('click', function () {
        $(window).scrollTop(0);
        $('#ghDiv').hide();
        $('#diseaseShow').show();
        SearchDisease();
    });
    $('.c-ser-back').on('click', function () {
        $('#diseaseShow').hide();
        $('#ghDiv').show();
    });
    $('#inpDiseaseName').bind('input propertychange', function () {
        SearchDisease(true);
    });
    $('#orderBtn').show();
    $('#orderBtn').click(SubOrder); //点击'立即预约'或者'确认登记'按钮
    AuthConfig.InitAuth();//实名认证
    CheckDelayRegister();

});


function getDbjzkServiceConfig(){
	$.ajax({
		url: '../do/doctorInfo/getServiceConfig',
		timeout: 8000,
		dataType: "json",
		type: 'POST',
		async: false,
		data: {
			hospitalId: hospitalId,
			serviceCode: '2108',
			channelId: channelId
		},
		success: function (result) {
			if (result.Code == 10000) {
				if(result.Result && result.Result.state==1){ // state:1 开启
					if(hospitalId!=0){
						isShowDbjzk = true;
					}
				}
			}
		},
		error: function () { }
	});
}
function checkNcov(){
	var canreg = getUrlParam('canreg');
	if(isNotNull(canreg)&&canreg==0){
		$.ajax({
			type : 'post',
			url : getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
			data : {
				hospitalId : hospitalId,
				type : 48,
				time : new Date().getTime()
			},
			beforeSend : function() {},
			error : function() {
				myLayer.clear();
				myLayer.alert('网络繁忙,请刷新后重试!');
			},
			success : function(d) {
				myLayer.clear();
				if (d.Code == 10000) {
					if (d.Result.value == 2) {
						var hintContent='基于您的身体状况，我们暂时无法为您提供线上预约服务。';
						if(hospitalId==1565){
							hintContent='请您到我院发热门诊就诊';
						}else if(hospitalId==474||hospitalId==1024727){
                            hintContent='请与疾控中心联系，东湖区防疫电话0791-86770843';
                        }else if(hospitalId==31){
                            hintContent='请至10号楼发热门诊就诊';
                        }else if(hospitalId==1032369){
                            hintContent='请至发热门诊就诊';
                        }else if(hospitalId==700302){
                            hintContent='很抱歉，您不符合门诊预约条件，请您到发热门诊就诊。感谢您的理解与配合。';
                        }else if(hospitalId==2134||hospitalId==1026278){
                            hintContent='基于您的身体状况，我们暂时无法为您提供线上预约服务，您可以携带本人有效证件现场预约。';
                        }else if(hospitalId==21045){
                            hintContent='请到发热门诊排查';
                        }
						ConfDialog('提示', hintContent, '确定',99);
			            $("#surebtn").click(function () {
			            	if(isXiongzhanghao()){
				                location.href=StrYyghUrl+'/appoint/doctor/xzhDoctorIndex.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
			            	}else{
				                location.href=StrYyghUrl+'/appoint/doctor/doctorArrange.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
			            	}
			            });
					}
				}
			}
		});
	}

	var warningType = getUrlParam('warningType');
	if(isNotNull(warningType)&&warningType!=0){
		$.ajax({
			type : 'post',
			url : getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
			data : {
				hospitalId : hospitalId,
				type : 48,
				time : new Date().getTime()
			},
			beforeSend : function() {},
			error : function() {
				myLayer.clear();
				myLayer.alert('网络繁忙,请刷新后重试!');
			},
			success : function(d) {
				myLayer.clear();
				if (d.Code == 10000) {
					if (d.Result.value == 2) {
						if(warningType==1){
							ConfDialog('提示', '请联系社区到所在辖区医院发热门诊就诊。', '确定',99);
						}else if(warningType==2){
							ConfDialog('提示', '请联系社区到定点医院就诊。', '确定',99);
						}
			            $("#surebtn").click(function () {
			            	if(isXiongzhanghao()){
				                location.href=StrYyghUrl+'/appoint/doctor/xzhDoctorIndex.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
			            	}else{
				                location.href=StrYyghUrl+'/appoint/doctor/doctorArrange.html?deptId='+deptId+'&doctorId='+doctorInfo.doctorUid+'&platformType='+platformType;
			            	}
			            });
					}
				}
			}
		});
	}
}

//医生信息
var getDoctorInfo =
	 $.ajax({
	      url: "../do/baseInfo/getDoctorInfo",
	      timeout: 8000,
	      type: "POST",
	      dataType: "json",
	      async: true,
	      cache: true,
	      data: {
	    	  doctorSn: doctorSn
	      },
	      success: function (json) {
		      if (json && json.Code==10000 && json.Result.length>0) {
		           doctorInfo = json.Result[0];
	          }
	      }
	  });
//科室信息
var getDeptInfo =
	 $.ajax({
	      url: "../do/baseInfo/getDeptInfo",
	      timeout: 8000,
	      type: "POST",
	      dataType: "json",
	      async: true,
	      cache: true,
	      data: {
	    	  deptId: deptId
	      },
	      success: function (json) {
		      if (json && json.Code==10000 && json.Result.length>0) {
		          var deptInfo = json.Result[0];
		          if(deptInfo.hosDeptServiceList.split(',').indexOf('6') >=0){
		        	  HasPacsServer = true;
		          }
	          }
	      }
	  });
//排班信息
var getArrangeInfo =
	 $.ajax({
	      url: "../do/baseInfo/getArrangeInfo",
	      timeout: 8000,
	      type: "POST",
	      dataType: "json",
	      async: true,
	      cache: true,
	      data: {
	    	  doctorSn: doctorSn,
	    	  hospitalId: hospitalId,
	    	  arrangeId: arrangeId,
	    	  channelId: channelId
	      },
	      success: function (json) {
		      if (json && json.Code==10000 && json.Result.length>0) {
		    	  arrangeInfo = json.Result[0];
	          }
	      }
	  });
///加载页面基本内容
function LoadPage() {
    //显示医院/医生/号源信息
	var reDate = arrangeInfo.registerdate;
    $('#reDateInfo').text("（"+reDate + " 星期" + GetWeek(reDate) + " " + GetTimeName(arrangeInfo.timeid)+")");
    //$('#hosName').text(OrderInfo.GH_HosName);     //医院名称
    $('#depName').text("("+ doctorInfo.deptName +")");      //科室名称
    $('#docName').text(doctorInfo.doctorName);       //医生姓名
    $(".nradio").click(function () {
        $(this).siblings(".nradio").find(".stradio").removeClass("stcheck");
        $(this).find(".stradio").addClass("stcheck");
    });
    //显示选择就诊成员面板
    $('#selmember').click(function () {
        GetMemberList();
        var totH = $(window).height();
        if ($("#nowMember").val() > 0) {
            $('.pop_screen').show();
            $('#memberDiv').show();
            $("#memlist").css("max-height", totH / 2 - 38);
            memChoo.refresh();
        } else {
            if (MemLod) {
                location.href = $("#ToAddMember").attr("href");
            } //如果就诊人信息还未加载完成先不触发跳转
        }
    });
    //显示选择号源面板
    $('#selnum').click(function () {
        GetNumberWaterList();
        $('.pop_screen').show();
        $('#numDiv').show();
        numChoo.refresh();
    });
    //返回按钮
    $('#returnbtn').click(function () {
        var nmuDiv = $('#numDiv');
        var memberDiv = $('#memberDiv');
        if (nmuDiv.css('display') == 'block') {
            nmuDiv.hide();
        } //当前号原面板为显示,返回按钮为关闭挂号面板
        if (memberDiv.css('display') == 'block') {
            memberDiv.hide();
        } //当前号原面板为显示,返回按钮为关闭挂号面板
        else {
            history.go(-1);
            return false;
        }
    });
}
///查询挂号参数表单并载入成员
function GetInputFormControls() {
    $.ajax({
	      url: "../do/registerInfo/getInputFormControls",
	      timeout: 8000,
	      type: "POST",
	      dataType: "json",
	      async: true,
	      cache: true,
	      data: {
	    	  hospitalId: hospitalId,
	    	  deptId: deptId,
	    	  doctorSn: doctorSn,
	    	  arrangeId: arrangeId,
	    	  channelId: channelId
	      },
        beforeSend: function () {
           myLayer.load("正在加载");
        },
        success: function (json) {
        	myLayer.clear();
            if (json) {
                if (json.Code == 10000) {
                    BindInputForm(json.Result);
                    GetMemberList(1);        //获取成员列表
                }
                else {
                	myLayer.alert(json.Message);
                }
            }
            else {
            	myLayer.clear();
            	myLayer.alert('网络繁忙,请求失败EA' + stackUUID);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        	myLayer.clear();
        	myLayer.alert('网络繁忙,请刷新后重试EE' + stackUUID);
        }
    });
}
///绑定表单
function BindInputForm(lists) {
    var con;
    var listHtml = "";
    var clinicCardHtml = "";
    var maxlength = 28;
    //var notShowFiename = ",name,mobile,cardno,sex,familyaddress,birthday,"; //不显示控件的fiename
    var notShowFiename = ",name,cmb_disease,"; //不显示控件的fiename
    var plusFiename = "";//",area,communityhos,";     //特殊处理字段
    var placeholder = "";
    var hasCardType = lists.filter(function (d) { return d.fiename.toLowerCase() == 'cardtype' }).length > 0;
    $.each(lists, function (i, item) {
        var contorKey = item.fiename.toLowerCase();
        if (contorKey == "cardtype") hasCardType = true;
        if (contorKey == "checkcode") {checkcode = true; return;}
        FormCon.push([item.fiename, ""]);    //记录所有表单信息

        if (plusFiename.indexOf(',' + contorKey + ',') < 0) {
            if (item.ismust == 1) {
                Ismust.push([item.fiename, item.parabsname, item.fietype, item.fienamec]);
            } //必填信息记录
            if (notShowFiename.indexOf(',' + contorKey + ',') < 0) {
            con = "";
            var fienamec = item.fienamec;
            if (contorKey == "cardno") {
                if (!hasCardType) {
                    con += '<li><div class="pmn_tit c-333">证件类型：</div> <div class="pmn_con c-909090"><select id="cardtype" name="ghFormCon" MassTxt="证件类型必填" class="pminput" >';
                    con += '<option value="1">居民身份证</option><option value="3">中国护照</option><option value="7">台胞证</option><option value="8">国外护照</option><option value="9">港澳身份证</option><option value="10">婴幼儿出生证</option><option value="11">台湾身份证</option><option value="12">港澳台居住证</option>';
                    con += '</select><span class="seljt arrow-right"></span></div></li>';
                }
                con += ("<li><div class='pmn_tit c-333'>证件号码：</div>");
            }
            else {
                con += ("<li><div class='pmn_tit c-333'>" + fienamec + "：</div>");
            }
            switch (item.fietype) {
                case 'T':   //文本框
                default :
                    switch (contorKey) {
                        case "cliniccard":  //就诊卡
                            con = "";
                            if (item.ismust != 1) {
                                clinicCardHtml += '<li id="cardliSel"><div class="pmn_tit c-333"><i class="iconfont\
                                 icon-zhifu\
                                 mr5"></i>就诊卡</div>\
                                    <div class="pmn_con c-909090">\
                                    <div class="input-group-radio mr10">\
                                    <div>\
                                        <div class="input-group-pack checked">\
                                            <input type="radio" name="jzlx" id="jzlxcz"  checked="checked">\
                                            <span class="disc"></span>\
                                        </div>无就诊卡\
                                    </div>\
                                </div>\
                                 <div class="input-group-radio mr10">\
                                    <div>\
                                        <div class="input-group-pack">\
                                            <input type="radio" name="jzlx" id="jzlxfz">\
                                            <span class="disc"></span>\
                                        </div>有就诊卡\
                                    </div>\
                                </div></div>' + ShowNote(item) +'</li>';
                                clinicCardHtml += '<li id="cardli" style="display:none;"><div class="pmn_tit c-333"><i class="iconfont icon-qiahao mr5"></i>' + item.fienamec + '</div><div class="pmn_con c-909090"><input class="pminput" type="text" id="' + contorKey + '" name="ghFormCon" maxlength="' + maxlength + '" placeholder="' + placeholder + '" /></div>' + ShowNote(item) +'</li>';
                            } //非必填
                            else {
                                clinicCardHtml += '<li id="cardliMust" ><div class="pmn_tit c-333"><i class="iconfont icon-qiahao mr5"></i>' + item.fienamec + '</div><div class="pmn_con c-909090"><input class="pminput" type="text" id="' + contorKey + '" name="ghFormCon" maxlength="' + maxlength + '" /></div>' + ShowNote(item) +'</li>';
                            } //就诊卡必填
                            break;
                        default:
                            con += '<div class="pmn_con c-909090"><input class="pminput" type="text" id="' + contorKey + '" name="ghFormCon" maxlength="' + maxlength + '" MassTxt="' + item.parabsname + '" />' + ShowNote(item) +'</div>';
                            break;
                    }
                    break;
                case 'C':   //下拉列表框
                    if(contorKey=='selectarea'){
                        con += '<div class="pmn_con c-909090"><div class="pminput" id="' + contorKey + '" name="ghFormCon"' +
                            ' MassTxt="' + item.parabsname + '"></div>';
                        con += '<span class="seljt arrow-right"></span>' + ShowNote(item) +'</div>';
                    }else{
                        if (item.dict) {
                            con += '<div class="pmn_con c-909090"><select id="' + contorKey + '" name="ghFormCon" MassTxt="' + item.parabsname + '" class="pminput" >';
                            if (typeof(item.dict) != "string") {
                                $.each(item.dict, function (j, dictItem) {
                                    con += '<option value="' + dictItem.value + '">' + dictItem.name + '</option>';
                                });
                            }
                            con += '</select><span class="seljt arrow-right"></span>' + ShowNote(item) +'</div>';
                        }
                    }
                    break;
                case 'D':   //日期选择框
                    con += '<div class="pmn_con c-909090"><input class="pminput" type="text" data-time="date" id="' + contorKey + '" name="ghFormCon" maxlength="' + maxlength + '" MassTxt="' + item.parabsname + '" />' + ShowNote(item) +'</div>';
                    break;
                case 'P': //阅读须知
                    var msg = item.parabsname.replaceAll('"', '&quot;').replaceAll("'", "&apos;").replaceAll("\r", "").replaceAll("\n", "");
                    var js = "ConfDialog('" + fienamec + "', '<div class=c-t-left>" + msg + "</div>','确定,取消',0);"
                      + "$('#canclebtn').click(function () {"
                      + "$('#" + contorKey + "').val('1');"
                      + "$('#openDiv').empty();"
                      + "});"
                      + "$('#surebtn').click(function () {"
                      + "$('#openDiv').empty();"
                      + "});";
                    con = '<input type="hidden" id="readDialog" value="' + js + '">';
                    con += '<input type="hidden" name="ghFormCon" id="' + contorKey + '" >';
                    eval(js);
                    break;
            }
            listHtml += con + "</li>";
        }
      }
    });
    $('#liClinicCard').replaceWith(clinicCardHtml);
    $('#liMoreInfo').replaceWith(listHtml);

    $('input[data-time="date"]').mobiscroll({
        preset: 'date',
        theme: 'ios',
        lang:'zh',
        dateFormat: 'yy-mm-dd',
        maxDate: new Date(),
        onShow:function(){
            try {
                var nowDate = $(this).val();
                if (isNotNull(nowDate)) {
                    var nowDateArr = nowDate.split('-');
                    var nowYear = nowDateArr[0];
                    var nowMonth = parseInt(nowDateArr[1]) - 1;
                    var nowDay = nowDateArr[2];
                    $(this).mobiscroll('selectWheel', [nowYear, nowMonth, nowDay], 0, true);
                }
            } catch (e) {
            }
        }
    });
    //单选
    $(".input-group-radio").on("click", function () {
        $(this).siblings(".input-group-radio").find(".input-group-pack").removeClass("checked").find("input").removeAttr("checked");
        $(this).find(".input-group-pack").addClass("checked").find("input").attr("checked", "true");
        if ($('#jzlxfz').attr("checked") == "checked") {
            $('#cardli').show();
            $('#cardliSel').find('.card-stp').hide();
        }
        else {
            $('#cardli').hide();
            $('#cardliSel').find('.card-stp').show();
        }
    });
    //付费类型
    $("#regpaytype").on("change", function () {
        ShowPayTypeMsg($("#regpaytype").val());
    });

    //选择地区
    $('#selectarea').cmArea({full:true,street:true});

    if (typeof (FormPlusInit) == "function") {
        FormPlusInit();
    }
    if (HasPacsServer) {
        $('#cliniccard').bind('blur input propertychange', function () {
           GetPacsApplyInfo();
        });
    }

    //显示字段备注
    function ShowNote(item) {
        if (item.shownote == "Y") {
            var urlDb = "";
            var dbJzk = "";
            if(item.fiename=='ClinicCard'&&isShowDbjzk){
            	urlDb = StrTradeUrl+"/trade/fabinfo/assistHosPage.html?channelId="+channelId+"&hospitalId="+hospitalId+"&busType=6";
            	dbJzk = "&nbsp;&nbsp;<a href="+urlDb+" class='card-link'>在线代办就诊卡&gt;&gt;</a>";
            }
            var noteHtml = '<div class="card-stp"><a href="javascript:;" onclick="$(this).parent().hide()"><i class="iconfont icon-guanbi"></i></a>'
                + item.parabsname + dbJzk+'</div>';
            return noteHtml;
        }
        return "";
    }
}
var queryNumberParamCache = '';
var queryNumberTimes = 0;
///获取号源when 1:载入首次 2:切换成员 3:检查项 9:提交 null:点击号源
function GetNumberWaterList(when, appDept,applyNo) {
    if (isloadhy > 0 && when == null) {
        return;
    }
    if (HasPacsServer == 1 && (appDept == null || appDept == '')) {
        return;
    }
    if (hospitalId == '964') {//964 广东省人民
        if (when == 1) {
            return;
        }
        var paramData = DoctorRegOrder.memberName
            + GetInputCliniccard()
            + DoctorRegOrder.identitytype
            + DoctorRegOrder.memberZhengjianid
            + DoctorRegOrder.memberSn
            + DoctorRegOrder.memberSex;
        if (paramData == queryNumberParamCache) {
            return ShowNumWarn(when);
        }
        else {
            queryNumberParamCache = paramData;
            queryNumberTimes ++;
            BindNumberWaterList();
        }
    }
    else if (when == 2 || when == 9) {
        return;
    }
    var async = (when == 9 ? false : true);//同步/异步获取
    var isfirst = (when == 1 ? true : false);
    if (appDept == null) {
        appDept = '';
    }
    var ret = "";
    $.ajax({
        url: "../do/registerInfo/getNumbers",
        timeout: 25000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        data: {
        	arrangeId: arrangeId,
        	hospitalId: hospitalId,
        	appliedDepartment: appDept,
        	channelId:channelId,
            name: DoctorRegOrder.memberName,
            ClinicCard: GetInputCliniccard(),
            zjType: DoctorRegOrder.identitytype,
            CardNo: DoctorRegOrder.memberZhengjianid,
            memberSn: DoctorRegOrder.memberSn,
            sex: DoctorRegOrder.memberSex,
            applyNo:applyNo
        },
        beforeSend: function () {
            if (!isfirst) {
                myLayer.load('正在加载');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (!isfirst) {
                myLayer.clear();
                myLayer.alert('网络繁忙,请刷新后重试CE' + stackUUID);
                //SysLog('CE' + stackUUID, XMLHttpRequest, textStatus, errorThrown);
            }
        },
        success: function (json) {
            if (!isfirst) { myLayer.clear(); }
            if (json) {
                if (json.Code == 10000) {
                    BindNumberWaterList(json.Result);
                    isloadhy = 1;
                }
                else {
                	BindNumberWaterList([]);
                    if (!isfirst) {
                        ConfDialog('提示', json.Message, '确定');
                        $("#surebtn").click(function () {
                            $("#openDiv").empty();
                            $('.pop_screen').hide();
                        });
                        ret = "err";
                    }
                }
            }
            else {
                if (!isfirst) {
                    myLayer.alert("网络繁忙,请求失败CA" + stackUUID);
                }
            }
        }
    });
    return ShowNumWarn(when);
}
//号源更新提示
function ShowNumWarn(when, ret) {
    if (when == 9) {
        if (ret == "err") {
            return "checkNum";
        }
        if (queryNumberTimes > 1) {
            queryNumberTimes = 1;
            ConfDialog('提示', '号源信息已重新获取，请确认', '确定预约,重新选择');
            $('#canclebtn').click(function () {
                $('#openDiv').empty();
                SubOrder(2);
            });
            $('#surebtn').click(function () {
                $('#openDiv').empty();
            });
            return "checkNum";
        }
    }
}
//绑定号源信息
function BindNumberWaterList(listResult) {
	if(listResult==null){
		DoctorRegOrder.waterId = 0;
        SelNumber(null, 0, '', null);
        $('#numWatUl').html('<li>暂无号源</li>');
        numChoo.refresh();
        return;
	}
    var listHtml = "";
    var comText = "";
    var showNum = false;
    $.each(listResult, function (i, item) {
        if (item.HintType == null || item.HintType != '4') {
            showNum = true;
        }
        comText = GetNumbersText(item.HintType, item.SerialNo, item.CommendTime, item.CommendScope);
        listHtml += '<li onclick="SelNumber(this,' + item.NumberSN + ',\'' + comText + '\',\'' + item.Store + '\',\'' + item.SerialNo + '\')">' + comText + '</li>';
        if (i == 0) { //默认选第1条
           SelNumber(null, item.NumberSN, comText, item.Store,item.SerialNo);
        }
        if (parseInt(arrangeInfo.IsPartTime) == 1) {
            //SelNumber(null, item.NumberSN, comText, item.Store);
            return false;
        }
    });
    if (listResult.length > 0 && !showNum) {
        $('#selnum').hide();
    }
    if (listHtml == "") {
        DoctorRegOrder.waterId = 0;
        listHtml += '<li>暂无号源</li>';
        SelNumber(null, 0, '暂无号源', null);
    }
    $('#numWatUl').html(listHtml);
    numChoo.refresh();
}
///获取成员列表并载入默认成员
function GetMemberList(first,selMem) {
    var hosKey = "";//不为空时，处理医院就诊人
    if (hospitalId == '1284' || ( isDebug && hospitalId == '1314')) { //
        hosKey = hospitalId;
        $('#maxmember').val('3');
        var fromUrl = encodeURIComponent(location.href.replace('&addm=1','') + "&addm=1");
        var url = "addHosMember.html?platformType=" + platformType + "&hospitalId=" + hospitalId + '&from='+fromUrl;
        $("#ToAddMember").attr("href", url);
    }
    if (isloadcy > 0 && first!=2)
        return;
    var isfirst = (first == 1);//首次加载
    $.ajax({
        url: "../do/registerInfo/getMemberList",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: true,
        data: {
    	    accountSn: loginUserInfo.accountSn,
            hosKey: hosKey
        },
        beforeSend: function () {
            if (!isfirst) {
                myLayer.load('正在加载');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myLayer.clear();
            if (!isfirst) {
                myLayer.alert('网络繁忙,请刷新后重试DE' + stackUUID);
                //SysLog('DE' + stackUUID, XMLHttpRequest, textStatus, errorThrown);
            }
        },
        success: function (json) {
            if (!isfirst) { myLayer.clear(); }
            if (json) {
                var listHtml = "";
                if (json.Code == 10000) {
                    var memberCount = 0;
                    MemList = json.Result;
                    $.each(json.Result, function (i, item) {
                        if (item.HosAdd == null || item.HosAdd !="0") {
                            listHtml += '<li data-msn=\'' + item.Membersn + '\' onclick=\'SelMem(' + JSON.stringify(item) + ')\' >' + item.Cname + '<li>';
                            memberCount++;
                        }
                    });
                    $("#nowMember").val(memberCount);
                    $('#memberUl').html(listHtml);
                    memChoo.refresh();
                    isloadcy = 1;
                    if (memberCount > 0) {
                        selMem = (selMem != null ? selMem : $('#reqMemSn').val());//默认memberSn
                        if(selMem != '') {
                            for (var i = 0; i < memberCount; i++){
                                if ($($("#memberUl li")[i * 2]).attr('data-msn') == selMem) {
                                    $($("#memberUl li")[i * 2]).click();
                                    break;
                                }
                            }
                        }
                        if (DoctorRegOrder.memberSn == null) {
                            if (location.href.indexOf("&addm=1") > 0) {
                                $("#memberUl li").eq((memberCount - 1) * 2).click();//默认最后一个
                            }
                            else {
                                $("#memberUl li").eq(0).click();   //默认选择第一个就诊人
                            }
                        }
                    }
                    else { myLayer.clear(); }
                    //添加成员按钮
                    if ($('#maxmember').val() > memberCount) { //json.Data.Result.length
                        $('#ToAddMember').show();
                    } //当前成员小于最大成员数,显示添加按钮
                    else {
                        $('#ToAddMember').hide();
                    } //隐藏添加按钮
                }
                else {
                    myLayer.clear();
                    if (!isfirst) {
                        myLayer.alert(json.Message);
                    }
                }
            }
            else {
                myLayer.clear();
                if (!isfirst) {
                    myLayer.alert("网络繁忙,请求失败DA" + stackUUID);
                }
            }
            MemLod = true;
        }
    });
}
///选择就诊成员
function SelMem(item) {
	selMemTimes ++;
    $('#memname').text(item.Cname);
    if ($('#telephon').length > 0 && item.Phone != null && item.Phone != '') {//发送验证码号码
        $('#telephon').val(item.Phone);
        $("#telephon").attr("readonly", true);
    }
    else {
        $('#telephon').val('');
        $("#telephon").attr("readonly", false);
    }
    //记录预约信息用,写入挂号临时表
    DoctorRegOrder.memberSn = item.Membersn;         //成员sn
    DoctorRegOrder.memberName = item.Cname;          //成员姓名
    DoctorRegOrder.memberPhone = item.Phone;         //成员电话
    DoctorRegOrder.memberAddress = item.Address;     //成员地址
    DoctorRegOrder.memberBirthday = item.Birthday;   //成员生日
    DoctorRegOrder.memberSex = item.Sex;             //成员性别
    DoctorRegOrder.memberId = item.Memberid;
    DoctorRegOrder.othercard = item.Othercard;          //医保卡
    DoctorRegOrder.guardianmembersn = item.Guardianmembersn;  //监护人sn

    ShowGh();   //显示挂号面板
    $.each(FormCon, function (chei, cheitem) {//清空值
    	var tName = cheitem[0].toLowerCase();
        if ($('#' + tName).length > 0 && tName != 'isread') {
        	if($('#' + tName)[0].tagName=='SELECT'){
        		$('#' + tName + ' option:first').prop("selected", 'selected');
        	}
        	else {
                $('#' + tName).val('');
        	}
        }
    });
    $('#cmb_disease').val('');
    $('#cmb_diseasename').text('请选择');
    // $('#cmb_disease').val(OrderInfo.fromDisId == null ? '' : OrderInfo.fromDisId);
    //$('#cmb_diseasename').text(OrderInfo.fromDisName == null || OrderInfo.fromDisName == '' ? '请选择' : OrderInfo.fromDisName);
    GetCertList(item.Membersn);//获取证件列表
    LoadMemberInfo();  //载入用户数据
    GetClinicCard();    //获取成员就诊卡
    if(defaultDisease()||doctorSn==710903371){
    	 $('#cmb_disease').val(0);
         $('#cmb_diseasename').text('未确诊');
    }
    GetRememberKv();
    checkMemActivationState(true);//检查账户激活
    AuthConfig.ShowYzmInput();
}
//载入用户信息
function LoadMemberInfo() {
    $("#sex").val(DoctorRegOrder.memberSex);
    $("#birthday").val(DoctorRegOrder.memberBirthday); //出生年月
    $("#familyaddress").val(DoctorRegOrder.memberAddress); //地址
    $('#mobile').val(DoctorRegOrder.memberPhone);
    if ($('#mobile').val() != "") {
        $('#mobile').parent().parent().hide();
    }
    else {
        $('#mobile').parent().parent().show();
    }
    if (DoctorRegOrder.othercard != '无医保卡') {
        var cardnum = DoctorRegOrder.othercard ? DoctorRegOrder.othercard : '';
        if ($('#insurancecard').length > 0) {
            $('#insurancecard').val(cardnum);  //医保卡
        }
        else if ($('#citizencard').length > 0) {
            $('#citizencard').val(cardnum);    //市民卡
        }
    }
    if(typeof(LoadAddProCity) == "function"){
         LoadAddProCity();
    }
}
//获取证件列表
var certList;
function GetCertList(memSn) {
    $('#cardno').val('');
    $("#cardno").attr("readonly", true);
    DoctorRegOrder.identitytype = '';
    DoctorRegOrder.memberZhengjianid = '';
    DoctorRegOrder.memberIdcard = '';
    certList = {};
    $.ajax({
    	url: "../do/registerInfo/getMemberCertList",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: true,
        data: {
			channelId:channelId,
			accountSn : loginUserInfo.accountSn,
            memberSn: memSn
        },
        beforeSend: function () {
        },
        success: function (rejson) {
            if (rejson && rejson.Code == 10000) {
                if (rejson.Result == null || rejson.Result.length == 0) {
                    $("#cardno").attr("readonly", false);
                    return;
                }
                var setVal = false;
                $.each(rejson.Result, function (i, item) {
                    certList['Type' + item.certType] = item.certCode;
                        for (var i in $('#cardtype').children()) {
                            if ($('#cardtype').children().eq(i).val() == item.certType) {
                                SetIdcard(item.certType, item.certCode);
                                setVal = true;
                            }
                            if (item.certType == '1'){
                                DoctorRegOrder.memberIdcard = item.certCode;
                            }
                    }
                });
            }
        },
        error: function () {
        }
    });
    $('#cardtype').on('change', function () {
        //console.log(this.value);
        SetIdcard(this.value, certList['Type' + this.value])
    });
}
//设置身份证信息
function SetIdcard(certType, certCode) {
    if( $('#cardtype').val()!= certType){
        $('#cardtype').val(certType);
    }
    $('#cardno').val(certCode);
    if (certType == '0' || (certType == '1' && $('#cardno').val() != '')) {
        $("#cardno").attr("readonly", true);
    }
    else {
        $("#cardno").attr("readonly", false);
    }
    DoctorRegOrder.identitytype = certType;         //证件类别
    DoctorRegOrder.memberZhengjianid = certCode;    //成员证件ID
}
//获取自定义字段
function GetRememberKv() {
    if (DoctorRegOrder.memberSn > 0) {
        $.ajax({
        	url: "../do/registerInfo/getRememberKv",
            timeout: 15000,
            type: "POST",
            dataType: "json",
            async: true,
            cache: true,
            data: {
               memberSn: DoctorRegOrder.memberSn,
               hospitalId: hospitalId,
               deptId: deptId
            },
            beforeSend: function () {
                //myLayer.load('正在加载');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //myLayer.clear();
                //myLayer.alert('网络繁忙,请刷新后重试FE' + stackUUID);
            },
            success: function (json) {
            	if(json.Code==10000 && json.Result ){
            		  $.each(json.Result, function (i, item) {
            			  //{"keyValue":"4,16,221","keyName":"area"}
            			  var keyName = item.keyName.toLowerCase();
            			  var keyValue = item.keyValue;
            			  if(keyName == 'cmb_disease'){
            				  if (keyValue.split('|').length > 1 && keyValue.split('|')[0] != '' && keyValue.split('|')[1] != '' && $('#cmb_disease').val()=="") {
                                  $('#cmb_disease').val(keyValue.split('|')[0]);
                                  $('#cmb_diseasename').text(keyValue.split('|')[1]);
                              }
            			  }else if(keyName == 'isrediagnosis'){

            			  }else if(keyName == 'selectarea'){

                          }else{
            				  if ($('#' + keyName).val() == null || $('#' + keyName).val() == "") {
                                  $('#' + keyName).val(keyValue);
                              }
            			  }
            		  });
            	}
            }
        });
    }
}
///获取成员就诊卡
function GetClinicCard() {
    if ($('#cliniccard').length == 0 || DoctorRegOrder.memberSn == null || DoctorRegOrder.memberSn=='') {
        myLayer.clear();
        return;
    }
    $.ajax({
    	url: "../do/registerInfo/getClinicCard",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: true,
        data: {
           accountSn: loginUserInfo.accountSn,
           hospitalId: hospitalId,
           memberId: DoctorRegOrder.memberId
        },
        beforeSend: function () {
            //myLayer.load('正在加载');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myLayer.clear();
            myLayer.alert('网络繁忙,获取就诊卡失败,请刷新后重试GE' + stackUUID);
            ////SysLog('GE' + stackUUID, XMLHttpRequest, textStatus, errorThrown);
        },
        success: function (json) {
            //myLayer.clear();
            if (json) {
                if (json.Code == 10000) {
                    if (json.Result.length > 0 && json.Result[0].Cardno != null && json.Result[0].Cardno != "初诊") {
                        $('#cliniccard').val(json.Result[0].Cardno);   //自动填写就诊卡
                        if ($('#cardli').length > 0) {
                            $('#jzlxcz').removeAttr("checked");
                            $('#jzlxcz').parent().removeClass("checked");
                            $('#jzlxfz').attr('checked', 'checked');
                            $('#jzlxfz').parent().addClass("checked");
                            $('#cardli').show();
                            $('#cardliSel').find('.card-stp').hide();
                        }
                    }
                    else {
                        $('#jzlxfz').removeAttr("checked");
                        $('#jzlxfz').parent().removeClass("checked");
                        $('#jzlxcz').attr('checked', 'checked');
                        $('#jzlxcz').parent().addClass("checked");
                        $('#cardli').hide();
                        $('#cardliSel').find('.card-stp').show();
                    }
                   GetPacsApplyInfo();
                   GetNumberWaterList(2);
                }
                else {
                    myLayer.alert(json.Message);
                }
            }
            else {
                myLayer.alert('网络繁忙,请求失败GA' + stackUUID);
            }
        }
    });
}
///查询用户未完成订单
function GetLockOrderInfo() {
    $.ajax({
    	url: "../do/registerInfo/getUnfinishOrder",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        data: {
           accountSn: loginUserInfo.accountSn,
           channelId: channelId
        },
        beforeSend: function () {
            //myLayer.load('正在加载');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //myLayer.clear();
        },
        success: function (json) {
            //myLayer.clear();
            if (json) {
                //console.log(JSON.stringify(json));
                if (json.Code == 10000 && json.Result && json.Result.length > 0) {  //有锁号并创建订单信息
                        var orderInfo = json.Result[0];
                        var ddOrderId = orderInfo.Orderid;
                        //console.log(orderUid);
                        if(ConfDialog("提示", "您有未处理的订单，请先处理", '查看订单',10)){
                        	$("#surebtn").click(function () {
                                window.location = "../orderRecord/recordDetail.html?platformType=" + platformType + "&orderid=" + ddOrderId;
                                $("#openDiv").empty();
                            });
                        }

                }
            }
        }
    });
}
//获取号源展示信息
function GetNumbersText(hintType, serialNo, commendTime, commendScope) {
    var comText = "";
    switch (parseInt(hintType)) {
        case 1: //1提示序号
            comText = "第" + serialNo + "号 ";
            break;
        case 2: //2提示时间
            comText = commendTime ? commendTime : commendScope ? commendScope : "--";
            break;
        case 3: //3提示序号与时间
            if (serialNo > 0) {
                comText = "第" + serialNo + "号 ";
            }
            else {
                comText = "-- ";
            }
            comText += commendTime ? commendTime : commendScope ? commendScope : "";
            break;
        case 4: //4什么都不提示
            comText = "--";
            break;
        case 5:
            comText = commendScope ? commendScope : "--";
            break;
        default:
            if (hospitalId == 700331) {
                comText = commendTime ? commendTime : commendScope ? commendScope : "--";
            } //华中科技大学同济医学院附属协和医院 特殊展示
            else {
                comText = commendScope ? commendScope : "--";
            }
            break;
    }
    //if (hospitalId == 2114) {
    //    comText = comText.replace("候诊", "取号");
    //} //北京市海淀区 武警总医院 特殊展示
    return comText;
}
//显示更多
function ShowMoreInfo(obj, show) {
    if (show == null) {
        show = !$(obj).hasClass('eshow');
    }
    if (show) {
        $(obj).addClass('eshow').find('label').html('收起');
        $('.childul').show();
    } else {
        $(obj).removeClass('eshow').find('label').html('编辑就诊人');
        $('.childul').hide();
    }
}
//表单信息提示
function AlertErr(ErrMsg) {
        var showMsg = "", exInfo = "", title = "提示", btn = "确定", okFun = "";
        if (typeof (ErrMsg) == "string") {
            showMsg = ErrMsg;
        }
        else {
            showMsg = ErrMsg[0];
            exInfo = ErrMsg[1];
            title = (ErrMsg[2] != null ? ErrMsg[2] : "提示");
        }
        switch (exInfo) {
            case "BaseInfo":
                break;
            case "DiseaseInfo":
                okFun = '$(window).scrollTop(0);'
                    + '$("#ghDiv").hide();'
                    + '$("#diseaseShow").show();'
                    + 'SearchDisease();';
                break;
            case "P":
                eval($('#readDialog').val());
                return;
            default:
                ShowMoreInfo($('.editinto'), true);
                break;
        }
        ConfDialog(title, showMsg, btn);
        $("#surebtn").click(function () {
            $("#openDiv").empty();
            eval(okFun);
        });
        return;
}
///立即预约
function SubOrder(flag) { // flag ==1 不实名验证 2不判断号源刷新
	myLayer.load('正在处理中');
	if($('#cliniccard').length>0){
		 $('#cliniccard').val($('#cliniccard').val().toUpperCase());
	}
    //表单信息处理
    var ErrMsg = VerOrderForm();
    //表单信息提示
    if (ErrMsg != null && ErrMsg != "") {
        AlertErr(ErrMsg);
        myLayer.clear();
        return;
    }
    if(!checkMemActivationState()){//检查账户激活
    	myLayer.clear();
    	return;
    }
    if(!checkMore(flag)){
    	return;
    }
//    alert('continue');
//    return;
    try { //实名认证
        if (AuthConfig.OnOff == 1 && flag != 1) {
            AuthConfig.CheckMemAuth(false);
            //认证
            if (!AuthConfig.ShowAuthWin()) {
            	myLayer.clear();
                return;
            }
            //防刷验证
            if (!AuthConfig.ShowAuthAntiBrush()) {
            	myLayer.clear();
                return;
            }
        }
    }
    catch (e) { }
    if (flag != 2 && GetNumberWaterList(9) == "checkNum") {
    	myLayer.clear();
        return;
    }
    ///获取预约表单中的值
    var hasformConStr = JSON.stringify(FormCon);   //表单内容值字符串
    //获取预约表单信息
    if ($('[name=ghFormCon]').length > 0) {
        var jzlxcz = $('#jzlxcz');  //初诊单选框
        var isselcz = false;        //默认选择复诊
        if (jzlxcz.length > 0) {
            isselcz = $('#jzlxcz').attr('checked') ? true : false;
        } //如果有初诊单选框就获取选择的状态
        var clinicCardKey = "";
        $.each($('[name=ghFormCon]'), function (i, item) {
            item = $(item);
            //判断数据中是否已经有该字段
            var hasformConStrPd = hasformConStr.toLowerCase();
            if (hasformConStrPd.indexOf(item.attr('id')) > -1) {
                //便利数组,修改对应键的值
                $.each(FormCon, function (chei, cheitem) {
                    if (cheitem[0].toLowerCase() == item.attr('id')) {
                        if (item.attr('id') == "cliniccard") {
                            //就诊卡没有选择复诊的时候设置空值
                            if (isselcz) {
                                cheitem[1] = '';
                            }
                            else {
                                cheitem[1] = item.val();
                            }
                        }
                        else {
                            cheitem[1] = item.val();
                        }
                    }
                });
            } //有该字段,修改值
            else {
                if (item.val()) {
                    if (item.attr('id') == "cliniccard") {
                        //就诊卡没有选择复诊的时候设置空值
                        if (isselcz) {
                        }
                        else {
                            FormCon.push([item.attr('id'), item.val().replaceAll("'", "")]);
                        }
                    }
                    else {
                        FormCon.push([item.attr('id'), item.val().replaceAll("'", "")]);
                    }
                }
            } //没有该字段添加对应的字段和值
        });
        //FormCon.push(["cmb_diseaseName", $('#cmb_diseasename').text()]);//增加疾病名称参数boss
    }
    // SetJianHuRen(); //监护人信息

    //记录预约信息用,写入挂号临时表
    DoctorRegOrder.doctorSn = doctorInfo.doctorSn;        //医生sn
    DoctorRegOrder.hosDeptId = doctorInfo.hosDeptId;     //科室id
    DoctorRegOrder.hosDeptName =doctorInfo.deptName;    //科室名称
    DoctorRegOrder.hospitalId = doctorInfo.hospitalId;           //预约时使用的医院ID
    DoctorRegOrder.hospitalName = doctorInfo.hosName;            //预约时使用的医院名称
    DoctorRegOrder.doctorService_gh = doctorInfo.doctorService_gh; //挂号状态	2:开放，1，未开放，3暂停
    DoctorRegOrder.doctorUid = doctorInfo.doctorUid;            //医生uid
    DoctorRegOrder.doctorName = doctorInfo.doctorName;           //医生名称
    DoctorRegOrder.doctorPic = doctorInfo.photoUri;            //医生头像
    DoctorRegOrder.doctorClinicName = doctorInfo.lczcName;     //医生职称
    DoctorRegOrder.GH_HosProId = doctorInfo.provinceId;  //省份id
    DoctorRegOrder.GH_HosProName = doctorInfo.provinceName;  //省份id
    DoctorRegOrder.GH_HosCityId =doctorInfo.cityId;  //城市id
    DoctorRegOrder.GH_HosCityName = doctorInfo.cityName;  //城市id

    DoctorRegOrder.registerDate = arrangeInfo.registerdate;    //就诊日期
    DoctorRegOrder.timeId = arrangeInfo.timeid;         //时间段(上午,下午,晚上)
    DoctorRegOrder.arrangeId = arrangeInfo.ArrangeID;   //排班ID
    DoctorRegOrder.ghAmount = arrangeInfo.specialprice;       //挂号费用
    DoctorRegOrder.securityDeposit = arrangeInfo.securityDeposit;// 守约保证金
    DoctorRegOrder.ghfeeway = arrangeInfo.ghfeeway;     //挂号费处理方式:1守约保证金,3代缴挂号费
    DoctorRegOrder.ModeId = arrangeInfo.ModeId;         //0非实时,1实时
    DoctorRegOrder.GhFee = arrangeInfo.GhFee;           //挂号服务费
    DoctorRegOrder.AllFee = arrangeInfo.AllFee;           //总收费
    DoctorRegOrder.availablenum = arrangeInfo.availablenum; //剩余号源
    DoctorRegOrder.Gh_TypeName = arrangeInfo.typeName;     //门诊类型
    DoctorRegOrder.feeName = arrangeInfo.feeName;
    //DoctorRegOrder.fromDisId = disId;
    //DoctorRegOrder.fromDisName = disName;
    DoctorRegOrder.UnOpened = arrangeInfo.UnOpened;//未放号
    DoctorRegOrder.FHTimes = arrangeInfo.FHTimes;//放号时间
    DoctorRegOrder.FHDays = arrangeInfo.FHDays;//放号天数 = $("#doctorid").val();            //医生uid
    DoctorRegOrder.accountSn = loginUserInfo.accountSn;
    DoctorRegOrder.cardNumber = loginUserInfo.cardNumber;
    DoctorRegOrder.loginId = loginUserInfo.loginId;
    DoctorRegOrder.channelId = channelId;
    DoctorRegOrder.utm_source = jkzlAn.getUtmSource();
    DoctorRegOrder.doctorOfficeName=doctorOfficeName;
    for (var i = 0; i < FormCon.length; i++) {
    	FormConKv.push({keyValue:FormCon[i][1] , keyName:FormCon[i][0]});
    }
    if ($('#cmb_disease').val() != '' && $('#cmb_diseasename').text()!='') {
    	FormConKv.push({keyValue:$('#cmb_disease').val()  , keyName:"cmb_disease"});
    	FormConKv.push({keyValue:$('#cmb_diseasename').text()  , keyName:"cmb_diseaseName"});
    }

    if(isNotNull(DoctorRegOrder.selectArea)){
        FormConKv.push({keyValue:DoctorRegOrder.selectArea  , keyName:"selectArea"});
    }

    if(isNotNull(retId)){
    	DoctorRegOrder.retId=retId;
    }

    //泰康加油站特殊标识
    if(channelId=='9001181'){
        var glCustomChannel = $.cookie(channelId+"_attach") == null ? "" : $.cookie(channelId+"_attach");
        if(isNotNull(glCustomChannel)){
            DoctorRegOrder.glCustomChannel=glCustomChannel;
        }
    }

    console.log(DoctorRegOrder);

    UpdateMemberInfo();  //更新成员信息
    //福建省立医院就诊卡为空时判断是否有在医院就诊过，如果选是则就诊卡必填，选否则允许就诊卡为空
    if(DoctorRegOrder.hospitalId==31&&isNotNull(DoctorRegOrder.cliniccard)&&DoctorRegOrder.cliniccard==0){
    	checkMemberInfoService.checkMemberRegwater();
    }else if(DoctorRegOrder.hospitalId==33&&isNotNull(DoctorRegOrder.cliniccard)){
    	checkMemberInfoService.checkMember();
    }else{
    	GH();
    }
    return false;
}
function GH(){
    if(delayRegister){
        myLayer.load('正在处理中');
        var delayTime=Math.ceil(Math.random()*1000)+1000;
        setTimeout(function () {
            if(getYuYueChannelConfigs("msgSubPush")==1){
                showPopSubscribeTips("appoint.ghtjym",function () {
                    toGH();
                });
            }else{
                toGH();
            }
        },delayTime)
    }else{
        if(getYuYueChannelConfigs("msgSubPush")==1){
            showPopSubscribeTips("appoint.ghtjym",function () {
                toGH();
            });
        }else{
            toGH();
        }
    }
}

function toGH() {
    myLayer.load('正在处理中');
    var drStr = JSON.stringify(DoctorRegOrder); //预约信息
    var formConStr = JSON.stringify(FormConKv);   //表单内容值
    if (arrangeInfo.IsCheckOrder == 1 ) {
        GhFunNew(drStr, formConStr);
    }
    else {
        GhFun(drStr, formConStr);
    }
}
///请求挂号
function GhFun(drStr, formConStr) {
    $.ajax({
    	url: "../do/registerInfo/register",
        timeout: 30000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        data: {
        	doctorRegOrder: drStr,
        	ghFormCon: formConStr
        },
        beforeSend: function () {
            myLayer.load('正在处理中');
        },
        success: function (rejson) {
        	//console.log(JSON.stringify(rejson));
            if (rejson) {
                if (rejson.Code == 10000) {
                	myLayer.load("加载中...");
                	var orderId = rejson.Orderid;
                        var pzItem = GetPzItem();
//                      if (pzItem != "") {
//                            ServerHitPz("peizhen109");
//                      }
                    location.href = 'confirmOrder.html?platformType=' + platformType + '&doctorSn=' + doctorSn + '&orderId=' + orderId + '&pzItem=' + pzItem;
                } //
                else if (rejson.Code == -771) { //未开放预约
                	myLayer.clear();
                    var showMsg = "预计放号时间：<span class='c-ff8100'>" + rejson.Data + "</span>";
                    ConfDialog("未到放号时间", showMsg, "确定");
                    $("#surebtn").click(function () {
                        $("#openDiv").empty();
                    });
                }
                else if (rejson.Code == -3004) { //信用值
                	myLayer.clear();
                    var url = StrUserUrl + "/UserCenter/web/member/credit.html?membersn=" + DoctorRegOrder.memberSn + "&hosid=" + OrderInfo.GH_HosID + "&platformType=" + PlatformType + "&sourcetype=0&sourceid=0";
                    var showMsg = "您的信用不足，预约失败，点击<span style='color:red'>查看信用</span>可了解更多详情";
                    ConfDialog('提示', showMsg, '取消,查看信用');
                    $("#surebtn").click(function () {
                        location.href = url;
                    });
                }
                else {
                	myLayer.clear();
                    ConfDialog('提示', (rejson.Message == "" ? ("网络忙，请稍后再试(" + rejson.Code + ")") : rejson.Message) + 'BA' + stackUUID, '知道了');
                    $("#surebtn").click(function () {
                        $("#openDiv").empty();
                    });
                } //
            }
            else {
            	myLayer.clear();
                myLayer.alert("预约超时,请稍后再试BB" + stackUUID);
            } // objJson为空
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myLayer.clear();
            myLayer.alert('网络繁忙,请刷新后重试BE' + stackUUID);
            //SysLog('BE' + stackUUID, XMLHttpRequest, textStatus, errorThrown);
        }
    });
}
//监护人信息处理
function SetJianHuRen()
{
    var GuardianID = $("#guardianid").val();
    var jianhuren =  $("#jianhuren").val();
    try{
        if ((DoctorRegOrder.memberZhengjianid == null || DoctorRegOrder.memberZhengjianid == "") && GuardianID != null && GuardianID !='')
        {
            DoctorRegOrder.identitytype = '1';
            DoctorRegOrder.memberZhengjianid = GuardianID;
            DoctorRegOrder.memberIdcard =  GuardianID;
            if (jianhuren != null && jianhuren != "")
            {
                DoctorRegOrder.memberName = jianhuren;
            }
        }

    }
    catch(e)
    {}
}
///验证预约信息
function VerOrderForm() {
    //记录预约信息用,写入挂号临时表
    if ($("#cardno").val() != null && $("#cardno").val() != "") {
        DoctorRegOrder.memberZhengjianid = $("#cardno").val();       //成员证件
        DoctorRegOrder.identitytype = $("#cardtype").val();    //SelIdCardInfo.idCardTypeId; //身份证类型(新)
    }
    //记录地址
    if ($("#familyaddress").val() != null) {
        DoctorRegOrder.memberAddress = $("#familyaddress").val();       //地址
    }
    //生日
    if ($("#birthday").val() != null && $("#birthday").val() != "") {
        DoctorRegOrder.birthday = $("#birthday").val();
        DoctorRegOrder.memberBirthday = $("#birthday").val();
    }
    //性别
    if ($("#sex").val() != null) {
       DoctorRegOrder.memberSex = '';
    }
    if ( $("#sex").val() == "1" || $("#sex").val() == "2") {
        DoctorRegOrder.memberSex = $("#sex").val();
    }
    else if (DoctorRegOrder.memberIdcard != '') {
        var idc;
        if (DoctorRegOrder.memberIdcard.length == 18) {
            idc = parseInt(DoctorRegOrder.memberIdcard.substr(16, 1));
        }
        else if(DoctorRegOrder.memberIdcard.length == 15){
            idc = parseInt(DoctorRegOrder.memberIdcard.substr(14, 1));
        }
        if (idc != null) {
            DoctorRegOrder.memberSex = (idc % 2 == 0 ? 2 : 1);
        }
    }
    if ($("#mobile").val() !=null  && $("#mobile").val() != "") {
        DoctorRegOrder.memberPhone = $("#mobile").val();
    }
    //收费类型
    DoctorRegOrder.regpaytype = $('#regpaytype').val() != null ? $('#regpaytype').val() : "";
    //就诊卡
    DoctorRegOrder.cliniccard = GetInputCliniccard();

    if ($('#insurancecard').length > 0) {
        DoctorRegOrder.othercard =  $('#insurancecard').val().toUpperCase();  //医保卡
    }
    else if ($('#citizencard').length > 0) {
        DoctorRegOrder.othercard = $('#citizencard').val().toUpperCase();    //市民卡
    }
    if ($('#selPacsItem').length > 0) {    //检查单
        DoctorRegOrder.applyNo = $('#selPacsItem').val();
    }
    else {
        DoctorRegOrder.applyNo = '';
    }

    if($('#selectarea').length>0&&isNotNull($('#selectarea').attr('data-gbcode'))){
        var idArr=$('#selectarea').attr('data-code').split(',');
        var gBCodeArr=$('#selectarea').attr('data-gbcode').split(',');
        var nameArr=$('#selectarea').attr('data-val').split(',');
        var pcasJson={};
        pcasJson.provId=idArr[0];
        pcasJson.provName=nameArr[0];
        pcasJson.provinceGBCode=gBCodeArr[0];

        pcasJson.cityId=idArr[1];
        pcasJson.cityName=nameArr[1];
        pcasJson.cityGBCode=gBCodeArr[1];

        pcasJson.areaId=(idArr.length==3?'':idArr[2]);
        pcasJson.areaName=(idArr.length==3?'':nameArr[2]);
        pcasJson.areaGBCode=(idArr.length==3?'':gBCodeArr[2]);

        pcasJson.streetId=idArr[idArr.length-1];
        pcasJson.streetName=nameArr[idArr.length-1];
        pcasJson.streetGBCode=gBCodeArr[idArr.length-1];

        DoctorRegOrder.selectArea=pcasJson;
    }

    //判断必填项
    if (!DoctorRegOrder.memberSn) {
        return ['请选择成员','BaseInfo'];
    }
    if (HasPacsServer) { //检查单服务就诊卡
        if (DoctorRegOrder.cliniccard == '') {
            return ['请先输入就诊卡号', 'BaseInfo'];
        }
        if ($('#liPacsItem').length == 0){
            return ['该就诊卡未查询到对应的检查项', 'BaseInfo'];
        }
        if ($('#liPacsItem').length > 0 && $('#selPacsItem').val() == '') {
            return ['请选择检查项', 'BaseInfo'];
        }
    }
    if (DoctorRegOrder.waterId == null) {
        return ['请选择号源','BaseInfo'];
    }
    else if (DoctorRegOrder.waterId == 0) {
        return ['暂无号源','BaseInfo'];
    }
    if ($.trim($("#cardno").val()) != "") {
        $("#cardno").val($("#cardno").val().toUpperCase());
        if (!CheckCert($("#cardno").val(), $("#cardtype").val())) {
            return '请输入正确的证件号码！';
        }
    }
    if ($("#guardianid").val() != null && $("#guardianid").val() != "" && !isIdCardNo($("#guardianid").val()))
    {
        return  '请输入正确监护人身份证！';
    }
    var cliniccard = DoctorRegOrder.cliniccard;
    if ($("#jzlxfz").attr("checked") == "checked" && cliniccard == "") {
        return ['请输入就诊卡号', 'BaseInfo'];
    }
    if ($("#SourceId").val() == "944" && $("#jzlxfz").attr("checked") && cliniccard != "" && cliniccard != $("#cardno").val()) {
        return ['就诊卡错误', 'BaseInfo'];
    }
    if (cliniccard != "初诊" && ContainChinese(cliniccard)) {
        return ['就诊卡填写有误', 'BaseInfo'];
    }

    if (!DoctorRegOrder.memberPhone || DoctorRegOrder.memberPhone == "") {
        return "该就诊人个人信息不完善，请完善手机号码";
    }
    if ($('#cmb_disease').val() == '') {
        return ['请选择就诊的疾病信息。<br>还未确认疾病的，请选择"未确诊"。', 'DiseaseInfo'];
    }
    //判断表单内容
    var messText = "";
    $.each(Ismust, function (i, item) {
        //,name,mobile,CardNo,sex,familyaddress,birthday,
        var itemKey = item[0].toLowerCase();
        switch (itemKey) {
            case "name":
                if (!DoctorRegOrder["memberName"]) {
                    messText = ['该就诊人个人信息姓名不完善，请完善姓名','BaseInfo'];
                    return;
                }
                break; //姓名,从成员信息中获取
            case "cardno":
                if ($.trim($("#cardno").val()) == "" && (isNaN($("#guardianid").length) || $("#guardianid").length <= 0 || $.trim($("#guardianid").val()) == "")) { //
                    messText = '证件信息不完善，请完善证件信息';
                    return;
                }
                break;
            case "sex":
                if (!DoctorRegOrder["memberSex"]) {
                    messText = '该就诊人个人信息不完善，请完善性别信息';
                    return;
                }
                break;
            case "birthday":
                if ( !$("#birthday").val()) {
                    messText = '该就诊人个人信息生日不完善，请完善生日';
                    return;
                }
                break; //生日,从成员信息中获取
            case "familyaddress":
                if (!DoctorRegOrder["memberAddress"] || DoctorRegOrder["memberAddress"] == '无') {
                    messText = '该就诊人个人信息地址不完善，请完善地址';
                    return;
                }
                break; //地址,从成员信息中获取
            case "selectarea":
                if (!DoctorRegOrder["selectArea"]) {
                    var str = (item[1] == null || item[1] == "" ? "表单验证失败" : item[1]);
                    messText = [str, item[2], item[3]];
                    return;
                }
                break;
            default:
                if ($('#' + itemKey).length <= 0 || ($('#' + itemKey).val() != null && $('#' + itemKey).val().NoSpace().length <= 0)) {
                    var str = (item[1] == null || item[1] == "" ? "表单验证失败" : item[1]);
                    messText = [str, item[2], item[3]];
                } //判断表单内容
                else {
                    //记录预约信息用,写入挂号临时表
                    DoctorRegOrder[item[0]] = ($('#' + itemKey).val() != null ? $('#' + itemKey).val().NoSpace() : '');   //记录表单信息
                }
                break;
        }
    });
    return messText;
}
///选择号源
function SelNumber(th, numsn, comtext, store,serialNo) {
    th = $(th);
    th.siblings().removeAttr('class');
    th.attr('class', 'hit');
    //记录预约信息用,写入挂号临时表
    DoctorRegOrder.waterId = numsn;         //号源ID
    DoctorRegOrder.waitingInfor = comtext;  //就诊信息
    DoctorRegOrder.store = store;           //实时对接（Store）
    DoctorRegOrder.serialNo = (serialNo == null? '': serialNo);
    $('#numcon').removeAttr('style').text(comtext); //显示选择的号源内容
    ShowGh();                   //显示挂号面板
}
///显示挂号面板,隐藏选择号源成员面板
function ShowGh() {
    $('#memberDiv').hide();
    $('#numDiv').hide();
    if ($('#openDiv').text().trim() == '') {
        $('.pop_screen').hide();
    }
    //$('#dvSeleCardType').hide();
}
//更新成员信息
function UpdateMemberInfo() {
    //alert(DoctorRegOrder.memberSn);
    var userSex = $("#sex").val();
    var userIdCard = $("#cardno").val() ? $("#cardno").val() : "";
    var idcardType = $("#cardtype").val();
    var userBirthday = $("#birthday").val() ? $("#birthday").val() : "";
    //var address = $("#familyaddress").val() ? $("#familyaddress").val() : "";
    var formConStr = $.toJSON(FormConKv);
    $.ajax({
    	url: "../do/registerInfo/updateMemberInfo",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        data: {
        	"channelId": channelId,
            "memberSn": DoctorRegOrder.memberSn,
            "memberId": DoctorRegOrder.memberId,
            "accountSn":loginUserInfo.accountSn,
            "phone": DoctorRegOrder.memberPhone,
            "identityType": idcardType,
            "idcard": userIdCard,
            "Othercard": DoctorRegOrder.othercard,
            "sex": userSex,
            "birthday": userBirthday,
            "address": DoctorRegOrder.memberAddress,
            "provinceId": DoctorRegOrder.addProId,
            "cityId": DoctorRegOrder.addCityId,
            "areaId": DoctorRegOrder.addAreaId,
            "memberName": DoctorRegOrder.memberName,
            "cardId": loginUserInfo.cardNumber,
            "hospitalId": hospitalId,
            "hospitalName": doctorInfo.hosName,
            "cliniccard": DoctorRegOrder.cliniccard,
            "kvItem": formConStr
        },
        beforeSend: function () {
        },
        error: function () {
        },
        success: function (json) {
            if (typeof (AddPatientPool) == "function") { //增加患者池
                AddPatientPool();
            }
        }
    });
}
//获取备注
function ShowRemark()
{
    var registerDate = arrangeInfo.registerdate;
    $.ajax({
    	url: "../do/registerInfo/getArrangeWaterRemark",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: true,
        data: {
        	hospitalId: hospitalId,
            doctorSn: doctorSn,
            registerDate: registerDate,
            channelId: channelId
        },
        beforeSend: function () {
            //myLayer.load('正在加载');
        },
        error: function () {
            //myLayer.clear();
        },
        success: function (text) {
            //myLayer.clear();
            if (text != "") {
                $('#divRemarkBlock').show();
                $('#divRemark').html(text);
            }
        }
    });
}
//获取输入的就诊卡
function GetInputCliniccard() {
    var cliniccard = "";
    if (($('#jzlxfz').length > 0 && $('#jzlxfz').attr('checked') == 'checked' && $("#cliniccard").val() != null)
       || ($('#jzlxfz').length == 0 && $("#cliniccard").val() != null)) {
        cliniccard = $("#cliniccard").val().toUpperCase();
    }
    return cliniccard;
}
//显示费用类型提示
function ShowPayTypeMsg(type) { //
    if (hospitalId == '1284' && (type == '1' || type == '2')) {//中山附一
        var msg = TxtContent("GhPayTypeMsg/Type" + type, true);
        if (msg != "") {
            ConfDialog('提示', '<div class=c-t-left>' + msg + '</div>', '确定');
            $("#surebtn").click(function () {
                $("#openDiv").empty();
            });
        }
    }
}
//添加就诊人
function toAddMember(){
	var fromUrl = encodeURIComponent(location.href.replace('&addm=1','') + "&addm=1");
	var url = StrUserUrl + '/UserCenter/web/member/member.html?platformType='+ platformType +'&sourceType=0&sourceId=0&from='+fromUrl;
	location.href= url;
}
var checkMemberInfoService={
		checkMember:function(){
			$.ajax({
		    	url: "../do/registerInfo/queryUserInfo4YY",
		        timeout: 30000,
		        type: "POST",
		        dataType: "json",
		        cache: false,
		        data: {
		        	clinicCard: DoctorRegOrder.cliniccard,
		        	username: DoctorRegOrder.memberName,
		        	mobile: DoctorRegOrder.mobile,
		        	CardNo: DoctorRegOrder.memberZhengjianid,
		        	cardtype: DoctorRegOrder.identitytype,
		        	membersn: DoctorRegOrder.memberSn,
		        	version: DoctorRegOrder.identitytype==1?1:0,
		        	ghthospitalid: DoctorRegOrder.hospitalId
		        },
		        beforeSend: function () {
		        },
		        success: function (d) {
		        	if(d.Code==10000){
		        		//挂号
		        		GH();
		        	}else if(d.Code==-13003&&isNotNull(d.Data)&&isNotNull(d.Data.sfzh)){
                		myLayer.clear();
		        		var sfzh=d.Data.sfzh;
		        		var msgHtml='院内预留身份证：'+sfzh;
		        		msgHtml+='<br/>身份证信息必须与院内预留身份证相同才能预约，请确认是否同步改为院内预留身份证（如院内预留身份证不正确请到医院修改后再预约）';
		        		myLayer.confirm({
		        	        title:'提示',
		        	        con:msgHtml,
		        	        cancel: function(){
		        	        	DoctorRegOrder.memberIdcard=sfzh;
		                        DoctorRegOrder.memberZhengjianid=sfzh;
		                        FormConKv = FormConKv.map(function(obj,index) {
		                  		   var rObj = {};
		                  		   rObj.keyName=obj.keyName;
		                  		   if(obj.keyName=='CardNo'){
		                  			   rObj.keyValue = sfzh;//修改属性
		                  		   }else{
		                  			   rObj.keyValue = obj.keyValue;
		                  		   }
		                  		   return rObj;
		                  		});
		        	        	checkMemberInfoService.updateAccMember();
		        	        },
		        	        cancelValue:'确认',
		        	        ok: function(){},
		        	        okValue:'取消'
		        	    });
		            }else {
                		myLayer.clear();
		            	myLayer.confirm({
		        	        title:'提示',
		        	        con:isNotNull(d.Message)?d.Message:'网络忙，请稍后再试',
		        	        cancel: function(){},
		        	        cancelValue:'确认',
		        	        ok: function(){},
		        	        okValue:'取消'
		        	    });
		            }
		        }
		    });
		},
		updateAccMember:function(){
			$.ajax({
            	url: "../do/registerInfo/updateAccMember",
                timeout: 30000,
                type: "POST",
                dataType: "json",
                cache: false,
                data: {
                	memberSn: DoctorRegOrder.memberSn,
                	Idcard: DoctorRegOrder.memberIdcard,
                	Identitytype:1
                },
                beforeSend: function () {
                },
                success: function (d) {
                	if(d.Code==10000){
                		//挂号
                		GH();
                    }else {
                		myLayer.clear();
                    	myLayer.confirm({
		        	        title:'提示',
		        	        con:isNotNull(d.Message)?d.Message:'网络忙，请稍后再试',
		        	        cancel: function(){},
		        	        cancelValue:'确认',
		        	        ok: function(){},
		        	        okValue:'取消'
		        	    });
                    }
                }
            });
		},
		checkMemberRegwater:function(){
			$.ajax({
            	url: "../do/registerInfo/checkMemberRegwater",
                timeout: 30000,
                type: "POST",
                dataType: "json",
                cache: false,
                data: {
                	memberSn: DoctorRegOrder.memberSn,
                	hospitalId: DoctorRegOrder.hospitalId
                },
                beforeSend: function () {
                },
                success: function (d) {
                	if(d.Code==10000){
                		if(d.Total<=0){
    		        		var msgHtml='请确认是否有在医院就诊过';
    		        		myLayer.clear();
    		        		myLayer.confirm({
    		        	        title:'提示',
    		        	        con:msgHtml,
    		        	        ok: function(){
    		        	        	if(DoctorRegOrder.cliniccard=="0"){
    		        	        		myLayer.alert('您已在医院就诊过，请填写就诊卡，并且就诊卡号不能为0');
    		        	        		return;
//    		        	        		alert("请填写就诊卡");
    		        	        	}
    		        	        },
    		        	        cancel: function(){
    		        	        	myLayer.load('正在处理中');
    		        	        	GH();
    		        	        },
    		        	        cancelValue:'否',
    		        	        okValue:'是'
    		        	    });
                		}else{
                			if(DoctorRegOrder.cliniccard=="0"){
        		        		myLayer.clear();
	        	        		myLayer.alert('您已在医院就诊过，请填写就诊卡，并且就诊卡号不能为0');
	        	        		return;
                			}else{
		        	        	myLayer.load('正在处理中');
    	        	        	GH();
                			}
                		}
                    }else{
        	        	myLayer.load('正在处理中');
        	        	GH();
                    }
                }
            });
		}
}

function defaultDisease(){
	var ret = false;
		$.ajax({
			type : 'post',
			async : false,
			url : getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
			data : {
				hospitalId : hospitalId,
				type : 41,
				time : new Date().getTime()
			},
			beforeSend : function() {},
			error : function() {
			},
			success : function(d) {
				myLayer.clear();
				if (d.Code == 10000) {
					if (d.Result.value == 1) {
						ret = true;
					}
				}
			}
		});
		return ret;
}


function CheckDelayRegister() {
    $.ajax({
        type : 'post',
        async: true,
        url :  "../do/baseInfo/getYuYueChannelConfigs",
        data : {
            channelId: channelId,
            type : 'delayRegister',
        },
        success : function(d) {
            if (d.Code == 10000 && d.value=='1') {
                delayRegister=true;
            }
        }
    });
}

