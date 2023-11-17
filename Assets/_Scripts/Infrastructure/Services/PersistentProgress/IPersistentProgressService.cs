using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerData playerData { get; set; }
    }
}