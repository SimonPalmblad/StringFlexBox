using System.Text;

namespace StringFlexBox
{
    public enum TextboxCorners { TopLeft , TopRight, BottomLeft, BottomRight };

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
        protected int previousTextSize;

        protected List<FlexBox> sources;
    
        protected string formattedText = string.Empty;
        protected string textWithoutEscapeChars = string.Empty;
        protected int width = 0;
        protected int height = 0;

        protected int paddingWidthOffset = 0;
        protected int paddingHeightOffset = 0;


        protected FlexBox widestSource;
        protected FlexBox tallestSource;

        protected List<string> texts = new List<string>();
        protected List<string> content = new List<string>();

        protected bool isDirty = true;

        public List<FlexBox> Sources => sources;
        public Padding Padding = new Padding();
        public FlexBoxBorder BorderStyle = FlexBoxBorder.Default;

        public virtual string FormattedText => formattedText;
        public virtual string TextWithoutEscapeChars => throw new NotImplementedException();

        public int Width => BoxWidth();
        public int Height => BoxHeight();

        //TODO: Fix implementation - needed for TextBox class.
        public FlexBox()
        {
            sources = new List<FlexBox>();
        }

        public FlexBox(List<FlexBox> sources, Padding padding, FlexBoxBorder borderStyle)
        {
            this.sources = sources;
            Padding = padding;
            BorderStyle = borderStyle;

            widestSource = sources.OrderByDescending(x => x.Width).First();
            tallestSource = sources.OrderByDescending(x => x.Height).First();
        }

        public FlexBox(List<FlexBox> sources, int padding)
            : this(sources, new Padding(padding), FlexBoxBorder.Default)
        { }

        public FlexBox(List<FlexBox> sources, Padding padding)
            : this(sources, padding, FlexBoxBorder.Default)
        { }


        /// <returns>The number of rows this object contains.</returns>
        public virtual int Rows()
        {
            int sum = 0;

            for (int i = 0; i < Sources.Count; i++)
            {
                sum += Sources[i].Content().Count;
            }

            return sum;
        }

        /// <summary>
        /// Complies the object into a text box.
        /// </summary>
        /// <returns>The formatted box.</returns>
        protected virtual string BuildTextBox(int _paddingWidthOffset = 2)
        {                                                                                
            StringBuilder builder = new StringBuilder();

            content.Clear();

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

            paddingWidthOffset = _paddingWidthOffset;

            isDirty = false;
            return builder.ToString();
        }


        /// <summary>
        /// Logic for adding the formatting at the top of this box.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        protected virtual void AppendTopOfBox(StringBuilder builder)
        {
            #region Top line padding
            var topPaddingContent = VerticalPadding(Padding.Side.Top);
            builder.Append(topPaddingContent);

            #endregion
        }

        /// <summary>
        /// The center content of this object before padding and borders are applied. Override to change how this logic is performed.
        /// </summary>
        /// <param name="builder">StringBuilder to append content to.</param>
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
                    texts.Add(Sources[s].TextAtLine(i));
                
                    result.AppendLine();
                    builder.Append(result);
                }
            }
        }

        /// <summary>
        /// Logic for adding the formatting at the bottom of this box.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
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
            builder.Append(BorderStyle.LeftBorder + Padding.PaddingString(Padding.Side.Left));
        }

        protected virtual void AppendRightPadding(StringBuilder builder, int sourceIndex)
        {
            // how much padding should be added 
            var widthOffset = Math.Max(widestSource.BoxWidth() - sources[sourceIndex].BoxWidth(), 0);
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

        protected virtual void BuildTexts(int paddingWidthOffset = 2)
        {
            formattedText = BuildTextBox(paddingWidthOffset);
            textWithoutEscapeChars = BuildTextWithoutEscapeChars();
        }

        protected virtual string BuildTextWithoutEscapeChars() =>
            formattedText.RemoveNewLineEndings();


        #region FormattableBox Implementation
        public virtual int BoxHeight()
        {
            return content.Count;
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
        /// <summary>
        /// Full content including borders and padding.
        /// </summary>
        /// <returns>A list representing the content.</returns>
        public List<string> Content() => content;

        /// <summary>
        /// The content without padding and borders.
        /// </summary>
        /// <returns>A list represting the formatted text.</returns>
        public List<string> Texts() => texts;

        /// <summary>
        /// Full content at the given line index.
        /// </summary>
        /// <param name="line">Line index to return.</param>
        /// <returns>The content at given line, if there is any. Otherwise string.Empty.</returns>
        public string ContentAtLine(int line) =>
            line < content.Count ? content[line]
                : string.Empty;

        /// <summary>
        /// The content without padding and borders at the given index.
        /// </summary>
        /// <param name="line">Line index to return.</param>
        /// <returns>The content at given line, if there is any. Otherwise string.Empty.</returns>
        public string TextAtLine(int line) =>
            line < texts.Count ? texts[line]
                : string.Empty;

        public void AddToTexts(string text) => texts.Add(text);

   

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

        protected virtual int VerticalPaddingAmount => Width + HorizontalPadding();

        protected virtual int VerticalPaddingConversion(int paddingAmount) => paddingAmount / 2;


        public override string ToString() =>
            this.FormattedText;
    
    }
}