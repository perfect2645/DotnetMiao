const key = CryptoJS.enc.Utf8.parse("1234567890hijklm"); // 十六位十六进制数作为密钥
const iv = CryptoJS.enc.Utf8.parse("1234567890abcdef"); // 十六位十六进制数作为密钥偏移量
/* 加密方法 */
function myEncrypt(word) {
  const srcs = CryptoJS.enc.Utf8.parse(word);
  const encrypted = CryptoJS.AES.encrypt(srcs, key, {
    iv,
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7,
  });
  return CryptoJS.enc.Base64.stringify(encrypted.ciphertext);
}
/* 解密方法 */
function myDecrypt(word) {
  const encryptedHexStr = CryptoJS.enc.Base64.parse(word);
  const srcs = CryptoJS.enc.Base64.stringify(encryptedHexStr);
  const decrypt = CryptoJS.AES.decrypt(srcs, key, {
    iv,
    mode: CryptoJS.mode.CBC,
    padding: CryptoJS.pad.Pkcs7,
  });
  const decryptedStr = decrypt.toString(CryptoJS.enc.Utf8);
  return decryptedStr.toString();
}

function decryptByAes(t) {
  // var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "3132333435363738393041424344454631323334353637383930414243444566"
  //   , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : "30313233343536373839414243444546";
  let key = "3132333435363738393041424344454631323334353637383930414243444566";
  let n = "30313233343536373839414243444546";

  key = CryptoJS.enc.Hex.parse(key);
  var iv = CryptoJS.enc.Hex.parse(n),
    i = CryptoJS.AES.decrypt(CryptoJS.format.Hex.parse(t), key, {
      iv: iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    }),
    o = CryptoJS.enc.Utf8.stringify(i);
  return o;
}

function decryptByDes(t) {
  // var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "2f6e34b30461730b62e50d64d0fd26132f6e34b30461730b"
  //   , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : "79647779736f6674";
  let e = "2f6e34b30461730b62e50d64d0fd26132f6e34b30461730b";
  let n = "79647779736f6674";

  e = CryptoJS.enc.Hex.parse(e);
  var r = CryptoJS.enc.Hex.parse(n),
    i = CryptoJS.TripleDES.decrypt(CryptoJS.format.Hex.parse(t), e, {
      iv: r,
      mode: CryptoJS.mode.CFB,
      padding: CryptoJS.pad.NoPadding,
    }),
    o = CryptoJS.enc.Utf8.stringify(i);
  return o;
}

function encryptByAes(t) {
  let e = "3132333435363738393041424344454631323334353637383930414243444566";
  let n = "30313233343536373839414243444546";
  e = CryptoJS.enc.Hex.parse(e);
  var r = CryptoJS.enc.Hex.parse(n),
    i = CryptoJS.AES.encrypt(t, e, {
      iv: r,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    }),
    o = i.ciphertext.toString();
  return o;
}
