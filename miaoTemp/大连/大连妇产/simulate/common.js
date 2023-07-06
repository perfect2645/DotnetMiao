var requestResult = {};
var req =
  '{"appId":"wxa794c2a4fcfeb7f4","openId":null,"hospId":10018,"hospitalId":10018}';

a = t;
function t(e, a) {
  var n = f();
  return (t = function (e, a) {
    return n[(e -= 207)];
  })(e, a);
}
!(function (e, a) {
  for (var n = t, o = f(); ; )
    try {
      if (
        685483 ===
        parseInt(n(267)) / 1 +
          -parseInt(n(292)) / 2 +
          parseInt(n(265)) / 3 +
          parseInt(n(297)) / 4 +
          parseInt(n(287)) / 5 +
          parseInt(n(272)) / 6 +
          -parseInt(n(245)) / 7
      )
        break;
      o.push(o.shift());
    } catch (e) {
      o.push(o.shift());
    }
})();
var nGlobalData = {
  globalData: {
    appid: "wxa794c2a4fcfeb7f4",
    appointmentCancleTmpl: "XkV9bhx48huOPgvXtkZXRsi3xhruS0yFCME9FXkc9Jo",
    appointmentSuccessTmpl: "HwY-pBZH7gSSE6txEgyUDErtKnrkDtK51letwiPACDk",
    baseCaseUrl: "https://ghsf.dletyy.com",
    baseUrl: "https://hlwyy.dlfeyljt.com/patient/v1",
    bindingCardTmpl: "1c4EcGS6kDrDDOZvrfOmMW719lPfqMNFcFSyMSmkelc",
    cloudLongTmpl: "-DNbE99GuHRXbIs1ZA5ozWMhQzdoLl2ES6_qEgQFCnc",
    compoundName: "",
    consultLongTmpl: "-DNbE99GuHRXbIs1ZA5ozWMhQzdoLl2ES6_qEgQFCnc",
    escortReviewTmpl: "",
  },
}; // o = requirePlugin(a(207)),
// r = require(a(220));
function miniRequest(e, t, o) {
  var r = a;
  (t[r(318)] = nGlobalData.globalData.appid),
    (t[r(277)] = nGlobalData[r(279)].openId),
    (t.hospId = nGlobalData[r(279)][r(306)]),
    (t[r(306)] = nGlobalData[r(279)].hospitalId);
  var i,
    s,
    c,
    u,
    f,
    g = new Date()[r(231)](),
    h = 5,
    v = r(248),
    w = nGlobalData[r(279)][r(246)],
    x = d(),
    m = e[r(301)]("/"),
    I = nGlobalData[r(279)][r(323)][r(301)]("/"),
    y = m[r(312)]("d3"),
    S = I.indexOf("v1"),
    b = (function (e, t, n, o) {
      var r = a,
        s = "";
      o >= 0
        ? ((c = a),
          (u = ["Ycde", c(258), c(239), c(317), "czO", c(336)]),
          (s = (u = u[c(283)](1, 5))[c(299)]("")))
        : (s = l());
      var c, u;
      var d = e[r(315)](5),
        f = sm3index(req);
      return sm3index(s + e + d + f);
    })(x, 0, t, y);
  y >= 0
    ? ((w = 0), (h = 99))
    : S > 0 &&
      ((i = t),
      (s = a),
      // (c = require("sm4_index.js")),
      // require(s(238)),
      (u = p()),
      (f = JSON.stringify(i)),
      (t = {
        requestData: encrypt(f, u),
      }),
      (enc = t));
  requestResult.request = enc;
  requestResult.timestamp = g;
  requestResult.nonce = x;
  return requestResult;
  var k,
    q,
    D,
    T,
    P = "";
  nGlobalData[r(279)][r(244)]
    ? ((k = w),
      (q = a),
      (D = require(q(327))),
      require(q(238)),
      (T = p()),
      console[q(274)](q(324), T),
      (P = 0 == k ? "" : D[q(303)](k, T)))
    : (P = w),
    console[r(274)](r(314), P),
    wx[r(219)]({
      url: nGlobalData.globalData.baseUrl + e,
      data: t,
      header: {
        "content-type": r(255),
        timestamp: g,
        appType: h,
        appMark: "1",
        appKind: 1,
        appVersion: v,
        nonce: x,
        signature: b,
        hospitalId: nGlobalData[r(279)][r(306)],
        userId: P,
        token: nGlobalData[r(279)].token ? nGlobalData[r(279)].token : "",
        appId: nGlobalData[r(279)].appid,
      },
      method: r(252),
      dataType: r(305),
      success: function (e) {
        var a = r;
        if (-3 == e.data[a(266)])
          return (
            wx[a(264)]({
              key: a(309),
              data: "2",
            }),
            wx[a(289)](),
            void wx[a(250)]({
              url: a(326),
            })
          );
        o(0, e);
      },
      fail: function (e) {
        o(1, e);
      },
    });
}

function d() {
  for (var e = a, t = [], n = e(280), o = 0; o < 36; o++)
    t[o] = n[e(251)](Math.floor(16 * Math[e(290)]()), 1);
  return (
    (t[14] = "4"),
    (t[19] = n[e(251)]((3 & t[19]) | 8, 1)),
    (t[8] = t[13] = t[18] = t[23] = "-"),
    t.join("").replace("-", "")
  );
}
function l() {
  var e = a,
    t = [
      "PGyMh",
      "Ev",
      e(241),
      e(291),
      "XB23hw8w",
      "Pw",
      e(240),
      e(213),
      e(333),
      e(328),
      e(208),
      "GyTsHpMh",
    ];
  return (t = t[e(283)](1, 11))[e(299)]("");
}
function p() {
  var e = a,
    t = [e(281), e(325), e(282), e(296), "522a3433", e(234), "ic190"];
  return (t = t[e(283)](1, 6)).join("");
}
function f() {
  var e = [
    "txSig",
    "/pages/personal_center/health_card_login/healthCardLogin?path=",
    "6689442JtHbKJ",
    "getPatientsResp",
    "log",
    "/common/getHospConfSrv",
    "sm3_index.js",
    "openId",
    "getSetting",
    "globalData",
    "0123456789abcdef",
    "ed367",
    "696f4c",
    "slice",
    "onCheckForUpdate",
    "数据上传中",
    "getInfo",
    "5823930JuXlYU",
    "?jsonParm=",
    "hideLoading",
    "random",
    "QXqhNr",
    "2091132HkRgVE",
    "hospitalName",
    "currentDto",
    "url",
    "6e5659",
    "2315904OvlFeq",
    "none",
    "join",
    "新版本已经准备好，是否重启应用？",
    "split",
    "h5SignInResp",
    "encrypt",
    "code",
    "json",
    "hospitalId",
    "&jsonParm=",
    "onUpdateFailed",
    "loginStatus",
    "forEach",
    "decrypt",
    "indexOf",
    "wechatCode",
    "userIdEncry",
    "substring",
    "parse",
    "RhYqRedo=K7JvcuyjpbyppnZr7qQGs21JQsTNSp5TJm",
    "appId",
    "appid",
    "msg",
    "data",
    "scope.userLocation",
    "baseUrl",
    "signKey",
    "3879",
    "/pages/login/login/login?from=noLogin",
    "sm4_index.js",
    "TsHpNxR9",
    "txAccountId",
    "/pages/personal_center/add_health_card/addHealthCard?path=",
    "result",
    "patientId",
    "BFKJTHGzBX",
    "getStorageSync",
    "健康卡登录接口res",
    "Tyrcs",
    "&back=",
    "showModal",
    "file",
    "showToastTime",
    "needunionId",
    "/pages/login/login/login?path=",
    "healthCard",
    "PGyMhErNEvAu",
    "authSetting",
    "uploadFile",
    "reverseGeocoder",
    "onUpdateReady",
    "AHNqipm",
    "&wechatCode=",
    "token",
    "获取位置信息失败，请授权后再试",
    "showToast",
    "当前配置",
    "request",
    "qqmap-wx-jssdk.min.js",
    "type",
    "setStorageSync",
    "/common/uploadFileToServer",
    "stringify",
    "/pages/personal_center/add_patient/addPatient?path=",
    "boolean",
    "checkSession",
    "isEleHealthCard",
    "times",
    "getUpdateManager",
    "getTime",
    "/login/openIdFromWxCallBack",
    "length",
    "266f2e33",
    "patients",
    "phoneNo",
    "unionid",
    "Base64.js",
    "xPe9iO{WXsAxZM",
    "73xHz",
    "Aucy",
    "location",
    "unionId",
    "encryptionUserId",
    "23393972SVALfZ",
    "userId",
    "miniRequest",
    "1.0",
    "未配置",
    "navigateTo",
    "substr",
    "POST",
    "exports",
    "openid",
    "application/json;charset=UTF-8",
    "qqmapKey",
    "login",
    "NBME",
    "confirm",
    "errcode",
    "showLoading",
    "/other/getNoticeMessages",
    "/login/getOpenId",
    "setStorage",
    "3051279JQvxXe",
    "status",
    "1197283NbpLFp",
    "srvKey",
    "qqmapSigKey",
  ];
  return (f = function () {
    return e;
  })();
}
