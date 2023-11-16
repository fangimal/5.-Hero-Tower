using System;
using _Scripts.Infrastructure.Audio;
using UnityEngine;

namespace _Scripts.Level
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform pointVisual;
        [SerializeField] private Collider collider;
        [SerializeField] private Transform fxRebase;
        [SerializeField] private Transform fxMoney;
        public Transform GetSpawnPoint => spawnPoint;
        public int GetIndex => index;

        private int index;

        private void Awake()
        {
            fxRebase.gameObject.SetActive(false);
            fxMoney.gameObject.SetActive(false);
        }

        public void Init(bool isActive, int index)
        {
            this.index = index;

            pointVisual.gameObject.SetActive(isActive);
            collider.enabled = isActive;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerSpawner playerSpawner = other.GetComponent<PlayerSpawner>();
                playerSpawner.GetCoins();
                playerSpawner.SetTargetPosition(GetIndex);
                pointVisual.gameObject.SetActive(false);
                collider.enabled = false;
                fxMoney.gameObject.SetActive(true);
            }
        }

        public void ShowFx()
        {
            fxRebase.gameObject.SetActive(true);
        }
    }
}
