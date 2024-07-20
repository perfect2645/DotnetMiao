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
                case "Fawei": return "17016557-A584-417E-9DEF-BC0B104C90C1";
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
            //AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer a25074bd-a091-4105-a5bf-99da6df8a7bd");
            //AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 500f9a6b-a1da-456a-91c3-7c115e00d207");
            //AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 72a79847-83dc-4c68-9459-2efa9b03496b");
            //AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer b2b6092d-1f7e-4f45-aaae-db487f270d93");//55477  
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 278390fd-7123-4e01-ad8f-ff849a1147af");
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 32ee4d7d-6477-42b5-8076-88e36149f7e2");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 21b6a644-993e-47ce-8a0b-86c4e70ce290");
            ////17116844410
            //AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 5e2ecc6b-085b-405d-bb3f-0ae7493539cb");
            ////17143794175
            //AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 261e1497-3daf-4837-b86d-b54a85ad9c3e");
            ////17143791544
            //AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 7c9fca62-6777-4428-a926-93397c975ab1");
            ////16791014827--7月10号到期10元物业券
            //AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer 2582629b-8569-4090-b091-941c9979aaea");
            ////13204122645
            //AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 2f91e862-9738-4d15-a340-618b52a9c7cb");
            ////13050516050
            //AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer cdb489a3-8de9-4bb7-9be8-1258f92c2201");
            ////18604266644孙森
            //AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 18976ef0-da67-48ef-ba75-ec9dae26d91a");
            ////13904221688恬恬姥姥
            //AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer 7712b192-2e96-4f7e-b906-1067f1df88cb");

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
