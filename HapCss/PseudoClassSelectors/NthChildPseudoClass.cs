using System;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("nth-child")]
    internal class NthChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter) =>
            node.GetIndexOnParent() == int.Parse(parameter) - 1;
    }
}