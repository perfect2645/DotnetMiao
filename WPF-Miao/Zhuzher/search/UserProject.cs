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
            ////AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer 63f05f95-b21d-47ff-924e-7f2969927bbf");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer ae4ddbc9-70b1-4c86-b529-6e696d0677de");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e6ee2700-b4b8-4cb8-9cc7-4937195921f2");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer d33f1b76-9635-4e65-b320-e6fd5cc5be5c");
            //AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 4dddffae-c5d0-4a4d-87d6-ee481120515e");
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
