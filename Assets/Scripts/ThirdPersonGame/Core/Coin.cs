using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Coin
    {
        private readonly Transform _transform;

        public Coin(Transform transform)
        {
            _transform = transform;
        }

        public void Update()
        {
            Rotate();
        }

        public void CollisionEnter()
        {
            _transform.gameObject.SetActive(false);
        }

        private void Rotate()
        {
            _transform.DORotateQuaternion(_transform.rotation * Quaternion.Euler(0, 1, 0), Time.deltaTime);
        }
    }
}
