using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Request
{
    public class FreeContentType : MediaTypeHeaderValue
    {
        public FreeContentType(string mediaType) : base(mediaType)
        {
        }
        public override string ToString()
        {
            return base.MediaType;
        }
    }
}
