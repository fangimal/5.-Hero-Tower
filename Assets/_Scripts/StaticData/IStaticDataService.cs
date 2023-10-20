using _Scripts.Infrastructure.Services;
using _Scripts.StaticData.Windows;
using _Scripts.UI;

namespace _Scripts.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadDatas();
        WindowConfig ForWindow(WindowId windowId);
        LevelStaticData ForLevel(int sceneBuildIndex);
    }
}