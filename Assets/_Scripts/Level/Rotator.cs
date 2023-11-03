using UnityEngine;

namespace _Scripts.Level
{
    public class Rotator : Parenter
    {
        [SerializeField] private float xRotation = 0;
        [SerializeField] private float yRotation = 0;
        [SerializeField] private float zRotation = 0;

        private void Update()
        {
            transform.Rotate(xRotation,yRotation,zRotation);
        }
    }
}