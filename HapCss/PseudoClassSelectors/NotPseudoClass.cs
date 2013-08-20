using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.PseudoClassSelectors
{
    [PseudoClassName("not")]
    internal class NotPseudoClass : PseudoClass
    {
        protected override bool CheckNode(HtmlAgilityPack.HtmlNode node, string parameter)
        {
            var selectors = CssSelector.Parse(parameter);
            var nodes = new[] { node };

            foreach (var selector in selectors)
                if (selector.FilterCore(nodes).Count() == 1)
                    return false;

            return true;
        }
    }
}