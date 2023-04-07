//add by zms
//add Date 2015-11-09
//设置cookie
function SetCookie(cname, cvalue, exdays) {
  var d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  var expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + "; " + expires + ";path=/";
}
//获取cookie
function GetCookie(cname) {
  var result = "";
  if (
    typeof cname !== "undefined" &&
    cname !== null &&
    $.trim(cname).length > 0
  ) {
    var cookie = document.cookie.match(
      new RegExp("(^| )" + cname + "=([^;]*)(;|$)")
    );

    if (
      typeof cookie !== "undefined" &&
      cookie !== null &&
      $.trim(cookie).length > 0
    ) {
      var value = unescape(cookie[2]);
      if (
        typeof value !== "undefined" &&
        value !== null &&
        $.trim(value).length > 0
      ) {
        result = value;
      }
    }
  }
  return result;
}
//清除cookie
function ClearCookie(cname) {
  SetCookie(cname, "", -1);
}
//设置cookie，带上域名
function SetDomainCookie(name, value, domain, exdays) {
  var d = new Date();
  var expires = new Date(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  value =
    name +
    "=" +
    value +
    ";expires=" +
    expires.toGMTString() +
    ";domain:" +
    domain;
  document.cookie = value;
}
//设置cookie
function SetCookiePath(cname, cvalue, exdays) {
  $.ajax({
    url: "/jhservice/SetCookie",
    type: "post",
    async: false,
    data: { name: cname, val: cvalue },
    success: function () {},
  });
}
//获取cookie
function GetCookiePath(cname) {
  var result = "";
  if (
    typeof cname !== "undefined" &&
    cname !== null &&
    $.trim(cname).length > 0
  ) {
    var cookie = document.cookie.match(
      new RegExp("(^| )" + cname + "=([^;]*)(;|$)")
    );

    if (
      typeof cookie !== "undefined" &&
      cookie !== null &&
      $.trim(cookie).length > 0
    ) {
      var value = cookie[2];
      if (
        typeof value !== "undefined" &&
        value !== null &&
        $.trim(value).length > 0
      ) {
        result = value;
      }
    }
  }
  return decodeURIComponent(result);
}
