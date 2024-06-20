using System;
using UnityEditor;
using UnityEngine;

namespace Picker3D
{
	[CreateAssetMenu(fileName = "SphereDetectionStrategy", menuName = "ScriptableObjects/Detections/SphereDetectionStrategy")]
	public class SphereDetectionStrategy : DetectionStrategy
	{
		[SerializeField] private float m_detectionRadius;
		[SerializeField] private Vector3 m_detectionOriginOffset;
		
		public override void DrawGizmos(Transform detector)
		{
			Gizmos.DrawWireSphere(detector.position + m_detectionOriginOffset, m_detectionRadius);
		}

		public override bool Cast(Transform target, Transform detector)
		{
			return IsWithinSphere(target.position, detector.position + m_detectionOriginOffset, m_detectionRadius);
		}
		
		private bool IsWithinSphere(Vector3 point, Vector3 origin, float radius) 
		{
			float distanceToPlayer = Vector3.Distance(point, origin);
			
			return distanceToPlayer <= radius;
		}
	}
}
