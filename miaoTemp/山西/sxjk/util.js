var VUE_APP_DEPLOY_CITY;
var MD5_KEY = "2~s^}0app1[md5s7";
var sKey_app = "shen=new#su@app-";

window.axios && (axios.defaults.withCredentials = true);

function encrypt(word) {
  var key = CryptoJS.enc.Utf8.parse(sKey_app); // 16位
  var encrypted = "";
  if (typeof word === "string") {
    var srcs = CryptoJS.enc.Utf8.parse(word);
    encrypted = CryptoJS.AES.encrypt(srcs, key, {
      mode: CryptoJS.mode.ECB,
      padding: CryptoJS.pad.Pkcs7,
    });
  } else if (typeof word === "object") {
    // 对象格式的转成json字符串
    data = JSON.stringify(word);
    var srcs = CryptoJS.enc.Utf8.parse(data);
    encrypted = CryptoJS.AES.encrypt(srcs, key, {
      mode: CryptoJS.mode.ECB,
      padding: CryptoJS.pad.Pkcs7,
    });
  }
  return encrypted.toString();
}
function decrypt(word) {
  var key = CryptoJS.enc.Utf8.parse(sKey_app);
  word = CryptoJS.enc.Base64.parse(word).toString();
  var encryptedHexStr = CryptoJS.enc.Hex.parse(word);
  var srcs = CryptoJS.enc.Base64.stringify(encryptedHexStr).toString(
    CryptoJS.enc.Utf8
  );
  var decrypt = CryptoJS.AES.decrypt(srcs, key, {
    mode: CryptoJS.mode.ECB,
    padding: CryptoJS.pad.Pkcs7,
  });
  var decryptedStr = decrypt.toString(CryptoJS.enc.Utf8);
  var result = decryptedStr.toString().replace(/\u0000/g, "");
  try {
    return JSON.parse(result);
  } catch (e) {
    return result;
  }
}

var backfunction;
window.onload = function () {
  document.body.addEventListener(
    "touchmove",
    function (e) {
      // e.preventDefault(); //阻止默认的处理方式(阻止下拉滑动的效果)
    },
    { passive: false }
  );
};
function setDate(picker, timestr) {
  picker.setSelectedValue(timestr);
}
function setmuidatafocus() {
  var buttons = document.getElementsByTagName("button");
  for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("touchstart", function () {
      setBlur();
    });
    buttons[i].addEventListener("click", function () {
      setBlur();
    });
  }
  var inputs = document.getElementsByTagName("input");
  for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener("click", function () {
      setBlur(this);
      var that = this;
      setTimeout(function () {
        that.scrollIntoViewIfNeeded(true);
      }, 200);
    });
    inputs[i].addEventListener("touchstart", function () {
      setBlur(this);
      var that = this;
      setTimeout(function () {
        that.scrollIntoViewIfNeeded(true);
      }, 200);
    });
  }
  var textareas = document.getElementsByTagName("textarea");
  for (var i = 0; i < textareas.length; i++) {
    textareas[i].addEventListener("click", function () {
      setBlur(this);
      var that = this;
      setTimeout(function () {
        that.scrollIntoViewIfNeeded(true);
      }, 200);
    });
    textareas[i].addEventListener("touchstart", function () {
      setBlur(this);
      var that = this;
      setTimeout(function () {
        that.scrollIntoViewIfNeeded(true);
      }, 200);
    });
  }

  var arr = document.getElementsByTagName("ons-icon");
  for (var i = 0; i < arr.length; i++) {
    arr[i].addEventListener("touchstart", function () {
      setBlur();
    });
    arr[i].addEventListener("click", function () {
      setBlur();
    });
  }
  var disables = document.querySelectorAll("input:disabled");
  for (var i = 0; i < disables.length; i++) {
    disables[i].addEventListener("touchstart", function () {
      setBlur();
    });
    disables[i].addEventListener("click", function () {
      setBlur();
    });
  }
}

function setinputBlur(obj) {
  var inputs = document.getElementsByTagName("input");
  // alert("inputs失焦")
  for (var i = 0; i < inputs.length; i++) {
    if (inputs[i] != obj) {
      inputs[i].blur();
    }
  }
}
function compressImage(file, callback, maxsize) {
  if (!maxsize) {
    maxsize = 128 * 1024;
  }
  var size = file.size;
  if (size <= maxsize) {
    var reads = new FileReader();
    reads.readAsDataURL(file);
    reads.onload = function () {
      callback(this.result);
    };
    return;
  }
  var quality = Math.ceil((size / maxsize) * 1000) / 1000;
  var reads = new FileReader();
  reads.readAsDataURL(file);
  reads.onload = function () {
    pictureCompress({
      img: this.result,
      width: 400,
      height: 400,
      quality: quality,
    }).then(function (res) {
      callback(res.img);
    });
  };
}
function settextareaBlur(obj) {
  var textareas = document.getElementsByTagName("textarea");
  // alert("textareas失焦")
  for (var i = 0; i < textareas.length; i++) {
    if (textareas[i] != obj) {
      textareas[i].blur();
    }
  }
}
function setBlur(obj) {
  var inputs = document.getElementsByTagName("input");
  for (var i = 0; i < inputs.length; i++) {
    if (inputs[i] != obj) {
      inputs[i].blur();
    }
  }
  var textareas = document.getElementsByTagName("textarea");
  for (var i = 0; i < textareas.length; i++) {
    if (textareas[i] != obj) {
      textareas[i].blur();
    }
  }
}

// 上线修改
/*
 * 山东（8）
 **   省
      - h5       sdxg
 **   日照
      - h5       SDRZJMBWebAPP
      - weixin   SDWXJMBWebAPP
 **   烟台
      - weixin   WXYTJMBWebAPP
 **   威海
      - weixin   WXWHJMBWebAPP
 **   青岛
      - weixin   QDWXJMBWebAPP
 * 山西省（67）
 **
      - zfb      ShanXiZfbJmbWeb
      - weixin   ShanXiWxJmbWeb
 * 沈阳
 **
      - weixin   WXLNJMBWebAPP
 */

//  dev开发 / test测试 / prod生产
var VUE_APP_DEPLOY_ENV = "prod";
//  SMART智慧医院 / ONECODE一码办（仅在 测试 时需要修改改区分，因为接口不同）
// 部署的城市（VUE_APP_DEPLOY_CITY取值看上面备注，同步修改 .env.xxx -> VUE_APP_DEPLOY_CITY |||| .env.xxx -> VUE_APP_API_BASE_URL取值看下面的判断里JMBWebAPPConfig.interfaceUrl）
// supId 正式环境  山东：8 ，海南：31，山西：67，沈阳：7      测试环境：31
// returnUrl 支付完成返回首页重新授权地址

VUE_APP_DEPLOY_CITY = "SXJKWX";

var JMBWebAPPConfig = { interfaceUrl: "", supId: "31", title: "金苗宝" };

/// ////////////////////// 本地开发 ////////////////////////////
if (VUE_APP_DEPLOY_ENV === "dev") {
  JMBWebAPPConfig.interfaceUrl = "http://localhost:17070/jmb";
  // JMBWebAPPConfig.interfaceUrl = 'http://localhost:17070/jmbsx'
}
/// ///////////////////////// 测试 /////////////////////////////////////
// 测试 - 山东省
else if (VUE_APP_DEPLOY_ENV === "test" && VUE_APP_DEPLOY_CITY === "sdxg") {
  JMBWebAPPConfig.supId = "8";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net/jmbNbapp";
}
// 测试 - 山西省 -  H5/WEIXIN
else if (
  VUE_APP_DEPLOY_ENV === "test" &&
  VUE_APP_DEPLOY_CITY === "ShanXiWxJmbWeb"
) {
  JMBWebAPPConfig.supId = "67";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net/jmbNbapp";
}
// 测试 - （日照 - H5 / WEIXIN） （烟台 - weixin）（威海 - weixin）（青岛 - weixin）（沈阳 - WEIXIN）
else if (
  VUE_APP_DEPLOY_ENV === "test" &&
  (VUE_APP_DEPLOY_CITY === "SDRZJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY === "SDWXJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY === "WXYTJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY === "WXWHJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY === "QDWXJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY === "WXLNJMBWebAPP")
) {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net:7714/jmb";
}
// 测试 - 健康山东公众号
else if (VUE_APP_DEPLOY_ENV === "test" && VUE_APP_DEPLOY_CITY === "WXJKSD") {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net:7714/jmb";
}
// 测试 - 通用版本
else if (
  VUE_APP_DEPLOY_ENV === "test" &&
  VUE_APP_DEPLOY_CITY === "WXSDTYJmbWeb"
) {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net:7714/jmb";
}
// 测试 - 通用版本（含非免规苗）
else if (
  VUE_APP_DEPLOY_ENV === "test" &&
  VUE_APP_DEPLOY_CITY === "WXSDTYFMJmbWeb"
) {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net:7714/jmb";
}
/// ///////////////////////// 正式 /////////////////////////////////////
// 正式 - 山东省
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "sdxg") {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "https://sdepi.sdcdc.cn/jmbshandong";
}
// 正式 - 日照 - H5
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "SDRZJMBWebAPP"
) {
  JMBWebAPPConfig.supId = "";
  // JMBWebAPPConfig.interfaceUrl = 'https://wjinmiao.com:8010/jmbshandong'
  JMBWebAPPConfig.interfaceUrl = "https://sdepi.sdcdc.cn/jmbshandong";
}
// 正式 - 日照 - WEIXIN
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "WXJKSD") {
  JMBWebAPPConfig.supId = "8";
  // JMBWebAPPConfig.interfaceUrl = 'https://sdepi.sdcdc.cn/jmbshandong'
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://sdepi.sdcdc.cn/jmbshandong";
}
// 正式 - 山东 - 通用 - WXSDTYJmbWeb
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "WXSDTYJmbWeb"
) {
  JMBWebAPPConfig.supId = "31"; // 测试环境用31，山东正式环境用8
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  JMBWebAPPConfig.interfaceUrl = "http://wjinmiao.net:7714/jmb"; // 测试环境
  // JMBWebAPPConfig.interfaceUrl = 'https://sdepi.sdcdc.cn/jmbshandong'//正式环境
}
// 正式 - https测试支付功能 - 通用 - testH5WXPAY
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "testH5WXPAY"
) {
  JMBWebAPPConfig.title = "金苗宝";
  JMBWebAPPConfig.supId = "31"; // 测试环境用31，山东正式环境用8
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  JMBWebAPPConfig.interfaceUrl = "https://wjinmiao.com/jmbsx"; // 测试环境
  // JMBWebAPPConfig.interfaceUrl = 'https://sdepi.sdcdc.cn/jmbshandong'//正式环境
}
// 正式 - 山东疾控微信公众号 - dist
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "dist") {
  JMBWebAPPConfig.supId = "8";
  // JMBWebAPPConfig.interfaceUrl = 'https://sdepi.sdcdc.cn/jmbshandong'
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://sdepi.sdcdc.cn/jmbshandong";
}
// 正式 -  旧的 每个城市的微信公众号 - SDWXJMBWebAPP
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "SDWXJMBWebAPP"
) {
  JMBWebAPPConfig.supId = "8";
  // JMBWebAPPConfig.interfaceUrl = 'https://sdepi.sdcdc.cn/jmbshandong'
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://sdepi.sdcdc.cn/jmbshandong";
}
// 正式 -  海南 - HNOneCodeJmbWeb
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "HNOneCodeJmbWeb"
) {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.title = "金苗宝";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl =
    "https://onecode-yimiao.digitalhainan.com.cn/jmbhainan";
}
// 正式 -  海南测试 - TEST
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "TEST") {
  JMBWebAPPConfig.supId = "31";
  JMBWebAPPConfig.interfaceUrl = "http://36.101.208.34:9103/jmbhn";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  // JMBWebAPPConfig.interfaceUrl = 'https://onecode-yimiao.digitalhainan.com.cn/jmbhainan'
}
// 正式 -  山西疾控 - SXJKWX
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "SXJKWX") {
  JMBWebAPPConfig.title = "预防接种服务";
  JMBWebAPPConfig.supId = "67";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://ymjz.sxcdc.cn/jmbshanxi";
}
// 正式 -  沈阳微信公众号 - WXLNJMBWebAPP
else if (VUE_APP_DEPLOY_ENV === "prod" && VUE_APP_DEPLOY_CITY === "WXSYWEB") {
  JMBWebAPPConfig.supId = "7";
  JMBWebAPPConfig.returnUrl = "";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://wjinmiao.com:8011/jmbliaoning";
}
// 正式 -  湖南长沙县微信公众号 - CSWXJMBWebAPP
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "CSWXJMBWebAPP"
) {
  JMBWebAPPConfig.title = "长沙县疫苗接种与查验证系统";
  JMBWebAPPConfig.supId = "";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  //  JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://yfjz.csxjkxs.com/jmbhunan";
}
// 正式 -  对接在星沙app - CSJMBWebAPP
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "CSJMBWebAPP"
) {
  JMBWebAPPConfig.title = "长沙县疫苗接种与查验证系统";
  JMBWebAPPConfig.supId = "";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'http://wjinmiao.net:7714/jmb'
  JMBWebAPPConfig.interfaceUrl = "https://wjinmiao.com/jmbhunan";
}
// 正式 -  对接海南智慧岛小程序 - haiNanSmartPro
else if (
  VUE_APP_DEPLOY_ENV === "prod" &&
  VUE_APP_DEPLOY_CITY === "haiNanSmartPro"
) {
  JMBWebAPPConfig.title = "预防接种";
  JMBWebAPPConfig.supId = "31";
  // JMBWebAPPConfig.interfaceUrl = 'http://127.0.0.1:17070'
  // JMBWebAPPConfig.interfaceUrl = 'https://wjinmiao.com/jmbsx'
  JMBWebAPPConfig.interfaceUrl = "https://hnmygh.hiunihealth.cn/jmbhainan";
}
document.title = JMBWebAPPConfig.title;

function GetQueryString(name) {
  var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

  var r = window.location.search.substr(1).match(reg);

  if (r != null) return unescape(r[2]);
  return null;
}
function axiosGet(args, method, fun) {
  if (!args) {
    args = {};
  }

  // for(var key in args) {
  //   args[key] = encodeURI(args[key]);
  // }
  // args.token=sessionStorage.getItem("token");
  if (!args.user_name) {
    args.user_name = sessionStorage.getItem("user") || "";
  }
  args.city_code = sessionStorage.getItem("citycode") || "";
  args.version_name = "6.6.0";
  args.os = "web";
  // 健康山东新增的参数
  // args.userId='F6DE5D57616EC150E6E49C933AB53C81';
  // args.timeStamp='7C37F68CE847249D20FE3E83E8AE4B44';
  args.userId = GetQueryString("userId");
  args.timeStamp = GetQueryString("timeStamp");
  // 加密后的
  let url = "";
  if (
    method == "/Encryption/Highlight/GetContentByPage" ||
    method == "/Encryption/Highlight/GetContentById" ||
    method == "/Encryption/Highlight/GetBaseType" ||
    method == "/Encryption/Highlight/uploadAspectTime"
  ) {
    url = "https://wjinmiao.com:9030/jmbcommon" + method; // 正式
    // url = 'http://221.224.159.213:9015/jmbcommon' + method //测试
    // url = 'http://221.224.159.210:8119/jmbhighlight' + method //本地
  } else {
    url = JMBWebAPPConfig.interfaceUrl + method;
  }
  let hasCredentials = true;
  if (
    method == "/Encryption/Highlight/GetContentByPage" ||
    method == "/Encryption/Highlight/GetContentById" ||
    method == "/Encryption/Highlight/GetBaseType" ||
    method == "/Encryption/Highlight/uploadAspectTime"
  ) {
    hasCredentials = false;
  } else {
    hasCredentials = true;
  }
  if (
    VUE_APP_DEPLOY_CITY === "CSWXJMBWebAPP" ||
    VUE_APP_DEPLOY_CITY == "CSJMBWebAPP"
  ) {
    if (
      method == "/Encryption/Child/SearchChildList" ||
      method == "/Encryption/Station/GetStationAreaList"
    ) {
      // args.city_code = '460202000000' //测试用
      args.city_code = "430121000000"; // 湖南长沙县正式用
    }
  }
  var params = "";
  for (var item in args) {
    params += item + "=" + args[item] + "&";
  }
  params = params.substr(0, params.length - 1);
  console.log("请求参数");
  console.log(params);
  var check = encrypt(params);

  axios
    .get(url, {
      // params: args,
      // 加密后的
      params: {
        parameters: check,
        sign: md5(check + MD5_KEY),
        timestamp: new Date().getTime(),
      },
      withCredentials: hasCredentials,
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
    })
    .then(function (response) {
      if (response.status == 200) {
        console.log("get成功");
        if (!response.data.result) {
          fun(response.data);
          return;
        }
        if (md5(response.data.result + MD5_KEY) != response.data.sign) {
          console.log("md5");
          ons.notification.alert("登录信息已失效，请重新登录", {
            title: "登录过期",
            buttonLabels: "重新登录",
            class: "customDialog",
            callback: function () {
              wx.closeWindow();
            },
          });
          return;
        }
        var data = decrypt(response.data.result);
        if (data.code == "9") {
          console.log("code9");
          ons.notification.alert("登录信息已失效，请重新登录", {
            title: "登录过期",
            buttonLabels: "重新登录",
            class: "customDialog",
            callback: function () {
              wx.closeWindow();
            },
          });
          return;
        }
        fun(data);
      } else {
        console.log("get失败");
        ons.notification.alert("访问错误,请检查网络", {
          title: "提示",
          buttonLabels: "重新登录",
          class: "customDialog",
          callback: function () {
            wx.closeWindow();
          },
        });
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}
function axiosPost(formData, method, fun) {
  if (formData == undefined) {
    formData = new FormData();
  }
  formData.append("user_name", sessionStorage.getItem("user") || "");
  formData.append("city_code", sessionStorage.getItem("citycode") || "");
  formData.append("timeout", 1000);
  formData.append("visit_flag", "web");

  var config = {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  };
  axios
    .post(JMBWebAPPConfig.interfaceUrl + method + "?os=web", formData, config)
    .then(function (response) {
      if (response.status == 200) {
        console.log("post成功");
        if (md5(response.data.result + MD5_KEY) != response.data.sign) {
          ons.notification.alert("登录信息已失效，请重新登录", {
            title: "登录过期",
            buttonLabels: "重新登录",
            class: "customDialog",
            callback: function () {
              wx.closeWindow();
            },
          });
          return;
        }
        var data = decrypt(response.data.result);
        if (data.code == "1" && data.message == "访问失败，请重新登录") {
          ons.notification.alert("登录信息已失效，请重新登录", {
            title: "登录过期",
            buttonLabels: "重新登录",
            class: "customDialog",
            callback: function () {
              wx.closeWindow();
            },
          });
          return;
        }
        fun(data);
      } else {
        console.log("post失败");
        ons.notification.alert("访问错误,请检查网络", {
          title: "提示",
          buttonLabels: "重新登录",
          callback: function () {
            wx.closeWindow();
          },
        });
      }
    })
    .catch(function (error) {
      console.log(error);
    });
}

// 判断是否是18位受种者编码
function isChildNo(val) {
  var reg = /^\d{18}$/;
  if (reg.test(val)) {
    return true;
  }
  return false;
}

// 判断是否是非负整数
function isNumber(val) {
  var reg = /^\d+$/;
  if (reg.test(val)) {
    return true;
  }
  return false;
}

// 身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
function isCardNo(card) {
  var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
  if (reg.test(card) === false) {
    return false;
  }
  return true;
}

// 判断是否是手机号码
function isMobileNumber(val) {
  var reg = /^1\d{10}$/;
  if (reg.test(val) === false) {
    return false;
  }
  return true;
}

// 判断是否是固定电话
function isTel(val) {
  var reg = /^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}$/;
  if (reg.test(val) === false) {
    return false;
  }
  return true;
}

// 判断是否是邮箱地址
function isEmail(val) {
  var reg = /^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/;
  if (reg.test(val) === false) {
    return false;
  }
  return true;
}
// 获取当前时间
function getNowFormatDate() {
  var date = new Date();
  var seperator1 = "-";
  var seperator2 = ":";
  var month = date.getMonth() + 1;
  var strDate = date.getDate();
  if (month >= 1 && month <= 9) {
    month = "0" + month;
  }
  if (strDate >= 0 && strDate <= 9) {
    strDate = "0" + strDate;
  }
  var currentdate =
    date.getFullYear() +
    seperator1 +
    month +
    seperator1 +
    strDate +
    " " +
    date.getHours() +
    seperator2 +
    date.getMinutes() +
    seperator2 +
    date.getSeconds();
  return currentdate;
}

// 获取当前日期
function getNowFormatDate2() {
  var date = new Date();
  var month = date.getMonth();
  var strDate = date.getDate();
  if (month >= 1 && month <= 9) {
    month = "0" + month;
  }
  if (strDate >= 0 && strDate <= 9) {
    strDate = "0" + strDate;
  }
  var currentdate = [];
  currentdate.push(date.getFullYear());
  currentdate.push(month);
  currentdate.push(strDate);
  return currentdate;
}

// 获取当前日期
function getNowFormatDate3() {
  var date = new Date();
  var month = date.getMonth() + 1;
  var strDate = date.getDate();
  var currentdate = [];
  currentdate.push(date.getFullYear());
  currentdate.push(date.getMonth() + 1);
  currentdate.push(date.getDate());
  return currentdate;
}

function getFormatDate(timestamp) {
  var date = new Date(parseInt(timestamp));
  var month = date.getMonth() + 1;
  var strDate = date.getDate();
  if (month >= 1 && month <= 9) {
    month = "0" + month;
  }
  if (strDate >= 0 && strDate <= 9) {
    strDate = "0" + strDate;
  }
  var currentdate = [];
  currentdate.push(date.getFullYear());
  currentdate.push(month);
  currentdate.push(strDate);
  return currentdate.join("-");
}
function parseAddress(address) {
  var result = ["", ""];
  if (address) {
    if (address.indexOf(".") > -1) {
      var i = address.lastIndexOf(".");
      /* if(address.substr(i, 1) == ".") {
				result[0] = address.substring(0, i-1).replace(/\./g, "");
			} else {
				result[0] = address.substring(0, i).replace(/\./g, "");
			} */
      result[0] = address.substring(0, i);
      result[1] = address.substring(i + 1);
      return result;
    }
    result[0] = address;
  }
  return result;
}

function coverArrayToString(arr) {
  var result = "";
  if (arr != null && arr.length > 0) {
    for (var i = 0; i < arr.length; i++) {
      if (i > 0) {
        result += ",";
      }
      result += arr[i];
    }
  }
  return result;
}

String.prototype.trim = function () {
  return this.replace(/(^\s*)|(\s*$)/g, "");
};

/* function parseBase64Data(imgUrls) {
	let urlArr = [];
	for(let i = 0; i < imgUrls.length; i++) {
		if(imgUrls[i].indexOf('file') == -1) {
			urlArr.push(imgUrls[i].split(',')[1]);
		} else {
			urlArr.push(imgUrls[i]);
		}
	}
	return urlArr;
}
*/

function dataURItoBlob(dataURI) {
  var byteString = atob(dataURI.split(",")[1]);
  var mimeString = dataURI.split(",")[0].split(":")[1].split(";")[0];
  var ab = new ArrayBuffer(byteString.length);
  var ia = new Uint8Array(ab);
  for (var i = 0; i < byteString.length; i++) {
    ia[i] = byteString.charCodeAt(i);
  }
  return new Blob([ab], { type: mimeString });
}

/* function byteToString(arr) {
    if(typeof arr === 'string') {
        return arr;
    }
    var str = '',
        _arr = arr;
    for(var i = 0; i < _arr.length; i++) {
        var one = _arr[i].toString(2),
            v = one.match(/^1+?(?=0)/);
        if(v && one.length == 8) {
            var bytesLength = v[0].length;
            var store = _arr[i].toString(2).slice(7 - bytesLength);
            for(var st = 1; st < bytesLength; st++) {
                store += _arr[st + i].toString(2).slice(2);
            }
            str += String.fromCharCode(parseInt(store, 2));
            i += bytesLength - 1;
        } else {
            str += String.fromCharCode(_arr[i]);
        }
    }
    return str;
}
*/

function removePoint(s) {
  if (s && s == ".") {
    s = "";
  }
  return s;
}

// 判断是否是安卓还是ios
function isAndroid_ios() {
  var u = navigator.userAgent;
  var app = navigator.appVersion;
  var isAndroid = u.indexOf("Android") > -1 || u.indexOf("Linux") > -1; // android终端或者uc浏览器
  var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); // ios终端
  return isAndroid == true;
}

/* if(!isWeixinBrowser()){
window.history.go(-1);
} */
function isWeixinBrowser() {
  var agent = navigator.userAgent.toLowerCase();
  if (agent.match(/MicroMessenger/i) == "micromessenger") {
    return true;
  } else {
    return false;
  }
}

//微信中打开
function isOpenInWeixin() {
  var ua = navigator.userAgent.toLowerCase();
  return ua.match(/MicroMessenger/i) == "micromessenger";
}
// 在支付宝中打开
function isOpenInAlipay() {
  var ua = navigator.userAgent.toLowerCase();
  return ua.match(/Alipay/i) == "alipay";
}
// 在app中打开
function isOpenInApp() {
  var ua = navigator.userAgent.toLowerCase();
  console.log("==============app中打开==============");
  console.log(ua);
  return ua.match(/myapp/i) == "myapp";
}
