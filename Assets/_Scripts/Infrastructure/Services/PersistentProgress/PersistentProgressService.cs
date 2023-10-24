using _Scripts.Data;

namespace _Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public DataGroup DataGroup { get; set; }
    }
}