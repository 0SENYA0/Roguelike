using System.Collections.Generic;
using System.Linq;

namespace Assets.Infrastructure.DataStorageSystem
{
    public readonly struct GameStatistics
    {
        public readonly int NumberOfAttempts;
        public readonly int NumberOfEnemiesKilled;
        public readonly int NumberOfBossesKilled;

        public GameStatistics(string statisticLine)
        {
            List<int> list_numbers = statisticLine.Split(';').Select(int.Parse).ToList();
            NumberOfAttempts = list_numbers[0];
            NumberOfEnemiesKilled = list_numbers[1];
            NumberOfBossesKilled = list_numbers[2];
        }

        public GameStatistics(int numberOfAttempts, int numberOfEnemiesKilled, int numberOfBossesKilled)
        {
            NumberOfAttempts = numberOfAttempts;
            NumberOfEnemiesKilled = numberOfEnemiesKilled;
            NumberOfBossesKilled = numberOfBossesKilled;
        }

        public string ConvertValueToStringLine() =>
            $"{NumberOfAttempts};{NumberOfEnemiesKilled};{NumberOfBossesKilled}";
    }
}