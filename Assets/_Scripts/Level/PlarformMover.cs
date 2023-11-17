using UnityEngine;

namespace _Scripts.Level
{
    public class PlarformMover : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private Transform moverPlatform;
        [SerializeField] private float speed = 5f;

        private Transform target;
        private Transform start;

        private void Start()
        {
            target = startPosition;
        }

        private void Update()
        {
            moverPlatform.position =
                Vector3.MoveTowards(moverPlatform.position, target.position, speed * Time.deltaTime);

            CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(moverPlatform.position, target.position) < 0.1f)
            {
                target = target == startPosition? endPosition : startPosition;
            }
        }
    }
}