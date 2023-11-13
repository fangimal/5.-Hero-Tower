﻿using System;
using _Scripts.Data;
using _Scripts.Infrastructure.ADS;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.States;
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

        private StarterAssetsInputs _starterAssetsInputs;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(() => { OpenPausePanel(true); });

            _pausePanelUI.OnNextPoint += () =>
            {
                OpenPausePanel(false);
                _adsService.ShowReward(RewardId.Checkpoint);
            };

            _pausePanelUI.OnContinue += () => { OpenPausePanel(false); };

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

            _starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
            OpenPausePanel(false);

            if (isMobile)
            {
                _canvasController.starterAssetsInputs = _starterAssetsInputs;
                _mobileDisableAutoSwitchControls.playerInput = player.GetComponent<PlayerInput>();
                _mobileDisableAutoSwitchControls.Init();
            }

            ShowMobileInput(isMobile);
            
            _adsService.OnNextCheckPoint += GetRewardGoNextPoint;
        }

        private void GetRewardGoNextPoint()
        {
            player.playerSpawner.SetNextCheckPointAndRebase(
                PlayerData.checkpointIndex[PlayerData.checkpointIndex.Count - 1] + 1);
        }

        private void OpenPausePanel(bool isOpen)
        {
            _pausePanelUI.gameObject.SetActive(isOpen);
            _starterAssetsInputs.SetCursour(!isOpen);
            _pausePanelUI.SetInteractableNextPointButton(PlayerData.checkpointIndex[PlayerData.checkpointIndex.Count - 1] < player.playerSpawner.GetCheckpointsCount - 1);
        }


        private void LoadPauseUI()
        {
            //TODO CreatePause UI
            gameStateMachine.Enter<LoadSceneState, int>(1);
        }


        private void ShowMobileInput(bool show)
        {
            for (int i = 0; i < _mobileInput.Length; i++)
            {
                _mobileInput[i].gameObject.SetActive(show);
            }
        }

        public void LoadProgress(DataGroup dataGroup)
        {
            _coinsCount.text = dataGroup.playerData.Coins.ToString();
        }

        public void UpdateProgress(DataGroup dataGroup)
        {
            _coinsCount.text = dataGroup.playerData.Coins.ToString();
        }
    }
}