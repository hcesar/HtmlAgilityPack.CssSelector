using System;
using System.Linq;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("last-child")]
    internal class LastChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            return node.ParentNode.GetChildElements().Last() == node;
        }
    }
}