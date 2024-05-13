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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 3d63230a-214e-4634-838d-f8e5150c33dd");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer dfe09bb7-f3e9-442e-897a-c0a6103adb10");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer bc84f888-5fd1-4869-96d1-7631d1005f61");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer c6316638-5e4c-4b9f-a1e2-0e98a5fcc178");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e1f68e48-063e-4952-8535-d2c68ff166ad");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer c089e377-a62e-466f-a6f1-232fcd1502cf");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 4a5b6c81-44c3-4ae0-83d1-913b0156c276");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 853caee5-05ba-4a14-9935-2921febdd9bb");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 4cf8a50d-3bd2-4ea8-b37d-045f749a9998");
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer f02ca2f3-bdfa-4e1b-bb71-9d4e72e47d4c");
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
