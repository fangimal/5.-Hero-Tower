using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    public Transform GetSpawnPoint => spawnPoint;

    public int index;

    public event Action<int> OnSet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Find Player");
            OnSet?.Invoke(index);
        }
    }
}
