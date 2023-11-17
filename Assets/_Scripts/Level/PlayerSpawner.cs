using System;
using System.Collections;
using _Scripts.Data;
using _Scripts.Infrastructure.Audio;
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
        [SerializeField] private StarterAssetsInputs starterAssetsInputs;
        [SerializeField] private GameObject rebaseParticlePrefabStart;
        [SerializeField] private LevelHelper levelHelper;
        [SerializeField] private Transform lastSavePosition;

        public float distance = 10f;
        public LayerMask layerMask;
        public int GetCheckpointsCount => levelHelper.GetCheckPoints.Length;

        private ThirdPersonController _thirdPersonController;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _persistentProgress;
        private IAudioService _audioService;
        private CharacterController characterController;
        private LevelStaticData data;

        private float _startTimer = 2f;
        private float _timer;
        private bool canRebase = true;
        public bool playerIsFall = false;
        private int currentCheckpointIndex;

        public event Action OnRebasePlayer;
        private void OnEnable()
        {
            OnRebasePlayer += RebaseStart;
        }

        private void OnDisable()
        {
            OnRebasePlayer -= RebaseStart;
        }

        public void Init(ThirdPersonController thirdPersonController, LevelHelper levelHelper,
            IPersistentProgressService persistentProgressService, LevelStaticData data, IAudioService audioService)
        {
            this.levelHelper = levelHelper;
            _thirdPersonController = thirdPersonController;
            _timer = _startTimer;
            this.data = data;

            _audioService = audioService;

            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _persistentProgress = persistentProgressService;
            SetTargetPosition(
                _persistentProgress.playerData.checkpointIndex[
                    _persistentProgress.playerData.checkpointIndex.Count - 1]);

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
                    if (canRebase)
                    {
                        _audioService.PlayAudio(AudioClipName.Burst);
                        OnRebasePlayer?.Invoke();
                    }
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

            if (!_persistentProgress.playerData.checkpointIndex.Contains(index))
            {
                _persistentProgress.playerData.checkpointIndex.Add(index);
            }

            currentCheckpointIndex = index;

            _saveLoadService.SaveProgress();
        }

        public void RewardGoNextCheckPoint(int index)
        {
            SetTargetPosition(index);
            //RebasePlayer(lastSavePosition);
            RebaseEnd();
        }

        public void GetCoins()
        {
            _persistentProgress.playerData.AddCoins(1);
            _audioService.PlayAudio(AudioClipName.Coins);
        }

        public void OnPlayerIsDamaged()
        {
            OnRebasePlayer?.Invoke();
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
            /*if (canRebase)
            {*/
                canRebase = false;
                characterController.enabled = false;
                StartCoroutine(Rebase(targetTransform));
            /*}*/
        }

        private void RebaseStart()
        {
            playerIsFall = true;
            canRebase = false;
            characterController.enabled = false;
            _thirdPersonController.GetVisualize.gameObject.SetActive(false);
        }

        public void RebaseEnd()
        {
            StartCoroutine(Rebase(lastSavePosition));
            _thirdPersonController.GetVisualize.gameObject.SetActive(true);

            RebaseFX();
        }

        private void RebaseFX()
        {
            if (currentCheckpointIndex != -1)
            {
                levelHelper.GetCheckPoints[currentCheckpointIndex].ShowFx();
            }
            
            _audioService.PlayAudio(AudioClipName.Teleport);
        }

        private IEnumerator Rebase(Transform targetTransform)
        {
            gameObject.transform.position = targetTransform.position;
            gameObject.transform.rotation = targetTransform.rotation;
            gameObject.transform.localScale = Vector3.one;
            starterAssetsInputs.MoveInput(Vector2.zero);
            yield return new WaitForFixedUpdate();
            _thirdPersonController.Grounded = true;
            characterController.enabled = true;
            rebaseParticlePrefabStart.gameObject.SetActive(true);
            characterController.SimpleMove(Vector3.zero);
            Physics.SyncTransforms();
            StartCoroutine(CanRebase());
            playerIsFall = false;
        }

        private IEnumerator CanRebase()
        {
            yield return new WaitForSeconds(1f);
            canRebase = true;
        }
    }
}