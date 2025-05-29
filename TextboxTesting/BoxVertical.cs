using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BoxVertical : StringFlexBox, IFormattableBox
{
    public BoxVertical(List<StringFlexBox> box, Padding padding)
        :base(box, padding)
    {
        BuildTexts();
    }
    
    public BoxVertical(List<StringFlexBox> box, int padding)
    : base(box, new Padding(padding))
    {
        BuildTexts();
    }
}

