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
            Console.WriteLine($"Width of {box}: {box.Width} ");
            Console.WriteLine($"Height of {box}: {box.Height} ");
        }
    }
}
