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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer b3b3f2c7-8079-459e-ae72-8ec08a80ab50");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 48e017f0-7269-4f60-af45-e724d7226ee3");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer c7d2078b-5b17-4910-91cd-7f292c16b579");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 1d7dfb3b-f331-499d-b5f3-8833b468f960");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 387eaa66-c53d-43fb-9a65-e495387ba7b4");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 59efc03d-4594-480b-8a1d-a5cbc95fbb38");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 05d60b7d-ed29-48cf-8f83-47af0b8fc54a");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 287ebe4d-162c-48d6-ae9e-d69197a3988f");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 69b84017-c7ce-4b33-97f2-b4dbcadc77d2");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer d1a8922f-d803-4b28-9491-5cc75e99e055");//417
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
