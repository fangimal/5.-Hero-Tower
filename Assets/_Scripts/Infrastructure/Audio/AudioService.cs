using System.Collections.Generic;
using _Scripts.Data;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.Infrastructure.Audio
{
    public class AudioService : IAudioService
    {
        private GameObject audioContainer;

        private AudioStaticData _audionConfig;
        
        private IPersistentProgressService _progressService;
        
        private Dictionary<string, AudioSource> audios;
        private PlayerData _playerData => _progressService.playerData;
        public AudioService(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _audionConfig = staticData.GetAudioConfig();
        }

        public void Cleanup()
        {
            audioContainer = null;
            audios = null;
        }

        public void CreateStartAudio()
        {
            if (_playerData.isMusicOn)
            {
                GetAudio(_audionConfig.GetStartBackAudio, true).Play();
            }
        }

        public void CreateLevelAudio()
        {
            if (_playerData.isMusicOn)
            {
                GetAudio(_audionConfig.GetLevelBackAudio, true).Play();
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

        private void InitAudio()
        {
            if (audioContainer == null || audios == null)
            {
                audioContainer = new GameObject("AudioContainer");

                audios = new();
            }
        }

        private void SetupClip(AudioClip clip, bool loop = false)
        {
            AudioSource audioSource = audioContainer.AddComponent<AudioSource>();
            audioSource.loop = loop;
            audioSource.playOnAwake = false;
            audioSource.clip = clip;
            audios.Add(clip.name, audioSource);
        }

        private AudioSource GetAudio(AudioClip audioClip, bool loop = false)
        {
            InitAudio();

            if (!audios.ContainsKey(audioClip.name))
            {
                SetupClip(audioClip, loop);
            }

            return audios[audioClip.name];
        }
    }
}