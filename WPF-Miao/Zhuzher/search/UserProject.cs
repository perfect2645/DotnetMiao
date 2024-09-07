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
                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
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
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 6cbbd30f-e88b-40c1-b4c0-487994c8c2ec");
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 500f9a6b-a1da-456a-91c3-7c115e00d207");
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 02837e20-d48f-4d3e-82f1-78ed4fac7682");
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 065a2af3-0df3-48e1-abce-f5a37714f5af");//55477  
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer ce1c052e-12e8-44cf-8632-7795dcacbb97");
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 88c6cca2-e286-4d3f-a38a-6da41dd1b268");
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer d9ad293e-b309-4dd6-8e08-a4e6f54e937a");
            //17116844410
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 8edd68a8-c8c3-47ce-924f-33379d74ca7b");
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 696751c9-3ccf-4218-bea1-c12b848d320d");
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 6e12137b-f0a4-42ee-9aa4-82384e2e15c9");
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer 17688fd2-c103-4afb-84e2-ec6842748146");
            //13204122645
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer b784b3fd-fdae-4609-8fc9-a2a11dc64ca8");
            //13050516050
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer 45985e8a-edd0-44c5-80ec-073db2bca196");
            //18604266644孙森--物业券0831
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 33485f2e-ef34-4c24-9eef-5a12f90124fa");
            //13904221688
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer dd292b25-2176-4e9e-a27f-c7ec53fd6194");

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
