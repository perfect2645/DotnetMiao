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
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e44ed039-db7a-47dc-8f8e-20e5cb6d8e58");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer 28c0b276-c6d6-461b-8462-8804e10ee942");
            //AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 6dec9468-da9b-4dad-9e91-68a29796ef89");
            AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer e30c4d14-635d-428c-bbd2-91d5640843fb");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer b26ed045-4534-452e-a595-dd8559c946db");
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
