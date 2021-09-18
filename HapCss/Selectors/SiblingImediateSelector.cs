using System;
using System.Collections.Generic;
using System.Linq;

namespace HapCss.Selectors
{
    internal class SiblingImediateSelector : CssSelector
    {
        public override bool AllowTraverse => false;

        public override string Token => "+";

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (HtmlAgilityPack.HtmlNode node in currentNodes)
            {
                int idx = node.GetIndexOnParent();
                HtmlAgilityPack.HtmlNode n = node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlAgilityPack.HtmlNodeType.Element).Skip(idx + 1).FirstOrDefault();

                if (n != null)
                    yield return n;
            }
        }
    }
}