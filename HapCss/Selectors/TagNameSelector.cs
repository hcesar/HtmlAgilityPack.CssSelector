using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class TagNameSelector : CssSelector
{
    public override string Token => string.Empty;

    protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        foreach (HtmlNode node in currentNodes)
        {
            if (node.Name.Equals(Selector, StringComparison.InvariantCultureIgnoreCase))
                yield return node;
        }
    }
}
