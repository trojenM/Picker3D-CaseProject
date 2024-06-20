using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace Picker3D
{
	public abstract class CustomUIComponent : ValidatedMonoBehaviour
	{
		protected override void OnValidate() 
		{
			base.OnValidate();
			UpdateUI();
		}
		
		protected void Awake() 
		{
			UpdateUI();
		}
		
		public abstract void Configure();
		
		private void UpdateUI() 
		{
			Configure();
		}
	}
}
