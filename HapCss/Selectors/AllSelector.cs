using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class AllSelector : CssSelector
{
    public override string Token => "*";

    protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes) =>
        currentNodes;
}
