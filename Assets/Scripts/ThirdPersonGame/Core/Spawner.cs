using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Spawner
    {
        public async Task<GameObject> SpawnAsync(string prefabName, Transform parent = null)
        {
            var result = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await result.Task;

            return Object.Instantiate(result.Result, parent);
        }

        public async Task<GameObject> SpawnAsync(string prefabName, Vector3 position, Transform parent = null)
        {
            var result = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await result.Task;

            return Object.Instantiate(result.Result, position, result.Result.transform.rotation, parent);
        }
    }
}
