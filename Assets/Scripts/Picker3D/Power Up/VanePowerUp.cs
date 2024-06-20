using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class VanePowerUp : PowerUp
	{
		[Header("References")]
		[SerializeField, Self] private PlayerDetector m_playerDetector;
		
		private ObjectPoolManager objectPoolManager;

		private void OnEnable() => m_playerDetector.OnPlayerDetected += OnPlayerDetected;
		private void OnDisable() => m_playerDetector.OnPlayerDetected -= OnPlayerDetected;

		private void Start() => objectPoolManager = ServiceLocator.GetService<ObjectPoolManager>();

		public override void ApplyEffect(PlayerPowerUpSystem powerUpController)
		{
			powerUpController.VanePowerUp();
			DestroySelf();
		}
		
		private void OnPlayerDetected(Transform player) 
		{
			Debug.Log("OnPlayerDetected");
			
			if (player.TryGetComponent<PlayerPowerUpSystem>(out var powerUpController))
			{
				ApplyEffect(powerUpController);
			}
			else Debug.LogError("PlayerPowerUpController not found on player.");
		}

		private void DestroySelf() => objectPoolManager.ReturnToPool(gameObject);
	}

}
