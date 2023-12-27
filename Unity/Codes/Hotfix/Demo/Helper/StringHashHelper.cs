namespace ET
{
    public static class StringHashHelper
    {
        public static long GetLongHashCode(this string self)
        {
            const uint seed = 131;
            ulong hash = 0;
            for (int i = 0; i < self.Length; i++)
            {
                char c = self[i];
                byte high = (byte)(c >>8);
                byte low = (byte)(c & byte.MaxValue);
                hash = hash * seed + high;
                hash = hash * seed + low;
            }

            return (long)hash;
        }
    }
}