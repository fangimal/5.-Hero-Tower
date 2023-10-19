using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;

namespace _Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const int Initial = 0;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadStart);
        }

        public void Exit()
        {
                    
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IAssets>(new AssetsProvider());
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _stateMachine,_services.Single<IAssets>(), _services.Single<IStaticDataService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadDatas();
            _services.RegisterSingle(staticData);
        }

        private void EnterLoadStart()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}