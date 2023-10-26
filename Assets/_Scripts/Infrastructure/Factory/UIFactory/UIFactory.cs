using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using _Scripts.StaticData.Windows;
using _Scripts.UI;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;
        private StartUI startUI;

        public UIFactory(IGameStateMachine stateMachine, IAssetsProvider assetsProvider, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateUI(int sceneIndex, ThirdPersonController player)
        {
            
            if (sceneIndex == 1)
            {
                _uiRoot = _assetsProvider.Instantiate(AssetPath.UIRootPath).transform;
                CreateStartUI(player);
            }
            else
            {
                CreateGameUI(player);
            }
        }
        

        public void CreateStartUI(ThirdPersonController player)
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Start);
            startUI = Object.Instantiate(config.Prefab, _uiRoot) as StartUI;
            startUI.Construct(_stateMachine, player, _progressService);
        }

        public void CreateGameUI(ThirdPersonController player)
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Level);
            LevelUI levelUI = Object.Instantiate(config.Prefab, _uiRoot) as LevelUI;
            levelUI.Construct(_stateMachine, player, _progressService, true);
        }
    }
}