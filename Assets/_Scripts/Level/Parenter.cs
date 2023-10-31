using UnityEngine;

namespace _Scripts.Level
{
    public class Parenter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.parent = transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.parent = null;
            }
        }
    }
}