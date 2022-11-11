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
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer a7d0469b-22dd-4c4a-b4d7-8a73311470ef");
            AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer b42b8d98-3477-438a-8e65-836c6a31156d");
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer c4524d4f-58cd-4304-aa36-7978688d3a64");
            //AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer d19fc1cd-7efd-4bb2-a1cb-af42d92cf051");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer e1f8ee9a-2a6c-4a68-9f77-e62df4ef091f");
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
