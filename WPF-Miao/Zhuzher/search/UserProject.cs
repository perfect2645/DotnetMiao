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
                case "13050516050": return "17016557-A584-417E-9DEF-BC0B104C90C1";//fawei
                case "妈妈": return "0E3DBBA8-D7F6-4527-B2F4-27007B77A7AF";//mom
                case "爸爸": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645
                case "Fawei2645": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";//2645

                case "石琳": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";
                case "4175": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";//SL
                case "恬恬姥姥": return "BA1AB2D3-745D-4214-87C3-293D9D7A1D81";//SL
                case "4410": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "1544": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "4827": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "孙森": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13204122645": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "7719": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";

                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            //Fawei
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 0e55a0c1-3a61-4ec4-9186-ee4112c0f188");//Fawei
            //13050516050
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer 82378f7e-60aa-4ce8-9f50-5d451d748079");//Fawei
            //2645
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 48323554-d1d8-4dee-b92e-5eb200f044fc");//2645
            //// mom 橙子
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer 79ece6dc-5874-40bc-8bc0-b8bebf0ec220");//mom
            //// dad 橙子
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 5d3abe8f-9de3-4577-a2bb-139cf91ef600");//mom
            // SL 橙子
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 2cf676f1-2f2b-45e1-a5ba-88020f54845e");//sl
            //17143794175
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer 2ead2c79-e8e2-47ab-82d1-c94e481d601d");
            //13904221688  橙子
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer d5b724fe-dc40-4245-958e-14dcd4ad8643");
            //17116844410 
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer e0304db4-2858-4ae4-bb08-3277b5d328d9");//SLpad
            //17143791544 
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer 166de921-e112-4311-ab76-2dac50817bf9"); //SLpad
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer a3f6fbb3-395b-49b8-91d2-0a10420c4aca");// SLpad
            //18604266644孙森
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer 6af8c9b1-77ba-4921-95fb-909366610165");// SLpad
            //LSC橙子
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer fba74794-0f85-43ef-a6fd-63d79c227c5a");//lsc
            //13204122645
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 9f40d17f-e76e-459f-a917-58652dd86b74");//lsc
            //7719
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer 3e73109e-8685-4b34-96d7-24f76150d70d");
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
