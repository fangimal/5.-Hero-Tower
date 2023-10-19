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
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;
        private StartUI startUI;
        
        public UIFactory(IGameStateMachine stateMachine, IAssets assets, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateUIRoot(int sceneIndex)
        {
            _uiRoot = _assets.Instantiate(AssetPath.UIRootPath).transform;

            if (sceneIndex == 1)
            {
                CreateStartUI();
            }
            else
            {
                CreateGameUI();
            }
            Debug.Log("CreateUIRoot: " + sceneIndex);
        }
        
        public void CreateStartUI()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Start);
            StartUI startUI = Object.Instantiate(config.Prefab, _uiRoot) as StartUI;
            startUI.Construct(_stateMachine);
            //
            // GameObject startUIObj = _assets.Instantiate(AssetPath.StartUIPath);
            // startUIObj.transform.parent = _uiRoot;
            //
            // startUI = startUIObj.GetComponent<StartUI>();
        }

        public void CreateGameUI()
        {
            Debug.Log("GameUI don't created!");
        }
    }
}