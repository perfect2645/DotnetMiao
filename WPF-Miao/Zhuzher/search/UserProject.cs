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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer db401307-3491-40c1-91ae-72aa35236503");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 11ec863a-4f2d-41c2-993e-8b36442a9c17");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer dde8a43f-4504-4d78-9ab5-795e07d0aaf1");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 7ec5a385-3eed-47d3-8c07-866901da057b");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 4a762366-60b0-4ec4-acdf-e83f7b7e1ab4");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer e5a793f5-081a-4bf6-9eb2-6f186d1b0b7d");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 9b56ff61-9225-4911-9ae5-0e4b93c5bfd3");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer e167e05d-b580-4b03-a9d8-342dbfc35fd6");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer aee4ecec-2805-4641-b7fc-dbfe4149e8e0");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 6993b581-76e7-420c-976f-1433c20ef4d6");//417
            //16791014827
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
