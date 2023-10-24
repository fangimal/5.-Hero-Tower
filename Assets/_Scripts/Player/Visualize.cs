using UnityEngine;

namespace _Scripts.Player
{
    public class Visualize : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private AudioClip[] _footstepAudioClips;
        private float _footstepAudioVolume;
        private CharacterController _controller;
        private AudioClip _landingAudioClip;
        public Animator GetAnimator => _animator;

        public void Init(AudioClip[] footstepAudioClips, float footstepAudioVolume, CharacterController controller, AudioClip landingAudioClip)
        {
            _footstepAudioClips = footstepAudioClips;
            _footstepAudioVolume = footstepAudioVolume;
            _controller = controller;
            _landingAudioClip = landingAudioClip;
        }
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (_footstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, _footstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(_footstepAudioClips[index], transform.TransformPoint(_controller.center), _footstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(_landingAudioClip, transform.TransformPoint(_controller.center), _footstepAudioVolume);
            }
        }
    }
}