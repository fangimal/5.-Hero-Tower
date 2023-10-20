using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper BootstrapperPrafab;

        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstrapper>();

            if (bootstraper == null) 
                Instantiate((BootstrapperPrafab));
        }
    }
}