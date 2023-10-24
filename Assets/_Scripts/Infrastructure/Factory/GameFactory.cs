using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.Services.PersistentProgress;
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
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject PlayerGameObject { get; set; }
        private LevelHelper levelHelper;
        public LevelHelper GetLvlHelper => levelHelper;
        public event Action<ThirdPersonController> OnPlayerCreated;

        public GameFactory(IAssetsProvider assets, IStaticDataService staticData, IPersistentProgressService persistentProgressService)
        {
            _assets = assets;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            
            //_assets.Cleanup();
        }

        public GameObject CreatePlayer(LevelStaticData levelData)
        {
            PlayerGameObject = InstantiateRegistered(AssetPath.PlayerPath, levelData.InitialHeroPosition);
            SetPlayerData(PlayerGameObject, levelData);
            OnPlayerCreated?.Invoke(PlayerGameObject.GetComponent<ThirdPersonController>());
            return PlayerGameObject;
        }
        private GameObject InstantiateRegistered(string prefabPath, Vector3  at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            //TODO Register ISavedProgressReader item
            Register();
        }

        private void Register()
        {
            //TODO Register
        }

        private void SetPlayerData(GameObject player, LevelStaticData data)
        {
            player.GetComponent<ThirdPersonController>().MoveSpeed = data.playerMoveSpeed;

            if (data.levelBuildIndex == 1)
            {
                player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            }
            else
            {
                player.GetComponent<StarterAssetsInputs>().cursorLocked = true;
            }
        }
    }
}