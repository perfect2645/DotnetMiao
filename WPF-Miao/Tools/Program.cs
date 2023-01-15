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

var de = converter.UrlDecode(@"cmd=yuyue_post&date=2023-01-02+10%3A00%3A00&qty=1&dizhi=400&yuyue_name=%E5%91%A8%E7%91%A0%E5%A4%8F&yuyue_user_code=612426199806235426&yuyue_user_add=%E7%BF%A1%E7%BF%A0%E6%A2%A6%E5%A2%83&yuyue_user_suoshu=%E7%99%BE%E7%9B%9B", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

Console.ReadLine();