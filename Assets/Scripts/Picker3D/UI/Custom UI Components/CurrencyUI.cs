using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D
{
	public class CurrencyUI : CustomUIComponent
	{
		[Header("References")]
		[SerializeField, Anywhere] private TMP_Text m_currencyText;
		[SerializeField, Anywhere] private Image m_currencyImage;
		
		[Header("Properties")]
		[SerializeField] private int m_currency;
		[SerializeField] private float m_imageExpandTime;
		[SerializeField] private float m_imageExpandSize;
		
		public int Currency 
		{ 
			get => m_currency; 
			set 
			{ 
				m_currency = value;
				Configure(); 
			} 
		}
		
		public override void Configure()
		{
			m_currencyText.text = m_currency.ToString();
			
			if (!Application.isEditor) 
			{
				m_currencyImage.rectTransform.DORewind();
				m_currencyImage.rectTransform.DOPunchScale(Vector3.one * m_imageExpandSize, m_imageExpandTime, 0, 0);	
			}
		}
	}
}
