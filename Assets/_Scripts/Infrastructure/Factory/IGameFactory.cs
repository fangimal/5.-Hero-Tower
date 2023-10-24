using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        GameObject CreatePlayer(LevelStaticData levelData);

        public event Action<ThirdPersonController> OnPlayerCreated;
    }
}