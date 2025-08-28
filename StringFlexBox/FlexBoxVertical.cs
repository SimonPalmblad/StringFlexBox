namespace StringFlexBox
{
    public class FlexBoxVertical : FlexBox, IFormattableBox
    {
        // ____ CONSTRUCTOR BLOCKS ____ //
        public FlexBoxVertical(List<FlexBox> box, Padding padding, FlexBoxBorder borderStyle)
        :base(box, padding, borderStyle)
        {
            BuildTexts();
        }

        public FlexBoxVertical(List<FlexBox> box, Padding padding)
            : this(box, padding, FlexBoxBorder.Default) {
        }

        public FlexBoxVertical(List<FlexBox> box, int padding)
            : this(box, new Padding(padding), FlexBoxBorder.Default) {
        }

        public FlexBoxVertical(List<FlexBox> box, int padding, FlexBoxBorder borderStyle)
            : this(box, new Padding(padding), borderStyle) {
        }
    }
}