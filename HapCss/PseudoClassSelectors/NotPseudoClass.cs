using HtmlAgilityPack;

namespace HapCss.PseudoClassSelectors;

[PseudoClassName("not")]
internal class NotPseudoClass : PseudoClass
{
    protected override bool CheckNode(HtmlNode node, string parameter)
    {
        IList<CssSelector> selectors = CssSelector.Parse(parameter);
        HtmlNode[] nodes = new[] { node };

        foreach (CssSelector selector in selectors)
            if (selector.FilterCore(nodes).Count() == 1)
                return false;

        return true;
    }
}
