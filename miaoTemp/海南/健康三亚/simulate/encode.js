import CryptoJS from "crypto-js";

var zoeParams = JSON.parse(
  decodeURIComponent(
    escape(
      atob(
        "eyJvcmdDb2RlIjoiIiwiYXBwQ29kZSI6IkhDX1NBTllBX1dYIiwiem9uZUNvZGUiOiI0NjAyMDAiLCJ6b25lTmFtZSI6IjVMaUo1THFhNWJpQyIsImhvc0NvZGUiOiIiLCJvcGVuSUQiOiJvR3FIYjA2UE9fTXFRcEdKZW9HbDBzdW9ZbHFvIiwidW5pb25JZCI6IiIsImxvbmdpdHVkZSI6IiIsImxhdGl0dWRlIjoiIiwiaXAiOiIxMDAuOTIuOTkuMTg0LCA1OS4xMDkuOTkuMTk3LCAxMjMuNjAuMjU0LjM0LCAxMjIuMTEyLjIwOC4yMzQsIDE3Mi4xNi4wLjY1IiwidGhlbWUiOiJibHVlIiwidG9rZW4iOiJwZmQ5ZTY2MzJlZDU2NTQyNzM5ZmVjYjBmNmEzZWQ5ZWQ2IiwiY3NyZlRva2VuIjoiIiwiY29ubmVjdFNpZCI6InM6ampISW9BOEtfNTRwVFZlTDRSUHJqa2NKa2NVTi1xSUMuN05nNkZ2TFFlcHQrQ0FEYU9rRUZlMVppZFBKY2IzbFQwNGRUR1RxV1pFNCIsInpvZVV1aWQiOiI1RDlBNzgwQjE5NjA0MzdDOEQwMjgwMEJFQTY0OTlCOCIsInZlcnNpb24iOiIzLjEwLjIyIiwiaGFzaCI6IjE2OTAzNDQwMzIzMjMiLCJidXJ5aW5nUG9pbnRTdGF0dXMiOiIxIiwiYnVyeWluZ1BvaW50UGFnZUlkVXBkYXRlVGltZSI6IjIwMjItMDItMjEiLCJ0cmFuc3BhcmVudENvbmZpZyI6W3sia2V5IjoiYnNjIiwidmFsdWUiOiJidXJ5aW5nUG9pbnRCU0MifSx7ImtleSI6InBtYyIsInZhbHVlIjoiYnVyeWluZ1BvaW50UE1DIn0seyJrZXkiOiJsZXZlbCIsInZhbHVlIjoidHJhbnNwYXJlbnRMZXZlbCJ9XSwiZGVidWdBdXRob3JpemUiOiIiLCJjaGVja0RhdGFNRDUiOiJGMEVCM0YyOUI2MURFN0ExMTU1MEQwQTdEQTBFM0M4QyJ9"
      )
    )
  )
);
for (var name in zoeParams) {
  console.log(name, zoeParams[name]);
  window[name] = zoeParams[name];
}

var testParam =
  "appCode=encryptFlag=1md5Check=80c813b346d2a6b66672dc852e1f4041password=dcc136a4e8fc2edea7b8e000b0e45f44thirdId=80c813b346d2a6b66672dc852e1f4041thirdType=e4ff24350d3bc3fc2b12783afd9e3ce6token=tokenId=username=22383c35a31f2a119fdfe446944f8df9zoeUuid=614832CD3ED643BEB84F6D23929DD2F2";

("appCode=HC_SANYA_WXparameterCode=OPEN_MY_PRE_BUTTON_ORGtoken=pfd9e6632ed56542739fecb0f6a3ed9ed6tokenId=pfd9e6632ed56542739fecb0f6a3ed9ed6zoeUuid=5D9A780B1960437C8D02800BEA6499B8");

var decryptByAes = function (t) {
  // var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "3132333435363738393041424344454631323334353637383930414243444566"
  //   , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : "30313233343536373839414243444546";
  let e = "3132333435363738393041424344454631323334353637383930414243444566";
  let n = "30313233343536373839414243444546";

  e = CryptoJS.default.enc.Hex.parse(e);
  var r = CryptoJS.default.enc.Hex.parse(n),
    i = CryptoJS.default.AES.decrypt(CryptoJS.default.format.Hex.parse(t), e, {
      iv: r,
      mode: CryptoJS.default.mode.CBC,
      padding: CryptoJS.default.pad.Pkcs7,
    }),
    o = CryptoJS.default.enc.Utf8.stringify(i);
  return o;
};

var decryptByDes = function (t) {
  // var e = arguments.length > 1 && void 0 !== arguments[1] ? arguments[1] : "2f6e34b30461730b62e50d64d0fd26132f6e34b30461730b"
  //   , n = arguments.length > 2 && void 0 !== arguments[2] ? arguments[2] : "79647779736f6674";
  let e = "2f6e34b30461730b62e50d64d0fd26132f6e34b30461730b";
  let n = "79647779736f6674";

  e = CryptoJS.default.enc.Hex.parse(e);
  var r = CryptoJS.default.enc.Hex.parse(n),
    i = CryptoJS.default.TripleDES.decrypt(
      CryptoJS.default.format.Hex.parse(t),
      e,
      {
        iv: r,
        mode: CryptoJS.default.mode.CFB,
        padding: CryptoJS.default.pad.NoPadding,
      }
    ),
    o = CryptoJS.default.enc.Utf8.stringify(i);
  return o;
};
