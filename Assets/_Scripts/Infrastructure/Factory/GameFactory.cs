using System;
using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.Level;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IAudioService _audioService;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject PlayerGameObject { get; set; }
        private LevelHelper levelHelper;
        public event Action<ThirdPersonController> OnPlayerCreated;

        public GameFactory(IAssetsProvider assets, IStaticDataService staticData, IPersistentProgressService persistentProgressService, IAudioService audioService)
        {
            _assets = assets;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
            _audioService = audioService;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            
            //_assets.Cleanup();
        }

        public void SetLevelHelper(LevelHelper levelHelper)
        {
            this.levelHelper = levelHelper;
        }
        public ThirdPersonController CreatePlayer(LevelStaticData levelData)
        {
            PlayerGameObject = InstantiateRegistered(AssetPath.PlayerPath, levelData.InitialHeroPosition);
            ThirdPersonController player = PlayerGameObject.GetComponent<ThirdPersonController>();
            SetPlayerData(player, levelData);
            return player;
        }
        private GameObject InstantiateRegistered(string prefabPath, Vector3  at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progeressWriter) 
                ProgressWriters.Add(progeressWriter);
            
            ProgressReaders.Add(progressReader);
        }

        private void SetPlayerData(ThirdPersonController player, LevelStaticData data)
        {
            player.MoveSpeed = data.playerMoveSpeed;

            PlayerData playerData = _persistentProgressService.playerData;

            levelHelper.Initialize(player, _persistentProgressService);
            player.playerSpawner.Init(player, levelHelper, _persistentProgressService, data, _audioService);
            player.Init(playerData.playerSkin);

            if (data.levelBuildIndex == 1)
            {
                player.GetComponent<StarterAssetsInputs>().SetCursour(false);
            }
            else
            {
                player.GetComponent<StarterAssetsInputs>().SetCursour(true);
            }
            
        }
    }
}