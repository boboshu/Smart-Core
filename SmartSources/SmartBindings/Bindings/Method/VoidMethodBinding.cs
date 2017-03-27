using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Smart.Bindings.Method
{
    [AddComponentMenu("Smart/Bindings/Methods/Void Method Binding")]
    public class VoidMethodBinding : MethodBinding
    {
        public void Execute()
        {
            PrepareBindingRoot();

            foreach (var m in sourceObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name == sourceMember && m.ReturnType == typeof(void)))
            {
                var pp = m.GetParameters();
                if (pp.Any()) continue;
                m.Invoke(sourceObject, null);
                return;
            }
        }
    }
}
