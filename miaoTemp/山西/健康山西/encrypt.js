var key = "sxlkkjoefncxmggg";
var iv = "0682036802830600";
var clearEncoding = "utf8";
var cipherEncoding = "base64";
var algorithm = "aes-128-cbc";
var decode = function (e) {
  var t = n.enc.Utf8.parse(r.key);
  return n.AES.decrypt(e, t, {
    iv: n.enc.Utf8.parse(r.iv),
    mode: n.mode.CBC,
    padding: n.pad.Pkcs7,
  })
    .toString(n.enc.Utf8)
    .toString();
};
var encode = function (e) {
  var t = e;
  "string" != typeof e && (t = JSON.stringify(e));
  var i = n.enc.Utf8.parse(t),
    o = n.enc.Utf8.parse(r.key);
  return n.AES.encrypt(i, o, {
    iv: n.enc.Utf8.parse(r.iv),
    mode: n.mode.CBC,
    padding: n.pad.Pkcs7,
  }).toString();
};
var objToBase64 = function (t) {
  return encodeURIComponent(e.from(JSON.stringify(t)).toString("base64"));
};
var base64ToObj = function (t) {
  return JSON.parse(e.from(decodeURIComponent(t), "base64").toString());
};

//"{"page_id":8189}"            ---    "7LgAHqwkZ/g3ETlBBxRFFz5Gpc6An1dQOrnk/QxCczM="
