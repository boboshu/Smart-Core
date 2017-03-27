using System.Collections.Generic;
using Smart.Bindings;
using Smart.Types;
using UnityEngine;

namespace Smart.Managers
{
    [ClearTransform]
    [AddComponentMenu("Smart/Managers/Bindings Manager")]
    public class BindingsManager : MonoBehaviour
    {
        public bool dontDestroyOnLoad = true;
        private const int _UpdatesPerSecond = 20;
        private int _updatesCounter;

        void Awake()
        {
            instance = this;
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (Time.fixedTime < _nextUpdateTime) return;
            _nextUpdateTime = Time.fixedTime + (1f / _UpdatesPerSecond);

            ++_updatesCounter;
            UpdateBindingsList(Binding.UpdatesPerSecond.Twenty);
            
            if (_updatesCounter % 2 != 0) return;
            UpdateBindingsList(Binding.UpdatesPerSecond.Ten);

            if (_updatesCounter % 4 == 0) // Не кратно остальным значениям
                UpdateBindingsList(Binding.UpdatesPerSecond.Five);

            if (_updatesCounter % 10 != 0) return;
            UpdateBindingsList(Binding.UpdatesPerSecond.Two);

            if (_updatesCounter % 20 != 0) return;
            UpdateBindingsList(Binding.UpdatesPerSecond.One);

            if (_updatesCounter % 40 != 0) return;
            UpdateBindingsList(Binding.UpdatesPerSecond.Half);
            
            _updatesCounter = 0; // Reset counter
        }

        void OnDestroy()
        {
            instance = null;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static BindingsManager instance;
        private static readonly List<Binding>[] _bindingsToUpdate = { new List<Binding>(), new List<Binding>(), new List<Binding>(), new List<Binding>(), new List<Binding>(), new List<Binding>() };
        private static float _nextUpdateTime;

        public static void RegisterBinding(Binding binding, Binding.UpdatesPerSecond updatesPerSecond)
        {
            _bindingsToUpdate[(int)updatesPerSecond].Add(binding);
        }

        public static void UnRegisterBinding(Binding binding, Binding.UpdatesPerSecond updatesPerSecond)
        {
            _bindingsToUpdate[(int)updatesPerSecond].Remove(binding);
        }

        private static void UpdateBindingsList(Binding.UpdatesPerSecond updatesPerSecond)
        {
            var list = _bindingsToUpdate[(int)updatesPerSecond];
            for (var i = 0; i < list.Count; i++)
                list[i].UpdateBinding(false);
        }
    }
}
