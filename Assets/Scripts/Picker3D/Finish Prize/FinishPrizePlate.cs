using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Picker3D
{
	public class FinishPrizePlate : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private TMP_Text m_prizeText;
		
		[Header("Properties")]
		[SerializeField] private int m_prizeAmount;
		
		public int PrizeAmount { get => m_prizeAmount; set 
		{
			m_prizeAmount = value;
			m_prizeText.text = m_prizeAmount.ToString();
		}}
		
		private void Awake() 
		{
			PrizeAmount = m_prizeAmount;
		}
		
		
	}
}
