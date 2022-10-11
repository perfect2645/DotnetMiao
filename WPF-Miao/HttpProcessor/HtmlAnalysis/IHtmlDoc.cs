using HtmlAgilityPack;

namespace HttpProcessor.HtmlAnalysis
{
    public interface IHtmlDoc
    {
        string Html { get; }

        HtmlDocument HtmlDocument { get; }

        HtmlNodeCollection SearchNodes(string xpath);
    }
}
