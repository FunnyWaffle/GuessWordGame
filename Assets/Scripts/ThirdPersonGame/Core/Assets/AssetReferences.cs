using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core.Assets
{
    public static class AssetReferences
    {
        private static Dictionary<AssetName, AssetReference> _data;

        public static void SetReferences(Dictionary<AssetName, AssetReference> data)
        {
            _data = data;
        }

        public static AssetReference GetAssetReference(AssetName assetName)
        {
            return _data[assetName];
        }
    }
}
