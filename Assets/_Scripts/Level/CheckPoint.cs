using UnityEngine;

namespace _Scripts.Level
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform pointVisual;
        [SerializeField] private Collider collider;
        public Transform GetSpawnPoint => spawnPoint;
        public int GetIndex => index;

        private int index;

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
                other.GetComponent<PlayerSpawner>().SetTargetPosition(GetIndex);
                pointVisual.gameObject.SetActive(false);
                collider.enabled = false;
            }
        }
    }
}
