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
            //AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 22a1828d-299a-4c9f-8586-d4e2f9055b33");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer bb89d50d-db73-409a-98c4-2be3eccc497b");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer b0fd568f-e16e-4945-9165-b97b8d068ef7");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 404db7f4-20e6-4c16-bb82-1dfe85cc4f76");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer fd31e6b6-1acc-4d84-8b44-489794522b81");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer aee51e94-bf33-475f-8ff1-19ad92145052");
            //AddUserProject(12513073, "Zoe", "21020012", "大连樱花园", "Bearer d91f2b58-fa2c-4f4a-9b13-b9a135105596");

            //AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer d19fc1cd-7efd-4bb2-a1cb-af42d92cf051");
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
