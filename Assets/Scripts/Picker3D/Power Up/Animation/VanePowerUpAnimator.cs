using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Picker3D
{
	public class VanePowerUpAnimator : MonoBehaviour
	{
		[Header("References")]
		[SerializeField] private Transform m_vane;
		
		private DG.Tweening.Core.TweenerCore<Quaternion, Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> tween;
		
		private void OnEnable()
		{
			tween = m_vane.DOLocalRotate(m_vane.eulerAngles + new Vector3(0f, 0f, -360f), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
		}

		private void OnDisable() => tween.Kill();
	}
}
