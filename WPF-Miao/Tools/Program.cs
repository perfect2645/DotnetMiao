// See https://aka.ms/new-console-template for more information
using System.Text;
using Tools.Converter;

Console.WriteLine("Hello, World!");

var converter = new UnicodeConverter();
var result  = converter.UrlDecode("https://weixinauth.yihu.com/?m=wxauth&c=wxauth&tourl=https%3A%2F%2Fappoint.yihu.com%2Fappoint%2Fhospital%2FghDeptList.html%3FplatformType%3D9001026%26hospitalId%3D1047063%26_channel_id%3D9001026&code=031hkZZv36UM8Z23VT1w38Mt6X0hkZZc&state=1", Encoding.UTF8);



Console.ReadLine();