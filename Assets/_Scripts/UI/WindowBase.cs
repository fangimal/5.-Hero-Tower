using _Scripts.Infrastructure.Services;
using UnityEngine;

namespace _Scripts.UI
{
    public class WindowBase : MonoBehaviour
    {
        protected IGameStateMachine gameStateMachine;
        [SerializeField] protected GameObject player;

        protected virtual void Initialize(bool isMobile)
        {
            
        }
        public void Construct(IGameStateMachine stateMachine, GameObject player, bool isMobile = false)
        {
            gameStateMachine = stateMachine;
            this.player = player;

            Initialize(isMobile);
        }
    }
}