using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D
{
	public abstract class DetectionStrategy : ScriptableObject
	{
		public abstract bool Cast(Transform target, Transform detector);		
		public abstract void DrawGizmos(Transform detector);
	}
}