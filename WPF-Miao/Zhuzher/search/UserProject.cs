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
                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 34267227-a59c-453f-8178-e0f49485a074");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer dfe09bb7-f3e9-442e-897a-c0a6103adb10");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer bc84f888-5fd1-4869-96d1-7631d1005f61");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer c6316638-5e4c-4b9f-a1e2-0e98a5fcc178");
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer e1f68e48-063e-4952-8535-d2c68ff166ad");
            AddUserProject(21520695, "刘书辰", "21020025", "大连万科半山半海", "Bearer c3656770-6605-4439-8728-9d9f327e0153");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 7d4de249-833b-4262-9803-96d7287a6ba9");
            //17116844410 asdf2645
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer e4a82620-e4e8-46a9-89d2-4c35c7b54318");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer e232cb95-ab99-4053-8a4f-9fe14c61007c");
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer ee1d3581-ca09-40e3-b982-a6df5c95408a");
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer 1b51f179-e307-45f1-b318-7fb3eff62c89");
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
