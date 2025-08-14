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
                case "Fawei": return "17016557-A584-417E-9DEF-BC0B104C90C1";//fawei
                case "13050516050": return "17016557-A584-417E-9DEF-BC0B104C90C1";//fawei
                case "妈妈": return "0E3DBBA8-D7F6-4527-B2F4-27007B77A7AF";//mom
                case "爸爸": return "0E3DBBA8-D7F6-4527-B2F4-27007B77A7AF";//mom
                case "Fawei2645": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645
                case "孙森": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645
                case "刘6717": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645
                case "石琳": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";//SL
                case "4175": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";//SL
                case "恬恬姥姥": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";//SL
                case "4410": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "1544": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "4827": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD

                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13204122645": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "刘0946": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";

                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            //Fawei
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer c0d9fab8-2fe2-432a-b418-3602ce46374f");//Fawei
            //13050516050
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer 411dc8cc-7a7a-476c-b588-ee1a3546b190");//Fawei
            //2645
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 15366cc6-1538-417e-a004-2981c7e1ef74");//2645
            //18604266644
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 922e603b-75f1-4eb0-810e-b177fdb8f64f");//2645
            //13522386717 lsc
            AddUserProject(31614041, "刘6717", "21020025", "大连万科半山半海", "Bearer 447aa52c-5058-4ee8-8ba7-c1d8c749b9c4");//2645
            //// mom
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 7f27355c-e93f-430a-9c89-5874919e8567");//mom
            //// dad
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 0a2796ec-45cf-4778-bd6f-e7657356a29f");//mom
            // SL
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 3b34ab8e-f467-4e59-9f96-b1a3cd03884c");//sl
            //13904221688 
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer 1b5e5ae2-19e3-472f-9848-589befdcee91");
            //LSC 13998021788
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 2813fb78-3d39-4fb2-b7bd-7984d6268282");//lsc
            //13204122645
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer df0ca31d-9b67-4374-9ac4-5cd1492df6bd");//lsc
            // 13552710946  pad
            AddUserProject(31613972, "刘0946", "21080001", "营口海港城", "Bearer 40413ab1-82a8-4ad1-9556-123b66c06802");//lsc

            ////7719
            //AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 3e73109e-8685-4b34-96d7-24f76150d70d");
            ////17143794175
            //AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 2ead2c79-e8e2-47ab-82d1-c94e481d601d");
            ////17116844410 
            ////AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer e0304db4-2858-4ae4-bb08-3277b5d328d9");//SLpad
            ////17143791544 
            //AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 166de921-e112-4311-ab76-2dac50817bf9"); //SLpad
            ////16791014827
            //AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer de0dcf47-c2df-4c32-866e-917487fb27af");// SLpad

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
