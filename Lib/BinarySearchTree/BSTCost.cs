using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.BinarySearchTree
{
    public static class BSTCost
    {
        public static double Calculate(BST bst)
        {
            if (bst == null)
                return 0;

            var stack = new Stack<Tuple<BST, int>>();
            stack.Push(Tuple.Create(bst, 1));

            var sum = 0d;
            while (stack.Count > 0)
            {
                var topTuple = stack.Pop();
                var topBst = topTuple.Item1;
                var topItemLevel = topTuple.Item2;

                sum += topBst.Item.Word.Probability * topItemLevel;

                if (topBst.Left != null)
                    stack.Push(Tuple.Create(topBst.Left, topItemLevel + 1));
                if (topBst.Right != null)
                    stack.Push(Tuple.Create(topBst.Right, topItemLevel + 1));
            }

            return sum;
        }
    }
}
