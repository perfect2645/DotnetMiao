using System;
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
        public string DeviceId { get; set; }
    }

    internal class UserProjectList
    {

        public List<UserProject> UserProjects { get; set; }

        public UserProjectList()
        {
            UserProjects = new List<UserProject>();
            InitUserProjectList();
            
        }

        private string InitUserDeviceId(string userName)
        {
            switch(userName)
            {
                case "Fawei": return "46251174-A584-417E-9DEF-BC0B104C65C1";
                case "妈妈": return "46187513-3D05-475G-SZ67-375JL335C75X";
                case "7719": return "18864638-G836-96NF-8D65-QIG5725VD955";
                case "Fawei2645": return "18704963-42VC-DB46-1046-BA10J4274993";
                case "石琳": return "17088463-B007-886A-OIU6-CBJ6QAS47DDZ";
                case "刘书辰": return "25184668-SF66-S36H-DHKY-CBK7VS22ZMB8";
                case "爸爸": return "40887965-6496-47QP-GV11-JKV9YIHD6248";
                case "4410": return "17045123-9583-67FA-SX55-NVA71345ALOP";
                case "4175": return "23164809-NF25-375S-NK21-ZG23XG66ZFET";
                case "1544": return "31548096-JK72-475M-SM10-TYH5WD11SX6G";
                case "4827": return "34059398-SJT8-3453-24MB-SDFKJ23SD8TN";
                case "13204122645": return "32403098-SDF7-FGJ9-CVB8-ASDKHET48NE0";
                case "13050516050": return "34698023-QPWF-DFG8-8JKG-TRICKRB3MW5L";
                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 8c839891-60c3-4a25-a60b-95d1c7508bdc");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer dfe09bb7-f3e9-442e-897a-c0a6103adb10");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer bc84f888-5fd1-4869-96d1-7631d1005f61");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer b2ac6ee7-99fd-46ff-a5c1-4c131cbe0184");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 4924d22a-5822-4e9c-83d1-c17e9d033a8e");
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 288d394c-07e5-47ba-80fc-549b1e8195e2");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer e716f8a7-b845-404e-9baf-18d2bbfaa838");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer d4b74d7b-e955-4fb2-8d3e-68d1b07ae004");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 8c622a2e-0aad-48bf-a3cd-12b30a416e61");
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 99a25af9-d492-4bd8-9db7-c68eb17f48e7");
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer 6469872a-a2eb-408a-b480-ae3135d9fc9f");
            //13204122645
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 9aad3d33-2fd3-49a5-a2b9-f0277896ad45");
            //13050516050
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer f5ef7488-5f47-43a8-ac24-f94bc8afe37f");

        }

        private void AddUserProject(int userId, string userName, string projectCode, string projectName, string auth)
        {
            UserProjects.Add(new UserProject
            {
                UserId = userId,
                UserName = userName,
                ProjectCode = projectCode,
                ProjectName = projectName,
                Authorization = auth,
                DeviceId = InitUserDeviceId(userName)
        });
            
        }
    }
}
