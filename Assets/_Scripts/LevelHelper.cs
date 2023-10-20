using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using Cinemachine;
using StarterAssets;
using UnityEngine;

namespace _Scripts
{
    public class LevelHelper : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CheckReseter _reseter;

        private void Awake()
        {
            var gameFactory = AllServices.Container.Single<IGameFactory>();

            gameFactory.OnPlayerCreated += Initialize;
        }

        public void Initialize(ThirdPersonController player)
        {
            _camera.Follow = player.CinemachineCameraTarget.transform;
            _reseter.Initialize(player);
        }
    }
}