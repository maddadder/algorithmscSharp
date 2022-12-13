namespace System.Collections
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
    }
}