using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D
{
	public class FillImageUI : CustomUIComponent
	{
		[Header("References")]
		[SerializeField, Anywhere] private Image m_fillImage;
		
		[Header("Properties")]
		[SerializeField] [Range(0f, 1f)] private float m_fillAmount;
		
		public float FillAmount 
		{ 
			get => m_fillAmount; 
			set 
			{ 
				m_fillAmount = value;
				Configure(); 
			} 
		}
		
		public override void Configure()
		{
			m_fillAmount = Mathf.Clamp01(m_fillAmount);
			m_fillImage.fillAmount = m_fillAmount;
		}
	}
}
