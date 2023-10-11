using System.Collections.Generic;
using Agava.YandexGames;
using Assets.Config;
using Assets.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.YandexLeaderboard
{
    public class YandexLeaderboard : MonoBehaviour
    {
        [Header("Control buttons")]
        [SerializeField] private Button _showLeaderboard;
        [SerializeField] private Button _closeLeaderboard;
        [Header("Panels")]
        [SerializeField] private AuthorizationMessage _authorization;
        [SerializeField] private LeaderboardItem _playerRanking;
        [SerializeField] private LeaderboardView _leaderboardView;

        private void OnEnable()
        {
            _showLeaderboard.onClick.AddListener(ShowLeaderboard);
            _closeLeaderboard.onClick.AddListener(CloseLeaderboard);
        }

        private void OnDisable()
        {
            _showLeaderboard.onClick.RemoveListener(ShowLeaderboard);
            _closeLeaderboard.onClick.RemoveListener(CloseLeaderboard);
        }

        private void CloseLeaderboard()
        {
            _leaderboardView.gameObject.SetActive(false);
        }

        private void ShowLeaderboard()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                AddPlayerToLeaderboard();
                FormListOfPlayers();
                FormPlayersResults();
                _leaderboardView.gameObject.SetActive(true);
            }
            else
            {
                ShowAuthorizationWarning();
            }
        }

        private void ShowAuthorizationWarning()
        {
            _authorization.gameObject.SetActive(true);
            _authorization.Show();
            _authorization.UserResponse += OnUserResponse;
        }

        private void OnUserResponse(bool userResponse)
        {
            _authorization.UserResponse -= OnUserResponse;
            
            if (userResponse)
                Authorized();
        }

        private void Authorized()
        {
            PlayerAccount.Authorize(
                onSuccessCallback: () =>
                {
                    PlayerAccount.RequestPersonalProfileDataPermission();
                    AddPlayerToLeaderboard();
                    FormListOfPlayers();
                    FormPlayersResults();
                    _leaderboardView.gameObject.SetActive(true);
                });
        }

        private void AddPlayerToLeaderboard()
        {
            var response = new LeaderboardEntryResponse();
            
            Leaderboard.GetPlayerEntry(
                leaderboardName: LeaderboardConfig.LeaderboardKey,
                onSuccessCallback: (result) => response = result,
                onErrorCallback: (result) => Debug.LogError($"[YandexLeaderboard] Error in receiving player records: {result}"));
            
            int playerScore = CalculatePlayerScore();
            
            if (response.player != null)
            {
                if (playerScore > response.score)
                {
                    Leaderboard.SetScore(LeaderboardConfig.LeaderboardKey,playerScore);
                }
            }
            else
            {
                Leaderboard.SetScore(LeaderboardConfig.LeaderboardKey, playerScore);
            }
        }

        private void FormListOfPlayers()
        {
            var players = new List<LeaderboardData>();
            
            Leaderboard.GetEntries(LeaderboardConfig.LeaderboardKey, (result) =>
            {
                int resultAmount = Mathf.Clamp(result.entries.Length, 1, LeaderboardConfig.MaxNumberOfPlayersInLeaderboard);

                for (int i = 0; i < resultAmount; i++)
                {
                    var entry = result.entries[i];

                    int rank = entry.rank;
                    string language = entry.player.lang;
                    string nickName = GetName(entry.player.publicName);
                    int score = entry.score;
                    string picture = entry.player.profilePicture;

                    players.Add(new LeaderboardData(rank, language, nickName, score, picture));
                }

                _leaderboardView.ConstructLeaderboard(players);
            });
        }

        private void FormPlayersResults()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                _playerRanking.Initialize(new LeaderboardData(
                    rank: 0,
                    language: Game.GameSettings.CurrentLocalization,
                    nickName: LanguageConfig.GetAnonymous(Game.GameSettings.CurrentLocalization),
                    score: CalculatePlayerScore(),
                    picture: ""));
            }
            else
            {
                Leaderboard.GetPlayerEntry(LeaderboardConfig.LeaderboardKey, (result) =>
                {
                    int rank = result.rank;
                    string language = result.player.lang;
                    string nickName = GetName(result.player.publicName);
                    int score = result.score;
                    string picture = result.player.profilePicture;

                    _playerRanking.Initialize(new LeaderboardData(rank, language, nickName, score, picture));
                });
            }
        }

        private string GetName(string publicName)
        {
            string value = publicName;

            if (string.IsNullOrEmpty(value))
                value = LanguageConfig.GetAnonymous(Game.GameSettings.CurrentLocalization);

            return value;
        }

        private int CalculatePlayerScore()
        {
            var playerStat = Game.GameSettings.PlayerData.GameStatistics;
            var playerScore = playerStat.NumberOfAttempts * playerStat.NumberOfEnemiesKilled + playerStat.NumberOfBossesKilled;

            return playerScore;
        }
    }
}