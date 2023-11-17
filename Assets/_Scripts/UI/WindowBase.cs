using System;
using _Scripts.Data;
using _Scripts.Infrastructure.ADS;
using _Scripts.Infrastructure.Audio;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.UI
{
    public class WindowBase : MonoBehaviour
    {
        protected ThirdPersonController _player;
        protected IGameStateMachine _gameStateMachine;
        protected IPersistentProgressService _progressService;
        protected ISaveLoadService _saveLoadService;
        protected IAdsService _adsService;
        protected IAudioService _audioService;
        protected PlayerData PlayerData => _progressService.playerData;
        protected PlayerStaticData _playerStaticData;
        
        protected virtual void Initialize(bool isMobile)
        {

        }

        protected void OnClickedPlay(AudioClipName audioClipName)
        {
            switch (audioClipName)
            {
                case AudioClipName.Btn:
                    _audioService.PlayAudio(AudioClipName.Btn);
                    break;
                case AudioClipName.Coins:
                    _audioService.PlayAudio(AudioClipName.Coins);
                    break;
                case AudioClipName.Burst:
                    break;
            }
        }
        public void Construct(IGameStateMachine stateMachine, ThirdPersonController player, 
            IPersistentProgressService progressService, IAdsService adsService, IAudioService audioService, bool isMobile = false)
        {
            _gameStateMachine = stateMachine;
            _progressService = progressService;
            _adsService = adsService;
            _player = player;
            _audioService = audioService;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _playerStaticData = player.GetComponent<ThirdPersonController>().PlayerStaticData;
            
            Initialize(isMobile);
        }
    }
}