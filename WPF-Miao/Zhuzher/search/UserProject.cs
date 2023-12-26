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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 9d248fca-a8be-4b2b-bca5-3a354c11b63e");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 95a3c10b-49dc-4630-9bb1-4bb9f5efe132");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 1c440a48-a88c-4839-ae30-b5b97549f03c");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer a6d4a793-6eb3-4b32-8450-4750ea606bda");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 49b01920-ec66-4b0c-8e68-09c302e59dde");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer d514c231-99ab-42f3-9fb5-cc4ac05417b1");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 7419de08-ac17-482b-813c-500272301490");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 81b17e48-3ca9-4e63-b6a0-0c7da0a21bdd");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 1559623f-6cea-4a48-bb75-b4a4cea05fe3");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 9a16d720-d7db-4739-87a9-4bad6bd365bc");//417
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
