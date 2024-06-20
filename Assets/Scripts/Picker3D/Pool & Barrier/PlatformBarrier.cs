using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public class PlatformBarrier : ValidatedMonoBehaviour, IOpenable
	{
		[Header("References")]
		[SerializeField, Self] private PlatformBarrierAnimator m_barrierAnimator;

		public void Open()
		{
			m_barrierAnimator.PlayAnimation();
		}
	}
}
