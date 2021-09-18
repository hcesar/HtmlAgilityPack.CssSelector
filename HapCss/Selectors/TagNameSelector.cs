using System;
using System.Collections.Generic;

namespace HapCss.Selectors
{
    internal class TagNameSelector : CssSelector
    {
        public override string Token => string.Empty;

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (HtmlAgilityPack.HtmlNode node in currentNodes)
            {
                if (node.Name.Equals(this.Selector, StringComparison.InvariantCultureIgnoreCase))
                    yield return node;
            }
        }
    }
}
