using System.Collections.Generic;
using _Scripts.StaticData;
using UnityEngine;

namespace _Scripts.Infrastructure.Audio
{
    public class AudioService : IAudioService
    {
        private GameObject audioContainer;

        private AudioStaticData audionConfig;
        
        private Dictionary<string, AudioSource> audios;
        public AudioService(IStaticDataService staticData)
        {
            audionConfig = staticData.GetAudioConfig();
        }

        public void Cleanup()
        {
            audioContainer = null;
            audios = null;
        }

        public void CreateStartAudio()
        {
            InitAudio();
            SetupClip(audionConfig.GetStartBackAudio, true);
            GetAudio(audionConfig.GetStartBackAudio).Play();
        }

        public void CreateLevelAudio()
        {
            InitAudio();
            SetupClip(audionConfig.GetLevelBackAudio, true);
            GetAudio(audionConfig.GetLevelBackAudio).Play();
        }

        public void PlayAudio(AudioClipName audioType)
        {
            switch (audioType)
            {
                case AudioClipName.Btn:
                    GetAudio(audionConfig.GetUIButton).Play();
                    break;
                case AudioClipName.Coins:
                    GetAudio(audionConfig.GetCoins).Play();
                    break;
                case AudioClipName.Burst:
                    GetAudio(audionConfig.GetBurst).Play();
                    break;               
                case AudioClipName.Teleport:
                    GetAudio(audionConfig.GetTeleport).Play();
                    break;
                default:
                    Debug.Log("Don't find Audio Clip");
                    break;
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

        private AudioSource GetAudio(AudioClip audioClip)
        {
            InitAudio();

            if (!audios.ContainsKey(audioClip.name))
            {
                SetupClip(audioClip);
            }

            return audios[audioClip.name];
        }
    }
}