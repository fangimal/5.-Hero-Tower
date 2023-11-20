using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.Infrastructure.Audio
{
    public class AudioService : IAudioService
    {
        private GameObject _audioContainer;
        
        private AudioStaticData _audionConfig;
        
        private IPersistentProgressService _progressService;
        
        private Dictionary<string, AudioSource> _audios;
        
        private AudioClip _backMusic;
        private PlayerData _playerData => _progressService.playerData;

        private AudioYB _sound;

        private const string StartBackMusicKey = "StartBack";
        private const string LevelBackMusicKey_1 = "LevelBack_1";
        private const string LevelBackMusicKey_2 = "LevelBack_2";
        private const string BurstSoundKey = "burst";
        private const string TeleportSoundKey = "teleport";
        private const string CoinsSoundKey = "coins";
        private const string UIBtnSoundKey = "btn";
        private int levelIndex;

        public AudioService(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _audionConfig = staticData.GetAudioConfig();
        }

        public void Init(int index, AudioYB sound)
        {
            levelIndex = index;

            _sound = sound;
        }

        public void Cleanup()
        {
            _audioContainer = null;
            _audios = null;
            _backMusic = null;
        }

        public void CreateStartAudio()
        {
            _backMusic = _audionConfig.GetStartBackAudio;
            
            if (_playerData.isMusicOn)
            {
                GetAudio(_backMusic, true).Play();
            }
        }

        public void CreateLevelAudio()
        {
            _backMusic = _audionConfig.GetLevelBackAudio;
            
            if (_playerData.isMusicOn)
            {
                GetAudio(_backMusic, true).Play();
            }
        }

        public void PlayAudio(AudioClipName audioType)
        {
            if (_playerData.isSoundOn)
            {
                switch (audioType)
                {
                    case AudioClipName.Btn:
                        //GetAudio(_audionConfig.GetUIButton).Play();
                        _sound.Play(UIBtnSoundKey);
                        break;
                    case AudioClipName.Coins:
                        //GetAudio(_audionConfig.GetCoins).Play();
                        _sound.Play(CoinsSoundKey);
                        break;
                    case AudioClipName.Burst:
                        //GetAudio(_audionConfig.GetBurst).Play();
                        _sound.Play(BurstSoundKey);
                        break;               
                    case AudioClipName.Teleport:
                        //GetAudio(_audionConfig.GetTeleport).Play();
                        _sound.Play(TeleportSoundKey);
                        break;
                    default:
                        Debug.Log("Don't find Audio Clip");
                        break;
                }
            }
        }

        public void OnOffBackMusic(bool isOn, bool isLevel = false)
        {
            if (!isOn)
            {
                GetAudio(_backMusic).Stop();
            }
            else
            {
                if (!GetAudio(_backMusic).isPlaying)
                {
                    GetAudio(_backMusic,true).Play();
                }
            }
        }

        private void InitAudio()
        {
            if (_audioContainer == null || _audios == null)
            {
                _audioContainer = new GameObject("AudioContainer");
        
                _audios = new();
            }
        }

        private void SetupClip(AudioClip clip, bool loop = false)
        {
            AudioSource audioSource = _audioContainer.AddComponent<AudioSource>();
            audioSource.loop = loop;
            audioSource.playOnAwake = false;
            audioSource.clip = clip;
            _audios.Add(clip.name, audioSource);
        }

        private AudioSource GetAudio(AudioClip audioClip, bool loop = false)
        {
            InitAudio();
        
            if (!_audios.ContainsKey(audioClip.name))
            {
                SetupClip(audioClip, loop);
            }
        
            return _audios[audioClip.name];
        }
    }
}