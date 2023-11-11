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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 5664f93c-1e16-46cc-b8fc-a0a94db61e14");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer e033c0d7-dd83-4e34-b247-cb6adc20f447");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 5dacad36-8fda-49b4-a239-46ffeaa46d3b");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 1484a672-56d5-4796-adc4-de8e2612ff05");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer e63bb539-8b8f-459c-a561-e4d9666af2cc");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 4aceb54d-32ce-4883-a95f-c557dc649b9e");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer afa888af-067c-4ff4-9237-d7133c0d4b77");

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
