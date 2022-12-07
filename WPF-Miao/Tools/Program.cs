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
var result  = converter.UrlDecode("", Encoding.UTF8);

var de = converter.UrlDecode(@"name=%E5%88%98%E4%BA%91&memberSn=54193649", Encoding.UTF8);

var en = converter.UrlEncode(de);

var cs7 = new PKCS7();
var cs7result = cs7.DeCodePkcs7("HTx7Z8VfeugcBEpzgw7zD7/5LGgXc5y1U2AL+y0KJXVzbK3JfXk4AthniG3c5ln4ernk9wEPIME/Om99B9kjqkR7CPNofT+cTdnrgBxTz57HOaYsdLS5bHoi41Xda1hMJIslR6IJaSUjDvi2f/JQ0mvxB/RIlZNUjgRV3jQnDxKPqD4z7X0Snp0NmARROvbl+WVXXawAy37zw9yEwjcwj87sXehbiEvsyPb97VJPiVkzN8o/CoM4qThAQl99FCriOxh9029YSZA4P6/GzTPLEIyFr7l+vSRFLazyztfCIdIb+u9crrR1wqe5MYq5WL8CsU7sSQvsqHQmboWtZhkfwHOT0gh0/cAzGlbwvsjN2VgRumrCxJxJn09/3l+Argdag/3aEixT8q4GPN/qq0X1y+u2kPggP8pAnHk/Jv9SItusyIj1iNz0Y/1VxMI8x/ZUIzPxzGzv/CuncNjhZpq5QfNsb1hAMg+PQyoDCzgnT4hvubGujO3ckU0d0avl8svFttJM0j0Vr4AdtM/2Ff2QeY4xvBjc/L6k6qJVB0OChTw6+r6++wb3JqBZRUM4p1Hqal+mhdEj82vx57QeT5Qdu12EgB2NOZCM+Y1Cx9wcM0zaL/fj5BsNO26Tq7gsqzFUxIrTDF7/z8brRnXcC+JWatu0xLI0jsqZsWQ6jmBQnQHEKxCDpQ1IiYa/1JS1c8Fbo4isIV8lpyUlLgzY84TU2MAOk0zNes6pS4jM6zGsuSwow48PPYwdJJWXeZUkJRWwJxgKjU8uScgdnAivI3+gkQInH5BCvS2zMLzLDY3miw1gKzz55MGEh6pBUavDcoy3lPRiru1ywXXoKd/DIyCY8MilVx3puVpPMNT1etwUNlD5mBtP1rfbTJXsTaXG0aWIBhNGpLH37GCqlFdHKClY77wXvYFqCdNJSjG8Sgoj1aDeg2RpfH+MiMKM9NP2O8xwQTPXGiT4Q2QpWWnzKfiRJKnudRhpxa3lzWYnJ1H62tAkDdt0DaF8QRxMRJUSfRYVJycac5N+4iYr7C7rkKbYwMS+VH/TQWu47o5E7N92Utmy+BegUGlFCztyCH5FnSuI5+WVbDCGwUg5V0kFZRxC628n5o9HsbpGcHAP3TxAvRN1YptIduIhe/pf5vInFqIznn7SmXmzodKwKTj8gDK4+J6/aQTPGmAgn7vTOdrKP0QRpwgo7+E43GmhJpx+debeUrVd/F2B6vfs5n//DTvLTXig1WX6/9KOuvx27HlWybkAQQQH0vPotvZb3Ui6meeZyh6QqLdYZ69WG3U4mSWHGDiIsqKs5SFbhDh1g0mqLWVpj122GNl+227abANHYhY5J53m1VqcddUvr5qAWJHRxg==");

Console.ReadLine();