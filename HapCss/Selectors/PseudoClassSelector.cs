using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapCss.Selectors
{
    internal class PseudoClassSelector : CssSelector
    {
        public override string Token
        {
            get { return ":"; }
        }

        protected internal override IEnumerable<HtmlAgilityPack.HtmlNode> FilterCore(IEnumerable<HtmlAgilityPack.HtmlNode> currentNodes)
        {
            string[] values = this.Selector.TrimEnd(')').Split(new[] { '(' }, 2);

            var pseudoClass = PseudoClass.GetPseudoClass(values[0]);
            string value = values.Length > 1 ? values[1] : null;

            return pseudoClass.Filter(currentNodes, value);
        }
    }
}
