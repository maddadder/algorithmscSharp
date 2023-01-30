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
        public int Children {get;set;}
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
        // This is used to fill children counts.
        static int getElements(Node root)
        {
            if (root == null)
                return 0;
            return getElements(root.Left) +
                getElements(root.Right) + 1;
        }
        // Inserts Children count for each node
        public static Node insertChildrenCount(Node root)
        {
            if (root == null)
                return null;
        
            root.Children = getElements(root) - 1;
            root.Left = insertChildrenCount(root.Left);
            root.Right = insertChildrenCount(root.Right);
            return root;
        }
        // returns number of children for root
        static int children(Node root)
        {
            if (root == null)
                return 0;
            return root.Children + 1;
        }
        static string randomNodeUtil(Node root, int count)
        {
            if (root == null)
                return default;
        
            if (count == children(root.Left))
                return root.NodeData.Value;
        
            if (count < children(root.Left))
                return randomNodeUtil(root.Left, count);
        
            return randomNodeUtil(root.Right,
                count - children(root.Left) - 1);
        }
        
        // Returns Random node
        public static string randomNode(Node root)
        {
            int count = (int) new Random().Next(0, root.Children + 1);
            return randomNodeUtil(root, count);
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
