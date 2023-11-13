using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using _Scripts.Data;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.States;
using _Scripts.UI;
using TMPro;
using UnityEngine;

public class StartUI : WindowBase, ISavedProgress
{
    [SerializeField] private StartPanelUI _startPanelUI;
    [SerializeField] private SkinPanelUI _skinPanelUI;
    [SerializeField] private string link;
    [SerializeField] private TextMeshProUGUI coins;

    private void Awake()
    {
        _startPanelUI.OnNewGameClicked += LodNewLevel;
        _startPanelUI.OnSkinClicked += LoadSkinPanel;
        _startPanelUI.OnContinueClicked += ContinueLevel;
        _startPanelUI.OnOurGamesClicked += OurGamesLink;
        _skinPanelUI.OnBackStart += BackStartUI;
        BackStartUI();
    }

    protected override void Initialize(bool isMobile)
    {
        base.Initialize(isMobile);
        _skinPanelUI.Construct(gameStateMachine, player, ProgressService, _adsService);
        _startPanelUI.SetContinueButton(PlayerData.checkpointIndex.Count > 1);
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

    private void OurGamesLink()
    {
        string domen = "ru"; //TODO get domen
        
        Application.OpenURL(GetLink(domen)); 
    }
    
    private string GetLink(string domen)
    {
        if (string.IsNullOrEmpty(link)) link = "https://yandex.ru/games/developer?name=FanG";
        
        string result = link;
        
        if (string.IsNullOrEmpty(domen))
        {
            domen = "ru";
        }

        // Поиск подстроки между символами "." и "/"
        string pattern = @"\.(.*?)\/";
        Match match = Regex.Match(link, pattern);

        string extractedSubstring = "ru";

        if (match.Success && match.Groups.Count >= 2)
        {
            extractedSubstring = match.Groups[1].Value; // Извлеченная подстрока: " + extractedSubstring
        }
        
        // Замена ".ru" на указанный домен
        string modifiedInput = link.Replace(extractedSubstring, domen);

        // Разделение на составляющие
        string[] parts = modifiedInput.Split(new string[] { domen }, StringSplitOptions.None);

        if (parts.Length == 2)
        {
            string firstPart = parts[0] + domen;
            string secondPart = parts[1];
            
            Debug.Log(firstPart+secondPart);
            result = firstPart + secondPart;
        }
        else
        {
            Debug.LogWarning("Строка не разделилась корректно.");
        }

        return result; 
    }

    public void LoadProgress(DataGroup dataGroup)
    {
        coins.text = dataGroup.playerData.Coins.ToString();
    }

    public void UpdateProgress(DataGroup dataGroup)
    {
        coins.text = dataGroup.playerData.Coins.ToString();
    }
}
