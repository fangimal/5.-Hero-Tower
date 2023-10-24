using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(DataGroup dataGroup);
    }
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(DataGroup dataGroup);
    }
}