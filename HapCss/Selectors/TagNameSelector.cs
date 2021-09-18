using System;
using System.Collections.Generic;

namespace HapCss.Selectors
{
    internal class TagNameSelector : CssSelector
    {
        public override string Token
        {
            get { return string.Empty; }
        }

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (var node in currentNodes)
            {
                if (node.Name.Equals(this.Selector, StringComparison.InvariantCultureIgnoreCase))
                    yield return node;
            }
        }
    }
}
