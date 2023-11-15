using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerData PlayerData { get; set; }
    }
}