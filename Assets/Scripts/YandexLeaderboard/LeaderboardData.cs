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

        public int Rank { get; }
        public string Language { get; }
        public string NickName  { get; }
        public int Score { get; }
        public string Picture { get; }

    }
}