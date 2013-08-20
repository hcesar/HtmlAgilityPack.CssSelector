using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("last-child")]
    internal class LastChildPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            return node.ParentNode.ChildNodes.Reverse().First(i => i.NodeType == HtmlNodeType.Element) == node;
        }
    }
}