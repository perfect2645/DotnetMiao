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
var result  = converter.UrlDecode("doctorRegOrder=%7B%22memberSn%22%3A%2256919725%22%2C%22memberName%22%3A%22%E6%9E%97%E6%80%A1%E9%9D%92%22%2C%22memberPhone%22%3A%2217124961419%22%2C%22memberAddress%22%3A%22%E6%BC%B3%E5%B7%9E%22%2C%22memberBirthday%22%3A%222000-06-02%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2256919725%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%223506**********2549%22%2C%22memberIdcard%22%3A%223506**********2549%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2217124961419%22%2C%22accountSn%22%3A156166098%2C%22cardNumber%22%3A%222086251473%22%2C%22hosDeptId%22%3A%227244902%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710813045%22%2C%22doctorName%22%3A%22%E9%A2%84%E9%98%B2%E6%8E%A5%E7%A7%8D%E7%A7%91%E5%AE%A4%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2213%22%2C%22GH_HosProName%22%3A%22%E7%A6%8F%E5%BB%BA%22%2C%22GH_HosCityId%22%3A%22123%22%2C%22GH_HosCityName%22%3A%22%E6%BC%B3%E5%B7%9E%22%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22store%22%3A%22%22%2C%22UnOpened%22%3Afalse%2C%22loginId%22%3A%22ovTa55n3Tvr2huU6RBbJ3Q0AX11s%22%2C%22channelId%22%3A%229001567%22%2C%22doctorOfficeName%22%3A%22%22%2C%22isread%22%3A%221%22%2C%22retid%22%3A%22%22%2C%22registerDate%22%3A%222023-04-13%22%2C%22hosDeptName%22%3A%22%E9%A2%84%E9%98%B2%E6%8E%A5%E7%A7%8D%22%2C%22hospitalId%22%3A%221101614%22%2C%22hospitalName%22%3A%22%E9%BE%99%E6%96%87%E5%8C%BA%E7%A2%A7%E6%B9%96%E8%A1%97%E9%81%93%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E4%B8%AD%E5%BF%83%22%2C%22availablenum%22%3A99%2C%22FHTimes%22%3A%2200%3A00%22%2C%22FHDays%22%3A%223%22%2C%22waterId%22%3A%22804435923%22%2C%22waitingInfor%22%3A%2209%3A00-09%3A30%22%2C%22serialNo%22%3A%2241%22%2C%22doctorSn%22%3A%22711276210%22%2C%22arrangeId%22%3A170017559%2C%22timeId%22%3A%221%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%223506**********2549%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%2217124961419%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%2C%7B%22keyValue%22%3A%22%E6%BC%B3%E5%B7%9E%22%2C%22keyName%22%3A%22familyaddress%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%222000-06-02%22%2C%22keyName%22%3A%22birthday%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22isread%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();

Console.ReadLine();