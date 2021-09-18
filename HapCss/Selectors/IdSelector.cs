using System;
using System.Collections.Generic;

namespace HapCss.Selectors
{
    internal class IdSelector : CssSelector
    {
        public override string Token
        {
            get { return "#"; }
        }

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (var node in currentNodes)
            {
                if (node.Id.Equals(this.Selector, StringComparison.InvariantCultureIgnoreCase))
                    return new[] { node };
            }

            return new HtmlAgilityPack.HtmlNode[0];
        }
    }
}
