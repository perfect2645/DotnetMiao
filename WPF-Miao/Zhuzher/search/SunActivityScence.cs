using System.Collections.Generic;

namespace Zhuzher.search
{
    internal class SunActivityScence
    {
        public string SceneCode { get; set; }

        public int SceneTimes { get; set; }

        public string Url { get; set; }
    }

    internal class SunActivityScenceList
    {
        public List<SunActivityScence> ScenceList = new List<SunActivityScence>();
        public SunActivityScenceList()
        {
            InitSceneList();
        }

        private void InitSceneList()
        {
            //AddScene("app-startapp", 1, "1");
            AddScene("app-mutuallike", 1, "https://enterprise.4009515151.com/marketing-pages/forum/matchmakingCorner");
            AddScene("app-post-review", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=289");;

        }

        private void AddScene(string code, int time, string? url=null)
        {
            ScenceList.Add(new SunActivityScence
            {
                SceneCode = code,
                SceneTimes = time,
                Url = url
            });
        }
    }
}
