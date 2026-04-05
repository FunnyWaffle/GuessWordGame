using Assets.Scripts.ThirdPersonGame.Core;
using System;
using UnityEditor.Animations;
using UnityEngine;
using Animator = UnityEngine.Animator;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _movementVelocity;
        [SerializeField] private float _horizontalAcceleration;
        [SerializeField] private float _horizontalDeceleration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private SerializableDictionary<Core.AnimatorState, AnimatorController> _animatorStates;
        [SerializeField] private GameObject[] _vfxEffects;
        [SerializeField] private AudioClip[] _danceClips;
        [SerializeField] private AudioSource _audioSource;

        public CharacterController CharacterController => _characterController;
        public Animator Animator => _animator;
        public float MovementVelocity => _movementVelocity;
        public float MovementAcceleration => _horizontalAcceleration;
        public float MovementDeceleration => _horizontalDeceleration;
        public float JumpForce => _jumpForce;
        public SerializableDictionary<Core.AnimatorState, AnimatorController> AnimatorStates => _animatorStates;
        public GameObject[] VFXEffects => _vfxEffects;
        public AudioClip[] DanceClips => _danceClips;
        public AudioSource AudioSource => _audioSource;


        public event Action<float> MovementVelocityChanged;
        public event Action<float> HorizontalAccelerationChanged;
        public event Action<float> HorizontalDecelerationChanged;
        public event Action<float> JumpForceChanged;

        private void OnValidate()
        {
            MovementVelocityChanged?.Invoke(_movementVelocity);
            HorizontalAccelerationChanged?.Invoke(_horizontalAcceleration);
            HorizontalDecelerationChanged?.Invoke(_horizontalDeceleration);
            JumpForceChanged?.Invoke(_jumpForce);
        }
    }
}
