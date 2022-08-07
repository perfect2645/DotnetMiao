using HttpProcessor.Content;
using System.Collections.Generic;
using Utils;

namespace Baohe.search
{
    internal class UserInfoContent : HttpStringContent
    {
        public UserInfoContent(string url) : base(url)
        {
        }
    }
}
