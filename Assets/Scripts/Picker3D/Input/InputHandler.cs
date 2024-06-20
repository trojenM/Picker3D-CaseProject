using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
	public class InputHandler : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] InputReader m_inputReader;
		
		private Vector3 previousMousePosition;
		
		void Update()
		{
			OnPointerDown();
			OnPointerHold();
			OnPointerUp();
			OnPointerRelease();
		}
		
		private void OnPointerDown() 
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				previousMousePosition = Input.mousePosition;
				m_inputReader.OnPointerDown?.Invoke(previousMousePosition);	
			}
		}
		
		private void OnPointerUp() 
		{
			if (Input.GetMouseButtonUp(0))
				m_inputReader.OnPointerUp?.Invoke(Input.mousePosition);
		}
		
		private void OnPointerHold() 
		{
			Vector3 currentMousePosition = Input.mousePosition;
			
			if (Input.GetMouseButton(0)) 
			{
				if (previousMousePosition != currentMousePosition) 
				{
					m_inputReader.OnPointerHoldMove?.Invoke(currentMousePosition);
					previousMousePosition = currentMousePosition;	
				}
				else
					m_inputReader.OnPointerHoldStill?.Invoke();
			}
		}
		
		private void OnPointerRelease() 
		{
			if (!Input.GetMouseButton(0)) 
			{
				m_inputReader.OnPointerRelease?.Invoke();
			}
		}
	}
}
