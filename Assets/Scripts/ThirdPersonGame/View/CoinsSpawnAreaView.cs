using System;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.View
{
    public class CoinsSpawnAreaView : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;

        public BoxCollider BoxCollider => _boxCollider;

        public event Action TriggerPerformed;

        private void OnTriggerEnter(Collider other) =>
            TriggerPerformed?.Invoke();
    }
}
