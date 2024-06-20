using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public abstract class PowerUp : ValidatedMonoBehaviour
	{
		public abstract void ApplyEffect(PlayerPowerUpSystem powerupController);
	}
}
