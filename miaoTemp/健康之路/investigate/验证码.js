//var plusFiename = ",area,communityhos,";   //表单特殊处理字段
//显示保证金说明
function ShowBzjRemark() {
    var content = TxtContent("BzjRemark", true);
    var content = "<div class='c-t-left' id='popintro'>" + content + "</div>";
    myLayer.confirm({ title: '守约保证金说明', con: content, okValue: "知道了" });

    var docheight = document.documentElement.clientHeight;
    var dtop = docheight - $('#popintro').parents('.conf_box').height();
    $("#openDiv .confcontent").css({ "max-height": (docheight - 200) + "px", "overflow-y": "auto" });
    $("#openDiv .conf_box").css({ "top": dtop /2});

    $("#surebtn").click(function () {
        $("#openDiv").empty();
    });
}

var HasLoadDisease = false; //已载入疾病数据
//搜索疾病
function SearchDisease(getNew) {
    if (HasLoadDisease && !getNew) {
        return;
    }
    var inpName = $("#inpDiseaseName").val().trim();
    $.ajax({
    	url: "../do/registerInfo/searchDisease",
        timeout: 8000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: true,
        data: {
        	 deptId: deptId,
             doctorSn: doctorSn,
             diseaseName: inpName
        },
        beforeSend: function () {
            localLoad('ulDiseaseList');
        },
        success: function (rejson) {
            //myLayer.clear();
            if (rejson) {
                if (rejson.Code == 10000) {
                    var listHtml = '<li data-id="0" onclick="SelDisease(this)" >未确诊</li>';
                    if (rejson != null && rejson.Result != null) {
                        $.each(rejson.Result.Dept, function (i, item) {
                            //if (inpName=='' || item.diseaseName.indexOf(inpName) > -1) {
                                listHtml += '<li data-id="' + item.diseaseId + '" ' + 'onclick="SelDisease(this)"' + '>' + item.diseaseName + '</li>';
                            //}
                        });
                        $("#ulDiseaseList").html(listHtml);
                        HasLoadDisease = true;
                    }
                } //
            }
            else {
                //myLayer.alert("获取陪诊超时,请稍后再试VB" + stackUUID);
            } // objJson为空
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        	localLoadRemove('ulDiseaseList');
            //myLayer.alert('网络繁忙,请刷新后重试VE' + stackUUID);
        }
    });
}
//选中疾病
function SelDisease(obj) {
    $("#cmb_diseasename").text(obj.innerText);
    $("#cmb_disease").val($(obj).data('id'));
    $('#diseaseShow').hide();
    $('#ghDiv').show();
}

///////发送验证码
function SendYzm() {
    var tel = $('#telephon').val();
    if (tel == "") {
        myLayer.alert('请填写手机号');
        return;
    }
    if($('#liGraphAuth').length>0 && $('#liGraphAuth').css('display')!='none'){
        if ($('#graphAuthInput').val() == '') {
            myLayer.confirm({
                title : '提示',
                con : '请先输入正确的图形验证码',
                okValue : "确定"
            });
            return;
        }
        if(!CheckValidateCode()){
    		myLayer.confirm({ title: '提示', con: '图形验证码不正确或已失效', okValue: "确定"});
    		return;
    	}
    }
    $.ajax({
        url: '../do/registerAuth/sendYzm',
        timeout: 8000,
        dataType: "json",
        type: "POST",
        async: true,
        cache: false,
        data: {
            tel: tel,
            accountSn:loginUserInfo.accountSn,
        },
        beforeSend: function () {
            myLayer.load('正在加载');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myLayer.clear();
            myLayer.alert('网络繁忙,请刷新后重试KE' + stackUUID);
        },
        success: function (json) {
            myLayer.clear();
            if (json) {
                if (json.Code == 10000) {
                    GetValidCode();
                    myLayer.alert('验证码已发送');
                }
                else {
                    myLayer.alert(json.Message);
                }
            }
            else {
                myLayer.alert('网络繁忙,请求失败KA' + stackUUID);
            }
        }
    });
}
//校验验证码
function CheckYzm() {
    if ($('#liYzmCode').css('display')=='none' || $("#valCode").length < 1) {
        return true;
    }
    var ret = false;
    var tel = $('#telephon').val();
    var valCode = $('#valCode').val();
    $.ajax({
        url: '../do/registerAuth/checkYzm',
        timeout: 8000,
        dataType: "json",
        type: "POST",
        async: false,
        cache: false,
        data: {
            tel: tel,
            yzmCode: valCode,
            accountSn: loginUserInfo.accountSn
        },
        beforeSend: function () {
            myLayer.load('正在加载');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            myLayer.clear();
            myLayer.alert('网络繁忙,请刷新后重试LE' + stackUUID);
        },
        success: function (json) {
            myLayer.clear();
            if (json) {
                if (json.Code == 10000) {
                    ret = true;
                }
            }
            else {
                myLayer.alert('网络繁忙,请求失败LA' + stackUUID);
            }
        }
    });
    return ret;
}
var sendTiem;  //倒计时变量
var restSendTiem = 60;  //重新获取验证码时间
var setintName;
var sendValidCodeControl = "getValidCode";
///发送手机验证60倒计时
function GetValidCode() {
    //$('#' + sendValidCodeControl).unbind('click');
    $('#' + sendValidCodeControl).attr('onclick', "");
    sendTiem = restSendTiem;
    ReduceTime();
    setintName = setInterval(ReduceTime, 1000);
}
///倒计时
function ReduceTime() {
    sendTiem--;
    if (sendTiem > 0) {
        $('#' + sendValidCodeControl).html(sendTiem + "s");
    }
    else {
        $('#' + sendValidCodeControl).html("发送验证码");
        clearInterval(setintName);
        $('#' + sendValidCodeControl).attr('onclick', 'SendYzm()');
        $('#' + sendValidCodeControl).attr("disabled", false);
    }
}
//////发送验证码end

//实名验证身份证信息/0失败，1成功，2绑定
function AuthIdCard(authGuidian, showAlert) {
    if (authGuidian == null && showAlert == null) {
        return 1;
    }
    var ret = 1;
    var AuthMemInfo;
    if (authGuidian == 1) { //验证监护人
        if (DoctorRegOrder.guardianmembersn == "" || DoctorRegOrder.guardianmembersn == "0") {
            AuthConfig.AddGuardian();
            return 2;
        }
        AuthMemInfo = AuthConfig.GetGuardian();
    }
    else {
        AuthMemInfo = DoctorRegOrder;
    }

    $.ajax({
        url: '../do/registerAuth/idCardAuth',
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: false,//
        data: {
            name: AuthMemInfo.memberName,
            memberSn: AuthMemInfo.memberSn
        },
        beforeSend: function () {
            myLayer.load('加载中...');
        },
        success: function (rejson) {
            myLayer.clear();
            if (rejson) {
                if (rejson.Code != 10000) {
                    ret = 0;
                }
                else {
                    AuthConfig.AuthResult[AuthMemInfo.memberSn] = 1;
                    ret = 1;
                }
            }
        },
        error: function () {
            myLayer.clear();
        }
    });

    if (!ret && showAlert) {

        myLayer.confirm({ title: '提示', con: '实名认证未通过，您可到个人中心-常用就诊人里进行身份验证，通过后才能使用挂号服务', okValue: "确定"});
        $("#surebtn").click(function () {
            $("#openDiv").empty();
        });
    }
    return ret;
}

//认证功能配置
var AuthConfig = {
    ////0102：身份验证，0103：银行卡验证, 0104: 照片验证, 0105: 人脸识别，0106：人脸对比，0107：手机实名验证
    ////1:短信验证码，2:图形验证码
    //已载入认证项js
    //LoadJsForItem: [],
    //开关
    OnOff: 0,
    //认证检查内容
    AuthJson: null,
    //选择认证项显示
    AuthWaitChkShow: [],
    //校验验证码
    AuthYzm:false,
    //错误信息
    Error: '',
    //初始化载入js
    InitAuth: function () {
        var auth = this;
        $.ajax({
            url: '../do/registerAuth/queryAuthOnoff',
            timeout: 8000,
            type: "POST",
            dataType: "json",
            cache: true,
            data: {
            	hospitalId: hospitalId,
                deptId: deptId,
                doctorSn: doctorSn
            },
            beforeSend: function () {
            },
            success: function (rejson) {
                var loadJs = false;
                if (rejson) {
                    if (rejson.Code == 10000) { //成功
                        for (var i in rejson.Result) {
                            if (rejson.Result[i].onOff == 1) {
                                if (!loadJs) {
                                    $.ajaxSetup({ async: false, cache: true });
                                    $.getScript("../js/sha.js");
                                    $.getScript("../js/sign.js");
                                    $.getScript("../js/register/authIdCardPhoto.js?v=20210609");
                                    $.getScript("https://res.wx.qq.com/open/js/jweixin-1.6.0.js");
                                    $.getScript("../js/register/authTelRealName.js");
                                    //$.getScript("../js/register/authBankCard.js");
                                    $.getScript("../js/register/authValidateCode.js?v=3");
                                    $.ajaxSetup({ async: true, cache: false });
                                    loadJs = true;
                                    auth.OnOff = 1;
                                }
                                if (rejson.Result[i].ooCode == '0101') {
                                	auth.AuthYzm =true;
                                	auth.ShowYzmInput();
                                }
                                if (rejson.Result[i].ooCode == '0108') {
                                	GraphAuth();
                                    ShowMoreInfo($('.editinto'), true);
                                }

                                if (auth.OnOff == 1&& $('#waitChkTip').length<1) {
                                    var tipHtml = '<div id="waitChkTip" class="tip">\
                                             <i class="iconfont icon-xiaolaba"></i>\
                                                   <p>医院已开通实名认证，请确保就诊人与证件一致，如儿童无身份证，请将出生日期改为儿童本人的。</p>\
                                                   </div>';
                                    $('#ghDiv').children().eq(0).before(tipHtml);
                                }
                            }
                        }

                    }
                }
            },
            error: function () {
            }
        });

    },
    //显示验证码输入
    ShowYzmInput:function (){
    	var tel = $('#telephon').val();
    	 if(!this.AuthYzm || tel == null || tel ==''){
    		 return;
    	 }
    	 $.ajax({
             url: '../do/registerAuth/checkYzmHistory',
             timeout: 8000,
             type: "POST",
             dataType: "json",
             async: true,//
             cache: true,
             data: {
            	 tel: tel,
            	 accountSn: loginUserInfo.accountSn
             },
             beforeSend: function () {
             },
             success: function (rejson) {
                 if (rejson && rejson.Code == 10000 ) {
               	      $('#liYzmPhone').hide();
                      $('#liYzmCode').hide();
                 }
                 else{
                      $('#liYzmPhone').show();
                      $('#liYzmCode').show();
                      ShowMoreInfo($('.editinto'), true);
                 }
             },
             error: function () {
            	 $('#liYzmPhone').show();
                 $('#liYzmCode').show();
                 ShowMoreInfo($('.editinto'), true);
             }
         });
    },
    //获取成员认证项
    CheckMemAuth: function (flag) {
        if (flag == null) flag = true;
        var auth = this;
        auth.AuthJson = null;
        auth.AuthWaitChkShow = [];
        auth.Error = ''; //-1 年龄获取失败
        $.ajax({
            url: '../do/registerAuth/queryAuthOnoff_v2',
            timeout: 8000,
            type: "POST",
            dataType: "json",
            async: flag,//
            cache: true,
            data: {
            	hospitalId: hospitalId,
                deptId: deptId,
                doctorSn: doctorSn,
                memberSn: DoctorRegOrder.memberSn,
                cardType: DoctorRegOrder.identitytype,
                cardNo: DoctorRegOrder.memberZhengjianid,
                birthday: DoctorRegOrder.memberBirthday
            },
            beforeSend: function () {
            },
            success: function (rejson) {
                if (rejson) {
                    if (rejson.Code == 10000) { //成功
                        auth.AuthJson = rejson.Result;
                        for (var i in auth.AuthJson.waitChk) {
                            if (auth.AuthJson.waitChk[i] != '0102' && auth.AuthJson.waitChk[i] != '0103') { //!=身份实名验证
                                auth.AuthWaitChkShow.push(auth.AuthJson.waitChk[i]);
                            }
                        }
                    }
                    else if (rejson.Code == -16400003) {
                        auth.Error = '-1';
                    }
                }
            },
            error: function () {
            }
        });
    },
    //显示验证选择窗
    ShowAuthWin: function () {
        if (this.AuthJson.waitChk.length == 0) {
            if (this.AuthJson.allow == 0) {
            	myLayer.confirm({ title: '提示', con: '该院只允许居民身份证预约', okValue: "确定"});
                return false;
            }
            else {
                return true;
            }
        }
        var MustRealNameAuth = this.AuthJson.waitChk.indexOf('0102') >= 0;  //需实名验证
        var authGuardian = (this.AuthJson.isChild == 1 && this.AuthJson.childChk == '2' ? '1' : '0');     //1认证监护人
        if (this.AuthResult[DoctorRegOrder.memberSn] == 1) {
            return true;
        }
        if (authGuardian == '1' && this.AuthResult[DoctorRegOrder.guardianmembersn] == 1) {
            return true;
        }
        var authItem = this.AuthWaitChkShow;
        if (MustRealNameAuth) {
            var showAlert = (authItem.length == 0 ? true : false);
            var authIdc = AuthIdCard(authGuardian, showAlert);
            if (authIdc == 1) {
                return true;
            }
            else if (authIdc == 0 && showAlert) {
                return false;
            }
            else if (authIdc == 2) {
                return false;
            }
        }
        if (authItem == null || authItem.length == 0) {
            return true;
        }
        else if (authItem.length == 1) {
            return this.AuthFun(authItem[0], authGuardian);
        }
        else {
            return this.ShowAuthSelectWin(authItem, authGuardian);
        }
    },
    //显示验证选择窗Html
    ShowAuthSelectWin: function (authItem, authGuardian) {
        $('#actionSheet-wrap').remove();
        var authHtml = '<div id="actionSheet-wrap">\
		<div class="doc-mask-transition doc-fade-toggle" id="mask"></div>\
		<div class="doc-actionsheet " id="doc-actionsheet">\
			<p class="ptb10 c-f15 plr15">请进行身份验证</p>\
			<div class="photo-notice ptb10 plr15 c-f12">\
				小贴士：该院要求身份验证，不进行身份验证无法继续预约挂号，请你配合，选择以下任意一个方式验证。验证通过后，下一次无需再次验证\
			</div>\
			<div class="pm_mess_new validation-list"><ul>';

        if (authItem.indexOf('0103') >= 0) {
            authHtml += '<li data-type="0103" data-authguard="' + authGuardian + '">\
						<div class="pmn_tit c-14a9ec "><i class="iconfont icon-yinhangqiayanzheng c-f28 v-h-m"></i></div>\
						<div class="pmn_con">\
							银行卡验证\
							<span class="rightjt arrow-right"></span>\
						</div>\
					</li>';
        }
        if (authItem.indexOf('0104') >= 0) {
            authHtml += '<li data-type="0104" data-authguard="' + authGuardian + '">\
						<div class="pmn_tit c-feae20 "><i class="iconfont icon-zhaopianyanzheng c-f28 v-h-m"></i></div>\
						<div class="pmn_con">\
							身份证照片验证\
							<span class="rightjt arrow-right"></span>\
						</div>\
					</li>';
        }
        if (authItem.indexOf('0105') >= 0) {
            authHtml += '<li data-type="0105">\
							<div class="pmn_tit c-02cfb9 "><i class="iconfont icon-renlianshibie c-f28 v-h-m"></i></div>\
							<div class="pmn_con">\
								人脸识别验证\
								<span class="rightjt arrow-right"></span>\
							</div>\
						</li>';
        }
        if (authItem.indexOf('0106') >= 0) {
			authHtml += '<li data-type="0106">\
							<div class="pmn_tit c-17b3ec "><i class="iconfont icon-renlianduibi c-f28 v-h-m"></i></div>\
							<div class="pmn_con">\
								人脸对比验证\
								<span class="rightjt arrow-right"></span>\
							</div>\
						</li>';
         }
         if (authItem.indexOf('0107') >= 0) {
             authHtml += '<li data-type="0107" data-authguard="' + authGuardian + '">\
							<div class="pmn_tit c-ff8100 "><i class="iconfont icon-shoujishiming c-f28 v-h-m"></i></div>\
							<div class="pmn_con">\
								手机实名验证\
								<span class="rightjt arrow-right"></span>\
							</div>\
						</li>';
        }

        authHtml += '</ul></div></div></div>';
        $('body').append(authHtml);

        var mask = $('#mask');
        var weuiActionsheet = $('#doc-actionsheet');
        showValidation();

        $(".validation-list").on("click", "li", function () {
            //获取身份验证类型
            var type = $(this).data("type");
            var authGuard = $(this).data("authguard");
            if (type != undefined) {
                hideActionSheet(weuiActionsheet, mask);
                //显示身份验证内容
                AuthConfig.AuthFun(type, authGuard);
            }
        });

        //显示浮层
        function showValidation() {
            weuiActionsheet.addClass('doc-actionsheet-toggle');
            mask.show().addClass('doc-fade-toggle').one('click', function () {
                hideActionSheet(weuiActionsheet, mask);
            });
            //$('#actionsheet-cancel').one('click', function () {
            //    hideActionSheet(weuiActionsheet, mask);
            //});
            weuiActionsheet.unbind('transitionend').unbind('webkitTransitionEnd');
        }
        //隐藏浮层
        function hideActionSheet(weuiActionsheet, mask) {
            weuiActionsheet.removeClass('doc-actionsheet-toggle');
            mask.removeClass('doc-fade-toggle');
            weuiActionsheet.on('transitionend', function () {
                mask.hide();
            }).on('webkitTransitionEnd', function () {
                mask.hide();
            })
        }
    },
    //认证功能
    AuthFun: function (aType, authGuard) {
        if (authGuard == "1" && DoctorRegOrder.guardianmembersn == "") {
            this.AddGuardian();
            return false;
        }
        switch (aType) {
            case ('0103'):
                $("#ghDiv").hide();
                return BankCardAuth.ShowAuthBankCardWin(authGuard);
            case ('0104'):
                $("#ghDiv").hide();
                return IdCardPhotoAuth.ShowAuthPhotoWin(authGuard);
            case ('0105'):
            case ('0106'):
                if ($('#face-tip').length < 1) {
                    var appTipHtml = '<div id="face-tip" class="face pop c-hide">\
			             <div class="auth-pop">\
            	                <div class="atp-tit">提示</div>\
                                <div class="atp-con mt10">该院挂号需进行人脸验证，公众号不支持此种验证方式，请到APP进行验证，给您造成的不便，敬请原谅。</div>\
                                <div class="atp-btn mt20"><a href="https://at.umeng.com/onelink/01PDSr" class=" c-btn c-btn-full c-btn-4dcd70 c-f15 border-radius-rounded">点击下载APP</a></div>\
                            </div>\
                            <div class="auth-mask"></div>\
		                </div>';
                    $('body').append(appTipHtml);
                    //关闭人脸提示
                    $('.auth-mask').click(function () {
                        $(this).parent().hide();
                    });
                }
                $("#face-tip").show();
                return false;
            case ('0107'):
                $("#ghDiv").hide();
                return AuthTelRealName.ShowAuthTelWin(authGuard);
            default:
                return true;
        }
    },
    //显示防刷验证
    ShowAuthAntiBrush: function () {
        if (this.AuthJson.antiBrushChk.indexOf(2) >= 0) {  //图形验证码
	    	 if (!CheckValidateCode()) {
	          	myLayer.confirm({ title: '提示', con: '图形验证码不正确或已失效', okValue: "确定"});
	            return false;
	         }
        }
        if (this.AuthJson.antiBrushChk.indexOf(1) >= 0) { //短信验证码
            if (!CheckYzm()) {
            	myLayer.confirm({ title: '提示', con: '短信验证码不正确或已失效', okValue: "确定"});
                return false;
            }
        }
        return true;
    },
    //认证后保存状态AuthResult[membersn]
    AuthResult: new Object(),
    //监护人
    GetGuardian: function () {
        var GuardianInfo = new Object();
        var gMsn = DoctorRegOrder.guardianmembersn;
        if (gMsn != null && gMsn != '') {
            var item = null;
            for(var i=0;i<MemList.length;i++){
            	if(MemList[i].Membersn == gMsn){
            		item = MemList[i];
            		break;
            	}
            }
            if(item!=null){
	            var memsn = item.Membersn;
	            GuardianInfo.memberSn = memsn;                 //成员sn
	            GuardianInfo.memberName = item.Cname;          //成员姓名
	            GuardianInfo.memberPhone = item.Phone;         //成员电话
	            GuardianInfo.memberIdcard = item.Idcard;       //成员身份证
	            GuardianInfo.memberAddress = item.Address;     //成员地址
	            GuardianInfo.memberBirthday = item.Birthday;   //成员生日
	            GuardianInfo.memberSex = item.Sex;             //成员性别
	            GuardianInfo.memberZhengjianid = item.Zhengjianid;     //成员证件ID
	            GuardianInfo.identitytype = item.Identitytype;         //成员证件(新)
	            GuardianInfo.memberid = item.Memberid;            //成员ID
	            GuardianInfo.othercard = item.Othercard;          //医保卡
	            GuardianInfo.guardianmembersn = item.Guardianmembersn;  //监护人sn
            }
        }
        return GuardianInfo;
    },
    //绑定监护人
    AddGuardian: function () {
        myLayer.confirm({
            title: '添加监护人',
            con: '您选择的成员为儿童成员，请为该成员选择一个监护人，点击"添加监护人"按钮添加。',
            cancelValue: "取消",
            cancel: function () { },
            okValue: "添加监护人",
            ok: function () {
            	var fromUrl = encodeURIComponent(location.href);
            	var url = "addGuardian.html?PlatformType=" + platformType + "&msn=" + DoctorRegOrder.memberSn +'&from=' + fromUrl ;
                location.href = url;
            }
           });
        return;
    }
}
