using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class Collectible : ValidatedMonoBehaviour, IDestroyable
	{
		[Header("References")]
		[SerializeField, Anywhere] private CollectibleSettings m_collectibleSettings;
		[SerializeField, Self] Transform m_thisTransform;
		
		private CollectibleManager collectibleManager;
		private SoundManager soundManager;
		private ObjectPoolManager objectPoolManager;
		private ParticleManager particleMananger;
		private CollectibleState state = CollectibleState.STATIC;
		
		public CollectibleState State { get => state; }
		
		private void Start() 
		{
			collectibleManager = ServiceLocator.GetService<CollectibleManager>();
			soundManager = ServiceLocator.GetService<SoundManager>();
			objectPoolManager = ServiceLocator.GetService<ObjectPoolManager>();
			particleMananger = ServiceLocator.GetService<ParticleManager>();
			collectibleManager.RegisterCollectible(this);
		}
		
		private void OnCollisionEnter(Collision other) 
		{
			if (other.collider.CompareTag("Player") || other.collider.TryGetComponent<Collectible>(out var _)) 
			{
				if (state == CollectibleState.STATIC) 
				{
					soundManager.Play(m_collectibleSettings.SoundTagOnCollideWithPlayer);
					state = CollectibleState.DYNAMIC;
				}
			}
			
			if (other.collider.CompareTag("CollectiblePool")) 
			{
				if (state != CollectibleState.ONPOOL) 
				{
					soundManager.Play(m_collectibleSettings.SoundTagOnEnterPool);
					state = CollectibleState.ONPOOL;
				}
			}
		}

		public void DestroySelf(bool playParticle = false) 
		{
			objectPoolManager.ReturnToPool(gameObject);
			
			if (!playParticle)
				return;
				
			particleMananger.SpawnParticle(m_collectibleSettings.ParticleOnDestroy, m_thisTransform.position, m_collectibleSettings.ParticleColorOnDestroy);
		}
	}
	
	public enum CollectibleState 
	{
		STATIC,
		DYNAMIC,
		ONPOOL,
	}
}
