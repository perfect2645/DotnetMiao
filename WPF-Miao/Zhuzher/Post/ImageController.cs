using HttpProcessor.Client;
using HttpProcessor.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    internal class ImageController : HttpClientBase
    {
        public ImageController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<byte[]> GetImageAsync(string imgUrl, UserProject user)
        {
            return Task.Factory.StartNew(() =>
            {
                return GetImage(imgUrl, user);
            });

        }

        public byte[] GetImage(string imgUrl, UserProject user)
        {
            try
            {
                var content = new ImageContent(imgUrl);
                content.BuildDefaultHeaders(Client);
                var response = GetImageAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetImage - {response?.Message},请检查参数");
                }
                var imageContent = response?.Body.GetImageContent();
                return imageContent;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetImage失败 - {ex.Message} - {ex.StackTrace}");
                return new byte[0];
            }
        }
    }
}