using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IFormattableBox
{
    int BoxWidth();
    int BoxHeight();
}

public class BoxHorizontal: StringFlexBox, IFormattableBox
{
    private int centerPaddingTotal = 0;

    public BoxHorizontal(List<StringFlexBox> box, Padding padding)
        :base(box, padding)
    {
        SetCenterPadding();
        BuildTexts();
    }

    public BoxHorizontal(List<StringFlexBox> box, int padding)
    : base(box, padding)
    {
        SetCenterPadding();
        BuildTexts();
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
        builder.Append(Padding.PaddingString(Padding.Side.Right) + flexBoxBorder.RightBorder);
    }


    public override int BoxWidth() =>
        sources.Sum(x => x.BoxWidth())
            + HorizontalPadding()
            + paddingWidthOffset
            + centerPaddingTotal;
    
    protected override int VerticalPaddingWidth => width;

    private void SetCenterPadding()
    {
        for (int i = 0; i < sources.Count -1; i++)
        {
            centerPaddingTotal += Padding.GetSide(Padding.Side.Center);
        }
    }



}

