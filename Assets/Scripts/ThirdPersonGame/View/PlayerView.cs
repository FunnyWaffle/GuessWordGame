using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _horizontalVelocity;
        [SerializeField] private float _horizontalAcceleration;
        [SerializeField] private float _horizontalDeceleration;
        [SerializeField] private float _jumpForce;

        public CharacterController CharacterController => _characterController;
        public float HorizontalVelocity => _horizontalVelocity;
        public float HorizontalAcceleration => _horizontalAcceleration;
        public float HorizontalDeceleration => _horizontalDeceleration;
        public float JumpForce => _jumpForce;
    }
}
