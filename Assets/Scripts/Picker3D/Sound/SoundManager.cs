using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Picker3D
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private List<Sound> m_sounds;
		private bool state;
		
		private void OnEnable() => ServiceLocator.Register<SoundManager>(this);
		
		private void OnDisable() => ServiceLocator.UnRegister<SoundManager>();
		
		private void Awake() => Initialize();

		private void Initialize()
		{
			foreach (Sound sound in m_sounds)
			{
				AudioSource source = gameObject.AddComponent<AudioSource>();

				if (sound.Clips.Length == 0)
					continue;

				AudioClip audioClip = sound.Clips[Random.Range(0, sound.Clips.Length)];

				source.clip = audioClip;
				source.pitch = sound.Pitch;
				source.volume = sound.Volume;
				source.loop = sound.IsLoop;

				sound.Source = source;
			}

			state = PlayerPrefs.GetInt(CommonTypes.SOUND_STATE) == 0;
			ChangeState(state);
		}

		public void Play(string tag)
		{
			Sound targetSound = m_sounds.SingleOrDefault(x => x.Tag == tag);
			AudioClip targetClip = null;

			if (targetSound == null)
				return;

			if (targetSound.Clips.Length == 0)
				return;

			targetClip = targetSound.Clips[Random.Range(0, targetSound.Clips.Length)];

			if (targetClip == null)
			{
				return;
			}

			targetSound.Source.PlayOneShot(targetClip);
		}

		public void ChangeState(bool state)
		{
			AudioListener.volume = state ? 1 : 0;
			PlayerPrefs.SetInt(CommonTypes.SOUND_STATE, state ? 0 : 1);
		}
		public bool GetState()
		{
			return state;
		}
	}
}
