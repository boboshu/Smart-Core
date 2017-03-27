using UnityEngine;

namespace Smart.Bindings.Values
{
    [AddComponentMenu("Smart/Bindings/Values/Audio Binding")]
    public class AudioBinding : Binding<AudioClip>
    {
        public enum Kind { None, AudioSource }
        public Kind kind;

        public AudioSource audioSource;

        protected override void Apply(AudioClip value)
        {
            switch (kind)
            {
                case Kind.AudioSource:
                    if (audioSource)
                    {
                        audioSource.clip = value;
                        audioSource.enabled = (value != null);
                    }
                    break;
            }
        }

        protected override bool IsEquals(AudioClip v1, AudioClip v2)
        {
            return v1 == v2;
        }
    }
}
