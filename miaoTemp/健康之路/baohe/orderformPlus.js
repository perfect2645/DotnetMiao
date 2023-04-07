GET https://appoint.yihu.com/appoint/js/register/orderFormPlus.js?v=20210609 HTTP/1.1
Host: appoint.yihu.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)
Accept: */*
Cookie: YiHu_OpenId=eyJPcGVuSUQiOiJvOGVfODBZb196X1RaVmRGRzE4V0l4MjRkSTVJIiwiU2VjU3RyIjoiMUFFQ0FGQkUxMDA3MkYzNzUwMEMxMjdDMjJFQjc5MDIifQ%3D%3D; logintype=62; loginid=o8e_80Yo_z_TZVdFG18WIx24dI5I; OpenID=o8e_80Yo_z_TZVdFG18WIx24dI5I; LoginChannel=9001026; TOKEN_FF92E02EA4CF7703D9822F4F8653931A=CD65CE0480E0410E98DA89C89CAD3D5F; _YyghSysTrackUUID=19122812629; jkzlAn_uuid=08212D24-6F80-476B-9345-61E89AA82CE6; jkzlAn_channelid=9001026; jkzlAn_userid=149169340; jkzlAn_p=-1; jkzlAn_c=-1; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDkxNjkzNDAiLCJDYXJkTnVtYmVyIjoiMjA3OTI3MjEwOSIsIkxvZ2luSWQiOiJvOGVfODBZb196X1RaVmRGRzE4V0l4MjRkSTVJIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI4QkFEQzY5MUJCRUQ0QUY4MDJBRjFGMzQxNTU0MjAzMiJ9; jkzlAn_sid=C3F99B72-E68C-4227-BF44-D8764F28A8C3; jkzlAn_utm_source=0.0.h.1026.bus010.0__0.0.h.1026.bus010.0; jkzlAn_ct=1660910406827
Sec-Fetch-Site: same-origin
Sec-Fetch-Mode: no-cors
Sec-Fetch-Dest: script
Referer: https://appoint.yihu.com/appoint/register/registerOrder.html?platformType=9001026&hospitalId=1047063&deptId=7229244&doctorSn=711188785&arrangeId=160450472&utm_source=0.0.h.1026.bus010.0
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7


HTTP/1.1 200 OK
Server: nginx
Date: Fri, 19 Aug 2022 12:02:26 GMT
Content-Type: application/javascript
Connection: keep-alive
Vary: Accept-Encoding
ETag: W/"10964-1660894731000"
Last-Modified: Fri, 19 Aug 2022 07:38:51 GMT
Content-Length: 10964


var loadPacsClinicCard = '';
//获取检查单项目信息
function GetPacsApplyInfo() {
    //K52409495
    if (!HasPacsServer) {
        return;
    }
    var clinicCard = GetInputCliniccard();
    if (clinicCard != '' && loadPacsClinicCard == clinicCard) {
        return;
    }
    $('#liPacsItem').replaceWith('');
    loadPacsClinicCard = '';
    BindNumberWaterList([]);
    $('#numWatUl').html("<li>暂无号源，请确认就诊卡和检查项是否正确！</li>");
    if (clinicCard == '' || clinicCard.length < 5) {
        return;
    }
    $.ajax({
    	url: "../do/registerInfo/getPacsApplyInfo",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        data: {
            clinicCard: clinicCard,
            phone : DoctorRegOrder.memberPhone,
            memberSn:DoctorRegOrder.memberSn,
            hospitalId : hospitalId
        },
        beforeSend: function () {
        },
        success: function (objJson) {
            var html = '';
            if (objJson && objJson.Code == 10000) {
                if (objJson.Data.length > 0){
                    html = '<li id="liPacsItem"><div class="pmn_tit c-333"><i class="iconfont icon-zhifu mr5"></i>检查项</div> <div class="pmn_con c-909090"><select id="selPacsItem"  name="ghFormCon" MassTxt="检查项必选" class="pminput" ><option value="" >请选择</option>';
                    $.each(objJson.Data, function (i, item) {
                        html += '<option value="' + item.applyNo + '" AppliedDepartment="' + item.AppliedDepartment + '" >' + item.itemName + '</option>';
                    });
                    html += '</select><span class="seljt arrow-right"></span></div></li>';
                }
                loadPacsClinicCard = clinicCard;
            }
            else {
                loadPacsClinicCard = '';
            }
            if ($('#liPacsItem').length > 0) {
                $('#liPacsItem').replaceWith(html);
            }
            else {
                $('#cardliMust').after(html);
            }
            if ($('#liPacsItem').length > 0) {
                $("#selPacsItem").on("change", function () {
                    var appDept = $("option:selected", this).attr('AppliedDepartment');
                    var applyNo = $("option:selected", this).val();
                    if (appDept != null && appDept != '') {
                        GetNumberWaterList(3, appDept,applyNo);
                    }
                    else {
                        BindNumberWaterList([]);
                    }
                });
            }
        },
        error: function () {
        }
    });
}
var accountManageOpen = false;
//查询账号管理配置
function getAccountManageConfig(){
	if(channelId!='1000007' && channelId!='1000025' && channelId !='1000006'){
		return;
	}
	$.ajax({
    	url: "../do/registerInfo/getAccountManageConfig",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        cache: true,
        data: {
        	provinceId: doctorInfo.provinceId,
            hospitalId : hospitalId
        },
        beforeSend: function () {
        },
        success: function (objJson) {
            if (objJson && objJson.Code == 10000) {
            	accountManageOpen = true;
            }
        },
        error: function () {
        }
    });
}
//检查账户激活状态
function checkMemActivationState(changeMem){
	if(changeMem){
		DoctorRegOrder.accManageGoodSn = null;
		if(selMemTimes<2){
			return true;
		}
	}
	if(!accountManageOpen || DoctorRegOrder.accManageGoodSn != null){
		return true;
	}
	var actState = false;
	$.ajax({
    	url: "../do/registerInfo/checkMemActivationStateAndEnablePkgs",
        timeout: 15000,
        type: "POST",
        dataType: "json",
        async: false,
        cache: true,
        data: {
        	accountSn : loginUserInfo.accountSn,
        	memberSn: DoctorRegOrder.memberSn
        },
        beforeSend: function () {
        },
        success: function (objJson) {
            if (objJson && objJson.Code == 10000) {
            	if(objJson.Activationstate ==1){
            		actState = true;
            		DoctorRegOrder.accManageGoodSn = '0';
            		$('#divShowOpenAccManage').html('');
            	}
            	else{
            		var item = objJson.Result[0];
            		showAccountManage(item);
            	}
            }
        },
        error: function () {
        }
    });
	return actState;
}
//显示开通选项
function showAccountManage(item){
var name = DoctorRegOrder.memberName;
var serName = item.Goodsname;
var serFee = item.Price/100;
var tradegoodssn = item.Tradegoodssn;
var healthManaUrl = healthManageUrl();
var html =
   '<div id="divAccountManage" class="acna-hold">\
	<div class="acna-mask"></div>\
	<div class="acna-pop">\
			<a href="javascript:;" class="acna-close"><i class="iconfont icon-close_icon"></i></a>\
			<div class="acp-tit">\
				<h4>您的成员【'+ name +'】</h4>\
				<p>尚未开通账户管理，无法挂号</p>\
			</div>\
			<div class="acp-pre">\
				<h4><span class="c-bold mr5">家庭健康管家服务</span> <span class="c-ff8100"></span></h4>\
				<p>免5个成员账户管理费，同时可享受一对一健康管家服务、就医指导、医生候补、放号提醒等服务特权<a href="'+ healthManaUrl +'" class="acp-more">了解更多<i class="iconfont icon-jiantouyou1"></i></a></p>\
				<a href="'+ healthManaUrl +'" class="acp-open">立即开通</a>\
			</div>\
			<div class="acp-pre">\
				<h4><span class="c-bold mr5">'+ serName +'</span> <span class="c-ff8100">¥'+ serFee +'/月</span></h4>\
				<p>每人每月'+ serFee +'元，可享受成员健康档案，就诊信息管理，身份证实名认证，数据存储等一系列账户服务</p>\
				<a href="javascript:showOpenAccManage(\''+ tradegoodssn +'\','+ serFee +');" class="acp-open bgc-4dcd70">立即开通</a>\
			</div>\
			<div class="acp-gife"><img src="../images/a-gife.png" alt="" />注册新账户首个成员免账户管理费 ~</div>\
			<div class="mt10"><a class="acp-share"><i class="iconfont icon-fenxiang2"></i>分享</a></div>\
	</div>\
   </div>';
   $('#divAccountManage').replaceWith(html);
   saveSiteTrack("bus009btn1012",'');
	//管家服务
	$('.acp-share').click(function(){
		$('.scode-hold').show();
		saveSiteTrack("bus009btn1013",'');
	});
	$('.scode-close').click(function(){
		$('.scode-hold').hide();
	});
	$('.acna-close').click(function(){
		$('.acna-hold').hide();
	});
   $('#acnaChoo').click(function(){
       $('.acna-hold').show();
   });
}
//显示选择开通
function showOpenAccManage(tradegoodssn,serFee){
	var name = DoctorRegOrder.memberName;
	$('.acna-hold').hide();
	var html =
	'<div id="divShowOpenAccManage" class="pm_mess_new c-border-tb mt10">\
    <div class="pntop" style="display:-webkit-box;" id="acnaChoo">\
        <div>您已选择开通'+ name +'账户管理服务</div>\
        <div class="pmn_con c-ff8100 c-t-right pr15">\
            <span>¥'+ serFee +'元/月</span>\
        </div>\
      </div>\
     </div>';
	$('#divShowOpenAccManage').replaceWith(html);
	DoctorRegOrder.accManageGoodSn = tradegoodssn;
	saveSiteTrack("bus009btn1014",'');
}
//会员服务地址
function healthManageUrl(){
	var fromUrl = encodeURIComponent(location.href);
	var url = StrUserUrl + '/UserCenter/web/vip/m/index.html?platformType='+ platformType +'&sourceId=0&sourceType=0&from='+ fromUrl +'&referCode=yw_hyfw_1059&utm_source=0.0.h.1001.bus009.0';
    return url;
}
//医院验证
function checkMore(flag){
	if(checkcode){//省妇幼
		if(flag=='PassCodeCaptcha'){
			return true;
		}
		getCodeCaptcha(false);
		return false;
	}else{
		return true;
	}
}
function getCodeCaptcha(isRefresh){
	$.ajax({
    	url: "../do/registerAuth/getCodeCaptcha",
        timeout: 30000,
        type: "POST",
        dataType: "json",
        cache: true,
        async: true,
        data: {
            hospitalId : hospitalId,
            name : DoctorRegOrder.memberName,
            clinicCard:DoctorRegOrder.cliniccard
        },
        beforeSend: function () {
        	myLayer.clear();
        	myLayer.load("验证码加载中...");
        },
        success: function (objJson) {
        	myLayer.clear();
        	if(objJson){
	            if (objJson.Code == 10000) {
	            	var data = objJson.Data;
	            	if(!isRefresh){
	            	  var ghl = '<div id="graphAuth" class=graph-hold>' +
  			          '<div class="auth-pop">' +
  			          '<div class="atp-tit">图形验证</div>' +
  			          '<div class="atp-con mt15">' +
  			          '<div class="atp-graph">' +
  			          '<input id="graphAuthInput" type="text" class="graph-text" placeholder="请输入验证码" />' +
  			          '<a href="javascript:;"><img id="refreshCodeCaptcha" data-codeid="'+(isNotNull(objJson.ID)?objJson.ID:'')+'" onclick=\'getCodeCaptcha('+true+')\' src="'+ data +'" /></a>' +
  			          '</div>' +
  			          '<div class="atp-tip mt10 c-hide"><i id="grapthAuthMsg" class="iconfont icon-buzhengque"></i></div>' +
  			          '</div>' +
  			          '<p id="capMsg" class="c-red plr15 pt10 c-f11"></p>' +
  			          '<div class="atp-btn mt20"><a href="javascript:comfirmCodeCaptcha();" class=" c-btn c-btn-full c-btn-4dcd70 c-f15 border-radius-rounded">立即预约</a></div>' +
  			          '</div>' +
  			          '<div class="auth-mask"></div>' +
  			          '</div>';
  			          $('body').append(ghl);
  			          $('.auth-mask').click(function () {
  			             $(this).parent().remove();
  			          });
	            	}else{
                        $('#refreshCodeCaptcha').attr('data-codeid',(isNotNull(objJson.ID)?objJson.ID:''));
                        $('#refreshCodeCaptcha').attr('src',data);
	            		$('#graphAuthInput').val('');
	            	}
	            }
	            else{
                    ConfDialog('提示', objJson.Message, '确定');
                    $("#surebtn").click(function () {
                        $("#openDiv").empty();
                        $('.pop_screen').hide();
                    });
	            }
        	}
        },
        error: function () {
        	myLayer.clear();
        	ConfDialog('提示','加载验证码超时，请重试！', '确定');
            $("#surebtn").click(function () {
                $("#openDiv").empty();
                $('.pop_screen').hide();
            });
        }
    });
}
function comfirmCodeCaptcha(){
	var checkCode = $.trim($('#graphAuthInput').val());
	if(checkCode.length!=4)
    {
	    $('#capMsg').text('请输入4位验证码');
	    return;
	}
	if(hospitalId==22351){
        var codeId=$('#refreshCodeCaptcha').attr('data-codeid');
        DoctorRegOrder.CheckCode = codeId+','+checkCode;
    }else{
        DoctorRegOrder.CheckCode = checkCode;
    }
	$('.auth-mask').parent().hide();
    SubOrder('PassCodeCaptcha');
}





