using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Spawner
    {
        public async Task<GameObject> SpawnAsync(string prefabName)
        {
            var result = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await result.Task;

            return Object.Instantiate(result.Result);
        }
    }
}
