using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEditor;
using UnityEngine;

namespace Picker3D
{
	public class LevelEditor : ValidatedMonoBehaviour
	{
		[SerializeField, Self] private Transform m_thisTransform;
		[SerializeField, Anywhere] private Level[] m_levels;
		
		private void Awake()
		{
			DestroyObjects();
		}
		
		public void SaveLevel(Level level) 
		{
			SerializableKeyValuePairList<GameObject, ObjectData> objectsData = new SerializableKeyValuePairList<GameObject, ObjectData>();
			
			List<CollectiblePoolProperty> collectiblePoolProperties = new List<CollectiblePoolProperty>();
			
			for (int i = 0; i < m_thisTransform.childCount; i++) 
			{
				Transform objectTransform = m_thisTransform.GetChild(i);
				Vector3 position = objectTransform.position;
				Quaternion rotation = objectTransform.rotation;
				GameObject gameObject = objectTransform.gameObject;
				ObjectData objectData = new ObjectData(position, rotation);
				
				if (gameObject.TryGetComponent<CollectiblePool>(out var component)) 
				{
					collectiblePoolProperties.Add(new CollectiblePoolProperty(component.CountCollectiblesTime, component.MaxCollectiblesCount, gameObject.name));
				}
				
				objectsData.List.Add(new SerializableKeyValuePair<GameObject, ObjectData>(PrefabUtility.GetCorrespondingObjectFromSource(gameObject), objectData));
			}
			
			level.ObjectsData.List = objectsData.List;
			level.CollectiblePoolProperties = collectiblePoolProperties;
		}
		
		public void LoadLevel(Level level) 
		{
			DestroyObjects();
			
			for (int i = 0; i < level.ObjectsData.List.Count; i++) 
			{
				GameObject gameObject = level.ObjectsData.List[i].Key;
				ObjectData objectData = level.ObjectsData.List[i].Value;
				
				GameObject instantiatedObject = (GameObject)PrefabUtility.InstantiatePrefab(gameObject, m_thisTransform);
				
				instantiatedObject.transform.position = objectData.Position;
				instantiatedObject.transform.rotation = objectData.Rotation;
			}
		}
		
		public void DestroyObjects() 
		{
			while (m_thisTransform.childCount > 0) 
			{
				DestroyImmediate(m_thisTransform.GetChild(0).gameObject);
			}
		}
	}
}
