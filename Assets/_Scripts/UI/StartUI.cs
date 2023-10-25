using System.Collections.Generic;
using _Scripts.Infrastructure.States;
using _Scripts.UI;
using UnityEngine;

public class StartUI : WindowBase
{
    [SerializeField] private StartPanelUI _startPanelUI;
    [SerializeField] private SkinPanelUI _skinPanelUI;

    private void Awake()
    {
        _startPanelUI.OnNewGameClicked += LodNewLevel;
        _startPanelUI.OnSkinClicked += LoadSkinPanel;
        _startPanelUI.OnContinueClicked += ContinueLevel;
        _skinPanelUI.OnBackStart += BackStartUI;
        BackStartUI();
    }

    protected override void Initialize(bool isMobile)
    {
        base.Initialize(isMobile);
        _skinPanelUI.Construct(gameStateMachine, player, ProgressService);
    }

    private void OnDestroy()
    {
        _startPanelUI.OnNewGameClicked -= LodNewLevel;
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
        _skinPanelUI.Open();
    }

    private void LodNewLevel()
    {
        PlayerData.checkpointIndex = new List<int>(){-1};
        _saveLoadService.SaveProgress();
        gameStateMachine.Enter<LoadSceneState, int>(2);
    }

    private void ContinueLevel()
    {
        gameStateMachine.Enter<LoadSceneState, int>(2);
    }
}
