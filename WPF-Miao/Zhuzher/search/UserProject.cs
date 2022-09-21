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
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer ba23c5db-fcb6-47d9-961c-5783ec74c275");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海");
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 2983d086-279f-451d-967a-95115923bf19");
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
