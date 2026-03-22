using System;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class CollisionListener : MonoBehaviour
    {
        public event Action CollisionPerformed;

        private void OnTriggerEnter(Collider other)
        {
            CollisionPerformed?.Invoke();
        }
    }
}
