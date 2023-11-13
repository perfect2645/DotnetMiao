require("../@babel/runtime/helpers/Arrayincludes");

var e = require("../@babel/runtime/helpers/typeof");

var encodeF = function (e, t) {
  var n =
    n ||
    (function (e, t) {
      var n = {},
        r = (n.lib = {}),
        o = function () {},
        i = (r.Base = {
          extend: function (e) {
            o.prototype = this;
            var t = new o();
            return (
              e && t.mixIn(e),
              t.hasOwnProperty("init") ||
                (t.init = function () {
                  t.$super.init.apply(this, arguments);
                }),
              (t.init.prototype = t),
              (t.$super = this),
              t
            );
          },
          create: function () {
            var e = this.extend();
            return e.init.apply(e, arguments), e;
          },
          init: function () {},
          mixIn: function (e) {
            for (var t in e) e.hasOwnProperty(t) && (this[t] = e[t]);
            e.hasOwnProperty("toString") && (this.toString = e.toString);
          },
          clone: function () {
            return this.init.prototype.extend(this);
          },
        }),
        a = (r.WordArray = i.extend({
          init: function (e, t) {
            (e = this.words = e || []),
              (this.sigBytes = null != t ? t : 4 * e.length);
          },
          toString: function (e) {
            return (e || c).stringify(this);
          },
          concat: function (e) {
            var t = this.words,
              n = e.words,
              r = this.sigBytes;
            if (((e = e.sigBytes), this.clamp(), r % 4))
              for (var o = 0; o < e; o++)
                t[(r + o) >>> 2] |=
                  ((n[o >>> 2] >>> (24 - (o % 4) * 8)) & 255) <<
                  (24 - ((r + o) % 4) * 8);
            else if (65535 < n.length)
              for (o = 0; o < e; o += 4) t[(r + o) >>> 2] = n[o >>> 2];
            else t.push.apply(t, n);
            return (this.sigBytes += e), this;
          },
          clamp: function () {
            var t = this.words,
              n = this.sigBytes;
            (t[n >>> 2] &= 4294967295 << (32 - (n % 4) * 8)),
              (t.length = e.ceil(n / 4));
          },
          clone: function () {
            var e = i.clone.call(this);
            return (e.words = this.words.slice(0)), e;
          },
          random: function (t) {
            for (var n = [], r = 0; r < t; r += 4)
              n.push((4294967296 * e.random()) | 0);
            return new a.init(n, t);
          },
        })),
        s = (n.enc = {}),
        c = (s.Hex = {
          stringify: function (e) {
            var t = e.words;
            e = e.sigBytes;
            for (var n = [], r = 0; r < e; r++) {
              var o = (t[r >>> 2] >>> (24 - (r % 4) * 8)) & 255;
              n.push((o >>> 4).toString(16)), n.push((15 & o).toString(16));
            }
            return n.join("");
          },
          parse: function (e) {
            for (var t = e.length, n = [], r = 0; r < t; r += 2)
              n[r >>> 3] |= parseInt(e.substr(r, 2), 16) << (24 - (r % 8) * 4);
            return new a.init(n, t / 2);
          },
        }),
        u = (s.Latin1 = {
          stringify: function (e) {
            var t = e.words;
            e = e.sigBytes;
            for (var n = [], r = 0; r < e; r++)
              n.push(
                String.fromCharCode((t[r >>> 2] >>> (24 - (r % 4) * 8)) & 255)
              );
            return n.join("");
          },
          parse: function (e) {
            for (var t = e.length, n = [], r = 0; r < t; r++)
              n[r >>> 2] |= (255 & e.charCodeAt(r)) << (24 - (r % 4) * 8);
            return new a.init(n, t);
          },
        }),
        f = (s.Utf8 = {
          stringify: function (e) {
            try {
              return decodeURIComponent(escape(u.stringify(e)));
            } catch (e) {
              throw Error("Malformed UTF-8 data");
            }
          },
          parse: function (e) {
            return u.parse(unescape(encodeURIComponent(e)));
          },
        }),
        l = (r.BufferedBlockAlgorithm = i.extend({
          reset: function () {
            (this._data = new a.init()), (this._nDataBytes = 0);
          },
          _append: function (e) {
            "string" == typeof e && (e = f.parse(e)),
              this._data.concat(e),
              (this._nDataBytes += e.sigBytes);
          },
          _process: function (t) {
            var n = this._data,
              r = n.words,
              o = n.sigBytes,
              i = this.blockSize,
              s = o / (4 * i);
            if (
              ((t =
                (s = t ? e.ceil(s) : e.max((0 | s) - this._minBufferSize, 0)) *
                i),
              (o = e.min(4 * t, o)),
              t)
            ) {
              for (var c = 0; c < t; c += i) this._doProcessBlock(r, c);
              (c = r.splice(0, t)), (n.sigBytes -= o);
            }
            return new a.init(c, o);
          },
          clone: function () {
            var e = i.clone.call(this);
            return (e._data = this._data.clone()), e;
          },
          _minBufferSize: 0,
        }));
      r.Hasher = l.extend({
        cfg: i.extend(),
        init: function (e) {
          (this.cfg = this.cfg.extend(e)), this.reset();
        },
        reset: function () {
          l.reset.call(this), this._doReset();
        },
        update: function (e) {
          return this._append(e), this._process(), this;
        },
        finalize: function (e) {
          return e && this._append(e), this._doFinalize();
        },
        blockSize: 16,
        _createHelper: function (e) {
          return function (t, n) {
            return new e.init(n).finalize(t);
          };
        },
        _createHmacHelper: function (e) {
          return function (t, n) {
            return new p.HMAC.init(e, n).finalize(t);
          };
        },
      });
      var p = (n.algo = {});
      return n;
    })(Math);
  (function () {
    var e = n,
      t = e.lib.WordArray;
    e.enc.Base64 = {
      stringify: function (e) {
        var t = e.words,
          n = e.sigBytes,
          r = this._map;
        e.clamp(), (e = []);
        for (var o = 0; o < n; o += 3)
          for (
            var i =
                (((t[o >>> 2] >>> (24 - (o % 4) * 8)) & 255) << 16) |
                (((t[(o + 1) >>> 2] >>> (24 - ((o + 1) % 4) * 8)) & 255) << 8) |
                ((t[(o + 2) >>> 2] >>> (24 - ((o + 2) % 4) * 8)) & 255),
              a = 0;
            4 > a && o + 0.75 * a < n;
            a++
          )
            e.push(r.charAt((i >>> (6 * (3 - a))) & 63));
        if ((t = r.charAt(64))) for (; e.length % 4; ) e.push(t);
        return e.join("");
      },
      parse: function (e) {
        var n = e.length,
          r = this._map,
          o = r.charAt(64);
        o && -1 != (o = e.indexOf(o)) && (n = o), (o = []);
        for (var i = 0, a = 0; a < n; a++)
          if (a % 4) {
            var s = r.indexOf(e.charAt(a - 1)) << ((a % 4) * 2),
              c = r.indexOf(e.charAt(a)) >>> (6 - (a % 4) * 2);
            (o[i >>> 2] |= (s | c) << (24 - (i % 4) * 8)), i++;
          }
        return t.create(o, i);
      },
      _map: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
    };
  })(),
    (function (e) {
      function t(e, t, n, r, o, i, a) {
        return (
          (((e = e + ((t & n) | (~t & r)) + o + a) << i) | (e >>> (32 - i))) + t
        );
      }
      function r(e, t, n, r, o, i, a) {
        return (
          (((e = e + ((t & r) | (n & ~r)) + o + a) << i) | (e >>> (32 - i))) + t
        );
      }
      function o(e, t, n, r, o, i, a) {
        return (((e = e + (t ^ n ^ r) + o + a) << i) | (e >>> (32 - i))) + t;
      }
      function i(e, t, n, r, o, i, a) {
        return (((e = e + (n ^ (t | ~r)) + o + a) << i) | (e >>> (32 - i))) + t;
      }
      for (
        var a = n,
          s = a.lib,
          c = s.WordArray,
          u = s.Hasher,
          f = ((s = a.algo), []),
          l = 0;
        64 > l;
        l++
      )
        f[l] = (4294967296 * e.abs(e.sin(l + 1))) | 0;
      (s = s.MD5 =
        u.extend({
          _doReset: function () {
            this._hash = new c.init([
              1732584193, 4023233417, 2562383102, 271733878,
            ]);
          },
          _doProcessBlock: function (e, n) {
            for (var a = 0; 16 > a; a++) {
              var s = n + a,
                c = e[s];
              e[s] =
                (16711935 & ((c << 8) | (c >>> 24))) |
                (4278255360 & ((c << 24) | (c >>> 8)));
            }
            (a = this._hash.words), (s = e[n + 0]), (c = e[n + 1]);
            var u = e[n + 2],
              l = e[n + 3],
              p = e[n + 4],
              d = e[n + 5],
              h = e[n + 6],
              v = e[n + 7],
              y = e[n + 8],
              g = e[n + 9],
              m = e[n + 10],
              _ = e[n + 11],
              b = e[n + 12],
              w = e[n + 13],
              O = e[n + 14],
              x = e[n + 15],
              S = a[0],
              $ = a[1],
              A = a[2],
              k = a[3];
            (S = t(S, $, A, k, s, 7, f[0])),
              (k = t(k, S, $, A, c, 12, f[1])),
              (A = t(A, k, S, $, u, 17, f[2])),
              ($ = t($, A, k, S, l, 22, f[3])),
              (S = t(S, $, A, k, p, 7, f[4])),
              (k = t(k, S, $, A, d, 12, f[5])),
              (A = t(A, k, S, $, h, 17, f[6])),
              ($ = t($, A, k, S, v, 22, f[7])),
              (S = t(S, $, A, k, y, 7, f[8])),
              (k = t(k, S, $, A, g, 12, f[9])),
              (A = t(A, k, S, $, m, 17, f[10])),
              ($ = t($, A, k, S, _, 22, f[11])),
              (S = t(S, $, A, k, b, 7, f[12])),
              (k = t(k, S, $, A, w, 12, f[13])),
              (A = t(A, k, S, $, O, 17, f[14])),
              (S = r(S, ($ = t($, A, k, S, x, 22, f[15])), A, k, c, 5, f[16])),
              (k = r(k, S, $, A, h, 9, f[17])),
              (A = r(A, k, S, $, _, 14, f[18])),
              ($ = r($, A, k, S, s, 20, f[19])),
              (S = r(S, $, A, k, d, 5, f[20])),
              (k = r(k, S, $, A, m, 9, f[21])),
              (A = r(A, k, S, $, x, 14, f[22])),
              ($ = r($, A, k, S, p, 20, f[23])),
              (S = r(S, $, A, k, g, 5, f[24])),
              (k = r(k, S, $, A, O, 9, f[25])),
              (A = r(A, k, S, $, l, 14, f[26])),
              ($ = r($, A, k, S, y, 20, f[27])),
              (S = r(S, $, A, k, w, 5, f[28])),
              (k = r(k, S, $, A, u, 9, f[29])),
              (A = r(A, k, S, $, v, 14, f[30])),
              (S = o(S, ($ = r($, A, k, S, b, 20, f[31])), A, k, d, 4, f[32])),
              (k = o(k, S, $, A, y, 11, f[33])),
              (A = o(A, k, S, $, _, 16, f[34])),
              ($ = o($, A, k, S, O, 23, f[35])),
              (S = o(S, $, A, k, c, 4, f[36])),
              (k = o(k, S, $, A, p, 11, f[37])),
              (A = o(A, k, S, $, v, 16, f[38])),
              ($ = o($, A, k, S, m, 23, f[39])),
              (S = o(S, $, A, k, w, 4, f[40])),
              (k = o(k, S, $, A, s, 11, f[41])),
              (A = o(A, k, S, $, l, 16, f[42])),
              ($ = o($, A, k, S, h, 23, f[43])),
              (S = o(S, $, A, k, g, 4, f[44])),
              (k = o(k, S, $, A, b, 11, f[45])),
              (A = o(A, k, S, $, x, 16, f[46])),
              (S = i(S, ($ = o($, A, k, S, u, 23, f[47])), A, k, s, 6, f[48])),
              (k = i(k, S, $, A, v, 10, f[49])),
              (A = i(A, k, S, $, O, 15, f[50])),
              ($ = i($, A, k, S, d, 21, f[51])),
              (S = i(S, $, A, k, b, 6, f[52])),
              (k = i(k, S, $, A, l, 10, f[53])),
              (A = i(A, k, S, $, m, 15, f[54])),
              ($ = i($, A, k, S, c, 21, f[55])),
              (S = i(S, $, A, k, y, 6, f[56])),
              (k = i(k, S, $, A, x, 10, f[57])),
              (A = i(A, k, S, $, h, 15, f[58])),
              ($ = i($, A, k, S, w, 21, f[59])),
              (S = i(S, $, A, k, p, 6, f[60])),
              (k = i(k, S, $, A, _, 10, f[61])),
              (A = i(A, k, S, $, u, 15, f[62])),
              ($ = i($, A, k, S, g, 21, f[63])),
              (a[0] = (a[0] + S) | 0),
              (a[1] = (a[1] + $) | 0),
              (a[2] = (a[2] + A) | 0),
              (a[3] = (a[3] + k) | 0);
          },
          _doFinalize: function () {
            var t = this._data,
              n = t.words,
              r = 8 * this._nDataBytes,
              o = 8 * t.sigBytes;
            n[o >>> 5] |= 128 << (24 - (o % 32));
            var i = e.floor(r / 4294967296);
            for (
              n[15 + (((o + 64) >>> 9) << 4)] =
                (16711935 & ((i << 8) | (i >>> 24))) |
                (4278255360 & ((i << 24) | (i >>> 8))),
                n[14 + (((o + 64) >>> 9) << 4)] =
                  (16711935 & ((r << 8) | (r >>> 24))) |
                  (4278255360 & ((r << 24) | (r >>> 8))),
                t.sigBytes = 4 * (n.length + 1),
                this._process(),
                n = (t = this._hash).words,
                r = 0;
              4 > r;
              r++
            )
              (o = n[r]),
                (n[r] =
                  (16711935 & ((o << 8) | (o >>> 24))) |
                  (4278255360 & ((o << 24) | (o >>> 8))));
            return t;
          },
          clone: function () {
            var e = u.clone.call(this);
            return (e._hash = this._hash.clone()), e;
          },
        })),
        (a.MD5 = u._createHelper(s)),
        (a.HmacMD5 = u._createHmacHelper(s));
    })(Math),
    (function () {
      var e = n,
        t = e.lib,
        r = t.Base,
        o = t.WordArray,
        i = ((t = e.algo).EvpKDF = r.extend({
          cfg: r.extend({
            keySize: 4,
            hasher: t.MD5,
            iterations: 1,
          }),
          init: function (e) {
            this.cfg = this.cfg.extend(e);
          },
          compute: function (e, t) {
            var n = this.cfg,
              r = n.hasher.create(),
              i = o.create(),
              a = i.words,
              s = n.keySize;
            for (n = n.iterations; a.length < s; ) {
              c && r.update(c);
              var c = r.update(e).finalize(t);
              r.reset();
              for (var u = 1; u < n; u++) (c = r.finalize(c)), r.reset();
              i.concat(c);
            }
            return (i.sigBytes = 4 * s), i;
          },
        }));
      e.EvpKDF = function (e, t, n) {
        return i.create(n).compute(e, t);
      };
    })(),
    n.lib.Cipher ||
      (function (e) {
        var t = n,
          r = t.lib,
          o = r.Base,
          i = r.WordArray,
          a = r.BufferedBlockAlgorithm,
          s = t.enc.Base64,
          c = t.algo.EvpKDF,
          u = (r.Cipher = a.extend({
            cfg: o.extend(),
            createEncryptor: function (e, t) {
              return this.create(this._ENC_XFORM_MODE, e, t);
            },
            createDecryptor: function (e, t) {
              return this.create(this._DEC_XFORM_MODE, e, t);
            },
            init: function (e, t, n) {
              (this.cfg = this.cfg.extend(n)),
                (this._xformMode = e),
                (this._key = t),
                this.reset();
            },
            reset: function () {
              a.reset.call(this), this._doReset();
            },
            process: function (e) {
              return this._append(e), this._process();
            },
            finalize: function (e) {
              return e && this._append(e), this._doFinalize();
            },
            keySize: 4,
            ivSize: 4,
            _ENC_XFORM_MODE: 1,
            _DEC_XFORM_MODE: 2,
            _createHelper: function (e) {
              return {
                encrypt: function (t, n, r) {
                  return ("string" == typeof n ? v : h).encrypt(e, t, n, r);
                },
                decrypt: function (t, n, r) {
                  return ("string" == typeof n ? v : h).decrypt(e, t, n, r);
                },
              };
            },
          }));
        r.StreamCipher = u.extend({
          _doFinalize: function () {
            return this._process(!0);
          },
          blockSize: 1,
        });
        var f = (t.mode = {}),
          l = function (e, t, n) {
            var r = this._iv;
            r ? (this._iv = void 0) : (r = this._prevBlock);
            for (var o = 0; o < n; o++) e[t + o] ^= r[o];
          },
          p = (r.BlockCipherMode = o.extend({
            createEncryptor: function (e, t) {
              return this.Encryptor.create(e, t);
            },
            createDecryptor: function (e, t) {
              return this.Decryptor.create(e, t);
            },
            init: function (e, t) {
              (this._cipher = e), (this._iv = t);
            },
          })).extend();
        (p.Encryptor = p.extend({
          processBlock: function (e, t) {
            var n = this._cipher,
              r = n.blockSize;
            l.call(this, e, t, r),
              n.encryptBlock(e, t),
              (this._prevBlock = e.slice(t, t + r));
          },
        })),
          (p.Decryptor = p.extend({
            processBlock: function (e, t) {
              var n = this._cipher,
                r = n.blockSize,
                o = e.slice(t, t + r);
              n.decryptBlock(e, t),
                l.call(this, e, t, r),
                (this._prevBlock = o);
            },
          })),
          (f = f.CBC = p),
          (p = (t.pad = {}).Pkcs7 =
            {
              pad: function (e, t) {
                for (
                  var n = 4 * t,
                    r =
                      ((n -= e.sigBytes % n) << 24) | (n << 16) | (n << 8) | n,
                    o = [],
                    a = 0;
                  a < n;
                  a += 4
                )
                  o.push(r);
                (n = i.create(o, n)), e.concat(n);
              },
              unpad: function (e) {
                e.sigBytes -= 255 & e.words[(e.sigBytes - 1) >>> 2];
              },
            }),
          (r.BlockCipher = u.extend({
            cfg: u.cfg.extend({
              mode: f,
              padding: p,
            }),
            reset: function () {
              u.reset.call(this);
              var e = this.cfg,
                t = e.iv;
              if (((e = e.mode), this._xformMode == this._ENC_XFORM_MODE))
                var n = e.createEncryptor;
              else (n = e.createDecryptor), (this._minBufferSize = 1);
              this._mode = n.call(e, this, t && t.words);
            },
            _doProcessBlock: function (e, t) {
              this._mode.processBlock(e, t);
            },
            _doFinalize: function () {
              var e = this.cfg.padding;
              if (this._xformMode == this._ENC_XFORM_MODE) {
                e.pad(this._data, this.blockSize);
                var t = this._process(!0);
              } else (t = this._process(!0)), e.unpad(t);
              return t;
            },
            blockSize: 4,
          }));
        var d = (r.CipherParams = o.extend({
            init: function (e) {
              this.mixIn(e);
            },
            toString: function (e) {
              return (e || this.formatter).stringify(this);
            },
          })),
          h =
            ((f = (t.format = {}).OpenSSL =
              {
                stringify: function (e) {
                  var t = e.ciphertext;
                  return (
                    (e = e.salt)
                      ? i.create([1398893684, 1701076831]).concat(e).concat(t)
                      : t
                  ).toString(s);
                },
                parse: function (e) {
                  var t = (e = s.parse(e)).words;
                  if (1398893684 == t[0] && 1701076831 == t[1]) {
                    var n = i.create(t.slice(2, 4));
                    t.splice(0, 4), (e.sigBytes -= 16);
                  }
                  return d.create({
                    ciphertext: e,
                    salt: n,
                  });
                },
              }),
            (r.SerializableCipher = o.extend({
              cfg: o.extend({
                format: f,
              }),
              encrypt: function (e, t, n, r) {
                r = this.cfg.extend(r);
                var o = e.createEncryptor(n, r);
                return (
                  (t = o.finalize(t)),
                  (o = o.cfg),
                  d.create({
                    ciphertext: t,
                    key: n,
                    iv: o.iv,
                    algorithm: e,
                    mode: o.mode,
                    padding: o.padding,
                    blockSize: e.blockSize,
                    formatter: r.format,
                  })
                );
              },
              decrypt: function (e, t, n, r) {
                return (
                  (r = this.cfg.extend(r)),
                  (t = this._parse(t, r.format)),
                  e.createDecryptor(n, r).finalize(t.ciphertext)
                );
              },
              _parse: function (e, t) {
                return "string" == typeof e ? t.parse(e, this) : e;
              },
            }))),
          v =
            ((t = (t.kdf = {}).OpenSSL =
              {
                execute: function (e, t, n, r) {
                  return (
                    r || (r = i.random(8)),
                    (e = c
                      .create({
                        keySize: t + n,
                      })
                      .compute(e, r)),
                    (n = i.create(e.words.slice(t), 4 * n)),
                    (e.sigBytes = 4 * t),
                    d.create({
                      key: e,
                      iv: n,
                      salt: r,
                    })
                  );
                },
              }),
            (r.PasswordBasedCipher = h.extend({
              cfg: h.cfg.extend({
                kdf: t,
              }),
              encrypt: function (e, t, n, r) {
                return (
                  (n = (r = this.cfg.extend(r)).kdf.execute(
                    n,
                    e.keySize,
                    e.ivSize
                  )),
                  (r.iv = n.iv),
                  (e = h.encrypt.call(this, e, t, n.key, r)).mixIn(n),
                  e
                );
              },
              decrypt: function (e, t, n, r) {
                return (
                  (r = this.cfg.extend(r)),
                  (t = this._parse(t, r.format)),
                  (n = r.kdf.execute(n, e.keySize, e.ivSize, t.salt)),
                  (r.iv = n.iv),
                  h.decrypt.call(this, e, t, n.key, r)
                );
              },
            })));
      })(),
    (function () {
      for (
        var e = n,
          t = e.lib.BlockCipher,
          r = e.algo,
          o = [],
          i = [],
          a = [],
          s = [],
          c = [],
          u = [],
          f = [],
          l = [],
          p = [],
          d = [],
          h = [],
          v = 0;
        256 > v;
        v++
      )
        h[v] = 128 > v ? v << 1 : (v << 1) ^ 283;
      var y = 0,
        g = 0;
      for (v = 0; 256 > v; v++) {
        var m = g ^ (g << 1) ^ (g << 2) ^ (g << 3) ^ (g << 4);
        (m = (m >>> 8) ^ (255 & m) ^ 99), (o[y] = m), (i[m] = y);
        var _ = h[y],
          b = h[_],
          w = h[b],
          O = (257 * h[m]) ^ (16843008 * m);
        (a[y] = (O << 24) | (O >>> 8)),
          (s[y] = (O << 16) | (O >>> 16)),
          (c[y] = (O << 8) | (O >>> 24)),
          (u[y] = O),
          (O = (16843009 * w) ^ (65537 * b) ^ (257 * _) ^ (16843008 * y)),
          (f[m] = (O << 24) | (O >>> 8)),
          (l[m] = (O << 16) | (O >>> 16)),
          (p[m] = (O << 8) | (O >>> 24)),
          (d[m] = O),
          y ? ((y = _ ^ h[h[h[w ^ _]]]), (g ^= h[h[g]])) : (y = g = 1);
      }
      var x = [0, 1, 2, 4, 8, 16, 32, 64, 128, 27, 54];
      (r = r.AES =
        t.extend({
          _doReset: function () {
            for (
              var e = this._key,
                t = e.words,
                n = e.sigBytes / 4,
                r =
                  ((e = 4 * ((this._nRounds = n + 6) + 1)),
                  (this._keySchedule = [])),
                i = 0;
              i < e;
              i++
            )
              if (i < n) r[i] = t[i];
              else {
                var a = r[i - 1];
                i % n
                  ? 6 < n &&
                    4 == i % n &&
                    (a =
                      (o[a >>> 24] << 24) |
                      (o[(a >>> 16) & 255] << 16) |
                      (o[(a >>> 8) & 255] << 8) |
                      o[255 & a])
                  : ((a =
                      (o[(a = (a << 8) | (a >>> 24)) >>> 24] << 24) |
                      (o[(a >>> 16) & 255] << 16) |
                      (o[(a >>> 8) & 255] << 8) |
                      o[255 & a]),
                    (a ^= x[(i / n) | 0] << 24)),
                  (r[i] = r[i - n] ^ a);
              }
            for (t = this._invKeySchedule = [], n = 0; n < e; n++)
              (i = e - n),
                (a = n % 4 ? r[i] : r[i - 4]),
                (t[n] =
                  4 > n || 4 >= i
                    ? a
                    : f[o[a >>> 24]] ^
                      l[o[(a >>> 16) & 255]] ^
                      p[o[(a >>> 8) & 255]] ^
                      d[o[255 & a]]);
          },
          encryptBlock: function (e, t) {
            this._doCryptBlock(e, t, this._keySchedule, a, s, c, u, o);
          },
          decryptBlock: function (e, t) {
            var n = e[t + 1];
            (e[t + 1] = e[t + 3]),
              (e[t + 3] = n),
              this._doCryptBlock(e, t, this._invKeySchedule, f, l, p, d, i),
              (n = e[t + 1]),
              (e[t + 1] = e[t + 3]),
              (e[t + 3] = n);
          },
          _doCryptBlock: function (e, t, n, r, o, i, a, s) {
            for (
              var c = this._nRounds,
                u = e[t] ^ n[0],
                f = e[t + 1] ^ n[1],
                l = e[t + 2] ^ n[2],
                p = e[t + 3] ^ n[3],
                d = 4,
                h = 1;
              h < c;
              h++
            ) {
              var v =
                  r[u >>> 24] ^
                  o[(f >>> 16) & 255] ^
                  i[(l >>> 8) & 255] ^
                  a[255 & p] ^
                  n[d++],
                y =
                  r[f >>> 24] ^
                  o[(l >>> 16) & 255] ^
                  i[(p >>> 8) & 255] ^
                  a[255 & u] ^
                  n[d++],
                g =
                  r[l >>> 24] ^
                  o[(p >>> 16) & 255] ^
                  i[(u >>> 8) & 255] ^
                  a[255 & f] ^
                  n[d++];
              (p =
                r[p >>> 24] ^
                o[(u >>> 16) & 255] ^
                i[(f >>> 8) & 255] ^
                a[255 & l] ^
                n[d++]),
                (u = v),
                (f = y),
                (l = g);
            }
            (v =
              ((s[u >>> 24] << 24) |
                (s[(f >>> 16) & 255] << 16) |
                (s[(l >>> 8) & 255] << 8) |
                s[255 & p]) ^
              n[d++]),
              (y =
                ((s[f >>> 24] << 24) |
                  (s[(l >>> 16) & 255] << 16) |
                  (s[(p >>> 8) & 255] << 8) |
                  s[255 & u]) ^
                n[d++]),
              (g =
                ((s[l >>> 24] << 24) |
                  (s[(p >>> 16) & 255] << 16) |
                  (s[(u >>> 8) & 255] << 8) |
                  s[255 & f]) ^
                n[d++]),
              (p =
                ((s[p >>> 24] << 24) |
                  (s[(u >>> 16) & 255] << 16) |
                  (s[(f >>> 8) & 255] << 8) |
                  s[255 & l]) ^
                n[d++]),
              (e[t] = v),
              (e[t + 1] = y),
              (e[t + 2] = g),
              (e[t + 3] = p);
          },
          keySize: 8,
        })),
        (e.AES = t._createHelper(r));
    })(),
    (e.exports = n);
};
