using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
	public class ObjectPoolManager : MonoBehaviour
	{
		[Header("Properties")]
		[SerializeField] private int m_poolSize = 10;
		[SerializeField] List<GameObject> m_prefabsToPool;

		private Dictionary<GameObject, List<GameObject>> objectPools;
		
		private void OnEnable() => ServiceLocator.Register<ObjectPoolManager>(this);
		
		private void OnDisable() => ServiceLocator.UnRegister<ObjectPoolManager>();

		private void Awake() => InitializePools();

		private void InitializePools()
		{
			objectPools = new Dictionary<GameObject, List<GameObject>>();

			foreach (var prefab in m_prefabsToPool)
			{
				List<GameObject> pool = new List<GameObject>();

				for (int i = 0; i < m_poolSize; i++)
				{
					GameObject obj = Instantiate(prefab);
					obj.SetActive(false);
					pool.Add(obj);
				}

				objectPools[prefab] = pool;
			}
		}

		public GameObject GetPooledObject(GameObject prefab)
		{
			if (objectPools.TryGetValue(prefab, out List<GameObject> pool))
			{
				foreach (GameObject obj in pool)
				{
					if (!obj.activeInHierarchy)
					{
						obj.SetActive(true);
						obj.transform.SetParent(null);
						return obj;
					}
				}

				// If there are no available objects, grow the pool.
				GameObject newObj = Instantiate(prefab);
				pool.Add(newObj);

				return newObj;
			}

			Debug.LogError($"The GameObject, you are trying to get is not registered on pool. Returning null. {prefab.name}");

			return null;
		}

		public void ReturnToPool(GameObject obj)
		{
			obj.SetActive(false);
			obj.transform.SetParent(transform);
		}
	}
}
