using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Factory.UIFactory;
using _Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }
        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.playerData);
            
            foreach (ISavedProgress progressWriter in _uiFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.playerData);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.playerData.ToJson());
        }

        public PlayerData LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialzed<PlayerData>();

        public void ResetProgress()
        {
            _progressService.playerData = new(); 
            
            PlayerPrefs.SetString(ProgressKey, _progressService.playerData.ToJson());
        }
    }
}