using System;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public interface IReadOnlyCoin
    {
        public event EventHandler<Collider> CollisionPerformed;
    }
}
