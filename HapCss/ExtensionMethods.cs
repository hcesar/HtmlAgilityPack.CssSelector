using HapCss;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static partial class HapCssExtensionMethods
    {
        public static HtmlNode QuerySelector(this HtmlDocument doc, string cssSelector) =>
            doc.QuerySelectorAll(cssSelector).FirstOrDefault();

        public static HtmlNode QuerySelector(this HtmlNode node, string cssSelector) =>
            node.QuerySelectorAll(cssSelector).FirstOrDefault();

        public static IList<HtmlNode> QuerySelectorAll(this HtmlDocument doc, string cssSelector) =>
            doc.DocumentNode.QuerySelectorAll(cssSelector);

        public static IList<HtmlNode> QuerySelectorAll(this HtmlNode node, string cssSelector) =>
            new[] { node }.QuerySelectorAll(cssSelector);
        
        public static IList<HtmlNode> QuerySelectorAll(this IEnumerable<HtmlNode> nodes, string cssSelector)
        {
            if (cssSelector == null)
                throw new ArgumentNullException("cssSelector");

            if (cssSelector.Contains(','))
            {
                string[] combinedSelectors = cssSelector.Split(',');
                IList<HtmlNode> rt = nodes.QuerySelectorAll(combinedSelectors[0]);
                foreach (string s in combinedSelectors.Skip(1))
                    foreach (HtmlNode n in nodes.QuerySelectorAll(s))
                        if (!rt.Contains(n))
                            rt.Add(n);

                return rt;
            }

            cssSelector = cssSelector.Trim();

            IList<CssSelector> selectors = CssSelector.Parse(cssSelector);

            bool allowTraverse = true;

            foreach (CssSelector selector in selectors)
            {
                if (allowTraverse && selector.AllowTraverse)
                    nodes = Traverse(nodes);

                nodes = selector.Filter(nodes);
                allowTraverse = selector.AllowTraverse;
            }

            return nodes.Distinct().ToList();
        }

        private static IEnumerable<HtmlNode> Traverse(IEnumerable<HtmlNode> nodes)
        {
            foreach (HtmlNode node in nodes)
                foreach (HtmlNode n in Traverse(node).Where(i => i.NodeType == HtmlNodeType.Element))
                    yield return n;
        }

        private static IEnumerable<HtmlNode> Traverse(HtmlNode node)
        {
            yield return node;

            foreach (HtmlNode child in node.ChildNodes)
                foreach (HtmlNode n in Traverse(child))
                    yield return n;
        }
    }
}