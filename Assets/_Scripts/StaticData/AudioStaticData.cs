using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "AudioStaticData", menuName = "Config/Audio Static Data")]
    public class AudioStaticData : ScriptableObject
    {
        [Header("Back Music")]
        [SerializeField] private AudioClip _startBackMusic;
        [SerializeField] private AudioClip[] _levelBackMusic;

        [Header("Sounds")] 
        [SerializeField] private AudioClip _coins;
        [SerializeField] private AudioClip _burst;
        [SerializeField] private AudioClip _teleport;
        
        [Header("UI")] 
        [SerializeField] private AudioClip _btn;
        
        private AudioClip GetRandomClip(AudioClip[] clips)
        {
            return clips[Random.Range(0, clips.Length)];
        }

        public AudioClip GetStartBackAudio => _startBackMusic;
        public AudioClip GetLevelBackAudio => GetRandomClip(_levelBackMusic);
        
        public AudioClip GetCoins => _coins;
        public AudioClip GetBurst => _burst;
        public AudioClip GetUIButton => _btn;
        
        public AudioClip GetTeleport => _teleport;
    }

    public enum AudioClipName
    {
        Btn,
        Coins,
        Burst,
        Teleport
    }
}