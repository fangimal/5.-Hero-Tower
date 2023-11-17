using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SettingsPanelUI : MonoBehaviour
    {
        [SerializeField] private Button _backBtn;
        [SerializeField] private Button _resetProgressBtn;
        [SerializeField] private Button _langBtn;
        [SerializeField] private Image _flagIcon;
        [SerializeField] private Button _musicBtn;
        [SerializeField] private Image _musicIcon;
        [SerializeField] private Button _soundBtn;
        [SerializeField] private Image _soundIcon;

        public event Action OnBackClicked;
        public event Action OnChangeLanguage;
        public event Action OnResetGameProgress;
        public event Action OnSoundOnOff;
        public event Action OnMusicOnOff;

        private void Awake()
        {
            _backBtn.onClick.AddListener(() => OnBackClicked?.Invoke());

            _langBtn.onClick.AddListener(() => OnChangeLanguage?.Invoke());

            _resetProgressBtn.onClick.AddListener(() => OnResetGameProgress?.Invoke());
            
            _musicBtn.onClick.AddListener(() =>
            {
                OnMusicOnOff?.Invoke();
            });
            
            _soundBtn.onClick.AddListener(() =>
            {
                OnSoundOnOff?.Invoke();
            });
        }

        public void SetLanguageIcon(Sprite sprite)
        {
            _flagIcon.sprite = sprite;
        }

        public void SetSoundIcon(Sprite sprite)
        {
            _soundIcon.sprite = sprite;
        }       
        
        public void SetMusicIcon(Sprite sprite)
        {
            _musicIcon.sprite = sprite;
        }
    }
}