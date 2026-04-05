using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ThirdPersonGame.Core
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<SerializableKeyValuePair<TKey, TValue>> _elements;

        public Dictionary<TKey, TValue> ToDictionary()
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (var element in _elements)
            {
                dictionary[element.Key] = element.Value;
            }

            return dictionary;
        }
    }
}
