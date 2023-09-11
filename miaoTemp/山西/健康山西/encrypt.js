var clearEncoding = "utf8";
var cipherEncoding = "base64";
var algorithm = "aes-128-cbc";
function decryptByAes(t) {
  const key = CryptoJS.enc.Utf8.parse("sxlkkjoefncxmggg"); // 十六位十六进制数作为密钥
  const iv = CryptoJS.enc.Utf8.parse("0682036802830600"); // 十六位十六进制数作为密钥偏移量

  const encryptedHexStr = CryptoJS.enc.Base64.parse(t);
  const srcs = CryptoJS.enc.Base64.stringify(encryptedHexStr);
  const decrypt = CryptoJS.AES.decrypt(srcs, key, {
    iv,
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7,
  });
  const decryptedStr = CryptoJS.enc.Utf8.stringify(decrypt);
  return decryptedStr.toString();
}
var encode = function (e) {
  var t = e;
  "string" != typeof e && (t = JSON.stringify(e));
  var i = CryptoJS.enc.Utf8.parse(t),
    o = CryptoJS.enc.Utf8.parse(r.key);
  return CryptoJS.AES.encrypt(i, o, {
    iv: CryptoJS.enc.Utf8.parse(iv),
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7,
  }).toString();
};
var objToBase64 = function (t) {
  return encodeURIComponent(e.from(JSON.stringify(t)).toString("base64"));
};
var base64ToObj = function (t) {
  return JSON.parse(e.from(decodeURIComponent(t), "base64").toString());
};

//"{"page_id":8189}"            ---    "7LgAHqwkZ/g3ETlBBxRFFz5Gpc6An1dQOrnk/QxCczM="
