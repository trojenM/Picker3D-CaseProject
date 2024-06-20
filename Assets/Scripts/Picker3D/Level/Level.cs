using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Picker3D
{
	[ExecuteInEditMode]
	[CreateAssetMenu(fileName = "Level", menuName = "Picker3D/Level/New Level")]
	public class Level : ScriptableObject
	{
		[SerializeField] private Vector3 m_startPosition = new Vector3();
		
		[SerializeField] private SerializableKeyValuePairList<GameObject, ObjectData> m_objectsData = new SerializableKeyValuePairList<GameObject, ObjectData>();
		
		[SerializeField] private List<CollectiblePoolProperty> m_collectiblePoolProperties = new List<CollectiblePoolProperty>();
				
		public Vector3 StartPosition { get =>  m_startPosition; }
		
		public SerializableKeyValuePairList<GameObject, ObjectData> ObjectsData 
		{ get => m_objectsData; set => m_objectsData = value; }
		
		public List<CollectiblePoolProperty> CollectiblePoolProperties 
		{ get => m_collectiblePoolProperties; set => m_collectiblePoolProperties = value; }
		
		private void Awake() => EditorUtility.SetDirty(this);
	}
	
	[Serializable]
	public class ObjectData
	{
		[SerializeField] private Vector3 m_position;
		[SerializeField] private Quaternion m_rotation;
		
		public Vector3 Position { get => m_position; }
		public Quaternion Rotation { get => m_rotation; }
		
		public ObjectData() {}

		public ObjectData(Vector3 position, Quaternion rotation)
		{
			m_position = position;
			m_rotation = rotation;
		}
	}
	
	[Serializable]
	public class CollectiblePoolProperty 
	{
		[HideInInspector] [SerializeField] private string m_name;
		[SerializeField] private float m_countCollectiblesTime;
		[SerializeField] private int m_maxCollectiblesCount;
		
		public string Name { get => m_name; }
		public float CountCollectiblesTime { get => m_countCollectiblesTime; }
		public int MaxCollectiblesCount { get => m_maxCollectiblesCount; }
		
		public CollectiblePoolProperty() {}
		
		public CollectiblePoolProperty(float countCollectiblesTime, int maxCollectiblesCount, string elementName = null) 
		{
			m_countCollectiblesTime = countCollectiblesTime;
			m_maxCollectiblesCount = maxCollectiblesCount;
			m_name = elementName;
		}
	}
}
