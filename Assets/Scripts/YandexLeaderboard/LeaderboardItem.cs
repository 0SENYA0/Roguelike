using System.Collections;
using Assets.UI.SettingsWindow.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.YandexLeaderboard
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private Image _country;
        [SerializeField] private Image _profilePicture;
        [SerializeField] private LeaderboardCountry _countrySprites;

        public void Initialize(LeaderboardData data)
        {
            _rank.text = data.Rank.ToString();
            _nickName.text = data.NickName;
            _score.text = data.Score.ToString();
            
            ChooseCountry(data.Language);
            StartCoroutine(SetProfileImage(data.Picture));
        }
        
        private void ChooseCountry(string language)
        {
            switch (Language.DefineLanguage(language))
            {
                case Language.ENG:
                    _country.sprite = _countrySprites.Eng;
                    break;
                case Language.RUS:
                    _country.sprite = _countrySprites.Rus;
                    break;
                case Language.TUR:
                    _country.sprite = _countrySprites.Tur;
                    break;
            }
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
    }
}