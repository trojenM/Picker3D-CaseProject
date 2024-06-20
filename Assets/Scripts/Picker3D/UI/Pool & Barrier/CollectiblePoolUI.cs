using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace Picker3D
{
	public class CollectiblePoolUI : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private TMP_Text m_poolCollectibleCountText;
		[SerializeField] private TMP_Text m_barrierAchievementText;
		[SerializeField] private string[] m_barrierAchievementStrings;
		
		public void UpdatePoolCollectibleCount(int currentCount, int maxCount) 
		{
			m_poolCollectibleCountText.text = $"{currentCount} / {maxCount}";
		}
		
		public void SetRandomBarrierAchievementText() 
		{
			string achievementWord = m_barrierAchievementStrings[Random.Range(0, m_barrierAchievementStrings.Length - 1)];
			m_barrierAchievementText.text = achievementWord;
		}
	}
}
