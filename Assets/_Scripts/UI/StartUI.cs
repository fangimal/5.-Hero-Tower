using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using _Scripts.Data;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.States;
using _Scripts.StaticData;
using _Scripts.UI;
using TMPro;
using UnityEngine;

public class StartUI : WindowBase, ISavedProgress
{
    [SerializeField] private StartPanelUI _startPanelUI;
    [SerializeField] private SkinPanelUI _skinPanelUI;
    [SerializeField] private SettingsPanelUI _settingsUI;
    [SerializeField] private string link;
    [SerializeField] private TextMeshProUGUI coins;
    
    private void Awake()
    {
        _startPanelUI.OnNewGameClicked += LodNewLevel;
        _startPanelUI.OnSkinClicked += LoadSkinPanel;
        _startPanelUI.OnContinueClicked += ContinueLevel;
        _startPanelUI.OnSettingsCkicked += OpenSettings;
        _startPanelUI.OnOurGamesClicked += OurGamesLink;
        _skinPanelUI.OnBackStart += BackStartUI;
        
        _settingsUI.OnBackClicked += ()=>
        {
            OnClickedPlay(AudioClipName.Btn);
            BackStartUI();
        };

        _settingsUI.OnResetGameProgress += () =>
        {
            _saveLoadService.ResetProgress();
            UpdateProgress(PlayerData);
            _skinPanelUI.ClickedSkinItem(PlayerData.playerSkin);
        };

        _settingsUI.OnChangeLanguage += () =>
        {
            OnClickedPlay(AudioClipName.Btn);

            PlayerData.langIndex++;
            
            if (PlayerData.langIndex >= _playerStaticData.GetLanguageSprites.Length)
            {
                PlayerData.langIndex = 0;
            }
            
            _settingsUI.SetLanguageIcon(_playerStaticData.GetLanguageSprites[PlayerData.langIndex]);
            
            _saveLoadService.SaveProgress();
        };

        BackStartUI();
    }

    protected override void Initialize(bool isMobile)
    {
        base.Initialize(isMobile);
        _skinPanelUI.Construct(_gameStateMachine, _player, _progressService, _adsService, _audioService);
        _startPanelUI.SetContinueButton(PlayerData.checkpointIndex.Count > 1);
        _audioService.CreateStartAudio();
    }

    private void OnDestroy()
    {
        _startPanelUI.OnNewGameClicked -= LodNewLevel;
        _startPanelUI.OnSkinClicked -= LoadSkinPanel;
        _skinPanelUI.OnBackStart -= BackStartUI;
    }

    private void OpenSettings()
    {
        OnClickedPlay(AudioClipName.Btn);
        _startPanelUI.gameObject.SetActive(false);
        _settingsUI.gameObject.SetActive(true);
    }

    private void BackStartUI()
    {
        _startPanelUI.gameObject.SetActive(true);
        _skinPanelUI.gameObject.SetActive(false);
        _settingsUI.gameObject.SetActive(false);
    }

    private void LoadSkinPanel()
    {
        OnClickedPlay(AudioClipName.Btn);

        _startPanelUI.gameObject.SetActive(false);
        _settingsUI.gameObject.SetActive(false);
        _skinPanelUI.gameObject.SetActive(true);
        _skinPanelUI.Open();
    }

    private void LodNewLevel()
    {
        OnClickedPlay(AudioClipName.Btn);

        PlayerData.checkpointIndex = new List<int>() { -1 };
        _saveLoadService.SaveProgress();
        _gameStateMachine.Enter<LoadSceneState, int>(2);
    }

    private void ContinueLevel()
    {
        OnClickedPlay(AudioClipName.Btn);

        _gameStateMachine.Enter<LoadSceneState, int>(2);
    }

    private void OurGamesLink()
    {
        OnClickedPlay(AudioClipName.Btn);

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

            Debug.Log(firstPart + secondPart);
            result = firstPart + secondPart;
        }
        else
        {
            Debug.LogWarning("Строка не разделилась корректно.");
        }

        return result;
    }

    public void LoadProgress(PlayerData playerData)
    {
        coins.text = playerData.Coins.ToString();
    }

    public void UpdateProgress(PlayerData playerData)
    {
        coins.text = playerData.Coins.ToString();
    }
}