using _Scripts.Infrastructure.ADS;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
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
            RegisterAdsService();
            
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IAudioService>(new AudioService(_services.Single<IStaticDataService>(),
                _services.Single<IPersistentProgressService>()));
            
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _stateMachine,_services.Single<IAssetsProvider>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IPersistentProgressService>(),
                _services.Single<IAdsService>(), _services.Single<IAudioService>()));
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetsProvider>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IPersistentProgressService>(), _services.Single<IAudioService>()));
            
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), 
                _services.Single<IGameFactory>(), _services.Single<IUIFactory>()));
            
        }

        private void RegisterAdsService()
        {
            var adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
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