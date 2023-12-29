using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace _Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        public int sceneIndex = 1;

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            
            Debug.Log("LoadProgressState");
            VKProvider.Instance.OnLoadData += () =>
            {
                _progressService.playerData = VKProvider.Instance.DG.playerData;
                
                _gameStateMachine.Enter<LoadSceneState, int>(sceneIndex);
                Debug.Log("LoadSceneState");
            };
        }

        public void Enter()
        {
            Debug.Log("LoadProgressState");
            VKProvider.Instance.LoadWEBData();
            
            //LoadProgressOrInitNew();
            
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.playerData =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }

        private PlayerData  NewProgress()
        {
            var progress = new PlayerData();

            /*progress.playerSkin = 0;
            progress.checkpointIndex = new List<int>(){-1};*/
             
            return progress;
        }
    }
}