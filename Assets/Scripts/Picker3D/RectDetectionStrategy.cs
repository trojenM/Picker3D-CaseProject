using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
	[CreateAssetMenu(fileName = "RectDetectionStrategy", menuName = "ScriptableObjects/Detections/RectDetectionStrategy")]
	public class RectDetectionStrategy : DetectionStrategy
	{
		[SerializeField] private Vector3 m_detectionOriginOffset;
		[SerializeField] private Vector3 m_size;
				
		public override void DrawGizmos(Transform detector)
		{
			Gizmos.DrawWireCube(detector.position + m_detectionOriginOffset, m_size);
		}
		
		public override bool Cast(Transform target, Transform detector)
		{
			Debug.Log(IsWithinRectangle(target.position, detector.position + m_detectionOriginOffset, m_size));
			return IsWithinRectangle(target.position, detector.position + m_detectionOriginOffset, m_size);
		}
		
		private bool IsWithinRectangle(Vector3 point, Vector3 origin, Vector3 size)
		{
			Vector3 halfSize = size / 2f;
			Vector3 lowerBound = origin - halfSize;
			Vector3 upperBound = origin + halfSize;

			return point.x >= lowerBound.x && point.x <= upperBound.x &&
				   point.y >= lowerBound.y && point.y <= upperBound.y &&
				   point.z >= lowerBound.z && point.z <= upperBound.z;
		}
	}
}
