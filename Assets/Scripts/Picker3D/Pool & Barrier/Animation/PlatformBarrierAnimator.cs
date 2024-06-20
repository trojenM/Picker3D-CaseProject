using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class PlatformBarrierAnimator : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Transform[] m_barriers;
		[SerializeField, Child] private RectTransform m_achievementCanvas;

		private Sequence sequence;
		
		private const float ZeroF = 0f;

		private void OnEnable()
		{
			sequence = DOTween.Sequence();
			sequence.Pause();

			foreach (var barrier in m_barriers) sequence.Join(barrier.DOLocalRotate(barrier.eulerAngles + new Vector3(ZeroF, ZeroF, -60f), 1f));
			sequence.Join(m_achievementCanvas.DOScale(Vector3.one, 1f).ChangeStartValue(Vector3.zero));
			sequence.Join(m_achievementCanvas.DOMoveY(6f, 1f).ChangeStartValue(Vector3.zero));
		}

		private void OnDisable() => sequence.Kill();

		public void PlayAnimation()
		{
			sequence?.Play();
		}
	}
}
