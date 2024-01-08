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
        private PlayerData _playerData => _progressService.playerData;

        private AudioClip _backMusic;
        public AudioService(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _audionConfig = staticData.GetAudioConfig();
            VKProvider.Instance.OnPause += () => OnOffBackMusic(false);
            VKProvider.Instance.OnUnPause += () => OnOffBackMusic(true);
        }
        
        public void Cleanup()
        {
            _audioContainer = null;
            _audios = null;
            _backMusic = null;
        }

        public void CreateStartAudio()
        {
            if (_playerData.isMusicOn)
            {
                _backMusic = _audionConfig.GetStartBackAudio;
                GetAudio(_backMusic, true).Play();
            }
        }

        public void CreateLevelAudio()
        {
            if (_playerData.isMusicOn)
            {
                _backMusic = _audionConfig.GetLevelBackAudio;
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
                        GetAudio(_audionConfig.GetUIButton).Play();
                        break;
                    case AudioClipName.Coins:
                        GetAudio(_audionConfig.GetCoins).Play();
                        break;
                    case AudioClipName.Burst:
                        GetAudio(_audionConfig.GetBurst).Play();
                        break;               
                    case AudioClipName.Teleport:
                        GetAudio(_audionConfig.GetTeleport).Play();
                        break;
                    default:
                        Debug.Log("Don't find Audio Clip");
                        break;
                }
            }
        }

        public void OnOffBackMusic(bool isOn, bool isLevel = false)
        {
            if (_backMusic == null)
            {
                _backMusic = isLevel? _audionConfig.GetLevelBackAudio : _audionConfig.GetStartBackAudio;
                GetAudio(_backMusic, true);
            }
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