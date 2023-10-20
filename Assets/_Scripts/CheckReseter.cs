using StarterAssets;
using UnityEngine;

public class CheckReseter : MonoBehaviour
{
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private Transform startPoint;

    private const string PLAYERDATA = "DATA";

    private ThirdPersonController player;
    private FallDetected _fallDetected;

    public void Initialize(ThirdPersonController player)
    {
        this.player = player;
        _fallDetected = player.gameObject.GetComponent<FallDetected>();
        _fallDetected.OnEnabled += LoadData;
    }

    private void Start()
    {
        //LoadData();
        SetIndex();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(PLAYERDATA))
        {
            RebasePlayer(checkPoints[PlayerPrefs.GetInt(PLAYERDATA)].GetSpawnPoint);
        }
        else
        {
            RebasePlayer(startPoint);
        }
        
        _fallDetected.OnCharacterController();
    }

    private void SetIndex()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].index = i;
            checkPoints[i].OnSet += SavePosition;
        }
    }

    public void RebasePlayer(Transform targetTransform)
    {
        player.gameObject.transform.position = targetTransform.position;
        
    }

    private void SavePosition(int index)
    {
        PlayerPrefs.SetInt(PLAYERDATA, index);
    }
}
