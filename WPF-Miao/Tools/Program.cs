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
var result  = converter.UrlDecode("hospitalUserID=LC-1d752966508c4f02b96f7dd010229026&resourceID=$LC$FZAD8PFheSeN+r+HOhnleP/wHdJdlrvUgS954Kc+4f1/W83xKVx7usrarZb2OC4LU1AUWrxpzP1ElTR72MD4KfJlFLOChyEjMDoHm20rDHIPtnrvnatXkmVPPhK4Xfeo$LC$&registDate=2023-04-29+10%3A00-10%3A30&url=https%3A%2F%2Ffwcs.linkingcloud.cn%2FApp%2Fyuyue%2Findex.html%23%2Fpages%2Fyuyue%2Fdoctor%3FdataSource%3D&docCode=0400%7C%7C%E5%84%BF%E7%A7%91%E9%97%A8%E8%AF%8A%E6%99%AE%E9%80%9A%E5%8F%B7%7C%E5%84%BF%E7%A7%91%E9%97%A8%E8%AF%8A%7C%E6%99%AE%E9%80%9A%E9%97%A8%E8%AF%8A&hospitalID=01&docName=%E5%84%BF%E7%A7%91%E9%97%A8%E8%AF%8A%E6%99%AE%E9%80%9A%E5%8F%B7&docDuty=&deptCode=deptCode%3D0400%7C%E5%84%BF%E7%A7%91%E9%97%A8%E8%AF%8A%7C1%7C%E6%99%AE%E9%80%9A%E9%97%A8%E8%AF%8A&deptName=%E5%84%BF%E7%A7%91%E9%97%A8%E8%AF%8A&hospitalName=%E4%B8%8A%E6%B5%B7%E5%B8%82%E7%AC%AC%E5%9B%9B%E4%BA%BA%E6%B0%91%E5%8C%BB%E9%99%A2&docPhotoPath=&extInfo={}&feeType=&t=1", Encoding.UTF8);

var de = converter.UrlDecode(@"%E4%BA%8C%E4%BB%B7HPV%E8%BF%9B%E5%8F%A3", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

var httpEncode = new HttpEncode();


var ngarihealth = new Ngarihealth();

Console.ReadLine();