using HtmlAgilityPack;
using System.Collections.Generic;

namespace HapCss.Selectors
{
    internal class PseudoClassSelector : CssSelector
    {
        public override string Token => ":";

        protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
        {
            string[] values = this.Selector.TrimEnd(')').Split(new[] { '(' }, 2);

            PseudoClass pseudoClass = PseudoClass.GetPseudoClass(values[0]);
            string value = values.Length > 1 ? values[1] : null;

            return pseudoClass.Filter(currentNodes, value);
        }
    }
}
