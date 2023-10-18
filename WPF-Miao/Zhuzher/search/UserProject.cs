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
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 37e45690-098a-460c-b940-570081f3d5a8");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer b6f86ea7-1f77-4577-a304-61df4beddd14");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer f42f4289-d1c3-4177-8cb0-22cb3c6e903b");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer e63bb539-8b8f-459c-a561-e4d9666af2cc");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 14935fb2-b184-47ab-892d-d5d2c697a5f3");
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
