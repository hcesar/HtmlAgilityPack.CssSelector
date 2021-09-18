using System.Collections.Generic;
using System.Linq;

namespace HapCss.Selectors
{
    internal class ImediateChildrenSelector : CssSelector
    {
        public override bool AllowTraverse => false;

        public override string Token => ">";

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            return currentNodes.SelectMany(i => i.ChildNodes);
        }
    }
}