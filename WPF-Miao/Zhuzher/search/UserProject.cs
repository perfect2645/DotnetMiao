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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer b138f0c0-c9b9-43a4-aa9a-6ae7d68ecd37");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 37e45690-098a-460c-b940-570081f3d5a8");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer bea0310e-f57c-46c3-8894-7e5b924ec2b8");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 6a5e93cc-6829-44e4-8f9e-3683f9a6f79c");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer e63bb539-8b8f-459c-a561-e4d9666af2cc");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 71624839-84c4-4fd3-ae88-bf8f15e7082d");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer a37d0104-cef8-416e-be4c-b0b0e44117a3");

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
