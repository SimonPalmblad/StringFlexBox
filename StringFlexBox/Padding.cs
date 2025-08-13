namespace StringFlexBox
{
    public class Padding
    {
        public enum Side { Left, Right, Top, Bottom, Center };

        private int[] paddingValues = new int[5];

        public Padding()
        {
            SetAllPadding(0);
        }

        public Padding(int paddingAmount)
        {
            SetAllPadding(paddingAmount);
        }

        public Padding(int horizontalPadding, int verticalPadding, int centerPadding)
        {
            SetPaddingLeft(horizontalPadding);
            SetPaddingRight(horizontalPadding);
            SetPaddingTop(verticalPadding);
            SetPaddingBottom(verticalPadding);
            SetPaddingCenter(centerPadding);

        }

        public Padding(int left, int right, int top, int bottom, int center)
        {
            SetPaddingLeft(left);
            SetPaddingRight(right);
            SetPaddingTop(top);
            SetPaddingBottom(bottom);
            SetPaddingCenter(center);
        }

        public int GetSide(Side side) =>
            paddingValues[(int)side];


        public int GetSide(int index) =>
            index <= paddingValues.Length ? paddingValues[index]
                : throw new IndexOutOfRangeException($"No side of index {index} found in the padding.");

        public void SetAllPadding(int amount)
        {
            for (int i = 0; i < paddingValues.Length; i++)
            {
                paddingValues[i] = amount;
            }
        }

        public void SetPaddingLeft(int amount) =>
            SetPadding(0, amount);

        public void SetPaddingRight(int amount) =>
            SetPadding(1, amount);

        public void SetPaddingTop(int amount) =>
            SetPadding(2, amount);

        public void SetPaddingBottom(int amount) =>
            SetPadding(3, amount);

        public void SetPaddingCenter(int amount) =>
            SetPadding(4, amount);

        public void SetPadding(Side side, int amount) =>
            paddingValues.SetValue((int)side, amount);

        public void SetPadding(int index, int amount) =>
            paddingValues.SetValue(amount, index);

        public string PaddingString(Side side, int additionalPadding = 0) =>
            paddingValues[(int)side] + additionalPadding > 0
                ? new String(' ', paddingValues[(int)side] + additionalPadding)
                : string.Empty;


        public string PaddingString(int paddingIndex, int additionalPadding = 0) =>
            paddingValues[paddingIndex] + additionalPadding > 0
                ? new String(' ', paddingValues[paddingIndex] + additionalPadding)
                : string.Empty;
    }
}