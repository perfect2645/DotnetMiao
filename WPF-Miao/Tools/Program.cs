// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("name=%E5%88%98%E4%BA%91&memberSn=54193649", Encoding.UTF8);

var de = converter.UrlDecode(@"name=%E5%88%98%E4%BA%91&memberSn=54193649", Encoding.UTF8);

var en = converter.UrlEncode(de);

Console.ReadLine();