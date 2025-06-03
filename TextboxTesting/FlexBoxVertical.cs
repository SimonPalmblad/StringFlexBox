using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FlexBoxVertical : StringFlexBox, IFormattableBox
{
    public FlexBoxVertical(List<StringFlexBox> box, Padding padding)
        :base(box, padding, FlexBoxBorder.Default)
    {
        BuildTexts();
    }
    
    public FlexBoxVertical(List<StringFlexBox> box, int padding)
    : base(box, new Padding(padding), FlexBoxBorder.Default)
    {
        BuildTexts();
    }
}

