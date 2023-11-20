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
using YG;

public class StartUI : WindowBase, ISavedProgress
{
    [SerializeField] private StartPanelUI _startPanelUI;
    [SerializeField] private SkinPanelUI _skinPanelUI;
    [SerializeField] private SettingsPanelUI _settingsUI;
    [SerializeField] private string link;
    [SerializeField] private TextMeshProUGUI coins;
    private void Awake()
    {
        _startPanelUI.OnNewGameClicked += LoadNewLevel;
        _startPanelUI.OnSkinClicked += LoadSkinPanel;
        _startPanelUI.OnContinueClicked += ContinueLevel;
        _startPanelUI.OnSettingsCkicked += OpenSettings;
        _startPanelUI.OnOurGamesClicked += OurGamesLink;
        _skinPanelUI.OnBackStart += BackStartUI;
        _skinPanelUI.OnBuyNewSkin += ByuSkin;
        
        _settingsUI.OnBackClicked += ()=>
        {
            OnClickedPlay(AudioClipName.Btn);
            _saveLoadService.SaveProgress();
            BackStartUI();
        };

        _settingsUI.OnResetGameProgress += () =>
        {
            OnClickedPlay(AudioClipName.Btn);
            _saveLoadService.ResetProgress();
            UpdateProgress(PlayerData);
            _skinPanelUI.ClickedSkinItem(PlayerData.playerSkin);
            TrigerSend("Reset Clicked", "Reset");
        };

        _settingsUI.OnChangeLanguage += () =>
        {
            OnClickedPlay(AudioClipName.Btn);

            PlayerData.langIndex++;
            
            if (PlayerData.langIndex >= _playerStaticData.GetLocals.Length)
            {
                PlayerData.langIndex = 0;
            }
            
            _settingsUI.SetLanguageIcon(_playerStaticData.GetLocals[PlayerData.langIndex].langSprite);
            string lang = _playerStaticData.GetLocals[PlayerData.langIndex].langCode;
            YandexGame.SwitchLanguage(lang);
            
            //_saveLoadService.SaveProgress();
        };

        _settingsUI.OnMusicOnOff += (bool isOn) =>
        {
            PlayerData.isMusicOn = isOn;
            OnClickedPlay(AudioClipName.Btn);

            _audioService.OnOffBackMusic(isOn);
        };
        
        _settingsUI.OnSoundOnOff += (bool isOn) =>
        {
            PlayerData.isSoundOn = isOn;
            OnClickedPlay(AudioClipName.Btn);
        };

        BackStartUI();
    }

    private void ByuSkin(int skinIndex)
    {
        TrigerSend("Skins","ByuSkin: " + skinIndex);
    }

    protected override void Initialize(bool isMobile)
    {
        base.Initialize(isMobile);
        _skinPanelUI.Construct(_gameStateMachine, _player, _progressService, _adsService, _audioService);
        _startPanelUI.SetContinueButton(PlayerData.checkpointIndex.Count > 1);
        _audioService.CreateStartAudio();
        _settingsUI.Init(PlayerData.isMusicOn, PlayerData.isSoundOn, 
            _playerStaticData.GetLocals[PlayerData.langIndex].langSprite, _playerStaticData);
    }

    private void OnDestroy()
    {
        _startPanelUI.OnNewGameClicked -= LoadNewLevel;
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

    private void LoadNewLevel()
    {
        OnClickedPlay(AudioClipName.Btn);

        PlayerData.checkpointIndex = new List<int>() { -1 };
        _saveLoadService.SaveProgress();
        TrigerSend("Skins","LoadNewLevel with Skin: " + PlayerData.playerSkin);
        _gameStateMachine.Enter<LoadSceneState, int>(2);
    }

    private void ContinueLevel()
    {
        OnClickedPlay(AudioClipName.Btn);
        
        TrigerSend("Skins","ContinueLevel with Skin: " + PlayerData.playerSkin);

        _gameStateMachine.Enter<LoadSceneState, int>(2);
    }

    private void OurGamesLink()
    {
        string domen = YandexGame.EnvironmentData.domain;
        
        Application.OpenURL(GetLink(domen)); 
        
        OnClickedPlay(AudioClipName.Btn);
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
    private void TrigerSend(string eventName, string name)
    {
        var eventParams = new Dictionary<string, string>
        {
            { eventName, name }
        };

        YandexMetrica.Send(eventName, eventParams);
    }
}