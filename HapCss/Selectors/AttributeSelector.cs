using System.Globalization;
using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class AttributeSelector : CssSelector
{
    public override string Token => "[";

    internal protected override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        Func<HtmlNode, bool> filter = GetFilter();
        foreach (HtmlNode node in currentNodes)
        {
            if (filter(node))
                yield return node;
        }
    }

    private Func<HtmlNode, bool> GetFilter()
    {
        string filter = Selector.Trim('[', ']');

        int idx = filter.IndexOf('=');

        if (idx == 0)
            throw new InvalidOperationException("Uso inválido de seletor por atributo: " + Selector);

        if (idx < 0)
            return (HtmlNode node) => node.Attributes.Contains(filter);

        Func<string, string, bool> operation = GetOperation(filter[idx - 1]);

        if (!char.IsLetterOrDigit(filter[idx - 1]))
            filter = filter.Remove(idx - 1, 1);

        string[] values = filter.Split(new[] { '=' }, 2);
        filter = values[0];
        string value = values[1];

        if (value[0] == value[^1] && (value[0] == '"' || value[0] == '\''))
            value = value[1..^1];

        return (HtmlNode node) => node.Attributes.Contains(filter) && operation(node.Attributes[filter].Value, value);
    }

    private Func<string, string, bool> GetOperation(char value)
    {
        if (char.IsLetterOrDigit(value))
            return (attr, v) => attr == v;

        return value switch
        {
            '*' => (attr, v) => attr == v || attr.Contains(v),
            '^' => (attr, v) => attr.StartsWith(v),
            '$' => (attr, v) => attr.EndsWith(v),
            '~' => (attr, v) => attr.Split(' ').Contains(v),
            _ => throw new NotSupportedException("Uso inválido de seletor por atributo: " + Selector),
        };
    }
}
