using System;
using System.Linq;
using System.Reflection;
using Smart.Managers;
using Object = UnityEngine.Object;

namespace Smart.Bindings
{
    public abstract class Binding<T> : Binding
    {
        public UpdatesPerSecond updatesPerSecond = UpdatesPerSecond.Five;

        private T _oldValue;
        private Func<T> _onGetValue;
        private Action<T> _onSetValue;
        private Object _lastSourceObject;
        private static readonly Func<T> _nullGetter = () => default(T);
        private static readonly Action<T> _nullSetter = _ => { };
        private BindingRoot _cachedBindingRoot;

        void Awake()
        {
            BindingsManager.RegisterBinding(this, updatesPerSecond);
        }

        void Start()
        {
            UpdateBinding(true);
        }

        void OnDestroy()
        {
            BindingsManager.UnRegisterBinding(this, updatesPerSecond);
        }

        public void ChangeUpdatesPerSecond(UpdatesPerSecond newUpdatesPerSecond)
        {
            if (updatesPerSecond == newUpdatesPerSecond) return;
            BindingsManager.UnRegisterBinding(this, updatesPerSecond);
            updatesPerSecond = newUpdatesPerSecond;
            BindingsManager.RegisterBinding(this, updatesPerSecond);
        }

        private Func<T> ConstructValueGetter()
        {
            if (sourceObject == null || string.IsNullOrEmpty(sourceMember)) return _nullGetter;

            var t = typeof(T);
            var st = sourceObject.GetType();

            // try to use field
            var f = st.GetFields(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name == sourceMember && t.IsAssignableFrom(x.FieldType));
            if (f != null) return () => (sourceObject == null) ? default(T) : (T) f.GetValue(sourceObject);

            // try to use property
            var p = st.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name == sourceMember && t.IsAssignableFrom(x.PropertyType) && !x.GetIndexParameters().Any());
            if (p != null) return () => (sourceObject == null) ? default(T) : (T)p.GetValue(sourceObject, null);

            return _nullGetter;
        }

        private Action<T> ConstructValueSetter()
        {
            if (sourceObject == null || string.IsNullOrEmpty(sourceMember)) return _nullSetter;

            var t = typeof(T);
            var st = sourceObject.GetType();

            // try to use field
            var f = st.GetFields(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name == sourceMember && t.IsAssignableFrom(x.FieldType));
            if (f != null) return v => f.SetValue(sourceObject, v);

            // try to use property
            var p = st.GetProperties(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(x => x.Name == sourceMember && t.IsAssignableFrom(x.PropertyType) && !x.GetIndexParameters().Any());
            if (p != null) return v => p.SetValue(sourceObject, v, null);

            return _nullSetter;
        }

        public override void UpdateBinding(bool forced)
        {
            PrepareBindingRoot();

            // Construct getter
            if (_onGetValue == null)
                _onGetValue = ConstructValueGetter();

            // Apply binding
            var v = _onGetValue();
            if (IsEquals(v, _oldValue) && !forced) return;
            Apply(_oldValue = v);
        }

        private void PrepareBindingRoot()
        {
            // Apply root if needed
            if (!useBindingRoot) return;

            // cache root, bad when reparent object, but good for performance
            if (_cachedBindingRoot == null)
                _cachedBindingRoot = GetComponentInParent<BindingRoot>();
            sourceObject = _cachedBindingRoot ? _cachedBindingRoot.sourceObject : null;

            // clear source object getter and setter
            if (sourceObject != _lastSourceObject)
            {
                _lastSourceObject = sourceObject;
                _onGetValue = null;
                _onSetValue = null;
            }
        }

        protected abstract void Apply(T value);

        protected abstract bool IsEquals(T v1, T v2);

        public void Set(T value)
        {
            PrepareBindingRoot();

            // Construct setter
            if (_onSetValue == null)
                _onSetValue = ConstructValueSetter();

            // Apply binding
            if (IsEquals(_oldValue, value)) return;
            _onSetValue(_oldValue = value);
            Apply(value);
        }
    }
}
