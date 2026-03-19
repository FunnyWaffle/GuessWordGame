using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ThirdPersonGame
{
    public class SceneLoader
    {
        public int MaxSceneId { get; } = 1;

        public event Action SceneLoaded;

        public bool IsSceneExist(int sceneId) =>
            sceneId > 0 && sceneId <= MaxSceneId;

        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
            OnSceneLoaded();
        }

        public async Task LoadSceneAsync(int sceneId)
        {
            await SceneManager.LoadSceneAsync(sceneId);
            OnSceneLoaded();
        }

        private void OnSceneLoaded()
        {
            SceneLoaded?.Invoke();
        }
    }
}
