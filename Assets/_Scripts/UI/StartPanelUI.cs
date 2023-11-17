using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class StartPanelUI : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButtone;
        [SerializeField] private Button skinsButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button ourGamesButton;
        public event Action OnNewGameClicked;
        public event Action OnSkinClicked;
        public event Action OnContinueClicked;
        public event Action OnOurGamesClicked;
        public event Action OnSettingsCkicked;

        private void Awake()
        {
            newGameButton.onClick.AddListener(()=>
            {
                OnNewGameClicked?.Invoke();
            });
            
            skinsButton.onClick.AddListener(() =>
            {
                OnSkinClicked?.Invoke();
            });
            
            continueButtone.onClick.AddListener(() =>
            {
                OnContinueClicked?.Invoke();
            });
            
            ourGamesButton.onClick.AddListener(() =>
            {
                OnOurGamesClicked?.Invoke();
            });
            
            settingsButton.onClick.AddListener(() =>
            {
                OnSettingsCkicked?.Invoke();
            });
        }

        public void SetContinueButton(bool isActive)
        {
            continueButtone.interactable = isActive;
        }
    }
}