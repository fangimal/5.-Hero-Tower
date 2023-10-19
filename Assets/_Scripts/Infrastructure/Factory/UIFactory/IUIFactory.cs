using _Scripts.Infrastructure.Services;

namespace _Scripts.Infrastructure.Factory.UIFactory
{
    public interface IUIFactory: IService
    {
        void CreateUIRoot(int index);
        void CreateStartUI();
        void CreateGameUI();
    }
}