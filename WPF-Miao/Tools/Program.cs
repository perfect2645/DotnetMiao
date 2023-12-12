// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Decode;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

//var jmbDecode = new JmbDecode();

new DateTest();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("doctorRegOrder=%7B%22memberSn%22%3A%2256761558%22%2C%22memberName%22%3A%22%E5%80%99%E8%BF%9B%E6%A2%85%22%2C%22memberPhone%22%3A%2215055158121%22%2C%22memberAddress%22%3A%22%E5%90%88%E8%82%A5%22%2C%22memberBirthday%22%3A%221994-01-10%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2256761558%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%223412**********4287%22%2C%22memberIdcard%22%3A%223412**********4287%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2215055158121%22%2C%22accountSn%22%3A155646401%2C%22cardNumber%22%3A%222085732827%22%2C%22hosDeptId%22%3A%227210427%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710786713%22%2C%22doctorName%22%3A%22%E4%B9%9D%E4%BB%B7HPV%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E4%B8%BB%E6%B2%BB%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2212%22%2C%22GH_HosProName%22%3A%22%E5%AE%89%E5%BE%BD%22%2C%22GH_HosCityId%22%3A%22101%22%2C%22GH_HosCityName%22%3A%22%E5%90%88%E8%82%A5%22%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22store%22%3A%22%22%2C%22UnOpened%22%3Afalse%2C%22loginId%22%3A%22oHN-twvtIGqwuHVRfKjpHqg0rGok%22%2C%22channelId%22%3A%229001016%22%2C%22doctorOfficeName%22%3A%22%22%2C%22isread%22%3A%221%22%2C%22retid%22%3A%22%22%2C%22birthday%22%3A%221994-01-10%22%2C%22registerDate%22%3A%222023-12-13%22%2C%22hosDeptName%22%3A%22%E6%88%90%E4%BA%BA%E7%96%AB%E8%8B%97%E9%A2%84%E7%BA%A6%22%2C%22hospitalId%22%3A%221031364%22%2C%22hospitalName%22%3A%22%E8%9C%80%E5%B1%B1%E5%8C%BA%E8%A5%BF%E5%9B%AD%E8%A1%97%E9%81%93%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E4%B8%AD%E5%BF%83%22%2C%22availablenum%22%3A150%2C%22FHTimes%22%3A%2220%3A30%22%2C%22FHDays%22%3A%222%22%2C%22waterId%22%3A885502734%2C%22waitingInfor%22%3A%22%E7%AC%AC65%E5%8F%B7%2015%3A00-15%3A30%22%2C%22serialNo%22%3A%2265%22%2C%22doctorSn%22%3A%22711188596%22%2C%22arrangeId%22%3A179857091%2C%22timeId%22%3A%222%22%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%223412**********4287%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%2215055158121%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%2C%7B%22keyValue%22%3A%22%E5%90%88%E8%82%A5%22%2C%22keyName%22%3A%22familyaddress%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%221994-01-10%22%2C%22keyName%22%3A%22birthday%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22isread%22%7D%5D", Encoding.UTF8);

var aescbc = new AesCBC();

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("lpklEBvry7LYTNrPchHySrvYnAvGI4+DumoPjFzG4fPPkj5UDziGgEqvEGWrwBbmV4s6HBE2RZhL3Ku1uhs/XA==");

//var httpEncode = new HttpEncode();


//var ngarihealth = new Ngarihealth();

Console.ReadLine();