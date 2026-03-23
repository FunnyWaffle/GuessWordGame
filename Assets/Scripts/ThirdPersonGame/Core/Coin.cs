using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Coin : IReadOnlyCoin
    {
        private readonly Transform _transform;

        public Coin(Transform transform)
        {
            _transform = transform;
        }

        public event EventHandler<Collider> CollisionPerformed;

        public void Update()
        {
            Rotate();
        }

        public void CollisionEnter(Collider collider)
        {
            CollisionPerformed?.Invoke(this, collider);
            _transform.gameObject.SetActive(false);
        }

        private void Rotate()
        {
            _transform.DORotateQuaternion(_transform.rotation * Quaternion.Euler(0, 1, 0), Time.deltaTime);
        }
    }
}
