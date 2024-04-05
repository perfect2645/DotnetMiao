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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer e92c5930-3a30-4bbe-9b19-7083648d5654");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 004df9f0-337e-49f8-ab67-cd375e0fd526");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 2cff92ef-da1f-43a5-ac21-2f7c57b41d60");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 96c5e3a4-e426-485b-9bb8-604e4fd5b592");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 2c0f56bb-80e5-4fad-9cb2-69feecb20b56");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer b1e2150d-e16d-409d-8f87-0ff13ab4924f");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 65a9a7d5-09de-4469-845f-ad0e731cea94");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer a8ac18aa-a162-45b6-a075-41aab5366a7e");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 71ca6375-3eeb-433f-947d-35ef0ed7e7e4");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 61003445-3506-4580-ae96-6f90694f08a7");//417
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
