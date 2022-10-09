using HtmlAgilityPack;
using HttpProcessor.ExceptionManager;

namespace HttpProcessor.HtmlAnalysis
{
    public class HtmlDoc: IHtmlDoc
    {
        #region Properties

        public string Html { get; private set; }

        public HtmlDocument HtmlDocument { get; private set; }

        public HtmlNode RootNode
        { 
            get { return HtmlDocument?.DocumentNode; }
        }

        #endregion Properties

        #region Constructor

        public HtmlDoc(string html)
        {
            Html = html;
            BuildHtmlDoc();
        }
        private void BuildHtmlDoc()
        {
            if (string.IsNullOrEmpty(Html))
            {
                throw new HttpException("Build HtmlDoc failed, Html is Empty");
            }

            HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(Html);
        }

        #endregion Constructor

        #region Html Node

        public HtmlNodeCollection SearchNodes(string xpath)
        {
            return RootNode.SelectNodes(xpath);
        }

        #endregion Html Node
    }
}
