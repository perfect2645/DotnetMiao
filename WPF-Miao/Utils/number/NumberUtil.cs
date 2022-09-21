namespace Utils.number
{
    public static class NumberUtil
    {

        public static int IntRandom(int from, int to, int seed)
        {
            Random r = new Random(seed);
            return r.Next(from, to + 1);
        }

        public static int IntRandom(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to + 1);
        }
    }
}
