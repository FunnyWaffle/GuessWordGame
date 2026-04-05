using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Animator
    {
        private readonly Dictionary<AnimatorState, AnimatorController> _states;
        private readonly UnityEngine.Animator _animator;

        public Animator(UnityEngine.Animator animator,
            Dictionary<AnimatorState, AnimatorController> states)
        {
            _animator = animator;
            _states = states;
        }

        public bool TrySetAnimatorState(AnimatorState animatorState)
        {
            if (!_states.TryGetValue(animatorState, out var state))
            {
                Debug.Log($"Animator {_animator} has no animation {animatorState}.");
                return false;
            }

            _animator.runtimeAnimatorController = state;
            return true;
        }

        public void SetRootMotion(bool isEnable)
        {
            _animator.applyRootMotion = isEnable;
        }

        public void SetFloat(string parameterName, float value) =>
            _animator.SetFloat(parameterName, value);

        public void SetFloat(int parameterIndex, float value) =>
            _animator.SetFloat(parameterIndex, value);

        public void SetBool(string parameterName, bool value) =>
            _animator.SetBool(parameterName, value);

        public void SetBool(int parameterIndex, bool value) =>
            _animator.SetBool(parameterIndex, value);

        public void SetTrigger(string parameterName) =>
            _animator.SetTrigger(parameterName);

        public void SetTrigger(int parameterIndex) =>
            _animator.SetTrigger(parameterIndex);
    }

    public enum AnimatorState
    {
        Default,
        Dance,
    }
}
