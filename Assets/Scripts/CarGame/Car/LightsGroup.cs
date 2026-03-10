using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Car
{
    public class LightsGroup : MonoBehaviour
    {
        [SerializeField] private List<Light> _lights;
        [SerializeField] private List<LensFlareComponentSRP> _flares;

        public void SetActiveLights(bool isActive)
        {
            foreach (var light in _lights)
            {
                light.enabled = isActive;
            }
            foreach (var flare in _flares)
            {
                flare.enabled = isActive;
            }
        }
    }
}
