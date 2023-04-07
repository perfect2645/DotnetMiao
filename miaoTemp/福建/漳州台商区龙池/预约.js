var modal = $.modal.prototype.defaults;
modal.modalButtonOk = "确定";
modal.modalButtonCancel = "取消";
modal.modalPreloaderTitle = "正在加载...";

layui
  .config({
    base: "../../controller/module/", //假设这是你存放拓展模块的根目录
    version: layui.data("ver").val,
  })
  .use(["carousel", "element", "flow", "layer", "select_time"], function () {
    var carousel = layui.carousel,
      element = layui.element,
      flow = layui.flow,
      layer = layui.layer,
      select_time = layui.select_time,
      goods_carousel = layui.carousel;

    var $goods_sku;

    var ins;

    var user_id = ytui.getUrlParam("id");

    if (user_id) {
      ytui.req({
        type: "post",
        url: "../../api/yuyue.php",
        data: { cmd: "cmd", user_id: user_id },
        success: function (res) {
          data = res.data[0];
          if (res.code == 1) {
            $("#yuyue_name").val(data.姓名);
            $("#yuyue_user_code").val(data.身份证号);
            $("#yuyue_user_add").val(data.居住地址);
            $("#yuyue_user_suoshu").val(data.所属单位);
          }
        },
      });
    }

    var sw = layui.data("switch");

    if (sw.hide_4848 != 1) {
      $.post(
        "../../api/yuyue.php?cmd=get_tongzhi",
        { page_id: 5504 },
        function (data) {
          if (data.error_code == 0) {
            if (data.data.page_content != "无") {
              var con = data.data.page_content;

              con = con.replace("\n", "<br/>");

              $.modal({
                title:
                  '<div style="font-size:18px;text-align:left;max-height:400px;overflow:auto">' +
                  con +
                  "</div>",
                buttons: [{ text: "知道了！" }],
              });
            }
          }
        },
        "JSON"
      );
    }

    var h = window.screen.height;
    var w = window.screen.width;
    var sid = ytui.getUrlParam("sid");

    var title = "预约时间";
    var jine = 999;
    var list =
      "[{title:'03月28日',val:'2021-03-28'},{title:'03月29日',val:'2021-03-29'},{title:'03月30日',val:'2021-03-30'}]";

    var list_time =
      "[{title:'08:00',val:'08:00'},{title:'09:00',val:'09:00'},{title:'10:00',val:'10:00'},{title:'11:00',val:'11:00'}]";

    // yuyue_dizhi

    ytui.req({
      type: "post",
      url: "../../api/yuyue.php",
      data: { cmd: "get_yuyue_dizhi", redis: 1 },
      success: function (data) {
        if (data.code == 1) {
          $("#yuyue_dizhi").append('<option value="">请选择地点</option>');

          for (var i in data.rows) {
            $("#yuyue_dizhi").append(
              '<option  min_age="' +
                data.rows[i].最小年龄 +
                '"  max_age="' +
                data.rows[i].最大年龄 +
                '"  title="' +
                data.rows[i].疫苗名称 +
                '"   sex="' +
                data.rows[i].性别 +
                '" value="' +
                data.rows[i].id +
                '">' +
                data.rows[i].text +
                "</option>"
            );
          }

          //选择select时触发
          $("#yuyue_dizhi").change(function () {
            var title = $(this).find("option:selected").attr("title");

            $("#yuyue_title").text(title);
          });
        }
      },
    });

    //select_time.show(title,jine,list);

    $(".testBtn").click(function () {

    $(".select_time_btn").click(function () {
      //选择时间

      var yuyue_qty = $("#yuyue_qty").val();
      var yuyue_dizhi = $("#yuyue_dizhi").val();

      if (yuyue_dizhi == "") {
        layer.msg("请先选择【接种地点】");
        return;
      }

      if (yuyue_qty > 0) {
        var sw = layui.data("switch");

        sw.zjsj_s = sw.zjsj_s ? sw.zjsj_s : "";
        sw.zjsj_e = sw.zjsj_e ? sw.zjsj_e : "";
        sw.begin_time = sw.begin_time ? sw.begin_time : "";

        var a = judgeTime(sw.zjsj_s);
        var b = judgeTime(sw.zjsj_e);

        if (sw.zjsj_s != "") {
          if (a && !b) {
            var msg_01 = layer.msg("正在排队预约中，请耐心等候...", {
              title: "当前预约高峰期",
              icon: 16,
              shade: 0.8,
              time: 0,
            });

            setTimeout(function () {
              layer.close(msg_01);
              $.modal({
                title:
                  '<span style="font-size:18px;">很抱歉，预约已满！</span>',
                buttons: [{ text: "知道了！" }],
              });
            }, 1000 * 60 * 15);

            return;
          } else {
            layer.msg('<div style="font-size:18px;">当前没有预约号可用</div>', {
              anim: 6,
              offset: "t",
            });
          }

          return;
        }

        if (sw.begin_time != "") {
          var b = judgeTime(sw.begin_time);
          if (!b) {
            layer.msg('<div style="font-size:18px;">当前没有预约号可用</div>', {
              anim: 6,
              offset: "t",
            });
            return;
          }
        }

        ytui.req({
          type: "post",
          url: "../../api/yuyue.php",
          data: { cmd: "get_yuyue_date", dizhi: yuyue_dizhi, qty: yuyue_qty },
          success: function (data) {
            var begin_time = data.begin ? data.begin : "";
            layui.data("switch", { key: "begin_time", value: begin_time });

            if (data.code == 2) {
              var msg_02 = layer.msg("正在排队预约中，请耐心等候...", {
                title: "当前预约高峰期",
                icon: 16,
                shade: 0.8,
                time: 0,
              });

              setTimeout(function () {
                layer.close(msg_02);
                $.modal({
                  title:
                    '<span style="font-size:18px;">很抱歉，预约已满！</span>',
                  buttons: [{ text: "知道了！" }],
                });
              }, 1000 * 60 * 15);
              return;
            }

            if (data.code == 1) {
              select_time.show(title, yuyue_qty, data.rows, yuyue_dizhi);
            } else {
              layer.msg(data.msg);
              return;
            }
          },
        });
      } else {
        layer.msg("请先输入【接种人数】");
        return;
      }
    });

    function judgeTime(time) {
      var strtime = time.replace("/-/g", "/"); //时间转换
      //时间
      var date1 = new Date(strtime);
      //现在时间
      var date2 = new Date();
      //判断时间是否过期
      return date1 < date2 ? true : false;
    }

    $(document)
      .off("click", ".goods_pay_btn")
      .on("click", ".goods_pay_btn", function () {
        var yuyue_qty = parseInt($("#yuyue_qty").val());
        var yuyue_dizhi = $("#yuyue_dizhi").val();
        var yuyue_dizhi_mane = $("#yuyue_dizhi").find("option:selected").text();
        var date = $("#yuyue_time").html();

        if (yuyue_dizhi == "") {
          layer.msg("请先选择【接种地点】");
          return;
        }

        if (yuyue_qty < 1) {
          layer.msg("请先输入【接种人数】");
          return;
        }

        if (date == "") {
          layer.msg("请选择预约日期");
          return;
        }
        if (date == "请选择") {
          layer.msg("请选择预约日期");
          return;
        }

        var yuyue_name = $("#yuyue_name").val();
        var yuyue_user_code = $("#yuyue_user_code").val();
        var yuyue_user_add = $("#yuyue_user_add").val();
        var yuyue_user_suoshu = $("#yuyue_user_suoshu").val();

        if (yuyue_name == "") {
          layer.msg("请输入姓名");
          return;
        }

        if (yuyue_user_code == "") {
          layer.msg("请输入身份证号码");
          return;
        }
        if (yuyue_user_suoshu == "") {
          layer.msg("请输入所属单位");
          return;
        }
        if (yuyue_user_add == "") {
          layer.msg("请输入地址");
          return;
        }

        layer.msg("预约中，请稍后...", { icon: 16, shade: 0.8, time: 30000 });

        $.post(
          "../../api/yuyue.php",
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

    function lunxun_status(id) {
      layer.msg("预约中，请稍后...", { icon: 16, shade: 0.8, time: 30000 });

      $.post(
        "../../api/yuyue.php",
        { cmd: "yuyue_return_id_get", id: id },
        function (res) {
          //进入下一个查询
          if (res.code == 2) {
            setTimeout(function () {
              lunxun_status(id);
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

          layer.closeAll();

          $.router.back("../index/index.html");

          var yuyue_dizhi_mane = $("#yuyue_dizhi")
            .find("option:selected")
            .text();

          var s = res.beginTime;
          s = s.substr(0, 10);

          $.alert(
            '<div style="font-size:16px">您的预约号是：<span style="font-size:28px;color:#f00">' +
              res.order_id +
              '</span><br/>请在 <span style="font-size:18px;color:#07F">' +
              s +
              "</span><br/> 到达接种地点<br/>【" +
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
    }
  });
