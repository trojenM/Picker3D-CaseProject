using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class PlayerPowerUpSystem : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField] private GameObject m_playerVane;
		[SerializeField] private GameObject m_playerFireBoost;
		[SerializeField] private FillImageUI m_finishPrizeEnergyUI;
		[SerializeField, Self] private Transform m_playerTransform;
		[SerializeField, Self] private PlayerMovementController m_playerMovementController;
		
		[Header("Properties")]
		[SerializeField] private float m_playerSizeMultiplier;
		[SerializeField] private float m_playerForwardVelocityMultiplier;
		[SerializeField] [Range(0f, 1f)] private float m_finishPrizeEnergy;
		
		private Vector3 initialPlayerScale;
		private float initialPlayerForwardVelocity;
		private float initialPlayerScaleMultiplier;
		private float initialPlayerSpeedMultiplier;
		private float collectiblesCountInLevel;
		private CollectibleManager collectibleManager;
		
		public float PlayerScaleMultiplier { get => m_playerSizeMultiplier; }
		public float PlayerForwardVelocityMultiplier { get => m_playerForwardVelocityMultiplier; }
		public float FinishPrizeEnergy { get => m_finishPrizeEnergy; set => m_finishPrizeEnergy = value;}
		
		private void Awake() 
		{
			if (!Application.isEditor) 
			{
				m_playerForwardVelocityMultiplier = 1f;
				m_playerSizeMultiplier = 1f;
			}
			
			initialPlayerScale = transform.localScale;
			initialPlayerForwardVelocity = m_playerMovementController.ForwardVelocity;
			initialPlayerScaleMultiplier = m_playerSizeMultiplier;
			initialPlayerSpeedMultiplier = m_playerForwardVelocityMultiplier;
		}	
		
		private void Start() 
		{
			collectibleManager = ServiceLocator.GetService<CollectibleManager>();
			collectiblesCountInLevel = collectibleManager.Collectibles.Count;
		}
		
		private void OnTriggerEnter(Collider other) 
		{
			if (other.TryGetComponent<Collectible>(out var col))
			{
				if (col.State == CollectibleState.STATIC) 
				{
					
					Debug.Log("Collectible collected!");
				}
			}
		}

		public void VanePowerUp() => m_playerVane.SetActive(true);
		
		public void VanePowerUpEnd() => m_playerVane.SetActive(false);
		
		public void FireBoostPowerUp() => m_playerFireBoost.SetActive(true);
		
		public void FireBoostPowerUpEnd() => m_playerFireBoost.SetActive(false);

		public void ForwardVelocityMultiplierSet(float value)
		{
			m_playerForwardVelocityMultiplier = value;
			UpdatePlayerMovementSpeed();
		}

		public void ForwardVelocityMultiplierReset()
		{
			m_playerForwardVelocityMultiplier = initialPlayerSpeedMultiplier;
			UpdatePlayerMovementSpeed();
		}

		public void ScaleMultiplierSet(float value)
		{
			m_playerSizeMultiplier = value;
			UpdatePlayerTransformScale();
		}

		public void ScaleMultiplierReset()
		{
			m_playerSizeMultiplier = initialPlayerScaleMultiplier;
			UpdatePlayerTransformScale();
		}
		
		public void UpdateFinishPrizeEnergy() 
		{
			m_finishPrizeEnergy += 1f / collectiblesCountInLevel;
			m_finishPrizeEnergyUI.FillAmount = m_finishPrizeEnergy;
		}

		private void UpdatePlayerTransformScale() => transform.localScale = initialPlayerScale * m_playerSizeMultiplier;
		private void UpdatePlayerMovementSpeed() => m_playerMovementController.ForwardVelocity = initialPlayerForwardVelocity * m_playerForwardVelocityMultiplier;
	}
}
