using HtmlAgilityPack;

public class HTMLParser {

    public string Parse(string html) {

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var body = htmlDoc.DocumentNode.SelectSingleNode("html/body");

        return body.InnerHtml;
    }
}
