using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class AllSelector : CssSelector
{
    public override string Token => "*";

    internal protected override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes) =>
        currentNodes;
}
