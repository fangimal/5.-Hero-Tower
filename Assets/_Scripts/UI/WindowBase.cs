using _Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class WindowBase : MonoBehaviour
    {
        protected IGameStateMachine gameStateMachine;

        public void Construct(IGameStateMachine stateMachine)
        {
            gameStateMachine = stateMachine;
        }
    }
}