using _Scripts.Infrastructure.Services;
using _Scripts.StaticData;

namespace _Scripts.Infrastructure.Audio
{
    public interface IAudioService : IService
    {
        void Init(int levelIndex, AudioYB sound);
        void CreateStartAudio();
        void CreateLevelAudio();
        void PlayAudio(AudioClipName audioType);
        void OnOffBackMusic(bool isOn, bool isLevel = false);
        void Cleanup();
    }
}