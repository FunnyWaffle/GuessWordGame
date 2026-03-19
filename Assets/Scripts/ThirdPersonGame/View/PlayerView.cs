using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _velocity;

        public CharacterController CharacterController => _characterController;
        public float Velocity => _velocity;
    }
}
