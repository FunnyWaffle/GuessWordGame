using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private List<CollisionListener> _collisionListeners;

        public event Action CollisionPerformed;

        private void Start()
        {
            foreach (var listener in _collisionListeners)
            {
                listener.CollisionPerformed += HanldeCollision;
            }
        }

        private void HanldeCollision()
        {
            CollisionPerformed?.Invoke();
        }
    }
}
