using System;
using System.Collections.Generic;
using System.Linq;

namespace HapCss.Selectors
{
    internal class SiblingSelector : CssSelector
    {
        public override bool AllowTraverse => false;

        public override string Token => "~";

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (HtmlAgilityPack.HtmlNode node in currentNodes)
            {
                int idx = node.GetIndexOnParent();
                foreach (HtmlAgilityPack.HtmlNode n in node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlAgilityPack.HtmlNodeType.Element).Skip(idx + 1))
                    yield return n;
            }
        }
    }
}