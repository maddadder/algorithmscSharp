using System;
namespace Lib.Model
{
    public class Job
    {
        public Job(int weight, int length)
        {
            Weight = weight;
            Length = length;
        }

        public int Length { get; private set; }
        public int Weight { get; private set; }
        public int Cost()
        {
            return Length;
        }
        public int Value()
        {
            return Weight;
        }
    }
}