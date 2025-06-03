using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

    enum Wrap {Normal, Formatted };

    public static class StringExtensions
    {

        public static string RemoveNewLineEndings(this string value) => value.Replace(Environment.NewLine, string.Empty);

        public static string MaxLength(this string value, int maxLength) => value.Substring(0, Math.Min(value.Length, maxLength));    

        public static string WordWrap(this string source, int maxParagraphLength)
        {
            StringBuilder formattedText = SplitIntoParagraphs(source, maxParagraphLength , Wrap.Normal, out var unused);

            return formattedText.ToString();
        }

        public static string WordWrapWithList(this string source, int maxParagraphLength, out List<string> formatAsList)
        {
            StringBuilder formattedText = SplitIntoParagraphs(source, maxParagraphLength, Wrap.Normal, out var textAsList);
        
            formatAsList = textAsList;
            return formattedText.ToString();
        }

        /// <summary>
        /// Returns a string with maximum line length equal to MaxParagraphLength, wrapping at full words if possible. Provide a string formatting to be applied to each line with an optional empty formatted line at end.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="maxParagraphLength"></param>
        /// <param name="format"></param>
        /// <param name="lineBreakAtEnd"></param>
        /// <returns></returns>
        public static string WordWrapFormatted(this string source, int maxParagraphLength, string format, bool lineBreakAtEnd = false)
        {

            StringBuilder formattedText = SplitIntoParagraphs(source, maxParagraphLength, Wrap.Formatted, out var unused, format);

            return lineBreakAtEnd
                ? formattedText.AppendFormat(format, "").ToString()
                : formattedText.ToString();            
        }

        private static StringBuilder SplitIntoParagraphs(string source, int maxParagraphLength, Wrap wrap, out List<string> textAsList, string format = "")
        {
            StringBuilder formattedText = new StringBuilder();
            List<string> formattedList = new List<string>();

            for (int i = 0; i < source.Length; i += maxParagraphLength)
            {
                var paragraphLength = Math.Min(source.Length - i, maxParagraphLength);
                string split = source.Substring(i, paragraphLength);
                StringBuilder result = new StringBuilder();


                // If paragraph is the last one or paragraph ends with whitespace
                if (i + paragraphLength == source.Length || split.LastIndexOf(" ") == paragraphLength)
                    {
                    
                        if(wrap == Wrap.Formatted)
                            result.AppendFormat(format, split.Trim());

                        else 
                        { 
                            result.Append(split.Trim());
                        }
                    }

                    // Wrap last word to next paragraph
                    else
                    {
                        var removalResult = AmountToRemove(split, paragraphLength);
                        var wrapped = source.Substring(i, paragraphLength - removalResult.amount);

                        if (wrap == Wrap.Formatted)
                        result.AppendFormat(format, wrapped.Trim());
                    
                        else result.Append(wrapped.Trim());

                        // how does this work?
                        // It decrements 'i' by as many characters as were removed during formatting, or none if 'i' is 1.
                        // 'i' represents how many characters there are in the string.
                        // also decrement by an additional index if any characters have been removed to compansate for any special characters

                        int decrementByString = paragraphLength - wrapped.Length;

                        int decrementIfSpace = (removalResult.specialChar || removalResult.amount == 0) ? 0 : 1;

                        i = Math.Max(
                            - (maxParagraphLength - 1),                   // decrease by at minumum 1 before looping
                            i - (decrementByString - decrementIfSpace)); // decrement by number of chars removed, not carrying over spaces to next loop

                    }
                
                    formattedList.Add(result.ToString());
                    formattedText.Append(result);
                    formattedText.AppendLine();
            }
            textAsList = formattedList;
            return formattedText;
        }

        private static (int amount, bool specialChar) AmountToRemove(string input, int paragraphLength) 
        {
            var lastSpace = input.LastIndexOf(' ');
            var lastHyphen = input.LastIndexOf('-');
            
            // There are no spaces in the string,
            // or the space is at the beginning of the string.

            if (lastSpace < 1 && lastHyphen < 1) 
            {
                return (0, false);
            }

            if (lastSpace >= 1)
            {
                return (paragraphLength - lastSpace, false);
            }

            return (paragraphLength - (lastHyphen + 1), true); // keep hyphen when removing other letters
  


        }
    }

