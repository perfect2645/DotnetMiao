/**
购物车模块
    
 */

layui.define("layer", function (exports) {
  var $ = layui.$,
    layer = layui.layer,
    select_time = {};

  var w = window.screen.width;

  var layer_index = layer.index;

  $(document)
    .off("click", ".select_time")
    .on("click", ".select_time", function () {
      $(".select_time").css("background-color", "#fff");
      $(".select_time").css("color", "#000");

      $(this).css("background-color", "#49b0f1");
      $(this).css("color", "#000");

      var time = $(this).attr("time");
      var max_qty = parseInt($(this).attr("max_qty"));
      var qty = parseInt($(this).attr("qty"));
      var dizhi = $(this).attr("dizhi");

      console.log(qty);
      console.log(max_qty);

      if (qty > max_qty) {
        layer.alert("当前时间段最多只能预约" + max_qty + "人", {
          title: "系统提示",
        });
        $(".select_time").css("background-color", "#fff");
        $(".select_time").css("color", "#000");
        return;
      }

      $("#yuyue_time").html(time);

      layer.close(layer_index);
    });

  $(document)
    .off("click", ".select_jine")
    .on("click", ".select_jine", function () {
      $(".select_jine").css("background-color", "#fff");
      $(".select_jine").css("color", "#000");

      $(this).css("background-color", "#49b0f1");
      $(this).css("color", "#000");

      var date = $(this).attr("val");
      var qty = parseInt($(this).attr("qty"));
      var dizhi = $(this).attr("dizhi");

      ytui.req({
        type: "post",
        url: "../../api/yuyue.php",
        data: { cmd: "get_yuyue_time", date: date, qty: qty, dizhi: dizhi },
        success: function (data) {
          var html = "";
          $.each(data.rows, function (i, val) {
            if (parseInt(val.qty) < qty) {
              html +=
                ' <label dizhi="' +
                dizhi +
                '" qty="' +
                qty +
                '"  max_qty="' +
                val.qty +
                '"  time="' +
                val.val +
                '" class=" layui-form-label" style="border-radius:5px;padding:5px 0px;font-size:16px;text-align:center;border:1px solid #aaa;margin:5px 5px 10px 5px;width:100px;background-color:#eee;color:#999">' +
                val.title +
                '<br/><span class="val_cls" style="font-size:12px;color:#f00">可约' +
                val.qty +
                "人</span></label>";
            } else {
              html +=
                ' <label dizhi="' +
                dizhi +
                '" qty="' +
                qty +
                '"  max_qty="' +
                val.qty +
                '"  time="' +
                val.val +
                '" class="select_time layui-form-label" style="border-radius:5px;padding:5px 0px;font-size:16px;text-align:center;border:1px solid #aaa;margin:5px 5px 10px 5px;width:100px">' +
                val.title +
                '<br/><span class="val_cls" style="font-size:12px;color:#f00">可约' +
                val.qty +
                "人</span></label>";
            }
          });

          if (html == "") {
            $(".time_list_row").html("无可预约时间");
          }
          $(".time_list_div").show();
          $(".time_list_row").html(html);
        },
      });
    });

  function show_sk(title, yuyue_qty, date, yuyue_dizhi) {
    var html =
      '<div class="layui-container" id="pay_title" val="' +
      title +
      '" style="margin-bottom:50px">';

    html += '<div class="layui-row" style="package:15px">';

    html += '<div class="layui-col-md12" style="padding-top:20px">';

    html += '<div class="layui-form-item">';
    html +=
      ' <label class="layui-form-label" style="padding:5px 0px;font-size:16px;width:100%;text-align:left;font-Weight:bold">请选择预约日期：</label>';

    html += " </div>";

    html += '<div class="layui-form-item">';

    $.each(date, function (i, val) {
      html +=
        ' <label dizhi="' +
        yuyue_dizhi +
        '" qty="' +
        yuyue_qty +
        '" val="' +
        val.val +
        '" class="select_jine layui-form-label" style="border-radius:5px;padding:5px 0px;font-size:16px;text-align:center;border:1px solid #aaa;margin:5px 5px 10px 5px">' +
        val.title +
        "</label>";
    });

    html += "</div>";

    html += "</div>";

    html += '<div class="layui-col-md12 time_list_div" style="display:none" >';

    html += '<div class="layui-form-item" >';
    // html+=' <label class="layui-form-label" style="padding:5px 0px;font-size:16px;width:100%;text-align:left;font-Weight:bold">请选择时间：</label>';
    html += "<hr/>";
    html += " </div>";

    html += '<div class="layui-form-item time_list_row">';

    html += "</div>";

    html += "</div>";

    html += "</div>";

    html += "</div>";

    //在这里面输入任何合法的js语句
    layer_index = layer.open({
      type: 1, //Page层类型
      area: [w + "px", "90%"],
      title: title,
      shade: 0.6, //遮罩透明度
      maxmin: false, //允许全屏最小化
      anim: 2, //0-6的动画形式，-1不开启
      closeBtn: 1,
      shadeClose: 1,
      offset: "b",
      content: html,
    });

    // $('.layui-layer-close').css('background-color','#f00');;
  }

  select_time.show = function (title, jine, date, yuyue_dizhi) {
    if (!jine) {
      jine = 0;
    }

    if (!title) {
      title = "";
    }
    show_sk(title, jine, date, yuyue_dizhi);
  };

  //对外暴露的接口
  exports("select_time", select_time);
});
