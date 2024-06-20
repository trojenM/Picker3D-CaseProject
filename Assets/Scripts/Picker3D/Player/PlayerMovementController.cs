using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class PlayerMovementController : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField, Anywhere] private InputReader m_input;
		[SerializeField, Self] private Rigidbody m_parentRigidbody;
		[SerializeField, Self] private Transform m_parentTransform;
		[SerializeField, Child(Flag.Editable)] private Rigidbody m_childRigidBody;
		[SerializeField, Child(Flag.Editable)] private Transform m_childTransform;
		[SerializeField, Self] private PlayerPowerUpSystem m_powerUpController;
		
		[Header("Properties")]
		[SerializeField] private bool m_isPlayerMoveForward = false;
		[SerializeField] private float m_forwardVelocity;
		[SerializeField] private float m_playerDragDistance;
		
		private Camera mainCam;
		private Plane rayPlane;
		private bool isPlayerDraggable = false;
		
		private const float ZeroF = 0f;
		
		public bool IsPlayerMoveForward { get => m_isPlayerMoveForward; 
										set => m_isPlayerMoveForward = value; }
										
		public float ForwardVelocity { get => m_forwardVelocity; 
										set => m_forwardVelocity = value; }
		
		private void OnEnable() 
		{
			m_input.OnPointerDown += OnPointerDown;
			m_input.OnPointerHoldStill += OnPointerHold;
			m_input.OnPointerHoldMove += OnPointerMove;
			m_input.OnPointerUp += OnPointerUp;
			m_input.OnPointerRelease += OnPointerRelease;
		}
		
		private void OnDisable() 
		{
			m_input.OnPointerDown -= OnPointerDown;
			m_input.OnPointerHoldStill -= OnPointerHold;
			m_input.OnPointerHoldMove -= OnPointerMove;
			m_input.OnPointerUp -= OnPointerUp;
			m_input.OnPointerRelease -= OnPointerRelease;
		}
		
		private void Awake() 
		{
			mainCam = Camera.main;
			rayPlane = new Plane(Vector3.down, -m_childTransform.position);	
		}
		
		private void FixedUpdate()
		{
			// Setting forward velocity.
			if (m_isPlayerMoveForward)
				m_parentRigidbody.velocity = new Vector3(ZeroF, ZeroF, m_forwardVelocity);
		}
		
		private void OnPointerDown(Vector3 screenPosition)
		{
			Vector3 worldPosition = CastRayToWorld(screenPosition);
			isPlayerDraggable = CheckPointerOnPlayer(worldPosition);
		}

		private void OnPointerMove(Vector3 screenPosition) 
		{
			if (!isPlayerDraggable)
				return;
			
			Vector3 worldPosition = CastRayToWorld(screenPosition);
			m_childRigidBody.velocity = new Vector3(worldPosition.x - m_childTransform.position.x, ZeroF, ZeroF) * (1 / Time.fixedDeltaTime);
		}
		
		private void OnPointerHold() 
		{
			m_childRigidBody.velocity = Vector3.zero;
		}
		
		private void OnPointerRelease() 
		{
			m_childRigidBody.velocity = Vector3.zero;
		}
		
		private void OnPointerUp(Vector3 screenPosition) 
		{
			m_childRigidBody.velocity = Vector3.zero;
			isPlayerDraggable = false;
		}

		private Vector3 CastRayToWorld(Vector3 screenPosition)
		{
			Ray ray = mainCam.ScreenPointToRay(screenPosition);
			rayPlane.Raycast(ray, out float distance);
			Vector3 worldPosition = ray.GetPoint(distance);
			
			return worldPosition;
		}
		
		private bool CheckPointerOnPlayer(Vector3 pointerWorldPosition) 
		{
			return Vector3.Distance(pointerWorldPosition, m_childTransform.position) <= m_playerDragDistance;
		}
	}
}
