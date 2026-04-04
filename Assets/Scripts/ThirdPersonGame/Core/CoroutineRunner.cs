using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    public static class CoroutineRunner
    {
        private static MonoBehaviour _coroutineStarter;

        public static void SetCoroutineStarter(MonoBehaviour gameObject)
        {
            _coroutineStarter = gameObject;
        }

        public static Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return _coroutineStarter.StartCoroutine(enumerator);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            _coroutineStarter.StopCoroutine(coroutine);
        }
    }
}
