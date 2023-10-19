using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<int>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        
        private int currentSceneIndex;
        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
        }

        public void Enter(int sceneIndex)
        {
            currentSceneIndex = sceneIndex;
            
            _curtain.Show();

            _sceneLoader.Load(sceneIndex, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitUI();
            
            InitStartWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitStartWorld()
        {
            Debug.Log("World don't Init!");
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot(currentSceneIndex);
        }
    }
}