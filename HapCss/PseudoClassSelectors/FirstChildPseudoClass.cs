using HtmlAgilityPack;
using System;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("first-child")]
    internal class FirstChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlNode node, string parameter) =>
            node.GetIndexOnParent() == 0;
    }
}