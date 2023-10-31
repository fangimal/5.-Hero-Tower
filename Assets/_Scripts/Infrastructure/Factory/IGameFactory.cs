using System;
using System.Collections.Generic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Level;
using _Scripts.StaticData;
using StarterAssets;

namespace _Scripts.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        ThirdPersonController CreatePlayer(LevelStaticData levelData);
        void SetLevelHelper(LevelHelper levelHelper);
    }
}