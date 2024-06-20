using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class CollectiblePool : ValidatedMonoBehaviour, IOpenable
	{
		[Header("References")]
		[SerializeField, Self] private PlayerDetector m_playerDetector;
		[SerializeField, Self] private CollectiblePoolUI m_collectiblePoolUI;
		[SerializeField, Child(Flag.Editable)] private PlatformBarrier m_barrier;
		[SerializeField, Anywhere] private Transform m_poolGroundTransform;

		[Header("Properties")]
		[SerializeField] private float m_countCollectiblesTime;
		[SerializeField] private int m_maxCollectiblesCount;

		private int currentCollectiblesCount = 0;
		private PlayerMovementController playerMovementController;
		private CollectibleManager collectibleManager;
		private UIScreenManager screenManager;
		
		public float CountCollectiblesTime 
		{ get => m_countCollectiblesTime; set => m_countCollectiblesTime = value; }
		
		public int MaxCollectiblesCount 
		{ get => m_maxCollectiblesCount; set => m_maxCollectiblesCount = value; }

		private void OnEnable() => m_playerDetector.OnPlayerDetected += OnPlayerDetected;
		private void OnDisable() => m_playerDetector.OnPlayerDetected += OnPlayerDetected;

		private void Awake() => RefreshCollectiblePoolUI();

		private void Start()
		{
			collectibleManager = ServiceLocator.GetService<CollectibleManager>();
			screenManager = ServiceLocator.GetService<UIScreenManager>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Collectible>(out Collectible col))
			{
				OnCollectibleEnterPool(col);
			}
		}
		
		public void Open()
		{
			// Elevate pool's ground.
			m_poolGroundTransform.DOLocalMove(Vector3.zero, 1.2f).SetEase(Ease.OutBack).OnComplete((TweenCallback)(() =>
			{
				playerMovementController.IsPlayerMoveForward = true;
				collectibleManager.DestroyDynamicCollectibles();
				m_collectiblePoolUI.SetRandomBarrierAchievementText();
				m_barrier.Open();
			}));
		}

		public void RefreshCollectiblePoolUI() => m_collectiblePoolUI.UpdatePoolCollectibleCount(currentCollectiblesCount, m_maxCollectiblesCount);

		private void OnCollectibleEnterPool(Collectible collectibleObject) 
		{
			currentCollectiblesCount += 1;
			RefreshCollectiblePoolUI();
		}

		private void OnPlayerDetected(Transform player)
		{
			playerMovementController = player.GetComponent<PlayerMovementController>();
			playerMovementController.IsPlayerMoveForward = false;
			
			PlayerPowerUpSystem powerUpController = player.GetComponent<PlayerPowerUpSystem>();
			powerUpController.VanePowerUpEnd();
			
			StartCoroutine(PoolCounter());
		}

		private void CheckCollectibleCountHasReached()
		{
			if (currentCollectiblesCount < m_maxCollectiblesCount)
			{
				screenManager.OnGameFailed();
				return;
			}
			
			collectibleManager.DestroyCollectiblesOnPool();
			Open();
		}
		
		private IEnumerator PoolCounter() 
		{
			yield return new WaitForSeconds(m_countCollectiblesTime);
			CheckCollectibleCountHasReached();
		}
	}
}
