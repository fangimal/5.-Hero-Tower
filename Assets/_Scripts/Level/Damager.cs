using UnityEngine;

namespace _Scripts.Level
{
    public class Damager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerSpawner>().OnPlayerIsDamaged();
            }
        }
    }
}