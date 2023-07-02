var r, t, n, o, a;
(r = [
  "A",
  "B",
  "C",
  "D",
  "E",
  "F",
  "G",
  "H",
  "I",
  "J",
  "K",
  "L",
  "M",
  "N",
  "O",
  "P",
  "Q",
  "R",
  "S",
  "T",
  "U",
  "V",
  "W",
  "X",
  "Y",
  "Z",
  "a",
  "b",
  "c",
  "d",
  "e",
  "f",
  "g",
  "h",
  "i",
  "j",
  "k",
  "l",
  "m",
  "n",
  "o",
  "p",
  "q",
  "r",
  "s",
  "t",
  "u",
  "v",
  "w",
  "x",
  "y",
  "z",
  "0",
  "1",
  "2",
  "3",
  "4",
  "5",
  "6",
  "7",
  "8",
  "9",
  "+",
  "/",
]),
  (t = function (r) {
    for (var t = new Array(); r > 0; ) {
      var n = r % 2;
      (r = Math.floor(r / 2)), t.push(n);
    }
    return t.reverse(), t;
  }),
  (n = function (r) {
    for (var t = 0, n = 0, o = r.length - 1; o >= 0; --o)
      1 == r[o] && (t += Math.pow(2, n)), ++n;
    return t;
  }),
  (o = function (r, t) {
    for (var n = 8 - (r + 1) + 6 * (r - 1) - t.length; --n >= 0; ) t.unshift(0);
    for (var o = [], a = r; --a >= 0; ) o.push(1);
    o.push(0);
    for (var e = 0, c = 8 - (r + 1); e < c; ++e) o.push(t[e]);
    for (var f = 0; f < r - 1; ++f) {
      o.push(1), o.push(0);
      for (var h = 6; --h >= 0; ) o.push(t[e++]);
    }
    return o;
  }),
  (a = {
    encoder: function (a) {
      for (var e = [], c = [], f = 0, h = a.length; f < h; ++f) {
        var s = a.charCodeAt(f),
          u = t(s);
        if (s < 128) {
          for (var v = 8 - u.length; --v >= 0; ) u.unshift(0);
          c = c.concat(u);
        } else
          s >= 128 && s <= 2047
            ? (c = c.concat(o(2, u)))
            : s >= 2048 && s <= 65535
            ? (c = c.concat(o(3, u)))
            : s >= 65536 && s <= 2097151
            ? (c = c.concat(o(4, u)))
            : s >= 2097152 && s <= 67108863
            ? (c = c.concat(o(5, u)))
            : s >= 4e6 && s <= 2147483647 && (c = c.concat(o(6, u)));
      }
      var l = 0;
      for (f = 0, h = c.length; f < h; f += 6) {
        var i = f + 6 - h;
        2 == i ? (l = 2) : 4 == i && (l = 4);
        for (var g = l; --g >= 0; ) c.push(0);
        e.push(n(c.slice(f, f + 6)));
      }
      var p = "";
      for (f = 0, h = e.length; f < h; ++f) p += r[e[f]];
      for (f = 0, h = l / 2; f < h; ++f) p += "=";
      return p;
    },
    decoder: function (o) {
      var a = o.length,
        e = 0;
      "=" == o.charAt(a - 1) &&
        ("=" == o.charAt(a - 2)
          ? ((e = 4), (o = o.substring(0, a - 2)))
          : ((e = 2), (o = o.substring(0, a - 1))));
      for (var c = [], f = 0, h = o.length; f < h; ++f)
        for (var s = o.charAt(f), u = 0, v = r.length; u < v; ++u)
          if (s == r[u]) {
            var l = t(u),
              i = l.length;
            if (6 - i > 0) for (var g = 6 - i; g > 0; --g) l.unshift(0);
            c = c.concat(l);
            break;
          }
      e > 0 && (c = c.slice(0, c.length - e));
      var p = [],
        A = [];
      for (f = 0, h = c.length; f < h; )
        if (0 == c[f]) (p = p.concat(n(c.slice(f, f + 8)))), (f += 8);
        else {
          for (var d = 0; f < h && 1 == c[f]; ) ++d, ++f;
          for (A = A.concat(c.slice(f + 1, f + 8 - d)), f += 8 - d; d > 1; )
            (A = A.concat(c.slice(f + 2, f + 8))), (f += 8), --d;
          (p = p.concat(n(A))), (A = []);
        }
      return p;
    },
  }),
  (module.exports = { CusBASE64: a });
