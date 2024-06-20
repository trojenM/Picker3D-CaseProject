using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Picker3D
{
	public class UIScreenManager : MonoBehaviour
	{
		[SerializeField] private RectTransform 
		m_startScreenRect, 
		m_inGameScreenRect, 
		m_failScreenRect, 
		m_succesScreenRect;
		
		[SerializeField] private CanvasGroup 
		m_startScreenGroup, 
		m_inGameScreenGroup, 
		m_failScreenGroup, 
		m_succesScreenGroup;
		
		private void OnEnable() => ServiceLocator.Register<UIScreenManager>(this);

		private void OnDisable() => ServiceLocator.UnRegister<UIScreenManager>();
		
		public void OnGameStarted() 
		{
			SwitchCanvasGroup(m_startScreenGroup, m_inGameScreenGroup);
		}
		
		public void OnGameFailed() 
		{
			SwitchCanvasGroup(m_inGameScreenGroup, m_failScreenGroup);
		}
		
		public void OnGameSucces() 
		{
			SwitchCanvasGroup(m_inGameScreenGroup, m_succesScreenGroup);
		}
		
		private void SwitchCanvasGroup(CanvasGroup a, CanvasGroup b, float duration = 0.25F)
		{
			Sequence sequence = DOTween.Sequence();

			if(a != null)
				sequence.Join(a.DOFade(0, duration));
			if(b != null)
				sequence.Join(b.DOFade(1, duration));

			sequence.OnComplete(() =>
			{
				if(a != null)
					a.blocksRaycasts = false;
				if(b != null)
					b.blocksRaycasts = true;
			});

			sequence.Play();
		}

		
	}
}
