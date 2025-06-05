using System.Text;

public class TextBox : StringFlexBox
{
    public static int minWidth { get; } = 10;
    private string sourceText;
    private int textWidth;

    public override string FormattedText
    {
        get
        {
            if (isDirty)
            {
                BuildTexts();
            }

            return formattedText;
        }
    }

    public override string TextWithoutEscapeChars
    {
        get
        {
            if (isDirty)
            {
                BuildTexts();
            }
            return textWithoutEscapeChars;
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
        content.Clear();
        
        sourceText = text;
        Padding = padding;
        BorderStyle = borderStyle;
        this.textWidth = textWidth;

        sources = new List<StringFlexBox>() { this };

        StringBuilder builder = new StringBuilder();
        // create a new word-wrapped string with an allowance for adding characters later on, formatted in a list of paragraphs
        text = text.WordWrapWithList(textWidth - wrapAllowance, out var formatAsList);
        texts = formatAsList;

        int lines = (text.Length / SetBoxWidth(BoxWidth()));
        BuildTexts();

        height = content.Count;
    }

    protected override string TopLine() =>
    $"{BorderStyle.TopLeftCorner}" +
    $"{new string(BorderStyle.TopBorder, VerticalPaddingAmount)}" +
    $"{BorderStyle.TopRightCorner}";

    protected override string BottomLine() =>
        $"{BorderStyle.BottomLeftCorner}" +
        $"{new string(BorderStyle.BottomBorder, VerticalPaddingAmount)}" +
        $"{BorderStyle.BottomRightCorner}";

    protected override void AppendTopOfBox(StringBuilder builder)
    {
        var topPaddingContent = VerticalPadding(Padding.Side.Top);
        builder.Append(topPaddingContent);
    }

    protected override void AppendBottomOfBox(StringBuilder builder)
    {
        var botPaddingContent = VerticalPadding(Padding.Side.Bottom);
        paddingHeightOffset += VerticalPaddingConversion(Padding.GetSide(Padding.Side.Bottom));
        builder.Append(botPaddingContent);
    }

    protected override void AppendContent(StringBuilder builder)
    {
        for (int i = 0; i < texts.Count(); i++)
        {
            var newLineRemoved = texts[i].Replace(Environment.NewLine, string.Empty);

            StringBuilder result = new StringBuilder();
            AppendLeftPadding(result);
            result.Append(newLineRemoved);
            AppendTextLineEnd(result, i, SetBoxWidth(VerticalPaddingAmount));
            content.Add(result.ToString());

            builder.Append(result)
                   .AppendLine();
        }
    }

    protected void AppendTextLineEnd(StringBuilder builder, int lineIndex, int width)
    {
        // subtract already added text on left side padding.
        var lineFill = Math.Max(width - (texts[lineIndex].Count() + Padding.GetSide(Padding.Side.Left)), 0);

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

            content.Add(paddingString);
            builder.AppendLine(paddingString);
        }

        return builder.ToString();
    }

    public override int BoxWidth() =>
        textWidth + HorizontalPadding() + paddingWidthOffset;

    public override int BoxHeight() =>
        content.Count();

    public int TextWidth => textWidth;

    protected override int VerticalPaddingAmount => textWidth + HorizontalPadding();


    public static int SetBoxWidth(int _width) => Math.Max(minWidth, _width);

    public void Resize(int newWidth, int wrapAllowance  = 0)
    {
        string newSourceText = string.Empty;

        for (int i = 0; i < texts.Count; i++)
        {
            newSourceText += texts[i].ToString() + " "; //adding back a space that was removed in the word wrapping
        }

        InitializeTextBox(newSourceText, newWidth, Padding, BorderStyle, wrapAllowance);
    }

    /// <summary>
    /// Attemps to get text at the specified <paramref name="line"/>, if valid.
    /// </summary>
    /// <param name="line">A line of text inside the TextBox</param>
    /// <returns>A string without linebreak formatting.</returns>
    /// <exception cref="IndexOutOfRangeException">Line is greater than the TextBox's existing number of lines.</exception>       
    public string GetTextAtLine(int line) =>
        line <= texts.Count ? texts[line]
                            : throw new IndexOutOfRangeException($"Line {line} is outside of maximum lines in the TextBox.");


}

