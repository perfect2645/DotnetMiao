// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Encode;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

new TestHmacSHA256();

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("%E4%B8%8B%E5%8D%88", Encoding.UTF8);

var de = converter.UrlDecode(@"phone_model%22%3A%22iPhone%20X%20(GSM%2BCDMA)%3CiPhone10%2C3%3E%22", Encoding.UTF8);

var en = converter.UrlEncode(de);

Console.ReadLine();