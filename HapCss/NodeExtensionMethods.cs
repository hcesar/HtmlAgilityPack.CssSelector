using HapCss;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static partial class HapCssExtensionMethods
    {
        public static IEnumerable<HtmlAgilityPack.HtmlNode> GetChildElements(this HtmlAgilityPack.HtmlNode node)
        {
            return node.ChildNodes.Where(i => i.NodeType == HtmlNodeType.Element);
        }

        public static IList<string> GetClassList(this HtmlAgilityPack.HtmlNode node)
        {
            var attr = node.Attributes["class"];
            if (attr == null)
                return new string[0];
            return attr.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int GetIndexOnParent(this HtmlNode node)
        {
            return node.GetChildElements().TakeWhile(n => n != node).Count();
        }

        public static HtmlNode NextSiblingElement(this HtmlNode node)
        {
            var rt = node.NextSibling;

            while (rt != null && rt.NodeType != HtmlNodeType.Element)
                rt = rt.NextSibling;

            return rt;
        }

        public static HtmlNode PreviousSiblingElement(this HtmlNode node)
        {
            var rt = node.PreviousSibling;

            while (rt != null && rt.NodeType != HtmlNodeType.Element)
                rt = rt.PreviousSibling;

            return rt;
        }
    }
}