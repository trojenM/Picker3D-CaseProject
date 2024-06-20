using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;

namespace Picker3D
{
	public class PlayerDetector : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField, Anywhere] private DetectionStrategy detectionStrategy;
		[SerializeField, Self] private Transform m_thisTransform;
		
		private Transform player;
		private bool isPlayerDetected = false;
		
		public event Action<Transform> OnPlayerDetected;
		
		private void OnDrawGizmos()
		{
			if (detectionStrategy != null) detectionStrategy.DrawGizmos(m_thisTransform);
		}

		private void Start()
		{
		   player = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{		
			if (!isPlayerDetected) 
			{
				if (detectionStrategy.Cast(player, m_thisTransform)) 
				{
					OnPlayerDetected?.Invoke(player);
					isPlayerDetected = true;
				}
			}
		}
	}
}
