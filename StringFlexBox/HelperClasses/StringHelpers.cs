using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


public static class StringHelpers
{
    public static string CreatePadding(int amount) => 
        new string(' ', amount);

    public static string GetStringAtLine(string text, int line, int textWidth) =>
        text.Substring(line * textWidth, Math.Min((text.Length - line * textWidth), textWidth));

    public static string Fill(int amount) =>
        new string(' ', amount);

    private static string AddSpacesToCamelCase(this string text) =>
       Regex.Replace(text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

}

