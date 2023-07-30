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

var md5SV = function () {
  var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {},
    e = {},
    t = JSON.parse(JSON.stringify(t));
  var n = "";
  for (var r in e) t[r] = e[r];
  var i = this.objKeySort(t);
  for (var o in i) n = "".concat(n).concat(o, "=").concat(t[o]);
  return s().MD5(n).toString().toUpperCase();
};

var objKeySort = function () {
  var t = arguments.length > 0 && void 0 !== arguments[0] ? arguments[0] : {};
  if (t instanceof Object && "[object Object]" === t.toString()) {
    var e = {},
      n = Object.keys(t).sort();
    return (
      n.map(function (r, i) {
        e[r] = t[n[i]];
      }),
      e
    );
  }
  return t;
};
1;
