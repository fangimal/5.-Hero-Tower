using System;
using _Scripts.Infrastructure.AssetManagment;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private GameObject PlayerGameObject { get; set; }
        private LevelHelper levelHelper;
        public LevelHelper GetLvlHelper => levelHelper;
        public event Action<ThirdPersonController> OnPlayerCreated;
        public GameFactory(IAssetsProvider assetsProvider, IStaticDataService staticData)
        {
            _assetsProvider = assetsProvider;
            _staticData = staticData;
        }

        public void Clenup()
        {
            
        }

        public GameObject CreatePlayer(LevelStaticData levelData)
        {
            PlayerGameObject = InstantiateRegistered(AssetPath.PlayerPath, levelData.InitialHeroPosition);
            SetPlayerData(PlayerGameObject, levelData);
            OnPlayerCreated?.Invoke(PlayerGameObject.GetComponent<ThirdPersonController>());
            Debug.Log("CreatePlayer");
            return PlayerGameObject;
        }
        private GameObject InstantiateRegistered(string prefabPath, Vector3  at)
        {
            GameObject gameObject = _assetsProvider.Instantiate(prefabPath, at);
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
        }
    }
}