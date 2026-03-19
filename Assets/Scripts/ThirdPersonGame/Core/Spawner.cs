using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public class Spawner
    {
        public async Task SpawnAsync(string prefabName)
        {
            var result = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await result.Task;

            Object.Instantiate(result.Result);
        }

        public async Task<T> SpawnAsync<T>(string prefabName) where T : MonoBehaviour
        {
            var result = Addressables.LoadAssetAsync<GameObject>(prefabName);
            await result.Task;

            var gameObject = Object.Instantiate(result.Result);

            return gameObject.GetComponent<T>();
        }
    }
}
