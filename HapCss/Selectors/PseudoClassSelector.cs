using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class PseudoClassSelector : CssSelector
{
    public override string Token => ":";

    internal protected override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        string[] values = Selector.TrimEnd(')').Split(new[] { '(' }, 2);

        PseudoClass pseudoClass = PseudoClass.GetPseudoClass(values[0]);
        string value = values.Length > 1 ? values[1] : null;

        return pseudoClass.Filter(currentNodes, value);
    }
}
