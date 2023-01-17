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
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 30cfe5b1-6273-4e8f-ac80-d39e8c8888bb");
            AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer bb89d50d-db73-409a-98c4-2be3eccc497b");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 4221de6d-2780-4ae7-86b2-f0c5ebec4d58");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 9fb2fa13-6a41-4e5b-b8b0-ecce6d03fe6a");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer aee51e94-bf33-475f-8ff1-19ad92145052");
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
