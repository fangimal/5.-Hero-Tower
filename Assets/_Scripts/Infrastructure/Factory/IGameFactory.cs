using System;
using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        void Clenup();
        GameObject CreatePlayer(LevelStaticData levelData);

        public event Action<ThirdPersonController> OnPlayerCreated;
    }
}