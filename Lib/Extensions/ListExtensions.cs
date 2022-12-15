using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T[] Slice<T>(this T[] source, int index, int length)
        {       
            T[] slice = new T[length];
            Array.Copy(source, index, slice, 0, length);
            return slice;
        }
        public static T[] Add<T>(this T[] target, T item)
        {
            T[] result = new T[target.Length + 1];
            target.CopyTo(result, 0);
            result[target.Length] = item;
            return result;
        }
        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");
            List<T>[] partitions = new List<T>[totalPartitions];
            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;
            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }
            return partitions;
        }
    }
}