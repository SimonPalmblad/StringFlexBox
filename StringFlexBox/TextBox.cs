using System.Text;

namespace StringFlexBox
{
    public class TextBox : FlexBox
    {
        public static int MinWidth { get; } = 10;
        private int m_TextWidth;

        public override string FormattedText
        {
            get
            {
                if (m_IsDirty)
                {
                    BuildTexts();
                }

                return m_FormattedText;
            }
        }

        public override string TextWithoutEscapeChars
        {
            get
            {
                if (m_IsDirty)
                {
                    BuildTexts();
                }
                return m_TextWithoutEscapeChars;
            }
        }

        public static TextBox Empty =>
            new TextBox(string.Empty, 0, new Padding(0), FlexBoxBorder.Default);

        public TextBox(string text, int textWidth, int padding)
            : this(text, textWidth, new Padding(padding), FlexBoxBorder.Default)
        {

        }

        public TextBox(string text, int textWidth, Padding padding)
            : this(text, textWidth, padding, FlexBoxBorder.Default)
        {

        }

        public TextBox(string text, int textWidth, int padding, FlexBoxBorder borderStyle)
            : this(text, textWidth, new Padding(padding), borderStyle)
        {

        }

        public TextBox(string text, int textWidth, Padding padding, FlexBoxBorder borderStyle)
        {
            InitializeTextBox(text, textWidth, padding, borderStyle);
        }

        private void InitializeTextBox(string text, int textWidth, Padding padding, FlexBoxBorder borderStyle, int wrapAllowance = 0)
        {
            m_Content.Clear();
            Padding = padding;
            BorderStyle = borderStyle;

            if (this.m_TextWidth != 0)
            {
                m_PreviousTextSize = this.m_TextWidth;
            }

            else
            {
                m_PreviousTextSize = textWidth;
            }

            this.m_TextWidth = textWidth;

            m_Sources = new List<FlexBox>() { this };

            // StringBuilder builder = new StringBuilder();
            // create a new word-wrapped string with an allowance for adding characters later on, formatted in a list of paragraphs
            text.WordWrapWithList(textWidth - wrapAllowance, out var formatAsList);
            m_Texts = formatAsList;

            // int lines = (text.Length / SetBoxWidth(BoxWidth()));
            BuildTexts();

            m_Height = m_Content.Count;
        }

        protected override string TopLine() =>
            $"{BorderStyle.TopLeftCorner}" +
            $"{new string(BorderStyle.TopBorder, VerticalPaddingAmount)}" +
            $"{BorderStyle.TopRightCorner}";

        protected override string BottomLine() =>
            $"{BorderStyle.BottomLeftCorner}" +
            $"{new string(BorderStyle.BottomBorder, VerticalPaddingAmount)}" +
            $"{BorderStyle.BottomRightCorner}";

        protected override void AppendTopPadding(StringBuilder builder)
        {
            var topPaddingContent = VerticalPadding(Padding.Side.Top);
            builder.Append(topPaddingContent);
        }

        protected override void AppendBottomPadding(StringBuilder builder)
        {
            var botPaddingContent = VerticalPadding(Padding.Side.Bottom);
            m_PaddingHeightOffset += VerticalPaddingConversion(Padding.GetSide(Padding.Side.Bottom));
            builder.Append(botPaddingContent);
        }

        protected override void AppendContent(StringBuilder builder)
        {
            for (int i = 0; i < m_Texts.Count(); i++)
            {
                var newLineRemoved = m_Texts[i].Replace(Environment.NewLine, string.Empty);

                StringBuilder result = new StringBuilder();
                AppendLeftPadding(result);
                result.Append(newLineRemoved);
                AppendTextLineEnd(result, i, SetBoxWidth(VerticalPaddingAmount));
                m_Content.Add(result.ToString());

                builder.Append(result);
                
                // if(DrawBorder)
                    builder.AppendLine();
            }
        }

        protected void AppendTextLineEnd(StringBuilder builder, int lineIndex, int width)
        {
            // subtract already added text on left side padding.
            var lineFill = Math.Max(width - (m_Texts[lineIndex].Count() + Padding.GetSide(Padding.Side.Left)), 0);

            var result = StringHelpers.Fill(lineFill) + BorderStyle.RightBorder;
            builder.Append(result);
        }

        protected override string VerticalPadding(Padding.Side padding)
        {
            StringBuilder builder = new StringBuilder();
            var width = VerticalPaddingAmount;
            var convertedPadding = VerticalPaddingConversion(Padding.GetSide(padding));

            for (int i = 0; i < convertedPadding; i++)
            {
                var paddingString =
                    $"{BorderStyle.LeftBorder}" +
                    $"{StringHelpers.Fill(width)}" +
                    $"{BorderStyle.RightBorder}";

                m_Content.Add(paddingString);
                builder.AppendLine(paddingString);
            }

            return builder.ToString();
        }

        public override int BoxWidth() =>
            m_TextWidth + HorizontalPadding() + m_PaddingWidthOffset;

        public override int BoxHeight() =>
            m_Content.Count();

        public int TextWidth => m_TextWidth;

        protected override int VerticalPaddingAmount => m_TextWidth + HorizontalPadding();

        public static int SetBoxWidth(int _width) => Math.Max(MinWidth, _width);

        public void Resize(int newWidth, int wrapAllowance  = 0)
        {
            if(newWidth == m_TextWidth)
                return;

            string newSourceText = string.Empty;

            for (int i = 0; i < m_Texts.Count; i++)
            {
                newSourceText += m_Texts[i] + " "; //adding back a space removed in the word wrapping
            }

            InitializeTextBox(newSourceText, newWidth, Padding, BorderStyle, wrapAllowance);
        }

        public void ReplaceText(string text)
        {
            InitializeTextBox(text, m_TextWidth, Padding, BorderStyle);
        } 
        
        private void AddContentToText(string content, Action<string> contentAction)
        {
            var length = content.Length;

            if (TextWidth < length)
            {
                return;
            }

            Resize(m_PreviousTextSize + content.Length, content.Length);
            contentAction(content);
        
            m_IsDirty = true;

        }

        private void AddContentAtLine(string content, int line, Action<string, int> contentAction)
        {
            var length = content.Length;

            // Only perform content action if sources are textboxes.
            foreach (FlexBox item in m_Sources)
            {
           
                if (m_PreviousTextSize < length || GetTextAtLine(line) == string.Empty)
                {
                    continue;
                }

                Resize(m_PreviousTextSize + content.Length, content.Length);

                contentAction(content, line);
            }

            m_IsDirty = true;

        }


        /// <summary>
        /// Adds a string before all lines inside this object's text.
        /// </summary>
        /// <param name="text">The string to add as a prefix</param>
        public void AddLinePrefix(string text)
        {
            AddContentToText(text, AddLinePrefixAction);
        }

        public void AddPrefixAtLine(string text, int line)
        {
            AddContentAtLine(text, line, AddPrefixAtLineAction);
        }

        // WARNING - Looping this keeps increasing the size. Safeguard against resizing if the text is already the same size.
        private void AddPrefixAtLineAction(string text, int line)
        {
            var textAtLine = TextAtLine(line);
            textAtLine = text + textAtLine;

            m_Texts[line] = textAtLine;
        }

        private void AddLinePrefixAction(string text)
        {
            Resize(TextWidth + text.Length, text.Length);

            for (int i = 0; i < m_Texts.Count; i++)
            {
                m_Texts[i] = text + m_Texts[i];
            }

        }


        /// <summary>
        /// Adds a string at the end of all lines inside this object's text.
        /// </summary>
        /// <param name="suffix">The suffix string.</param>
        public void AddLineSuffix(string suffix)
        {
            AddContentToText(suffix, AddLineSuffixAction);
        }

        private void AddLineSuffixAction(string suffix)
        {
            Resize(TextWidth + suffix.Length, suffix.Length);

            for (int i = 0; i < m_Texts.Count; i++)
            {
                m_Texts[i] = m_Texts[i] + suffix;
            }
        }

        /// <summary>
        /// Attemps to get text at the specified <paramref name="line"/>, if valid.
        /// </summary>
        /// <param name="line">A line of text inside the TextBox</param>
        /// <returns>A string without linebreak formatting.</returns>
        /// <exception cref="IndexOutOfRangeException">Line is greater than the TextBox's existing number of lines.</exception>       
        public string GetTextAtLine(int line) =>
            line <= m_Texts.Count ? m_Texts[line]
                : throw new IndexOutOfRangeException($"Line {line} is outside of maximum lines in the TextBox.");


    }
}