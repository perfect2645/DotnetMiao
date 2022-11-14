namespace Jkchegu.session
{
    internal class User
    {
        public string Etid { get; set; }
        public string Name { get; set; }

        public string this[string etid]
        {
            get
            {
                if (etid == Etid)
                {
                    return Name;
                }
                return null;
            }
        }

        public User(string etid, string name)
        {
            Etid = etid;
            Name = name;
        }
    }
}
