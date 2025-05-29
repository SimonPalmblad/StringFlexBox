using System.Security.Cryptography;
using System.Text;
using TextboxTesting;

public enum TextboxPadding { left, right, top, bottom};
public enum TextboxCorners { topLeft , topRight, bottomLeft, bottomRight };

public interface IPaddable
{

    public int GetPadding(TextboxPadding side);
    public int GetPadding(int index);
    public int[] GetAllPadding();

    public void SetPadding(TextboxPadding side, int amount);
    public void SetPadding(int index, int amount);
    public void SetAllPadding(int amount);

    public string PaddingString(TextboxPadding side, int extraPadding = 0);
    public string PaddingString(int paddingIndex, int extraPadding = 0);
}

public interface ITextboxable
{
    public List<Textboxable> Sources { get; }
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

public abstract class Textboxable : ITextboxable, IPaddable, IFormattableBox
{
    public List<Textboxable> Sources => sources;
    protected List<Textboxable> sources;
    protected IPaddable paddableInterface;

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

    protected Textboxable? widestSource;
    protected Textboxable? tallestSource;

    protected List<string> texts = new List<string>();
    protected List<string> content = new List<string>();

    protected bool isDirty;

    public char VerticalBorder = '│';
    public char HorizontalBorder = '─';
    public char[] Corners = new char[4] { '┌', '┐', '└', '┘' };

    protected int[] padding = new int[4] { 0, 0, 0, 0 };

    public Textboxable()
    {
        sources = new List<Textboxable>();
    }

    public Textboxable(List<Textboxable> sources, int[] padding)
    {

        this.sources = sources;
        this.padding = padding;

        widestSource = sources?.OrderByDescending(x => x.Width).First();
        tallestSource = sources?.OrderByDescending(x => x.Height).First();
    }

    public Textboxable(List<Textboxable> sources, int padding)
        : this(sources, [padding, padding, padding, padding])
    { }

    protected virtual string BuildTextBox()
    {
        StringBuilder builder = new StringBuilder();

        #region Top Line logic
            builder.Append(TopLine()).AppendLine();
            content.Add(TopLine());
        #endregion

        // All text content
        AppendContent(builder);


        #region Bottom Line logic
            builder.Append(BottomLine()).AppendLine();
            content.Add(BottomLine());
        #endregion

        paddingWidthOffset = 2;

        isDirty = false;
        return builder.ToString();
    }

    protected virtual void AppendContent(StringBuilder builder)
    {        
        #region Top line padding
            var topPaddingContent = VerticalPadding(TextboxPadding.top);
            builder.Append(topPaddingContent);

        #endregion

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

            #region Bottom line padding
            var botPaddingContent = VerticalPadding(TextboxPadding.bottom);
            paddingHeightOffset += VerticalPaddingConversion(GetPadding(TextboxPadding.bottom));
            builder.Append(botPaddingContent);
            #endregion
        }
    }
    protected virtual void AppendLeftPadding(StringBuilder builder)
    {
        // -2 is to offset for the vertical borders being added. Why 2???
        builder.Append(VerticalBorder + PaddingString(TextboxPadding.left));
    }

    protected virtual void AppendRightPadding(StringBuilder builder, int sourceIndex)
    {
        // how much padding should be added 
        var widthOffset = Math.Max(widestSource.BoxWidth() - sources[sourceIndex].BoxWidth(), 0);
        // -2 is to offset for the vertical borders being added. Why 2???
        builder.Append(PaddingString(TextboxPadding.right) + StringHelpers.Fill(widthOffset) + VerticalBorder );

    }

    protected virtual string TopLine() =>
        $"{Corners[(int)TextboxCorners.topLeft]}" +
        $"{new string(HorizontalBorder, (BoxWidth()))}" +
        $"{Corners[(int)TextboxCorners.topRight]}";

    protected virtual string BottomLine() =>
        $"{Corners[(int)TextboxCorners.bottomLeft]}" +
        $"{new string(HorizontalBorder, (BoxWidth()))}" +
        $"{Corners[(int)TextboxCorners.bottomRight]}";

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
   
    protected virtual string VerticalPadding(TextboxPadding paddingSide)
    {
        StringBuilder builder = new StringBuilder();
        var convertedPadding = VerticalPaddingConversion(GetPadding(paddingSide));

        for (int i = 0; i < convertedPadding; i++)
        {
            var paddingString =
                $"{VerticalBorder}" +
                $"{StringHelpers.Fill(BoxWidth())}" +
                $"{VerticalBorder}";
            

            content.Add(paddingString);
            builder.AppendLine(paddingString);
        }

        return builder.ToString();
    }
    protected virtual int HorizontalPadding() => padding[0] + padding[1];

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


    #region Paddable Implementation
    public virtual int GetPadding(TextboxPadding side) =>    
        paddableInterface.GetPadding(side);    

    public virtual int GetPadding(int index) =>
        paddableInterface.GetPadding(index);

    public virtual int[] GetAllPadding() =>
        paddableInterface.GetAllPadding();

    public virtual void SetPadding(TextboxPadding side, int amount) =>
        paddableInterface.SetPadding(side, amount); 

    public virtual void SetPadding(int index, int amount) =>
         paddableInterface.SetPadding((TextboxPadding)index, amount);

    public virtual void SetAllPadding(int amount) =>
         paddableInterface.SetAllPadding(amount);

    public virtual string PaddingString(TextboxPadding side, int extraPadding = 0) =>
         paddableInterface.PaddingString(side, extraPadding);

    public virtual string PaddingString(int paddingIndex, int extraPadding = 0) =>
         paddableInterface.PaddingString(paddingIndex, extraPadding);

    #endregion

}
