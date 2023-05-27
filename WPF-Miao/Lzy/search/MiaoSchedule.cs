using HttpProcessor.HtmlAnalysis;
using Lzy.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lzy.search
{
    internal class MiaoSchedule
    {
        public HtmlDoc TimeItem { get; set; }
        public string TimeId { get; set; }
        public bool IsAvailable { get; set; }
        public MiaoSchedule(string htmlStr)
        {
            TimeItem = new HtmlDoc(htmlStr);
            BuildSchedule();
        }

        private void BuildSchedule()
        {
            try
            {
                var dataIdXpath = "//div[@data-id]";
                var dataIdNote = TimeItem.GetDefaultNode(dataIdXpath);
                var dataId = dataIdNote?.GetAttributeValue("data-id", "0");
                TimeId = dataId;

                var itemStateXpath = "//div[@class=\"item-state\"]//span";
                var itemStateNode = TimeItem.GetDefaultNode(itemStateXpath);
                var itemState = itemStateNode.InnerText;
                if ("可以约".Equals(itemState))
                {
                    IsAvailable = true;
                }
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"BuildSchedule failed, error={ex.Message}");
            }
        }
    }
}
