using System.Collections;
using Agava.YandexGames;
using Assets.Config;
using Assets.Infrastructure;
using Assets.Person;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.UI.HUD
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Image _heart;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private Image _profilePicture;
        
        private PlayerView _player;

        private void OnDestroy()
        {
            if (_player != null)
                _player.PlayerPresenter.Player.HealthChanged -= OnHealthChang;
        }

        public void Init(PlayerView player)
        {
            _player = player;
            _player.PlayerPresenter.Player.HealthChanged += OnHealthChang;
            OnHealthChang(_player.PlayerPresenter.Player.Health);

            if (Application.isEditor == false && YandexGamesSdk.IsInitialized && PlayerAccount.IsAuthorized)
            {
                PlayerAccount.GetProfileData(SetPlayerName);
            }
            else
            {
                _playerName.text = LanguageConfig.GetAnonymous(Game.GameSettings.CurrentLocalization);
            }
        }

        private void SetPlayerName(PlayerAccountProfileDataResponse player)
        {
            _playerName.text = player.publicName;
            StartCoroutine(SetProfileImage(player.profilePicture));
        }
        
        private IEnumerator SetProfileImage(string url)
        {   
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"[download image error] {request.error}");
            }
            else
            {
                var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                _profilePicture.sprite = sprite;
            }
        }

        private void OnHealthChang(float value)
        {
            float currentHeartsCount = value / PlayerHealth.MaxPlayerHealth;
            _heart.fillAmount = currentHeartsCount;
        }
    }
}