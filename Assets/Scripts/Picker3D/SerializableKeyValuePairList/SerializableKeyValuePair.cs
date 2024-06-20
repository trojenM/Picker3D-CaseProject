using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
    [Serializable]
	public class SerializableKeyValuePair<TKey, TValue>
	{
		[SerializeField] private TKey m_key;
		[SerializeField] private TValue m_value;
	
		public TKey Key { get => m_key; set => m_key = value; }
		public TValue Value { get => m_value; set => m_value = value; }
	
		public SerializableKeyValuePair() { }
	
		public SerializableKeyValuePair(TKey key, TValue value)
		{
			m_key = key;
			m_value = value;
		}
	}
}
