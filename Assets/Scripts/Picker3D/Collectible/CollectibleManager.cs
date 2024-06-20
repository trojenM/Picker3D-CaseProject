using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Picker3D
{
	public class CollectibleManager : MonoBehaviour
	{
	 	private List<Collectible> collectibles = new List<Collectible>();
		private List<Collectible> garbageCollectibles = new List<Collectible>();
		
		public List<Collectible> Collectibles { get => collectibles; }
		
		private void OnEnable() => ServiceLocator.Register<CollectibleManager>(this);
		private void OnDisable() => ServiceLocator.UnRegister<CollectibleManager>();
		
		public void RegisterCollectible(Collectible collectible) 
		{
			collectibles.Add(collectible);
		}

		public void DestroyDynamicCollectibles() 
		{
			for (int i = 0; i < collectibles.Count; i++) 
			{
				Collectible col = collectibles[i];
				if (col.State == CollectibleState.DYNAMIC) 
					MoveCollectibleToGarbage(col);
			}
			
			DestroyCollectiblesOnGarbage();
		}
		
		public void DestroyCollectiblesOnPool() 
		{
			for (int i = 0; i < collectibles.Count; i++) 
			{
				Collectible col = collectibles[i];
				if (col.State == CollectibleState.ONPOOL) 
					MoveCollectibleToGarbage(col);
			}
			
			DestroyCollectiblesOnGarbage(true);
		}
		
		private void MoveCollectibleToGarbage(Collectible col) 
		{
			//collectibles.Remove(col);
			garbageCollectibles.Add(col);
		}

		private void DestroyCollectiblesOnGarbage(bool playParticle = false) 
		{
			foreach (Collectible col in garbageCollectibles) 
				col.DestroySelf(playParticle);
			
			garbageCollectibles.Clear();
		}
	}
}
