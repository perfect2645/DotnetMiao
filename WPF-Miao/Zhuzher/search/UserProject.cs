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
                case "孙森": return "6C17A131-4FBE-4ED1-AF16-5D4A48AA8D16";
                case "石琳": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";
                case "4175": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";//SL
                case "恬恬姥姥": return "83C363A2-63A1-4951-AA4F-75B38F7CC1EC";//SL
                case "4410": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "1544": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "4827": return "264DA340-F204-4FE1-A5A1-BE93CC50CA64";//SL PAD
                case "刘书辰": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "13204122645": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";
                case "7719": return "A45A2B40-CA8D-4F51-A8AC-0B9E29919369";

                default:return string.Empty;
            }
        }

        private void InitUserProjectList()
        {
            //Fawei
            AddUserProject(11067046, "Fawei", "21020025", "大连万科半山半海", "Bearer 6860265d-b155-4e84-9804-ee91b156cceb");//Fawei
            //13050516050
            AddUserProject(28566129, "13050516050", "21020025", "大连万科半山半海", "Bearer 82378f7e-60aa-4ce8-9f50-5d451d748079");//Fawei
            //2645
            AddUserProject(24521842, "Fawei2645", "21020025", "大连万科半山半海", "Bearer 48323554-d1d8-4dee-b92e-5eb200f044fc");//2645
            //18604266644孙森
            AddUserProject(16022567, "孙森", "21080001", "营口海港城", "Bearer cf7aa09c-1fbc-4082-b96a-b749e3d79ec4");//2645
            //// mom 橙子
            AddUserProject(15045709, "妈妈", "21020002", "大连假日风景", "Bearer bd6c54e4-7221-45d0-a38e-c5d8244b890c");//mom
            //// dad 橙子
            AddUserProject(21744666, "爸爸", "21020002", "大连假日风景", "Bearer 9f034c83-8e36-4a74-9ee9-db6c6c9dc21e");//mom
            // SL 橙子
            AddUserProject(13071092, "石琳", "21020025", "大连万科半山半海", "Bearer 3c94a61e-ff4d-4645-bb83-ee007232164d");//sl
            //17143794175 
            AddUserProject(26652422, "4175", "21020002", "大连假日风景", "Bearer eae23ab9-9174-4acd-8a30-a44c4aa0b61e");
            //13904221688  橙子
            AddUserProject(28701528, "恬恬姥姥", "28701528", "大连万科半山半海", "Bearer ba22a297-79fa-4a02-b8a3-e39f4bfa9054");
            //17116844410 
            AddUserProject(26652384, "4410", "21020025", "大连万科半山半海", "Bearer e0304db4-2858-4ae4-bb08-3277b5d328d9");//SLpad
            //17143791544 
            AddUserProject(26897697, "1544", "21020025", "大连万科半山半海", "Bearer fef36c90-9b8b-4edd-962b-73fe0a8d059e"); //SLpad
            //16791014827
            AddUserProject(26652432, "4827", "21020002", "大连假日风景", "Bearer a3f6fbb3-395b-49b8-91d2-0a10420c4aca");// SLpad
            //LSC橙子
            AddUserProject(21520695, "刘书辰", "21020002", "大连假日风景", "Bearer 5858a183-3e51-4fce-859a-244997b643da");//lsc
            //13204122645橙子
            AddUserProject(28566093, "13204122645", "21020025", "大连万科半山半海", "Bearer 71e1319e-316f-46aa-90d8-7dc655361512");//lsc
            //7719
            AddUserProject(26190961, "7719", "21020002", "大连假日风景", "Bearer a1cc5d51-135a-4c84-a521-c8b4190435f0");
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
