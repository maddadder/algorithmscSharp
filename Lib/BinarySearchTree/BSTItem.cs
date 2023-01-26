using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BSTItem
    {
        public Word Word { get; set; }

        public BSTItem(Word word)
        {
            Word = word;
        }

        public override string ToString()
        {
            return $"{Word}";
        }
    }

}
