var formatter = {
  stringify: function (e) {
    var t = e.ciphertext,
      n = e.salt;
    if (n) var i = o.create([1398893684, 1701076831]).concat(n).concat(t);
    else i = t;
    return i.toString(l);
  },
  parse: function (e) {
    var t = l.parse(e),
      n = t.words;
    if (1398893684 == n[0] && 1701076831 == n[1]) {
      var i = o.create(n.slice(2, 4));
      n.splice(0, 4), (t.sigBytes -= 16);
    }
    return g.create({
      ciphertext: t,
      salt: i,
    });
  },
};

var parse = function (e) {
  for (var t = e.length, n = [], i = 0; i < t; i++)
    n[i >>> 2] |= (255 & e.charCodeAt(i)) << (24 - (i % 4) * 8);
  return new a.init(n, t);
};

var encrypt = function (e, t, n, i) {
  i = this.cfg.extend(i);
  var r = e.createEncryptor(n, i),
    o = r.finalize(t),
    a = r.cfg;
  return g.create({
    ciphertext: o,
    key: n,
    iv: a.iv,
    algorithm: e,
    mode: a.mode,
    padding: a.padding,
    blockSize: e.blockSize,
    formatter: i.format,
  });
};
var decrypt = function (e, t, n, i) {
  (i = this.cfg.extend(i)), (t = this._parse(t, i.format));
  var r = e.createDecryptor(n, i).finalize(t.ciphertext);
  return r;
};

var m = function (e) {
  return (e = e.replace(/\+/gi, "-")), (e = e.replace(/\//gi, "_")), e;
};

var b = function (e) {
  if (null === e || "" === e || "undefined" === e) return null;
  var t = parse("imed2019imed2019"),
    n = encrypt(parse(e), t, {
      mode: s.a,
      padding: l.a,
    });
  return m(n.toString());
};
