namespace Zhuzher.Score
{
    internal class ScoreBet
    {
        public int ScoreExchangeId { get; set; }
        public int GameGoodId { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

        public ScoreBet(int scoreExchangeId, int gameGoodId, int number, string description)
        {
            ScoreExchangeId = scoreExchangeId;
            GameGoodId = gameGoodId;
            Number = number;
            Description = description;
        }
    }
}
