using System;
using _Scripts.StaticData;
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
        public event Action OnResetGameProgress;
        public event Action OnChangeLanguage;
        public event Action<bool> OnSoundOnOff;
        public event Action<bool> OnMusicOnOff;

        private PlayerStaticData _playerStaticData;
        private bool isMusicOn;
        private bool isSoundOn;

        private void Awake()
        {
            _backBtn.onClick.AddListener(() => OnBackClicked?.Invoke());

            _langBtn.onClick.AddListener(() => OnChangeLanguage?.Invoke());

            _resetProgressBtn.onClick.AddListener(() => OnResetGameProgress?.Invoke());

            _musicBtn.onClick.AddListener(() =>
            {
                isMusicOn = !isMusicOn;
                SetMusicIcon();
                OnMusicOnOff?.Invoke(isMusicOn);
            });

            _soundBtn.onClick.AddListener(() =>
            {
                isSoundOn = !isSoundOn;
                SetSoundIcon();
                OnSoundOnOff?.Invoke(isSoundOn);
            });
        }

        public void Init(bool isMusicOn, bool isSoundOn, Sprite flagIcon, PlayerStaticData playerStaticData)
        {
            _playerStaticData = playerStaticData;
            this.isMusicOn = isMusicOn;
            this.isSoundOn = isSoundOn;
            SetMusicIcon();
            SetSoundIcon();
            SetLanguageIcon(flagIcon);
        }

        public void SetLanguageIcon(Sprite sprite)
        {
            _flagIcon.sprite = sprite;
        }

        private void SetSoundIcon()
        {
            _soundIcon.sprite = isSoundOn ? _playerStaticData.GetOnIcon : _playerStaticData.GetOffIcon;
        }

        private void SetMusicIcon()
        {
            _musicIcon.sprite = isMusicOn ? _playerStaticData.GetOnIcon : _playerStaticData.GetOffIcon;
        }
    }
}