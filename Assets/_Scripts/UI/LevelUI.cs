using _Scripts.Data;
using _Scripts.Infrastructure.ADS;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.States;
using _Scripts.Level;
using _Scripts.StaticData;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class LevelUI : WindowBase, ISavedProgress
    {
        [SerializeField] private UICanvasControllerInput _canvasController;
        [SerializeField] private MobileDisableAutoSwitchControls _mobileDisableAutoSwitchControls;
        [SerializeField] private Transform[] _mobileInput;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private PausePanelUI _pausePanelUI;
        [SerializeField] private TextMeshProUGUI _coinsCount;
        [SerializeField] private TextMeshProUGUI _pauseTab;

        private StarterAssetsInputs _starterAssetsInputs;
        private PlayerSpawner _playerSpawner;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(() => { OpenPausePanel(true); });

            _pausePanelUI.OnNextPoint += () =>
            {
                OnClickedPlay(AudioClipName.Btn);
                OpenPausePanel(false);
                _adsService.ShowReward(RewardId.Checkpoint);
            };

            _pausePanelUI.OnContinue += () =>
            {
                OnClickedPlay(AudioClipName.Btn);
                OpenPausePanel(false);
                RebasePlayer();
            };

            _pausePanelUI.OnBack += LoadPauseUI;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OpenPausePanel(true);
            }
        }

        private void OnDestroy()
        {
            _adsService.OnNextCheckPoint -= GetRewardGoNextPoint;
        }

        protected override void Initialize(bool isMobile)
        {
            base.Initialize(isMobile);

            _audioService.CreateLevelAudio();
            _starterAssetsInputs = _player.GetComponent<StarterAssetsInputs>();
            OpenPausePanel(false);

            if (isMobile)
            {
                _pauseTab.gameObject.SetActive(false);
                _canvasController.starterAssetsInputs = _starterAssetsInputs;
                _mobileDisableAutoSwitchControls.playerInput = _player.GetComponent<PlayerInput>();
                _mobileDisableAutoSwitchControls.Init();
            }

            ShowMobileInput(isMobile);
            
            _adsService.OnNextCheckPoint += GetRewardGoNextPoint;

            _playerSpawner = _player.GetComponent<PlayerSpawner>();
            _playerSpawner.OnRebasePlayer += PlayerFall;
        }

        private void PlayerFall()
        {
            OpenPausePanel(true);
            _adsService.ShowIterstisial();
        }

        private void GetRewardGoNextPoint()
        {
            _player.playerSpawner.RewardGoNextCheckPoint(
                PlayerData.checkpointIndex[PlayerData.checkpointIndex.Count - 1] + 1);
        }

        private void OpenPausePanel(bool isOpen)
        {
            _pausePanelUI.gameObject.SetActive(isOpen);
            _starterAssetsInputs.SetCursour(!isOpen, !isOpen);
            _pausePanelUI.SetInteractableNextPointButton(PlayerData.checkpointIndex[PlayerData.checkpointIndex.Count - 1] < _player.playerSpawner.GetCheckpointsCount - 1);
        }

        private void RebasePlayer()
        {
            if (_playerSpawner.playerIsFall)
            {
                _playerSpawner.RebaseEnd();
            }
        }

        private void LoadPauseUI()
        {
            OnClickedPlay(AudioClipName.Btn);
            _gameStateMachine.Enter<LoadSceneState, int>(1);
        }


        private void ShowMobileInput(bool show)
        {
            for (int i = 0; i < _mobileInput.Length; i++)
            {
                _mobileInput[i].gameObject.SetActive(show);
            }
        }

        public void LoadProgress(PlayerData playerData)
        {
            _coinsCount.text = playerData.Coins.ToString();
        }

        public void UpdateProgress(PlayerData playerData)
        {
            _coinsCount.text = playerData.Coins.ToString();
        }
    }
}