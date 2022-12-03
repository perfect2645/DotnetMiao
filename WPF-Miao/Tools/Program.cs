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
var result  = converter.UrlDecode("doctorRegOrder=%7B%22memberSn%22%3A%2253937899%22%2C%22memberName%22%3A%22%E7%8E%8B%E5%A4%A9%E5%A8%81%22%2C%22memberPhone%22%3A%2213322243359%22%2C%22memberAddress%22%3A%22%E4%B8%8D%E5%91%8A%E8%AF%89%E4%BD%A0%22%2C%22memberBirthday%22%3A%222002-10-06%22%2C%22memberSex%22%3A%222%22%2C%22memberId%22%3A%2253937899%22%2C%22othercard%22%3A%22%22%2C%22guardianmembersn%22%3A%22%22%2C%22identitytype%22%3A%221%22%2C%22memberZhengjianid%22%3A%224514**********6943%22%2C%22memberIdcard%22%3A%224514**********6943%22%2C%22accManageGoodSn%22%3Anull%2C%22regpaytype%22%3A%22%22%2C%22cliniccard%22%3A%22%22%2C%22applyNo%22%3A%22%22%2C%22mobile%22%3A%2213322243359%22%2C%22accountSn%22%3A142687055%2C%22cardNumber%22%3A%222072800766%22%2C%22hosDeptId%22%3A%227235364%22%2C%22doctorService_gh%22%3A%222%22%2C%22doctorUid%22%3A%22710796399%22%2C%22doctorName%22%3A%22%E4%B9%9D%E4%BB%B7%E7%96%AB%E8%8B%97%22%2C%22doctorPic%22%3A%22%22%2C%22doctorClinicName%22%3A%22%E5%8C%BB%E5%B8%88%22%2C%22GH_HosProId%22%3A%2210%22%2C%22GH_HosProName%22%3A%22%E6%B1%9F%E8%8B%8F%22%2C%22GH_HosCityId%22%3A%2277%22%2C%22GH_HosCityName%22%3A%22%E5%8D%97%E4%BA%AC%22%2C%22timeId%22%3A1%2C%22ghAmount%22%3A0%2C%22securityDeposit%22%3A0%2C%22ghfeeway%22%3A0%2C%22ModeId%22%3A0%2C%22GhFee%22%3A0%2C%22AllFee%22%3A0%2C%22store%22%3A%22%22%2C%22UnOpened%22%3Afalse%2C%22loginId%22%3A%22off6ts0dsj7egTDEnzoc7pz7g_FY%22%2C%22channelId%22%3A%221000031%22%2C%22doctorOfficeName%22%3A%22%22%2C%22isread%22%3A%221%22%2C%22birthday%22%3A%222002-10-06%22%2C%22registerDate%22%3A%222022-12-09%22%2C%22hosDeptName%22%3A%22%E5%AE%AB%E9%A2%88%E7%99%8C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%97%A8%E8%AF%8A%EF%BC%88%E9%A6%96%E9%92%88%EF%BC%89%22%2C%22hospitalId%22%3A%221099108%22%2C%22hospitalName%22%3A%22%E9%9B%A8%E8%8A%B1%E7%BB%8F%E6%B5%8E%E5%BC%80%E5%8F%91%E5%8C%BA%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E4%B8%AD%E5%BF%83%22%2C%22availablenum%22%3A6%2C%22FHTimes%22%3A%2208%3A00%22%2C%22FHDays%22%3A%227%22%2C%22waterId%22%3A763783584%2C%22waitingInfor%22%3A%2209%3A00-09%3A30%22%2C%22serialNo%22%3A%2249%22%2C%22doctorSn%22%3A%22711230166%22%2C%22arrangeId%22%3A165074306%7D&ghFormCon=%5B%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22name%22%7D%2C%7B%22keyValue%22%3A%224514**********6943%22%2C%22keyName%22%3A%22CardNo%22%7D%2C%7B%22keyValue%22%3A%222%22%2C%22keyName%22%3A%22sex%22%7D%2C%7B%22keyValue%22%3A%2213322243359%22%2C%22keyName%22%3A%22mobile%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22cardtype%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%220%22%2C%22keyName%22%3A%22cmb_disease%22%7D%2C%7B%22keyValue%22%3A%22%E6%9C%AA%E7%A1%AE%E8%AF%8A%22%2C%22keyName%22%3A%22cmb_diseaseName%22%7D%2C%7B%22keyValue%22%3A%22%22%2C%22keyName%22%3A%22ClinicCard%22%7D%2C%7B%22keyValue%22%3A%222002-10-06%22%2C%22keyName%22%3A%22birthday%22%7D%2C%7B%22keyValue%22%3A%221%22%2C%22keyName%22%3A%22isread%22%7D%5D", Encoding.UTF8);

var de = converter.UrlDecode(@"name=%E5%88%98%E4%BA%91&memberSn=54193649", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodeNormal("HTx7Z8VfeugcBEpzgw7zD1pbdRJQPbjzxdWbAXgrOVoXDM0eeI0YP6Unv0m86uZbQi5x/AnFATDU8+ytCnck3RdfzEZeJyk4YE8rh2dywIEjlLgAIUTfIHQXBPHy4HJw+ub1u8M8Rrbbyog3h+ki3u5LXe2NHcEjljAqHIjy9iAgt0Td1kwuUPMjMG4pbnNTKHEVHbn0cunOuBSdE8Qj69d3CTkWs73E/1SeCFbpIyTUE4JOtP75WepKgTWqppIM6ULvA6PnXx2C5YEpUegFdzYaOIDL+cy+dau0bKrT7Gp8zdh/kfUOQlnpe+XqwzX+L1A1sN2tFsa5tm/jX0onibEGMzkmjBhdnvE3Rlsi65zvR9b0Bs/RMEkmD5c7eISiBPBXNgRdgBROXqqBVqhP2kCqrnnKY0VcYRulk89y9tvvTZBamxWMtze1s0cjYpRT");

Console.ReadLine();