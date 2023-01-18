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
var result  = converter.UrlDecode("08%253A40", Encoding.UTF8);

var de = converter.UrlDecode(@"%5B%7B%22PAGE_CODE%22:%22appoint_confirm%22,%22ELEMENT_CODE%22:%22countAppointConfirm2%22,%22OPERATE_TIME%22:%222023-01-18+20:38:44%22,%22USER_ID%22:24020334,%22HOSPITAL_ID%22:%223110012%22%7D%5D", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

Console.ReadLine();