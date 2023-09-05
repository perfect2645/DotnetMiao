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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 4cd790fb-4c7e-4cfa-a89e-26503dd9c888");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 43147122-943b-4be9-82d0-062f6766e992");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer da78c0dd-87cc-4375-82fc-3690ac51650a");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e2f53c62-0ad5-4d9d-955e-0468eb40e092");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer 0e12b234-797c-4ed2-bc71-fc79522fe469");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 404db7f4-20e6-4c16-bb82-1dfe85cc4f76");
            //AddUserProject(12513073, "Zoe", "21020012", "大连樱花园", "Bearer d91f2b58-fa2c-4f4a-9b13-b9a135105596");

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
