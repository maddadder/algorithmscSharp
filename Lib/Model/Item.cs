using System;
namespace Lib.Model
{
    public class Item
    {
        public Item(int value, int cost)
        {
            Weight = value;
            Size = cost;
        }

        public int Size { get; private set; }
        public int Weight { get; private set; }
        public int Cost()
        {
            return Size;
        }
        public int Value()
        {
            return Weight;
        }
    }
}