using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<int>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private GameObject player;
        
        private int currentSceneIndex;
        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, 
            IUIFactory uiFactory, IStaticDataService staticData, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _staticData = staticData;
            _gameFactory = gameFactory;
        }

        public void Enter(int sceneIndex)
        {
            currentSceneIndex = sceneIndex;
            
            _curtain.Show();
            
            _gameFactory.Cleanup();

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

            _stateMachine.Enter<GameLoopState>();
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