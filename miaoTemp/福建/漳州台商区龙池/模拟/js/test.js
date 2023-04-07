$(document).on("click", ".testBtn", function () {
  var yuyue_qty = parseInt($("#yuyue_qty").val());
  var yuyue_dizhi = $("#yuyue_dizhi").val();
  var yuyue_dizhi_mane = $("#yuyue_dizhi").find("option:selected").text();
  var date = "2023-01-02 08:00";

  var yuyue_name = "周瑠夏";
  var yuyue_user_code = "612426199806235426";
  var yuyue_user_add = "翡翠梦境";
  var yuyue_user_suoshu = "百盛";

  $.post(
    "http://hpv_ym.zzytrj.net:15003/api/yuyue.php",
    {
      cmd: "yuyue_post",
      date: date,
      qty: yuyue_qty,
      dizhi: yuyue_dizhi,
      yuyue_name: yuyue_name,
      yuyue_user_code: yuyue_user_code,
      yuyue_user_add: yuyue_user_add,
      yuyue_user_suoshu: yuyue_user_suoshu,
    },
    function (res) {
      if (res.code == 2) {
        setTimeout(function () {
          lunxun_status(res.return_id);
        }, 3000);
        return;
      }

      if (res.code != 1) {
        layer.closeAll();

        $.modal({
          title: '<span style="font-size:18px;">' + res.msg + "</span>",
          buttons: [{ text: "知道了！" }],
        });

        return;
      }

      $.router.back("../index/index.html");

      var s = res.beginTime;
      s = s.substr(0, 10);
      $.alert(
        '<div style="font-size:16px">您的预约号是：<span style="font-size:28px;color:#f00">' +
          res.order_id +
          '</span><br/>请在 <span style="font-size:18px;color:#07F">' +
          s +
          "</span><br/>到达接种地点<br/>【" +
          yuyue_dizhi_mane +
          "】</div>",
        "预约成功",
        function () {}
      );
    },
    "json"
  ).error(function (xhr, status, info) {
    layer.msg("预约高峰期，请耐心等候！", {
      icon: 16,
      shade: 0.8,
      time: 30000,
    });

    setTimeout(function () {
      lunxun_status(id);
    }, 10000);
    return;
  });
});
