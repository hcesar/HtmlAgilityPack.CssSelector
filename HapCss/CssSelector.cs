using HtmlAgilityPack;

namespace HapCss;

public abstract class CssSelector
{
    public CssSelector()
    {
        SubSelectors = new List<CssSelector>();
    }

    private static readonly CssSelector[] s_Selectors = FindSelectors();
    public abstract string Token { get; }
    protected virtual bool IsSubSelector => false;
    public virtual bool AllowTraverse => true;

    public IList<CssSelector> SubSelectors { get; set; }
    public string Selector { get; set; }

    protected internal abstract IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes);

    public IEnumerable<HtmlNode> Filter(IEnumerable<HtmlNode> currentNodes)
    {
        IEnumerable<HtmlNode> nodes = currentNodes;
        IEnumerable<HtmlNode> rt = FilterCore(nodes).Distinct();

        if (SubSelectors.Count == 0)
            return rt;

        foreach (CssSelector selector in SubSelectors)
            rt = selector.FilterCore(rt);

        return rt;
    }

    public virtual string GetSelectorParameter(string selector) =>
        selector.Substring(Token.Length);

    public static IList<CssSelector> Parse(string cssSelector)
    {
        List<CssSelector> rt = new();
        IEnumerable<Token> tokens = Tokenizer.GetTokens(cssSelector);
        foreach (Token token in tokens)
            rt.Add(ParseSelector(token));

        return rt;
    }

    private static CssSelector ParseSelector(Token token)
    {
        Type selectorType;
        CssSelector selector;

        if (char.IsLetter(token.Filter[0]))
            selector = s_Selectors.First(i => i is Selectors.TagNameSelector);
        else
            selector = s_Selectors.Where(s => s.Token.Length > 0).FirstOrDefault(s => token.Filter.StartsWith(s.Token));

        if (selector == null)
            throw new InvalidOperationException("Token inválido: " + token.Filter);

        selectorType = selector.GetType();
        CssSelector rt = (CssSelector)Activator.CreateInstance(selectorType);

        string filter = token.Filter.Substring(selector.Token.Length);
        rt.SubSelectors = token.SubTokens.Select(i => CssSelector.ParseSelector(i)).ToList();

        rt.Selector = filter;
        return rt;
    }

    private static CssSelector[] FindSelectors()
    {
        System.Reflection.Assembly defaultAsm = typeof(CssSelector).Assembly;
        Func<Type, bool> typeQuery = type => type.IsSubclassOf(typeof(CssSelector)) && !type.IsAbstract;

        IEnumerable<Type> defaultTypes = defaultAsm.GetTypes().Where(typeQuery);
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm == defaultAsm).SelectMany(asm => asm.GetTypes().Where(typeQuery));
        types = defaultTypes.Concat(types);

        CssSelector[] rt = types.Select(t => Activator.CreateInstance(t)).Cast<CssSelector>().ToArray();
        return rt;
    }
}
