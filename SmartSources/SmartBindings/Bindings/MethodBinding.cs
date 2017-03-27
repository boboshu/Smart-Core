using System.Linq;
using System.Reflection;

namespace Smart.Bindings
{
    public abstract class MethodBinding : Binding
    {
        private BindingRoot _cachedBindingRoot;

        public override void UpdateBinding(bool forced)
        {
            // Do Nothing
        }

        protected void PrepareBindingRoot()
        {
            // Apply root if needed
            if (!useBindingRoot) return;

            // cache root, bad when reparent object, but good for performance
            if (_cachedBindingRoot == null)
                _cachedBindingRoot = GetComponentInParent<BindingRoot>();
            sourceObject = _cachedBindingRoot ? _cachedBindingRoot.sourceObject : null;
        }
    }

    public abstract class MethodBinding<T> : MethodBinding
    {
        public void Execute(T param)
        {
            PrepareBindingRoot();

            foreach (var m in sourceObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name == sourceMember && m.ReturnType == typeof(void)))
            {
                var pp = m.GetParameters();
                if (pp.Count() == 1 && pp.First().ParameterType == typeof(T))
                {
                    m.Invoke(sourceObject, new object[] { param });
                    return;
                }
            }
        }
    }
}
