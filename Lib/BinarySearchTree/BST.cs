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

        public static BST FromArray(BSTItem[] array)
        {
            if (array == null || array.Length == 0)
                return null;

            var bst = new BST(array[0]);

            foreach (var i in array.Skip(1))
            {
                bst.Add(i);
            }

            return bst;
        }

        private class Help
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public Help(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        public static BST FromTable(List<Word> keys, int[,] table)
        {
            var stack = new Queue<Help>();
            var rootHelp = new Help(0, table.GetLength(0) - 1);
            stack.Enqueue(rootHelp);

            var rootIndex = table[rootHelp.Row,rootHelp.Column] - 1;
            var rootItem = new BSTItem(keys[rootIndex]);
            var bst = new BST(rootItem);
            keys[rootIndex].Added = true;

            while (stack.Count != 0)
            {
                var current = stack.Dequeue();
                if (current.Row > current.Column)
                {
                    
                }
                else
                {
                    var currentItemIndex = table[current.Row,current.Column] - 1;

                    stack.Enqueue(new Help(current.Row, currentItemIndex - 1));
                    stack.Enqueue(new Help(currentItemIndex + 1, current.Column));
                    var word = keys[currentItemIndex];
                    if (word.Added)
                        continue;

                    bst.Add(new BSTItem(word));
                    word.Added = true;

                }
            }

            return bst;
        }

        public void Add(BSTItem item)
        {
            var root = this;
            while (true)
            {
                if (item.Word != null && item.Word == root.Item.Word)
                    throw new Exception($"Value {item.Word} already in tree");

                if (item.Word.CompareTo(root.Item.Word) < 0)
                {
                    if (root.Left != null)
                    {
                        root = root.Left;
                        continue;
                    }
                    else
                    {
                        root.Left = new BST(item);
                        return;
                    }
                }
                else if (item.Word.CompareTo(root.Item.Word) > 0)
                {
                    if (root.Right != null)
                    {
                        root = root.Right;
                        continue;
                    }
                    else
                    {
                        root.Right = new BST(item);
                        return;
                    }
                }
            }
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

        /*
        For large datasets this will stackoverflow
        public static BST FromTable(List<Word> keys, int[,] R)
        {
            //var KeysList = keys.ToList();
            //KeysList.Insert(0,new Word());
            return FromTable(keys, R, 0, R.GetLength(0) - 1);
        }
        static BST FromTable(List<Word> keys, int[,] R, int i, int j)
        {
            BST p = null;
            var index = R[i,j];
            if(i == j)
            {
                
            }
            else
            {
                var word = keys[index];
                p = new BST(new BSTItem(word));
                p.Left = FromTable(keys, R, i, index - 1); //left subtree
                p.Right = FromTable(keys, R, index, j); //right subtree
            }
            return p;
        }*/
    }
}
