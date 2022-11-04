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
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer a7d0469b-22dd-4c4a-b4d7-8a73311470ef");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer f6720270-57e3-47a3-83f4-790fd1576f9b");
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 61535645-3330-4add-a507-9dc9c8db5565");
            //AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer d19fc1cd-7efd-4bb2-a1cb-af42d92cf051");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer f2981e49-1765-498e-9d45-ad94b11562f2");
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
