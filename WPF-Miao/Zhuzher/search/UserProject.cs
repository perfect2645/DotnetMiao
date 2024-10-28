using System;
using System.Collections.Generic;
using System.Windows.Documents;

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
                case "7719": return "17016557-A584-417E-9DEF-BC0B104C90C1";//fawei
                case "妈妈": return "0E3DBBA8-D7F6-4527-B2F4-27007B77A7AF";//mom
                case "爸爸": return "0E3DBBA8-D7F6-4527-B2F4-27007B77A7AF";//mom
                case "Fawei2645": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645
                case "孙森": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";
                case "石琳": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";
                case "4175": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";//SL
                case "4410": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "1544": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "4827": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13204122645": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13050516050": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "恬恬姥姥": return "34598072-DSF3-VSDR-PUYH-WSDFJK32446R";
                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 6346a312-b0cc-4284-bc38-7c3fd26498d2");//Fawei
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 1be58542-5dee-45af-af15-ae2eeb9ec2a1");//Fawei
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 9cd004ab-bc23-466e-acc1-efe5d673d621");//2645
            //18604266644孙森 已使用500
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 9bc89d56-67a4-46af-aef7-29a8cfd1731d");//2645
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 3be6f236-7f66-418c-a229-2c1030db15b7");//mom
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 04da01bc-b14d-445a-b5ac-a7ff390a042a");//mom
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer a38465ad-bee2-40dc-8a2a-1b86027c5cd9");//sl
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 335dadfb-7f32-49ee-8807-a36a65332888");//sl
            //17116844410
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 6346a312-b0cc-4284-bc38-7c3fd26498d2");//SLpad
            //17143791544
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer a01cc484-4dd2-4b92-8a19-350a6b79d1c0"); //SLpad
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer bda3012f-31b9-41b8-8cf2-8aac9bd97589");// SLpad
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 3ec9aa89-681a-4265-9724-72dd746248fd");//lsc
            ////13204122645 已使用500
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 4c7f47ca-fbb0-436e-bd87-c102cb5fbc59");//lsc
            //13050516050 已使用500
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer 96a5afc4-61cf-4229-a834-1c6943884f42");//lsc
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
