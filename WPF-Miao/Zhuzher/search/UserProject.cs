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
                case "孙森": return "42398734-SDT6-V974-XMND-QFGOBCN6439A";
                case "恬恬姥姥": return "34598072-DSF3-VSDR-PUYH-WSDFJK32446R";
                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            //AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer fd917af0-a233-4fa9-9ae3-7742f0d4bdc7");
            //AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 5f452611-f3fa-4a77-8f46-cdc78c4ef00d");
            //AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 5106290f-2e06-498f-a1aa-9af6286927a8");
            //AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 5f155233-513c-429b-beaa-c40df8079bc3");
            //AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 70ae913d-6f23-415b-a734-b49ae61892e9");
            //AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 288d394c-07e5-47ba-80fc-549b1e8195e2");
            //AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 8238a70a-ca77-4749-9f7b-0a19445db9f0");
            ////17116844410 asdf2645
            //AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer ba501f77-358a-4f33-aa94-b80203deb2a7");
            ////17143794175
            //AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 26097b04-3903-4cf0-ac01-2a11b8614545");
            ////17143791544
            //AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer d5e54faf-a331-4cf9-b76e-2723dd5fe7ac");
            ////16791014827
            //AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer 3b0a6641-cc1b-4a66-9c0a-3cf675e7cc5c");
            ////13204122645
            //AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 9aad3d33-2fd3-49a5-a2b9-f0277896ad45");
            ////13050516050
            //AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer f5ef7488-5f47-43a8-ac24-f94bc8afe37f");
            ////18604266644孙森
            //AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 6adaf2e4-72da-4b73-a710-4e66528daa01");
            //13904221688恬恬姥姥
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer 7712b192-2e96-4f7e-b906-1067f1df88cb");

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
