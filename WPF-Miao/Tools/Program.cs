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
var result  = converter.UrlDecode("5RGp2oQK6AmbKv2DlOPycH7bubDQq3UH67YhSiKWkFTOzwqV3w/jVjTxQHvtlzj%2B%0D%0AZZ16b7ipmLCHpCo5TiAJB17f3PZUSiCxa78jeCVWodS7SiKil5f%2BxBph11JUJyMd%0D%0ANkAip81RGAJa4dHMm98baDdpkvbN0AsoupsEIXfYmdi2te5Ga1Gx/QNToMXnHllt%0D%0AdUJdby8bqT1knsyadvIryu4aSdkSiBqlY1hwugeeXoGVLnqZbJk8yl61CqCStztC%0D%0A0Wq3%2Bqnn9CfiToNwC9xUa23yoMTi4uX5vlCNtJKkLZh/6bbJdz67OygRZvC3Qcda%0D%0AFbhCycNahbuq05JDnGOpse2V0o%2BCb4mg%2BSa6gS%2BsnTUuN6T5s4VZwxBT2QBoqZZ4%0D%0ASGQReg%2B1IfRFsfkE2GRN46mySowOGOzr%2BQKTunIcV%2BjVE3hLCkG5GgOe7%2BUkmQBD%0D%0AIm8TsLdR%2BTox/cbh3N0UxuXuDNbxADNwlZwN6Ns8yhTOtGRtOCIgXodRto5J7zkd%0D%0AQcu3n4Yxu0jMwTYm377kspIKsY/zUUmfQf3FCk%2BtDXkgEkicCLMXJ06TNbNB4sIw%0D%0AIGv7Z9g7bISubhc7%2Bgszac4A8JSFPd0TNDE/7YmbhyU%3D", Encoding.UTF8);

var de = converter.UrlDecode(@"name=%E5%88%98%E4%BA%91&memberSn=54193649", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("5RGp2oQK6AmbKv2DlOPycH7bubDQq3UH67YhSiKWkFTOzwqV3w/jVjTxQHvtlzj+ZZ16b7ipmLCHpCo5TiAJB17f3PZUSiCxa78jeCVWodS7SiKil5f+xBph11JUJyMdNkAip81RGAJa4dHMm98baDdpkvbN0AsoupsEIXfYmdi2te5Ga1Gx/QNToMXnHlltdUJdby8bqT1knsyadvIryu4aSdkSiBqlY1hwugeeXoGVLnqZbJk8yl61CqCStztC0Wq3+qnn9CfiToNwC9xUa23yoMTi4uX5vlCNtJKkLZh/6bbJdz67OygRZvC3QcdaFbhCycNahbuq05JDnGOpse2V0o+Cb4mg+Sa6gS+snTUuN6T5s4VZwxBT2QBoqZZ4SGQReg+1IfRFsfkE2GRN46mySowOGOzr+QKTunIcV+jVE3hLCkG5GgOe7+UkmQBDIm8TsLdR+Tox/cbh3N0UxuXuDNbxADNwlZwN6Ns8yhTOtGRtOCIgXodRto5J7zkdQcu3n4Yxu0jMwTYm377kspIKsY/zUUmfQf3FCk+tDXkgEkicCLMXJ06TNbNB4sIwIGv7Z9g7bISubhc7+gszac4A8JSFPd0TNDE/7YmbhyU=");

Console.ReadLine();