using StarterAssets;
using UnityEngine;

public class CheckReseter : MonoBehaviour
{
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private int index;

    private const string PLAYERDATA = "DATA";

    private ThirdPersonController player;
    private FallDetected _fallDetected;
    private CharacterController characterController;
    
    public void Initialize(ThirdPersonController player)
    { 
        this.player = player;
        Debug.Log("player: " + player);
        _fallDetected = player.gameObject.GetComponent<FallDetected>();
        _fallDetected.OnEnabled += LoadData;
        characterController = player.gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {
        LoadData();
        SetIndex();
    }

    private void LoadData()
    {
        Debug.Log("LoadData");
        RebasePlayer(SetTargetPosition());
    }

    private Transform SetTargetPosition()
    {
        Transform pos;
        
        if (PlayerPrefs.HasKey(PLAYERDATA))
        {
            pos = checkPoints[PlayerPrefs.GetInt(PLAYERDATA)].GetSpawnPoint;
        }
        else
        {
            pos = startPoint;
        }

        return targetPoint = pos;
    }

    private void SetIndex()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].index = i;
            checkPoints[i].OnSet += SavePosition;
        }
    }

    private void RebasePlayer(Transform targetTransform)
    {
        if (player)
        {
            characterController.enabled = false;
            characterController.SimpleMove(Vector3.zero);
            Debug.Log("velocity: " + characterController.velocity);
            player.gameObject.transform.position = targetTransform.position;
            characterController.enabled = true;
        }
        Debug.Log("RebasePlayer");
    }

    private void SavePosition(int index)
    {
        PlayerPrefs.SetInt(PLAYERDATA, index);
        this.index = index;
    }
}
