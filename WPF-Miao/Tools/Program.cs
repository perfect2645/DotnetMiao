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
var result  = converter.UrlDecode("hospitalUserID=LC-1d752966508c4f02b96f7dd010229026&resourceID=%24LC%24XYQYeMkHEOkz%2BkVgfHk%2BGeDjnHULn8QxL0GDXosSpeIvlN9LCsOOuXfipJeEisS8z85aGQwwE53FPrPoZe%2FipC%2BU30sKw465d%2BKkl4SKxLw4K9rw5Q0BJ3G165U9n5y8Igvcd5BRhttin9RV0cK%2F5g%3D%3D%24LC%24&registDate=2023-04-25%2015%3A30-16%3A00&url=https%3A%2F%2Ffwcs.linkingcloud.cn%2FApp%2Fyuyue%2Findex.html%23%2Fpages%2Fyuyue%2Fdoctor%3FdataSource%3D%26docCode%3D9289%7C%7C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A%7C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A%7C%E7%96%AB%E8%8B%97%E9%97%A8%E8%AF%8A%26hospitalID%3D01&docCode=9289%7C%7C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A%7C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A%7C%E7%96%AB%E8%8B%97%E9%97%A8%E8%AF%8A&docName=%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A&docDuty=&deptCode=9289%7C%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A%7C102%7C%E7%96%AB%E8%8B%97%E9%97%A8%E8%AF%8A&deptName=%E7%96%AB%E8%8B%97%E6%8E%A5%E7%A7%8D%E9%AB%98%E7%BA%A7%E4%B8%93%E5%AE%B6%E9%97%A8%E8%AF%8A&hospitalName=%E4%B8%8A%E6%B5%B7%E5%B8%82%E7%AC%AC%E5%9B%9B%E4%BA%BA%E6%B0%91%E5%8C%BB%E9%99%A2&docPhotoPath=&extInfo=%7B%7D&feeType=&t=1", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();

Console.ReadLine();