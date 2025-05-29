using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextboxTesting
{
    public class Padding
    {
        public enum Side { Left, Right, Top, Bottom, Center };

        private int[] paddingValues = new int[5];

        public Padding(int paddingAmount)
        {
            SetAllPadding(paddingAmount);
        }

        public Padding(int left, int right, int top, int bottom, int center)
        {
            SetPaddingLeft(left);
            SetPaddingRight(right);
            SetPaddingTop(top);
            SetPaddingBottom(bottom);
            SetPaddingCenter(center);
        }

        public int GetPadding(Side side) =>
            paddingValues[(int)side];


        public int GetPadding(int index) =>
            paddingValues[index];

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
            paddingValues.SetValue(index, amount);

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
    

