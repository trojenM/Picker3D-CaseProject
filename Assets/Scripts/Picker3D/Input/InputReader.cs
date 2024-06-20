using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/Input/New InputReader", order = 0)]
public class InputReader : ScriptableObject
{
	public Action<Vector3> OnPointerDown;
	public Action<Vector3> OnPointerUp;
	public Action<Vector3> OnPointerHoldMove;
	public Action OnPointerHoldStill;
	public Action OnPointerRelease;
}
