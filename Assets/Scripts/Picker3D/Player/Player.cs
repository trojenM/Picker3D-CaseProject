using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Picker3D
{
	public class Player : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private FillImageUI m_energyUI;
		[SerializeField] private CurrencyUI m_currencyUI;
		
		[Header("Properties")]
		[SerializeField] private int m_currency;
		[SerializeField] [Range(0f, 1f)] private float m_energy;
		
		public int Currency
		{
			get => m_currency;
			set
			{
				m_currency = value;
				m_currencyUI.Currency = m_currency;
			}
		}
		public float Energy
		{
			get => m_energy;
			set
			{
				m_energy = value;
				m_energyUI.FillAmount = m_energy;
			}
		}

		private void Awake()
		{
			if (!Application.isEditor)
				m_currency = PlayerPrefs.GetInt(CommonTypes.PLAYER_CURRENCY, 0);
				
			m_energyUI.FillAmount = m_energy;
			m_currencyUI.Currency = m_currency;
		}
	}
}
