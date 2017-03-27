using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Smart.Extensions
{
    public static class PrefabExtensions
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static GameObject CreateInstance(string prefabName)
        {
            var obj = Resources.Load<GameObject>(prefabName);
            if (obj == null) throw new Exception("Prefab not found: " + prefabName);
            return Object.Instantiate(obj);
        }

        public static T CreateInstance<T>(string prefabName) where T : Component
        {
            var cmp = CreateInstance(prefabName).GetComponent<T>();
            if (cmp == null) throw new Exception("Prefab component not found: " + prefabName + " - " + typeof(T).Name);
            return cmp;
        }

        //---------------------------------------------------------------------------------------------------------------------------------

        public static GameObject CreateInstance(this GameObject go)
        {
            if (go == null) return null;
            var inst = Object.Instantiate(go);
            return inst;
        }

        public static T CreateInstance<T>(this T cmp) where T : Component
        {
            if (cmp == null) return null;
            var inst = Object.Instantiate(cmp);
            return inst;
        }

        //---------------------------------------------------------------------------------------------------------------------------------

        public static GameObject CreateInstance(this GameObject go, GameObject parent)
        {
            if (go == null) return null;
            var inst = Object.Instantiate(go);
            if (parent) inst.Reparent(parent);
            return inst;
        }

        public static T CreateInstance<T>(this T cmp, GameObject parent) where T : Component
        {
            if (cmp == null) return null;
            var inst = Object.Instantiate(cmp);
            if (parent) inst.Reparent(parent);
            return inst;
        }

        //---------------------------------------------------------------------------------------------------------------------------------

        public static GameObject CreateInstance(this GameObject go, Component parent)
        {
            if (go == null) return null;
            var inst = Object.Instantiate(go);
            if (parent) inst.Reparent(parent);
            return inst;
        }

        public static T CreateInstance<T>(this T cmp, Component parent) where T : Component
        {
            if (cmp == null) return null;
            var inst = Object.Instantiate(cmp);
            if (parent) inst.Reparent(parent);
            return inst;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}