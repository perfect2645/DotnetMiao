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
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 74c8df7c-1c0a-46de-bd05-bffa0adeddd2");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海");
            //AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer b1ccb488-2b65-4db8-a954-7852013f0ee9");
            //AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer b6c4cc0f-68d8-483b-a1b0-ddde3cb77108");
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
