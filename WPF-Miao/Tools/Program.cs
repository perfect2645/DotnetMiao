// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("regtype=12&type=reg&deptId=5220066&deptName=%E4%BA%A7%E7%A7%91&doctorId=16825177&doctorName=%E5%90%B4%E6%A1%82%E9%BB%94&orgName=%E6%8F%AD%E9%98%B3%E5%AE%89%E7%9C%9F%E5%A6%87%E4%BA%A7%E5%8C%BB%E9%99%A2&customerName=%E8%B5%B5%E6%A2%A6%E4%BA%91&customerId=78083792&appointAmount=23&appointDate=2022-12-03&timeRange=16%3A00-16%3A30&remark=&newVersion=true&insuranceReg=0&medicalCardId=&childMedicalCardId=&scheduleId=10577817&insurancePromotion=false&bco01=3437", Encoding.UTF8);

var de = converter.UrlDecode(@"phone_model%22%3A%22iPhone%20X%20(GSM%2BCDMA)%3CiPhone10%2C3%3E%22", Encoding.UTF8);

var en = converter.UrlEncode(de);

Console.ReadLine();