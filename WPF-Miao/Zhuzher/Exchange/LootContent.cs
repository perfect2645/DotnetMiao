using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Exchange
{
    internal class LootContent : OnewoContent
    {
        public const string Url = "https://z.onewo.com/market/api/activity/loot/exchange";
        public LootItem LootItem { get; set; }

        public LootContent(UserProject user, LootItem lootItem) : base(Url, user)
        {
            LootItem = lootItem;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("activityGameId", LootItem.ActivityGameId);
            AddContent("activityId", LootItem.ActivityId);
            AddContent("gameGoodId", LootItem.GameGoodId);
            AddContent("number", LootItem.Number);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}