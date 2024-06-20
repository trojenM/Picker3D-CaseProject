using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Picker3D
{
	public class LevelManager : ValidatedMonoBehaviour
	{
		[Header("Properties")]
		[SerializeField] private int m_levelIndex;
		
		[Header("References")]
		[SerializeField, Self(Flag.Editable)] private Transform m_levelParent;
		[SerializeField] private Level[] m_levels;
		
		private Dictionary<Level, List<GameObject>> initializedLevels = new Dictionary<Level, List<GameObject>>();
		private ObjectPoolManager objectPoolManager;
		
		public Level PreviousLevel { get => m_levels[m_levelIndex - 1]; }
		public Level CurrentLevel { get => m_levels[m_levelIndex]; }
		
		private void Awake() 
		{
			if (!Application.isEditor)
				m_levelIndex = PlayerPrefs.GetInt(CommonTypes.LEVEL_INDEX, 0);
		}

		private void Start()
		{
			// This line of code must be on top.
			objectPoolManager = ServiceLocator.GetService<ObjectPoolManager>();
			
			InitializeLevel(CurrentLevel);
		}

		public void InitializeLevel(Level level) 
		{
			List<GameObject> instantiatedObjects = new List<GameObject>();
			int currentCollectiblePoolIndex = 0;
			
			foreach (var data in level.ObjectsData.List) 
			{
				GameObject gameObject = data.Key;
				ObjectData objectData = data.Value;
				
				GameObject instantiatedObject = objectPoolManager.GetPooledObject(gameObject);
				instantiatedObject.transform.position = objectData.Position;
				instantiatedObject.transform.rotation = objectData.Rotation;
				
				// Try to set Collectible Pools property.
				if (instantiatedObject.TryGetComponent<CollectiblePool>(out var component)) 
				{
					CollectiblePoolProperty property = level.CollectiblePoolProperties[currentCollectiblePoolIndex];
					
					component.name = property.Name;
					component.CountCollectiblesTime = property.CountCollectiblesTime;
					component.MaxCollectiblesCount = property.MaxCollectiblesCount;
					component.RefreshCollectiblePoolUI();
					currentCollectiblePoolIndex += 1;
				}
				
				instantiatedObjects.Add(instantiatedObject);
			}
			
			initializedLevels.Add(level, instantiatedObjects);
		}
		
		public void DestroyLevel(Level level) 
		{
			if (initializedLevels.TryGetValue(level, out var objects))
			{
				foreach (GameObject gameObject in objects) 
					objectPoolManager.ReturnToPool(gameObject);
				
				initializedLevels.Remove(level);
			}
			
			Debug.LogError($"The level that you are trying to destroy is not already initialized {level.name}");
		}
				
		public void ReInitializeLevel(Level level) 
		{
			DestroyLevel(level);
			InitializeLevel(level);
		}
		
		public void SetNextLevelIndex() 
		{
			m_levelIndex += 1;
			PlayerPrefs.SetInt(CommonTypes.LEVEL_INDEX, m_levelIndex);
		}
	}
}
