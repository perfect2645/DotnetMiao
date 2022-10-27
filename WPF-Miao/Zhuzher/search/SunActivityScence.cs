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
            AddScene("5thzone-blackpearl-pay", 3, "https://uiis.4009515151.com/product/153949/projectCode/0?pageMark=zwtgxyy1");
            AddScene("5thzone-mall-verification", 1, "https://sqmallh5.onewo.com/#/groupBuyDetail?couponId=CI202207251155432956");
            AddScene("5thzone-blackpearl-pay", 3, "https://uiis.4009515151.com/product/153811/projectCode/0?pageMark=zwtgjctx");
            AddScene("5thzone-blackpearl-pay", 3, "https://uiis.4009515151.com/product/148678/projectCode/0?pageMark=zwtglpzj6b");

            //AddScene("5thzone-blackpearl-pay", 1, "https://uiis.4009515151.com/product/151575/projectCode/0?pageMark=zwtgnsssb");
            //AddScene("5thzone-blackpearl-pay", 1, "https://uiis.4009515151.com/product/154758/projectCode/0?pageMark=zwtgrxyzj");

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
