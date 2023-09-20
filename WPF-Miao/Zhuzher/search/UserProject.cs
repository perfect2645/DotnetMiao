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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer c3c3ad6e-27bf-4637-a0fa-59d233cdd95b");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 759da07c-29d5-4cc5-90af-69158cf03f51");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer d3b9a6a9-74c1-4075-916e-144d8822a71c");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer cb66a509-4a2e-450f-b806-f8665d791b2c");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer c07e4dfe-fa67-4b0f-b272-fc1b642e88c4");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 404db7f4-20e6-4c16-bb82-1dfe85cc4f76");
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
