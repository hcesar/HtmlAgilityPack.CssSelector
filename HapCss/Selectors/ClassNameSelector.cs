using System;
using System.Collections.Generic;
using System.Linq;

namespace HapCss.Selectors
{
    internal class ClassNameSelector : CssSelector
    {
        public override string Token => ".";

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (HtmlAgilityPack.HtmlNode node in currentNodes)
            {
                if (node.GetClassList().Any(c => c.Equals(this.Selector, StringComparison.InvariantCultureIgnoreCase)))
                    yield return node;
            }
        }
    }
}
