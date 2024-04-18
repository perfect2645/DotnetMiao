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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer c20fc2d5-6d2f-461a-9aac-71d6d24228d5");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 16fcad89-8465-4868-9083-a7fa0321dd2c");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 2cff92ef-da1f-43a5-ac21-2f7c57b41d60");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 9b426b1f-faea-4ca9-8fd5-aee12f7c33be");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 220a0965-7dbf-4a7e-84c3-d45c5055d167");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 05a97ae5-7b43-4f09-a98b-ff0eebbc2382");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer b4c0cd47-ed0a-42a1-a9ae-b1d3cadba9d5");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 8ae77d6e-96e7-4b2b-a827-a587813e6976");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 64bb2b2a-aedd-4e23-aa42-85808e14430c");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer cd7f3914-420f-4d30-9243-2aa7f73c5e78");//417
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
