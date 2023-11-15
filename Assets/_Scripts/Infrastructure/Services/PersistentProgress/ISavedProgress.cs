using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerData playerData);
    }
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerData playerData);
    }
}