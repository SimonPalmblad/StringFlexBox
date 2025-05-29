using System.Runtime.CompilerServices;
using System.Text;

public class TextBox : Textboxable
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
                BuildTexts ();
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

    public int[] Padding => padding;

    public static TextBox Empty =>
        new TextBox(string.Empty, 0, new int[4] {0,0,0,0} );

    public TextBox(string text, int textWidth, int padding)
        : this (text, textWidth, new int[] { padding, padding, padding, padding})
    {
    }

    public TextBox(string text, int textWidth, int[] _padding)
        : base ()
    {
        sourceText = text;
        padding = _padding;
        this.textWidth = textWidth;

        sources = new List<Textboxable>() { this };

        StringBuilder builder = new StringBuilder();
        text = text.WordWrapWithList(textWidth, out var formatAsList);
        texts = formatAsList;

        int lines = (text.Length / SetBoxWidth(BoxWidth()));
        BuildTexts();
        
        height = content.Count;
    }

    protected override void AppendContent(StringBuilder builder)
    {
        for (int i = 0; i < texts.Count(); i++)
        {
            var newLineRemoved = texts[i].Replace(Environment.NewLine, string.Empty);

            StringBuilder result = new StringBuilder();
            AppendLeftPadding(result);
            result.Append(newLineRemoved);
            AppendTextLineEnd(result, i, SetBoxWidth(BoxWidth()));
            content.Add(result.ToString());

            builder.Append(result)
                   .AppendLine();            
        }
    }

    protected void AppendTextLineEnd(StringBuilder builder, int lineIndex, int width)
    {
        // subtract already added text on left side padding.
        var lineFill = Math.Max(width - (texts[lineIndex].Count() + padding[(int)TextboxPadding.left]), 0); 

        var result = StringHelpers.Fill(lineFill) + VerticalBorder;
        builder.Append(result);
    }

    protected override string TopLine() =>
        $"{Corners[(int)TextboxCorners.topLeft]}" +
        $"{new string(HorizontalBorder, BoxWidth())}" +
        $"{Corners[(int)TextboxCorners.topRight]}";

    protected override string BottomLine() =>
        $"{Corners[(int)TextboxCorners.bottomLeft]}" +
        $"{new string(HorizontalBorder, BoxWidth())}" +
        $"{Corners[(int)TextboxCorners.bottomRight]}";

    protected override string VerticalPadding(TextboxPadding padding)
    {
        StringBuilder builder = new StringBuilder();
        var width = VerticalPaddingWidth;
        var convertedPadding = VerticalPaddingConversion(GetPadding(padding));

        for (int i = 0; i < convertedPadding; i++)
        {
            var paddingString =
                $"{VerticalBorder}" +
                $"{StringHelpers.Fill(width)}" +
                $"{VerticalBorder}";
            
            content.Add(paddingString);
            builder.AppendLine(paddingString);
        }

        return builder.ToString();
    }

    public override int BoxWidth() =>
        textWidth + HorizontalPadding() + paddingWidthOffset;

    public override int BoxHeight() =>
        content.Count();

    protected override int VerticalPaddingWidth => textWidth + HorizontalPadding();


    public static int SetBoxWidth(int _width) => Math.Max(minWidth, _width);
    public TextBox Resize(int _width) => new TextBox(this.sourceText, _width, this.padding);

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

