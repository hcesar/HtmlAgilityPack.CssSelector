using HtmlAgilityPack;

namespace HapCss.PseudoClassSelectors;

[PseudoClassName("nth-child")]
internal class NthChildPseudoClass : PseudoClass
{
    protected override bool CheckNode(HtmlNode node, string parameter) =>
        node.GetIndexOnParent() == int.Parse(parameter) - 1;
}
