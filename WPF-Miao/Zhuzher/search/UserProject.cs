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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer e7730def-3cb3-46f9-b3bb-b3e27e8523cb");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 73f14590-7f7d-4698-91c6-32b408d03a46");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 4258e961-b3bb-4edf-9223-b18e59d51019");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer b7527020-e941-4ce5-8ee9-7d8f8219541d");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer db0bc021-a22e-4840-9d77-c2b69db62342");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer d514c231-99ab-42f3-9fb5-cc4ac05417b1");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 6ca40641-4a25-42c9-b3c9-c27b5898b1a3");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer dbe91df3-8f81-446b-809b-65a2df1fe380");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 30c3f6d0-917e-4e32-aae5-11043aed9dca");
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer eaffbcb3-d258-417b-8525-21c42c256156");
            //16791014827
            //AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 30c3f6d0-917e-4e32-aae5-11043aed9dca");
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
