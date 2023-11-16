using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<int>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly IAudioService _audioService;
        
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private ThirdPersonController player;
        
        private int currentSceneIndex;
        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
            IUIFactory uiFactory, IStaticDataService staticData, IGameFactory gameFactory, IPersistentProgressService progressService, IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _staticData = staticData;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _audioService = audioService;
        }

        public void Enter(int sceneIndex)
        {
            currentSceneIndex = sceneIndex;
            
            _curtain.Show();
            
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
            _audioService.Cleanup();

            _sceneLoader.Load(sceneIndex, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitPlayer();
            
            InitUI();

            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _uiFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.DataGroup);
        }
        private void InitPlayer()
        {
            var levelData = GetLevelStaticData();

            player = _gameFactory.CreatePlayer(levelData);
        }

        private LevelStaticData GetLevelStaticData() => 
            _staticData.ForLevel(SceneManager.GetActiveScene().buildIndex);

        private void InitUI() => 
            _uiFactory.CreateUI(currentSceneIndex, player);
    }
}