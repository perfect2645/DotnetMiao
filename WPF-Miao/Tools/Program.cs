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

var de = converter.UrlDecode(@"type_id=7&date=1672358400&type_title=%E6%B5%81%E6%84%9F%E7%96%AB%E8%8B%97%EF%BC%88%E8%87%AA%E8%B4%B9-3%E5%91%A8%E5%B2%81%E4%BB%A5%E4%B8%8A%EF%BC%89&member_id=45491
&member_name=%E5%91%A8%E7%91%A0%E5%A4%8F&timeType=6&key=9b4fdBbFyGmrXQ1BmBQIjLAcmNbMfmQPg%2BIcLHe4G%2BKCdDc&token=d53cedbc714cdffd25c6a557c2a3d25a", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

Console.ReadLine();