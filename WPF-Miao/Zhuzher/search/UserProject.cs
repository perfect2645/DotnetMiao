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
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 3331f878-404b-41ec-8df1-a05f0203b664");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer bb89d50d-db73-409a-98c4-2be3eccc497b");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 3c41ef45-38b5-40ee-b42c-ff2470b1177d");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 798f571f-86d7-46ce-84b2-cb441c166fba");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer aee51e94-bf33-475f-8ff1-19ad92145052");
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
