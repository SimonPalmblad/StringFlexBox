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

public class BoxOutlineHorizontal: Textboxable, IFormattableBox
{
    public BoxOutlineHorizontal(List<Textboxable> box, int[] padding)
        :base(box, padding)
    {
        BuildTexts();
    }
    
    public BoxOutlineHorizontal(List<Textboxable> box, int padding)
    : base(box, padding)
    {
        BuildTexts();
    }

    protected override void AppendContent(StringBuilder builder)
    {
        #region Top line padding
            var topPaddingContent = VerticalPadding(TextboxPadding.top);
            builder.Append(topPaddingContent);
        #endregion

        // all content of line X
        // after all content has been added, go to next line

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
                    var paddingOffset = (Sources[i].GetPadding(TextboxPadding.left) + Sources[i].GetPadding(TextboxPadding.right));
                }

                else
                {
                    formattedLine = Sources[i].ContentAtLine(s).Replace(Environment.NewLine, string.Empty);
                }


                //SPACE FOR BETWEEN PADDING
                result.Append(formattedLine);
            }

            AppendRightPadding(result, s);

            content.Add(result.ToString());
            texts.Add(formattedLine);

            // Store information in this class
            builder.Append(result);

            builder.AppendLine();
        }

        #region Bottom line padding
            var botPaddingContent = VerticalPadding(TextboxPadding.bottom);
            builder.Append(botPaddingContent);
        #endregion
    }

    protected override void AppendRightPadding(StringBuilder builder, int sourceIndex = 0)
    {
        builder.Append(PaddingString(TextboxPadding.right) + VerticalBorder);
    }

    public override int BoxWidth() =>
        sources.Sum(x => x.BoxWidth())
            + HorizontalPadding()
            + paddingWidthOffset;
    
    protected override int VerticalPaddingWidth => width;





}

