using System;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("first-child")]
    internal class FirstChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            return node.GetIndexOnParent() == 0;
        }
    }
}