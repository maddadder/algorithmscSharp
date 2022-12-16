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
        public static IEnumerable<IEnumerable<T>> SplitByChunks<T>(this IEnumerable<T> source, int chunkSize)
        {
            if (chunkSize < 1)
            throw new ArgumentException("Chunk size must be greater than zero.");

            IEnumerator<T> enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return getChunk(enumerator, chunkSize);
            }
        }

        private static IEnumerable<T> getChunk<T>(IEnumerator<T> enumerator, int chunkSize)
        {
            int count = 0;
            do
            {
                yield return enumerator.Current;
            } while (++count < chunkSize && enumerator.MoveNext());           
        }
    }
}