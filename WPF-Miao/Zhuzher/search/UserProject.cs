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
            //AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer ac541f26-c138-4f15-bafc-6929a94aac04");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer 888b9128-ef24-4686-ad0d-8d6923982140");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 91986ceb-ccea-484c-a0d2-6a94ff07c4fd");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 404db7f4-20e6-4c16-bb82-1dfe85cc4f76");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer fcc94b5c-af58-4c33-b41e-2278b030475e");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer b7e60fa3-0c56-4a82-8bef-23aaa39fffb3");
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
