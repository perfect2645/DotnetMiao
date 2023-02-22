// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Decode;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("doctorRegOrder=%7B%22waterId%22%3A%22734897710%22%2C%22waitingInfor%22%3A%2209%3A00-09%3A15%22%2C%22store%22%3A%22%22%2C%22serialNo%22%3A%224%22%2C%22memberSn%22%3A%2253937899%22%2C%22memberName%22%3A%22%E7%8E%8B%E5%A4%A9%E5%A8%81%22%2C%22memberPhone%22%3A%2213322243359%22%2C%22memberAddress%22%3A%22%E4%B8%8D%E5%91%8A%E8%AF%89%E4%BD%A0%22%2C%22memberBirthday%22%3A%222002-10-06%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2253937899%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%22451***********6943%22%2C%22memberIdcard%22%3A%22451***********6943%22%2C%22accManageGoodSn%22%3Anull%2C%22birthday%22%3A%222002-10-06%22%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22isread%22%3A%221%22%2C%22mobile%22%3A%2213322243359%22%2C%22doctorSn%22%3A%22711091344%22%2C%22hosDeptId%22%3A%227211903%22%2C%22hosDeptName%22%3A%22%E4%B9%9D%E4%BB%B7HPV%E7%96%AB%E8%8B%97%22%2C%22hospitalId%22%3A%221040231%22%2C%22hospitalName%22%3A%22%E5%90%88%E8%82%A5%E5%B8%82%E8%9C%80%E5%B1%B1%E5%8C%BA%E5%8D%97%E5%B2%97%E9%95%87%E5%8D%AB%E7%94%9F%E9%99%A2%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710749125%22%2C%22doctorName%22%3A%22%E5%BC%A0%E6%81%A9%E7%8E%89%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E6%8A%A4%E5%B8%88%22%2C%22GH_HosProId%22%3A%2212%22%2C%22GH_HosProName%22%3A%22%E5%AE%89%E5%BE%BD%22%2C%22GH_HosCityId%22%3A%22101%22%2C%22GH_HosCityName%22%3A%22%E5%90%88%E8%82%A5%22%2C%22registerDate%22%3A%222022-09-16%22%2C%22timeId%22%3A1%2C%22arrangeId%22%3A161887050%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22availablenum%22%3A2%2C%22UnOpened%22%3Afalse%2C%22FHTimes%22%3A%2222%3A00%22%2C%22FHDays%22%3A%221%22%2C%22accountSn%22%3A142687055%2C%22cardNumber%22%3A%222072800766%22%2C%22loginId%22%3A%22od0AjwEUaWF5JGJzaM8glsbPd9xU%22%2C%22channelId%22%3A%229000370%22%2C%22utm_source%22%3A%220.0.h.1026.bus010.0__0.0.h.1026.bus010.0%22%2C%22doctorOfficeName%22%3A%22%22%2C%22retId%22%3A%22422f33f8a8f240aa86ffe3afd0cfbab6%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22451***********6943%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22isread%22%7D%2C%7B%22keyValue%22%3A%22%E4%B8%8D%E5%91%8A%E8%AF%89%E4%BD%A0%22%2C%22keyName%22%3A%22familyaddress%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%222002-10-06%22%2C%22keyName%22%3A%22birthday%22%7D%2C%7B%22keyValue%22%3A%2213322243359%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();

Console.ReadLine();