using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;
using _Scripts.StaticData.Windows;
using _Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;
        private StartUI startUI;
        
        public UIFactory(IGameStateMachine stateMachine, IAssetsProvider assetsProvider, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
        }

        public void CreateUIRoot(int sceneIndex)
        {
            _uiRoot = _assetsProvider.Instantiate(AssetPath.UIRootPath).transform;

            if (sceneIndex == 1)
            {
                CreateStartUI();
            }
            else
            {
                CreateGameUI();
            }
        }
        
        public void CreateStartUI()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Start);
            StartUI startUI = Object.Instantiate(config.Prefab, _uiRoot) as StartUI;
            startUI.Construct(_stateMachine);
        }

        public void CreateGameUI()
        {
            Debug.Log("GameUI don't created!");
        }
    }
}