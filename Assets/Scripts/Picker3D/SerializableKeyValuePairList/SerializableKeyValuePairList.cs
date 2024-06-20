using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
    [Serializable]
    public class SerializableKeyValuePairList<TKey, TValue>
    {
        [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> m_keyValuePairList = new List<SerializableKeyValuePair<TKey, TValue>>();

        public List<SerializableKeyValuePair<TKey, TValue>> List
        {
            get => m_keyValuePairList; set => m_keyValuePairList = value;
        }
    }
}
