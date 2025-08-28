using System.Text.RegularExpressions;

public static class StringHelpers
{
    public static string CreatePadding(int amount)
    {
        return new string(' ', amount);
    }

    public static string GetStringAtLine(string text, int line, int textWidth)
    {
        return text.Substring(line * textWidth, Math.Min(text.Length - line * textWidth, textWidth));
    }

    public static string Fill(int amount)
    {
        return new string(' ', amount);
    }

    private static string AddSpacesToCamelCase(this string text)
    {
        return Regex.Replace(text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
    }
}