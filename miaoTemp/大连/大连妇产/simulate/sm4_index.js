var sm4n = [
    214, 144, 233, 254, 204, 225, 61, 183, 22, 182, 20, 194, 40, 251, 44, 5, 43,
    103, 154, 118, 42, 190, 4, 195, 170, 68, 19, 38, 73, 134, 6, 153, 156, 66,
    80, 244, 145, 239, 152, 122, 51, 84, 11, 67, 237, 207, 172, 98, 228, 179,
    28, 169, 201, 8, 232, 149, 128, 223, 148, 250, 117, 143, 63, 166, 71, 7,
    167, 252, 243, 115, 23, 186, 131, 89, 60, 25, 230, 133, 79, 168, 104, 107,
    129, 178, 113, 100, 218, 139, 248, 235, 15, 75, 112, 86, 157, 53, 30, 36,
    14, 94, 99, 88, 209, 162, 37, 34, 124, 59, 1, 33, 120, 135, 212, 0, 70, 87,
    159, 211, 39, 82, 76, 54, 2, 231, 160, 196, 200, 158, 234, 191, 138, 210,
    64, 199, 56, 181, 163, 247, 242, 206, 249, 97, 21, 161, 224, 174, 93, 164,
    155, 52, 26, 85, 173, 147, 50, 48, 245, 140, 177, 227, 29, 246, 226, 46,
    130, 102, 202, 96, 192, 41, 35, 171, 13, 83, 78, 111, 213, 219, 55, 69, 222,
    253, 142, 47, 3, 255, 106, 114, 109, 108, 91, 81, 141, 27, 175, 146, 187,
    221, 188, 127, 17, 217, 92, 65, 31, 16, 90, 216, 10, 193, 49, 136, 165, 205,
    123, 189, 45, 116, 208, 18, 184, 229, 180, 176, 137, 105, 151, 74, 12, 150,
    119, 126, 101, 185, 241, 9, 197, 110, 198, 132, 24, 240, 125, 236, 58, 220,
    77, 32, 121, 238, 95, 62, 215, 203, 57, 72,
  ],
  sm4t = [
    462357, 472066609, 943670861, 1415275113, 1886879365, 2358483617,
    2830087869, 3301692121, 3773296373, 4228057617, 404694573, 876298825,
    1347903077, 1819507329, 2291111581, 2762715833, 3234320085, 3705924337,
    4177462797, 337322537, 808926789, 1280531041, 1752135293, 2223739545,
    2695343797, 3166948049, 3638552301, 4110090761, 269950501, 741554753,
    1213159005, 1684763257,
  ];
function ms4O(r) {
  for (var n = [], t = 0, o = r.length; t < o; t += 2)
    n.push(parseInt(r.substr(t, 2), 16));
  return n;
}
function ms4I(r) {
  return r
    .map(function (r) {
      return 1 === (r = r.toString(16)).length ? "0" + r : r;
    })
    .join("");
}
function ms4E(r) {
  for (var n = [], t = 0, o = r.length; t < o; t++) {
    var i = r.codePointAt(t);
    if (i <= 127) n.push(i);
    else if (i <= 2047) n.push(192 | (i >>> 6)), n.push(128 | (63 & i));
    else if (i <= 55295 || (i >= 57344 && i <= 65535))
      n.push(224 | (i >>> 12)),
        n.push(128 | ((i >>> 6) & 63)),
        n.push(128 | (63 & i));
    else {
      if (!(i >= 65536 && i <= 1114111))
        throw (n.push(i), new Error("input is not supported"));
      t++,
        n.push(240 | ((i >>> 18) & 28)),
        n.push(128 | ((i >>> 12) & 63)),
        n.push(128 | ((i >>> 6) & 63)),
        n.push(128 | (63 & i));
    }
  }
  return n;
}
function ms4U(r) {
  for (var n = [], t = 0, o = r.length; t < o; t++)
    r[t] >= 240 && r[t] <= 247
      ? (n.push(
          String.fromCodePoint(
            ((7 & r[t]) << 18) +
              ((63 & r[t + 1]) << 12) +
              ((63 & r[t + 2]) << 6) +
              (63 & r[t + 3])
          )
        ),
        (t += 3))
      : r[t] >= 224 && r[t] <= 239
      ? (n.push(
          String.fromCodePoint(
            ((15 & r[t]) << 12) + ((63 & r[t + 1]) << 6) + (63 & r[t + 2])
          )
        ),
        (t += 2))
      : r[t] >= 192 && r[t] <= 223
      ? (n.push(String.fromCodePoint(((31 & r[t]) << 6) + (63 & r[t + 1]))),
        t++)
      : n.push(String.fromCodePoint(r[t]));
  return n.join("");
}
function ms4F(r, n) {
  return (r << n) | (r >>> (32 - n));
}
function ms4S(r) {
  return (
    ((255 & sm4n[(r >>> 24) & 255]) << 24) |
    ((255 & sm4n[(r >>> 16) & 255]) << 16) |
    ((255 & sm4n[(r >>> 8) & 255]) << 8) |
    (255 & sm4n[255 & r])
  );
}
function ms4P(r) {
  return r ^ ms4F(r, 2) ^ ms4F(r, 10) ^ ms4F(r, 18) ^ ms4F(r, 24);
}
function ms4A(r) {
  return r ^ ms4F(r, 13) ^ ms4F(r, 23);
}
function ms4C(r, n, t) {
  for (var o = new Array(4), i = new Array(4), e = 0; e < 4; e++)
    (i[0] = 255 & r[4 * e]),
      (i[1] = 255 & r[4 * e + 1]),
      (i[2] = 255 & r[4 * e + 2]),
      (i[3] = 255 & r[4 * e + 3]),
      (o[e] = (i[0] << 24) | (i[1] << 16) | (i[2] << 8) | i[3]);
  for (var u, f = 0; f < 32; f += 4)
    (u = o[1] ^ o[2] ^ o[3] ^ t[f + 0]),
      (o[0] ^= ms4P(ms4S(u))),
      (u = o[2] ^ o[3] ^ o[0] ^ t[f + 1]),
      (o[1] ^= ms4P(ms4S(u))),
      (u = o[3] ^ o[0] ^ o[1] ^ t[f + 2]),
      (o[2] ^= ms4P(ms4S(u))),
      (u = o[0] ^ o[1] ^ o[2] ^ t[f + 3]),
      (o[3] ^= ms4P(ms4S(u)));
  for (var a = 0; a < 16; a += 4)
    (n[a] = (o[3 - a / 4] >>> 24) & 255),
      (n[a + 1] = (o[3 - a / 4] >>> 16) & 255),
      (n[a + 2] = (o[3 - a / 4] >>> 8) & 255),
      (n[a + 3] = 255 & o[3 - a / 4]);
}
function ms4H(r, n, o) {
  for (var i = new Array(4), e = new Array(4), u = 0; u < 4; u++)
    (e[0] = 255 & r[0 + 4 * u]),
      (e[1] = 255 & r[1 + 4 * u]),
      (e[2] = 255 & r[2 + 4 * u]),
      (e[3] = 255 & r[3 + 4 * u]),
      (i[u] = (e[0] << 24) | (e[1] << 16) | (e[2] << 8) | e[3]);
  (i[0] ^= 2746333894),
    (i[1] ^= 1453994832),
    (i[2] ^= 1736282519),
    (i[3] ^= 2993693404);
  for (var f, p = 0; p < 32; p += 4)
    (f = i[1] ^ i[2] ^ i[3] ^ sm4t[p + 0]),
      (n[p + 0] = i[0] ^= ms4A(ms4S(f))),
      (f = i[2] ^ i[3] ^ i[0] ^ sm4t[p + 1]),
      (n[p + 1] = i[1] ^= ms4A(ms4S(f))),
      (f = i[3] ^ i[0] ^ i[1] ^ sm4t[p + 2]),
      (n[p + 2] = i[2] ^= ms4A(ms4S(f))),
      (f = i[0] ^ i[1] ^ i[2] ^ sm4t[p + 3]),
      (n[p + 3] = i[3] ^= ms4A(ms4S(f)));
  if (0 === o)
    for (var c, h = 0; h < 16; h++)
      (c = n[h]), (n[h] = n[31 - h]), (n[31 - h] = c);
}
function sm4V(n, t, f) {
  var s = arguments.length > 3 && void 0 !== arguments[3] ? arguments[3] : {},
    p = s.padding,
    a = void 0 === p ? "pkcs#7" : p,
    v = s.mode,
    l = s.iv,
    g = void 0 === l ? [] : l,
    d = s.output,
    w = void 0 === d ? "string" : d;
  if ("cbc" === v && ("string" == typeof g && (g = ms4O(g)), 16 !== g.length))
    throw new Error("iv is invalid");
  if (("string" == typeof t && (t = ms4O(t)), 16 !== t.length))
    throw new Error("key is invalid");
  if (
    ((n = "string" == typeof n ? (0 !== f ? ms4E(n) : ms4O(n)) : r(n)),
    ("pkcs#5" === a || "pkcs#7" === a) && 0 !== f)
  )
    for (var y = 16 - (n.length % 16), m = 0; m < y; m++) n.push(y);
  var b = new Array(32);
  ms4H(t, b, f);
  for (var A = [], k = g, C = n.length, P = 0; C >= 16; ) {
    var S = n.slice(P, P + 16),
      E = new Array(16);
    if ("cbc" === v) for (var j = 0; j < 16; j++) 0 !== f && (S[j] ^= k[j]);
    ms4C(S, E, b);
    for (var q = 0; q < 16; q++)
      "cbc" === v && 0 === f && (E[q] ^= k[q]), (A[P + q] = E[q]);
    "cbc" === v && (k = 0 !== f ? E : S), (C -= 16), (P += 16);
  }
  if (("pkcs#5" === a || "pkcs#7" === a) && 0 === f) {
    for (var x = A.length, I = A[x - 1], z = 1; z <= I; z++)
      if (A[x - z] !== I) throw new Error("padding is invalid");
    A.splice(x - I, I);
  }
  return "array" !== w ? (0 !== f ? ms4I(A) : ms4U(A)) : A;
}

var encrypt = function (r, n, t) {
  var enResult = sm4V(r, n, 1, t);
  var deTestResult = sm4V(
    "55ea27eab149742d2701ec3c5cd3c7ea4048e12a048c3c8d910de4976a42b5317b4518bd794b32ec2855a2296d6e7108b6d3f7fad695f71f90025c7696d58e5c63a16989f27f8c6acd504d2f5a463229652e91b147e32a0e9204bbcc6991ced6e3e94f71c33e2db75796c7829aaa8e84b7bbcee666a813e51214f0acf3cfdba8eaa0b55c23e40f7ea6a61ec573280c0438d3741ffbc0c34c425f6b5414eb24253218ebbebc93e0e9d08535254e68b5bcbda13d6b827f88b2a0afd4204517363b90429a5934d1cceef0ca70124a059a69c16f740e58fc911c4f26555870fd5c70152c4a10cf7b67989a7ee1c09e546cced1b0035b08f43a9c98ba330dd6730b3af0f44c6ef1032f579e08701b3aabbdec646090e412ab67ac81011b1f638b846f81b58d97341ecf7729b86742f5a881ad5379b612dd4567e14addcb418793c6db960e7043c0f66d86c484983f2ebd3b5ef2bc0dac97acb9a95281064ee5ac71abc0cf63ced0379dc02862ce3b2cb3557059d5c8cf688631427bc7f8aff38005347517564a45861643f911c22fd045aa2b83a4fb176fdffb3ef40c4967d84ca122b074b7da1cdac6ef5b5f59e2307b2d306f56285f6cb574f5692df3d71f9b1089ccdeaaea4f4a02f6984b939263dba902a20745e1604b3502d6a60d2f9349735e0e1f1d114156154efea6dbde1736bc6c5c7a69f90f65282b7fa7e536ad86d8ba58ac81b44bcde41bca35ecffd7e5a2e18a90b4f4f9cc47e71be6faa6aa3cac5c87ad1d699c1094be0a3baa300442f621",
    n,
    0,
    t
  );

  console.log(deTestResult);

  return enResult;

  // return sm4V(r, n, 1, t);
};
var decrypt = function (r, n, t) {
  return sm4V(r, n, 0, t);
};
