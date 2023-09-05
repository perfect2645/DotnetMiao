using Utils.number;

namespace Utils.Rdom
{
    public static class ListRandom
    {
        public static T GetRandomMember<T>(this List<T> list)
        {
            if (!list.HasItem())
            {
                return default(T);
            }
            var randomOrder = list.DisorderItems();

            return randomOrder.FirstOrDefault();
        }

        public static T GetRandomMember<T>(this HashSet<T> list)
        {
            if (!list.HasItem())
            {
                return default(T);
            }
            var randomOrder = list.ToList().DisorderItems();

            return randomOrder.FirstOrDefault();
        }
    }
}
