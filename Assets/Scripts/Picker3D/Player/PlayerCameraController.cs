using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class PlayerCameraController : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField, Self] private Transform m_thisTransform;
		[SerializeField, Anywhere] private Transform m_playerTransform;

		[Header("Properties")]
		[SerializeField] private Vector3 m_cameraPositionOffset;
		[SerializeField] private Vector3 m_cameraLookAngleOffset;

		private const float ZeroF = 0f;

		private void LateUpdate()
		{
			CameraMovementHandler();
		}

		private void CameraMovementHandler()
		{
			// Camera Position
			m_thisTransform.position = m_playerTransform.position - m_playerTransform.forward * m_cameraPositionOffset.z + new Vector3(m_cameraPositionOffset.x, m_cameraPositionOffset.y, ZeroF);

			// Camera Rotation
			m_thisTransform.LookAt(m_playerTransform.position + m_cameraLookAngleOffset);
		}
	}
}
