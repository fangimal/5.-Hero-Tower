using _Scripts.Data;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using StarterAssets;
using UnityEngine;

namespace _Scripts.UI
{
    public class WindowBase : MonoBehaviour
    {
        protected ThirdPersonController player;
        protected IGameStateMachine gameStateMachine;
        protected IPersistentProgressService ProgressService;
        protected ISaveLoadService _saveLoadService;
        protected PlayerData PlayerData => ProgressService.DataGroup.playerData;

        protected virtual void Initialize(bool isMobile)
        {
            
        }
        public void Construct(IGameStateMachine stateMachine, ThirdPersonController player, 
            IPersistentProgressService progressService, bool isMobile = false)
        {
            gameStateMachine = stateMachine;
            ProgressService = progressService;
            this.player = player;
        
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            Initialize(isMobile);
        }
    }
}