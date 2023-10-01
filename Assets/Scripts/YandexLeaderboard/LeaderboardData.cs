namespace Assets.YandexLeaderboard
{
    public struct LeaderboardData
    {
        public LeaderboardData(int rank, string language, string nickName, int score, string picture)
        {
            Rank = rank;
            Language = language;
            NickName = nickName;
            Score = score;
            Picture = picture;
        }

        public int Rank { get; private set; }
        public string Language { get; private set; }
        public string NickName  { get; private set; }
        public int Score { get; private set; }
        public string Picture { get; private set; }

    }
}