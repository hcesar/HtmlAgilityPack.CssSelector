namespace HapCss;

public class Token
{
    public string Filter { get; set; }
    public IList<Token> SubTokens { get; set; }

    public Token(string word)
    {
        if (string.IsNullOrEmpty(word))
            throw new ArgumentNullException(nameof(word));

        List<string> tokens = SplitTokens(word).ToList();

        Filter = tokens.First();
        SubTokens = tokens.Skip(1).Select(i => new Token(i)).ToList();
    }

    private static IList<string> SplitTokens(string token)
    {
        static bool isNameToken(char c) => char.IsLetterOrDigit(c) || c == '-' || c == '_';
        List<string> rt = new();

        int start = 0;
        bool isPrefix = true;
        bool isOpeningBracket = false;
        char closeBracket = '\0';
        for (int i = 0; i < token.Length; i++)
        {
            if (isOpeningBracket && token[i] != closeBracket)
                continue;

            isOpeningBracket = false;

            if (token[i] == '(')
            {
                closeBracket = ')';
                isOpeningBracket = true;
            }
            else if (token[i] == '[')
            {
                closeBracket = ']';
                if (i != start)
                {
                    rt.Add(token[start..i]);
                    start = i;
                }
                isOpeningBracket = true;
            }
            else if (i == token.Length - 1)
            {
                rt.Add(token.Substring(start, i - start + 1));
            }
            else if (!isNameToken(token[i]) && !isPrefix)
            {
                rt.Add(token[start..i]);
                start = i;
            }
            else if (isNameToken(token[i]))
                isPrefix = false;
        }

        return rt;
    }
}
