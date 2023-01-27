using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public NodeData NodeData { get; set; }

        public Node(NodeData data)
        {
            NodeData = data;
        }

        public static Node FromTable(List<NodeData> keys, int[,] R)
        {
            return FromTable(keys, R, 0, R.GetLength(0) - 1);
        }
        static Node FromTable(List<NodeData> keys, int[,] R, int i, int j)
        {
            Node p = null;
            if(i == j + 1)
            {
                
            }
            else
            {
                var index = R[i,j] - 1;
                var data = keys[index];
                p = new Node(data);
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
                if (current.NodeData != null)
                    comparisonResult = string.CompareOrdinal(str, current.NodeData.Value);
                else
                    comparisonResult = 0;

                count++;

                if (comparisonResult < 0)
                {
                    if (current.Left == null)
                        return int.MinValue;
                    Console.WriteLine(current.NodeData.Value);
                    current = current.Left;
                }
                else if (comparisonResult > 0)
                {
                    if (current.Right == null)
                        return int.MaxValue;
                    Console.WriteLine(current.NodeData.Value);
                    current = current.Right;
                }
                else
                {
                    return count;
                }
            }
        }
        public static void Display(Node p, int padding)
        {
            if (p != null)
            {
                if (p.Right != null)
                {
                    Display(p.Right, padding + 4);
                }
                if (padding > 0)
                {
                    Console.Write(" ".PadLeft(padding));
                }
                if (p.Right != null)
                {
                    Console.Write("/\n");
                    Console.Write(" ".PadLeft(padding));
                }
                Console.Write(p.NodeData.Value + "\n ");
                if (p.Left != null)
                {
                    Console.Write(" ".PadLeft(padding) + "\\\n");
                    Display(p.Left, padding + 4);
                }
            }
        }
    }
}
