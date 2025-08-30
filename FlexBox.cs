using System.Text;

namespace StringFlexBox;

public enum TextboxCorners
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public interface IFlexibleTextContainer
{
    public List<FlexBox> Sources { get; }
    public string FormattedText { get; }
    public string TextWithoutEscapeChars { get; }

    public int Width { get; }

    public List<string> Content();
    public List<string> Texts();

    public string ContentAtLine(int line);
    public string TextAtLine(int line);

    public int Rows();
}

public abstract class FlexBox : IFlexibleTextContainer, IFormattableBox
{
    public FlexBoxBorder BorderStyle = FlexBoxBorder.Default;
    protected List<string> m_Content = new();

    protected string m_FormattedText = string.Empty;
    protected int m_Height = 0;

    protected bool m_IsDirty = true;
    protected int m_PaddingHeightOffset;

    protected int m_PaddingWidthOffset;
    protected int m_PreviousTextSize;

    protected List<FlexBox> m_Sources;
    protected FlexBox m_TallestSource;

    protected List<string> m_Texts = new();
    protected string m_TextWithoutEscapeChars = string.Empty;

    protected FlexBox m_WidestSource;
    protected int m_Width = 0;
    public Padding Padding = new();

    //TODO: Fix implementation - needed for TextBox class.
    public FlexBox()
    {
        m_Sources = new List<FlexBox>();
    }

    public FlexBox(List<FlexBox> sources, Padding padding, FlexBoxBorder borderStyle)
    {
        m_Sources = sources;
        Padding = padding;
        BorderStyle = borderStyle;

        m_WidestSource = sources.OrderByDescending(x => x.Width).First();
        m_TallestSource = sources.OrderByDescending(x => x.Height).First();
    }

    public FlexBox(List<FlexBox> sources, int padding)
        : this(sources, new Padding(padding), FlexBoxBorder.Default)
    {
    }

    public FlexBox(List<FlexBox> sources, Padding padding)
        : this(sources, padding, FlexBoxBorder.Default)
    {
    }

    public int Height => BoxHeight();

    protected bool DrawBorder => TopLine().Any(s => !s.Equals('\0'));

    protected virtual int VerticalPaddingAmount => Width + HorizontalPadding();

    public List<FlexBox> Sources => m_Sources;

    public virtual string FormattedText => m_FormattedText;
    public virtual string TextWithoutEscapeChars => throw new NotImplementedException();

    public int Width => BoxWidth();


    /// <returns>The number of rows this object contains.</returns>
    public virtual int Rows()
    {
        var sum = 0;

        for (var i = 0; i < Sources.Count; i++) sum += Sources[i].Content().Count;

        return sum;
    }

    /// <summary>
    ///     Compiles the object into a text box.
    /// </summary>
    /// <returns>The formatted box.</returns>
    protected virtual string BuildTextBox(int _paddingWidthOffset = 2)
    {
        var builder = new StringBuilder();
        m_Content.Clear();

        // ____ CREATE TOP OF BOX ____ //            
        if (DrawBorder)
        {
            builder.Append(TopLine()).AppendLine();
            m_Content.Add(TopLine());
        }

        AppendTopPadding(builder);

        // ____ INSERT CONTENT IN THE CENTER ____ //
        AppendContent(builder);

        // ____ CREATE BOTTOM OF BOX ____ //            
        AppendBottomPadding(builder);

        if (DrawBorder)
        {
            builder.Append(BottomLine()).AppendLine();
            m_Content.Add(BottomLine());

            m_PaddingWidthOffset = _paddingWidthOffset;
        }

        m_IsDirty = false;
        return builder.ToString();
    }

    /// <summary>
    ///     Logic for adding the formatting at the top of this box.
    /// </summary>
    /// <param name="builder">The builder to append to.</param>
    protected virtual void AppendTopPadding(StringBuilder builder)
    {
        #region Top line padding

        var topPaddingContent = VerticalPadding(Padding.Side.Top);
        builder.Append(topPaddingContent);

        #endregion
    }

    /// <summary>
    ///     The center content of this object before padding and borders are applied. Override to change how this logic is
    ///     performed.
    /// </summary>
    /// <param name="builder">StringBuilder to append content to.</param>
    protected virtual void AppendContent(StringBuilder builder)
    {
        for (var s = 0; s < Sources.Count(); s++)
        for (var i = 0; i < Sources[s].Content().Count; i++)
        {
            var newLineRemoved = Sources[s].ContentAtLine(i).Replace(Environment.NewLine, string.Empty);

            var result = new StringBuilder();
            AppendLeftPadding(result);
            result.Append(newLineRemoved);
            AppendRightPadding(result, s);

            m_Content.Add(result.ToString());
            m_Texts.Add(Sources[s].TextAtLine(i));

            // if (DrawBorder)
            result.AppendLine();

            builder.Append(result);
        }
    }

    /// <summary>
    ///     Appends the FlexBox bottom format to the supplied builder.
    /// </summary>
    /// <param name="builder">The StringBuilder to append to.</param>
    protected virtual void AppendBottomPadding(StringBuilder builder)
    {
        var botPaddingContent = VerticalPadding(Padding.Side.Bottom);
        m_PaddingHeightOffset += VerticalPaddingConversion(Padding.GetSide(Padding.Side.Bottom));
        builder.Append(botPaddingContent);
    }

    protected virtual void AppendLeftPadding(StringBuilder builder)
    {
        builder.Append(BorderStyle.LeftBorder + Padding.PaddingString(Padding.Side.Left));
    }

    protected virtual void AppendRightPadding(StringBuilder builder, int sourceIndex)
    {
        // Amount of padding and empty space to add
        var widthOffset = Math.Max(m_WidestSource.BoxWidth() - m_Sources[sourceIndex].BoxWidth(), 0);
        builder.Append(Padding.PaddingString(Padding.Side.Right) + StringHelpers.Fill(widthOffset) +
                       BorderStyle.RightBorder);
    }

    protected virtual string TopLine()
    {
        return $"{BorderStyle.TopLeftCorner}" +
               $"{new string(BorderStyle.TopBorder, BoxWidth())}" +
               $"{BorderStyle.TopRightCorner}";
    }

    protected virtual string BottomLine()
    {
        return $"{BorderStyle.BottomLeftCorner}" +
               $"{new string(BorderStyle.BottomBorder, BoxWidth())}" +
               $"{BorderStyle.BottomRightCorner}";
    }

    protected virtual void BuildTexts(int paddingWidthOffset = 2)
    {
        m_FormattedText = BuildTextBox(paddingWidthOffset);
        m_TextWithoutEscapeChars = BuildTextWithoutEscapeChars();
    }

    protected virtual string BuildTextWithoutEscapeChars()
    {
        return m_FormattedText.RemoveNewLineEndings();
    }

    protected virtual string VerticalPadding(Padding.Side paddingSide)
    {
        var builder = new StringBuilder();
        var convertedPadding = VerticalPaddingConversion(Padding.GetSide(paddingSide));

        for (var i = 0; i < convertedPadding; i++)
        {
            var paddingString =
                $"{BorderStyle.LeftBorder}" +
                $"{StringHelpers.Fill(BoxWidth())}" +
                $"{BorderStyle.RightBorder}";


            m_Content.Add(paddingString);
            builder.AppendLine(paddingString);
        }

        return builder.ToString();
    }

    protected virtual int HorizontalPadding()
    {
        return Padding.GetSide(Padding.Side.Left) + Padding.GetSide(Padding.Side.Right);
    }

    protected virtual int VerticalPaddingConversion(int paddingAmount)
    {
        return paddingAmount / 2;
    }


    public override string ToString()
    {
        return FormattedText;
    }


    #region FormattableBox Implementation

    public virtual int BoxHeight()
    {
        return m_Content.Count;
    }


    public virtual int BoxWidth()
    {
        var result = m_WidestSource.BoxWidth()
                     + HorizontalPadding()
                     + m_PaddingWidthOffset;

        return result;
    }

    #endregion

    #region Textboxable Implementation

    /// <summary>
    ///     Full content including borders and padding.
    /// </summary>
    /// <returns>A list representing the content.</returns>
    public List<string> Content()
    {
        return m_Content;
    }

    /// <summary>
    ///     The content without padding and borders.
    /// </summary>
    /// <returns>A list represting the formatted text.</returns>
    public List<string> Texts()
    {
        return m_Texts;
    }

    /// <summary>
    ///     Full content at the given line index.
    /// </summary>
    /// <param name="line">Line index to return.</param>
    /// <returns>The content at given line, if there is any. Otherwise string.Empty.</returns>
    public string ContentAtLine(int line)
    {
        return line < m_Content.Count
            ? m_Content[line]
            : string.Empty;
    }

    /// <summary>
    ///     The content without padding and borders at the given index.
    /// </summary>
    /// <param name="line">Line index to return.</param>
    /// <returns>The content at given line, if there is any. Otherwise string.Empty.</returns>
    public string TextAtLine(int line)
    {
        return line < m_Texts.Count
            ? m_Texts[line]
            : string.Empty;
    }

    public void AddToTexts(string text)
    {
        m_Texts.Add(text);
    }

    #endregion
}