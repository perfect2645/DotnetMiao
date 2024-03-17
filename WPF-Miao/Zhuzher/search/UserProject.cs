using System.Collections.Generic;

namespace Zhuzher.search
{
    internal class UserProject
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Authorization { get; set; }
    }

    internal class UserProjectList
    {

        public List<UserProject> UserProjects { get; set; }

        public UserProjectList()
        {
            UserProjects = new List<UserProject>();
            InitUserProjectList();
        }

        private void InitUserProjectList()
        {
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer d030e62c-5a88-4f9a-9304-77980a23a1c0");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer b319daa2-7e74-4523-aefc-7efab2ecce51");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 874f4607-f2e4-4641-8636-cce0d4ef3581");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 3b61e60c-20a6-4f7e-a37b-116c4ce8f34d");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 2c0f56bb-80e5-4fad-9cb2-69feecb20b56");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer b1e2150d-e16d-409d-8f87-0ff13ab4924f");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer acd2348f-cbc3-4d10-b724-e0af6a98d676");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer c3c8ebbe-56f4-4bec-b350-78dc00405b58");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 6828e22b-31d9-4639-8ab5-7cc0e2fed750");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 52323b8b-e599-4017-a550-f8f8aedc8d1c");//417
            //16791014827
        }

        private void AddUserProject(int userId, string userName, string projectCode, string projectName, string auth)
        {
            UserProjects.Add(new UserProject
            {
                UserId = userId,
                UserName = userName,
                ProjectCode = projectCode,
                ProjectName = projectName,
                Authorization = auth
            });
        }
    }
}
