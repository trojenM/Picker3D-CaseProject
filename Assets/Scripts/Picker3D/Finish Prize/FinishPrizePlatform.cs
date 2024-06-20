using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class FinishPrizePlatform : ValidatedMonoBehaviour
	{
		[SerializeField, Self] PlayerDetector m_playerDetector;

		private void OnEnable() => m_playerDetector.OnPlayerDetected += OnPlayerDetected;
		private void OnDisable() => m_playerDetector.OnPlayerDetected -= OnPlayerDetected;

		private void OnPlayerDetected(Transform player) 
		{
			
		}
	}
}
