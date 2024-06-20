using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "CollectibleSettings", menuName = "ScriptableObjects/Settings/New Collectible Settings", order = 0)]
public class CollectibleSettings : ScriptableObject
{
	[Header("Settings")]
	[SerializeField] private GameObject m_particleOnDestroy;
	[SerializeField] private Color m_particleColorOnDestroy;
	[SerializeField] private string m_soundTagOnEnterPool;
	[SerializeField] private string m_soundTagOnCollideWithPlayer;
	
	public GameObject ParticleOnDestroy { get => m_particleOnDestroy; }
	public Color ParticleColorOnDestroy { get => m_particleColorOnDestroy; }
	public string SoundTagOnEnterPool { get => m_soundTagOnEnterPool; }
	public string SoundTagOnCollideWithPlayer { get => m_soundTagOnCollideWithPlayer; }
}
