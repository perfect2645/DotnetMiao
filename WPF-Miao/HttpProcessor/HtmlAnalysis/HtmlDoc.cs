using HtmlAgilityPack;
using HttpProcessor.ExceptionManager;
using Utils;

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

        public HtmlNode GetDefaultNode(string xpath)
        {
            return RootNode.SelectNodes(xpath)?.FirstOrDefault();
        }

        public HtmlDoc GetInnerDoc(string xpath)
        {
            var innerNode = RootNode.SelectNodes(xpath)?.FirstOrDefault();
            if (innerNode == null)
            {
                return null;
            }

            var innerHtml = innerNode.InnerHtml;
            var innerDoc = new HtmlDoc(innerHtml);

            return innerDoc;
        }

        #endregion Html Node

        #region Form

        public HtmlNode GetFormNode(string elementId)
        {
            var xpath = XpathTemplate.IdPath(elementId);
            return GetDefaultNode(xpath);
        }

        public string GetFormString(string elementId, bool isEncode = false)
        {
            var formNode = GetFormNode(elementId);
            var formNodeName = formNode.Name;
            string formNodeValue = GetFormValue(formNode, isEncode);
            if (string.IsNullOrEmpty(formNodeName) || string.IsNullOrEmpty(formNodeValue))
            {
                return string.Empty;
            }

            return $"{formNodeName}={formNodeValue}";
        }

        private static string GetFormValue(HtmlNode formNode, bool isEncode = false)
        {
            var formValue = formNode.GetAttributeValue("value", string.Empty);
            if (isEncode)
            {
                formValue = UnicodeConverter.Encode(formValue);
            }
            return formValue;
        }

        #endregion Form
    }
}
