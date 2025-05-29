using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BoxOutline : Textboxable, IFormattableBox
{
    public BoxOutline(List<Textboxable> box, int[] padding)
        :base(box, padding)
    {
        BuildTexts();
    }
    
    public BoxOutline(List<Textboxable> box, int padding)
    : base(box, padding)
    {
        BuildTexts();
    }
}

