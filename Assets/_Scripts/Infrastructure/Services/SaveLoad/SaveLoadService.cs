using _Scripts.Data;
using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
        {
            _persistentProgressService = persistentProgressService;
            _gameFactory = gameFactory;
        }
        public void SaveProgress()
        {
            
        }

        public DataGroup LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialzed<DataGroup>();
    }
}