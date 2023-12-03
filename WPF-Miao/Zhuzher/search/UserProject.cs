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
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer a910f8bf-75ef-45d1-b9ae-8bc86b9354bf");
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 5664f93c-1e16-46cc-b8fc-a0a94db61e14");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer dc834a14-3829-4a50-99a4-60eda4d1d025");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 85cf97f7-862c-425a-9b45-2242a779f205");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 41de3e15-0c12-495f-90be-2133e10e0dee");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 427d6284-ad0d-424a-9de5-eac4a5c6b46c");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer dd047f06-1b31-434f-91cc-689a5fdfa6c9");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 8666d837-9664-4cb1-a1bd-f530fe7a5c13");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer b0371899-bf20-49df-a18b-9144d10417ad");
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
