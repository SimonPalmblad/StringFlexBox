namespace StringFlexBox
{
    public class FlexBoxVertical : FlexBox, IFormattableBox
    {
        public FlexBoxVertical(List<FlexBox> box, Padding padding)
            :base(box, padding, FlexBoxBorder.Default)
        {
            BuildTexts();
        }
    
        public FlexBoxVertical(List<FlexBox> box, int padding)
            : base(box, new Padding(padding), FlexBoxBorder.Default)
        {
            BuildTexts();
        }
    }
}