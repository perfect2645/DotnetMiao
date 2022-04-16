using System.Net;

namespace HttpProcessor
{
    public class Demo
    {
		public async Task<string> HttpGet(string Url, string host, WebProxy proxy1 = null, bool isSearchMiao = false, string ztsf = "")
		{
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
				request.Method = "GET";
				request.ContentType = "application/json";
				request.Host = host;
				request.KeepAlive = false;
				if (isSearchMiao)
				{
					request.Headers.Add("Cookie", "");
					request.Headers.Add("zftsl", "");
					this.WriteLog("SearchMiao");
				}
				else
				{
					request.Headers.Add("Cookie", this.CookieZ);
					//var zftsl = lllaaaaYY("http://localhost:8095/", "localhost");
					string text4 = ((TimerHelper.GetTimeStampFF(DateTime.Now) + LastTime) / 1000L).ToString("000000000");
					request.Headers.Add("zftsl", JsonHelper.GenerateMD5("zfsw_" + text4));
					//request.Headers.Add("zftsl", zftsl);
				}

				request.UserAgent = Consts.UserAgent;
				//request.AllowAutoRedirect = true;
				if (proxy1 != null)
				{
					this.WriteLog("u p");
					request.Proxy = proxy1;
				}

				request.Referer = Consts.Referer;
				request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
				//request.Timeout = 500000;
				//request.ServicePoint.ConnectionLimit = 10240;
				Stopwatch stopWatch = new Stopwatch();
				stopWatch.Start();
				HttpWebResponse response = (await request.GetResponseAsync()) as HttpWebResponse;
				stopWatch.Stop();

				//var localTime = DateTime.Now;
				//var serverTime = CommonHelp.GMT2Local(response.Headers["date"]);
				//var dd = ( localTime - serverTime ).TotalMilliseconds;

				//           if (Url.Contains("act=CustomerProduct&id=") || Url.Contains("act=GetCustSubscribeDateAll&pid="))
				//           {
				//               if (stopWatch.ElapsedMilliseconds >= 450 )
				//               {
				//	refreshSleep = 100;
				//	this.WriteLog("设置Sle 100");
				//}
				//           }

				WriteLog("请求耗时:" + stopWatch.ElapsedMilliseconds);
				//WriteLog("Diff:" + dd);
				//LastTime = TimerHelper.GetTimeStampFF(DateTime.Parse(response.Headers["date"])) - TimerHelper.GetTimeStampFF(DateTime.Now);
				Stream myResponseStream = response.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
				string retString = myStreamReader.ReadToEnd();
				if ((Url.Contains("GetCustSubscribeDateDetail")))
				{
					if (!retString.Contains("status"))
					{
						retString = DecdRes(retString);
					}
				}
				else if ((Url.Contains("CaptchaVerify") && !retString.Contains("重新请求")))
				{
					retString = DecdRes(retString);
				}
				else if (Url.Contains("GetCaptcha"))
				{
					var returnCook = response.Headers["Set-Cookie"];
					if (!string.IsNullOrEmpty(returnCook))
					{
						this.CookieZ = returnCook;
					}
				}

				LastTime = TimerHelper.GetTimeStampFF(DateTime.Parse(response.Headers["date"])) - TimerHelper.GetTimeStampFF(DateTime.Now);
				WriteLog(response.StatusCode.ToString() + ":" + response.StatusDescription);
				myStreamReader.Close();
				myResponseStream.Close();

				return retString;
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				WriteLog("Get请求异常: " + ex.Message);
				WriteLog("URL: " + Url);
				return "发送请求异常";
			}
		}

	}
}