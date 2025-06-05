using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextboxRebuild.HelperClasses
{
    public static class PrintBoxDebug
    {
        public static void PrintWithDimensions(StringFlexBox box)
        {
            Console.WriteLine(box.FormattedText);
            Console.WriteLine($"Width: {box.Width} ");
            
            if( box is TextBox)
            {
                var textBox = box as TextBox;
                Console.WriteLine($"Text Width: {textBox?.TextWidth} ");
            }

            Console.WriteLine($"Height: {box.Height} ");
        }
    }
}
