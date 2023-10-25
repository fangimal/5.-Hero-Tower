using System.Collections;
using _Scripts;
using _Scripts.Infrastructure.Services;
using _Scripts.Infrastructure.Services.PersistentProgress;
using _Scripts.Infrastructure.Services.SaveLoad;
using _Scripts.StaticData;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawner : MonoBehaviour
{
    public float distance = 10f;
    public LayerMask layerMask;
    [SerializeField] private GameObject rebaseParticlePrefab;
    [SerializeField] private GameObject rebaseParticlePrefabStart;

    private ThirdPersonController _thirdPersonController;
    private ISaveLoadService _saveLoadService;
    private IPersistentProgressService _persistentProgress;

    private float _startTimer = 2f;
    private float _timer;
    [SerializeField] private LevelHelper levelHelper;
    [SerializeField] private Transform lastSavePosition;
    private CharacterController characterController;
    private LevelStaticData data;


    public void Init(ThirdPersonController thirdPersonController, LevelHelper levelHelper, IPersistentProgressService persistentProgressService, LevelStaticData data)
    {
        this.levelHelper = levelHelper;
        _thirdPersonController = thirdPersonController;
        _timer = _startTimer;
        this.data = data;
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        _persistentProgress = persistentProgressService;
        SetTargetPosition(_persistentProgress.DataGroup.playerData.checkpointIndex[_persistentProgress.DataGroup.playerData.checkpointIndex.Count-1]);
        characterController = thirdPersonController.gameObject.GetComponent<CharacterController>();
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
            }
        }

        StartFallTimer();
    }

    public void SetTargetPosition(int index)
    {
        Debug.Log("index: " + index + " array: " + _persistentProgress.DataGroup.playerData.checkpointIndex);
        
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

    private void StartFallTimer()
    {
        if (!_thirdPersonController.Grounded)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void RebasePlayer(Transform targetTransform)
    {
        //Instantiate(rebaseParticlePrefab);
        characterController.enabled = false;
        StartCoroutine(StartWait(targetTransform));
        Debug.Log("RebasePlayer");
    }

    private IEnumerator StartWait(Transform targetTransform)
    {
        yield return new WaitForSeconds(.5f);
        characterController.SimpleMove(Vector3.zero);
        gameObject.transform.position = targetTransform.position;
        Instantiate(rebaseParticlePrefabStart);
        characterController.enabled = true;
    }
    
}