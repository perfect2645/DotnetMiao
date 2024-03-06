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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 2c62db30-f222-44b9-94ac-0252d52a5a63");//478
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 48e017f0-7269-4f60-af45-e724d7226ee3");//524
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer e9eb94f2-6067-488f-88a4-2cfef5627673");//524
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 36a18abc-3988-4c43-ab53-135b18141a5a");//524
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer c3fd4147-1c1c-476b-bc70-78dac5b588fc");//397
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 94a160cc-64ca-4227-9d05-1016bf85021e");//473
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 118b54e8-0b55-4835-aee3-7de161af1e38");//474
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 502cf342-1437-41dc-ab89-80ddebfaf13d");//474
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 8e371486-0a9a-4773-b849-8231216a7252");//474
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 3713d346-fba5-42d3-904e-a3511e921343");//417
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
