// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;
using Tools.Stringtest;

Console.WriteLine("Hello, World!");

var strConvert = new StringConvert();

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("data=%7B%22apiVersion%22%3A%22t141%22%2C%22appVersion%22%3A%221.5.1%22%2C%22areaCode%22%3A%22CS000016%22%2C%22channel%22%3A%22online%22%2C%22clientid%22%3A%22a7ea53059fc868e2e3e2dd7c04027035%22%2C%22device_id%22%3A%22z3tGgpTr98k8dQdVg6xeeSpiAMOQEPtRuq3x%22%2C%22time%22%3A1665849086011%2C%22reRule%22%3A%224%22%2C%22token%22%3A%22c5d372fd360d5f7f2dbdae25e08cb394%22%2C%22viewSize%22%3A%22720x1184%22%2C%22networkType%22%3A%22wifi%22%2C%22isSimulator%22%3Afalse%2C%22osType%22%3A%224%22%2C%22scopeType%22%3A1%2C%22businessType%22%3A1%2C%22businessId%22%3A%2230550001%22%2C%22deliveryCircleType%22%3A%221%22%2C%22body%22%3A%7B%22all_selected_status%22%3A0%2C%22is_get%22%3A1%2C%22action%22%3A2%2C%22item_detail_list%22%3A%5B%7B%22force_select%22%3A0%2C%22id%22%3A39825828%2C%22qty%22%3A1%2C%22other_qty%22%3A0%2C%22selected%22%3A1%7D%5D%2C%22select%22%3A0%2C%22store_id%22%3A%223055%22%7D%7D&h5=yx_touch&paramsMD5=5KQNXTBrNmbTiWrYKjPVbUPiQ9BJ0zI7k%2FWgSqVeo%2BE%3D", Encoding.UTF8);



Console.ReadLine();