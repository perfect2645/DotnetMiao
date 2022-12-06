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
            //AddUserProject(15045709, "妈妈", "21020025", "大连万科半山半海", "Bearer 63f05f95-b21d-47ff-924e-7f2969927bbf");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 689461aa-2097-49c0-9d43-76074a47b7f4");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer bf3c2d1a-bd58-4554-b2f5-b9e538d42f89");
            //AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer d33f1b76-9635-4e65-b320-e6fd5cc5be5c");
            AddUserProject(11067046, "Fawei", "21020002", "大连假日风景", "Bearer 56219e1c-ebfb-4d37-8c89-bd52afd57b25");
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
