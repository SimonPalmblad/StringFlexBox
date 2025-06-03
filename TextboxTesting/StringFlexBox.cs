using System.Security.Cryptography;
using System.Text;

public enum TextboxCorners { topLeft , topRight, bottomLeft, bottomRight };

public interface IFlexableTextContainer
{
    public List<StringFlexBox> Sources { get; }
    public string FormattedText { get; }
    public string TextWithoutEscapeChars { get; }

    public int Width { get; }

    public List<string> Content();
    public List<string> Texts();

    public string ContentAtLine(int line);
    public string StringAtLine(int line);
    
    public void AddLinePrefix(string prefix);
    public void AddLineSuffix(string prefix);

    public int Rows();
}

public abstract class StringFlexBox : IFlexableTextContainer, IFormattableBox
{
    public List<StringFlexBox> Sources => sources;
    public Padding Padding = new Padding();
    public FlexBoxBorder BorderStyle = FlexBoxBorder.Default;

    protected List<StringFlexBox> sources;
    
    protected string formattedText = string.Empty;
    protected string textWithoutEscapeChars = string.Empty;
    protected int width = 0;
    protected int height = 0;

    protected int paddingWidthOffset = 0;
    protected int paddingHeightOffset = 0;

    public virtual string FormattedText => formattedText;
    public virtual string TextWithoutEscapeChars => throw new NotImplementedException();

    public int Width => BoxWidth();
    public int Height => BoxHeight();

    protected StringFlexBox widestSource;
    protected StringFlexBox tallestSource;

    protected List<string> texts = new List<string>();
    protected List<string> content = new List<string>();

    protected bool isDirty;

    public StringFlexBox()
    {
        sources = new List<StringFlexBox>();
    }

    public StringFlexBox(List<StringFlexBox> sources, Padding padding, FlexBoxBorder borderStyle)
    {
        this.sources = sources;
        Padding = padding;
        BorderStyle = borderStyle;

        widestSource = sources.OrderByDescending(x => x.Width).First();
        tallestSource = sources.OrderByDescending(x => x.Height).First();
    }

    public StringFlexBox(List<StringFlexBox> sources, int padding)
        : this(sources, new Padding(padding), FlexBoxBorder.Default)
    { }

    public StringFlexBox(List<StringFlexBox> sources, Padding padding)
    : this(sources, padding, FlexBoxBorder.Default)
    { }

    protected virtual string BuildTextBox()
    {
        StringBuilder builder = new StringBuilder();

        #region Top Line logic
            builder.Append(TopLine()).AppendLine();
            content.Add(TopLine());
        #endregion

        AppendTopOfBox(builder);

        // All text content
        AppendContent(builder);

        AppendBottomOfBox(builder);

        #region Bottom Line logic
        builder.Append(BottomLine()).AppendLine();
            content.Add(BottomLine());
        #endregion

        paddingWidthOffset = 2;

        isDirty = false;
        return builder.ToString();
    }

    protected virtual void AppendTopOfBox(StringBuilder builder)
    {
        #region Top line padding
        var topPaddingContent = VerticalPadding(Padding.Side.Top);
        builder.Append(topPaddingContent);

        #endregion
    }

    protected virtual void AppendContent(StringBuilder builder)
    {        
        for (int s = 0; s < Sources.Count(); s++)
        {
            for (int i = 0; i < Sources[s].Content().Count; i++)
            {
                var newLineRemoved = Sources[s].ContentAtLine(i).Replace(Environment.NewLine, string.Empty);

                StringBuilder result = new StringBuilder();
                AppendLeftPadding(result);
                result.Append(newLineRemoved);
                AppendRightPadding(result, s);
               
                content.Add(result.ToString());
                texts.Add(Sources[s].StringAtLine(i));
                
                result.AppendLine();

                // Store information in this class
                builder.Append(result);
            }
        }
    }

    protected virtual void AppendBottomOfBox(StringBuilder builder)
    {
        #region Bottom line padding
            var botPaddingContent = VerticalPadding(Padding.Side.Bottom);
            paddingHeightOffset += VerticalPaddingConversion(Padding.GetSide(Padding.Side.Bottom));
            builder.Append(botPaddingContent);
        #endregion
    }

    protected virtual void AppendLeftPadding(StringBuilder builder)
    {
        // -2 is to offset for the vertical borders being added. Why 2???
        builder.Append(BorderStyle.LeftBorder + Padding.PaddingString(Padding.Side.Left));
    }

    protected virtual void AppendRightPadding(StringBuilder builder, int sourceIndex)
    {
        // how much padding should be added 
        var widthOffset = Math.Max(widestSource.BoxWidth() - sources[sourceIndex].BoxWidth(), 0);
        // -2 is to offset for the vertical borders being added. Why 2???
        builder.Append(Padding.PaddingString(Padding.Side.Right) + StringHelpers.Fill(widthOffset) + BorderStyle.RightBorder );

    }

    protected virtual string TopLine() =>
        $"{BorderStyle.TopLeftCorner}" +
        $"{new string(BorderStyle.TopBorder, BoxWidth())}" +
        $"{BorderStyle.TopRightCorner}";

    protected virtual string BottomLine() =>
        $"{BorderStyle.BottomLeftCorner}" +
        $"{new string(BorderStyle.BottomBorder, BoxWidth())}" +
        $"{BorderStyle.BottomRightCorner}";

    protected virtual void BuildTexts()
    {
        formattedText = BuildTextBox();
        textWithoutEscapeChars = BuildTextWithoutEscapeChars();
    }

    protected virtual string BuildTextWithoutEscapeChars() =>
        formattedText.RemoveNewLineEndings();


    #region FormattableBox Implementation
    public virtual int BoxHeight()
    {
        return content.Count;
        //return sources.Sum(x => x.Height)
        //    + paddingHeightOffset;
    }

    
    public virtual int BoxWidth()
    {
        var result = widestSource.BoxWidth()
                   + HorizontalPadding()
                   + paddingWidthOffset;
        
        return result;
    }

    #endregion

    #region Textboxable Implementation
    public List<string> Content() => content;

    public List<string> Texts() => texts;

    public string ContentAtLine(int line) =>
          line < content.Count ? content[line]
                               : string.Empty;

    public string StringAtLine(int line) =>
        line < texts.Count ? texts[line]
                           : string.Empty;

    public void AddToTexts(string text) => texts.Add(text);

    public void AddLinePrefix(string prefix)
    {
        isDirty = true;
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i] = prefix + texts[i];
        }
    }

    public void AddLineSuffix(string suffix)
    {
        isDirty = true;
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i] = texts[i] + suffix;
        }
    }
    #endregion
   
    protected virtual string VerticalPadding(Padding.Side paddingSide)
    {
        StringBuilder builder = new StringBuilder();
        var convertedPadding = VerticalPaddingConversion(Padding.GetSide(paddingSide));

        for (int i = 0; i < convertedPadding; i++)
        {
            var paddingString =
                $"{BorderStyle.LeftBorder}" +
                $"{StringHelpers.Fill(BoxWidth())}" +
                $"{BorderStyle.RightBorder}";
            

            content.Add(paddingString);
            builder.AppendLine(paddingString);
        }

        return builder.ToString();
    }
    protected virtual int HorizontalPadding() => Padding.GetSide(Padding.Side.Left) + Padding.GetSide(Padding.Side.Right);

    protected virtual int VerticalPaddingWidth => Width + HorizontalPadding();

    protected virtual int VerticalPaddingConversion(int paddingAmount) => paddingAmount / 2;

    public virtual int Rows()
    {
        int sum = 0;

        for (int i = 0; i < Sources.Count; i++)
        {
            sum += Sources[i].Content().Count;
        }

        return sum;
    }

    public override string ToString() =>
        this.FormattedText;
    
}
