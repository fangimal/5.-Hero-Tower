using _Scripts.Infrastructure.Factory;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using Cinemachine;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Level
{
    public class LevelHelper : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        [SerializeField] private CheckPoint[] checkPoints;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform targetPoint;
        public Transform GetStartPosition => startPoint;
        public CheckPoint[] GetCheckPoints => checkPoints;
        private IPersistentProgressService _persistentProgress;

        private void Start()
        {
            var gameFactory = AllServices.Container.Single<IGameFactory>();
            gameFactory.SetLevelHelper(this);
        }

        public void Initialize(ThirdPersonController player, IPersistentProgressService persistentProgress)
        {
            _camera.Follow = player.CinemachineCameraTarget.transform;
            _persistentProgress = persistentProgress;
            InitCheckPoints();
        }

        private void InitCheckPoints()
        {
            if (checkPoints.Length != 0)
            {
                for (int i = 0; i < checkPoints.Length; i++)
                {
                    bool isContains = _persistentProgress.playerData.checkpointIndex.Contains(i);
                    checkPoints[i].Init(!isContains, i);
                }
            }
        }
    }
}