namespace Utils.number
{
    public static class NumberUtil
    {

        public static int IntRandom(int from, int to, int seed)
        {
            Random r = new Random(seed);
            return r.Next(from, to + 1);
        }

        public static long LongRandom(long from, long to)
        {
            Random r = new Random();
            return r.NextInt64(from, to + 1);
        }

        public static int IntRandom(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to + 1);
        }

        public static List<T> DisorderItems<T>(this List<T> TList)
        {
            if (TList == null)
            {
                return null;
            }
            List<T> NewList = new List<T>();
            Random Rand = new Random();
            foreach (var item in TList)
            {
                NewList.Insert(Rand.Next(NewList.Count()), item);
            }
            return (NewList);
        }

        public static long GetRandomPhone()
        {
            var phonePreFix = "1";
            var phoneSurfix = LongRandom(5800000000, 8799999999);

            var phoneStr = $"{phonePreFix}{phoneSurfix}";
            return Convert.ToInt64(phoneStr);
        }
    }
}
