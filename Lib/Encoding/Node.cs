using System;

namespace Lib.Encoding
{
    public class Node : IComparable
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        //Constructor
        //Storing character,frequency and references to left and right
        public Node(char character, int frequency, Node left, Node right)
        {
            this.Character = character;
            this.Frequency = frequency;
            this.Left = left;
            this.Right = right;
        }

        //Implementing CompareTo from IComparable
        public int CompareTo(Object obj)
        {
            if (obj != null)
            {
                Node q = (Node)obj;   // Explicit cast
                if (q != null)
                    return Frequency - q.Frequency; //If the current node's freq is greater than the passed node's freq
                else
                    return 1;
            }
            else
                return 1;
        }
    }
}