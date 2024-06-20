using System;
using UnityEngine;

namespace Picker3D
{
    [Serializable]
    public class Sound
    {
        public AudioClip[] Clips = { };
        public string Tag;
        public float Volume;
        public float Pitch;
        public bool IsLoop;

        [HideInInspector] public AudioSource Source;
    }
}