var b = "0123456789abcdefghijklmnopqrstuvwxyz";

function c(a) {
  return b.charAt(a);
}

function Decode(a) {
  var d,
    g,
    b = "",
    e = 0,
    f = 0;
  for (d = 0; d < a.length && a.charAt(d) != k; ++d) {
    (g = j.indexOf(a.charAt(d))),
      0 > g ||
        (0 == e
          ? ((b += c(g >> 2)), (f = 3 & g), (e = 1))
          : 1 == e
          ? ((b += c((f << 2) | (g >> 4))), (f = 15 & g), (e = 2))
          : 2 == e
          ? ((b += c(f)), (b += c(g >> 2)), (f = 3 & g), (e = 3))
          : ((b += c((f << 2) | (g >> 4))), (b += c(15 & g)), (e = 0)));
  }
  return 1 == e && (b += c(f << 2)), b;
}
