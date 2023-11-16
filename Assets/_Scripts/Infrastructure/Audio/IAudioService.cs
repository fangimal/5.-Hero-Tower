using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;

namespace _Scripts.Infrastructure.Audio
{
    public interface IAudioService : IService
    {
        void CreateStartAudio();
        void CreateLevelAudio();
        void PlayAudio(AudioClipName audioType);

        void Cleanup();
    }
}