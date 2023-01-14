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
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer e6eea748-f688-4a24-8cb4-b929b00349eb");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer 02bf2b18-4550-40f2-ab09-c48cad06f883");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 4221de6d-2780-4ae7-86b2-f0c5ebec4d58");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 3246aa0e-52f1-472d-8b39-d17bf986c6e7");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 04c3d6fc-0b6d-4ce7-817c-7aa0f0bd90d6");
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
