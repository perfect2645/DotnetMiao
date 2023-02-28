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
var result  = converter.UrlDecode("doctorRegOrder=%7B%22waterId%22%3A%22789515170%22%2C%22waitingInfor%22%3A%2208%3A00-08%3A05%22%2C%22store%22%3A%22%22%2C%22serialNo%22%3A%221%22%2C%22memberSn%22%3A%2256311966%22%2C%22memberName%22%3A%22%E5%BE%90%E9%A2%96%22%2C%22memberPhone%22%3A%2213940897525%22%2C%22memberAddress%22%3A%22%E5%8D%97%E4%BA%AC%22%2C%22memberBirthday%22%3A%221994-05-22%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2256311966%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%22320***********2641%22%2C%22memberIdcard%22%3A%22320***********2641%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2213940897525%22%2C%22doctorSn%22%3A%22710869460%22%2C%22hosDeptId%22%3A%227175975%22%2C%22hosDeptName%22%3A%22%E5%84%BF%E7%AB%A5%E4%BF%9D%E5%81%A5%E7%A7%91%22%2C%22hospitalId%22%3A%221040231%22%2C%22hospitalName%22%3A%22%E5%90%88%E8%82%A5%E5%B8%82%E8%9C%80%E5%B1%B1%E5%8C%BA%E5%8D%97%E5%B2%97%E9%95%87%E5%8D%AB%E7%94%9F%E9%99%A2%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710659564%22%2C%22doctorName%22%3A%22%E5%84%BF%E4%BF%9D%E7%A7%91%E5%8C%BB%E7%94%9F%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E4%B8%BB%E6%B2%BB%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2212%22%2C%22GH_HosProName%22%3A%22%E5%AE%89%E5%BE%BD%22%2C%22GH_HosCityId%22%3A%22101%22%2C%22GH_HosCityName%22%3A%22%E5%90%88%E8%82%A5%22%2C%22registerDate%22%3A%222023-03-01%22%2C%22timeId%22%3A1%2C%22arrangeId%22%3A168360087%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22availablenum%22%3A10%2C%22UnOpened%22%3Afalse%2C%22FHTimes%22%3A%2223%3A00%22%2C%22FHDays%22%3A%222%22%2C%22accountSn%22%3A148528129%2C%22cardNumber%22%3A%222078631533%22%2C%22loginId%22%3A%22od0AjwJ0eodDYYA4pUfDRUCIIm8E%22%2C%22channelId%22%3A%229000370%22%2C%22utm_source%22%3A%220.0.h.1026.bus010.0__0.0.h.1026.bus010.0%22%2C%22doctorOfficeName%22%3A%22%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22320***********2641%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%22%E5%8D%97%E4%BA%AC%22%2C%22keyName%22%3A%22familyaddress%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%2213940897525%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();

Console.ReadLine();