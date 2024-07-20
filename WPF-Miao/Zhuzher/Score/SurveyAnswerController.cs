using HttpProcessor.Client;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Score
{
    internal class SurveyAnswerController : HttpClientBase
    {
        public SurveyAnswerController(HttpClient httpClient) : base(httpClient)
        {
            
        }

        public void SurveyAnswerAsync()
        {
            Task.Factory.StartNew(() =>
            {
                foreach(var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    SurveyAnswer(user);
                }
            });
        }

        public void SurveyAnswer(UserProject user)
        {
            var content = new SurveyAnswerContent(user);
            content.BuildDefaultHeaders(Client);
            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return;
            }
            if (response.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            MainSession.PrintLogEvent.Publish(this, $"{user.UserName} - SurveyAnswer - {msg}");
        }
    }
}
