namespace Utils.Rdom
{
    public static class StringRandom
    {
        public static HashSet<string> LastNameList { get; set; }
        public static HashSet<string> NameList { get; set; }

        static StringRandom()
        {
            BuildNameCache();
        }

        private static void BuildNameCache()
        {
            BuildLastNameList();
            BuildNameList();
        }

        private static void BuildLastNameList()
        {
            LastNameList = new HashSet<string>();
            LastNameList.Add("郑");
            LastNameList.Add("杨");
            LastNameList.Add("裴");
            LastNameList.Add("马");
            LastNameList.Add("车");
            LastNameList.Add("耿");
            LastNameList.Add("于");
            LastNameList.Add("石");
            LastNameList.Add("何");
            LastNameList.Add("田");
            LastNameList.Add("王");
            LastNameList.Add("刘");
            LastNameList.Add("田");
            LastNameList.Add("黄");
            LastNameList.Add("张");
            LastNameList.Add("骆");
            LastNameList.Add("蒋");
            LastNameList.Add("周");
            LastNameList.Add("刘");
            LastNameList.Add("郭");
            LastNameList.Add("赵");
        }

        private static void BuildNameList()
        {
            NameList = new HashSet<string>();
            NameList.Add("馨语");
            NameList.Add("纪娜");
            NameList.Add("花琴");
            NameList.Add("桃桃");
            NameList.Add("亚娟");
            NameList.Add("妍妍");
            NameList.Add("明月");
            NameList.Add("金欣");
            NameList.Add("晶晶");
            NameList.Add("婷婷");
            NameList.Add("春燕");
            NameList.Add("欣渝");
            NameList.Add("茂娇");
            NameList.Add("海青");
            NameList.Add("莉娟");
            NameList.Add("雪晴");
            NameList.Add("婉秀");
            NameList.Add("晨杨");
            NameList.Add("永飞");
            NameList.Add("艺珊");
            NameList.Add("雅雯");
            NameList.Add("新涵");
            NameList.Add("瑶");
            NameList.Add("露露");
            NameList.Add("诗瑶");
            NameList.Add("颖");
            NameList.Add("静");
            NameList.Add("雪");
            NameList.Add("丽娜");
            NameList.Add("敏");
            NameList.Add("丽梅");
            NameList.Add("文静");
            NameList.Add("慧");
            NameList.Add("维维");
            NameList.Add("娜娜");
            NameList.Add("欣");
            NameList.Add("美文");
            NameList.Add("惠月");
            NameList.Add("艺菲");
            NameList.Add("艺珊");
            NameList.Add("倩");
            NameList.Add("晓琴");
            NameList.Add("晓明");
            NameList.Add("悦悦");
            NameList.Add("悦");
        }
    }
}
