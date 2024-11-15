using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Zhuzher.search
{
    public class UserProject
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
                case "恬恬姥姥": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";//SL
                case "4410": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "1544": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "4827": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";
                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13204122645": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13050516050": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            // Fawei 已使用1000
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 7aa09638-075d-426a-a687-84f41ca9696b");//Fawei
            //7719 已使用1000
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 133defb7-8e4f-47a9-b97d-7e5e3094b621");//Fawei
            ////2645 已使用1000
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 9cd004ab-bc23-466e-acc1-efe5d673d621");//2645
            ////18604266644孙森 已使用500
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 9bc89d56-67a4-46af-aef7-29a8cfd1731d");//2645
            // mom 已使用500
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 766ab5f4-585e-4cb1-9b1d-423a5d63d31d");//mom
            //// dad 已使用1000
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 83707356-2154-48c2-b741-7c0ef7d64339");//mom
            // SL 已使用500
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 50a293f5-8e3e-4306-8fa3-c9c409ccf5b3");//sl
            //17143794175 //sl 已使用500
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 11e71bbe-15bc-4e9d-a487-b8c5179ab136");
            //13904221688 //sl 已使用500
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer 18dedc9d-31e5-4cfb-8047-3044d45a59fd");
            //17116844410 作废
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer 52abdb00-5cfc-4cc7-bda2-adb6bc47c132");//SLpad
            //17143791544 已使用500
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 6821a7fd-36c9-4e3f-a8f0-a322b20e186b"); //SLpad
            //16791014827 已使用500
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer bda3012f-31b9-41b8-8cf2-8aac9bd97589");// SLpad
            //LSC 已使用1000
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 0572b033-4954-4253-b85a-c4f56eaa4c75");//lsc
            //13204122645 已使用500
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 4c7f47ca-fbb0-436e-bd87-c102cb5fbc59");//lsc
            //13050516050 已使用500
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer dab8bb00-a8e5-4ab1-8569-d1b252d6acbf");//lsc
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
