using System;
using System.Collections;
using UnityEngine;

namespace _Scripts.Level
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private Transform[] fxes;
        
        private bool isFinish = true;

        private void Awake()
        {
            foreach (var fx in fxes)
            {
                fx.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isFinish)
            {
                isFinish = false;
                other.GetComponent<PlayerSpawner>().TrigerSend("Finish", "Finish");
                StartCoroutine(StartFirework());
            }
        }

        private IEnumerator StartFirework()
        {
            yield return new WaitForSeconds(0.5f);

            foreach (var fx in fxes)
            {
                fx.gameObject.SetActive(true);
            }
        }
    }
}