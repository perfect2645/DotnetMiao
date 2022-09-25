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
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer a7d0469b-22dd-4c4a-b4d7-8a73311470ef");
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer d19fc1cd-7efd-4bb2-a1cb-af42d92cf051");
            //AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer bcb0e3b8-2f3d-4a8e-b7f3-2e1782423321");
            //AddUserProject(14929048, "Gary", "21020012", "大连樱花园", "Bearer d19fc1cd-7efd-4bb2-a1cb-af42d92cf051");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer e9095c89-6c4c-425e-a2b4-4dff853e7412");
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
