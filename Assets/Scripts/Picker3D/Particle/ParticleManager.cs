using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class ParticleManager : MonoBehaviour
	{
		private ObjectPoolManager objectPoolManager;

        private void OnEnable() => ServiceLocator.Register<ParticleManager>(this);

        private void OnDisable() => ServiceLocator.UnRegister<ParticleManager>();

        private void Start() => objectPoolManager = ServiceLocator.GetService<ObjectPoolManager>();

        public void SpawnParticle(GameObject particle, Vector3 position, Color? color = null) 
		{
			GameObject spawnedParticle = objectPoolManager.GetPooledObject(particle);
			spawnedParticle.transform.position = position;
			
			ParticleSystem particleSystem = spawnedParticle.GetComponent<ParticleSystem>();
			
			// Set color.
			var main = particleSystem.main;
			main.startColor = color ?? Color.white;
			
			StartCoroutine(WaitParticleLifeSpan(particleSystem));
		}
		
		private IEnumerator WaitParticleLifeSpan(ParticleSystem particleSystem) 
		{
			yield return new WaitForSeconds(particleSystem.main.duration);
			DestroyParticle(particleSystem.gameObject);
		}
		
		private void DestroyParticle(GameObject particle) 
		{
			objectPoolManager.ReturnToPool(particle);
		}
	}
}
