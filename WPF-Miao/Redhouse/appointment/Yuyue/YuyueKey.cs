namespace Redhouse.appointment.Yuyue
{
    internal class YuyueKey
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Separator { get; set; } = "|";

        public string Key
        {
            get
            {
                return $"{Date}|{Time}";
            }
        }

        public YuyueKey(string date, string time)
        {
            Date = date;
            Time = time;
        }
    }
}
