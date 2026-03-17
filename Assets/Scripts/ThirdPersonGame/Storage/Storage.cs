using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame
{
    public static class Storage
    {
        public static readonly string UIDataPath = Path.Combine(Application.streamingAssetsPath, "data/UI.save");

        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public static async Task<T> Load<T>(T saveDataByDefault, string filePath)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (!File.Exists(filePath))
                {
                    if (saveDataByDefault != null)
                    {
                        await Save(saveDataByDefault, filePath);

                    }
                    return saveDataByDefault;
                }

                var json = File.ReadAllText(filePath);
                var savedData = JsonConvert.DeserializeObject<T>(json);

                return savedData == null ? saveDataByDefault : savedData;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public static async Task Save<T>(T saveData, string filePath, JsonSerializerSettings settings = null)
        {
            await _semaphore.WaitAsync();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                var json = JsonConvert.SerializeObject(saveData, settings);
                File.WriteAllText(filePath, json);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
