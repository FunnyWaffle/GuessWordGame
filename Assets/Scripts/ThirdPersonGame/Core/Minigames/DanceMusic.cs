using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core.Minigames
{
    public static class DanceMusic
    {
        private static readonly List<AudioClip> _clips = new();

        public static void AddClip(AudioClip audioClip)
        {
            _clips.Add(audioClip);
        }

        public static AudioClip GetRandomClip()
        {
            return _clips[Random.Range(0, _clips.Count)];
        }
    }
}
