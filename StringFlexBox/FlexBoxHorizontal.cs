using System.Text;

namespace StringFlexBox
{
    public interface IFormattableBox
    {
        int BoxWidth();
        int BoxHeight();
    }

    public class FlexBoxHorizontal: FlexBox, IFormattableBox
    {
        private int centerPaddingTotal = 0;

        public FlexBoxHorizontal(List<FlexBox> box, Padding padding, FlexBoxBorder borderStyle)
            :base(box, padding, borderStyle)
        {
            SetCenterPadding();
            BuildTexts();
        }

        public FlexBoxHorizontal(List<FlexBox> box, int padding, FlexBoxBorder borderStyle)
            : this(box, new Padding(padding), borderStyle)
        {
        }

        public FlexBoxHorizontal(List<FlexBox> box, Padding padding)
            : this(box, padding, FlexBoxBorder.Default)
        {
        }

        public FlexBoxHorizontal(List<FlexBox> box, int padding)
            : this(box, new Padding(padding), FlexBoxBorder.Default)
        {
        }


        protected override void AppendTopOfBox(StringBuilder builder)
        {
            var topPaddingContent = VerticalPadding(Padding.Side.Top);
            builder.Append(topPaddingContent);
        }

        protected override void AppendBottomOfBox(StringBuilder builder)
        {
            var botPaddingContent = VerticalPadding(Padding.Side.Bottom);
            builder.Append(botPaddingContent);
        }

        protected override void AppendContent(StringBuilder builder)
        {
            for (int s = 0; s < tallestSource.Height; s++)
            {
                StringBuilder result = new StringBuilder();
                AppendLeftPadding(result);
                string formattedLine = string.Empty;

                for (int i = 0; i < Sources.Count; i++)
                {
                
                    if (Sources[i].Height <= s)
                    {
                        formattedLine = StringHelpers.Fill(Sources[i].Width);
                        var paddingOffset = (Sources[i].Padding.GetSide(Padding.Side.Left) + Sources[i].Padding.GetSide(Padding.Side.Right));
                    }

                    else
                    {
                        formattedLine = Sources[i].ContentAtLine(s).Replace(Environment.NewLine, string.Empty);
                    }
                    result.Append(formattedLine);
                
                    //Don't add center padding if this is the last source
                    if(i == Sources.Count-1)
                    { 
                        continue; 
                    }

                    //Padding between elements
                    result.Append(Padding.PaddingString(Padding.Side.Center));
                }

                AppendRightPadding(result, s);

                content.Add(result.ToString());
                texts.Add(formattedLine);

                // Store information in this class
                builder.Append(result);
                builder.AppendLine();
            }
        }

        protected override void AppendRightPadding(StringBuilder builder, int sourceIndex = 0)
        {
            builder.Append(Padding.PaddingString(Padding.Side.Right) + BorderStyle.RightBorder);
        }


        public override int BoxWidth() =>
            sources.Sum(x => x.BoxWidth())
            + HorizontalPadding()
            + paddingWidthOffset
            + centerPaddingTotal;
    
        protected override int VerticalPaddingAmount => width;

        private void SetCenterPadding()
        {
            for (int i = 0; i < sources.Count -1; i++)
            {
                centerPaddingTotal += Padding.GetSide(Padding.Side.Center);
            }
        }



    }
}