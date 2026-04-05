using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class VFX
    {
        private readonly List<GameObject> _effects = new();

        public VFX(params GameObject[] effects)
        {
            _effects = effects.ToList();
        }

        public void Enable()
        {
            foreach (var effect in _effects)
            {
                effect.SetActive(true);
            }
        }

        public void Disable()
        {
            foreach (var effect in _effects)
            {
                effect.SetActive(false);
            }
        }
    }
}
