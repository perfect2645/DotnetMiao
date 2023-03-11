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
var result  = converter.UrlDecode("doctorRegOrder=%7B%22waterId%22%3A%22793358257%22%2C%22waitingInfor%22%3A%2214%3A00-17%3A00%22%2C%22store%22%3A%22%22%2C%22serialNo%22%3A%221%22%2C%22memberSn%22%3A%2256626950%22%2C%22memberName%22%3A%22%E8%B5%B5%E8%8E%89%E5%A8%9F%22%2C%22memberPhone%22%3A%2213940897525%22%2C%22memberAddress%22%3A%221%22%2C%22memberBirthday%22%3A%221998-06-11%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2256626950%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%22511***********6089%22%2C%22memberIdcard%22%3A%22511***********6089%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2213940897525%22%2C%22doctorSn%22%3A%22711157165%22%2C%22hosDeptId%22%3A%227222961%22%2C%22hosDeptName%22%3A%22%E5%85%A8%E7%A7%91%22%2C%22hospitalId%22%3A%221047032%22%2C%22hospitalName%22%3A%22%E5%8C%85%E6%B2%B3%E5%8C%BA%E5%B8%B8%E9%9D%92%E8%A1%97%E9%81%93%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E4%B8%AD%E5%BF%83%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710777083%22%2C%22doctorName%22%3A%22%E7%8E%8B%E9%94%8B%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E5%89%AF%E4%B8%BB%E4%BB%BB%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2212%22%2C%22GH_HosProName%22%3A%22%E5%AE%89%E5%BE%BD%22%2C%22GH_HosCityId%22%3A%22101%22%2C%22GH_HosCityName%22%3A%22%E5%90%88%E8%82%A5%22%2C%22registerDate%22%3A%222023-03-17%22%2C%22timeId%22%3A2%2C%22arrangeId%22%3A168809675%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22availablenum%22%3A9%2C%22UnOpened%22%3Afalse%2C%22FHTimes%22%3A%2200%3A00%22%2C%22FHDays%22%3A%227%22%2C%22accountSn%22%3A155046217%2C%22cardNumber%22%3A%222085134021%22%2C%22loginId%22%3A%22oQKsCwp-QUcWFDojDLNbTWdzlXjU%22%2C%22channelId%22%3A%229000415%22%2C%22utm_source%22%3A%220.0.h.1026.bus010.0__0.0.h.1026.bus010.0%22%2C%22doctorOfficeName%22%3A%22%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%22511***********6089%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22familyaddress%22%7D%2C%7B%22keyValue%22%3A%2213940897525%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();

Console.ReadLine();