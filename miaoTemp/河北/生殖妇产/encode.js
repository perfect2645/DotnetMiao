_data.isEncrypt && 1 == _data.isEncrypt) {
  var aesDecrypt = function(a) {
      var b = CryptoJS.enc.Utf8.parse("abcdefgabcdefg12")
        , c = CryptoJS.AES.decrypt(a, b, {
          mode: CryptoJS.mode.ECB,
          padding: CryptoJS.pad.Pkcs7
      });
      return CryptoJS.enc.Utf8.stringify(c).toString()
  };
  _data = JSON.parse(aesDecrypt(_data.result))
}