using Base.search;
using System.Net.Http;

namespace Jk160.search
{
    internal class Jk160Search : SearchController
    {
        public Jk160Search(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
