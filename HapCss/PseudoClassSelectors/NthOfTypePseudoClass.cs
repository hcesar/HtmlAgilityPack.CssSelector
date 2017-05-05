using HapCss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAgilityPack.CssSelectors.PseudoClassSelectors
{
    [PseudoClassName("nth-of-type")]
    internal class NthOfTypePseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            try
            {
                return node.ParentNode.GetChildElement(Convert.ToInt32(parameter)) == node;
            }
            catch
            {
                throw new NotSupportedException("Invalid Parameter: " + parameter);
            }
        }
    }
}
