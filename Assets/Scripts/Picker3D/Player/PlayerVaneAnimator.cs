using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using DG.Tweening;

namespace Picker3D
{
	public class PlayerVaneAnimator : ValidatedMonoBehaviour
	{
		[Header("References")]
		[SerializeField, Self] private Transform m_vane;
		
		[Header("Properties")]
		[SerializeField] private Direction m_direction;
		
		private enum Direction { Left = -1, Right = 1, }
		
		private DG.Tweening.Core.TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> tween;
		
		private void OnEnable()
		{
			tween = m_vane.DOLocalRotate(m_vane.eulerAngles + new Vector3(0f, (int)m_direction * 360f, 0), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
		}

		private void OnDisable() => tween.Kill();
	}
}
