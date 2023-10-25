using _Scripts.Infrastructure.Services;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public interface IUIFactory: IService
    {
        void CreateUI(int index, ThirdPersonController player);
        void CreateStartUI(ThirdPersonController player);
        void CreateGameUI(ThirdPersonController player);
    }
}