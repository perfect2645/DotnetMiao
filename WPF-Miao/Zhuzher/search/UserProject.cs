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
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer b809afe2-0cb2-4de2-a725-11add76d4f94");
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 5664f93c-1e16-46cc-b8fc-a0a94db61e14");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 9f36ff7d-a091-4603-94b8-d71090a71b62");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 6dd419ee-1c33-497e-a579-600e5dcdba45");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 57fb9866-9e37-43fc-87d1-d0048777cafb");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 427d6284-ad0d-424a-9de5-eac4a5c6b46c");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 165a410d-8f9f-4fa6-8cb7-a52359cb091d");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 8df49d1e-2a40-4842-b28c-1d509b24009f");
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
