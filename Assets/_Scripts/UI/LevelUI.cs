using _Scripts.Infrastructure.States;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class LevelUI: WindowBase
    {
        [SerializeField] private UICanvasControllerInput _canvasController;
        [SerializeField] private MobileDisableAutoSwitchControls _mobileDisableAutoSwitchControls;

        [SerializeField] private Button pauseButton;
        private void Awake()
        {
            pauseButton.onClick.AddListener(LoadPauseUI);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                LoadPauseUI();
            }
        }

        protected override void Initialize(bool isMobile)
        {
            base.Initialize(isMobile);
            
            if (isMobile)
            {
                _canvasController.starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
                _mobileDisableAutoSwitchControls.playerInput = player.GetComponent<PlayerInput>();
                _mobileDisableAutoSwitchControls.Init();
            }
            
            Debug.Log("Initialize Input");
        }

        private void LoadPauseUI()
        {
            //TODO CreatePause UI
            gameStateMachine.Enter<LoadSceneState, int>(1);
            
        }
        
        private void OnDestroy()
        {
            pauseButton.onClick.RemoveAllListeners();
        }
        
    }
}