using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.PseudoClassSelectors
{
    [FunctionName("first-child")]
    internal class FirstChildPseudoClass : PseudoClass
    {
        public override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            return node.GetIndexOnParent() == 0;
        }
    }
}