using HttpProcessor.Client;
using Jkchegu.session;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Jkchegu.search.yzm
{
    internal class YzmController : HttpClientBase
    {
        public YzmController(HttpClient httpClient) : base(httpClient)
        {
            //只需要调用一次初始化
            string rpath = System.IO.Directory.GetCurrentDirectory() + "\\";
            int ret = INIT(rpath.ToCharArray(), -1, 1);
            if (ret != 1)
            {
                JkSession.PrintLogEvent.Publish(this, $"图形识别初始化失败");
                //MessageBox.Show("启动失败");
            }
        }

        public async Task GetYzmAsync()
        {
            await Task.Factory.StartNew(() => GetYzm());
        }

        public void GetYzm()
        {
            var timeStamp = DateTimeUtil.GetTimeStamp();
            var url = $"http://app.whkfqws.com/wx-mobile/Vaccination/code.do?t={timeStamp}";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);

            content.BuildDefaultHeaders(Client);

            var byteYzm = SearchByteAsync(content.RequestUrl).Result;
            if (byteYzm == null)
            {
                JkSession.PrintLogEvent.Publish(this, $"GetYzm 失败");
            }

            var yzmResult = GetCodeFromOcr(byteYzm!);
            if (string.IsNullOrEmpty(yzmResult))
            {
                JkSession.PrintLogEvent.Publish(this, $"获取验证码 失败");
                return;
            }
            JkSession.PrintLogEvent.Publish(this, $"获取验证码 成功 - {yzmResult}");
            JkSession.MiaoSession.AddOrUpdate("Yzm", yzmResult);
        }

        // [StructLayout(LayoutKind.Sequential)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2000)]
        public char[] ch;

        #region 加载 识别引擎
        [DllImport("OCR.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int OCR(byte[] 图片指针, int 图片长度, IntPtr 返回指针);//信鸽新调用方式
        [DllImport("OCR.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int INIT(char[] 运行目录, int GPUID, int RPC);//INIT[int] (0=失败,1=成功)
        public string GetCodeFromOcr(byte[] data)
        {
            try
            {
                string msg = null;
                int bufferlen = 1024;
                IntPtr buffer = Marshal.AllocHGlobal(bufferlen);
                //ch = new char[1000];
                int Flag = OCR(data, data.Length, buffer);
                //MessageBox.Show(Flag.ToString());
                msg = Marshal.PtrToStringAnsi(buffer, Flag);
                // msg = buffer.ToString();
                // msg = Marshal.ReadByte(buffer);
                //byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringUni(buffer));//转成UNICODE编码
                //msg = System.Text.Encoding.UTF8.GetString(bytes);//再转成UTF8
                Marshal.FreeHGlobal(buffer);
                //MessageBox.Show("识别结果：" + msg.ToString());
                return msg;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("出错啦！" + ex.ToString());
                return string.Empty;
            }
        }
        #endregion
    }
}
