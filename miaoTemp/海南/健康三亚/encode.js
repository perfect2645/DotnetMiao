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
