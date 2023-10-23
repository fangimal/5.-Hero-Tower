using UnityEngine;

namespace _Scripts.Player
{
    public class Visualize : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Animator GetAnimator => _animator;
    }
}