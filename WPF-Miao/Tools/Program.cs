// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Decode;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new DateTest();

//new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("%7B%22type%22%3A%22hcTransParam%22%2C%22plat%22%3A%22gzhc365zhyy%22%2C%22birthday%22%3A%221988-11-25%22%2C%22Address%22%3A%22%E8%BE%BD%E5%AE%81%E7%9C%81%E5%A4%A7%E8%BF%9E%E5%B8%82%E6%B2%99%E6%B2%B3%E5%8F%A3%E5%8C%BA+%E4%B8%87%E7%A7%91%E5%8D%8A%E5%B1%B1%E5%8D%8A%E6%B5%B7%22%7D", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();


var ngarihealth = new Ngarihealth();

Console.ReadLine();