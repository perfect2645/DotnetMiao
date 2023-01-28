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

var de = converter.UrlDecode(@"LkYZqRj4BfUcUX4Tc9JGl/8A7iXdIjQFYrenclSG673/2mqsoXJYv23JJYsN2wP/kRwQkUEwqAO94gAig1LEefixedM0iRyHeQFKtOrGR0AvlDqu0IqLqVw3Iogsf0/N/4hyg/mP16LGFjRs8wnWrSmmf9eTvBF8maGgt9XOusm80jUAfM3ntbz82dnzMpH80TaCMfpldX3ucWheXukmngG/N/j2wCVwmmE1EHOdh9/znp/l9x5MyBxurfje1VMHtjrCMg/bFTFuq7zEjpZ/oPh73mlA90x+/uPxBsVC1+nNcuIK39iFcsA1bb7CO4DDhGhd940xtoUWm36/QfNkMWozVzdUDYvyDIJMFCqcV1/wg9JeebI4fyVEBc5VFmOTfkfrGKUmZtyPD8YRrjl0u+IYUpwrFqtJ7Bb4IyAnBsuUsQlYW9G/lf3Bhgg0/gIdS/qcAGxmf6hhYNQKAQx2kyhrwr4EzOdVgYjwRLDrE5BVCrmAhTRnhsYRrGoFdFzt53NIuQ8Bp8A0XFfB3Tr85x8l35kCPyrPIa5KKVMfclMxJpsmO2XizLOLQwyNp2deaL/If1HOu/5UtPquIZowxe43+rEAAu2kWlV4O4l4mJc29WNLgb/TzAgIY/mugpkUaf0OkQlWpcMq/cTNxbfi3LXEOGJK253ez69udXaiLBRwHKe/cO8H4mXK7P10B8xrK/BqJhFfWXVufSnx40vLTSqsJixhbGSc5Xzc92vOVidVCIvK+se4vKrevliK8lwA6JOeDTFm7b4y+gUE6zUpPGSEisRjKpI2eZdpTDAICqHHQZmRXw6OYz0PUrH7ZAp7Iqsnw6Pyv8/aM3ncjTVDloC8wXxbgJxLZx4j/IvABD5b0RDwzjPQSBK46GnMMthPNVVXJDnPJ1/UuRRt7jjwoNn0PFsS05jfp6iLv1/byv9CN+r9aLbUVAF5uSweC2SH7yDOF2gHRg1pIw8/lrhLWolpOLeMqc3qmGayUuI6KVNeVkKKefvtM0RIKS33CmX3vAtKMEeiLk5F52nsrOZyfImVkFRfIVuSqL5PUTJZTMn80Dw/lc3bG88ef/L4ii7Tp6efzJvTrCkAFhoGDtJm7QbDACXHH1DTu/xnlRJYZkEh3fVHXl1hdule6hOmwMy2", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("oSpLZsv5cKfTiMnUWDqkuEh_zOGM");

Console.ReadLine();