using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core.Assets
{
    [CreateAssetMenu(menuName = nameof(ThirdPersonGame) + "/" + nameof(AssetReferencesData),
        fileName = nameof(AssetReferencesData))]
    public class AssetReferencesData : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<AssetName, AssetReference> _assets;

        public Dictionary<AssetName, AssetReference> Assets => _assets.ToDictionary();
    }

    public enum AssetName
    {
        Remy,
        CameraWithCinemachineBrain,
        FreeLookCinemachine,
        CoinsSpawnArea,
        Coin,
    }
}
