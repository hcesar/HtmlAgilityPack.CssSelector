using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.Selectors
{
    internal class SiblingSelector : CssSelector
    {
        public override bool AllowTraverse
        {
            get { return false; }
        }

        public override string Token
        {
            get { return "~"; }
        }

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            foreach (var node in currentNodes)
            {
                var idx = node.GetIndexOnParent();
                foreach (var n in node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlAgilityPack.HtmlNodeType.Element).Skip(idx + 1))
                    yield return n;
            }
        }
    }
}