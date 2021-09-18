using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace HapCss.Selectors
{
    internal class ImediateChildrenSelector : CssSelector
    {
        public override bool AllowTraverse => false;

        public override string Token => ">";

        protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes) =>
            currentNodes.SelectMany(i => i.ChildNodes);
    }
}