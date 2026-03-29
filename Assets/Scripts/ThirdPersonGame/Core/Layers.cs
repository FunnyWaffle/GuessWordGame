using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public static class Layers
    {
        private static readonly Dictionary<LayerName, int> _layerIndexes = new()
        {
            [LayerName.RaycastIgnore] = LayerMask.NameToLayer("Raycast Ingnore"),
        };

        public static int GetLayerIndex(LayerName layerName)
        {
            return _layerIndexes[layerName];
        }
    }

    public enum LayerName
    {
        RaycastIgnore,
    }
}
