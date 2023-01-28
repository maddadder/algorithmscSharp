using System;

namespace Lib.BinarySearchTree
{
    public class NodeData : IComparable
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public bool Added { get; set; } = false;

        public static NodeData FromLine(string line)
        {
            var splitLine = line.Split(',');
            return new NodeData { Value = splitLine[0], Count = int.Parse(splitLine[1]) };
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (obj is NodeData other)
                return string.Compare(Value, other.Value, StringComparison.Ordinal);
            else
                throw new ArgumentException("Object is not a NodeData");
        }
    }
}
