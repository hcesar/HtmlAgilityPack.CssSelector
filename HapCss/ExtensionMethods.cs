using HapCss;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class HapCssExtensionMethods
    {
        public static IList<string> GetClassList(this HtmlAgilityPack.HtmlNode node)
        {
            var attr = node.Attributes["class"];
            if (attr == null)
                return new string[0];
            return attr.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static HtmlAgilityPack.HtmlNode QuerySelector(this HtmlAgilityPack.HtmlDocument doc, string cssSelector)
        {
            return doc.QuerySelectorAll(cssSelector).FirstOrDefault();
        }

        public static HtmlAgilityPack.HtmlNode QuerySelector(this HtmlAgilityPack.HtmlNode node, string cssSelector)
        {
            return node.QuerySelectorAll(cssSelector).FirstOrDefault();
        }

        public static IList<HtmlAgilityPack.HtmlNode> QuerySelectorAll(this HtmlAgilityPack.HtmlDocument doc, string cssSelector)
        {
            return doc.DocumentNode.QuerySelectorAll(cssSelector);
        }

        public static IList<HtmlAgilityPack.HtmlNode> QuerySelectorAll(this HtmlAgilityPack.HtmlNode node, string cssSelector)
        {
            return new[] { node }.QuerySelectorAll(cssSelector);
        }
        public static IList<HtmlAgilityPack.HtmlNode> QuerySelectorAll(this IEnumerable<HtmlAgilityPack.HtmlNode> nodes, string cssSelector)
        {
            if (cssSelector == null)
                throw new ArgumentNullException("cssSelector");

            if (cssSelector.Contains(','))
            {
                var combinedSelectors = cssSelector.Split(',');
                var rt = nodes.QuerySelectorAll(combinedSelectors[0]);
                foreach (var s in combinedSelectors.Skip(1))
                    foreach (var n in nodes.QuerySelectorAll(s))
                        if (!rt.Contains(n))
                            rt.Add(n);

                return rt;
            }

            cssSelector = cssSelector.Trim();

            var selectors = CssSelector.Parse(cssSelector);

            bool allowTraverse = true;

            foreach (var selector in selectors)
            {
                if (allowTraverse && selector.AllowTraverse)
                    nodes = Traverse(nodes);

                nodes = selector.Filter(nodes);
                allowTraverse = selector.AllowTraverse;
            }

            return nodes.Distinct().ToList();
        }


        private static IEnumerable<HtmlAgilityPack.HtmlNode> Traverse(IEnumerable<HtmlAgilityPack.HtmlNode> nodes)
        {
            foreach (var node in nodes)
                foreach (var n in Traverse(node).Where(i => i.NodeType == HtmlNodeType.Element))
                    yield return n;
        }
        private static IEnumerable<HtmlAgilityPack.HtmlNode> Traverse(HtmlAgilityPack.HtmlNode node)
        {
            yield return node;

            foreach (var child in node.ChildNodes)
                foreach (var n in Traverse(child))
                    yield return n;
        }

        public static int GetIndexOnParent(this HtmlNode node)
        {
            int idx = 0;
            foreach (var n in node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlNodeType.Element))
            {
                if (n == node)
                    return idx;
                idx++;
            }

            throw new InvalidOperationException("Node not found in its parent.");
        }
    }
}