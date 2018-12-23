using System;
using HtmlAgilityPack;
using System.Linq;

namespace BoilerPipe
{
    public class BoilerArticle
    {
        public string Text;
        public string Title;
    }
    public static class BoilerPipeMachine
    {
        public static BoilerArticle process(string html)
        {            
            var text=CommonExtractors.ARTICLE_EXTRACTOR.GetText(html);
            var output = new BoilerArticle()
            {
                Text = text
            };
            //now get the title
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            output.Title = htmlDoc.DocumentNode.Descendants("title").FirstOrDefault().InnerText;
            return output;
        }
    }
}

