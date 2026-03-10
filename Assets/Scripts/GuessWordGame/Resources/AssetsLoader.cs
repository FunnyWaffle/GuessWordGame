using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.GuessWordGame.Resources
{
    public static class AssetsLoader
    {
        public static async Task<GameObject> LoadAsync(string assetId)
        {
            var task = Addressables.LoadAssetAsync<GameObject>(assetId);
            var gameObject = await task.Task;
            return gameObject;
        }
    }
}
