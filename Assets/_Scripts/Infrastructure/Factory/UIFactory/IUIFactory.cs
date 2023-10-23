using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public interface IUIFactory: IService
    {
        void CreateUI(int index, GameObject player);
        void CreateStartUI(GameObject player);
        void CreateGameUI(GameObject player);
    }
}