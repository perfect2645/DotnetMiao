var testParam =
  "appCode=encryptFlag=1md5Check=80c813b346d2a6b66672dc852e1f4041password=dcc136a4e8fc2edea7b8e000b0e45f44thirdId=80c813b346d2a6b66672dc852e1f4041thirdType=e4ff24350d3bc3fc2b12783afd9e3ce6token=tokenId=username=22383c35a31f2a119fdfe446944f8df9zoeUuid=614832CD3ED643BEB84F6D23929DD2F2";

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
