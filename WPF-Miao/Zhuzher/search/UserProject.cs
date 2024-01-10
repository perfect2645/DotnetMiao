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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 9a8331b6-4bbb-40de-aa32-5aa4cac6e3d6");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 0328c3c8-1d9d-479c-a909-5c65ae19ad40");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer c7f77936-1e2a-4856-aac6-1af6dabf4df4");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer dec061d7-f108-4d40-81c5-4922dca27de0");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 4af9bdb1-77c9-433f-b4bb-56beb094be36");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 6844d603-1a64-4b37-95f6-3f44540055da");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer bf5f12b2-2e0c-4485-8c8d-60d082940bdc");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 24f4a008-a5eb-4f67-afa4-701af0540e30");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer f575c1d2-4166-426c-a721-80352705ebc6");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 7941530d-4dcc-44db-8657-47899172d559");//417
            //16791014827
            //AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 30c3f6d0-917e-4e32-aae5-11043aed9dca");
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
