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
            //AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 45b8d46c-8213-40ba-9eea-2290dd23f048");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 11ec863a-4f2d-41c2-993e-8b36442a9c17");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer f7a3860b-fb5a-40c8-8ef3-5307236275ae");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 4d80ae1b-2588-4837-a3d1-53c50d23477a");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e0db8cff-79f8-48cd-95cb-e0db8b0f774d");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer e5a793f5-081a-4bf6-9eb2-6f186d1b0b7d");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 0618c619-b3fb-4cf0-8427-a73e9cb5ce2c");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer c8eaa938-5c86-4331-9aa6-eaff20303c62");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 7ceefb03-666d-4c37-b463-0ca5075c6cc3");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 99f5bf2f-c593-4c30-b0c1-de268c215cda");//417
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
