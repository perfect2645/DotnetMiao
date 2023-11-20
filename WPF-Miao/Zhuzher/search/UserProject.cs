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
            //AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 5664f93c-1e16-46cc-b8fc-a0a94db61e14");
            //AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer d70aa4ba-83d1-42ec-a85c-99eb499ac17e");
            //AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 2b99ac14-dabd-4218-9beb-abd7dce0883e");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 2f77f3bc-991f-4dee-a77a-e0377dc7e239");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 427d6284-ad0d-424a-9de5-eac4a5c6b46c");
            //AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer a638a983-43ba-4200-b7e0-61d62af81328");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer b2c0e910-9ed7-4214-aa31-18545a653d5f");
            //17116844410 asdf2645
            //AddUserProject(, "4410", "21020002", "大连假日风景", "");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 97e04ef6-de92-453f-abd3-8f1cd42f252c");
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
