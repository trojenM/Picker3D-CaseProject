using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class FireBoostPowerUp : PowerUp
	{
		[Header("References")]
		[SerializeField, Self] private PlayerDetector m_playerDetector;
		
		private void OnEnable() => m_playerDetector.OnPlayerDetected += OnPlayerDetected;
		private void OnDisable() => m_playerDetector.OnPlayerDetected -= OnPlayerDetected;
		
		public override void ApplyEffect(PlayerPowerUpSystem powerupController)
		{
			StartCoroutine(FireBoostPowerUpCoroutine(powerupController));
		}
		
		private IEnumerator FireBoostPowerUpCoroutine(PlayerPowerUpSystem powerupController)
		{
			powerupController.FireBoostPowerUp();
			powerupController.ForwardVelocityMultiplierSet(2.5f);
			
			while (powerupController.FinishPrizeEnergy > 0f) 
			{
				powerupController.FinishPrizeEnergy -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			
			powerupController.FireBoostPowerUpEnd();
			powerupController.ForwardVelocityMultiplierReset();
		}
		
		private void OnPlayerDetected(Transform player) 
		{
			if (player.TryGetComponent<PlayerPowerUpSystem>(out var powerupController))
			{
				ApplyEffect(powerupController);
			}
			else Debug.LogError("PlayerPowerUpController not found on player.");
		}
	}
}
