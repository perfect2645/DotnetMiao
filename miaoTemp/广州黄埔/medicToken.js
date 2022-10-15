, u = "/PM-server"
, l = "token_" + localStorage.getItem("hospitalId")
, f = localStorage.getItem(l) || "ddtoken"
, p = {}
, d = window.navigator.userAgent.toLowerCase()
, h = (/micromessenger/i.test(d) || !Object(i.x)()) && "0"
, g = (/dingtalk/i.test(d) || Object(i.x)()) && "1"
, v = /yctapp/i.test(d) && "2"
, m = {
  0: "wx",
  1: "dd",
  2: "yg"
}[h || g || v]
, b = new c.a
, y = Object(i.J)()
, I = a.a.MD5((b.getItem("sessionkey") || y) + "MEDICTOKEN123456" + window.localStorage.getItem("openId")).toString(a.a.enc.Base64).toUpperCase();
p = {
  headers: {
      source: m,
      "Content-Type": "application/json",
      Authorization: f
  }
}