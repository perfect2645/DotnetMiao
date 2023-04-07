var utm_source = getUrlParam("utm_source");
var doctorId = getUrlParam("doctorId");
var deptId = getUrlParam("deptId");
var showMultiDept = getUrlParam("showMultiDept");
var hospitalInternal = getUrlParam("hospitalInternal");
if (isNull(showMultiDept)) {
  showMultiDept = 1;
}
var doctorName;
var doctorInfoFirst;
var doctorInfo;
var _FddxfsyebhSubHospitalArr; //复旦附属眼耳鼻喉
var standardDeptId = "";
var isManyDept = false;
var pageShowDoctorOfficeName = true;
$(function () {
  _FddxfsyebhSubHospitalArr = isDebug
    ? ["10235771", "22811", "1045722", "1037671"]
    : ["22811", "1045722", "1037671"];
  $.when(getDoctorInfo).then(function () {
    //医生对外备注
    DoctorService.getRemark();
    //判断账号绑定
    GetAccountBindInfo();

    //app分享小程序
    shareDoctorService.load(doctorId, deptId);

    //广告
    $.getScript(StrAdverUrl, function () {
      adverService.loadAdver();
    });
  });
  initShowShare(false, "", "", "", "", "");
});

//获取医生信息
var getDoctorInfo = $.ajax({
  url: "../do/doctorInfo/getDoctorInfoV2",
  timeout: 8000,
  type: "POST",
  dataType: "json",
  async: true,
  data: {
    doctorId: doctorId,
    channelId: channelId,
  },
  beforeSend: function () {
    myLayer.load("加载中...");
  },
  success: function (json) {
    myLayer.clear();
    if (json && json.Code == 10000 && json.Total > 0) {
      doctorInfo = json.Result;
      $("#divDoctorInfo").show();
      showDoctorInfo(
        json.Result,
        json.Zuozhen,
        json.isOpenPlatformChannel,
        json.privateHosPopularDoctor
      );
    } else {
      $("#divNullDoctor").show();
    }
  },
  error: function () {
    myLayer.clear();
    $("#divNullDoctor").show();
  },
});

//显示医生信息
function showDoctorInfo(
  items,
  Zuozhen,
  isOpenPlatformChannel,
  privateHosPopularDoctor
) {
  //医院科室
  for (var i = 0; i < items.length; i++) {
    if (
      items[i].hosDeptId == deptId ||
      (deptId == "" && items[i].main == "1")
    ) {
      doctorInfoFirst = items[i];
      break;
    }
  }
  if (isNull(doctorInfoFirst)) {
    doctorInfoFirst = items[0];
  }
  if (
    isNotNull(hospitalInternal) &&
    hospitalInternal == 1 &&
    doctorInfoFirst.nature == 2
  ) {
    pageShowDoctorOfficeName = false;
  }
  MultiDept.Zuozhen = Zuozhen;
  MultiDept.isOpenPlatformChannel = isOpenPlatformChannel;
  MultiDept.privateHosPopularDoctor = privateHosPopularDoctor;

  MultiDept.loadHospital();
  document.title = doctorInfoFirst.doctorName + "-预约挂号";
  standardDeptId = doctorInfoFirst.standardDeptId;

  LvTongService.checkIsCooperate(
    doctorInfoFirst.hospitalId,
    doctorInfoFirst.hosDeptId
  );
}

var MultiDept = {
  deptList: [],
  branchHospital: [],
  loadHospital: function () {
    var that = this;
    that.deptList.push(doctorInfoFirst);
    $.each(doctorInfo, function (index, item) {
      if (
        item.hosDeptId != doctorInfoFirst.hosDeptId &&
        item.hospitalId == doctorInfoFirst.hospitalId
      ) {
        that.deptList.push(item);
      }
    });
    if (isNotNull(showMultiDept) && showMultiDept == 1) {
      that.loadAllHospital();
    } else {
      that.getMultiDeptConfig();
    }
  },
  getMultiDeptConfig: function () {
    var that = this;
    $.ajax({
      url: "../do/doctorInfo/getServiceConfig",
      timeout: 8000,
      dataType: "json",
      type: "POST",
      data: {
        t: new Date().getTime(),
        hospitalId: doctorInfoFirst.hospitalId,
        serviceCode: "2104",
        channelId: channelId,
      },
      success: function (result) {
        if (result.Code == 10000) {
          if (result.Result && result.Result.state == 1) {
            // state:1
            that.loadAllHospital();
          } else {
            that.getBranchHospital();
          }
        } else {
          that.getBranchHospital();
        }
      },
      error: function () {
        that.getBranchHospital();
      },
    });
  },
  loadAllHospital: function () {
    var that = this;
    $.each(doctorInfo, function (index, item) {
      var pos = that.deptList.length;
      if (item.hospitalId != doctorInfoFirst.hospitalId) {
        for (var i in that.deptList) {
          if (item.hospitalId == that.deptList[i].hospitalId) {
            pos = parseInt(i) + 1;
          }
        }
        that.deptList.splice(pos, 0, item);
      }
    });
    that.loadEnd();
  },
  getBranchHospital: function () {
    var that = this;
    if (
      doctorInfoFirst.hospitalId == "33" ||
      doctorInfoFirst.hospitalId == "1098531"
    ) {
      that.loadEnd();
      return false;
    }
    $.ajax({
      url: "../do/doctorService/queryHospitalRelationList",
      timeout: 8000,
      dataType: "json",
      type: "POST",
      data: {
        t: new Date().getTime(),
        hospitalId: doctorInfoFirst.hospitalId,
      },
      success: function (objJson) {
        if (
          objJson &&
          objJson.Code == 10000 &&
          objJson.Result &&
          objJson.Result.length > 0
        ) {
          $.each(objJson.Result, function (index, item) {
            if (item.hospitalId != doctorInfoFirst.hospitalId) {
              that.branchHospital.push(item.hospitalId);
            }
          });
        }
        that.loadBranchHospital();
      },
    });
  },
  loadBranchHospital: function () {
    var that = this;
    $.each(doctorInfo, function (index, item) {
      var pos = that.deptList.length;
      if (item.hospitalId != doctorInfoFirst.hospitalId) {
        if (that.branchHospital.indexOf(parseInt(item.hospitalId)) >= 0) {
          for (var i in that.deptList) {
            if (item.hospitalId == that.deptList[i].hospitalId) {
              pos = parseInt(i) + 1;
            }
          }
          that.deptList.splice(pos, 0, item);
        }
      }
    });
    that.loadEnd();
  },
  loadEnd: function () {
    var that = this;
    // 执业点
    var placeDeptList = [];
    $.each(that.deptList, function (index, item) {
      if (item.typeId != 3) {
        placeDeptList.push(item);
      }
    });
    that.deptList = placeDeptList;
    if (that.deptList.length > 1) {
      isManyDept = true;
    }
    GuaHaoService.deptList = that.deptList;
    if (that.Zuozhen != 0) {
      if (
        $.inArray(doctorInfoFirst.hospitalId, _FddxfsyebhSubHospitalArr) > -1
      ) {
        $("#sched-ghbox").siblings(".fdmess-tit").remove();
        FddxGuaHaoService.loadDataInit();
      } else {
        var isManyHospital = false;
        var hasPrivateHos = false;
        var hospitalIdArr = [];
        var hospitalIds = "";
        try {
          $.each(that.deptList, function (index, item) {
            if (!hospitalIdArr.includes(item.hospitalId)) {
              hospitalIdArr.push(item.hospitalId);
              if (index == 0) {
                hospitalIds += item.hospitalId;
              } else {
                hospitalIds += "," + item.hospitalId;
              }
            }
            if (!hasPrivateHos && item.nature == 2) {
              hasPrivateHos = true;
            }
          });
        } catch (e) {}
        if (hospitalIdArr.length > 1) {
          isManyHospital = true;
        }
        //渠道开关推广民营医院热门医生
        if (
          isNotNull(that.privateHosPopularDoctor) &&
          that.privateHosPopularDoctor == 1
        ) {
          //暂停预约或者有开导流
          if (
            (isNotNull(doctorInfoFirst.config.hosPause) &&
              doctorInfoFirst.config.hosPause == 1) ||
            (isNotNull(doctorInfoFirst.config.dlService) &&
              doctorInfoFirst.config.dlService == 1)
          ) {
            if (isManyHospital && hasPrivateHos) {
              //多执业点并且有民营医院
              //民营医院排在前面，展开民营
              var sortDeptList = [];
              $.each(that.deptList, function (index, item) {
                if (item.nature == 2) {
                  sortDeptList.push(item);
                }
              });
              $.each(that.deptList, function (index, item) {
                if (item.nature != 2) {
                  sortDeptList.push(item);
                }
              });
              that.deptList = sortDeptList;
              GuaHaoService.deptList = that.deptList;
              deptId = sortDeptList[0].hosDeptId;
            } else {
              if (
                isNotNull(doctorInfoFirst.config.hosClosePopularDoctor) &&
                doctorInfoFirst.config.hosClosePopularDoctor == 1
              ) {
                //医院开关配置不显示热门医生
              } else {
                //显示热门医生
                PopularDoctor.queryDoctorListToRecommend();
              }
            }
          }
        }
        GuaHaoService.loadDeptList();
      }
    } else {
      // $('#divChuzhen').hide();
    }
  },
};

var PopularDoctor = {
  queryDoctorListToRecommend: function () {
    $.ajax({
      type: "post",
      async: true,
      url: "../do/doctorInfo/queryDoctorListToRecommend",
      data: {
        t: new Date().getTime(),
        provinceId: doctorInfoFirst.provinceId,
        cityId: doctorInfoFirst.cityId,
        smallDeptId: doctorInfoFirst.standardDeptId,
        hospitalId: doctorInfoFirst.hospitalId,
        deptId: doctorInfoFirst.hosDeptId,
      },
      success: function (d) {
        //console.log(d);
        if (d.Code == 10000 && d.Result && d.Result.length > 0) {
          var count = 0;
          var vHtml = "";
          for (var i = 0; i < d.Result.length; i++) {
            var item = d.Result[i];
            if (item.doctorUid != doctorId && count < 5) {
              vHtml +=
                '<li onclick="PopularDoctor.toArrangePage(' +
                item.hosDeptId +
                "," +
                item.doctorUid +
                ');">';
              vHtml +=
                '<div class="doc-avatar"><img src="' +
                (item.photoUri
                  ? getThumbnailUrl(item.photoUri, 0, 75)
                  : "//f1.yihuimg.com/concat/mobile/cross/images/d-male.png") +
                '" alt=""></div>';
              vHtml += '<div class="doc-info pr10">';
              vHtml +=
                '<h4 class="c-000 c-f14"><b class="c-f16 mr10">' +
                item.doctorName +
                "</b>" +
                (isNotNull(item.lczcName) ? item.lczcName : "") +
                "</h4>";
              vHtml +=
                '<p class="c-f15 c-555 mt3">' +
                item.hosName +
                " " +
                item.deptName +
                "</p>";
              vHtml += '<p class="c-f15 c-999 c-nowrap-multi mt3">擅长：';
              vHtml += isNotNull(item.skill) ? item.skill : "暂无";
              vHtml += "</p>";
              vHtml += "</div>";
              vHtml += "</li>";
              count++;
            }
          }
          $("#doctor-box").html(vHtml);
          $("#PopularDoctorDiv").show();
        } else {
          PopularDoctor.queryHotDoctorGroupList();
        }
      },
    });
  },
  queryHotDoctorGroupList: function () {
    $.ajax({
      type: "post",
      async: true,
      url: "../do/doctorInfo/queryHotDoctorGroupList",
      data: {
        t: new Date().getTime(),
        provinceId: doctorInfoFirst.provinceId,
        cityId: doctorInfoFirst.cityId,
        standardDeptId: doctorInfoFirst.standardDeptId,
        unDoctorUid: doctorId,
      },
      success: function (d) {
        //console.log(d);
        if (d.Code == 10000 && d.Result && d.Result.length > 0) {
          var vHtml = "";
          for (var i = 0; i < d.Result.length && i < 5; i++) {
            var item = d.Result[i];
            vHtml +=
              '<li onclick="PopularDoctor.toArrangePage(' +
              item.hosDeptId +
              "," +
              item.doctorUid +
              ');">';
            vHtml +=
              '<div class="doc-avatar"><img src="' +
              (item.photoUri
                ? getThumbnailUrl(item.photoUri, 0, 75)
                : "//f1.yihuimg.com/concat/mobile/cross/images/d-male.png") +
              '" alt=""></div>';
            vHtml += '<div class="doc-info pr10">';
            vHtml +=
              '<h4 class="c-000 c-f14"><b class="c-f16 mr10">' +
              item.doctorName +
              "</b>" +
              (isNotNull(item.lczcName) ? item.lczcName : "") +
              "</h4>";
            vHtml +=
              '<p class="c-f15 c-555 mt3">' +
              item.hosName +
              " " +
              item.deptName +
              "</p>";
            vHtml += '<p class="c-f15 c-999 c-nowrap-multi mt3">擅长：';
            vHtml += isNotNull(item.skill) ? item.skill : "暂无";
            vHtml += "</p>";
            vHtml += "</div>";
            vHtml += "</li>";
          }
          $("#doctor-box").html(vHtml);
          $("#PopularDoctorDiv").show();
        }
      },
    });
  },
  toArrangePage: function (hosDeptId, doctorUid) {
    saveSiteTrack("bus010btn1109", {
      originhosid: doctorInfoFirst.hospitalId,
      doctorid: doctorUid,
      deptid: hosDeptId,
    });
    var detailUrl =
      StrYyghUrl +
      "/appoint/doctor/doctorArrange.html?deptId=" +
      hosDeptId +
      "&doctorId=" +
      doctorUid +
      "&platformType=" +
      platformType +
      extendParam;
    setTimeout(function () {
      location.href = detailUrl;
    }, 200);
  },
};

//开通会员
function toOpenVipUrl(referCode) {
  if (!CheckLogin()) {
    return;
  }
  var fromUrl =
    location.href.replace("&isfanc=1", "") +
    "&isfanc=1&_t=" +
    new Date().getTime();
  location.href =
    StrUserUrl +
    "/UserCenter/web/vip/online.html?platformType=" +
    platformType +
    "&sourceType=0&sourceId=0&referCode=" +
    referCode +
    "&from=" +
    encodeURIComponent(fromUrl);
}
//挂号服务
var GuaHaoService = {
  labGhState: false,
  deptList: [],
  //加载医院科室信息
  loadDeptList: function () {
    var that = this;
    var _html = "";
    var tempHosId = 0;
    var hosids = "";
    var docSns = "";
    var maxIndex = that.deptList.length - 1;
    var openIndex = 0;
    var hosIndex = 0;
    $.each(that.deptList, function (i, item) {
      if (item.hosDeptId == deptId) {
        openIndex = i;
      }
      var showDoctorOfficeName = 0;
      if (
        pageShowDoctorOfficeName &&
        isNotNull(item.config.showDoctorOffice) &&
        item.config.showDoctorOffice == 1 &&
        isNotNull(item.config.doctorOfficeName)
      ) {
        showDoctorOfficeName = 1;
      }
      if (i == 0 || item.hospitalId != that.deptList[i - 1].hospitalId) {
        hosIndex++;
        var officeName = "";
        if (showDoctorOfficeName == 1) {
          officeName = item.config.doctorOfficeName;
        }
        _html +=
          '<div class="pregrad" id="auto-cl-gh-pregrad-' +
          item.hospitalId +
          '">';
        _html +=
          '<div class="grad-hosp" data-doctorofficename="' + officeName + '">';
        _html += '<div class="pregrad-address">出诊地点' + hosIndex + "</div>";
        _html += '<div class="hosp-cont">';
        _html +=
          "<b>" + (isNotNull(officeName) ? officeName : item.hosName) + "</b>";
        if (item.config.ghState == 1) {
          _html +=
            '<a href="doctorRemark.html?platformType=' +
            platformType +
            "&doctorId=" +
            item.doctorUid +
            "&deptId=" +
            item.hosDeptId +
            "&doctorOfficeName=" +
            encodeURIComponent(officeName) +
            '" class="tag-rule">预约规则</a>';
        }
        _html += "</div>";
        _html += "</div>";

        // _html += '<div class="pregrad-address">出诊地点'+hosIndex+'</div>';
        // _html += '<div class="grad-hosp" data-doctorofficename="'+officeName+'">' + (isNotNull(officeName)?officeName:item.hosName);
        // if(item.config.ghState==1){
        //  _html += '<span><a href="doctorRemark.html?platformType=' + platformType + '&doctorId=' + item.doctorUid + '&deptId=' + item.hosDeptId +'&doctorOfficeName=' + encodeURIComponent(officeName) + '" class="tag-rule">预约规则</a></span>';
        // }else{
        //  _html += '<span></span>';
        // }
        // _html += '</div>';

        _html += '<ul class="grad-dept">';
        hosids += (hosids != "" ? "," : "") + item.hospitalId;
      }
      if (item.config.ghState == 0 || item.config.ghState == 1) {
        _html +=
          '<li class="auto-cl-gh-deptbox" data-docsn="' +
          item.doctorSn +
          '" data-doctorofficename="' +
          item.config.doctorOfficeName +
          '" data-showdoctorofficename="' +
          showDoctorOfficeName +
          '" data-doctoruid="' +
          item.doctorUid +
          '" data-deptid="' +
          item.hosDeptId +
          '" data-hosid="' +
          item.hospitalId +
          '" data-jjsq="0" data-deptname="' +
          item.deptName +
          '" data-hosname="' +
          item.hosName +
          '" data-provinceid="' +
          item.provinceId +
          '" data-provincename="' +
          item.provinceName +
          '" data-cityid="' +
          item.cityId +
          '" data-cityname="' +
          item.cityName +
          '" data-hosPause="' +
          item.config.hosPause +
          '" data-ghstatus="' +
          1 +
          '">';
        _html +=
          '<div class="gdept-tit auto-cl-gh-deptname-' +
          item.hospitalId +
          '" >';
        _html += '<i class="tag-point"></i>' + item.deptName;
        if (item.config.hosPause == 1) {
          _html += '<i class="tag-nograd">暂停预约</i>';
        } else {
          docSns += (docSns != "" ? "," : "") + item.doctorSn;
        }
        _html += "</div>";
        _html +=
          '<div class="gdept-list" id="sched-ghbox-item-' +
          item.hosDeptId +
          '">';
        _html += "</div>";
        _html += "</li>";
      } else if (item.config.ghState == 2 || item.config.ghState == 4) {
        var needVip = item.config.ghState == 2 ? true : false;
        _html +=
          '<li class="auto-cl-gh-deptbox" data-docsn="' +
          item.doctorSn +
          '" data-doctorofficename="' +
          item.config.doctorOfficeName +
          '" data-showdoctorofficename="' +
          showDoctorOfficeName +
          '" data-doctoruid="' +
          item.doctorUid +
          '" data-deptid="' +
          item.hosDeptId +
          '" data-hosid="' +
          item.hospitalId +
          '" data-jjsq="0" data-deptname="' +
          item.deptName +
          '" data-hosname="' +
          item.hosName +
          '" data-provinceid="' +
          item.provinceId +
          '" data-provincename="' +
          item.provinceName +
          '" data-cityid="' +
          item.cityId +
          '" data-cityname="' +
          item.cityName +
          '" data-hosPause="' +
          item.config.hosPause +
          '" data-ghstatus="' +
          1 +
          '">';
        _html +=
          '<div class="gdept-tit auto-cl-gh-deptname-' +
          item.hospitalId +
          ' gnone" >';
        _html += '<i class="tag-dept"></i>' + item.deptName;
        _html +=
          '<a href="javascript:;" onclick="JkgjService.jkgj_toUnCooperationRegister(' +
          item.hospitalId +
          "," +
          needVip +
          ',this);" class="gdept-btn g-blue reser-tip2">挂号</a>';
        _html += "</div>";
        _html += '<div class="auto-gdept-jkgjsd"></div>';
        _html += "</li>";
      } else if (item.config.ghState == 3) {
        _html +=
          '<li class="auto-cl-gh-deptbox" data-docsn="' +
          item.doctorSn +
          '" data-doctorofficename="' +
          item.config.doctorOfficeName +
          '" data-showdoctorofficename="' +
          showDoctorOfficeName +
          '" data-doctoruid="' +
          item.doctorUid +
          '" data-deptid="' +
          item.hosDeptId +
          '" data-hosid="' +
          item.hospitalId +
          '" data-jjsq="0" data-deptname="' +
          item.deptName +
          '" data-hosname="' +
          item.hosName +
          '" data-provinceid="' +
          item.provinceId +
          '" data-provincename="' +
          item.provinceName +
          '" data-cityid="' +
          item.cityId +
          '" data-cityname="' +
          item.cityName +
          '" data-hosPause="' +
          item.config.hosPause +
          '" data-ghstatus="' +
          1 +
          '">';
        _html +=
          '<div class="gdept-tit auto-cl-gh-deptname-' +
          item.hospitalId +
          ' gnone" >';
        _html += '<i class="tag-dept"></i>' + item.deptName;
        _html +=
          '<a href="javascript:;" onclick="dyyService.tipYydj(' +
          item.hospitalId +
          ',this);" class="gdept-btn g-blue reser-tip2">挂号</a>';
        _html += "</div>";
        _html += '<div class="auto-gdept-jkgjsd"></div>';
        _html += "</li>";
      }

      if (i == maxIndex || item.hospitalId != that.deptList[i + 1].hospitalId) {
        _html += "</ul>";
        _html += "</div>";
        tempHosId = item.hospitalId;
      }
    });
    $("#sched-ghbox").html(_html);
    assistanceService.load(docSns);
    //默认展示排班
    var divobj = $("#sched-ghbox").find("li.auto-cl-gh-deptbox").eq(openIndex);
    if (divobj.length == 1 && deptId == $(divobj).attr("data-deptid")) {
      $(divobj).children(".gdept-tit").addClass("gshow");
      $(divobj).children(".gdept-list").slideDown(200);
      var docsn = $(divobj).attr("data-docsn");
      var deptid = $(divobj).attr("data-deptid");
      var hosid = $(divobj).attr("data-hosid");
      var divid = $(divobj).children(".gdept-list").first().attr("id");
      GuaHaoService.getArrangeWater(docsn, deptid, hosid, divid);
    }
    that.getDoctorHasArr(docSns);
    getHospitalIdentState(hosids);
    GhMarketingService.load(hosids);
    //显示排班
    $(".c-main").on("click", ".gdept-tit", function () {
      var isshow = $(this).hasClass("gshow");
      var grad = $(this).siblings(".gdept-list").is("div");
      if (grad) {
        if (isshow) {
          $(this).removeClass("gshow");
          $(this).siblings(".gdept-list").slideUp(200);
          $(this).children(".tag-refresh").remove();
        } else {
          $(this).addClass("gshow");
          $(this).siblings(".gdept-list").slideDown(200);
          var divobj = $(this).parents("li.auto-cl-gh-deptbox");
          var docsn = $(divobj).attr("data-docsn");
          var deptid = $(divobj).attr("data-deptid");
          var hosid = $(divobj).attr("data-hosid");
          var divid = $(this).siblings(".gdept-list").attr("id");
          GuaHaoService.getArrangeWater(docsn, deptid, hosid, divid);
        }
      }
    });
    //刷新排班
    $(".c-main").on("click", ".tag-refresh", function () {
      //$(this).parents().addClass('gshow');
      //$(this).parents().siblings('.gdept-list').slideDown(200);
      var divobj = $(this).parents("li.auto-cl-gh-deptbox");
      var docsn = $(divobj).attr("data-docsn");
      var deptid = $(divobj).attr("data-deptid");
      var hosid = $(divobj).attr("data-hosid");
      var divid = $(this).parents().siblings(".gdept-list").attr("id");
      GuaHaoService.refreshNumbers(docsn, deptid, hosid, divid);
      return false;
    });
    that.initDefaultOpen();
  },
  initDefaultOpen: function () {
    var hosIdArr = [];
    $("#sched-ghbox")
      .find("li.auto-cl-gh-deptbox")
      .each(function () {
        var hosid = $(this).attr("data-hosid");
        if (!hosIdArr.includes(hosid)) {
          hosIdArr.push(hosid);
        }
      });
    if (hosIdArr.length > 0) {
      var hosIds = hosIdArr.join(",");
      $.ajax({
        type: "post",
        url: getRootPath() + "/do/baseInfo/queryYuYueHosConfigsByHosIds",
        data: {
          hospitalIds: hosIds,
          type: 51,
          time: new Date().getTime(),
        },
        beforeSend: function () {},
        success: function (d) {
          if (d.Code == 10000 && d.Result && d.Result.length > 0) {
            var defaultOpenHos = [];
            $.each(d.Result, function (index, item) {
              if (item.value == 1) {
                defaultOpenHos.push(item.hospitalId);
              }
            });
            if (defaultOpenHos.length > 0) {
              $("#sched-ghbox")
                .find(".gdept-tit")
                .each(function () {
                  var isshow = $(this).hasClass("gshow");
                  var grad = $(this).siblings(".gdept-list").is("div");
                  if (grad) {
                    if (!isshow) {
                      var divobj = $(this).parents("li.auto-cl-gh-deptbox");
                      var docsn = $(divobj).attr("data-docsn");
                      var deptid = $(divobj).attr("data-deptid");
                      var hosid = $(divobj).attr("data-hosid");
                      var divid = $(this).siblings(".gdept-list").attr("id");
                      if (defaultOpenHos.includes(parseInt(hosid))) {
                        $(this).addClass("gshow");
                        $(this).siblings(".gdept-list").slideDown(200);
                        GuaHaoService.getArrangeWater(
                          docsn,
                          deptid,
                          hosid,
                          divid
                        );
                      }
                    }
                  }
                });
            }
          }
        },
      });
    }
  },
  //医生可预约排班流水
  getArrangeWater: function (doctsn, deptId, ghHosId, divobj) {
    var that = this;
    $.ajax({
      url: "../do/doctorArrange/getArrangeWater",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      cache: false,
      data: {
        doctorSn: doctsn,
        hospitalId: ghHosId,
        channelId: channelId,
      },
      beforeSend: function () {
        $("#" + divobj)
          .parent()
          .find(".fud-detail-left")
          .remove();
        localLoad(divobj);
      },
      success: function (objJson) {
        try {
          if (objJson) {
            if (parseInt(objJson.Code) === 10000) {
              var listHtml = "";
              var btnClick = ""; //按钮点调用方法
              var btnCol = "g-blue"; //按钮背景颜色
              var btnText = "";
              var txtFHTime = ""; //预计放号时间
              var feeName = objJson.SpecialName;
              var dateNow = objJson.DateNow;
              var ztCon = ""; //医院暂停预约以后的温馨提示
              var firstfeeCon = "";
              //var appurlParams = that.getAppUrlParam();
              var firstCanRegisterStr = "";
              var canRegisterArr = objJson.Result.filter(function (d) {
                return d.ArrangeStatus == 1 && d.availablenum > 0;
              });
              if (canRegisterArr.length > 0) {
                var canRegisterObj = canRegisterArr[0];
                firstCanRegisterStr =
                  canRegisterObj.registerdate +
                  " " +
                  GetTimeName(canRegisterObj.timeid);
              }
              for (var i = 0, count = objJson.Result.length; i < count; i++) {
                var item = objJson.Result[i];
                var arrageStatus = parseInt(item.ArrangeStatus);
                //var registerDate = StrToDateDot(item.registerdate);
                var parameObj =
                  "{registerdate:'" +
                  item.registerdate +
                  "',timeid:" +
                  item.timeid +
                  ",ArrangeID:" +
                  item.ArrangeID +
                  ",specialprice:" +
                  item.specialprice +
                  ",ghfeeway:" +
                  item.ghfeeway +
                  ",ModeId:" +
                  item.ModeId +
                  ",GhFee:" +
                  item.GhFee +
                  ",AllFee:" +
                  item.AllFee +
                  ",availablenum:" +
                  item.availablenum +
                  ",typeName:'" +
                  item.TypeName +
                  "',isparttime:'" +
                  item.IsPartTime +
                  "',IsCheckOrder:'" +
                  item.IsCheckOrder +
                  "',securityDeposit:" +
                  item.securityDeposit +
                  ",feeName:'" +
                  feeName +
                  "',UnOpened:" +
                  item.UnOpened +
                  ",FHDays:'" +
                  item.FHDays +
                  "',FHTimes:'" +
                  item.FHTimes +
                  "',hosname:'" +
                  item.hosname +
                  "',deptname:'" +
                  item.deptname +
                  "',hosid:'" +
                  item.hosid +
                  "',marketing:" +
                  item.marketing +
                  "}";
                var showServiceBwhz = 0;
                var currShowServiceBwhz = 0;
                $.each(doctorInfo, function (index, item) {
                  if (item.doctorSn == doctsn) {
                    showServiceBwhz = item.config.showServiceBwhz;
                    return false;
                  }
                });
                ////挂号状态：1正常；2 停诊；4 暂停预约
                if (arrageStatus == 1) {
                  //正常
                  if (item.OverTime == 1) {
                    btnCol = " g-gray";
                    btnClick = "";
                    btnText = "截止预约";
                  } else if (item.availablenum <= 0) {
                    if (
                      parseInt(item.houBuClosed) == 1 ||
                      item.registerdate == dateNow
                    ) {
                      //部分医院无号源，不允许候补，展示“已约满”，并且点击无效果
                      if (
                        isNotNull(assistanceService.assistanceInfo) &&
                        assistanceService.assistanceInfo.regEmptyState == 1
                      ) {
                        btnCol = "g-blue";
                        btnClick =
                          "onclick =\"assistanceService.arrangeFullShow('" +
                          deptId +
                          "','" +
                          firstCanRegisterStr +
                          "');\"";
                        btnText = "预约";
                      } else if (showServiceBwhz == 1) {
                        //服务包班外会诊入口
                        btnCol = " g-yellow banwBtn";
                        btnClick = 'onclick ="toServiceList();"';
                        btnText = "班外会诊";
                        currShowServiceBwhz = 1;
                      } else {
                        btnCol = " g-gray";
                        btnClick = "";
                        btnText = "约满";
                      }
                    } //如果无号源且允许候补，展示“候补”，点击后跳转到候补的站点，进行候补操作
                    else {
                      if (
                        isNotNull(assistanceService.assistanceInfo) &&
                        assistanceService.assistanceInfo.hbGuideState == 1
                      ) {
                        btnCol = "g-blue";
                        btnClick =
                          'onclick ="assistanceService.arrangeFullHbShow(this,' +
                          parameObj +
                          ",'HB','" +
                          deptId +
                          "','" +
                          firstCanRegisterStr +
                          "')\"";
                        btnText = "预约";
                      } else {
                        btnCol = " g-yellow";
                        btnClick =
                          'onclick ="GuaHaoService.onDoctorGhAffirm(this,' +
                          parameObj +
                          ",'HB')\"";
                        btnText = "约满候补";
                      }
                    }
                  } else {
                    btnCol = "g-blue";
                    btnClick =
                      'onclick ="GuaHaoService.onDoctorGhAffirm(this,' +
                      parameObj +
                      ",'GH')\""; //预约
                    if (item.diseaseAudit == "2") {
                      btnText = "预约申请";
                      btnClick =
                        'onclick ="GuaHaoService.onDoctorGhAffirm(this,' +
                        parameObj +
                        ",'Apply')\"";
                    } else if (String(item.UnOpened) == "true") {
                      btnText = "预约登记";
                      txtFHTime = "预计" + item.FHTimes + "放号";
                    } else if (item.registerdate == dateNow) {
                      btnText = "当天号";
                      btnClick =
                        'onclick ="GuaHaoService.onDoctorGhToday(this,' +
                        parameObj +
                        ')"';
                    } else if (parseInt(item.numCountVisible) === 1) {
                      //0非实时；1代表实时
                      btnText = "预约";
                    } else {
                      btnText = "剩" + item.availablenum + "个";
                    }
                  }
                } else if (arrageStatus === 4) {
                  //暂停预约
                  if (ztCon === "" && item.ArrRemark) {
                    ztCon = item.ArrRemark;
                  }
                  btnClick = "";
                  btnCol = " g-gray";
                  btnText = "暂停预约";
                } else {
                  //停诊
                  btnClick = "";
                  btnCol = " g-gray";
                  btnText = "停诊";
                }
                var feeRemark =
                  parseInt(item.ghfeeway) === 0 || parseInt(item.ghfeeway) === 1
                    ? "(到院付)"
                    : "";
                var feeConText = "";
                if (isNotNull(item.TypeName)) {
                  feeConText += item.TypeName;
                }
                if (currShowServiceBwhz == 1) {
                  feeConText += '<span class="c-ff8100">(已约满)</span>';
                }
                var feeCon = isNotNull(feeConText)
                  ? "<p>" + feeConText + "</p>"
                  : "";
                if (item.specialprice > 0) {
                  feeCon +=
                    '<p class="c-909090">' +
                    feeName +
                    '<span class="c-ff8100">' +
                    item.specialprice / 100 +
                    "</span>元" +
                    feeRemark +
                    "</p>";
                }
                if (firstfeeCon.length <= 0) {
                  firstfeeCon = that.showRegType(item.TypeName, ghHosId);
                }
                var hasNum =
                  parseInt(item.availablenum) > 0 &&
                  item.ArrangeStatus == 1 &&
                  item.OverTime != 1;
                listHtml +=
                  "<dl " +
                  (i >= 5 ? 'style="display: none" data-more="1" ' : "") +
                  " arrangeid=" +
                  item.ArrangeID +
                  " hasnum=" +
                  hasNum +
                  " " +
                  btnClick +
                  ">";
                listHtml +=
                  "<dd><p>" +
                  item.registerdate +
                  '</p><p class="c-909090">星期' +
                  GetWeek(item.registerdate) +
                  GetTimeName(item.timeid) +
                  "</p></dd>";
                listHtml += '<dd class="ddflex1">' + feeCon + "</dd>";
                listHtml +=
                  '<dd><a href="javascript:;" class="gdept-btn ' +
                  btnCol +
                  '">' +
                  btnText +
                  '</a><div class="c-f12 c-ff8100">' +
                  txtFHTime +
                  "</div></dd>";
                listHtml += "</dl>";

                //listHtml += '<li class="c-list-text c-list-cover" ' + btnClick + '><div class="c-list-info"><h4>' + item.registerdate + '</h4><p>' + weekArr2[registerDate.getDay()] + itemNaArr[item.timeid] + '</p></div><div class="c-list-info midsched">' + feeCon + '</div><div class="c-list-info"><a href="javascript:;" class="sched-btn' + btnCol + '">' + btnText + '</a><div class="c-f12 c-ff8100">' + txtFHTime + '</div></div></li>';
              }
              if (objJson.Result.length > 5) {
                listHtml +=
                  '<div class="gdept-lmore"> <a href="javascript:;" onclick="GuaHaoService.showMore(this);">查看更多排班</a></div>';
              }
              if (listHtml === "") {
                var noplanHtml = '<div class="gdept-noplan">';
                noplanHtml +=
                  '<div class="noplan-box">暂无本科室号源,可选择该医生其他出诊点</div>';
                noplanHtml += "</div>";
                $("#" + divobj).html(noplanHtml);
              } else {
                $("#" + divobj).html(listHtml);
              }
              try {
                PreRemindService.getPreArrange(
                  doctsn,
                  deptId,
                  ghHosId,
                  divobj,
                  firstfeeCon,
                  objJson.Result,
                  null,
                  objJson.Result.length > 5 ? true : false
                ); //显示放号提醒
                var hosPause = $("#" + divobj)
                  .parent()
                  .attr("data-hospause");
                if (hosPause != 1) {
                  //非暂停预约
                  that.showRefreshNum(divobj, objJson.Result);
                }
                doctorMarketingService.load(divobj);
              } catch (e) {}
            } else if (parseInt(objJson.Code) === -111111) {
              comNomess({ pack: divobj, con: objJson.Message, mid: true });
            }
          } else {
            comNomess({
              pack: divobj,
              con: "排班信息加载失败",
              btnValue: "重新加载",
              btn: function () {
                that.getArrangeWater(doctsn, deptId, ghHosId, divobj);
              },
            });
          }
        } catch (ex) {
          try {
            jkzlAn.track(
              "pubserbtn1020",
              {
                message: ex.message,
                stack: ex.stack,
              },
              "getArrangeWater"
            );
          } catch (e) {}
          comNomess({
            pack: divobj,
            con: "排班信息加载失败，请刷新重试",
            btn: function () {
              location.reload();
            },
            btnValue: "刷新重试",
          });
        }
        $(".no-mess-tb").css("margin-top", "0px");
        //号源宽度
        that.fudWide();
      },
      error: function (xmlHttpRequest, textStatus) {
        if (textStatus === "timeout") {
          comNomess({
            pack: divobj,
            con: "排班信息加载失败",
            btnValue: "重新加载",
            btn: function () {
              that.getArrangeWater(doctsn, deptId, ghHosId, divobj);
            },
          });
        } else {
          comNomess({
            pack: divobj,
            con: "网络异常，请检查您的网络或刷新重试",
          });
        }
      },
    });
  },
  showMore: function (obj) {
    $(obj)
      .parent()
      .parent()
      .find("dl[data-more=1]")
      .each(function () {
        $(this).show();
      });
    $(obj).parent().hide();
  },
  //获取医生排班状态
  getDoctorHasArr: function (docSns) {
    var showArrangeFirst = deptId != ""; //是否已展示第一个排班
    $.ajax({
      url: "../do/doctorArrange/getDoctorHasArrange",
      timeout: 8000,
      type: "POST",
      dataType: "json",
      cache: false,
      data: {
        doctorSns: docSns,
      },
      success: function (json) {
        if (json.Code == 10000 && json.Result) {
          $("#sched-ghbox .grad-dept li.auto-cl-gh-deptbox").each(function () {
            var schedhold = $(this);
            var docsn = $(schedhold).data("docsn");
            var num = null;
            for (var i = 0; i < json.Result.length; i++) {
              if (json.Result[i].doctorSn == docsn) {
                num = json.Result[i].count;
                break;
              }
            }
            if (parseInt(num) == 0) {
              $(schedhold).attr("data-hasarr", "0");
              $(schedhold)
                .find(".gdept-tit")
                .append('<i class="tag-nograd">暂无排班</i>');
            } else if (parseInt(num) > 0) {
              $(schedhold)
                .find(".gdept-tit")
                .append('<i class="tag-booked">可预约</i>');
              if (!showArrangeFirst) {
                showArrangeFirst = true;
                $(schedhold).children(".gdept-tit").addClass("gshow");
                $(schedhold).children(".gdept-list").slideDown(200);
                var deptid = $(schedhold).attr("data-deptid");
                var hosid = $(schedhold).attr("data-hosid");
                var divid = $(schedhold)
                  .children(".gdept-list")
                  .first()
                  .attr("id");
                GuaHaoService.getArrangeWater(docsn, deptid, hosid, divid);
              }
            }
          });
        }
      },
      error: function () {},
    });
  },
  //当天号提示
  HosNoticeForTodayNum: new Object(),
  //预约申请
  onDoctorGhApply: function (obj, parameObj) {
    if (!CheckLogin() || !CheckBindUser()) {
      return;
    }
    myLayer.alert(
      "您的预约申请将转登记页面，请核实后提交，由健康管家视您的权益不同为您积极协调并联系您。",
      "2000",
      function () {
        var ghdatadiv = $(obj).parents("li.auto-cl-gh-deptbox");
        var ghprodocsn =
          typeof parameObj.doctorSn == "undefined"
            ? $(ghdatadiv).attr("data-docsn")
            : parameObj.doctorSn;
        var lshosid = $(ghdatadiv).attr("data-hosid");
        var ghprohosid =
          typeof lshosid == "undefined" ? $("#GH_HosID").val() : lshosid;
        var ghprodeptid =
          typeof parameObj.deptId == "undefined"
            ? $(ghdatadiv).attr("data-deptid")
            : parameObj.deptId;
        var arrangeId = parameObj.ArrangeID;
        location.href =
          "../accurate/notice.html?platformType=" +
          platformType +
          "&hospitalId=" +
          ghprohosid +
          "&deptId=" +
          ghprodeptid +
          "&doctorSn=" +
          ghprodocsn +
          "&arrangeId=" +
          arrangeId;
      }
    );
  },
  //预约确认
  onDoctorGhAffirm: function (obj, parameObj, fun, todayNumMsg) {
    if (!CheckLogin() || !CheckBindUser()) {
      return;
    }
    var msg = "";
    if (isManyDept && GuaHaoService.checkGhAffirm(parameObj.hosid)) {
      var ghdatadiv = $(obj).parents("li.auto-cl-gh-deptbox");
      var showDoctorOfficeName = $(ghdatadiv).attr("data-showdoctorofficename"); //是否显示医生诊室
      var doctorOfficeName = $(ghdatadiv).attr("data-doctorofficename"); //医生诊室名称
      if (
        fun == "GH" &&
        isNotNull(showDoctorOfficeName) &&
        showDoctorOfficeName == 1
      ) {
        msg +=
          "该医生目前在多地点出诊。您选择的是" +
          doctorOfficeName +
          "-" +
          parameObj.deptname +
          "，确定要" +
          (fun == "GH" ? "预约" : fun == "HB" ? "候补" : "申请") +
          "吗？";
      } else {
        msg +=
          "该医生目前在多地点出诊。您选择的是" +
          parameObj.hosname +
          "-" +
          parameObj.deptname +
          "，确定要" +
          (fun == "GH" ? "预约" : fun == "HB" ? "候补" : "申请") +
          "吗？";
      }
    }
    if (isNotNull(todayNumMsg)) {
      if (isNotNull(msg)) {
        msg += "<br/>";
      }
      msg += todayNumMsg;
    }
    if (isNotNull(msg)) {
      myLayer.confirm({
        title: "提示",
        con: msg,
        ok: function () {},
        okValue: "取消",
        cancel: function () {
          if (fun == "Apply") {
            GuaHaoService.onDoctorGhApply(obj, parameObj);
          } else {
            GuaHaoService.onDoctorGh(obj, parameObj, fun);
          }
        },
        cancelValue: "确认",
      });
    } else {
      if (fun == "Apply") {
        GuaHaoService.onDoctorGhApply(obj, parameObj);
      } else {
        GuaHaoService.onDoctorGh(obj, parameObj, fun);
      }
    }
  },
  checkGhAffirm: function (hosid) {
    var ret = false;
    $.ajax({
      type: "post",
      async: false,
      url: getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
      data: {
        hospitalId: hosid,
        type: 35,
        time: new Date().getTime(),
      },
      beforeSend: function () {},
      error: function () {
        myLayer.clear();
        myLayer.alert("网络繁忙,请刷新后重试!");
      },
      success: function (d) {
        myLayer.clear();
        if (d.Code == 10000) {
          if (d.Result.value == 1) {
            ret = true;
          }
        }
      },
    });
    return ret;
  },
  //当天号预约
  onDoctorGhToday: function (obj, parameObj) {
    var ghdatadiv = $(obj).parents("li.auto-cl-gh-deptbox");
    var lshosid = $(ghdatadiv).attr("data-hosid");
    var ghprohosid =
      typeof lshosid == "undefined" ? $("#GH_HosID").val() : lshosid;
    var todayNumMsg = GuaHaoService.HosNoticeForTodayNum[ghprohosid];
    if (todayNumMsg != null) {
      GuaHaoService.onDoctorGhAffirm(obj, parameObj, "GH", todayNumMsg);
    } else {
      $.ajax({
        url: "../do/content/queryHospitalNotice",
        timeout: 8000,
        dataType: "json",
        type: "POST",
        async: false,
        data: {
          resId: 6,
          resUid: ghprohosid,
          isPop: 1,
        },
        success: function (json) {
          if (
            json.Code == 10000 &&
            json.Result != null &&
            json.Result.length > 0
          ) {
            GuaHaoService.HosNoticeForTodayNum[ghprohosid] =
              json.Result[0].articleContent;
          } else {
            GuaHaoService.HosNoticeForTodayNum[ghprohosid] = "";
          }
          GuaHaoService.onDoctorGhAffirm(
            obj,
            parameObj,
            "GH",
            GuaHaoService.HosNoticeForTodayNum[ghprohosid]
          );
        },
        error: function () {
          GuaHaoService.onDoctorGhAffirm(obj, parameObj, "GH");
        },
      });
    }
  },
  //预约
  onDoctorGh: function (obj, parameObj, fun) {
    var that = this;
    if (!CheckLogin() || !CheckBindUser()) {
      return;
    }
    //console.log(JSON.stringify(parameObj));
    var ghdatadiv = $(obj).parents("li.auto-cl-gh-deptbox");
    var doctorSn = $(ghdatadiv).attr("data-docsn"); //医生sn
    var hospitalId = $(ghdatadiv).attr("data-hosid"); //医院id
    var deptId = $(ghdatadiv).attr("data-deptid"); //科室id
    var doctorUid = $(ghdatadiv).attr("data-doctoruid"); //医生uid
    var showDoctorOfficeName = $(ghdatadiv).attr("data-showdoctorofficename"); //是否显示医生诊室
    var doctorOfficeName = $(ghdatadiv).attr("data-doctorofficename"); //医生诊室名称
    if (doctorSn == null) doctorSn = parameObj.doctorSn;
    if (hospitalId == null) hospitalId = parameObj.hosId;
    if (deptId == null) deptId = parameObj.deptId;
    var arrangeId = parameObj.ArrangeID;
    if (fun == "HB" && !HouBuGuide(hospitalId, deptId, doctorSn, parameObj)) {
      return;
    }
    var params =
      "?platformType=" +
      platformType +
      "&hospitalId=" +
      hospitalId +
      "&deptId=" +
      deptId +
      "&doctorSn=" +
      doctorSn +
      "&arrangeId=" +
      arrangeId +
      "&utm_source=" +
      utm_source;
    if (fun == "HB") {
      location.href = "../houbu/hbRegister.html" + params;
    } else {
      if (isNotNull(showDoctorOfficeName) && showDoctorOfficeName == 1) {
        params +=
          "&showDoctorOfficeName=1&doctorOfficeName=" +
          encodeURIComponent(doctorOfficeName);
      }
      saveSiteTrack("bus010btn1110", {
        originhosid: doctorInfoFirst.hospitalId,
        doctorid: doctorUid,
        deptid: deptId,
      });

      var regUri =
        location.protocol +
        StrYyghUrl +
        "/appoint/register/registerOrder.html" +
        params;
      if (
        channelId == "1000025" ||
        channelId == "1000007" ||
        channelId == "9000394" ||
        channelId == "9000920" ||
        channelId == "9000376" ||
        channelId == "9000370" ||
        channelId == "9000371" ||
        channelId == "9001064" ||
        channelId == "9000415" ||
        channelId == "9000241"
      ) {
        var ncovUrl = that.checkHosNcov(hospitalId);
        if (!isDebug && doctorId == "710771796") {
          ncovUrl =
            StrFycombatUrl +
            "/SXForm/BLNYMSQEpidemic?isGuahao=1&channelId={channelId}&tourl={tourl}";
        }
        if (isNotNull(ncovUrl)) {
          ncovUrl = ncovUrl
            .replace("{channelId}", channelId)
            .replace("{hospitalId}", hospitalId)
            .replace("{tourl}", encodeURIComponent(regUri));
          location.href = ncovUrl;
        } else {
          location.href = "../register/registerOrder.html" + params;
        }
      } else {
        location.href = "../register/registerOrder.html" + params;
      }
    }
  },
  checkHosNcov: function (hosid) {
    var ret = false;
    $.ajax({
      type: "post",
      async: false,
      url: getRootPath() + "/do/baseInfo/queryYuYueHosConfigs",
      data: {
        hospitalId: hosid,
        type: 48,
        time: new Date().getTime(),
      },
      beforeSend: function () {},
      error: function () {
        myLayer.clear();
        myLayer.alert("网络繁忙,请刷新后重试!");
      },
      success: function (d) {
        myLayer.clear();
        if (d.Code == 10000) {
          if (d.Result.value == 1 || d.Result.value == 2) {
            if (isNotNull(d.Result.remark) && IsURL(d.Result.remark)) {
              var ncovUrl = d.Result.remark;
              ret = ncovUrl;
            }
          }
        }
      },
    });
    return ret;
  },
  //获取appurl参数
  getAppUrlParam: function () {
    // doctorSn=<医生工号>&hospitalId=<医院id>&hosDeptId=<科室代码>&channelId=<渠道ID>&openId=<微信唯一ID>&accountSn=<账户编号>&cardId=<健康之路内部卡号>
    var doctorSn = $("#SelectedDocSn").val();
    var hosDeptId = $("#hosDeptId").val();
    var hospitalId = $("#GH_HosID").val();
    var channelId = $("#hiChannelId").val();
    var openId = $("#hiOpenId").val();
    var accountSn = $("#hiAccountsn").val();
    var cardId = $("#hiCardid").val();
    return (
      "doctorSn=" +
      doctorSn +
      "&hospitalId=" +
      hospitalId +
      "&hosDeptId=" +
      hosDeptId +
      "&channelId=" +
      channelId +
      "&openId=" +
      openId +
      "&accountSn=" +
      accountSn +
      "&cardId=" +
      cardId
    );
  },
  //url sourceId参数修改为选定医院id
  replaceHosId: function (param, hosId) {
    //var param = "platformType=14&sourceType=1&sourceId=31";
    if (hosId != null && hosId != "" && hosId != $("#HosID").val()) {
      param = param.replace(/&sourceid=/gi, "&fromid=") + "&sourceid=" + hosId;
    }
    return param;
  },
  //不是会员弹窗
  hbVipShow: function () {
    //点击【候补】按钮提交pv
    var isvip = GetUserIsVip();
    if (!isvip) {
      $(".vip-pop,.vip-pop-mb").show();
    }
    return isvip;
  },
  //号源宽度
  fudWide: function () {
    var wW = $(window).width() / 7.7;
    $(".fud-detail-left").width(wW + 2);
    $(".fud-detail td>div").width(wW);
  },
  groupDeptData: function () {
    //对数据进行分组
    var ndocdeptdata = doctordeptdata;
    if (ndocdeptdata != null && ndocdeptdata.length > 0) {
      var newHosDeptDate = [];
      var newHosDept_HosId_Data = [];
      for (var i = 0, len = ndocdeptdata.length; i < len; i++) {
        var dndocdept = ndocdeptdata[i];
        var isd = false;
        if (newHosDeptDate.length > 0) {
          for (var j = 0, jlen = newHosDeptDate.length; j < jlen; j++) {
            var jitem = newHosDeptDate[j];
            if (jitem.hosid == dndocdept.hosid) {
              jitem.deptdata.push(dndocdept);
              isd = true;
            }
          }
        }
        if (!isd) {
          newHosDeptDate.push({
            hosid: dndocdept.hosid,
            hosname: dndocdept.hosname,
            deptdata: [dndocdept],
          });
        }
      }

      this.deptList = newHosDeptDate;
    }
  },
  //显示门诊类型
  showRegType: function (TypeName, hosId) {
    //hosId == 31 || hosId == 1032369 || hosId == 1565
    if (TypeName != null && TypeName != "") {
      return "<p>" + TypeName + "</p>";
    }
    return "";
  },
  //刷新
  refreshNumbers: function (doctsn, deptId, ghHosId, divId) {
    var divObj = $("#" + divId);
    var length = divObj.children().length;
    var arrangeIds = "";
    for (var i = length; i > 0; i--) {
      var arrId = divObj
        .children()
        .eq(i - 1)
        .attr("arrangeid");
      if (arrId != null && arrId != "") {
        arrangeIds += (arrangeIds != "" ? "," : "") + arrId;
      }
    }
    if (arrangeIds == "") {
      return;
    }
    var that = this;
    $.ajax({
      url: "../do/doctorArrange/refreshNumbers",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      cache: false,
      data: {
        arrangeIds: arrangeIds,
        hospitalId: ghHosId,
        doctorSn: doctsn,
        channelId: channelId,
        loginId: loginUserInfo.loginId == null ? "" : loginUserInfo.loginId,
      },
      beforeSend: function () {},
      success: function (objJson) {
        if (objJson) {
          if (objJson.Code == 10000) {
            that.getArrangeWater(doctsn, deptId, ghHosId, divId);
            myLayer.alert("已刷新号源", 1000);
          } else if (objJson.Code == -1) {
            myLayer.alert("请10秒后再点击刷新", 2000);
          }
        }
      },
      error: function (xmlHttpRequest, textStatus) {},
    });
  },
  //显示刷新
  showRefreshNum: function (divobj, arrangeRet) {
    var arrangeNoNumber = arrangeRet.length > 0;
    for (var i in arrangeRet) {
      if (
        parseInt(arrangeRet[i].availablenum) > 0 &&
        arrangeRet[i].ArrangeStatus == 1 &&
        arrangeRet[i].OverTime != 1
      ) {
        arrangeNoNumber = false;
      }
    }
    var obj = $("#" + divobj);
    if (arrangeNoNumber) {
      if (obj.siblings().children(".tag-refresh").length < 1) {
        obj
          .siblings()
          .append(
            '<a class="tag-refresh"><i class="iconfont icon-shuaxin"></i>刷新</a>'
          );
      }
    } else {
      obj.siblings().children(".tag-refresh").remove();
    }
  },
};

//医生其他服务
var DoctorService = {
  //医生对外备注
  getRemark: function () {
    var that = this;
    $.ajax({
      url: "../do/doctorService/getDoctorRemark",
      timeout: 8000,
      dataType: "json",
      type: "post",
      async: true,
      cache: true,
      data: {
        typeId: 3,
        resId: doctorId,
      },
      beforeSend: function () {},
      success: function (objJson) {
        var remark;
        if (objJson) {
          if (parseInt(objJson.Code) === 10000) {
            remark = objJson.remark;
            if (typeof remark != "undefined" && $.trim(remark).length > 0) {
              that.showRemark(remark, doctorId, objJson.opertime);
            }
          }
        }
      },
      error: function () {
        $("#hos-notice").hide();
      },
    });
  },
  //显示备注
  showRemark: function (remark, doctorid, opertime) {
    $("#hos-notice-pop")
      .find("div.agree-box")
      .html(remark.replace(/\r\n/g, "<br/>").replace(/\n/g, "<br/>"));
    //读取storage中的记录
    var storageName = "MDOCTOR_DoctorIndex_Record_RemarkShow";
    var ishasshow = false;
    var issavesplic = -1;
    var showRemarkList = localStorage.getItem(storageName);
    if (showRemarkList != undefined && $.trim(showRemarkList).length > 3) {
      showRemarkList = eval("(" + unescape(showRemarkList) + ")");
      $.each(showRemarkList, function (i, item) {
        if (item.d == doctorid && item.t == opertime) {
          ishasshow = true;
          return false;
        }
        return true;
      });
    } else {
      showRemarkList = [];
    }
    if (channelId == "9000693") {
      ishasshow = true;
    }
    if (!ishasshow && $.trim(remark) != "" && $.trim(doctorid) != "") {
      var hisitme = { d: doctorid, t: "" + opertime + "" };
      if (issavesplic > -1) {
        showRemarkList.splice(issavesplic, 1, hisitme);
      } else {
        showRemarkList.push(hisitme);
      }
      //保存
      var infojson = escape(JSON.stringify(showRemarkList));
      try {
        localStorage.setItem(storageName, infojson);
      } catch (e) {
        localStorage.removeItem(storageName);
        localStorage.setItem(storageName, infojson);
      }
    }
    if (!ishasshow) {
      $(".agreepop,.agreemb").show();
      $(".agree-box").height($(".agreepop").height());
    }
  },
};
//放号提醒
var PreRemindService = {
  //获取放号提醒预排班
  getPreArrange: function (
    doctsn,
    deptId,
    ghHosId,
    divobj,
    feecon,
    arrangeRet,
    lastRegDate,
    inMore
  ) {
    //        if (!isAccountBind)
    //            return;
    var t = this;
    $.ajax({
      url: "../do/doctorArrange/getPreArrangeInfo",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      async: true,
      cache: false,
      data: {
        hospitalId: ghHosId,
        deptId: deptId,
        doctorSn: doctsn,
        channelId: channelId,
      },
      success: function (objJson) {
        if (objJson.Code == 10000 && objJson.Result.length > 0) {
          for (var i = 0, count = objJson.Result.length; i < count; i++) {
            var item = objJson.Result[i];
            if (!t.CheckHasArrange(item, arrangeRet)) {
              item["deptid"] = deptId;
              var listHtml =
                "<dl " +
                (inMore ? 'style="display: none" data-more="1" ' : "") +
                ' id="arrangeinfoid_' +
                item.arrangeinfoid +
                '"><dd><p>' +
                item.registerdate +
                '</p><p class="c-909090">星期' +
                GetWeek(item.registerdate) +
                GetTimeName(item.timeid) +
                '</p></dd><dd class="ddflex1">' +
                "</dd><dd><a href='javascript:PreRemindService.showpre(" +
                JSON.stringify(item) +
                ');\' class="gdept-btn g-yellow">放号提醒</a></dd></dl>';
              if ($("#" + divobj).children("[data-type='jjsq']").length > 0) {
                $("#" + divobj)
                  .children("[data-type='jjsq']")
                  .eq(0)
                  .before(listHtml);
              } else {
                $("#" + divobj).append(listHtml);
              }
            }
          }
        }
      },
    });
  },
  //检查排班中是否已列出
  CheckHasArrange: function (item, arrangeRet) {
    for (var i in arrangeRet) {
      if (
        item.registerdate == arrangeRet[i].registerdate &&
        item.timeid == arrangeRet[i].timeid
      ) {
        return true;
      }
    }
    return false;
  },
  showpre: function (item) {
    var that = this;
    if (channelId == "9000184") {
      that.showpreZFB(item);
    } else {
      that.showpreBase(item);
    }
  },
  //显示支付宝放号提醒窗
  showpreZFB: function (item) {
    var notshopvip = false;
    if (
      item.memberreminder == 2 ||
      channelId == "9000282" ||
      channelId == "9000283"
    ) {
      //不需要会员营销
      notshopvip = true;
    } else {
      notshopvip = GetUserIsVip();
    }
    var istation =
      typeof item.outstuation != "undefined" &&
      item.outstuation != null &&
      item.outstuation.length > 0
        ? parseInt(item.outstuation.replace("%", ""))
        : 0;

    var isup80 = istation < 80 ? false : true;

    var popHtml = "";
    popHtml += '<div class="wrap-mask"></div>';
    popHtml += '<div class="pop-remind-cont">';
    popHtml += '<i class="lingd"></i>';
    popHtml +=
      '<a href="javascript:$(\'#preRemindPop\').hide();" class="rpop-close"><i class="iconfont icon-guanbi3"></i></a>';
    popHtml += '<div class="cont-tit"><span>放号提醒</span></div>';
    popHtml += '<div class="mb5">';
    popHtml += '<p class="item-tit">就诊日期：</p>';
    popHtml +=
      '<p class="item-des">' +
      item.registerdate +
      " 星期" +
      GetWeek(item.registerdate) +
      GetTimeName(item.timeid) +
      "（未放号）</p>";
    popHtml += "</div>";
    popHtml += '<div class="mb5">';
    popHtml += '<p class="item-tit">预测放号时间：</p>';
    if (isup80) {
      popHtml +=
        '<p class="item-des">' + item.targetdate.substring(0, 16) + "</p>";
    } else {
      popHtml +=
        '<p class="item-des">' + item.targetdate.substring(0, 10) + "</p>";
    }
    popHtml +=
      '<p class="item-tips">注：预测放号时间是根据医生过去一个月排班时间进行推算， 仅供参考，以实际为准。</p>';
    popHtml += "</div>";

    if (!isup80) {
      popHtml += '<div class="mb5">';
      popHtml += '<p class="item-tit">温馨提示：</p>';
      popHtml +=
        '<p>为防止恶意刷号行为，该医生的放号时间为<span class="c-fb5e2f">不定时放号</span>，系统将在医生实际放号后通知您，请您及时关注手机信息。';
      popHtml += "</div>";
    } else if (notshopvip) {
      popHtml += '<div class="mb5">';
      popHtml += '<p class="item-tit">温馨提示：</p>';
      popHtml +=
        "<p>担心错过放号时间？您可以订阅放号提醒（点击下方按钮），系统将在医生实际放号时通知您，帮助您挂号。</p>";
      popHtml += "</div>";
    } else {
      popHtml += '<div class="mtb10">';
      popHtml += '<img src="../images/fanghao.png" class="width-100">';
      popHtml += "</div>";
    }
    popHtml += '<div class="mt20 mb10">';
    if (notshopvip) {
      //验证是否订阅
      var haspre = this.checkHaspre(
        item.hospitalid,
        item.doctorsn,
        item.registerdate,
        item.timeid
      );
      if (haspre) {
        popHtml +=
          '<a href="javascript:;" class="item-btn" >已订阅放号提醒</a>';
      } else {
        popHtml +=
          '<a href="javascript:;" class="item-btn" onclick=\'PreRemindService.subpre(' +
          JSON.stringify(item) +
          ")'>订阅放号提醒</a>";
      }
    } else {
      popHtml +=
        '<a href="javascript:toOpenVipZFB(\'yw_hyfw_1005\');" class="item-btn">立即开通</a>';
    }
    popHtml += "</div>";
    popHtml += "</div>";
    $("#preRemindPop").html(popHtml);
    $("#preRemindPop").show();
  },
  //显示放号提醒窗
  showpreBase: function (item) {
    var notshopvip = false;
    if (
      item.memberreminder == 2 ||
      channelId == "9000282" ||
      channelId == "9000283"
    ) {
      //不需要会员营销
      notshopvip = true;
    } else {
      notshopvip = GetUserIsVip();
    }
    var istation =
      typeof item.outstuation != "undefined" &&
      item.outstuation != null &&
      item.outstuation.length > 0
        ? parseInt(item.outstuation.replace("%", ""))
        : 0;

    var vmsg = "";
    var isup80 = true;
    if (istation < 80) {
      vmsg =
        '<p class="c-fb5e2f">温馨提示：</p><p>为防止恶意刷号行为，该医生的放号时间为<span class="c-fb5e2f">不定时放号</span>，系统将在医生实际放号后通知您，请您及时关注微信及手机信息。</p>';
      isup80 = false;
    } else if (notshopvip) {
      //根据您的登记信息，系统将在放号前10分钟左右下发通知消息，请保持关注。请及时登录并进入挂号页面，保持网络畅通。
      vmsg =
        '<p class="c-fb5e2f">温馨提示：</p><p>担心错过放号时间？您可以订阅放号提醒（点击下方按钮），系统将在医生实际放号时通知您，帮助您挂号。</p>';
    } else {
      vmsg =
        '<p class="c-fb5e2f">温馨提示：</p><p>担心错过放号时间？开通健康管家，即可订阅放号提醒，系统将为您检测，医生放号后马上通知您，帮助您轻松挂号。</p><p class="mt5">开通后请返回该页面，点击“订阅放号提醒”按钮。</p>';
    }

    //实时医院
    // if (notshopvip && item.modeid == 1) {
    //     this.getOldArrange(item.doctorsn, item.hospitalid, item.deptid, item.timeid, item.weekindex);
    //     //this.getOldArrange(22536, 1214, 4475);
    // }
    //拼接html
    var vhtml1 =
      '<i class="lingd"></i>' +
      '<a href="javascript:;" class="rpop-close" onclick="$(\'.remindpop,.remindpop-mb\').hide();"><i class="iconfont icon-guanbi2"></i></a>' +
      '<div class="rpop-main">' +
      "<h4><i>放号提醒</i></h4>" +
      '<div class="plr15 mt5">' +
      '<p class="c-666">就诊日期：</p>' +
      '<p class="c-bold c-f14">' +
      item.registerdate +
      " 星期" +
      GetWeek(item.registerdate) +
      GetTimeName(item.timeid) +
      '<span class="c-fb5e2f">（未放号）</span></p>' +
      "</div>" +
      '<ul class="rpop-mess mt5" id="rpop-mess-box">';
    if (isup80) {
      vhtml1 +=
        '<li><div class="rm-tit">预测放号时间：</div><div class="rm-con"><span class="c-bold c-4dcd70 c-f14">' +
        item.targetdate.substring(0, 16) +
        "</span></div></li>";
      if (notshopvip) {
        vhtml1 +=
          '<li><div class="rm-tit">准点放号率：</div><div class="rm-con"><span class="c-bold c-fb5e2f c-f14">' +
          item.outstuation +
          "</span></div></li>";
      }
    } else {
      vhtml1 +=
        '<li><div class="rm-tit">预测放号时间：</div><div class="rm-con"><span class="c-bold c-4dcd70 c-f14">' +
        item.targetdate.substring(0, 10) +
        "</span></div></li>";
    }
    vhtml1 += "</ul>";
    vhtml1 +=
      '<div class="plr15 mt5">' +
      '<p class="c-fb5e2f">预测时间说明：</p><p>预测放号时间是根据医生过去一个月的排班时间进行推算的，目的是为帮助用户挂号，仅供参考，具体放号时间请以实际为准。</p>' +
      "</div>";
    vhtml1 +=
      '<div class="plr15 mt5">' +
      vmsg +
      "</div>" +
      "</div>" +
      '<div class="rpop-btn">';
    if (notshopvip) {
      //验证是否订阅
      this.haspre(
        item.hospitalid,
        item.doctorsn,
        item.registerdate,
        item.timeid
      );
      vhtml1 +=
        '<a href="javascript:;" class="rbtn-open" id="rbtn-open-btndyfhtx" onclick=\'PreRemindService.subpre(' +
        JSON.stringify(item) +
        ")'>订阅放号提醒</a>";
    } else {
      var newCustomer = getNewCustomer();
      if (isNotNull(newCustomer)) {
        vhtml1 += '<div class="icon-festival">' + newCustomer + "</div>";
      }
      vhtml1 +=
        '<a href="javascript:toOpenVipUrl(\'yw_hyfw_1005\')" class="rbtn-open">马上开通健康管家</a>';
    }
    vhtml1 += "</div>";
    $(".remindpop").html(vhtml1);
    $(".remindpop,.remindpop-mb").show();
    this.resetmb();

    //记录
    //        if (isup80 && !notshopvip) {
    //            try {
    //                yihu('send', 'event', $('#hiChannelId').val() + "-yw_hyfw_1013-hyfw_btn111", "button", "click", "");
    //            } catch (e) { }
    //        }
  },
  //订阅放号提醒
  subpre: function (item) {
    if (!CheckLogin() || !CheckBindUser()) {
      return;
    }
    $.ajax({
      url: "../do/doctorArrange/addSubPreRemind",
      timeout: 12000,
      dataType: "json",
      type: "POST",
      data: {
        accountSn: loginUserInfo.accountSn,
        openTime: item.targetdate,
        hospitalId: item.hospitalid,
        hospitalName: item.hosname,
        deptId: item.deptid,
        deptName: item.deptname,
        doctorSn: item.doctorsn,
        doctorName: item.doctorname,
        registerDate: item.registerdate,
        timeId: item.timeid,
        modeId: item.modeid,
        outStuation: parseInt(item.outstuation.replace("%", "")),
        channelId: channelId,
      },
      beforeSend: function () {
        myLayer.load("正在订阅处理...");
      },
      success: function (objJson) {
        myLayer.clear();
        if (parseInt(objJson.Code) == 10000) {
          var con =
            '<i class="iconfont icon-chenggong1 c-4dcd70 mr5"></i>订阅成功，请留意通知';
          $("#arrangeinfoid_" + item.arrangeinfoid)
            .find("a")
            .removeClass("remind")
            .addClass("disab")
            .html("已订阅")
            .attr("href", "javascript:;");
          myLayer.alert(con, "1000", function () {
            $(".remindpop,.remindpop-mb").hide();
            $("#preRemindPop").hide();
          });
        } else if (
          parseInt(objJson.Code) == -10000 &&
          objJson.Message == "消息提醒已存在"
        ) {
          $(".remindpop,.remindpop-mb").hide();
          $("#preRemindPop").hide();
          $("#arrangeinfoid_" + item.arrangeinfoid)
            .find("a")
            .removeClass("remind")
            .addClass("disab")
            .html("已订阅")
            .attr("href", "javascript:;");
        } else {
          myLayer.alert(objJson.Message, "3000", function () {
            $(".remindpop,.remindpop-mb").hide();
            $("#preRemindPop").hide();
          });
        }
      },
      error: function () {
        myLayer.clear();
        myLayer.alert("订阅失败，处理异常！", "3000");
      },
    });
  },
  //获取历史三周放号
  getOldArrange: function (doctsn, hospitalid, hosdeptid, timeid, weekid) {
    var that = this;
    $.ajax({
      url: "../do/doctorArrange/getQueryOldArrange",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      data: {
        doctorSn: doctsn,
        hospitalId: hospitalid,
        deptId: hosdeptid,
        weekId: weekid,
        timeId: timeid,
      },
      success: function (objJson) {
        if (
          objJson.Code == 10000 &&
          objJson.Result &&
          objJson.Result.length > 0
        ) {
          var vhtml = "";
          var rpheight = 20;
          for (var i = 0, len = objJson.Result.length; i < len; i++) {
            var item = objJson.Result[i];
            rpheight += 20;
            vhtml +=
              "<p>" +
              item.openTime.substring(0, 10) +
              " 星期" +
              GetWeek(item.openTime) +
              " " +
              item.openTime.substring(10, 16) +
              "</p>";
          }
          if (vhtml !== "") {
            $("#rpop-mess-box").prepend(
              '<li><div class="rm-tit">近三周放号时间：</div><div class="rm-con">' +
                vhtml +
                "</div></li>"
            );
            $(".remindpop").height($(".remindpop").height() + rpheight);
            that.resetmb();
          }
        }
      },
    });
  },
  //是否已订阅
  checkHaspre: function (hospitalid, doctorsn, registerdate, timeid) {
    var ret = false;
    $.ajax({
      url: "../do/doctorArrange/getQueryMessageTip",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      async: false,
      data: {
        accountSn: loginUserInfo.accountSn,
        hospitalId: hospitalid,
        doctorSn: doctorsn,
        registerDate: registerdate,
        timeId: timeid,
      },
      success: function (objJson) {
        if (
          objJson.Code == 10000 &&
          objJson.Result &&
          objJson.Result.length > 0
        ) {
          ret = true;
        }
      },
    });
    return ret;
  },
  haspre: function (hospitalid, doctorsn, registerdate, timeid) {
    $.ajax({
      url: "../do/doctorArrange/getQueryMessageTip",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      data: {
        accountSn: loginUserInfo.accountSn,
        hospitalId: hospitalid,
        doctorSn: doctorsn,
        registerDate: registerdate,
        timeId: timeid,
      },
      success: function (objJson) {
        if (
          objJson.Code == 10000 &&
          objJson.Result &&
          objJson.Result.length > 0
        ) {
          $("#rbtn-open-btndyfhtx")
            .removeAttr("onclick")
            .html("已订阅放号提醒")
            .css({ "background-color": "#e0ccc6", color: "#313127" });
        }
      },
    });
  },
  //重新计算高度
  resetmb: function () {
    var rh = $(".remindpop").height() + 105;
    $(".rpop-main").height($(".remindpop").height());
    $(".remindpop").css("margin-top", -rh / 2);
  },
};

//复旦挂号特殊展示
var FddxGuaHaoService = {
  hospitalIds: "",
  waterData: [],
  specialName: "",
  docDeptArr: [],
  preArrangeData: [],
  loadDataInit: function () {
    var that = this;
    var myhos = _FddxfsyebhSubHospitalArr.slice(0);
    if (GuaHaoService.deptList.length > 0) {
      for (var j = 0; j < GuaHaoService.deptList.length; j++) {
        var hosidf = parseInt(GuaHaoService.deptList[j].hospitalId);
        if ($.inArray(hosidf, myhos) < 0) {
          myhos.push(hosidf);
        }
      }
    }
    that.hospitalIds = myhos.join(",");
    // 先去员工id
    //that.getDoctorInfoEmpId();
    //加载数据
    that.getArrangeWater();
  },
  //医生可预约排班流水
  getArrangeWater: function () {
    var that = this;
    var divobj = "sched-ghbox";
    $.ajax({
      url: "../do/doctorArrange/getArrangeWaterMuit",
      timeout: 12000,
      type: "POST",
      dataType: "json",
      cache: false,
      data: {
        doctorEmpId: doctorInfoFirst.doctorNumberList,
        hospitalIds: that.hospitalIds,
        channelId: channelId,
      },
      beforeSend: function () {
        localLoad(divobj);
      },
      success: function (objJson) {
        if (objJson) {
          if (objJson.DeptList && objJson.DeptList.length > 0) {
            that.docDeptArr = objJson.DeptList;
          }
          if (parseInt(objJson.Code) === 10000 && objJson.Result.length > 0) {
            var todayDate = objJson.DateNow;
            //获取日期
            that.waterData = that.getCalendar(
              todayDate,
              objJson.Result[objJson.Result.length - 1].registerdate
            );

            that.specialName = objJson.SpecialName;
            //遍历并填充数据
            for (var i = 0, count = objJson.Result.length; i < count; i++) {
              var item = objJson.Result[i];
              var registerdate = item.registerdate;
              var waterData = that.waterData;
              for (var j = 0, jcount = waterData.length; j < jcount; j++) {
                var jitem = waterData[j];
                if (jitem.r == registerdate) {
                  that.waterData[j].v.push(item);
                }
              }
            }
            that.getDateHtml(
              objJson.Result[objJson.Result.length - 1].registerdate,
              todayDate
            );
          } else if (parseInt(objJson.Code) === -111111) {
            comNomess({ pack: divobj, con: objJson.Message, mid: true });
          } else {
            comNomess({ pack: divobj, con: "该医生暂无排班信息.", mid: true });
          }
        } else {
          comNomess({ pack: divobj, con: "排班信息加载失败.", mid: true });
        }
      },
      error: function (xmlHttpRequest, textStatus) {
        comNomess({
          pack: divobj,
          con: "网络异常，请检查您的网络或刷新重试.",
          mid: true,
        });
      },
    });
  },
  getDateHtml: function (lastDate, todayDate) {
    var that = this;
    if (that.waterData.length == 0) {
      localArtReload({
        pack: "sched-ghbox",
        con: "网络异常，请检查您的网络或刷新重试",
      });
      return;
    }
    var lastDateDay = new Date(lastDate.replace(/-/g, "/")).getDay();
    var lastDateJ = 0;
    var isdefault = false;
    var defaultDate = "";
    var defaultWeek = "";
    var defaultPage = 1;
    var html =
      '<div class="mt10 h85"><div class="select-time" id="selectTime" ><div class="swiper-container"><div class="swiper-wrapper"><div class="swiper-slide"><ul>';
    for (var j = 0, jcount = that.waterData.length; j < jcount; j++) {
      var jitem = that.waterData[j];
      var islastDate = lastDate == jitem.r;
      if (jitem.v.length === 0) {
        html += '<li class="disabled auto-swperdlis-date-' + j + '">';
      } else if (!isdefault) {
        defaultPage =
          (j + 1) % 7 === 0 ? parseInt((j + 1) / 7) : parseInt((j + 1) / 7) + 1;
        isdefault = true;
        html +=
          '<li class="curr auto-swperdlis-date-' +
          j +
          '" onclick="FddxGuaHaoService.formtWaterHtml(' +
          j +
          "," +
          islastDate +
          "," +
          todayDate +
          ')">';
        defaultDate = jitem.r;
        defaultWeek = jitem.w;
        that.formtWaterHtml(j, islastDate);
      } else {
        html +=
          '<li class="auto-swperdlis-date-' +
          j +
          '" onclick="FddxGuaHaoService.formtWaterHtml(' +
          j +
          "," +
          islastDate +
          ",'" +
          todayDate +
          "')\">";
      }
      html +=
        '<div class="tit"> ' +
        jitem.w.replace("周", "") +
        '</div><a href="javascript:;" class="curr">' +
        jitem.r.substring(8) +
        "</a></li>";
      if (islastDate) {
        lastDateJ = j;
        break;
      }
      var m7 = (j + 1) % 7;
      if (j + 1 < jcount && m7 === 0) {
        html += '</div></ul><div class="swiper-slide"><ul>';
      }
    }

    if (lastDateDay > 0 && lastDateDay < 6) {
      for (var ky = 1; ky <= 6 - lastDateDay; ky++) {
        html +=
          '<li class="disabled auto-swperdlis-date-' + (lastDateJ + ky) + '">';
        html +=
          '<div class="tit"> ' +
          that.waterData[lastDateJ + ky].w.replace("周", "") +
          '</div><a href="javascript:;" class="curr">' +
          that.waterData[lastDateJ + ky].r.substring(8) +
          "</a></li>";
      }
    }

    html +=
      '</ul></div></div><div class="swiper-pagination"></div></div></div></div>';
    html +=
      '<ul class="selecttime-con mt10" id="selectTimeCon"><li class="select-item" data-time=""><div class="tit">' +
      defaultDate +
      " " +
      defaultWeek +
      "</div><ul></ul></li></ul>";
    $("#sched-ghbox").html(html);

    //选择时间
    var swiper = new Swiper(".swiper-container", {
      loop: true,
      pagination: ".swiper-pagination",
    });
    swiper.slideTo(defaultPage);
  },
  formtWaterHtml: function (index, islastDate, todayDate) {
    $(".swiper-wrapper").find("li").removeClass("curr");
    $(".swiper-wrapper ul li.auto-swperdlis-date-" + index).addClass("curr");

    var that = this;
    var result = that.waterData[index].v;
    var thatdate = that.waterData[index].r;

    var listHtml = "";
    var btnClick = ""; //按钮点调用方法
    var btnCol = ""; //按钮背景颜色
    var btnText = "";
    var txtFHTime = ""; //预计放号时间
    var feeName = that.specialName;
    var ztCon = ""; //医院暂停预约以后的温馨提示
    var firstfeeCon = "";
    var firstTypeName =
      typeof result[0].TypeName != "undefined" ? result[0].TypeName : "";
    //var appurlParams = that.getAppUrlParam();
    result.sort(that.sortTimeId);
    for (var i = 0, count = result.length; i < count; i++) {
      var item = result[i];
      var arrageStatus = parseInt(item.ArrangeStatus);
      //var registerDate = StrToDateDot(item.registerdate);
      var thatghstatus = 0;
      for (var sd = 0; sd < that.docDeptArr.length; sd++) {
        if (that.docDeptArr[sd].doctorSn == item.doctorsn) {
          thatghstatus = that.docDeptArr[sd].ghstatus;
        }
      }
      var parameObj =
        "{doctorSn:'" +
        item.doctorsn +
        "',doctorName:'" +
        item.doctorname +
        "',deptId:'" +
        item.deptid +
        "',deptName:'" +
        item.deptname +
        "',hosId:'" +
        item.hosid +
        "',hosName:'" +
        item.hosname +
        "',status:'" +
        item.thatghstatus +
        "', registerdate:'" +
        item.registerdate +
        "',timeid:" +
        item.timeid +
        ",ArrangeID:" +
        item.ArrangeID +
        ",specialprice:" +
        item.specialprice +
        ",ghfeeway:" +
        item.ghfeeway +
        ",ModeId:" +
        item.ModeId +
        ",GhFee:" +
        item.GhFee +
        ",AllFee:" +
        item.AllFee +
        ",availablenum:" +
        item.availablenum +
        ",typeName:'" +
        item.TypeName +
        "',isparttime:'" +
        item.IsPartTime +
        "',IsCheckOrder:'" +
        item.IsCheckOrder +
        "',securityDeposit:" +
        item.securityDeposit +
        ",feeName:'" +
        feeName +
        "',UnOpened:" +
        item.UnOpened +
        ",FHDays:'" +
        item.FHDays +
        "',FHTimes:'" +
        item.FHTimes +
        "',hosname:'" +
        item.hosname +
        "',deptname:'" +
        item.deptname +
        "',hosid:'" +
        item.hosid +
        "'}";
      ////挂号状态：1正常；2 停诊；4 暂停预约
      if (arrageStatus === 1) {
        //正常
        if (item.OverTime == 1) {
          btnCol = " disab";
          btnClick = "";
          btnText = "截止预约";
        } else if (item.availablenum <= 0) {
          if (
            parseInt(item.houBuClosed) === 1 ||
            item.registerdate == todayDate
          ) {
            //部分医院无号源，不允许候补，展示“已约满”，并且点击无效果
            btnCol = " disab";
            btnClick = "";
            btnText = "约满";
          } //如果无号源且允许候补，展示“候补”，点击后跳转到候补的站点，进行候补操作
          else {
            btnCol = " waited";
            btnClick =
              'onclick ="GuaHaoService.onDoctorGhAffirm(this,' +
              parameObj +
              ",'HB')\"";
            btnText = "约满候补";
          }
        } else {
          btnCol = "";
          btnClick =
            'onclick ="GuaHaoService.onDoctorGhAffirm(this,' +
            parameObj +
            ",'GH')\"";
          if (String(item.UnOpened) == "true") {
            btnText = "预约登记";
            txtFHTime = "预计" + item.FHTimes + "放号";
          } else if (parseInt(item.numCountVisible) === 1) {
            //0非实时；1代表实时
            btnText = "预约";
          } else {
            btnText = "剩" + item.availablenum + "个";
          }
        }
      } else if (arrageStatus === 4) {
        //暂停预约
        if (ztCon === "" && item.ArrRemark) {
          ztCon = item.ArrRemark;
        }
        btnClick = "";
        btnCol = " disab";
        btnText = "暂停预约";
      } else {
        //停诊
        btnClick = "";
        btnCol = " disab";
        btnText = "停诊";
      }
      var showhosname = item.hosname;
      switch (parseInt(item.hosid)) {
        case _FddxfsyebhSubHospitalArr[0]:
          showhosname = "汾阳院区";
          break;
        case _FddxfsyebhSubHospitalArr[1]:
          showhosname = "浦江院区";
          break;
        case _FddxfsyebhSubHospitalArr[2]:
          showhosname = "宝庆路院区";
          break;
      }
      listHtml +=
        "<li " +
        btnClick +
        '><a href="javascript:;"><i class="icon-noon">' +
        GetTimeName(item.timeid) +
        '</i><div class="info pr10 c-nowrap">' +
        item.TypeName +
        "（" +
        showhosname +
        '）</div><div class="key w60"><span class="price">' +
        item.specialprice / 100 +
        '元</span></div><div class="key w82"><span class="sched-btn' +
        btnCol +
        '">' +
        btnText +
        "</span></div></a></li>";
    }
    if (listHtml === "") {
      comNomess({
        pack: "selectTimeCon ul",
        con: "该医生暂无排班信息",
        mid: true,
      });
    } else {
      setTimeout(function () {
        $("#selectTimeCon li").hide();
        $("#selectTimeCon li")
          .eq(0)
          .find("div.tit")
          .html(thatdate + " 周" + GetWeekByDay(new Date(thatdate).getDay()));
        $("#selectTimeCon li").eq(0).find("ul").html(listHtml);
        $("#selectTimeCon li").eq(0).show();
        if (islastDate) {
          that.getPreArrange(firstTypeName); //显示放号提醒
        }
      }, 100);
    }
  },
  getCalendar: function (bdate, edate) {
    var that = this;
    var ret = [];

    var beginDate = new Date(bdate.replace(/-/g, "/"));
    var endDate = new Date(edate.replace(/-/g, "/"));
    var beginDay = beginDate.getDay();
    var endDay = endDate.getDay();

    for (var j = beginDay; j >= 0; j--) {
      var beforeDate = new Date(beginDate);
      var newbeforeDate = new Date(beforeDate.setDate(beginDate.getDate() - j));
      var curjskd = {
        r: that.DateFormat(newbeforeDate, "yyyy-MM-dd"),
        w: GetWeekByDay(newbeforeDate.getDay()),
        v: [],
      };
      ret.push(curjskd);
    }

    var d = parseInt(that.getDays(bdate, edate));
    if (d > 0) {
      for (var i = 1; i <= d; i++) {
        var newDate = new Date(beginDate.setDate(beginDate.getDate() + 1));
        var nj = {
          r: that.DateFormat(newDate, "yyyy-MM-dd"),
          w: GetWeekByDay(newDate.getDay()),
          v: [],
        };
        ret.push(nj);
      }
    }

    for (var e = 1; e <= 6 - endDay; e++) {
      var hendDate = new Date(endDate);
      var newendDate = new Date(hendDate.setDate(endDate.getDate() + e));
      var curjskd1 = {
        r: that.DateFormat(newendDate, "yyyy-MM-dd"),
        w: GetWeekByDay(newendDate.getDay()),
        v: [],
      };
      ret.push(curjskd1);
    }

    return ret;
  },
  getDays: function (strDateStart, strDateEnd) {
    var strSeparator = "-"; //日期分隔符
    var oDate1 = strDateStart.split(strSeparator);
    var oDate2 = strDateEnd.split(strSeparator);
    var strDateS = new Date(oDate1[0], oDate1[1] - 1, oDate1[2]);
    var strDateE = new Date(oDate2[0], oDate2[1] - 1, oDate2[2]);
    var iDays = parseInt((strDateE - strDateS) / 1000 / 60 / 60 / 24); //把相差的毫秒数转换为天数
    return iDays;
  },
  DateFormat: function (newDate, fmt) {
    //author: meizz
    var o = {
      "M+": newDate.getMonth() + 1, //月份
      "d+": newDate.getDate(), //日
      "h+": newDate.getHours(), //小时
      "m+": newDate.getMinutes(), //分
      "s+": newDate.getSeconds(), //秒
      "q+": Math.floor((newDate.getMonth() + 3) / 3), //季度
      S: newDate.getMilliseconds(), //毫秒
    };
    if (/(y+)/.test(fmt))
      fmt = fmt.replace(
        RegExp.$1,
        (newDate.getFullYear() + "").substr(4 - RegExp.$1.length)
      );
    for (var k in o)
      if (new RegExp("(" + k + ")").test(fmt))
        fmt = fmt.replace(
          RegExp.$1,
          RegExp.$1.length == 1
            ? o[k]
            : ("00" + o[k]).substr(("" + o[k]).length)
        );
    return fmt;
  },
  //获取放号提醒预排班
  getPreArrange: function (firstTypeName) {
    if (!isAccountBind) return;
    var t = this;
    if (t.preArrangeData.length > 0) {
      $("#selectTimeCon li").show();
      return;
    }
    if (t.docDeptArr.length === 0) return;
    for (var i = 0; i < t.docDeptArr.length; i++) {
      var ditem = t.docDeptArr[i];
      $.ajax({
        url: "../do/doctorArrange/getPreArrangeInfo",
        timeout: 12000,
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        data: {
          hospitalId: doctorInfoFirst.hospitalId,
          deptId: doctorInfoFirst.hosDeptId,
          doctorSn: doctorInfoFirst.doctorSn,
          channelId: channelId,
        },
        success: function (objJson) {
          if (objJson.Code == 10000 && objJson.Result.length > 0) {
            for (var i = 0, count = objJson.Result.length; i < count; i++) {
              var item = objJson.Result[i];
              item["deptid"] = ditem.deptId;

              var showhosname = item.hosname;
              switch (parseInt(item.hospitalid)) {
                case _FddxfsyebhSubHospitalArr[0]:
                  showhosname = "汾阳院区";
                  break;
                case _FddxfsyebhSubHospitalArr[1]:
                  showhosname = "浦江院区";
                  break;
                case _FddxfsyebhSubHospitalArr[2]:
                  showhosname = "宝庆路院区";
                  break;
              }

              var itemHtml =
                '<li id="arrangeinfoid_' +
                item.arrangeinfoid +
                '" data-type="fhtx">';
              itemHtml +=
                "<a href='javascript:PreRemindService.showpre(" +
                JSON.stringify(item) +
                ");'>";
              itemHtml +=
                '<i class="icon-noon">' + GetTimeName(item.timeid) + "</i>";
              itemHtml +=
                '<div class="info pr10 c-nowrap">' +
                firstTypeName +
                "（" +
                showhosname +
                "）</div>";
              itemHtml +=
                '<div class="key w82"><span class="sched-btn remind">放号提醒</span></div>';
              itemHtml += "</a>";
              itemHtml += "</li>";
              if ($.inArray(item.registerdate, t.preArrangeData) < 0) {
                t.preArrangeData.push(item.registerdate);
                var listHtml333 =
                  '<li class="select-item atuo-prearrayli-' +
                  item.registerdate +
                  '"><div class="tit">' +
                  item.registerdate +
                  " 周" +
                  GetWeek(item.registerdate) +
                  "</div><ul>" +
                  itemHtml +
                  "</ul></li>";
                $("#selectTimeCon").append(listHtml333);
              } else {
                $("#selectTimeCon")
                  .find(".atuo-prearrayli-" + item.registerdate + " ul")
                  .append(itemHtml);
              }
            }
          }
        },
      });
    }
  },
  sortTimeId: function (x, y) {
    return x.timeid > y.timeid ? 1 : -1;
  },
};

var JkgjService = {
  jkgj_toUnCooperationRegister: function (hosid, needVip, obj) {
    var that = this;
    // 验证是否登录绑定
    if (!CheckLogin() || !CheckBindUser()) {
      myLayer.clear();
      return;
    }
    if (needVip && !GetUserIsVip()) {
      //显示营销
      that.jkgj_marketing();
      return false;
    } else {
      var docsn = $(obj).parents("li").attr("data-docsn");
      var doctoruid = $(obj).parents("li").attr("data-doctoruid");
      var deptid = $(obj).parents("li").attr("data-deptid");
      that.jkgj_yynote(docsn, doctoruid, deptid, hosid);
    }
  },
  jkgj_yynote: function (docsn, doctoruid, deptid, hosid) {
    $("#remindYYNote").remove();
    var html =
      '<div class="remindpop nostring" id="remindYYNote">' +
      '        <a href="javascript:$(\'.remindpop,.remindpop-mb\').hide();$(\'#remindYYNote\').remove();" class="rpop-close"><i class="iconfont icon-guanbi2"></i></a>' +
      '        <div class="rpop-main">' +
      "            <span><h4>代预约挂号说明</h4></span>" +
      '            <div class="plr15">' +
      "                <p class=\"mt5\">1.<span class='c-ff8100'>代预约挂号仅为一种代办服务申请，提供代预约服务不等于一定预约挂号成功</span>，能否预约到号取决于当前医生的号源紧缺情况。</p>" +
      '                <p class="mt5">2.您可以按代预约挂号流程登记相关信息,进行代预约服务申请。</p>' +
      '                <p class="mt5">3.如果代预约挂号成功,您将收到短信通知，<span class=\'c-ff8100\'>同时您可在“个人中心我的代办"中查看代预约挂号进度情况。</span></p>' +
      "            </div>" +
      "        </div>" +
      '        <div class="rpop-btn">' +
      '            <a href="javascript:JkgjService.jkgj_toYY(' +
      docsn +
      "," +
      doctoruid +
      "," +
      deptid +
      "," +
      hosid +
      ');" class="rbtn-open">我知道了</a>' +
      "        </div>" +
      "    </div>";
    $("body").append(html);
    $("#remindYYNote,.remindpop-mb").show();
    var rh = $("#remindYYNote").height() + 105;
    $("#remindYYNote .rpop-main").height($("#remindYYNote").height());
    $("#remindYYNote").css("margin-top", -rh / 2);
  },
  jkgj_toYY: function (docsn, doctoruid, deptid, hosid) {
    location.href =
      StrTradeUrl +
      "/trade/fabinfo/assistHosPage.html?busType=7&type=1&channelId=" +
      channelId +
      "&hospitalId=" +
      hosid +
      "&hosDeptId=" +
      deptid +
      "&doctorUid=" +
      doctoruid;
  },
  jkgj_marketing: function () {
    $("#remindOpen").remove();
    // var newCustomer=getNewCustomer();
    var html =
      '<div class="remindpop" id="remindOpen">' +
      '        <i class="btn2-logo"></i>' +
      '        <a href="javascript:$(\'.remindpop,.remindpop-mb\').hide();$(\'#remindOpen\').remove();" class="rpop-close"><i class="iconfont icon-guanbi2"></i></a>' +
      '        <div class="rpop-main">' +
      '            <h4><i class="c-bold c-f18">代预约挂号</i></h4>' +
      '            <div class="plr15 mt5">' +
      '                <p class="mt5">开通健康管家服务，即可享受就医指导、分科导诊、放号提醒、医生停诊通知、家庭医生在线及健康助理代预约挂号等专属就医服务。</p>' +
      '                <p class="mt15 pb10"><img width="100%" src="../images/jkgj/jkgj-img1.png?v=1" /></p>' +
      "            </div>" +
      "        </div>" +
      '        <div class="mt15">' +
      '            <div class="jkgj-btn">' +
      '                <a href="javascript:JkgjService.jkgj_toOpenVip();">开通会员专享版  低至0.9元/天<i class="tag-yh"></i></a>' +
      "            </div>" +
      "        </div>" +
      "      </div>";
    // if(isNotNull(newCustomer)){
    // 	html+='<div class="icon-festival rpop-btn-addheights">'+newCustomer+'</div>';
    // }
    // html+='<a href="javascript:JkgjService.jkgj_toOpenVip();" class="rbtn-open">马上开通健康管家</a>' +
    // 	'        </div>' +
    // 	'    </div>';

    $("body").append(html);
    $("#remindOpen,.remindpop-mb").show();
    var rh = $("#remindOpen").height() + 105;
    // $(' #remindOpen .rpop-main').height($('#remindOpen').height()+$(".rpop-btn-addheights").height());
    $("#remindOpen").css("margin-top", -rh / 2);
  },
  jkgj_toOpenVip: function () {
    var url =
      StrUserUrl +
      "/UserCenter/web/vip/b-v.html?platformType=" +
      platformType +
      "&referCode=yw_hyfw_1109&sourceType=0&sourceId=0&from=" +
      escape(location.href);
    location.href = url;
  },
};

var dyyService = {
  tipYydj: function (hosid, obj) {
    myLayer.load();
    // 验证是否登录绑定
    if (!CheckLogin() || !CheckBindUser()) {
      myLayer.clear();
      return;
    }
    var thatDeptId = $(obj).parents("li").attr("data-deptid");
    var thatSecKey = "auto-yyrkWrap-" + hosid + "-" + thatDeptId;
    if ($("#" + thatSecKey).length > 0) {
      if ($("#" + thatSecKey).is(":hidden")) {
        $(".yyrkWrap").hide();
        $("#" + thatSecKey).show(200);
      }
      myLayer.clear();
      return;
    } else {
      $(".yyrkWrap").hide();
    }

    var ht =
      '<section class="yyrkWrap" id="' +
      thatSecKey +
      '">' +
      '<div class="yyrk-box">' +
      '<p class="c-f15 tit">就医信息</p>' +
      '<ul class="c-list c-f15">' +
      ' <li class="list-arrow-r ptb10">' +
      '  <div class="c-list-key c-333">就诊日期</div>' +
      '<div class="c-list-info"><input type="text" name="tx-jzrq" class="width-100 mbsc-comp" placeholder="请选择就诊日期" data-time="date" readonly=""></div>' +
      "</li>" +
      '<li class="list-arrow-r">' +
      '<input type="hidden" name="tx-jzr" />' +
      '  <a href="javascript:;" class="ptb10 c-list-a" onclick="dyyService.member.getMembers(\'' +
      thatSecKey +
      "');\">" +
      '     <div class="c-list-key c-333">就诊人</div>' +
      '     <div class="c-list-info c-b2b2b2 auto-jzr-text">请选择就诊人</div>' +
      "   </a>" +
      "</li>" +
      "</ul>" +
      '<div class="pr15 ptb10 textareabox">' +
      ' <p class="c-f15 c-333 mb5">就医需求</p>' +
      ' <textarea name="tx-jyxq" id="" rows="3" class="" placeholder="请输入就医需求"></textarea>' +
      " </div>" +
      "</div>" +
      ' <div class="yyrk-btnwrap">' +
      '  <p class="c-f13 tips mb5">提交本预约订单不代表已预约成功，我们将根据您的预约需求尽力为您预约，最终预约结果将由客服联系通知您。</p>' +
      "  <button onclick=\"dyyService.subRegist('" +
      thatSecKey +
      '\',this);" class="c-btn-full c-btn-4dcd70 c-f15 c-boxs">下一步</button>' +
      "</div>" +
      " </section>";
    $(
      "#auto-cl-gh-pregrad-" +
        hosid +
        " .auto-cl-gh-deptbox[data-deptid='" +
        thatDeptId +
        "'] .auto-gdept-jkgjsd"
    ).html(ht);
    var widowst = $(window).scrollTop();
    var btnOffsetTop = $(obj).offset().top;
    if (widowst <= btnOffsetTop) {
      $("body,html").animate({ scrollTop: btnOffsetTop - 100 }, 200);
    }
    $("#" + thatSecKey).slideDown(200);
    // 就诊日期
    var date = new Date();
    var dateEnd = new Date();
    dateEnd.setDate(date.getDate() + 30);
    $("#" + thatSecKey)
      .find("input[data-time=date]")
      .mobiscroll({
        preset: "date",
        dateFormat: "yy-mm-dd",
        theme: "ios",
        lang: "zh",
        minDate: date,
        maxDate: dateEnd,
      });
    saveSiteTrack("bus009btn1010", {
      hosid: $(obj).parents("li.auto-cl-gh-deptbox").attr("data-hosid"),
      deptid: $(obj).parents("li.auto-cl-gh-deptbox").attr("data-deptid"),
      provinceid: $(obj)
        .parents("li.auto-cl-gh-deptbox")
        .attr("data-provinceid"),
      cityid: $(obj).parents("li.auto-cl-gh-deptbox").attr("data-cityid"),
      doctorid: $("#doctorid").val(),
    });
    setTimeout(function () {
      myLayer.clear();
    }, 200);
  },
  subRegist: function (divobjkey, btnobj) {
    $(btnobj).attr("disabled", "disabled");
    // 获取医生信息
    var deptLiObj = $("#" + divobjkey).parents("li.auto-cl-gh-deptbox");
    var deptid = $(deptLiObj).attr("data-deptid");
    var hosid = $(deptLiObj).attr("data-hosid");
    var deptname = $(deptLiObj).attr("data-deptname");
    var hosname = $(deptLiObj).attr("data-hosname");
    var provinceid = $(deptLiObj).attr("data-provinceid");
    var provincename = $(deptLiObj).attr("data-provincename");
    var cityid = $(deptLiObj).attr("data-cityid");
    var cityname = $(deptLiObj).attr("data-cityname");
    // 提交信息
    var regdatejzr = $("#" + divobjkey).find("input[name=tx-jzr]");
    var memberSn = $(regdatejzr).attr("data-membersn");
    var idcard = $(regdatejzr).attr("data-idcard");
    var regdatejzrq = $("#" + divobjkey)
      .find("input[name=tx-jzrq]")
      .val();
    var regdatejyxq = $("#" + divobjkey)
      .find("textarea[name=tx-jyxq]")
      .val();
    var cname = $(regdatejzr).attr("data-cname");
    var phone = $(regdatejzr).attr("data-phone");
    var certtype = $(regdatejzr).attr("data-certtype");
    // 参数
    var data = {
      busType: 25,
      loginId: loginUserInfo.loginId,
      cardId: loginUserInfo.cardNumber,
      accountSn: loginUserInfo.accountSn,
      channelId: channelId,
      platformType: platformType,
      provinceId: provinceid,
      cityId: cityid,
      provinceName: provincename,
      cityName: cityname,
      hospitalId: hosid,
      hospitalName: hosname,
      hosDeptId: deptid,
      hosDeptName: deptname,
      doctorUid: doctorId,
      doctorName: doctorInfoFirst.doctorName,
      memberSn: memberSn,
      cname: cname,
      phone: phone,
      certtype: certtype,
      idcard: idcard,
      registerDate: regdatejzrq,
      remark: regdatejyxq,
      providerType: 1,
      providerInfo: doctorId,
      t: new Date().getTime(),
    };

    $.ajax({
      url: "../do/agency/agencyRegister",
      timeout: 12000,
      dataType: "json",
      async: false,
      type: "POST",
      data: data,
      beforeSend: function () {
        myLayer.load("正在提交...");
      },
      success: function (objJson) {
        //console.log(objJson);
        if (objJson.Code == 10000) {
          var regdate = $("#" + divobjkey)
            .find("input[name=tx-jzrq]")
            .val();
          location.href =
            "../register/unCooperationSuccess.html?platformType=" +
            platformType;
          setTimeout(function () {
            myLayer.clear();
            $(btnobj).removeAttr("disabled");
          }, 3000);
        } else {
          myLayer.clear();
          myLayer.alert("登记异常[" + objJson.Message + "]", "2000");
          $(btnobj).removeAttr("disabled");
        }
      },
      error: function () {
        myLayer.clear();
        myLayer.alert("关注失败，系统异常！", "2000");
        $(btnobj).removeAttr("disabled");
      },
    });
  },
  member: {
    isload: false,
    getMembers: function (divobjkey) {
      var that = this;
      if (that.isload) {
        var totH = $(window).height();
        $("body").height(totH).css("overflow", "hidden");
        $(".choosepop-mb,#humanchoo").show();
        $("#human-hold").css("max-height", totH / 2 - 95);
        return;
      }
      $.ajax({
        url: "../do/registerInfo/getMemberList",
        timeout: 12000,
        dataType: "json",
        async: false,
        type: "POST",
        data: {
          accountSn: loginUserInfo.accountSn,
          t: new Date().getTime(),
        },
        success: function (objJson) {
          //console.log(objJson);
          if (objJson.Code == 10000 && objJson.Result) {
            var ht =
              '<div class="choosepop" id="humanchoo">' +
              '<span class="c-add">选择就诊人<i class="c-close" onClick="dyyService.member.coloseDia();"></i></span>' +
              '<div id="human-hold">' +
              "<ul>";
            for (var i = 0, count = objJson.Result.length; i < count; i++) {
              var item = objJson.Result[i];
              ht +=
                "<li onclick=\"dyyService.member.selDia(this,'" +
                divobjkey +
                '\')" data-membersn="' +
                item.Membersn +
                '" data-certtype="' +
                item.Identitytype +
                '" data-idcard="' +
                item.Zhengjianid +
                '" data-cname="' +
                item.Cname +
                '" data-phone="' +
                item.Phone +
                '">' +
                item.Cname +
                "</li>";
            }
            ht += "</ul>" + "</div>";
            ht +=
              '<div class="c-add" id="dyyToAddMember" style="display:none;border-top: 1px solid' +
              ' #f2f2f2;"' +
              ' onclick="dyyService.member.toAddmember();">添加就诊人</div>';
            ht += "</div>";

            ht +=
              '<div class="choosepop-mb c-hide" onClick="dyyService.member.coloseDia();"></div>';
            $("body").append(ht);
            var totH = $(window).height();
            $("body").height(totH).css("overflow", "hidden");
            $(".choosepop-mb,#humanchoo").show();
            $("#human-hold").css("max-height", totH / 2 - 95);
            that.isload = true;
            if (objJson.Result.length < 5) {
              $("#dyyToAddMember").show();
            }
          }
        },
      });
    },
    coloseDia: function () {
      $(".choosepop-mb,.choosepop").hide();
      $("body").height("auto").css("overflow", "auto");
    },
    selDia: function (obj, divobjkey) {
      var that = this;
      var membersn = $(obj).attr("data-membersn");
      var idcard = $(obj).attr("data-idcard");
      var phone = $(obj).attr("data-phone");
      var cname = $(obj).attr("data-cname");
      var certtype = $(obj).attr("data-certtype");
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .val(membersn);
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .attr("data-membersn", membersn);
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .attr("data-idcard", idcard);
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .attr("data-phone", phone);
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .attr("data-cname", cname);
      $("#" + divobjkey)
        .find("input[name=tx-jzr]")
        .attr("data-certtype", certtype);
      $("#" + divobjkey)
        .find(".auto-jzr-text")
        .html($(obj).text())
        .css("color", "#333");
      that.coloseDia();
    },
    toAddmember: function () {
      var fromUrl = encodeURIComponent(
        location.href.replace("&addm=1", "") + "&addm=1"
      );
      var url =
        StrUserUrl +
        "/UserCenter/web/member/member.html?platformType=" +
        platformType +
        "&sourceType=0&sourceId=0&from=" +
        fromUrl;
      location.href = url;
    },
  },
};

var LvTongService = {
  deptId: "",
  hospitalId: "",
  checkIsCooperate: function (hospitalId, deptId) {
    LvTongService.hospitalId = hospitalId;
    LvTongService.deptId = deptId;
    $.ajax({
      type: "post",
      async: true,
      url: "../do/doctor/isCooperate",
      data: {
        t: new Date().getTime(),
        hospitalId: hospitalId,
      },
      success: function (d) {
        if (d.Code == 10000) {
          LvTongService.showLvTong();
        }
      },
    });
  },
  showLvTong: function () {
    $.ajax({
      type: "post",
      async: true,
      url: "../do/baseInfo/getYuYueChannelConfigs",
      data: {
        channelId: channelId,
        type: "LvtongChannel",
      },
      success: function (d) {
        if (d.Code == 10000 && isNotNull(d.value)) {
          var html = '<div class="bgc-fff c-border-tb jylt-reser mt10">';
          html += '<div class="jylt-reser-lt">';
          html +=
            '<span class="lt-icon"><i class="iconfont icon-jiuyilvtong"></i></span>';
          html += '<div class="lt-tit">';
          html += '<p class="c-333 c-f14 mb5">专家门诊安排</p>';
          html += '<span class="c-999 c-f12">便捷就诊安排可申请</span>';
          html += "</div>";
          html += "</div>";
          html +=
            "<a onclick=\"LvTongService.toLvTong('" +
            d.value +
            '\');" class="jylt-reser-btn">立即申请</a>';
          html += "</div>";
          $("#Lvtong").html(html);
        }
      },
    });
  },
  toLvTong: function (value) {
    saveSiteTrack("ghbtn1039", {
      hosid: LvTongService.hospitalId,
      deptid: LvTongService.deptId,
      doctorid: doctorId,
    });
    var ltUrl = "";
    if (value == 2) {
      ltUrl =
        StrTradeUrl +
        "/trade/fabinfo/hospitalizeGreenV2.html?channelId=" +
        channelId +
        "&busType=1&scene=1&hospitalId=" +
        LvTongService.hospitalId +
        "&hosDeptId=" +
        LvTongService.deptId +
        "&doctorUid=" +
        doctorId;
    } else {
      ltUrl =
        StrTradeUrl +
        "/trade/fabinfo/hospitalizeGreen.html?referCode=yw_zgrs_1003&platformType=" +
        platformType +
        "&sourceType=0&sourceId=0&HosId=" +
        LvTongService.hospitalId +
        "&DeptId=" +
        LvTongService.deptId +
        "&doctorUid=" +
        doctorId +
        "&openid=" +
        loginUserInfo.loginId;
    }
    location.href = ltUrl;
  },
};

var GhMarketingService = {
  load: function (hosids) {
    var that = this;
    $.ajax({
      type: "post",
      async: true,
      url: "../do/user/getMembertypeByAccountSn",
      data: {
        t: new Date().getTime(),
        accountSn: loginUserInfo.accountSn,
      },
      success: function (objJson) {
        if (objJson.Code == 10000) {
          if (objJson.memeberType > 0 && objJson.memeberType != 3) {
            // 管家
          } else {
            // 未开通管家
            that.queryHospitalGhMarketing(hosids);
          }
        }
      },
    });
  },
  queryHospitalGhMarketing: function (hosids) {
    var that = this;
    $.ajax({
      type: "post",
      async: true,
      url: "../do/doctorService/queryHospitalGhMarketing",
      data: {
        t: new Date().getTime(),
        channelId: channelId,
        hosids: hosids,
      },
      success: function (objJson) {
        if (objJson.Code == 10000) {
          if (objJson.Result && objJson.Result.length > 0) {
            var vHtml =
              '<div class="dredge clearfix" onclick="GhMarketingService.tojkgj();">';
            vHtml +=
              '<a href="javascript:;">开通健康管家，每次挂号最高可减5元</a>';
            vHtml += '<span class="fr">立即开通</span>';
            vHtml += "</div>";

            $.each(objJson.Result, function (index, item) {
              if (item.ghMarketingConfig == 1) {
                $("#auto-cl-gh-pregrad-" + item.hospitalId)
                  .find(".grad-hosp")
                  .after(vHtml);
              }
            });
          }
        }
      },
    });
  },
  tojkgj: function () {
    if (!CheckLogin()) {
      return;
    }
    location.href =
      StrUserUrl +
      "/UserCenter/web/vip/online.html?platformType=" +
      platformType +
      "&sourceId=0&sourceType=0&referCode=yw_hyfw_1095&from=" +
      encodeURIComponent(location.href);
  },
};

var doctorMarketingService = {
  serviceType: -1,
  isLoad: false,
  load: function (divobj) {
    var that = this;
    if (!that.isLoad) {
      $.ajax({
        url: "../do/doctorService/getDoctorMarketingService",
        async: false,
        data: {
          channelId: channelId,
          doctorUid: doctorId,
        },
        type: "POST",
        dataType: "json",
        timeout: 8000,
        beforeSend: function () {},
        error: function () {},
        success: function (d) {
          if (d.Code == 10000 && d.Result && isNotNull(d.Result.serviceType)) {
            that.serviceType = d.Result.serviceType;
            that.isLoad = true;
          }
        },
      });
    }
    if (that.serviceType == 1) {
      //面对面
      that.showMdmHtml(divobj);
    } else if (that.serviceType == 2) {
      //服务包
      that.showPackHtml(divobj);
    } else if (that.serviceType == 3) {
      //咨询
      that.showZxHtml(divobj);
    }
  },
  showMdmHtml: function (divobj) {
    var obj = $("#" + divobj);
    var html = "";
    html += '<div class="face-into clearfix">';
    html +=
      "<span>约不到" + doctorInfoFirst.doctorName + "医生？找他申请</span>";
    html +=
      '<a href="javascript:doctorMarketingService.toMdmApply();">班外面诊</a>';
    html += "</div>";
    obj.prepend(html);
  },
  toMdmApply: function () {
    var mdmUrl =
      StrMdmUrl +
      "/user/html/applyIntro.html?doctorUid=" +
      doctorId +
      "&channelId=" +
      channelId;
    location.href = mdmUrl;
  },
  showPackHtml: function (divobj) {
    var obj = $("#" + divobj);
    var html = "";
    html +=
      '<div class="face-into clearfix" onclick="doctorMarketingService.toPack();">';
    html +=
      "<span>找" + doctorInfoFirst.doctorName + "专家面对面问诊，请点我</span>";
    html += '<a href="javascript:;">点我</a>';
    html += "</div>";
    obj.prepend(html);
  },
  toPack: function () {
    var packUrl =
      "../servicePack/serviceList.html?doctorId=" +
      doctorId +
      "&platformType=" +
      platformType +
      "&utm_source=0.0.h.1066.bus060." +
      doctorId;
    location.href = packUrl;
  },
  showZxHtml: function (divobj) {
    var obj = $("#" + divobj);
    var html = "";
    html += '<div class="face-into clearfix">';
    html +=
      "<span>找" +
      doctorInfoFirst.doctorName +
      "医生看病？不如先来一次对话吧</span>";
    html += '<a href="javascript:doctorMarketingService.toZx();">去沟通</a>';
    html += "</div>";
    obj.prepend(html);
  },
  toZx: function () {
    var zxUrl =
      StrUserUrl +
      "/Ask/auth/enter?platformType=" +
      platformType +
      "&sourceType=3&sourceId=0&targetURL=" +
      encodeURIComponent(
        "web/lookdoctor/main.html?doctoruid=" +
          doctorId +
          extendParam +
          "&doctorgroupId=100&utm_source=0.0.h.1066.bus007." +
          doctorId +
          "&t=" +
          new Date().getTime()
      );
    location.href = zxUrl;
  },
};

//广告系统
var adverService = {
  loadAdver: function () {
    var param = {
      pos: 133, //广告位id
      area: doctorInfoFirst.cityName,
      hos: doctorInfoFirst.hospitalId, //医院id
      depart: doctorInfoFirst.standardDeptId, //标准科室二级id
      num: 1, //取多少条
      isSpecifieDoc: 1,
      doctorUid: doctorId,
      channelId: channelId, //渠道id
    };
    window.adver.createNew().queryAdverList(param, this.getAdvar);
  },
  getAdvar: function (data) {
    if (data.Code === 10000 && data.Result && data.Result.length > 0) {
      var item = data.Result[0];
      var advList = "";
      advList +=
        '<a href="' +
        item.adverlink +
        '"><img style="width:100%;" src="' +
        item.adverimgurl +
        '" /></a>';
      $("#adverBox").html(advList);
    }
  },
};

function toServiceList() {
  myLayer.confirm({
    title: "温馨提示",
    con: "班外会诊费用不包括线下挂号费、药品费、手术费、住院费等线下诊疗费用",
    cancel: function () {},
    cancelValue: '<span class="c-999">取消</span>',
    ok: function () {
      var packUrl =
        "../servicePack/serviceList.html?doctorId=" +
        doctorId +
        "&platformType=" +
        platformType +
        "&utm_source=0.0.h.1066.bus088." +
        doctorId;
      location.href = packUrl;
    },
    okValue: '<span style="color:#4dcd70">确认</span>',
  });
}

var assistanceService = {
  checkShow: function (doctorSns) {
    var that = this;
    var ret = false;
    $.ajax({
      url: "../do/doctorArrange/checkArrangeHasNumber",
      timeout: 8000,
      type: "POST",
      dataType: "json",
      async: false,
      cache: false,
      data: {
        doctorSns: doctorSns,
        channelId: channelId,
      },
      success: function (json) {
        if (json.Code == 10000 && json.hasNumber == false) {
          ret = true;
        }
      },
    });
    return ret;
  },
  load: function (doctorSns) {
    var that = this;
    if (!(channelId == "1000025" || channelId == "1000007")) {
      return false;
    }
    $.ajax({
      url: "../do/doctor/getAssistance",
      timeout: 3000,
      type: "POST",
      dataType: "json",
      async: false,
      cache: true,
      data: {
        doctorUid: doctorId,
      },
      success: function (json) {
        if (json && json.Code == 10000) {
          if (
            json.Result &&
            isNotNull(json.Result) &&
            isNotNull(json.Result.assistanceQrUrl)
          ) {
            that.assistanceInfo = json.Result;
            if (that.checkShow(doctorSns)) {
              $("#assistance").show();
              if (that.assistanceInfo.allRegEmptyState == 1) {
                that.allArrangeFullShow();
              }
            }
          }
        }
      },
    });
  },
  show: function () {
    var that = this;
    var assistanceInfo = that.assistanceInfo;
    var title = isNotNull(assistanceInfo.arrangeTitle)
      ? assistanceInfo.arrangeTitle
      : isNotNull(assistanceInfo.assistanceNickname)
      ? assistanceInfo.assistanceNickname
      : "";
    var qrUrl = assistanceInfo.assistanceQrUrl;
    var html = "";
    html += '<div class="road-hold road-float" id="assistanceInfo">';
    html +=
      '<div class="road-mask" onclick="$(\'#assistanceInfo\').remove();"></div>';
    html += '<div class="road-pop">';
    html +=
      '<a href="javascript:$(\'#assistanceInfo\').remove();" class="road-close">×</a>';
    html += '<h4 class="c-000">' + title + "</h4>";
    html += '<img src="' + qrUrl + '"  id="assistanceQr"/>';
    html += '<p class="c-000 mt10">长按二维码添加为好友</p>';
    html += "</div>";
    html += "</div>";
    $("body").append(html);
    $("#assistanceInfo").show();
    saveSiteTrack("yz_erweima_page", {
      detailvalue: assistanceInfo.uid,
      scenename: "医生排班页",
    });

    var timeOutEvent;
    // 长按
    $("#assistanceQr").on({
      touchstart: function (e) {
        // 长按事件触发
        timeOutEvent = setTimeout(function () {
          timeOutEvent = 0;
          saveSiteTrack("yz_erweima_btn", {
            detailvalue: assistanceInfo.uid,
            scenename: "医生排班页",
          });
        }, 400);
        //长按400毫秒
        // e.preventDefault();
      },
      touchmove: function () {
        clearTimeout(timeOutEvent);
        timeOutEvent = 0;
      },
      touchend: function () {
        clearTimeout(timeOutEvent);
        if (timeOutEvent != 0) {
          // 点击事件
        }
        return false;
      },
    });
  },
  allArrangeFullShow: function () {
    var that = this;
    var assistanceInfo = that.assistanceInfo;
    var title = isNotNull(assistanceInfo.allRegEmptyTitle)
      ? assistanceInfo.allRegEmptyTitle
      : isNotNull(assistanceInfo.assistanceNickname)
      ? assistanceInfo.assistanceNickname
      : "";
    var qrUrl = assistanceInfo.assistanceQrUrl;
    var html = "";
    html += '<div class="road-hold road-float" id="assistanceInfo">';
    html +=
      '<div class="road-mask" onclick="$(\'#assistanceInfo\').remove();"></div>';
    html += '<div class="road-pop">';
    html +=
      '<a href="javascript:$(\'#assistanceInfo\').remove();" class="road-close">×</a>';
    html += '<h4 class="c-000">' + title + "</h4>";
    html += '<img src="' + qrUrl + '"  id="assistanceQr"/>';
    html += '<p class="c-000 mt10">长按二维码添加为好友</p>';
    html += "</div>";
    html += "</div>";
    $("body").append(html);
    $("#assistanceInfo").show();
    saveSiteTrack("yz_erweima_page", {
      detailvalue: assistanceInfo.uid,
      scenename: "全排班约满",
    });

    var timeOutEvent;
    // 长按
    $("#assistanceQr").on({
      touchstart: function (e) {
        // 长按事件触发
        timeOutEvent = setTimeout(function () {
          timeOutEvent = 0;
          saveSiteTrack("yz_erweima_btn", {
            detailvalue: assistanceInfo.uid,
            scenename: "全排班约满",
          });
        }, 400);
        //长按400毫秒
        // e.preventDefault();
      },
      touchmove: function () {
        clearTimeout(timeOutEvent);
        timeOutEvent = 0;
      },
      touchend: function () {
        clearTimeout(timeOutEvent);
        if (timeOutEvent != 0) {
          // 点击事件
        }
        return false;
      },
    });
  },
  arrangeFullShow: function (deptId, firstCanRegisterStr) {
    var that = this;
    var assistanceInfo = that.assistanceInfo;
    var deptName = "";
    var currDoctor = doctorInfo.filter(function (d) {
      return d.hosDeptId == deptId;
    });
    if (currDoctor.length > 0) {
      deptName = currDoctor[0].deptName;
    }
    var title = isNotNull(assistanceInfo.regEmptyTitle)
      ? assistanceInfo.regEmptyTitle
      : isNotNull(assistanceInfo.assistanceNickname)
      ? assistanceInfo.assistanceNickname
      : "";
    if (isNotNull(firstCanRegisterStr)) {
      title =
        "该排班已约满，最近可约日期为" + firstCanRegisterStr + "，" + title;
    } else {
      title = "医生在" + deptName + "的排班都已约满，" + title;
    }
    var qrUrl = assistanceInfo.assistanceQrUrl;
    var html = "";
    html += '<div class="road-hold road-float" id="assistanceInfo">';
    html +=
      '<div class="road-mask" onclick="$(\'#assistanceInfo\').remove();"></div>';
    html += '<div class="road-pop">';
    html +=
      '<a href="javascript:$(\'#assistanceInfo\').remove();" class="road-close">×</a>';
    html += '<h4 class="c-000">' + title + "</h4>";
    html += '<img src="' + qrUrl + '"  id="assistanceQr"/>';
    html += '<p class="c-000 mt10">长按二维码添加为好友</p>';
    html += "</div>";
    html += "</div>";
    $("body").append(html);
    $("#assistanceInfo").show();
    saveSiteTrack("yz_erweima_page", {
      detailvalue: assistanceInfo.uid,
      scenename: "单排班约满",
    });

    var timeOutEvent;
    // 长按
    $("#assistanceQr").on({
      touchstart: function (e) {
        // 长按事件触发
        timeOutEvent = setTimeout(function () {
          timeOutEvent = 0;
          saveSiteTrack("yz_erweima_btn", {
            detailvalue: assistanceInfo.uid,
            scenename: "单排班约满",
          });
        }, 400);
        //长按400毫秒
        // e.preventDefault();
      },
      touchmove: function () {
        clearTimeout(timeOutEvent);
        timeOutEvent = 0;
      },
      touchend: function () {
        clearTimeout(timeOutEvent);
        if (timeOutEvent != 0) {
          // 点击事件
        }
        return false;
      },
    });
  },
  arrangeFullHbShow: function (
    obj,
    parameObj,
    fun,
    deptId,
    firstCanRegisterStr
  ) {
    var that = this;
    that.hbObj = obj;
    that.hbParameObj = parameObj;
    that.hbFun = fun;
    var assistanceInfo = that.assistanceInfo;
    var deptName = "";
    var currDoctor = doctorInfo.filter(function (d) {
      return d.hosDeptId == deptId;
    });
    if (currDoctor.length > 0) {
      deptName = currDoctor[0].deptName;
    }
    var title = "";
    if (GetUserIsVip()) {
      title = isNotNull(assistanceInfo.hbGuideTitle)
        ? assistanceInfo.hbGuideTitle
        : isNotNull(assistanceInfo.assistanceNickname)
        ? assistanceInfo.assistanceNickname
        : "";
    } else {
      title = isNotNull(assistanceInfo.regEmptyTitle)
        ? assistanceInfo.regEmptyTitle
        : isNotNull(assistanceInfo.assistanceNickname)
        ? assistanceInfo.assistanceNickname
        : "";
    }
    if (isNotNull(firstCanRegisterStr)) {
      title =
        "该排班已约满，最近可约日期为" + firstCanRegisterStr + "，" + title;
    } else {
      title = "医生在" + deptName + "的排班都已约满，" + title;
    }
    var qrUrl = assistanceInfo.assistanceQrUrl;
    var html = "";
    html += '<div class="road-hold road-float" id="assistanceInfo">';
    html +=
      '<div class="road-mask" onclick="$(\'#assistanceInfo\').remove();"></div>';
    html += '<div class="road-pop">';
    html +=
      '<a href="javascript:$(\'#assistanceInfo\').remove();" class="road-close">×</a>';
    html += '<h4 class="c-000">' + title + "</h4>";
    html += '<img src="' + qrUrl + '"  id="assistanceQr"/>';
    html += '<p class="c-000 mt10">长按二维码添加为好友</p>';
    if (GetUserIsVip()) {
      html += '<div class="mt10">';
      html +=
        '<a href="javascript:assistanceService.toHb();" class="wait-btn">候补</a>';
      html += "</div>";
    }
    html += "</div>";
    html += "</div>";
    $("body").append(html);
    $("#assistanceInfo").show();
    saveSiteTrack("yz_erweima_page", {
      detailvalue: assistanceInfo.uid,
      scenename: "单排班候补",
    });

    var timeOutEvent;
    // 长按
    $("#assistanceQr").on({
      touchstart: function (e) {
        // 长按事件触发
        timeOutEvent = setTimeout(function () {
          timeOutEvent = 0;
          saveSiteTrack("yz_erweima_btn", {
            detailvalue: assistanceInfo.uid,
            scenename: "单排班候补",
          });
        }, 400);
        //长按400毫秒
        // e.preventDefault();
      },
      touchmove: function () {
        clearTimeout(timeOutEvent);
        timeOutEvent = 0;
      },
      touchend: function () {
        clearTimeout(timeOutEvent);
        if (timeOutEvent != 0) {
          // 点击事件
        }
        return false;
      },
    });
  },
  toHb() {
    var that = this;
    GuaHaoService.onDoctorGhAffirm(that.hbObj, that.hbParameObj, that.hbFun);
  },
};
