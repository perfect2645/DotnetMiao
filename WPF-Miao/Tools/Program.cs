// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("doctorRegOrder=%7B%22waterId%22%3A%22752811624%22%2C%22waitingInfor%22%3A%2208%3A00-10%3A00%22%2C%22store%22%3A%22%22%2C%22serialNo%22%3A%2237%22%2C%22memberSn%22%3A%2254985240%22%2C%22memberName%22%3A%22%E5%88%98%E6%8C%AF%E5%AE%87%22%2C%22memberPhone%22%3A%2213940897525%22%2C%22memberAddress%22%3A%22%E4%B8%9C%E5%9F%8E%E7%99%BD%E4%BA%91%E5%B1%B1%22%2C%22memberBirthday%22%3A%222000-03-10%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2254985240%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%22110***********5689%22%2C%22memberIdcard%22%3A%22110***********5689%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2213940897525%22%2C%22doctorSn%22%3A%22711170863%22%2C%22hosDeptId%22%3A%227225728%22%2C%22hosDeptName%22%3A%22%E5%AE%AB%E9%A2%88%E7%99%8C%E7%96%AB%E8%8B%97%E9%A2%84%E7%BA%A6%EF%BC%88%E9%A6%96%E9%92%88%EF%BC%89%22%2C%22hospitalId%22%3A%221094417%22%2C%22hospitalName%22%3A%22%E9%9B%A8%E8%8A%B1%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E4%B8%AD%E5%BF%83%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710782307%22%2C%22doctorName%22%3A%22%E5%9B%9B%E4%BB%B7%E5%AE%AB%E9%A2%88%E7%99%8C%E7%96%AB%E8%8B%97%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2210%22%2C%22GH_HosProName%22%3A%22%E6%B1%9F%E8%8B%8F%22%2C%22GH_HosCityId%22%3A%2277%22%2C%22GH_HosCityName%22%3A%22%E5%8D%97%E4%BA%AC%22%2C%22registerDate%22%3A%222022-11-11%22%2C%22timeId%22%3A1%2C%22arrangeId%22%3A164017825%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22availablenum%22%3A19%2C%22UnOpened%22%3Afalse%2C%22FHTimes%22%3A%2208%3A00%22%2C%22FHDays%22%3A%224%22%2C%22accountSn%22%3A148528129%2C%22cardNumber%22%3A%222078631533%22%2C%22loginId%22%3A%22off6ts1LqGhmQhNOcgJowx4kPRWM%22%2C%22channelId%22%3A%221000031%22%2C%22utm_source%22%3A%220.0.h.1026.bus010.0__0.0.h.1026.bus010.0%22%2C%22doctorOfficeName%22%3A%22%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%22110***********5689%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%2213940897525%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"phone_model%22%3A%22iPhone%20X%20(GSM%2BCDMA)%3CiPhone10%2C3%3E%22", Encoding.UTF8);

var en = converter.UrlEncode(de);

Console.ReadLine();