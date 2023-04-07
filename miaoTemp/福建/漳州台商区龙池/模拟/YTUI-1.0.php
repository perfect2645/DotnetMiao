!function(win){
  "use strict";
var ytui_cachedScript= new Array();
  
	var  ytui= function(){
    this.v = '2210.7'; //版本号 
	this.shop_info={};
	this.href_list={}; 
	 this.url_list;
	 
  }
 
 
 
 String.prototype.Replace_ALL=function(f,e){//吧f替换成e
    var reg=new RegExp(f,"g"); //创建正则RegExp对象   
    return this.replace(reg,e); 
}
     
  ytui.prototype.get_url=function(url_id)
  {
	   
	  if(this.href_list[url_id]){	return this.href_list[url_id];}
	 return url_id;	 
  }
  
    
  ytui.prototype.loadShop=function(callback)
  {
	  
	  var sid=this.getUrlParam('sid');
	  var ucode=this.getUrlParam('code');
	  if(!sid){sid=layui.router().search.sid;}
	  var uid=this.getUrlParam('uid');
	   var openCode=this.getUrlParam('openCode');
	 
	 var wx_token=this.getUrlParam('token');
	 
	 // location='gb.html';
	//return;
 
 
	    $.get('../../api/shop.php?cmd=shop_info_get&uid='+uid+'&sid='+sid+'&code='+ucode+'&openCode='+openCode +'&wx_token='+wx_token, function(res){
 
 
	
	if(res.code==-998)
	{
		//触发微信登录
		
  location='http://a.zzytrj.net/ytpay/uppay/weixin/return_openid.php?return_url='+encodeURIComponent(window.location.href)+'&';
	 
	 
	return;
	}
	
	if(res.code!=1)
	{
alert(res.msg);
  
	}
	
	
	
	
	if(res.js_ver){$.ytui_js_ver=res.js_ver;}//加载模块JS 版本号
	
	 $.ytui_js_ver='2210.7';//1672498749	
	if(res.layui_ver){layui.config({version:res.layui_ver});}//加载 LAYUI版本号
	  
	
		var shop_info={};	 		
		shop_info.tel=res.客服电话;
		shop_info.shop_code=res.shop_id;
		shop_info.shop_name=res.商家名称;
		shop_info.shop_class=res.商品分类;
		shop_info.ad1=res.横幅广告1;
		shop_info.ad2=res.横幅广告2;
		shop_info.ad3=res.横幅广告3;
		if(shop_info.ad1){shop_info.ad1=shop_info.ad1;}
		if(shop_info.ad2){shop_info.ad2=shop_info.ad2;}
		if(shop_info.ad3){shop_info.ad3=shop_info.ad3;}
		
		shop_info['ini']=res['ini'] || {};
		
		shop_info['url_list']=[
	 {id:'index',bar:1,labele:'首页',url:'../index/index.html',icon:'icon-home'}
	,{id:'fenlei',bar:1,labele:'分类',url:'../fenlei/fenlei.html',icon:'icon-app'}
	,{id:'gouwuche',bar:1,labele:'购物车',url:'../gouwuche/gouwuche.html',icon:'icon-cart'}
	,{id:'user',bar:1,labele:'我的',url:'../user/user.html',icon:'icon-me'}	
	,{id:'goods',bar:0,labele:'商品详情',url:'../goods/goods.html',icon:'icon-app'}
	,{id:'huodong',bar:0,labele:'分类列表',url:'../huodong/huodong.html',icon:'icon-app'}
		];
		
	 
	 
	 
	 
	 
 
		if(callback){callback( shop_info);}
	  
    },'json');  
	  
	  
	  
  }
 
  
  
 
  //高亮当前页按钮
  var active=function(id,bar){	
  
 
  var toole='<!-- 工具栏 -->';
		toole+=' <nav class="bar bar-tab bar_tab_'+id+'">';
		
		var bar_id=$('#'+id).attr('bar_id');
		if(!bar_id){return ;}
		
		$.each(bar,function(i,v){
			 
			 if(v.bar==1){
   if(bar_id==v.id){ toole+='<a class="tab-item  active no-transition" href="#">';
   }else{
	    toole+='<a class="tab-item  no-transition" href="'+v.url+'">';
   
   }
   
		toole+='<span class="icon '+v.icon+'"></span>';
		toole+='<span class="tab-label">'+v.labele+'</span>';
		if(v.id=='gouwuche')
		{
		toole+='<span class="badge spinfo_gouwuche_qty fenlei_gouwuche_qty" style="display:none"></span>';
		}
		toole+='</a>';
		
			 }
		
});


		 
	 
		toole+='</nav>';
 
 
 $('#'+id).append(toole);
  
  	  
   var btn=$('.tab-item'); 
  	$('.tab-item').removeClass('active');		
  $.each(btn, function(index, item){
  var tmp=$(item).attr('href');
if(tmp=='#'){$(item).addClass('active');}
  });
  
  
  
	
 
 
  
  
  }
  
 
 ytui.prototype.touchstart=function(id,ins)
 {
	 
if(ins.elemItem.length>1)//大于1幅图片的才开启手势
{
	 
  //横幅广告 手势滑动
  $(id).on("touchstart", function (e) {
            var startX = e.originalEvent.targetTouches[0].pageX;//开始坐标X
            $(this).on('touchmove', function (e) {
                arguments[0].preventDefault();//阻止手机浏览器默认事件
            });
            $(this).on('touchend', function (e) {
                var endX = e.originalEvent.changedTouches[0].pageX;//结束坐标X
                e.stopPropagation();//停止DOM事件逐层往上传播
                //alert("开始滑动" + startX + "到" + endX);
                if (endX - startX > 30) {
                   ins.slide("sub"); //  alert("向右滑动");
                }
                else if (startX - endX > 30) {
                    ins.slide("add"); //   alert("向左滑动");
                }
                $(this).off('touchmove touchend');
            });
        });	 
		
}
 }
 
  
 
 ytui.prototype.load_js=function(doc)//自动载入JS模块文件
 { 

  $(document).attr("title",this.shop_info.shop_name);
	
  var doc_id=doc.attr('id');
 
 active(doc_id,this.url_list);//生成底部工具栏，并高亮当前页按钮
   
   
   
 var url=doc.attr('js-url');
 var cache=doc.attr('cache') ;
  
 
 if(!url){return;} 
 
 
var r=0;
 
if(cache==true){
 
for (var s in ytui_cachedScript) { 
//如果某个数组已经下载到了本地 
if (ytui_cachedScript[s]==url) {r=1;} 
}
   }
 
  //console.log(footer.attr('url'));
if( r==0)
{ 
var options = $.extend(options || {}, { 
dataType: "script", 
url:url+"?_="+$.ytui_js_ver, 
cache:true,
 success:function(res){ 

 },
 error: function() { } 
}); 

if(cache==true){ytui_cachedScript.push(url); }//将url地址放入script标记数组中 
$.ajax(options); 
 }
 
 
 }
   
   
   
   
   
   //数字格式化，保留2位小数
 ytui.prototype.Float_to_str=function(value)
	{
		var $tint= parseFloat(value );
		$tint=$tint.toFixed(2);
		 
				
		if (isNaN($tint))
		{return '';}
	else{ return $tint}
		 
	}
	
	

	
	
	//获取地址栏参数值
 ytui.prototype.getUrlParam=function(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数			
            if (r != null) return unescape(r[2]); return ''; //返回参数值
			
			
        } 	
 
 
 
 
 
  //Ajax请求
  ytui.prototype.req = function(options){
    var that = this
    ,success = options.success
    ,error = options.error
  
    ,debug = function(){
      return setter.debug 
        ? '<br><cite>URL：</cite>' + options.url
      : '';
    };
    
    options.data = options.data || {};
    options.headers = options.headers || {};
    
     
    
    delete options.success;
    delete options.error;
	var lay_post_index=layer.load(2);
	setTimeout(function(){
  layer.close(lay_post_index);
}, 10000);

    return $.ajax($.extend({
      type: 'get'
      ,dataType: 'json'
      ,success: function(res){
  
  layer.close(lay_post_index);
  
 
 
        //只有 response 的 code 一切正常才执行 done
        if(res['code'] ==1) {
          typeof options.done === 'function' && options.done(res); 
        } 
        
       
        
        //其它异常
        else {
				layer.msg('<div style="font-size:18px;">'+(res['msg'] || '返回状态码异常')+'</div>',{anim: 6,offset: 't'});
			
			if(res['code']==-999)
			{  
				$.router.loadPage({
				  url: "../user/user_bind.html",
				  noAnimation: false 
				  
				});
			}



	if(res['code']==-998)
	{
		//触发微信登录
		
 	location='http://a.zzytrj.net/ytpay/uppay/weixin/return_openid.php?return_url='+encodeURIComponent(window.location.href)+'&';
	 
	 
	}
	

	
        }
		
		
	 typeof success === 'function' && success(res);
				 
        //只要 http 状态码正常，无论 response 的 code 是否正常都执行 success
        
      }
      ,error: function(e, code){
            layer.close(lay_post_index);
        typeof error === 'function' && error(res);
      }
    }, options));
	
	

  };
  
 
 
 
 
 //延迟1秒显示加载条
 var show_tip=0
  
   $(window).on("pageLoadStart", function() {
	show_tip=1;
  setTimeout(function () {
 if(show_tip==1){
	 $.showIndicator() ;
	 
	  setTimeout(function () { show_tip=0; $.hideIndicator();  }, 10000);
	
	 }		
    }, 1000);	
    });
	
  $(window).on("pageLoadComplete", function() {
	show_tip=0;
	 $.hideIndicator();
    });
	
	 
 //延迟1秒显示加载条  END 
	
	
	
 
    $(window).on("pageLoadError", function() {
 
      layer.msg("加载失败，请检查网络！",{offset: 't'});
    });
	
	
 
	 /*  
  
   $.ajax({
   url:'user.html',
   dataType:'text',
   data:{},
   beforeSend :function(xmlHttp){
  
    xmlHttp.setRequestHeader("Cache-Control","no-cache");
   },
   success:function(response){
     //操作
   }
 
 });
 
 
  */
 
   win.ytui = new ytui(); 
 
 
   $(document).on("click", "a", function(e) {
	   
	    var href_list=win.ytui.href_list;
	    	   
		var href=$(this).attr('href');//#index#
	 
		if(!href ){return ;}
 
		var	temp = href.split('#');
		var url_id=temp[1];
 
		if(href_list[url_id]){		
		var tmp= href.replace('#'+url_id+'#',href_list[url_id]);			   	   
		$(this).attr('href',tmp);  	 
		}

 
 
    });
	
	
  function out_index(begin_time,xianzhi_time)
  {
	  
//	var begin_time='16:00:00';
	//var xianzhi_time=60*30;//
	
	
	//如果time2大于time1 返回true 否则 返回false
	  function compareTime(time1,time2) {
	  
	  return time_to_sec(time2)-time_to_sec(time1);
        if(time_to_sec(time2)-time_to_sec(time1)>0){ 
            return true;
        }
        return false;
   }

//将时分秒转为时间戳
   function time_to_sec(time) {
        if (time !== null) {
            var s = "";
            var hour = time.split(":")[0];
            var min = time.split(":")[1];
            var sec = time.split(":")[2];
            s = Number(hour * 3600) + Number(min * 60) + Number(sec);
            return s;
        }
    }
	
	function get_time()
	{
			var myDate = new Date(); //实例一个时间对象； 
		//	myDate.getFullYear();   //获取系统的年； 
			//myDate.getMonth()+1;   //获取系统月份，由于月份是从0开始计算，所以要加1 
			//myDate.getDate(); // 获取系统日， 
			//myDate.getHours(); //获取系统时， 
			//myDate.getMinutes(); //分 
			//myDate.getSeconds(); //秒
			return myDate.getHours()+":"+myDate.getMinutes()+":"+myDate.getSeconds();
			}


			function getTime_str(time) {
			// 转换为式分秒
			var h = parseInt(time / 60 / 60 % 24)
			if(h==0){
			h='';
			}else{
			h=h+'小时';
			}
			//h = h < 10 ? '0' + h : h
			var m = parseInt(time / 60 % 60)
			if(m==0){
			m='';
			}else{
			m=m+'分钟';
			}
			
			var s = parseInt(time % 60)
			s=s+'秒';
			
			
			// 作为返回值返回
			return h+ m+ s;
			}


	  
	  
  	var now_time=get_time();
	var qty=compareTime(now_time,begin_time);
	
	if(qty>0 && qty<xianzhi_time){
		 window.location.replace("../index/paidui.html");
console.log('end time');

  return ;
	}
	
  
  
	  
  }
     
  var getnow = new Date();  
     var getdate = getnow.getDate();//得到日期
	 if(getdate==1 ||  getdate==15){
		 
	setInterval(function() {    
 out_index('17:00:00',60*30);	 
}, 1000);

	 }
 



  
  
  
  
  
  
  }(window);
  
  
 /* 
         $(document).off('click','.title').on('click','.title',function(){//点击标题刷新页面
		 
  window.location.reload()
	   });
	   
	   
  */  
 
 
  //初始化页面
$(document).on("pageInit", function(e, pageId, $page) {	 




var xl=1;

var xl=0;var xl=0;
console.log('xl='+xl);

if(xl==1){
 
 layui.data('switch', { key: 'zjsj_s'  ,value: '2022-12-31 17:00:00'});
 layui.data('switch', { key: 'zjsj_e'  ,value: '2022-12-31 17:15:00'});
}else{
 
layui.data('switch', { key: 'zjsj_s'   ,remove: true});
layui.data('switch', { key: 'zjsj_e'  ,remove: true});
 
}
 
 
 
 
 var sw=1;//是否显示通知  
 
 var sw=0;var sw=1; 
 if(sw==0){	 
layui.data('switch', { key: 'hide_4824'  ,value: '1'});
layui.data('switch', { key: 'hide_4849'  ,value: '1'});
layui.data('switch', { key: 'hide_4848'  ,value: '1'});
layui.data('switch', { key: 'hide_4850'  ,value: '1'});
 }
 else{
layui.data('switch', { key: 'hide_4824'  ,value: '0'});
layui.data('switch', { key: 'hide_4849'  ,value: '0'});
layui.data('switch', { key: 'hide_4848'  ,value: '0'});
layui.data('switch', { key: 'hide_4850'  ,value: '0'});
 }













	layui.data('ver', { key: 'val'  ,value: 'v2210.7'});
     layui.config({version:layui.data('ver').val});
  
if(!ytui.shop_info.shop_code || ytui.getUrlParam('sid')!='' )
 {     
	 ytui.loadShop(function(e){
	    
		
		
	//加载页面索引	
	ytui.url_list=e['url_list'];  
  	var href_list={};
		$.each(ytui.url_list, function(index, item){
		href_list[item.id]=item.url;
		});	
	 ytui.href_list=href_list;
	 
	 
	 
	 ytui.shop_info=e;	 	 
	ytui.load_js($page);	
	 });	 
	 
 }
 else
 { 
ytui.load_js($page);

 }
	  
	  
 
 
 });
 