using System;

namespace BinarySearchTree
{
    public class Word : IComparable
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public bool Added { get; set; } = false;

        public double probability = double.NaN;
        public double Probability
        {
            get
            {
                if (double.IsNaN(probability))
                    probability = (double)Count / Algorithm.SumOfCounts;

                return probability;
            }
        }

        public static Word FromLine(string line)
        {
            var splitLine = line.Split(',');
            return new Word { Value = splitLine[0], Count = int.Parse(splitLine[1]) };
        }

        public override string ToString()
        {
            return $"V: {Value} C: {Count} P: {Probability}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (obj is Word other)
                return string.Compare(Value, other.Value, StringComparison.Ordinal);
            else
                throw new ArgumentException("Object is not a Word");
        }
    }
}
