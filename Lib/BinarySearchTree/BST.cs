using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public class BST
    {
        public BST Left { get; set; }
        public BST Right { get; set; }
        public BSTItem Item { get; set; }

        public BST(BSTItem rootItem)
        {
            Item = rootItem;
        }

        public static BST FromTable(List<Word> keys, int[,] R)
        {
            return FromTable(keys, R, 0, R.GetLength(0) - 1);
        }
        static BST FromTable(List<Word> keys, int[,] R, int i, int j)
        {
            BST p = null;
            if(i == j + 1)
            {
                
            }
            else
            {
                var index = R[i,j] - 1;
                var word = keys[index];
                p = new BST(new BSTItem(word));
                p.Left = FromTable(keys, R, i, index - 1); //left subtree
                p.Right = FromTable(keys, R, index + 1, j); //right subtree
            }
            return p;
        }

        public int Search(string str)
        {
            var count = 0;

            var current = this;
            while (true)
            {
                int comparisonResult;
                if (current.Item.Word != null)
                    comparisonResult = string.CompareOrdinal(str, current.Item.Word.Value);
                else
                    comparisonResult = 0;//string.CompareOrdinal(str, current.Item.Word.Value);

                count++;

                if (comparisonResult < 0)
                {
                    if (current.Left == null)
                        return int.MinValue;
                    Console.WriteLine(current.Item.Word.Value);
                    current = current.Left;
                }
                else if (comparisonResult > 0)
                {
                    if (current.Right == null)
                        return int.MaxValue;
                    Console.WriteLine(current.Item.Word.Value);
                    current = current.Right;
                }
                else
                {
                    return count;
                }
            }
        }
    }
}
