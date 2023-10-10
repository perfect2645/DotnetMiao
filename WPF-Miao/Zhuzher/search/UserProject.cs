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
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer d29c7ca7-8612-40f2-8798-e22c7e2eb1b0");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 49ce289b-a0d2-4b48-ac96-6ddcd8bbe3b7");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer f42f4289-d1c3-4177-8cb0-22cb3c6e903b");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 528e44c6-b6d0-4090-ad18-9afcc8b95ef6");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 3c5dd927-89ee-426c-aeee-f3bac1c95fa4");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 58d2b1a1-c86b-4805-9792-140cff34b06f");

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
