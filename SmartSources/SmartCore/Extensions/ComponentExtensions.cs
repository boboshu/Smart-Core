using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Extensions
{
    public static class ComponentExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static void SyncComponent<T>(this T targetComponent, T sourceComponent, bool syncPrivateMembers = true)
            where T : Component
        {
            if (targetComponent == null || sourceComponent == null) return;
            
            var type = typeof(T);
            var flags = BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | BindingFlags.Public;
            if (syncPrivateMembers) flags |= BindingFlags.NonPublic;
            
            foreach (var fld in type.GetFields(flags))
                fld.SetValue(targetComponent, fld.GetValue(sourceComponent));
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static T CopyComponent<T>(this GameObject go, T sourceComponent, bool syncPrivateMembers = true, bool deleteSource = false)
            where T : Component
        {
            if (sourceComponent == null) return null;
            if (sourceComponent is Transform) return null;

            // 1.
            var type = sourceComponent.GetType();
            var isActive = go.activeSelf;
            if (isActive)  go.SetActive(false);
            var cmp = go.AddComponent(type);

            // 2.
            var flags = BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | BindingFlags.Public;
            if (syncPrivateMembers) flags |= BindingFlags.NonPublic;           
            foreach (var fld in type.GetFields(flags))
                fld.SetValue(cmp, fld.GetValue(sourceComponent));
            
            // 3.
            if (deleteSource) Object.Destroy(sourceComponent);
            if (isActive) go.SetActive(true); // to call Awake method after all properties were set

            // 4.            
            var sourceBeh = sourceComponent as Behaviour;
            if (sourceBeh) ((Behaviour)cmp).enabled = sourceBeh.enabled; // restore script Enabled Flag state

            return (T)cmp;
        }

        public static void CopyAllComponents<T>(this GameObject go, GameObject sourceGameObject, bool syncPrivateMembers = true, bool deleteSource = false)
            where T : Component
        {
            var isActive = go.activeSelf;
            if (isActive) go.SetActive(false);

            foreach (var cmp in sourceGameObject.GetComponents<T>())
                CopyComponent(go, cmp, syncPrivateMembers, deleteSource);
            
            if (isActive) go.SetActive(true);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static T GetOrAddComponent<T>(this GameObject go)
            where T : Component
        {
            var cmp = go.GetComponent<T>();
            return cmp == null ? go.AddComponent<T>() : cmp; // can`t use ?? - BUG of compiling in Unity
        }

        public static T GetOrAddComponent<T>(this Component cmp)
            where T : Component
        {
            var go = cmp.gameObject;
            var cmpT = go.GetComponent<T>();
            return cmpT == null ? go.AddComponent<T>() : cmpT; // can`t use ?? - BUG of compiling in Unity
        }

        public static T AddComponent<T>(this Component cmp)
            where T : Component
        {
            return cmp.gameObject.AddComponent<T>();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static void ToggleEnabled(this Behaviour cmp)
        {
            cmp.enabled = !cmp.enabled;
        }

        public static void Enable(this Behaviour cmp)
        {
            cmp.enabled = true;
        }

        public static void Disable(this Behaviour cmp)
        {
            cmp.enabled = false;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}