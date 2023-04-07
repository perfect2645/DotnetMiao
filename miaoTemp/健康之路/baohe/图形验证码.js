GET https://appoint.yihu.com/appoint/js/register/authValidateCode.js?v=3 HTTP/1.1
Host: appoint.yihu.com
Connection: keep-alive
Accept: text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01
User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)
X-Requested-With: XMLHttpRequest
Cookie: YiHu_OpenId=eyJPcGVuSUQiOiJvOGVfODBZb196X1RaVmRGRzE4V0l4MjRkSTVJIiwiU2VjU3RyIjoiMUFFQ0FGQkUxMDA3MkYzNzUwMEMxMjdDMjJFQjc5MDIifQ%3D%3D; logintype=62; loginid=o8e_80Yo_z_TZVdFG18WIx24dI5I; OpenID=o8e_80Yo_z_TZVdFG18WIx24dI5I; LoginChannel=9001026; TOKEN_FF92E02EA4CF7703D9822F4F8653931A=CD65CE0480E0410E98DA89C89CAD3D5F; _YyghSysTrackUUID=19122812629; jkzlAn_uuid=08212D24-6F80-476B-9345-61E89AA82CE6; jkzlAn_channelid=9001026; jkzlAn_userid=149169340; jkzlAn_p=-1; jkzlAn_c=-1; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDkxNjkzNDAiLCJDYXJkTnVtYmVyIjoiMjA3OTI3MjEwOSIsIkxvZ2luSWQiOiJvOGVfODBZb196X1RaVmRGRzE4V0l4MjRkSTVJIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI4QkFEQzY5MUJCRUQ0QUY4MDJBRjFGMzQxNTU0MjAzMiJ9; jkzlAn_sid=C3F99B72-E68C-4227-BF44-D8764F28A8C3; jkzlAn_utm_source=0.0.h.1026.bus010.0__0.0.h.1026.bus010.0; jkzlAn_ct=1660910412267
Sec-Fetch-Site: same-origin
Sec-Fetch-Mode: cors
Sec-Fetch-Dest: empty
Referer: https://appoint.yihu.com/appoint/register/registerOrder.html?platformType=9001026&hospitalId=1047063&deptId=7229244&doctorSn=711188785&arrangeId=160450472&utm_source=0.0.h.1026.bus010.0
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7


HTTP/1.1 200 OK
Server: nginx
Date: Fri, 19 Aug 2022 12:02:33 GMT
Content-Type: application/javascript
Connection: keep-alive
Vary: Accept-Encoding
ETag: W/"2480-1660894731000"
Last-Modified: Fri, 19 Aug 2022 07:38:51 GMT
Content-Length: 2480

//图形验证
function GraphAuth() {
//    if ($('#graphAuth').length < 1) {
//        var ghl = '<div id="graphAuth" class=graph-hold>' +
//            '<div class="auth-pop">' +
//            '<div class="atp-tit">图形验证</div>' +
//            '<div class="atp-con mt15">' +
//            '<div class="atp-graph">' +
//            '<input id="graphAuthInput" type="text" class="graph-text" placeholder="请输入验证码" />' +
//            '<a href="javascript:;"><img id="aa" onclick=\'$(this).attr("src",$(this).attr("src")+"1")\' src="../do/registerAuth/showVerifyCode?t=1" /></a>' +
//            '</div>' +
//            '<div class="atp-tip mt10 c-hide"><i id="grapthAuthMsg" class="iconfont icon-buzhengque"></i></div>' +
//            '</div>' +
//            '<div class="atp-btn mt20"><a href="javascript:CheckValidateCode();" class=" c-btn c-btn-full c-btn-4dcd70 c-f15 border-radius-rounded">立即预约</a></div>' +
//            '</div>' +
//            '<div class="auth-mask"></div>' +
//            '</div>';
//        $('body').append(ghl);
//        $('.auth-mask').click(function () {
//            $(this).parent().hide();
//        });
//    }
//    $('#graphAuth').show();
    var html ='<li id="liGraphAuth">\
	    <div class="pmn_tit c-333">图形验证：</div>\
	    <div class="pmn_con c-909090">\
	    	<input id="graphAuthInput" type="text" class="pminput" placeholder="请输入验证码" /></div>\
	    	<a href="javascript:;"><img id="aa" onclick=\'$(this).attr("src",$(this).attr("src")+"1")\' src="../do/registerAuth/showVerifyCode?t=1" class="sched-btn" /></a>\
	    </li>';
    $('#liGraphAuth').replaceWith(html);
}
//验证图形验证码
function CheckValidateCode() {
    if($('#liGraphAuth').length==0 || $('#liGraphAuth').css('display')=='none') {
         return true;
    }
    var ret = false;
    var inputCode = $('#graphAuthInput').val();
    if (inputCode == '') {
        return false;
    }
    DoctorRegOrder.graphAuthCode=inputCode;
    $.ajax({
        url: '../do/registerAuth/checkVerifyCode',
        timeout: 8000,
        type: "POST",
        dataType: "json",
        async: false,
        cache: false,
        data: {
            inputCode: inputCode
        },
        beforeSend: function () {
        },
        success: function (json) {
            if (json && json.Code==10000) {
            	ret = true;
            }
        },
        error: function () {
        }
    });
    return ret;
}