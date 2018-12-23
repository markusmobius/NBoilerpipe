using HtmlAgilityPack;

namespace BoilerPipe
{
    public interface IContentHandler
    {
        void StartElement(HtmlNode node);
        void EndElement(HtmlNode node);
        void HandleText(HtmlTextNode node);
    }
}
