using Assets.Scripts.ThirdPersonGame.Core.Assets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Spawner
    {
        public async Task<GameObject> SpawnAsync(AssetName assetName, Transform parent = null)
        {
            var asset = AssetReferences.GetAssetReference(assetName);
            var result = Addressables.LoadAssetAsync<GameObject>(asset);
            await result.Task;

            return Object.Instantiate(result.Result, parent);
        }

        public async Task<GameObject> SpawnAsync(AssetName assetName, Vector3 position, Transform parent = null)
        {
            var asset = AssetReferences.GetAssetReference(assetName);
            var result = Addressables.LoadAssetAsync<GameObject>(asset);
            await result.Task;

            return Object.Instantiate(result.Result, position, result.Result.transform.rotation, parent);
        }
    }
}
