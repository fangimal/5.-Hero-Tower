using System.Collections.Generic;
using _Scripts.Infrastructure.ADS;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using _Scripts.StaticData.Windows;
using _Scripts.UI;
using StarterAssets;
using UnityEngine;
using YG;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAdsService _adsService;
        private readonly IAudioService _audioService;
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private Transform _uiRoot;
        private StartUI startUI;

        public UIFactory(IGameStateMachine stateMachine, 
            IAssetsProvider assetsProvider, IStaticDataService staticData, 
            IPersistentProgressService progressService, IAdsService adsService, IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
            _audioService = audioService;
        }

        public void CreateUI(int sceneIndex, ThirdPersonController player)
        {
            YandexGame.lang = player.PlayerStaticData.GetLocals[_progressService.playerData.langIndex].langCode;
            
            if (sceneIndex == 1)
            {
                _uiRoot = _assetsProvider.Instantiate(AssetPath.UIRootPath).transform;
                _audioService.Init(sceneIndex, player.SoundAudio);
                CreateStartUI(player);
            }
            else
            {
                _audioService.Init(sceneIndex, player.SoundAudio);
                CreateGameUI(player);
            }
            
            _adsService.Initialize(player.GetComponent<StarterAssetsInputs>());
        }

        public void CreateStartUI(ThirdPersonController player)
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Start);
            StartUI startUI = Object.Instantiate(config.Prefab, _uiRoot) as StartUI;
            startUI.Construct(_stateMachine, player, _progressService, _adsService, _audioService);
            Register(startUI);
        }

        public void CreateGameUI(ThirdPersonController player)
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Level);
            LevelUI levelUI = Object.Instantiate(config.Prefab, _uiRoot) as LevelUI;
            bool isMobile = YandexGame.EnvironmentData.deviceType != "desktop";
            levelUI.Construct(_stateMachine, player, _progressService, _adsService, _audioService, isMobile);
            Register(levelUI);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progeressWriter) 
                ProgressWriters.Add(progeressWriter);
            
            ProgressReaders.Add(progressReader);
        }
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}