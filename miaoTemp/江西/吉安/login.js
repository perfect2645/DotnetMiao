function(t, e, n) {
    var i = n("cGG2"), r = n("21It"), o = n("DQCr"), a = n("Oi+a"), s = n("oJlt"), c = n("GHBc"), A = n("FtD3"); t.exports = function (t) {
        return new Promise(function (e, u) {
            var l = t.data, h = t.headers; i.isFormData(l) && delete h["Content-Type"]; var d = new XMLHttpRequest;
            if (t.auth) { var f = t.auth.username || "", p = t.auth.password || ""; h.Authorization = "Basic " + btoa(f + ":" + p) } var g = a(t.baseURL, t.url);
        }
    }
}
