using _Scripts.Infrastructure.Services;

namespace _Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        public int sceneIndex = 1;

        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadSceneState, int>(sceneIndex);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
        }
    }
}