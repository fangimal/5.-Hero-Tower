using System;
using System.Collections;
using _Scripts.Data;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;

namespace _Scripts.Level
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject rebaseParticlePrefabStart;
        [SerializeField] private LevelHelper levelHelper;
        [SerializeField] private Transform lastSavePosition;
    
        public float distance = 10f;
        public LayerMask layerMask;
        public int GetCheckpointsCount => levelHelper.GetCheckPoints.Length;

        private ThirdPersonController _thirdPersonController;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _persistentProgress;
        private CharacterController characterController;
        private LevelStaticData data;

        private float _startTimer = 2f;
        private float _timer;
        private bool canRebase = true;

        public event Action OnRebasePlayer;

        public void Init(ThirdPersonController thirdPersonController, LevelHelper levelHelper, 
            IPersistentProgressService persistentProgressService, LevelStaticData data)
        {
            this.levelHelper = levelHelper;
            _thirdPersonController = thirdPersonController;
            _timer = _startTimer;
            this.data = data;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _persistentProgress = persistentProgressService;
            SetTargetPosition(_persistentProgress.DataGroup.playerData.checkpointIndex[_persistentProgress.DataGroup.playerData.checkpointIndex.Count-1]);
            characterController = thirdPersonController.gameObject.GetComponent<CharacterController>();
            RebasePlayer(lastSavePosition);
        }

        private void FixedUpdate()
        {
            if (_thirdPersonController.Grounded)
            {
                _timer = _startTimer;
            }

            if (!_thirdPersonController.Grounded && _timer < 0)
            {
                if (!Physics.Raycast(transform.position, Vector3.down, distance, layerMask))
                {
                    RebasePlayer(lastSavePosition);
                    OnRebasePlayer?.Invoke();
                }
            }

            StartFallTimer();
        }

        public void SetTargetPosition(int index)
        {
            if (index > levelHelper.GetCheckPoints.Length)
            {
                index = levelHelper.GetCheckPoints.Length;
            }
            
            if (index >= 0 && data.levelBuildIndex != 1)
            {
                lastSavePosition = levelHelper.GetCheckPoints[index].GetSpawnPoint;
            }
            else
            {
                lastSavePosition = levelHelper.GetStartPosition;
            }

            if (!_persistentProgress.DataGroup.playerData.checkpointIndex.Contains(index))
            {
                _persistentProgress.DataGroup.playerData.checkpointIndex.Add(index);
            }
        
            _saveLoadService.SaveProgress();
        }

        public void SetNextCheckPointAndRebase(int index)
        {
            SetTargetPosition(index);
            RebasePlayer(lastSavePosition);
        }

        public void GetCoins()
        {
            _persistentProgress.DataGroup.playerData.AddCoins(1);
        }

        private void StartFallTimer()
        {
            if (!_thirdPersonController.Grounded)
            {
                _timer -= Time.deltaTime;
            }
        }

        private void RebasePlayer(Transform targetTransform)
        {
            if (canRebase)
            {
                canRebase = false;
                characterController.enabled = false;
                StartCoroutine(StartWait(targetTransform));
            }
        }

        private IEnumerator StartWait(Transform targetTransform)
        {
            gameObject.transform.position = targetTransform.position;
            gameObject.transform.localScale = new Vector3(1,1,1);
            yield return new WaitForFixedUpdate();
            _thirdPersonController.Grounded = true;
            characterController.enabled = true;
            rebaseParticlePrefabStart.gameObject.SetActive(true);
            characterController.SimpleMove(Vector3.zero);
            Physics.SyncTransforms();
            StartCoroutine(CanRebase());
        }

        private IEnumerator CanRebase()
        {
            yield return new WaitForSeconds(2f);
            canRebase = true;
        }

        public void UpdateProgress(DataGroup dataGroup)
        {
            throw new System.NotImplementedException();
        }
    }
}