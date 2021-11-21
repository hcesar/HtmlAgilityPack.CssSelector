using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class IdSelector : CssSelector
{
    public override string Token => "#";

    internal protected override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        foreach (HtmlNode node in currentNodes)
        {
            if (node.Id.Equals(Selector, StringComparison.InvariantCultureIgnoreCase))
                return new[] { node };
        }

        return Array.Empty<HtmlNode>();
    }
}
