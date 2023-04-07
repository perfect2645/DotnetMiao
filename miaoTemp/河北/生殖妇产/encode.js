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




generateFullCheckCode4GET: function(a) {
  var b = d.getMemoryCache().get(CACHE_CONSTANTS.MEMORY_CACHE.TOKEN_4_FULL_CHECK)
    , c = f.SecurityUtils.md5(a.urlParamsString);
  return f.SecurityUtils.md5(c + "@@" + b)
},
generateFullCheckCode4POST: function(a) {
  var b = this
    , c = KyeeKit.keys(a.data).sort()
    , d = "";
  for (var e in c) {
      var f = c[e]
        , g = a.data[f];
      if (void 0 != g && null != g)
          if ("object" == typeof g) {
              var h = JSON.stringify(g);
              a.data[f] = h,
              d += f + "=" + h + "&"
          } else
              d += f + "=" + g + "&"
  }
  return d = d.substring(0, d.length - 1),
  a.urlParamsString = d,
  b.generateFullCheckCode4GET(a)
},