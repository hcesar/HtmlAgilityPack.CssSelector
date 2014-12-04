HtmlAgilityPack CSS Selector
=======

HAP-CSS is a .NET Extension Method for HtmlAgilityPack HtmlDocument and HtmlNode classes.
It is a handy tool for Web scrapers, and a good alternative to HAP XPath queries.

Usage:

    var doc = new HtmlAgilityPack.HtmlDocument();
    doc.Load("test.html");
  
    IList<HtmlNode> nodes = doc.QuerySelectorAll("div .my-class[data-attr=123] > ul li");
    HtmlNode node = nodes.QuerySelector("p.with-this-class span[data-myattr]");

