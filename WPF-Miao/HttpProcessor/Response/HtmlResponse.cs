using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Response
{
    public class HtmlResponse : HttpResponseBase
    {
        #region Properties

        public HtmlDoc Body { get; private set; }

        #endregion Properties

        #region Constructor

        public HtmlResponse(HttpResponseMessage response)
        {
            try
            {
                BuildHeaders(response.Headers);
                BuildBody(response.Content);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
            }
        }

        #endregion Constructor

        #region Body

        protected override void BuildBody(HttpContent content)
        {
            var contentStr = content.ReadAsStringAsync().Result;

            try
            {
                Body = new HtmlDoc(contentStr);
            }
            catch(Exception ex)
            {
                throw new HttpException(ex, "Build Html body error");
            }
        }

        #endregion Body
    }
}
