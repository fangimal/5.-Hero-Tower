using System.Collections.Generic;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public interface IUIFactory: IService
    {
        void CreateUI(int index, ThirdPersonController player);
        void CreateStartUI(ThirdPersonController player);
        void CreateGameUI(ThirdPersonController player);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
    }
}