using System.Text;

namespace HapCss;

public static class Tokenizer
{
    public static IEnumerable<Token> GetTokens(string cssFilter)
    {
        System.IO.StringReader reader = new(cssFilter);
        while (true)
        {
            int v = reader.Read();

            if (v < 0)
                yield break;

            char c = (char)v;

            if (c == '>')
            {
                yield return new Token(">");
                continue;
            }

            if (c is ' ' or '\t')
                continue;

            string word = c + ReadWord(reader);
            yield return new Token(word);
        }
    }

    private static string ReadWord(System.IO.StringReader reader)
    {
        StringBuilder sb = new();
        while (true)
        {
            int v = reader.Read();

            if (v < 0)
                break;

            char c = (char)v;

            if (c is ' ' or '\t')
                break;

            sb.Append(c);
        }

        return sb.ToString();
    }
}
