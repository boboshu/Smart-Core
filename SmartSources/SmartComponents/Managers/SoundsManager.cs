using System.Collections.Generic;
using System.Linq;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Managers
{
    public class SoundsManager : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public bool dontDestroyOnLoad = true;
        public static SoundsManager instance;

        void Awake()
        {
            instance = this;
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            instance = null;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        private readonly Stack<AudioSource> _freeAudioSources = new Stack<AudioSource>();
        private readonly LinkedList<AudioSource> _usedAudioSources = new LinkedList<AudioSource>();

        public AudioSource GetAudioSource()
        {
            // Release unused audio sources
            if (_usedAudioSources.Any(a => !a.isPlaying))
            {
                foreach (var a in _usedAudioSources.Where(a => !a.isPlaying).ToArray())
                {
                    _usedAudioSources.Remove(a);
                    _freeAudioSources.Push(a);
                }
            }

            // Get free audio source
            if (_freeAudioSources.Count > 0)
            {
                var a = _freeAudioSources.Pop();
                _usedAudioSources.AddLast(a);
                return a;
            }
            else // Or create new audio source
            {
                var go = new GameObject("AudioSource");
                go.Reparent(gameObject);
                var a = go.AddComponent<AudioSource>();
                _usedAudioSources.AddLast(a);
                return a;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static int PlayingSoundsCount => instance == null ? 0 : instance._usedAudioSources.Count;

        public static void StopAllSounds()
        {
            if (instance == null) return;
            if (instance._usedAudioSources.Count == 0) return;
            instance._usedAudioSources.Do(a => { a.Stop(); instance._freeAudioSources.Push(a); });
            instance._usedAudioSources.Clear();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static AudioSource Play2D(AudioClip sound, float volume = 1, float pitch = 1, bool loop = false)
        {
            if (instance == null || sound == null) return null;
            var a = instance.GetAudioSource();
            a.volume = volume;
            a.pitch = pitch;
            a.spatialBlend = 0; // 2D
            a.clip = sound;
            a.loop = loop;
            a.Play();
            return a;
        }

        public static AudioSource Play3D(AudioClip sound, Vector3 position, float volume = 1, float pitch = 1, float spatialBlend = 1, bool loop = false)
        {
            if (instance == null || sound == null) return null;
            var a = instance.GetAudioSource();
            a.volume = volume;
            a.pitch = pitch;
            a.spatialBlend = spatialBlend;
            a.clip = sound;
            a.loop = loop;
            a.transform.position = position;
            a.Play();
            return a;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
