using _Scripts.Infrastructure.States;
using _Scripts.UI;
using UnityEngine;

public class StartUI : WindowBase
{
    [SerializeField] private StartPanelUI _startPanelUI;
    [SerializeField] private SkinPanelUI _skinPanelUI;

    private void Awake()
    {
        BackStartUI();
        
        _startPanelUI.OnNewGameClicked += LodLevel;
        _startPanelUI.OnSkinClicked += LoadSkinPanel;

        _skinPanelUI.OnBackStart += BackStartUI;
    }

    private void OnDestroy()
    {
        _startPanelUI.OnNewGameClicked -= LodLevel;
        _startPanelUI.OnSkinClicked -= LoadSkinPanel;
        _skinPanelUI.OnBackStart -= BackStartUI;
    }

    private void BackStartUI()
    {
        _startPanelUI.gameObject.SetActive(true);
        _skinPanelUI.gameObject.SetActive(false);
    }

    private void LoadSkinPanel()
    {
        _startPanelUI.gameObject.SetActive(false);
        _skinPanelUI.gameObject.SetActive(true);
        
        _skinPanelUI.Init(player);
    }

    private void LodLevel()
    {
        gameStateMachine.Enter<LoadSceneState, int>(2);
    }
}
