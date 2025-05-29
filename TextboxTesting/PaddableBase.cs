using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TextboxTesting
{
    internal class PaddableBase: IPaddable
    {
        private int[] padding = new int[4] { 0, 0, 0, 0 };

        public PaddableBase(int[] padding)
        {
            this.padding = padding;
        }

        #region Paddable Implementation
        public int[] GetAllPadding() =>
            padding;

        public int GetPadding(TextboxPadding side) =>
            padding[(int)side];


        public int GetPadding(int index) =>
            padding[index];

        public void SetAllPadding(int amount)
        {
            for (int i = 0; i < padding.Length; i++)
            {
                padding.SetValue(i, amount);
            }
        }

        public void SetPadding(TextboxPadding side, int amount) =>
            padding.SetValue((int)side, amount);

        public void SetPadding(int index, int amount) =>
            padding.SetValue(index, amount);

        public string PaddingString(TextboxPadding side, int additionalPadding = 0) =>
            padding[(int)side] + additionalPadding > 0
                ? new String(' ', padding[(int)side] + additionalPadding)
                : string.Empty;


        public string PaddingString(int paddingIndex, int additionalPadding = 0) =>
            padding[paddingIndex] + additionalPadding > 0
                ? new String(' ', padding[paddingIndex] + additionalPadding)
                : string.Empty;


       
        #endregion
    }
}
