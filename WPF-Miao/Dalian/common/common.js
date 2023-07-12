var requestResult = {};

var nGlobalData = {
    globalData: {
        appid: "wxa794c2a4fcfeb7f4",
        appointmentCancleTmpl: "XkV9bhx48huOPgvXtkZXRsi3xhruS0yFCME9FXkc9Jo",
        appointmentSuccessTmpl: "HwY-pBZH7gSSE6txEgyUDErtKnrkDtK51letwiPACDk",
        baseCaseUrl: "https://ghsf.dletyy.com",
        baseUrl: "https://hlwyy.dlfeyljt.com/patient/v1",
        bindingCardTmpl: "1c4EcGS6kDrDDOZvrfOmMW719lPfqMNFcFSyMSmkelc",
        cloudLongTmpl: "-DNbE99GuHRXbIs1ZA5ozWMhQzdoLl2ES6_qEgQFCnc",
        compoundName: "",
        consultLongTmpl: "-DNbE99GuHRXbIs1ZA5ozWMhQzdoLl2ES6_qEgQFCnc",
        escortReviewTmpl: "",
    },
};

function miniRequest(e, t) {
    t["appId"] = parameterObj.appId;
    t["openId"] = parameterObj.openId;
    t.hospId = parameterObj.hospId;
    t["hospitalId"] = parameterObj.hospitalId;
    var i,
        u,
        f,
        g = new Date()["getTime"](),
        h = 5,
        v = "1.0",
        w = nGlobalData["globalData"]["userId"],
        x = d(),
        m = e["split"]("/"),
        I = nGlobalData["globalData"]["baseUrl"]["split"]("/"),
        y = m["indexOf"]("d3"),
        S = I.indexOf("v1"),
        b = (function (e, t, n, o) {
            var s = "";
            o >= 0
                ? ((u = [
                    "Ycde",
                    "NBME",
                    "xPe9iO{WXsAxZM",
                    "RhYqRedo=K7JvcuyjpbyppnZr7qQGs21JQsTNSp5TJm",
                    "czO",
                    "Tyrcs",
                ]),
                    (s = (u = u["slice"](1, 5))["join"]("")))
                : (s = l());
            var c, u;
            var d = e["substring"](5),
                f = sm3index(req);
            return sm3index(s + e + d + f);
        })(x, 0, t, y);
    y >= 0
        ? ((w = 0), (h = 99))
        : S > 0 &&
        ((i = t),
            // (c = require("sm4_index.js")),
            // require(s(238)),
            (u = p()),
            (f = JSON.stringify(i)),
            (t = {
                requestData: encrypt(f, u),
            }),
            (enc = t));
    requestResult.request = enc;
    requestResult.timestamp = g;
    requestResult.nonce = x;
    return requestResult;
}

function d() {
    for (t = [], n = "0123456789abcdef", o = 0; o < 36; o++)
        t[o] = n["substr"](Math.floor(16 * Math["random"]()), 1);
    return (
        (t[14] = "4"),
        (t[19] = n["substr"]((3 & t[19]) | 8, 1)),
        (t[8] = t[13] = t[18] = t[23] = "-"),
        t.join("").replace("-", "")
    );
}
function l() {
    t = [
        "PGyMh",
        "Ev",
        "Aucy",
        "QXqhNr",
        "XB23hw8w",
        "Pw",
        "73xHz",
        "AHNqipm",
        "BFKJTHGzBX",
        "TsHpNxR9",
        "PGyMhErNEvAu",
        "GyTsHpMh",
    ];
    return (t = t["slice"](1, 11))["join"]("");
}
function p() {
    t = ["ed367", "3879", "696f4c", "6e5659", "522a3433", "266f2e33", "ic190"];
    return (t = t["slice"](1, 6)).join("");
}
